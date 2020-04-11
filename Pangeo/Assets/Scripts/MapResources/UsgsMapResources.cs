using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// UsgsMapResources implements the IMapResources interface to pull data from USGS GIS
/// </summary>
public class UsgsMapResources : IMapResources
{
    /// <summary>
    /// Override the getMesh function to return a list of elevation points from USGS gridfloat files.
    /// </summary>
    /// <param name="x">Unity units in direction x from origin</param>
    /// <param name="z">Unity units in direction z from origin</param>
    /// <returns>A list of elevation points</returns>
    List<float> IMapResources.getMesh(float x, float z)
    {
        // For now, we're stubbing out a concrete example
        byte[] b;
        using (FileStream fs = new FileStream(@"gridFloatExample.flt", FileMode.Open, FileAccess.Read))
        {
            b = new byte[fs.Length];
            fs.Read(b, 0, b.Length);
        }
        int meshLength = 212;
        List<float> mesh = new List<float>(meshLength);

        for (int i = 0; i < b.Length - 4; i += 4)
            mesh.Add(BitConverter.ToSingle(b, i));
        return mesh;
    }
    /// <summary>
    /// Override the getSatelliteImagery to get an image from Usgs.
    /// </summary>
    /// <param name="x">Unity units in direction x from origin</param>
    /// <param name="z">Unity units in direction z from origin</param>
    /// <returns>A 2D texture of the image as a byte stream</returns>
    async Task<byte[]> IMapResources.getSatelliteImagery(float x, float z)
    {
        return null;
    }
}
