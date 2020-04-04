using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IMapResources
{
	List<float> getMesh(float x, float z);
	WWW getSatelliteImagery(float x, float z);
}
