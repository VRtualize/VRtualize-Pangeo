using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DataManagerUtils;

public static class TileBuilder
{
    /// <summary>
    /// This function takes in two Unity coordinates and finds the quadkey
    /// of those coordinates based on the Global starting latitude and longitude.
    /// It then checks to see if that quad key already exists in the database. 
    /// If it doesn't find the entry, it will return an empty mesh and material
    /// with a bool to indicate that these are not database entries, but rather
    /// defaults
    /// </summary>
    /// <param name="x">The X coordinate for the searched position in unity coordinates</param>
    /// <param name="z">The Z coordinate for the searched position in unity coordinates</param>
    /// <returns></returns>
    public static Tuple<Mesh, Material, bool> BuildTile(float x, float z){

        //Calculate the quadkey for the original starting tile
        String originQuadKey = QuadKeyFuncs.getQuadKey(Globals.latitude, Globals.longitude, 14);
        int initx = 0;
        int initz = 0;
        int initChosenZoomLevel = 14;
        //Find the X and Y coordinate in Bing Maps Tiles
        QuadKeyFuncs.QuadKeyToTileXY(originQuadKey, out initx, out initz, out initChosenZoomLevel);
        //Offset the BingMapsAPI coordinates from the starting point by x and z in unity
        initx = initx + Convert.ToInt32(x) / 256;
        initz = initz + Convert.ToInt32(z) / 256;
        //Get a quadkey for the new location
        String currQuadKey = QuadKeyFuncs.TileXYToQuadKey(initx, initz, initChosenZoomLevel);

        //Check DB for current entry
        Cache cache = new Cache();
        bool entryExists = cache.DBcheck(currQuadKey);

        //Create an empty mesh and Material
        Mesh mesh = new Mesh();
        Material mat = new Material(Shader.Find("Standard"));

        if (entryExists){
            Tuple<List<float>, Texture> TileTuple = cache.DBGet(currQuadKey);

            //Ready the mesh
            //Perform array size calculations
            int length = TileTuple.Item1.Count;
            int side = (int)Mathf.Sqrt(length);
            int sqrPtCount = (int)(Mathf.Pow((side - 1), 2) * 6);

            //Create empty vertices, uv, and triangles arrays and set their values
            Vector3[] vertices = fillVert(length, side, TileTuple.Item1);
            Vector2[] uv = fillUv(length, side);
            int[] triangles = fillTri(sqrPtCount, side);

            //Create a new Mesh to render, assign its components, and recalculate
            //its normals for smooth rendering
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            //Ready the material
            mat.mainTexture = TileTuple.Item2;
            mat.mainTextureScale = new Vector2((float)(1.0 / 32.0), (float)(1.0 / 32.0));

        }

        return new Tuple<Mesh, Material, bool>(mesh, mat, entryExists);
    }

    /// <summary>
    /// This function assigns elevation points into a 1D array or Vector3
    /// objects for use by the mesh
    /// </summary>
    /// <param name="len">The total length of the supplied array</param>
    /// <param name="side">The square root of len</param>
    /// <param name="ElevPts">The array of elevation points</param>
    /// <returns>A Vector3 array assigned our elevation points</returns>
    public static Vector3[] fillVert(int len, int side, List<float> ElevPts)
    {
        int chosenZoomLevel = 14;
        Vector3[] tmp = new Vector3[len];

        //Define the separation between points. Used to translate elevation
        //points into Unity space size so Y coordinates are consistent with 
        //X-Z plane points

        //TODO Make this our global zoom level
        float pointSep = Convert.ToSingle(Globals.zoomLevelMetersPerPixel[chosenZoomLevel] * 8);

        //Assign the points to a temporary array
        int pos = 0;
        for (int i = 0; i < side; i++)
        {
            for (int j = 0; j < side; j++)
            {
                tmp[pos] = new Vector3(i, ElevPts[pos] / pointSep, (side - j));
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
    public static Vector2[] fillUv(int len, int side)
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
    public static int[] fillTri(int ptCount, int side)
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
            int sqrRow = (int)(i / (side - 1));
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
