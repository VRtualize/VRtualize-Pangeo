using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DataManagerUtils;
using MySql.Data.MySqlClient;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class Cache
{
    private string host;
    private int port;
    private string database;
    private string username;
    private string password;

    private List<float> mesh;
	public List<float> getMesh(IMapResources res, float x, float z) { return res.getMesh(x, z); }
	async public Task<byte[]> getSatelliteImagery(IMapResources res, float x, float z) { return await res.getSatelliteImagery(x, z); }
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

    public Tuple<List<float>, Texture> DBGet(String quadkey)
    {

        //Create a connection to the MySQL Database
        MySqlConnection conn = DBConnect();

        //Perform a count query to see if the chunk exists in the database
        String query = "Select * from image_cache where quadkey = " + quadkey + " LIMIT 1";
        MySqlCommand cmd = new MySqlCommand(query, conn);
        MySqlDataReader queryResult = cmd.ExecuteReader();
        queryResult.Read();

        Texture2D mat = new Texture2D(32,32, TextureFormat.RGBA32, false);
        mat.LoadImage((byte[])queryResult[2]);

        List<float> ElevList = new List<float>();
        byte[] ElevData = (byte[])queryResult[3];
        for (int i = 0; i < ElevData.Length/4; i++){
            ElevList.Add(BitConverter.ToSingle(ElevData, i*4));
        }

        conn.Close();
        return new Tuple< List<float>, Texture > (ElevList, mat);
    }

    public void DBInsert(String quadkey, List<float> elevations, byte[] mat){
        //Create a connection to the MySQL Database
        MySqlConnection conn = DBConnect();

        byte[] elevationList = new byte[elevations.Count*4];
        for(int i = 0; i < elevations.Count; i++){
            byte[] temp_float = BitConverter.GetBytes(elevations[i]);
            for(int j = 0; j < 4; j++){
                elevationList[i * 4 + j] = temp_float[j];
            }
        }

        //Perform a count query to see if the chunk exists in the database
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

    public MySqlConnection DBConnect()
    {
        this.host = "manticorite.duckdns.org";
        this.port = 33066;
        this.database = "map_data";
        this.username = "VRPAN";
        this.password = "VRPAN";

        // Create string with connection credentials.
        String connString = "Server=" + this.host + ";Database=" + this.database
            + ";port=" + this.port + ";User Id=" + this.username + ";password=" + this.password + ";default command timeout=0";

        MySqlConnection conn = new MySqlConnection(connString);

        try
        {
            // Open connection.
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
