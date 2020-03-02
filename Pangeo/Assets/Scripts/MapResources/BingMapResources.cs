using System;
// using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DataManagerUtils;

public class BingMapResources : IMapResources
{
    private imageURLRequest restapiurl;
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
    public BingMapResources()
    {
        string[] lines = System.IO.File.ReadAllLines(@"Assets/config");
        string apikey = lines[0].Substring(17, lines[0].Length - 18);
        restapiurl = new imageURLRequest(apikey);
    }
    async Task<List<float>> IMapResources.getMesh(string quadkey) {
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
        double ucLat;
        double ucLong;
        QuadKeyFuncs.QuadKeyToLatLong(quadkey, out ucLat, out ucLong);
        double lcLat;
        double lcLong;
        //Get the lower right corner
        int tilex = 0;
        int tilez = 0;
        int chosenZoomLevel;
        QuadKeyFuncs.QuadKeyToTileXY(quadkey, out tilex, out tilez, out chosenZoomLevel);
        tilex = tilex + 1;
        tilez = tilez + 1;
        String lcquadkey = QuadKeyFuncs.TileXYToQuadKey(tilex, tilez, chosenZoomLevel);
        QuadKeyFuncs.QuadKeyToLatLong(lcquadkey, out lcLat, out lcLong);
        //Get chunks from database in Image tile range
        DataManager tempDataManager = new DataManager();

        List<float> mesh = await tempDataManager.ElevationRequest(ucLat, ucLong, lcLat, lcLong, 256, quadkey.Length);
        
        return mesh;
    }
    async Task<WWW> IMapResources.getSatelliteImagery(String QuadKey)
    {
        if (String.IsNullOrEmpty(restapiurl.subdomain))
        {
            await restapiurl.initializeURL();
        }
        String quadKeyURL = restapiurl.exampleURL;
        quadKeyURL = quadKeyURL.Replace("{subdomain}", restapiurl.subdomain);
        quadKeyURL = quadKeyURL.Replace("r{quadkey}", "a" + (string)QuadKey.ToString());
        quadKeyURL = quadKeyURL.Replace("{culture}", "en-US");
        WWW wwwLoader = new WWW(quadKeyURL.Replace("\\", ""));
        while (!wwwLoader.isDone);
        return wwwLoader;
    }
}
