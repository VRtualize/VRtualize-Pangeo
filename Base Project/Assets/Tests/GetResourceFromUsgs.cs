using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GetResourceFromUsgs
    {
        // A Test behaves as an ordinary method
        [Test]
        public void NewTestScriptSimplePasses()
        {
            // Use the Assert class to test conditions
            Cache cachingObject = new Cache();
            UsgsMapResources resources = new UsgsMapResources();
            //BingMapResources bing = new BingMapResources();
            cachingObject.setMesh(resources, 0, 0);
            Assert.AreEqual(116920969, cachingObject.getMesh().Count);
            //cachingObject.setMesh(bing);
            //Assert.AreEqual(4, cachingObject.getMesh().Count);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
