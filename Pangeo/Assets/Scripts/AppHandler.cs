using System;
using UnityEngine;

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
    void Awake()
    {
        //Future Work: Query starting location from user via menu.

        //The starting environment will be size^2 tiles in a square shape
        int size = 3;

        UnityEngine.Object pPrefab = Resources.Load("Prefabs/Tile");

        //Build the starting area
        for (int i = 0; i < size*size; i++)
        {
            GameObject Obj = (GameObject)GameObject.Instantiate(pPrefab, new Vector3(i/size*256 - i/size, 0, (i%size) - (i % size) * 256), Quaternion.identity);
            Obj.name = "Tilex" + Convert.ToString(i/size) + "y" + Convert.ToString(i%size);
            Obj.transform.localScale = new Vector3(256 / 10+1, 256 / 10+1, 256 / 10+1);
        }
    }

}
