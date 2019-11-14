using System.Collections.Generic;
public class Cache
{
	private List<List<float>> mesh;
	public List<List<float>> getMesh() { return mesh; }
	public void setMesh(IResources resources) { 
		mesh = resources.getMesh();
	}
	public static void getSatelliteImagery() { }
}

