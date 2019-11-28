using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class BingMapResources : IMapResources
{
    public class bingSchema
    {
        public class Resource
        {
            public string __type { get; set; }
            public List<int> elevations { get; set; }
            public int zoomLevel { get; set; }
        }
        public class ResourceSet
        {
            public int estimatedTotal { get; set; }
            public List<Resource> resources { get; set; }
        }
        public class RootObject
        {
            public string authenticationResultCode { get; set; }
            public string brandLogoUri { get; set; }
            public string copyright { get; set; }
            public List<ResourceSet> resourceSets { get; set; }
            public int statusCode { get; set; }
            public string statusDescription { get; set; }
            public string traceId { get; set; }
        }
    }
    List<float> IMapResources.getMesh(int x, int z) {
        // For now, we're stubbing out a concrete example
        bingSchema.RootObject items;
        using (StreamReader fs = new StreamReader(@"bingExample.json")) 
        {
            string json = fs.ReadToEnd();
            items = JsonUtility.FromJson<bingSchema.RootObject>(json);
        }
        int meshLength = items.resourceSets[0].resources[0].elevations.Count;
        List<float> mesh = new List<float>(meshLength);
        for(int i = 0; i < meshLength; i++)
            mesh.Add(items.resourceSets[0].resources[0].elevations[i]);
        return mesh;
    }
    void IMapResources.getSatelliteImagery() { }
}
