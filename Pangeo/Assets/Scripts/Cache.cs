using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using DataManagerUtils;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;


public class Cache
{
	private List<float> mesh;
    private imageURLRequest restapiurl;

    public Cache()
    {
        Debug.Log("Length: " + Globals.length);
        Debug.Log("MeshLength: " + Globals.meshLength);
        string[] lines = System.IO.File.ReadAllLines(@"Assets/config");
        string apikey = lines[0].Substring(17, lines[0].Length - 18);
        Debug.Log(apikey);
        restapiurl = new imageURLRequest(apikey);
    }
    
	async public Task<List<float>> getMesh(String quadkey)
    {
        BingMaps quadKeyFuncs = new BingMaps();
        double ucLat;
        double ucLong;
        quadKeyFuncs.QuadKeyToLatLong(quadkey, out ucLat, out ucLong);
        double lcLat;
        double lcLong;
        //Get the lower right corner
        int tilex = 0;
        int tilez = 0;
        int chosenZoomLevel;
        BingMaps.QuadKeyToTileXY(quadkey, out tilex, out tilez, out chosenZoomLevel);
        tilex = tilex + 1;
        tilez = tilez + 1;
        String lcquadkey = BingMaps.TileXYToQuadKey(tilex, tilez, chosenZoomLevel);
        quadKeyFuncs.QuadKeyToLatLong(lcquadkey, out lcLat, out lcLong);
        //Get chunks from database in Image tile range
        DataManager tempDataManager = new DataManager();

        List<float> mesh = await tempDataManager.ElevationRequest(ucLat, ucLong, lcLat, lcLong, 256, quadkey.Length);
        
        return mesh;
    }

	public void setMesh(IMapResources resources) { mesh = resources.getMesh(); }
	public async Task<WWW> getSatelliteImagery(String QuadKey)
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

