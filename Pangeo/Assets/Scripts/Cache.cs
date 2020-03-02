using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class Cache
{
	private List<float> mesh;
    
	async public Task<List<float>> getMesh(IMapResources res, float x, float z) { return await res.getMesh(x, z); }

	async public void setMesh(IMapResources resources, float x, float z) { mesh = await resources.getMesh(x, z); }
	async public Task<WWW> getSatelliteImagery(IMapResources res, float x, float z) { return await res.getSatelliteImagery(x, z); }
}
