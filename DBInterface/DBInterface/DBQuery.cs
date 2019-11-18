using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace DBInterface
{
    /// <summary>
    /// Class <c>DBQuery</c> 
    /// </summary>
    public class DBQuery
    {
        /// <summary>
        /// method <c>GetMapChunks</c> queries for a map chunk from the database.
        /// </summary>
        /// <param name="conn">connection to the database to use.</param>
        /// <param name="xCoor">x-coordinate of the chunk to be retrieved</param>
        /// <param name="yCoor">y-coordinate of the chunk to be retrieved</param>
        public static MapData GetMapChunks(MySqlConnection conn, double xCoor, double yCoor)
        {
            // SQL query to be executed.
            string sql = "SELECT latlongid, xllcorner, yllcorner, nrows, ncols FROM usgs_header_data " + "WHERE abs(xllcorner - (@xcoor)) <= 1 AND xllcorner <= @xcoor AND abs(yllcorner - (@ycoor)) <= 1 AND yllcorner <= @ycoor";
            //string sql = "SELECT latlongid, xllcorner, yllcorner, nrows, ncols, elevation_data FROM usgs_header_data " + "WHERE abs(xllcorner - (@xcoor)) <= 1 AND xllcorner <= @xcoor AND abs(yllcorner - (@ycoor)) <= 1 AND yllcorner <= @ycoor";

            // Create command.
            MySqlCommand cmd = new MySqlCommand();

            // Set timeout.
            cmd.CommandTimeout = 2147483;

            // Set connection for command.
            cmd.Connection = conn;
            cmd.CommandText = sql;

            // Set parameters
            cmd.Parameters.Add("@ycoor", MySqlDbType.Double).Value = yCoor;
            MySqlParameter xCoorParam = new MySqlParameter("@xcoor", MySqlDbType.Double);
            xCoorParam.Value = xCoor;
            cmd.Parameters.Add(xCoorParam);


            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    int latlongid = reader.GetInt32(0);
                    double xllcorner = reader.GetDouble(1);
                    double yllcorner = reader.GetDouble(2);
                    int nrows = reader.GetInt32(3);
                    int ncols = reader.GetInt32(4);
                    int cellsize = nrows * ncols;

                    //byte[] elevation_data;
                    //elevation_data = new byte[cellsize];

                    //reader.GetBytes(reader.GetOrdinal("elevation_data"), 0, elevation_data, 0, (Int32)cellsize);

                    MapData chunk = new MapData(latlongid, ncols, nrows, xllcorner, yllcorner, cellsize);
                    //MapData chunk = new MapData(latlongid, 0, 0, xllcorner, yllcorner, 0, "", elevation_data);
                    return chunk;
                }
                else
                {
                    //throw new Exception("There are no map chunks in the database.");
                    return null;
                }
            }
        }
    }
}
