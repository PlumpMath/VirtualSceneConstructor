﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestClass]
    public class SceneEngineTestCases
    {
        private SceneEngine _sceneEngine;

        [TestInitialize]
        public void Init()
        {
            _sceneEngine = new SceneEngine();
        }

        [TestMethod]
        public void InitStateTest()
        {
            Assert.IsNotNull(_sceneEngine.Scene);
            Assert.AreEqual(Constants.SceneEngine.DefaultUpdateRate, _sceneEngine.UpdateRate);
            Assert.IsNotNull(_sceneEngine.Cameras);
            Assert.AreEqual(0, _sceneEngine.Cameras.Count);
        }

        [TestMethod]
        public void SetCorrectUpdateRateTest()
        {
            const int newUpdateRate = Constants.SceneEngine.DefaultUpdateRate * 2;
            _sceneEngine.SetUpdateRate(newUpdateRate);
            Assert.AreEqual(newUpdateRate, _sceneEngine.UpdateRate);
        }

        [TestMethod]
        public void SetInvalidNegativeUpdateRate()
        {
            const int negativeUpdateRate = -1 * Constants.SceneEngine.DefaultUpdateRate;
            _sceneEngine.SetUpdateRate(negativeUpdateRate);
            Assert.AreEqual(Constants.SceneEngine.DefaultUpdateRate, _sceneEngine.UpdateRate);
        }

        [TestMethod]
        public void CreateViewportTest()
        {
            var sceneViewport = _sceneEngine.CreateViewport();
            Assert.IsNotNull(sceneViewport);
            Assert.IsNotNull(sceneViewport.Scene);
            Assert.IsNotNull(sceneViewport.Cameras);
            Assert.AreEqual(0, sceneViewport.Cameras.Count);
        }

        [TestMethod]
        public void CameraAddedToEngineIsAvailableInViewportAlsoTest()
        {
            var sceneViewport = _sceneEngine.CreateViewport();
            _sceneEngine.Cameras.Add(CreateCamera());
            Assert.AreEqual(1, sceneViewport.Cameras.Count);
        }

        private static Camera CreateCamera()
        {
            return CameraFactory.Create<ArcBallCamera>(new Vertex(0,0,0));
        }

    }
}