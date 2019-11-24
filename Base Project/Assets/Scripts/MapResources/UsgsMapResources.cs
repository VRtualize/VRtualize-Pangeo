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
		//int meshLength = 116920969;
        int meshLength = 256;
		List<float> mesh = new List<float>(meshLength);

		//for (int i = 0; i < b.Length; i += 4)
        for (int i = 0; i < meshLength; i++)
        {
            for (int j = 0; j < meshLength; j++)
            {
                mesh.Add(BitConverter.ToSingle(b, 10813 * 4 * i + j * 4));
            }
        }
		return mesh;
	}
	void IMapResources.getSatelliteImagery() { }
}
