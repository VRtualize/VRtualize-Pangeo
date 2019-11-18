using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBInterface;

namespace DBInterfaceTest
{
    [TestClass]
    public class MapDataTest
    {
        MapData map;
        [TestInitialize]
        public void Initialize()
        {
            this.map = new MapData(1, 10, 15, 10.001, 15.01, 100.02, "LSBFIRST");
        }

        [TestMethod]
        public void Get_Latlongid()
        {
            int latlongid = map.GetLatLongId();
            Assert.AreEqual(1, latlongid);
        }

        [TestMethod]
        public void Get_NCols()
        {
            int ncols = map.GetNCols();
            Assert.AreEqual(10, ncols);
        }

        [TestMethod]

        public void Get_NRows()
        {
            int nrows = map.GetNRows();
            Assert.AreEqual(15, nrows);
        }

        [TestMethod]

        public void Get_Xllcorner()
        {
            double xllcorner = map.GetXllcorner();
            Assert.AreEqual(10.001, xllcorner);
        }

        [TestMethod]

        public void Get_Yllcorner()
        {
            double yllcorner = map.GetYllcorner();
            Assert.AreEqual(15.01, yllcorner);
        }

        [TestMethod]

        public void Get_Cellsize()
        {
            double cellsize = map.GetCellsize();
            Assert.AreEqual(100.02, cellsize);
        }

        [TestMethod]

        public void Get_Byteorder()
        {
            string byteorder = map.GetByteorder();
            Assert.AreEqual("LSBFIRST", byteorder);
        }
    }
}
