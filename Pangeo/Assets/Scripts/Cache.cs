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
    
	async public Task<List<float>> getMesh(int chosenZoomLevel)
    {
        int meshLength = 256;
        BingMaps quadKeyFuncs = new BingMaps();

        //Get latitude and longitude of upper left corner of the Image tile using the quad key backwards
        String tempQuadKey = quadKeyFuncs.getQuadKey(Globals.latitude, Globals.longitude, chosenZoomLevel);
        double ucLat = Globals.latitude;
        double ucLong = Globals.longitude;
        Debug.Log("YEEUTSTHE1ST");
        quadKeyFuncs.QuadKeyToLatLong(tempQuadKey, out ucLat, out ucLong);
        //Get chunks from database in Image tile range
        DataManager tempDataManager = new DataManager();

        List<float> mesh = await tempDataManager.ElevationRequest(ucLat, ucLong, ucLat - (256 * Globals.zoomLevelMetersPerPixel[chosenZoomLevel]) / 111255.48, ucLong + (256 * Globals.zoomLevelMetersPerPixel[chosenZoomLevel]) / 111255.48, 256, chosenZoomLevel);
        
        return mesh;
    }

	public void setMesh(IMapResources resources) { mesh = resources.getMesh(); }
	public async Task<WWW> getSatelliteImagery(double latitude, double longitude, int zoomLevel)
    {
        if (String.IsNullOrEmpty(restapiurl.subdomain))
        {
            await restapiurl.initializeURL();
        }
        string imageURL = restapiurl.GetQuadKeyURL(latitude, longitude, zoomLevel);
        WWW wwwLoader = new WWW(imageURL.Replace("\\", ""));
        while (!wwwLoader.isDone);
        return wwwLoader;
        //await Task.CompletedTask;
        //return new WWW("https://thenewstalkers.com/group/image/group_image/149/large/_v=63f541505439300");
    }
}

