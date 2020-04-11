using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DataManagerUtils;
using MySql.Data.MySqlClient;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// The Cache class provides functions to get a mesh using Unity grid coordinates,
/// get satellite imagery for a mesh using the same coordinates, and provides 
/// database functionality to get both meshes and satellite imagery if it exists 
/// in the connected database. Credentials and address for the database must be 
/// placed in the config file.
/// </summary>
public class Cache
{
    private string host;
    private int port;
    private string database;
    private string username;
    private string password;

    /// <summary>
    /// This function take in a quadkey and returns whether it already has
    /// an entry in the database or not.
    /// </summary>
    /// <param name="quadkey">A quadkey for the Bing Maps Tile System</param>
    /// <returns></returns>
    public bool DBcheck(string quadkey){

        //Create a connection to the MySQL Database
        MySqlConnection conn = DBConnect();

        //Perform a count query to see if the chunk exists in the database
        String query = "Select count(*) from image_cache where quadkey = " + quadkey;
        MySqlCommand cmd = new MySqlCommand(query, conn);
        int num_results = Convert.ToInt32(cmd.ExecuteScalar());

        conn.Close();

        return (num_results > 0);

    }

    /// <summary>
    /// This function gets a quadkey entry from the database and returns a list of elevations
    /// and the satellite image for that tile as a texture.
    /// </summary>
    /// <param name="quadkey">A quadkey for the Bing Maps Tile System</param>
    /// <returns></returns>
    public Tuple<List<float>, Texture> DBGet(String quadkey)
    {

        //Create a connection to the MySQL Database
        MySqlConnection conn = DBConnect();

        //Perform a count query to see if the chunk exists in the database
        String query = "Select * from image_cache where quadkey = " + quadkey + " LIMIT 1";
        MySqlCommand cmd = new MySqlCommand(query, conn);
        MySqlDataReader queryResult = cmd.ExecuteReader();
        queryResult.Read();

        //Convert the image to a Texture
        Texture2D mat = new Texture2D(32,32, TextureFormat.RGBA32, false);
        mat.LoadImage((byte[])queryResult[2]);

        //Take the binary elevations and make them into a list of floats
        List<float> ElevList = new List<float>();
        byte[] ElevData = (byte[])queryResult[3];
        for (int i = 0; i < ElevData.Length/4; i++){
            ElevList.Add(BitConverter.ToSingle(ElevData, i*4));
        }

        conn.Close();

        return new Tuple< List<float>, Texture > (ElevList, mat);
    }

    /// <summary>
    /// This function takes a list of elevations and the satellite imagery for 
    /// the tile using the quadkey and places it into the database.
    /// </summary>
    /// <param name="quadkey">A quadkey for the BingMaps tile system</param>
    /// <param name="elevations">A list of elevations for the current chunk starting 
    /// from the upper left corner</param>
    /// <param name="mat">The satellite image for this tile</param>
    public void DBInsert(String quadkey, List<float> elevations, byte[] mat){
        //Create a connection to the MySQL Database
        MySqlConnection conn = DBConnect();

        //Convert the elevation list to a bytearray
        byte[] elevationList = new byte[elevations.Count*4];
        for(int i = 0; i < elevations.Count; i++){
            byte[] temp_float = BitConverter.GetBytes(elevations[i]);
            for(int j = 0; j < 4; j++){
                elevationList[i * 4 + j] = temp_float[j];
            }
        }

        //Put the chunk in the database
        String query = "Insert into image_cache(quadkey, zoomlevel, image_data, elevations) values(@quadkey, @zoomLevel, @image_data, @elevations);";
        using ( var cmd = new MySqlCommand(query, conn))
        {
            cmd.Parameters.Add("@quadkey", MySqlDbType.VarChar).Value = quadkey;
            cmd.Parameters.Add("@zoomLevel", MySqlDbType.Int16).Value = quadkey.Length;
            cmd.Parameters.Add("@image_data", MySqlDbType.MediumBlob).Value = mat;
            cmd.Parameters.Add("@elevations", MySqlDbType.MediumBlob).Value = elevationList;
            cmd.ExecuteNonQuery();
        }
        conn.Close();
    }

    /// <summary>
    /// Creates a connection to the database using the configuration file credentials
    /// </summary>
    /// <returns></returns>
    public MySqlConnection DBConnect()
    {
        string[] lines = System.IO.File.ReadAllLines(@"Assets/config");

        this.host = lines[1].Substring(9, lines[1].Length - 10); ;
        this.port = Convert.ToInt32(lines[2].Substring(9, lines[2].Length - 10)) ;
        this.database = lines[3].Substring(18, lines[3].Length - 19); ;
        this.username = lines[4].Substring(13, lines[4].Length - 14); ;
        this.password = lines[5].Substring(13, lines[5].Length - 14); ;

        // Create string with connection credentials.
        String connString = "Server=" + this.host + ";Database=" + this.database
            + ";port=" + this.port + ";User Id=" + this.username + ";password=" + this.password + ";default command timeout=0";

        MySqlConnection conn = new MySqlConnection(connString);

        try
        {
            conn.Open();
            return conn;
        }
        catch (Exception e)
        {
            Debug.Log("Connection was not able to be opened for MySql. This error occurs inside Cache.cs: DBConnect()");
            Debug.Log("Error: " + e);
            Debug.Log(e.StackTrace);
            return conn;
        }
    }
}
