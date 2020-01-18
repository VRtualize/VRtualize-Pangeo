using System.Collections.Generic;
public class Cache
{
	private List<float> mesh;
	public List<float> getMesh() { return mesh; }
	public void setMesh(IMapResources resources, int x, int z) {
		mesh = resources.getMesh(x, z);
	}
	public static void getSatelliteImagery() { }
}

