using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBInterface;
using MySql.Data.MySqlClient;

namespace DBInterfaceTest
{
    [TestClass]
    public class DBQueryTest
    {
        [TestMethod]
        public void Query_GetMapChunk()
        {
            // arrange connection
            DBUtils util = new DBUtils();
            util.DBConnect();
            MySqlConnection conn = util.GetDBConnection();

            // act
            MapData chunk = DBQuery.GetMapChunks(conn, -103.205985, 44.074386);

            // assert
            Assert.IsNotNull(chunk);
        }

        [TestMethod]
        public void Query_GetMapChunk_NotInDatabase()
        {
            // arrange connection
            DBUtils util = new DBUtils();
            util.DBConnect();
            MySqlConnection conn = util.GetDBConnection();

            // act
            MapData chunk = DBQuery.GetMapChunks(conn, 0, 0);

            // assert
            Assert.IsNull(chunk);
        }
    }
}
