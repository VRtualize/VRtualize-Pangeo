﻿using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DataManagerUtils
{
    public class DataManager
    {
        private DBUtils DBUtil = new DBUtils();
        private MySqlConnection DBConnection;

        public DataManager()
        {
            //Connect to the database and get a connection object
            DBUtil.DBConnect();
            this.DBConnection = this.DBUtil.GetDBConnection();

        }

        //ElevationDataRequest
        async public Task<MapData> ElevationRequest(double longupperleft, double latupperleft, double longlowerright, double latlowerright)
        {
            int ncols = 0;
            int nrows = 0;
            byte[] elevation_data;

            int num_results = 0;

            //Get range of chunks from the database
            String countText = "Select count(*) from usgs_header_data where xulcorner <= " + longlowerright.ToString()
                + " and yulcorner >= " + latlowerright.ToString() + " and xulcorner >= " + longupperleft.ToString() + " and yulcorner <= "
                + latupperleft.ToString();
            MySqlCommand countcmd = new MySqlCommand(countText, this.DBConnection);
            num_results = Convert.ToInt32(countcmd.ExecuteScalar());
            Console.WriteLine(num_results);

            String commandText = "Select * from usgs_header_data where xulcorner <= " + longlowerright.ToString()
                + " and yulcorner >= " + latlowerright.ToString() + " and xulcorner >= " + longupperleft.ToString() + " and yulcorner <= "
                + latupperleft.ToString();
            MySqlCommand query = new MySqlCommand(commandText, this.DBConnection);
            MySqlDataReader queryResult = query.ExecuteReader();

            MapData[] gridData = new MapData[(int)num_results];
            int i = 0;
            while(queryResult.Read())
            {
                gridData[i] = new MapData();
                gridData[i].latlongid = Convert.ToInt32(queryResult[0]);
                gridData[i].ncols = Convert.ToInt32(queryResult[1]);
                gridData[i].nrows = Convert.ToInt32(queryResult[2]);
                gridData[i].xulcorner = Convert.ToDouble(queryResult[3]);
                gridData[i].yulcorner = Convert.ToDouble(queryResult[4]);
                gridData[i].cellsize = Convert.ToDouble(queryResult[5]);
                gridData[i].byteorder = Convert.ToString(queryResult[7]);
                gridData[i].elevation_data = (byte[])(queryResult[8]);
                i++;
            }

            //Check coverage
            int num_points_needed_lat = Math.Abs((int)(((latupperleft - latlowerright) * 111000) / gridData[0].cellsize));
            int num_points_needed_long = Math.Abs((int)(((longupperleft - longlowerright) * 111000) / gridData[0].cellsize));
            Console.WriteLine(num_points_needed_lat);
            Console.WriteLine(num_points_needed_long);

            //TODO Get unavailable chunks from Bing API

            //Create a new map object
            MapData mapToReturn = new MapData();
            mapToReturn.latlongid = 0;
            mapToReturn.ncols = num_points_needed_long;
            mapToReturn.nrows = num_points_needed_lat;
            mapToReturn.xulcorner = longupperleft;
            mapToReturn.yulcorner = latupperleft;
            mapToReturn.cellsize = gridData[0].cellsize;
            mapToReturn.byteorder = gridData[0].byteorder;
            mapToReturn.elevation_data = new byte[num_points_needed_lat * num_points_needed_long];

            //Concatenate the objects into one large mesh
            //Go through, point by point and check if each point is in the desired area
            int j = 0;
            int k = 0;
            double xul_long_meters = (gridData[0].xulcorner * 111000);
            double yul_lat_meters = (gridData[0].yulcorner * 111000);
            Console.WriteLine(xul_long_meters);
            Console.WriteLine(yul_lat_meters);
            double x_side_length = (gridData[0].xulcorner * 111000) + mapToReturn.ncols*mapToReturn.cellsize;
            double y_side_length = (gridData[0].yulcorner * 111000) + mapToReturn.nrows * mapToReturn.cellsize;

            for (i = 0; i < num_results; i++)
            {
                for(j = 0; j < ncols; j++)
                {
                    for(k = 0; k < nrows; k++)
                    {
                        //If the point is within the needed square
                        if((gridData[i].xulcorner * 111000) + j * gridData[i].cellsize > xul_long_meters &&
                           (gridData[i].xulcorner * 111000) + j * gridData[i].cellsize < xul_long_meters + mapToReturn.ncols * mapToReturn.cellsize &&
                           (gridData[i].yulcorner * 111000) - k * gridData[i].cellsize < yul_lat_meters &&
                           (gridData[i].yulcorner * 111000) - k * gridData[i].cellsize > yul_lat_meters - mapToReturn.nrows * mapToReturn.cellsize)
                        {
                            //TODO may have issues near the equator
                            int x = Math.Abs((int)((((gridData[i].xulcorner * 111000) + j * gridData[i].cellsize) - xul_long_meters) / mapToReturn.cellsize));
                            int y = Math.Abs((int)((((gridData[i].yulcorner * 111000) - k * gridData[i].cellsize) - yul_lat_meters) / mapToReturn.cellsize));
                            if (x < mapToReturn.ncols && y < mapToReturn.nrows)
                            {
                                Console.WriteLine(x);
                                Console.WriteLine(y);
                                mapToReturn.elevation_data[y * mapToReturn.ncols + x] = gridData[i].elevation_data[j * k];
                            }
                            
                        }
                    }

                }
            }

            //Return MapData object
            //TODO ADD ELEVATION DATA
            return mapToReturn;
        }


        //ImageDataRequest

        //Conversion
    }
}
