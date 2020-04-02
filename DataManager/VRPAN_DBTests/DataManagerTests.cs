using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DataManagerUtils;

namespace DataManagerUtils.Tests
{
    [TestClass()]
    public class DataManagerTests
    {
        [TestMethod()]
        public async System.Threading.Tasks.Task ElevationRequestTestAsync()
        {
            DataManager testManager = new DataManager();
            MapData tempMapData = await testManager.ElevationRequest(0.0, 0.0, 0.0, 0.0);
            Assert.AreEqual(tempMapData, new MapData(0, 0, 0, 0.0, 0.0, 0.0, "LSBFIRST", null));
        }
    }
}