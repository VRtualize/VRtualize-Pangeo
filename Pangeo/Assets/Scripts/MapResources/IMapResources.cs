using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IMapResources
{
	Task<List<float>> getMesh(string quadkey);
	Task<WWW> getSatelliteImagery(string Quadkey);
}
