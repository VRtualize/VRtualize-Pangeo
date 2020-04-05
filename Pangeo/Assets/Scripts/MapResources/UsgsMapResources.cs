using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UsgsMapResources : IMapResources
{
    List<float> IMapResources.getMesh(float x, float z)
    {
        // For now, we're stubbing out a concrete example
        byte[] b;
        using (FileStream fs = new FileStream(@"gridFloatExample.flt", FileMode.Open, FileAccess.Read))
        {
            b = new byte[fs.Length];
            fs.Read(b, 0, b.Length);
        }
        //int meshLength = 116920969;
        int meshLength = 212;
        //int meshLength = (int)Math.Sqrt(b.Length);
        //DataManager manager = new DataManager();
        //MapData map = manager.ElevationRequest(-104, 40.05, -103.95, 40);
        //byte[] b;
        //b = map.elevation_data;
        //int meshLength = b.Length/4;

        //Globals.length = (int)b.Length;
        //Globals.meshLength = meshLength;

        List<float> mesh = new List<float>(meshLength);

        for (int i = 0; i < b.Length - 4; i += 4)
            mesh.Add(BitConverter.ToSingle(b, i));
        return mesh;
    }
    async Task<byte[]> IMapResources.getSatelliteImagery(float x, float z)
    {
        return null;
    }
}
