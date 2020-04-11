using System;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using DataManagerUtils;
using System.Linq;
using System.Net.Http;


/// <summary>
/// This class handles the generation of environment meshes.
/// </summary>
public class AppHandler : MonoBehaviour
{

    /// <summary>
    /// The first function called at the start of running the application. 
    /// Starts by building the mesh tiles that compose the starting location.
    /// </summary>
    /// 
    private HashSet<Tuple<int, int>> active;
    private List<GameObject> worldTiles;
    private int worldSize;
    private UnityEngine.Object pPrefab;
    private DateTime prevtime;
    private int counter;

    AppHandler()
    {
        counter = 0;
        worldSize = 1000;
        active = new HashSet<Tuple<int, int>>();
        prevtime = DateTime.Now;
        worldTiles = new List<GameObject>();
    }

    void Start()
    {
        ////Future Work: Query starting location from user via menu.

        ////The starting environment will be size^2 tiles in a square shape
        //int size = 2;

        string[] lines = System.IO.File.ReadAllLines(@"Assets/config");
        string apikey = lines[0].Substring(17, lines[0].Length - 18);
        Globals.BingAPIKey = apikey;

        pPrefab = Resources.Load("Prefabs/Tile");

        for (int i = 0; i < worldSize; i++)
        {
            worldTiles.Add((GameObject)GameObject.Instantiate(pPrefab, Vector3.one, Quaternion.identity));
        }

        Thread t = new Thread(new ThreadStart(tilePredictor));
        t.Start();

        ////Build the starting area
        ////for (int i = -2; i < size; i++)
        ////{
        ////    for (int j = -2; j < size; j++)
        ////    {
        ////        GameObject Obj = (GameObject)GameObject.Instantiate(pPrefab, new Vector3((i * 256 - i), 0, (j) - (j) * 256), Quaternion.identity);
        ////        Obj.name = "Tilex" + Convert.ToString(i) + "y" + Convert.ToString(j);
        ////        Obj.transform.localScale = new Vector3(256 / 10 + 1, 256 / 10 + 1, 256 / 10 + 1);
        ////    }
        ////}

        //PositionObserver po = new PositionObserver();
        //Thread t = new Thread(new ThreadStart(po.update));
        //t.Start();
    }

    void Update()
    {
        StartCoroutine(tileMonitor());
    }

    async public void tilePredictor()
    {
        //Create a cache object to do a database check
        Cache cache = new Cache();
        IMapResources BMR = new BingMapResources();


        while(true)
        {


            // Check current position
            Vector3 currpos = Globals.position;
            int x = Convert.ToInt32(Math.Round(currpos[0] / 32));
            int z = Convert.ToInt32(Math.Round(currpos[2] / 32));

            //Check for tiles within needed range
            //We want all tiles in a 3x3 range
            for (int i = x - 7; i < x + 7; i++)
            {
                for (int j = z - 7; j < z + 7; j++)
                {
                        //Check if current tile exists
                        Tuple<int, int> tempTuple = new Tuple<int, int>(i, j);
                        if (!active.Contains(tempTuple))
                        {
                            int mapi = i * 256;
                            int mapj = -j * 256;
                            //Check the database for current tile using quadkey
                            //Calculate the quadkey for the given tile
                            String originQuadKey = QuadKeyFuncs.getQuadKey(Globals.Latitude, Globals.Longitude, 14);
                            //Use x and z to offset the quadkey
                            int initx = 0;
                            int initz = 0;
                            int initChosenZoomLevel = 14;
                            QuadKeyFuncs.QuadKeyToTileXY(originQuadKey, out initx, out initz, out initChosenZoomLevel);
                            initx = initx + Convert.ToInt32(mapi) / 256;
                            initz = initz + Convert.ToInt32(mapj) / 256;
                            String newQuadKey = QuadKeyFuncs.TileXYToQuadKey(initx, initz, initChosenZoomLevel);
                            double ucLat;
                            double ucLong;
                            QuadKeyFuncs.QuadKeyToLatLong(newQuadKey, out ucLat, out ucLong);
                            //Get the lower right corner
                            int tilex = 0;
                            int tilez = 0;
                            int chosenZoomLevel;
                            QuadKeyFuncs.QuadKeyToTileXY(newQuadKey, out tilex, out tilez, out chosenZoomLevel);
                            tilex = tilex + 1;
                            tilez = tilez + 1;
                            String lcquadkey = QuadKeyFuncs.TileXYToQuadKey(tilex, tilez, chosenZoomLevel);

                            bool inDatabase = cache.DBcheck(lcquadkey);

                            
                            //If it doesn't exist in the database, we will add it ourselves
                            if (!inDatabase)
                            {

                                List<float> mesh = await getElevChunk((float)mapi, (float)mapj);
                                var mat = await BMR.getSatelliteImagery((float)mapi, (float)mapj);
                                cache.DBInsert(lcquadkey, mesh, mat);
                            }
                        }

                }
            }
        }
    }


