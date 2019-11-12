using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBInterface;
using MySql.Data.MySqlClient;

namespace DBInterfaceTest
{
    [TestClass]
    public class DBUtilsTest
    {
        [TestMethod]
        public void DBConnection_BadConnection()
        {
            // arrange connection with bad certificates
            DBUtils util = new DBUtils("badhost", 0, "db", "user", "pass");

            // act
            bool connection_status = util.DBConnect();

            // assert
            Assert.IsFalse(connection_status);
        }
    }
}
