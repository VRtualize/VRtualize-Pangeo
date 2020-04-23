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

    /// <summary>
    /// The constructor for AppHandler that sets initial variables
    /// </summary>
    AppHandler()
    {
        counter = 0;
        worldSize = 10000;
        active = new HashSet<Tuple<int, int>>();
        prevtime = DateTime.Now;
        worldTiles = new List<GameObject>();
    }

    /// <summary>
    /// The start function for AppHandler. Being a MonoBehaviour, this will only run once.
    /// It gets the Bing Maps API key, loads in the tile prefab, reserves space in memory 
    /// to hold the world and then sets off the thread for the tile predictor which run for
    /// the entirety of the program.
    /// </summary>
    void Start()
    {
        //Retrieve the Bing Maps API Key from the config file
        string[] lines = System.IO.File.ReadAllLines(@"Assets/config");
        string apikey = lines[0].Substring(17, lines[0].Length - 18);
        Globals.BingAPIKey = apikey;

        //Load in the prefab for all tiles
        pPrefab = Resources.Load("Prefabs/Tile");

        //Create a reserved list of tiles to be used when loading tiles
        for (int i = 0; i < worldSize; i++)
        {
            worldTiles.Add((GameObject)GameObject.Instantiate(pPrefab, Vector3.one, Quaternion.identity));
        }

        //Launch the Tile Predictor thread
        Thread t = new Thread(new ThreadStart(tilePredictor));
        t.Start();
    }

    /// <summary>
    /// The update function runs once for every frame rendered by Unity. It just runs the
    /// coroutine for placing tiles in Unity. It runs for a small while and then yields 
    /// control back to the main thread when it hits its yield.
    /// </summary>
    void Update()
    {
        //Run the coroutine of loading each tile in the middle of each frame rendered
        StartCoroutine(tileMonitor());
    }

    /// <summary>
    /// This function is the function run inside the Tile Prediction thread started in 
    /// AppHandler's start function. It runs for the entire program, checking the current
    /// location of the camera and loading any tiles nearby that it can't find already into
    /// the database. Having the database work as a cache like this makes it easy to have tiles
    /// ready for the actual placement function later.
    /// </summary>
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
            //We want all tiles in a 12x12 range
            for (int i = x - 6; i < x + 6; i++)
            {
                for (int j = z - 6; j < z + 6; j++)
                {
                    //Check if current tile exists
                    Tuple<int, int> tempTuple = new Tuple<int, int>(i, j);
                    if (!active.Contains(tempTuple))
                    {
                        //Make sure that the maps coordinates are converted from Unity to Bing Maps TileXY
                        int mapi = i * 256;
                        int mapj = -j * 256;
                        //Check the database for current tile using quadkey
                        //Calculate the quadkey for the given tile
                        String originQuadKey = QuadKeyFuncs.getQuadKey(Globals.latitude, Globals.longitude, 14);
                        //Use x and z to offset the quadkey
                        int initx = 0;
                        int initz = 0;
                        int initChosenZoomLevel = 14;
                        QuadKeyFuncs.QuadKeyToTileXY(originQuadKey, out initx, out initz, out initChosenZoomLevel);
                        initx = initx + Convert.ToInt32(mapi) / 256;
                        initz = initz + Convert.ToInt32(mapj) / 256;
                        String newQuadKey = QuadKeyFuncs.TileXYToQuadKey(initx, initz, initChosenZoomLevel);

                        bool inDatabase = cache.DBcheck(newQuadKey);

                        //If it doesn't exist in the database, we will add it ourselves
                        if (!inDatabase)
                        {

                            List<float> mesh = await getElevChunk((float)mapi, (float)mapj);
                            var mat = await BMR.getSatelliteImagery((float)mapi, (float)mapj);
                            cache.DBInsert(newQuadKey, mesh, mat);
                        }
                    }

                }
            }
        }
    }

    /// <summary>
    /// This is an asynchronous version of the getElevFunction in BingMapResources.cs.
    /// It gets an elevation chunk from BingMaps Rest API using an offset from the 
    /// starting latitude and longitude in globals. 
    /// </summary>
    /// <param name="x">The Unity coordinate for X</param>
    /// <param name="z">The Unity coordinate for Z</param>
    /// <returns></returns>
    async public Task<List<float>> getElevChunk(float x, float z)
    {
        String originQuadKey = QuadKeyFuncs.getQuadKey(Globals.latitude, Globals.longitude, 14);

        int initx = 0;
        int initz = 0;
        int initChosenZoomLevel = 14;
        QuadKeyFuncs.QuadKeyToTileXY(originQuadKey, out initx, out initz, out initChosenZoomLevel);
        initx = initx + Convert.ToInt32(x) / 256;
        initz = initz + Convert.ToInt32(z) / 256;
        //Get the upper left corner
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

        //Request Bing API elevations using the corners as a bounding box
        String requestString = "http://dev.virtualearth.net/REST/v1/Elevation/Bounds?bounds=" + (lcLat) + "," + (lcLong) + "," + (ucLat) + "," + (ucLong) + "&rows=32&cols=32&key=" + Globals.BingAPIKey;
        HttpClient client = new HttpClient();
        var content = "";
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

        int i;
        int j;
        int k;

        //Convert the retrieved elevations into a usable chunk
        int start = content.IndexOf("\"elevations\"") + 14;
        int end = content.IndexOf("\"zoomLevel\"") - 2;
        String elevation_string = content.Substring(start, end - start);
        List<String> elevation_strings = elevation_string.Split(',').ToList();
        List<float> retrieved_chunk = new List<float>();
        for (k = 0; k < elevation_strings.Count; k++)
        {
            retrieved_chunk.Add(Convert.ToSingle(elevation_strings[k]));
        }

        //Flip the values in the retrieved chunks so that it matches the order of satellite imagery
        //Note that elevations in Bing go from the bottom up in a square while the imagery uses the 
        //top left corner as its baseline. We chose to change the order of the elevations
        List<float> newElevChunk = new List<float>();
        for (i = 0; i < Math.Sqrt(retrieved_chunk.Count); i++)
        {
            for (j = 0; j < Math.Sqrt(retrieved_chunk.Count); j++)
            {
                newElevChunk.Add(retrieved_chunk[(31 - j) * Convert.ToInt32(Math.Sqrt(retrieved_chunk.Count)) + (31 - i)]);
            }
        }
        return newElevChunk;
    }

    /// <summary>
    /// This is a coroutine to place tiles into Unity in between frames. This coroutine will
    /// run until it hits yield and then return control to the main thread. It is essential
    /// in preventing Unity from freezing up while loading chunks. The only latency left is 
    /// in the call to the database. Note that we planned to solve this with an on disk cache.
    /// </summary>
    IEnumerator tileMonitor(){
        // Check current position
        Vector3 currpos = Globals.position;
        int x = Convert.ToInt32(Math.Round(currpos[0] / 32));
        int z = Convert.ToInt32(Math.Round(currpos[2] / 32));

        //Check for tiles within needed range
        //We want all tiles in a 6x6 range
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

                    //Get elevations and imagery from the database to build the tile
                    Tuple<Mesh, Material, bool> TileTuple = TileBuilder.BuildTile(i * 256, -j * 256);

                    //If the database returned a found true in the bool, apply the information to the tile
                    if (TileTuple.Item3)
                    {
                        Obj.GetComponent<MeshRenderer>().material = TileTuple.Item2;
                        Obj.GetComponent<MeshFilter>().mesh = TileTuple.Item1;
                        Obj.transform.localScale = Vector3.one;
                        active.Add(tempTuple);
                        counter = (counter + 1) % worldSize;
                    }
                    yield return null;
                }
            }
        }
    }
}

