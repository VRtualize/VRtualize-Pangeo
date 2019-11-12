using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DBInterface
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
                + ";port=" + this.port + ";User Id=" + this.username + ";password=" + this.password + ";";

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
}
