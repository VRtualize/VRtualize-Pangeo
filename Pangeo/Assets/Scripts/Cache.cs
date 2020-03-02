using System;
using System.Collections.Generic;
using DataManagerUtils;
using UnityEngine;
using System.Threading.Tasks;


public class Cache
{
	private List<float> mesh;
    
	async public Task<List<float>> getMesh(IMapResources res, String quadkey) { return await res.getMesh(quadkey); }

	async public void setMesh(IMapResources resources, string quadkey) { mesh = await resources.getMesh(quadkey); }
	async public Task<WWW> getSatelliteImagery(IMapResources res, String QuadKey) { return await res.getSatelliteImagery(QuadKey); }
}
