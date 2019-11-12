using System;
using MySql.Data.MySqlClient;

namespace vr_db_interaction
{
    class DBConnection
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public DBConnection()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "map_data";
            uid = "root";
            password = "Tacotime2";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
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
    }
}
