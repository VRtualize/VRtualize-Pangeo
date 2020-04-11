using System;
// using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DataManagerUtils;
using MySql.Data.MySqlClient;
using System.Net.Http;
using System.Linq;

public class BingMapResources : IMapResources
{
    private String originQuadKey;
    private imageURLRequest restapiurl;

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
        List<float> mesh = ElevationRequest(ucLat, ucLong, lcLat, lcLong, 32, newQuadKey.Length);


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

    public List<float> ElevationRequest(double latupper, double longleft, double latlower, double longright, int sideLength, int zoomLevel)
    {
        int i = 0;
        int j = 0;
        int k = 0;

        String requestString = "http://dev.virtualearth.net/REST/v1/Elevation/Bounds?bounds=" + (latlower) + "," + (longright) + "," + (latupper) + "," + (longleft) + "&rows=32&cols=32&key=" + Globals.BingAPIKey;
        var content = BingApiRequestManager.getUrlData(requestString);
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
}
