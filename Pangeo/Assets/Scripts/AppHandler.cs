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
    private UnityEngine.Object pPrefab;
    DateTime prevtime;

    AppHandler()
    {
        active = new HashSet<Tuple<int, int>>();
        prevtime = DateTime.Now;
    }

    void Start()
    {
        ////Future Work: Query starting location from user via menu.

        ////The starting environment will be size^2 tiles in a square shape
        //int size = 2;

        pPrefab = Resources.Load("Prefabs/Tile");

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
        DateTime currtime = DateTime.Now;
        if (currtime.Subtract(prevtime) > new TimeSpan(0, 0, 2))
        {
            // Check current position
            Vector3 currpos = Globals.position;
            int x = Convert.ToInt32(Math.Round(currpos[0] / 256));
            int z = Convert.ToInt32(Math.Round(currpos[2] / 256));

            //Check for tiles within needed range
            //We want all tiles in a 3x3 range
            for (int i = x - 1; i < x + 1; i++)
            {
                for (int j = z - 1; j < z + 1; j++)
                {
                    //Check if current tile exists
                    Tuple<int, int> tempTuple = new Tuple<int, int>(i, j);
                    if (!active.Contains(tempTuple))
                    {
                        active.Add(tempTuple);
                        //order a prefab to be created with the new coordinates
                        GameObject Obj = (GameObject)GameObject.Instantiate(pPrefab, new Vector3((i * 256 - i), 0, (j) - (j) * 256), Quaternion.identity);
                        Obj.name = "Tilex" + Convert.ToString(i) + "y" + Convert.ToString(j);
                        Obj.transform.localScale = new Vector3(256 / 10 + 1, 256 / 10 + 1, 256 / 10 + 1);
                    }

                }
            }
            prevtime = DateTime.Now;
        }
    }


}

