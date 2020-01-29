using System.Collections;
using System.Collections.Generic;
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
    void Awake()
    {
        //Future Work: Query starting location from user via menu.

        //Expected side length of each tile
        int tileSize = 212;

        //The starting environment will be size^2 tiles in a square shape
        int size = 1;

        ////Build the starting area
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                BuildNewTile(i * tileSize, 0.1F, j * tileSize, "Tile " +
                    (i * size + j));
            }
        }
    }

    /// <summary>
    /// This function is used to create a new tile and place it into the play
    /// environment.
    /// </summary>
    /// <param name="x">The x coordinate for the tile</param>
    /// <param name="y">The y coordinate for the tile</param>
    /// <param name="z">The z coordinate for the tile</param>
    /// <param name="name">The name of the tile in the Unity hierarchy</param>
    async void BuildNewTile(float x, float y, float z, string name)
    {
        //Retrieving the map chunk from the cache class
        UsgsMapResources res = new UsgsMapResources();
        Cache cache = new Cache();
        cache.setMesh(res, (int) x, (int) z);
        WWW imgLoader = await cache.getSatelliteImagery();
        List<float> ElevList = cache.getMesh();
        
        //Assign the GameObject material from the Resources folder
        //Future Work: Change the material to take satellite imagery from cache
        Material mat = Resources.Load("Materials/Stylize_Grass", 
            typeof(Material)) as Material;
        mat.color = Color.white;
        mat.mainTexture = imgLoader.texture;

        //Perform array size calculations
        int length = ElevList.Count;
        int side = (int) Mathf.Sqrt(length);
        int sqrPtCount = (int) (Mathf.Pow( (side - 1), 2) * 6);

        //Create empty vertices, uv, and triangles arrays and set their values
        Vector3[] vertices = fillVert(length, side, ElevList);
        Vector2[] uv = fillUv(length, side);
        int[] triangles = fillTri(sqrPtCount, side);

        //Create a new Mesh to render, assign its components, and recalculate
        //its normals for smooth rendering
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();


        //Assign the Mesh to the rendering engine and transform into position
        GameObject Obj = new GameObject(name, typeof(MeshFilter), 
            typeof(MeshRenderer));

        //Can be used to change the scale of the tile, currently no change
        Obj.transform.localScale = new Vector3(1, 1, 1);

        //Can be used to change the rotation of the tile, currently no change
        Obj.transform.rotation = Quaternion.Euler(0, 0, 0);

        //Used to position the tile in Unity space. There is a growing offset
        //in the tile location, so the call accounts for this when placing
        //tiles into th egame world
        Obj.transform.position = new Vector3(x - (x/side), y, 
            (side - z) - 212 + (z/side));

        //Set the object's mesh filter with our created mesh
        Obj.GetComponent<MeshFilter>().mesh = mesh;

        //Assign the object's renderer with specified material
        Obj.GetComponent<MeshRenderer>().material.mainTexture = imgLoader.texture;
        Obj.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2((float)(1.0/212.0), (float)(1.0/212.0));
    }

    /// <summary>
    /// This function assigns elevation points into a 1D array or Vector3
    /// objects for use by the mesh
    /// </summary>
    /// <param name="len">The total length of the supplied array</param>
    /// <param name="side">The square root of len</param>
    /// <param name="ElevPts">The array of elevation points</param>
    /// <returns>A Vector3 array assigned our elevation points</returns>
    Vector3[] fillVert(int len, int side, List<float> ElevPts)
    {
        Vector3[] tmp = new Vector3[len];

        //Define the separation between points. Used to translate elevation
        //points into Unity space size so Y coordinates are consistent with 
        //X-Z plane points
        const float pointSep = 10.29F;

        //Assign the points to a temporary array
        int pos = 0;
        for(int i = 0; i < side; i++)
        {
            for(int j = 0; j < side; j++)
            {
                tmp[pos] = new Vector3(i, ElevPts[pos]/pointSep, (side -j));
                pos++;
            }
        }

        return tmp;
    }

    /// <summary>
    /// This function creates a UV array used for defining where materials lay
    /// on top of the mesh.
    /// </summary>
    /// <param name="len">The total length of the array of points</param>
    /// <param name="side">The square root of len</param>
    /// <returns>A Vector2 array assigned UV point locations</returns>
    Vector2[] fillUv(int len, int side)
    {
        Vector2[] tmp = new Vector2[len];

        //Assign the points to a temporary array
        int pos = 0;
        for (int i = 0; i < side; i++)
        {
            for (int j = 0; j < side; j++)
            {
                tmp[pos] = new Vector2(i, (side - j));
                pos++;
            }
        }

        return tmp;
    }


    /// <summary>
    /// This function creates a triangle array of integers. Unity uses this
    /// array to assign rendered triangles to indexes in the vertices array.
    /// </summary>
    /// <param name="ptCount">
    /// The number of points to define the triangle
    /// array</param>
    /// <param name="side">
    /// The square root of the number of squares in mesh
    /// </param>
    /// <returns>An int array of points to define the mesh triangles</returns>
    int[] fillTri(int ptCount, int side)
    {
        int[] tmp = new int[ptCount];

        int pos = 0;

        //Define the number of squares in the mesh
        int sqrCount = ptCount / 6;
        for (int i = 0; i < sqrCount; i++)
        {
            //Each square in the mesh is defined by two triangles, and each
            //triangle is composed of three points
            //Define the two triangles in each square in the square mesh
            int sqrRow = (int) (i / (side - 1));
            tmp[pos++] = i + side + sqrRow;
            tmp[pos++] = i + 1 + sqrRow;
            tmp[pos++] = i + sqrRow;
            tmp[pos++] = i + side + 1 + sqrRow;
            tmp[pos++] = i + 1 + sqrRow;
            tmp[pos++] = i + side + sqrRow;
        }

        return tmp;
    }
}
