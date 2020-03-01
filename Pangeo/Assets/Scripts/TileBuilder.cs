using System;
using System.Collections.Generic;
using UnityEngine;
using DataManagerUtils;
using System.Threading.Tasks;

public static class TileBuilder
{
    /// <summary>
    /// This function is used to create a new tile and place it into the play
    /// environment.
    /// </summary>
    /// <param name="x">The x coordinate for the tile</param>
    /// <param name="z">The z coordinate for the tile</param>
    /// <param name="name">The name of the tile in the Unity hierarchy</param>
    async public static Task<Mesh> GetMesh(float x, float z)
    {
        //Get latitude/longitude offset using quadkeys and x/z offsets
        BingMaps quadKeyFuncs = new BingMaps();
        Double Lat = Globals.latitude;
        Double Long = Globals.longitude;
        int chosenZoomLevel = 14;
        Cache cache = new Cache();

        //Get latitude and longitude of upper left corner of the Image tile using the quad key backwards
        String tempQuadKey = quadKeyFuncs.getQuadKey(Lat, Long, chosenZoomLevel);

        //Use x and z to offset the quadkey
        int tilex = 0;
        int tilez = 0;
        BingMaps.QuadKeyToTileXY(tempQuadKey, out tilex, out tilez, out chosenZoomLevel);
        tilex = tilex + Convert.ToInt32(x) / 256;
        tilez = tilez + Convert.ToInt32(z) / 256;
        String newQuadKey = BingMaps.TileXYToQuadKey(tilex, tilez, chosenZoomLevel);


        //Get image at proper latitude and longitude
        List<float> ElevList = await cache.getMesh(newQuadKey);

        //Perform array size calculations
        int length = ElevList.Count;
        int side = (int)Mathf.Sqrt(length);
        int sqrPtCount = (int)(Mathf.Pow((side - 1), 2) * 6);

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

        return mesh;
    }

    /// <summary>
    /// This function is used to create a new tile and place it into the play
    /// environment.
    /// </summary>
    /// <param name="x">The x coordinate for the tile</param>
    /// <param name="y">The y coordinate for the tile</param>
    /// <param name="z">The z coordinate for the tile</param>
    /// <param name="name">The name of the tile in the Unity hierarchy</param>
    async public static Task<Material> GetMaterial(float x, float z)
    {
        //Get latitude/longitude offset using quadkeys and x/z offsets
        BingMaps quadKeyFuncs = new BingMaps();
        Double Lat = Globals.latitude;
        Double Long = Globals.longitude;
        int chosenZoomLevel = 14;
        Cache cache = new Cache();

        //Get latitude and longitude of upper left corner of the Image tile using the quad key backwards
        String tempQuadKey = quadKeyFuncs.getQuadKey(Lat, Long, chosenZoomLevel);

        //Use x and z to offset the quadkey
        int tilex = 0;
        int tilez = 0;
        BingMaps.QuadKeyToTileXY(tempQuadKey, out tilex, out tilez, out chosenZoomLevel);
        tilex = tilex + Convert.ToInt32(x) / 256;
        tilez = tilez + Convert.ToInt32(z) / 256;
        String newQuadKey = BingMaps.TileXYToQuadKey(tilex, tilez, chosenZoomLevel);


        //Get image at proper latitude and longitude
        WWW imgLoader = await cache.getSatelliteImagery(newQuadKey);


        //Assign the GameObject material from the Resources folder
        //Future Work: Change the material to take satellite imagery from cache
        Material mat = new Material(Shader.Find("Standard"));
        mat.mainTexture = imgLoader.texture;

        //Assign the object's renderer with specified material
        mat.mainTextureScale = new Vector2((float)(1.0 / 256.0), (float)(1.0 / 256.0));

        return mat;
    }

    /// <summary>
    /// This function assigns elevation points into a 1D array or Vector3
    /// objects for use by the mesh
    /// </summary>
    /// <param name="len">The total length of the supplied array</param>
    /// <param name="side">The square root of len</param>
    /// <param name="ElevPts">The array of elevation points</param>
    /// <returns>A Vector3 array assigned our elevation points</returns>
    static Vector3[] fillVert(int len, int side, List<float> ElevPts)
    {
        int chosenZoomLevel = 14;
        Vector3[] tmp = new Vector3[len];

        //Define the separation between points. Used to translate elevation
        //points into Unity space size so Y coordinates are consistent with 
        //X-Z plane points

        //TODO Make this our global zoom level
        float pointSep = Convert.ToSingle(Globals.zoomLevelMetersPerPixel[chosenZoomLevel]);

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
    static Vector2[] fillUv(int len, int side)
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
    static int[] fillTri(int ptCount, int side)
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
