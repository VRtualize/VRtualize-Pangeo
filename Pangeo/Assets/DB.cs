using System;
using MySql.Data.MySqlClient;

namespace vr_db_interaction
{
    class DBConnection
    {
        private MySqlConnection connection;
        private string server;
        private string port;
        private string database;
        private string uid;
        private string password;

        public DBConnection()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "manticorite.duckdns.org";
            port = "33066";
            database = "map_data";
            uid = "VRPAN";
            password = "VRPAN";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "PORT=" + port + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        public void printYeet()
        {
            Console.WriteLine("Yeet");
        }

        public void open()
        {
            connection.Open();
        }

        public void sampleQuery()
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM usgs_header_data";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            connection.Close();
            Console.WriteLine("Done.");
        }
    }
}