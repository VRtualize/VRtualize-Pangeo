using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;

namespace DataManagerUtils
{


    public class DataManager
    {
        

        //ElevationDataRequest
        public List<float> ElevationRequest(double latupper, double longleft, double latlower, double longright, int sideLength, int zoomLevel)
        {
            Debug.Log("DSTART");
            int i = 0;
            int j = 0;
            int k = 0;

            //Request Bing API elevations using input bounding box
            //var client = new HttpClient();
            // client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

            String requestString = "http://dev.virtualearth.net/REST/v1/Elevation/Bounds?bounds=" + (latlower) + "," + (longright) + "," + (latupper) + "," + (longleft) + "&rows=32&cols=32&key=" + "AvOVJReSdFZbRWwS3JZI91yN4JLK4RH5lW6mdFTcJOdE-U5PFmC2hGgUtWUM6wsr";
            //var content = await client.GetStringAsync(requestString);
            var content = BingApiRequestManager.getUrlData(requestString);
            int start = content.IndexOf("\"elevations\"") + 14;
            int end = content.IndexOf("\"zoomLevel\"") - 2;
            String elevation_string = content.Substring(start, end - start);
            List<String> elevation_strings = elevation_string.Split(',').ToList();
            List<float> retrieved_chunk = new List<float>();  
            for (k = 0; k < elevation_strings.Count; k++)
            {
                retrieved_chunk.Add(Convert.ToSingle(elevation_strings[k]));
            }

            List<float> newElevChunk = new List<float>();
            //Place elevations into elevation chunk
            for (i = 0; i < Math.Sqrt(retrieved_chunk.Count); i++)
            {
                for (j = 0; j < Math.Sqrt(retrieved_chunk.Count); j++)
                {
                    newElevChunk.Add(retrieved_chunk[(31-j) * Convert.ToInt32(Math.Sqrt(retrieved_chunk.Count)) + (31-i)]);
                }
            }
            Debug.Log("DEND");
            return newElevChunk;
        }


        //ImageDataRequest

        //Conversion
    }
}
