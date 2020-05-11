using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ScenesTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ScenesTestsSimplePasses()
        {
            GameObject sceneLoader = GameObject.FindGameObjectWithTag("SceneLoader");

            Assert.IsNotNull(sceneLoader);
        }

    }
}
