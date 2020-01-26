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
        string[] lines = System.IO.File.ReadAllLines(@"Assets/config");
        string apikey = lines[0].Substring(17, lines[0].Length - 18);
        Debug.Log(apikey);
        restapiurl = new imageURLRequest(apikey);
    }
    
	public List<float> getMesh() { return mesh; }
	public void setMesh(IMapResources resources, int x, int z) {
		mesh = resources.getMesh(x, z);
	}
	public async Task<WWW> getSatelliteImagery()
    {
        if (String.IsNullOrEmpty(restapiurl.subdomain))
        {
            await restapiurl.initializeURL();
        }
        string imageURL = restapiurl.GetQuadKeyURL(44.069, -103.228, 14);
        WWW wwwLoader = new WWW(imageURL.Replace("\\", ""));
        while (!wwwLoader.isDone);
        return wwwLoader;
    }
}

