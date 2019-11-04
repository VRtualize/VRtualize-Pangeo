using System;
using MySql.Data.MySqlClient;

public class Map_Retrieval
{
    static void Main()
    {
        string cs = @"server=localhost;database=map_data";
        MySqlConnection conn = null;

        using (MySqlConnection sqlConn = new MySqlConnection(cs))
        {
            string stm = @"SELECT * FROM usgs_header_data";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable maps = new DataTable();
            da.Fill(maps);
            data.GridView1.DataSource = new BindingSource(maps, null);
        }

        // try
        // {
        //     conn = new MySqlConnection(cs);
        //     conn.Open();
        //     Console.WriteLine("MySQL version : {0}", conn.ServerVersion);

        //     string stm = "SELECT * FROM usgs_header_data";
        //     MySqlCommand cmd = new MySqlCommand(stm, conn);
        //     rdr = cmd.ExecuteReader();

        //     Console.WriteLine("{0} {1}", rdr.GetName(0), rdr.GetName(1).PadLeft(18));

        //     while (rdr.Read())
        //     {
        //         Console.WriteLine(rdr.GetString(0).PadRight(18) + rdr.GetString(1));
        //     }
        // }
        // catch (MySqlException ex)
        // {
        //     Console.WriteLine("Error: {0}", ex.ToString());
        // }
        // finally
        // {
        //     if (rdr != null)
        //     {
        //         rdr.Close();
        //     }

        //     if (conn != null)
        //     {
        //         conn.Close();
        //     }
        // }
    }
}