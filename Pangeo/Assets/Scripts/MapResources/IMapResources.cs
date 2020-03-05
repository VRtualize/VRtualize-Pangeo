using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IMapResources
{
	Task<List<float>> getMesh(float x, float z);
	Task<WWW> getSatelliteImagery(float x, float z);
}
