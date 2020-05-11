using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GameControllerTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void GameControllerTestSimplePasses()
        {
            GameObject gameController = GameObject.FindGameObjectWithTag("GameController");

            Assert.IsNotNull(gameController);
        }

        [Test]
        public void GameControllerHasScript ()
        {
            GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
            Component[] components = gameController.GetComponents<Component>();
            List<String> names = new List<string>();
            int tst = 500;
            for (int i = 0; i < components.Length; i++)
            {
                names.Add(components[i].name);
            }

            CollectionAssert.Contains(names, "Game Session");

        }

    }
}
