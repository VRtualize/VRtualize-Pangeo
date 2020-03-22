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
        async public Task<List<float>> ElevationRequest(double latupper, double longleft, double latlower, double longright, int sideLength, int zoomLevel)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            double lat_distance = (latupper - latlower)/8;
            double long_distance = (longright - longleft)/8;

            //Make byte array of the size sidelength * sidelength
            List<float> elevation_chunk = new List<float>(sideLength*sideLength);
            List<List<float>> retrieved_chunks = new List<List<float>>();

            //Request Bing API elevations using input bounding box
            //var client = new HttpClient();
            // client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

            for(i=0;i<256/32;i++){
                for(j=0;j<256/32;j++){
                //TODO MAKE LAT AND LONG ITERABLE
                    String requestString = "http://dev.virtualearth.net/REST/v1/Elevation/Bounds?bounds=" + (latupper - lat_distance * (i+1)) + "," + (longleft + j * long_distance) + "," + (latupper - lat_distance * i) + "," + (longleft + (j+1) * long_distance) + "&rows=32&cols=32&key=" + "AvOVJReSdFZbRWwS3JZI91yN4JLK4RH5lW6mdFTcJOdE-U5PFmC2hGgUtWUM6wsr";
                    //var content = await client.GetStringAsync(requestString);
                    var content = await BingApiRequestManager.getUrlData(requestString);
                    int start = content.IndexOf("\"elevations\"") + 14;
                    int end = content.IndexOf("\"zoomLevel\"") - 2;
                    String elevation_string = content.Substring(start, end - start);
                    List<String> elevation_strings = elevation_string.Split(',').ToList();
                    List<float> retrieved_chunk = new List<float>();
                    for (k = 0; k < elevation_strings.Count; k++)
                    {
                        retrieved_chunk.Add(Convert.ToSingle(elevation_strings[k]));
                    }
                    retrieved_chunks.Add(retrieved_chunk);
                }
            }

            //Put squares into elevation chunks
            int l = 0;
            //Get through every row
            for (l = 0; l < 8; l++)
            {
                //Find the row in that chunk
                for (i = 0; i < 32; i++)
                {
                    //This is the current chunk in the row we are in
                    for (j = 0; j < 8; j++)
                    {
                        //Get all bits
                        for (k = 0; k < 32; k++)
                            elevation_chunk.Add(retrieved_chunks[l*8 + j][(31-i) * 32 + k]);
                    }
                }
            }

            List<float> newElevChunk = new List<float>();
            //Place elevations into elevation chunk
            for(i=0;i<Math.Sqrt(elevation_chunk.Count);i++){
                for (j = 0; j < Math.Sqrt(elevation_chunk.Count); j++){
                    newElevChunk.Add(elevation_chunk[j * Convert.ToInt32(Math.Sqrt(elevation_chunk.Count)) + i]);
                }
            }

            return newElevChunk;
        }


        //ImageDataRequest

        //Conversion
    }
}
