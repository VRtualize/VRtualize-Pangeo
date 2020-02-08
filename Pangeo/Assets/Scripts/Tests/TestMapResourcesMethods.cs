using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.IO;
using System;

namespace Tests
{
    public class TestMapResourcesMethods
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestGetMesh()
        {
            // Use the Assert class to test conditions
            byte[] data;
            using (FileStream fs = File.OpenRead(@"gridFloatExample.flt"))
            {
                data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
            }

            List<float> known = new List<float>();
            for(int i=0; i < 212; i++)
            {
                for(int j=0; j < 212; j++)
                {
                    known.Add(BitConverter.ToSingle(data, 10812*4*i + 4*j));
                }
            }

            IMapResources tmp = new UsgsMapResources();
            List<float> test = tmp.getMesh(0, 0);

            Assert.AreEqual(known, test);
        }
    }
}
