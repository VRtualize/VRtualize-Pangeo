using System;
// using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DataManagerUtils;
using MySql.Data.MySqlClient;
using System.Net.Http;

public class BingMapResources : IMapResources
{
    private String originQuadKey;
    private imageURLRequest restapiurl;
    /// <summary>
    /// Bing Map's REST API schema as an object for UnityEngine.JSONSerializeModule
    /// </summary>
    // public class bingSchema
    // {
    //     public class Resource
    //     {
    //         public string __type { get; set; }
    //         public List<int> elevations { get; set; }
    //         public int zoomLevel { get; set; }
    //     }
    //     public class ResourceSet
    //     {
    //         public int estimatedTotal { get; set; }
    //         public List<Resource> resources { get; set; }
    //     }
    //     public class RootObject
    //     {
    //         public string authenticationResultCode { get; set; }
    //         public string brandLogoUri { get; set; }
    //         public string copyright { get; set; }
    //         public List<ResourceSet> resourceSets { get; set; }
    //         public int statusCode { get; set; }
    //         public string statusDescription { get; set; }
    //         public string traceId { get; set; }
    //     }
    // }
    /// <summary>
    /// Constructor for BingMapResources.
    /// </summary>
    public BingMapResources()
    {
        string[] lines = System.IO.File.ReadAllLines(@"Assets/config");
        string apikey = lines[0].Substring(17, lines[0].Length - 18);
        restapiurl = new imageURLRequest(apikey);

        originQuadKey = QuadKeyFuncs.getQuadKey(Globals.Latitude, Globals.Longitude, 14);
    }
    /// <summary>
    /// Override the getMesh function to get data from Bing's REST API
    /// </summary>
    /// <param name="x">Unity units in direction x from origin</param>
    /// <param name="z">Unity units in direction z from origin</param>
    /// <returns>A list of elevation points</returns>
    List<float> IMapResources.getMesh(float x, float z) {
        // // For now, we're stubbing out a concrete example
        // bingSchema.RootObject items;
        // using (StreamReader fs = new StreamReader(@"bingExample.json")) 
        // {
        //     string json = fs.ReadToEnd();
        //     items = JsonUtility.FromJson<bingSchema.RootObject>(json);
        // }
        // int meshLength = items.resourceSets[0].resources[0].elevations.Count;
        // List<float> mesh = new List<float>();
        // for(int i = 0; i < meshLength; i++)
        //     mesh.Add(items.resourceSets[0].resources[0].elevations[i]);
        // return mesh;

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

        QuadKeyFuncs.QuadKeyToLatLong(lcquadkey, out lcLat, out lcLong);
        //Get chunks from database if it exists

        //Otherwise, get the mesh from Bing's REST API
        DataManager tempDataManager = new DataManager();
        List<float> mesh = tempDataManager.ElevationRequest(ucLat, ucLong, lcLat, lcLong, 32, newQuadKey.Length);


        return mesh;
    }
    /// <summary>
    /// Override the getSatelliteImagery to get an image from Bing's REST API
    /// </summary>
    /// <param name="x">Unity units in direction x from origin</param>
    /// <param name="z">Unity units in direction z from origin</param>
    /// <returns>A 2D texture of the image as a byte stream</returns>
    async Task<byte[]> IMapResources.getSatelliteImagery(float x, float z)
    {
        HttpClient client = new HttpClient();
        if (String.IsNullOrEmpty(restapiurl.subdomain))
        {
            await restapiurl.initializeURL();
        }

        //Use x and z to offset the quadkey
        int tilex = 0;
        int tilez = 0;
        int chosenZoomLevel = 14;
        QuadKeyFuncs.QuadKeyToTileXY(originQuadKey, out tilex, out tilez, out chosenZoomLevel);
        tilex = tilex + Convert.ToInt32(x) / 256;
        tilez = tilez + Convert.ToInt32(z) / 256;
        String newQuadKey = QuadKeyFuncs.TileXYToQuadKey(tilex, tilez, chosenZoomLevel);

        String quadKeyURL = restapiurl.exampleURL;
        quadKeyURL = quadKeyURL.Replace("{subdomain}", restapiurl.subdomain);
        quadKeyURL = quadKeyURL.Replace("r{quadkey}", "a" + (string)newQuadKey.ToString());
        quadKeyURL = quadKeyURL.Replace("{culture}", "en-US");
        var response = await client.GetAsync(quadKeyURL.Replace("\\", ""));
        var imageData = response.Content;
        var imageBytes = await imageData.ReadAsByteArrayAsync();
        return imageBytes;
    }
}
