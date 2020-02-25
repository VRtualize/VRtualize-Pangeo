using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;

namespace DataManagerUtils
{
    /// <summary>
    /// Class <c>DBUtils</c> maintains connection to the database.
    /// </summary>
    public class DBUtils
    {
        private string host;
        private int port;
        private string database;
        private string username;
        private string password;
        private MySqlConnection conn;

        /// <summary>
        /// Constructor initialized connection information to default credentials.
        /// </summary>
        public DBUtils()
        {
            this.host = "manticorite.duckdns.org";
            this.port = 33066;
            this.database = "map_data";
            this.username = "VRPAN";
            this.password = "VRPAN";
        }

        /// <summary>
        /// Constructor that initializes connection information to custom credentials.
        /// </summary>
        /// <param name="host">hostname</param>
        /// <param name="port">port number</param>
        /// <param name="database">name of the database</param>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        public DBUtils(string host, int port, string database, string username, string password)
        {
            this.host = host;
            this.port = port;
            this.database = database;
            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// Deconstructor closes the connection and frees up resources.
        /// </summary>
        ~DBUtils()
        {
            // Close connection.
            this.conn.Close();

            // Dispose object, freeing resources.
            this.conn.Dispose();
        }

        /// <summary>
        /// Connects to the database using stored credentials.
        /// </summary>
        /// <returns>A boolean signifying a successful connection.</returns>
        public bool DBConnect()
        {
            // Create string with connection credentials.
            String connString = "Server=" + this.host + ";Database=" + this.database
                + ";port=" + this.port + ";User Id=" + this.username + ";password=" + this.password + ";default command timeout=0";

            this.conn = new MySqlConnection(connString);

            try
            {
                // Open connection.
                this.conn.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Gets the connection object.
        /// </summary>
        /// <returns>Connection to the database.</returns>
        public MySqlConnection GetDBConnection()
        {
            return this.conn;
        }
    }

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

        //ElevationDataRequest num chunks
        public int ElevationRequestNumChunks(double longupperleft, double latupperleft, double longlowerright, double latlowerright){
            int num_results = 0;

            //Get range of chunks from the database
            String countText = "Select count(*) from usgs_header_data where xulcorner <= " + longlowerright.ToString()
                + " and yulcorner >= " + latlowerright.ToString() + " and xulcorner >= " + longupperleft.ToString() + " and yulcorner <= "
                + latupperleft.ToString();
            MySqlCommand countcmd = new MySqlCommand(countText, this.DBConnection);
            num_results = Convert.ToInt32(countcmd.ExecuteScalar());
            return num_results;
        }
        //mesh.Add(BitConverter.ToSingle(elevationChunk, i));
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
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
            for(i=0;i<256/32;i++){
                for(j=0;j<256/32;j++){
                //TODO MAKE LAT AND LONG ITERABLE
                    String requestString = "http://dev.virtualearth.net/REST/v1/Elevation/Bounds?bounds=" + (latupper - lat_distance * (i+1)) + "," + (longleft + j * long_distance) + "," + (latupper - lat_distance * i) + "," + (longleft + (j+1) * long_distance) + "&rows=32&cols=32&key=" + "9jZ0HHINNLU7kcFYeQl6~XFYbAjS6FYvcXH-iPNVsPg~AgOFExaKkIpdR0kX6qUzR-8HqOONNa9lpAF0l0d6gMoC-U7MLEddz8iMqdesaCCn";
                    var content = await client.GetStringAsync(requestString);
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
                    Thread.Sleep(250);
                }
            }



            for(i=0;i<retrieved_chunks[0].Count;i++){
                Debug.Log(retrieved_chunks[0][i]);
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