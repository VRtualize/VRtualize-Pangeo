using System;
using System.IO;
using System.Collections.Generic;

public class UsgsMapResources : IMapResources
{
	List<float> IMapResources.getMesh() {
		// For now, we're stubbing out a concrete example
		byte[] b;
		using (FileStream fs = new FileStream(@"gridFloatExample.flt", FileMode.Open, FileAccess.Read)) 
		{
			b = new byte[fs.Length];
			fs.Read(b, 0, b.Length);
		}
		int meshLength = 116920969;
		List<float> mesh = new List<float>(meshLength);

		for (int i = 0; i < b.Length; i += 4)
		{
            mesh.Add(BitConverter.ToSingle(b, i));
		}
		return mesh;
	}
	void IMapResources.getSatelliteImagery() { }
}
