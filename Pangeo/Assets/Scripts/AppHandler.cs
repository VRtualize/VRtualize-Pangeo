using System;
using UnityEngine;
using System.Threading;
using System.Collections.Generic;


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

        pPrefab = Resources.Load("Prefabs/Tile");

        for (int i = 0; i < worldSize; i++)
        {
            worldTiles.Add((GameObject)GameObject.Instantiate(pPrefab, Vector3.one, Quaternion.identity));
        }
        
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

    IEnumerator<int> tileMonitor(){
        Debug.Log("ACTUALYL RUNNING");
        // Check current position
        Vector3 currpos = Globals.position;
        int x = Convert.ToInt32(Math.Round(currpos[0] / 32));
        int z = Convert.ToInt32(Math.Round(currpos[2] / 32));

        //Check for tiles within needed range
        //We want all tiles in a 3x3 range
        for (int i = x - 5; i < x + 5; i++)
        {
            for (int j = z - 5; j < z + 5; j++)
            {
                //Check if current tile exists
                Tuple<int, int> tempTuple = new Tuple<int, int>(i, j);
                if (!active.Contains(tempTuple))
                {
                    counter = (counter + 1) % worldSize;
                    active.Add(tempTuple);
                    //order a prefab to be created with the new coordinates
                    GameObject Obj = worldTiles[counter];
                    Obj.name = "Tilex" + Convert.ToString(i) + "y" + Convert.ToString(j);
                    Obj.transform.position = new Vector3((i * 31), 0, (j) * 31);
                    Obj.transform.rotation = Quaternion.identity;
                    Obj.transform.localScale = new Vector3(256 / 10 + 1, 256 / 10 + 1, 256 / 10 + 1);

                    Tuple<Mesh, Material> TileTuple = TileBuilder.BuildTile(i * 256, -j * 256);
                    //Tuple<Mesh, Material> TileTuple = new Tuple<Mesh, Material>(new Mesh(), new Material(Shader.Find("Standard")));

                    Obj.GetComponent<MeshRenderer>().material = TileTuple.Item2;
                    Obj.GetComponent<MeshFilter>().mesh = TileTuple.Item1;
                    Obj.transform.localScale = Vector3.one;
                    yield return 1;
                }

            }
        }
    }


}

