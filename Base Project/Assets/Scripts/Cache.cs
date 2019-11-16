using System.Collections.Generic;
public class Cache
{
	private List<float> mesh;
	public List<float> getMesh() { return mesh; }
	public void setMesh(IMapResources resources) {
		mesh = resources.getMesh();
	}
	public static void getSatelliteImagery() { }
}

