using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DataManagerUtils;

public static class TileBuilder
{
    public static Tuple<Mesh, Material, bool> BuildTile(float x, float z){
        

        //Calculate the quadkey for the given tile
        String originQuadKey = QuadKeyFuncs.getQuadKey(Globals.Latitude, Globals.Longitude, 14);
        //Use x and z to offset the quadkey
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

        //Check DB for current entry
        Cache cache = new Cache();
        bool entryExists = cache.DBcheck(lcquadkey);

        Mesh mesh = new Mesh();
        Material mat = new Material(Shader.Find("Standard"));

        if (entryExists){
            Tuple<List<float>, Texture> TileTuple = cache.DBGet(lcquadkey);

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
    /// This function is used to create a new tile and place it into the play
    /// environment.
    /// </summary>
    /// <param name="x">The x coordinate for the tile</param>
    /// <param name="z">The z coordinate for the tile</param>
    /// <param name="name">The name of the tile in the Unity hierarchy</param>
    public static Tuple<Mesh,List<float>> GetMesh(float x, float z)
    {
        Cache cache = new Cache();



        //Get image at proper latitude and longitude
        List<float> ElevList = cache.getMesh(new BingMapResources(), x, z);

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

        return new Tuple<Mesh,List<float>>(mesh,ElevList);
    }


    //TODO THIS FUNCTION IS NO LONGER NEEDED
    /// <summary>
    /// This creates and returns a material out of satellite imagery corresponding to the global latitude
    /// and global longitude plus an x and z offset.
    /// </summary>
    /// <param name="x">The x coordinate for the tile</param>
    /// <param name="z">The z coordinate for the tile</param>
    public static Material GetMaterial(float x, float z)
    {
        Cache cache = new Cache();

        // Get image at proper latitude and longitude
        //WWW imgLoader = cache.getSatelliteImagery(new BingMapResources(), x, z);

        // Create and set the material
        Material mat = new Material(Shader.Find("Standard"));
        //mat.mainTexture = imgLoader.texture;
        mat.mainTextureScale = new Vector2((float)(1.0 / 32.0), (float)(1.0 / 32.0));

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
