using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Interface for Map Resources. IMapResources ensures all external systems such as Bing REST API come in with a certain format
/// </summary>
public interface IMapResources
{
	List<float> getMesh(float x, float z);
	Task<byte[]> getSatelliteImagery(float x, float z);
}