    async public Task<List<float>> getElevChunk(float x, float z)
    {
        String originQuadKey = QuadKeyFuncs.getQuadKey(Globals.Latitude, Globals.Longitude, 14);

        int initx = 0;
        int initz = 0;
        int initChosenZoomLevel = 14;
        QuadKeyFuncs.QuadKeyToTileXY(originQuadKey, out initx, out initz, out initChosenZoomLevel);
        initx = initx + Convert.ToInt32(x) / 256;
        initz = initz + Convert.ToInt32(z) / 256;
        String newQuadKey = QuadKeyFuncs.TileXYToQuadKey(initx, initz, initChosenZoomLevel);

        double ucLat;
        double ucLong;
        QuadKeyFuncs.QuadKeyToLatLong(newQuadKey, out ucLat, out ucLong);
        double lcLat;
        double lcLong;
        //Get the lower right corner
        int tilex = 0;
        int tilez = 0;
        int chosenZoomLevel;
        QuadKeyFuncs.QuadKeyToTileXY(newQuadKey, out tilex, out tilez, out chosenZoomLevel);
        tilex = tilex + 1;
        tilez = tilez + 1;
        String lcquadkey = QuadKeyFuncs.TileXYToQuadKey(tilex, tilez, chosenZoomLevel);

        QuadKeyFuncs.QuadKeyToLatLong(lcquadkey, out lcLat, out lcLong);

        int i = 0;
        int j = 0;
        int k = 0;

        //Request Bing API elevations using input bounding box

        String requestString = "http://dev.virtualearth.net/REST/v1/Elevation/Bounds?bounds=" + (lcLat) + "," + (lcLong) + "," + (ucLat) + "," + (ucLong) + "&rows=32&cols=32&key=" + "AvOVJReSdFZbRWwS3JZI91yN4JLK4RH5lW6mdFTcJOdE-U5PFmC2hGgUtWUM6wsr";

        ////////////////////////////////////////////////////////////
        HttpClient client = new HttpClient();
        var content = "";
        //while (DateTime.Now.Millisecond - start < 200) { await Task.Delay(1); }
        bool tooManyRequestFlag = false;
        do
        {
            tooManyRequestFlag = false;
            try
            {
                content = await client.GetStringAsync(requestString);
            }
            catch (HttpRequestException e)
            {
                if (e.Message == "429 (Too Many Requests)")
                    tooManyRequestFlag = true;
                else
                {
                    throw e;
                    
                }
            }
        } while (tooManyRequestFlag);

        ////////////////////////////////////////////////////////////

        int start = content.IndexOf("\"elevations\"") + 14;
        int end = content.IndexOf("\"zoomLevel\"") - 2;
        String elevation_string = content.Substring(start, end - start);
        List<String> elevation_strings = elevation_string.Split(',').ToList();
        List<float> retrieved_chunk = new List<float>();
        for (k = 0; k < elevation_strings.Count; k++)
        {
            retrieved_chunk.Add(Convert.ToSingle(elevation_strings[k]));
        }

        List<float> newElevChunk = new List<float>();
        //Place elevations into elevation chunk
        for (i = 0; i < Math.Sqrt(retrieved_chunk.Count); i++)
        {
            for (j = 0; j < Math.Sqrt(retrieved_chunk.Count); j++)
            {
                newElevChunk.Add(retrieved_chunk[(31 - j) * Convert.ToInt32(Math.Sqrt(retrieved_chunk.Count)) + (31 - i)]);
            }
        }
        return newElevChunk;
    }

    IEnumerator<int> tileMonitor(){
        // Check current position
        Vector3 currpos = Globals.position;
        int x = Convert.ToInt32(Math.Round(currpos[0] / 32));
        int z = Convert.ToInt32(Math.Round(currpos[2] / 32));

        //Check for tiles within needed range
        //We want all tiles in a 3x3 range
        for (int i = x - 3; i < x + 3; i++)
        {
            for (int j = z - 3; j < z + 3; j++)
            {
                //Check if current tile exists
                Tuple<int, int> tempTuple = new Tuple<int, int>(i, j);
                if (!active.Contains(tempTuple))
                {
                    //order a prefab to be created with the new coordinates
                    GameObject Obj = worldTiles[counter + 1];
                    Obj.name = "Tilex" + Convert.ToString(i) + "y" + Convert.ToString(j);
                    Obj.transform.position = new Vector3((i * 31), 0, (j) * 31);
                    Obj.transform.rotation = Quaternion.identity;
                    Obj.transform.localScale = new Vector3(256 / 10 + 1, 256 / 10 + 1, 256 / 10 + 1);

                    Tuple<Mesh, Material, bool> TileTuple = TileBuilder.BuildTile(i * 256, -j * 256);
                    //Tuple<Mesh, Material> TileTuple = new Tuple<Mesh, Material>(new Mesh(), new Material(Shader.Find("Standard")));

                    if (TileTuple.Item3)
                    {
                        Obj.GetComponent<MeshRenderer>().material = TileTuple.Item2;
                        Obj.GetComponent<MeshFilter>().mesh = TileTuple.Item1;
                        Obj.transform.localScale = Vector3.one;
                        active.Add(tempTuple);
                        counter = (counter + 1) % worldSize;
                    }
                    yield return 1;
                }

            }
        }
    }


}

