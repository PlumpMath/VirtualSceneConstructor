﻿using SharpGL.SceneGraph.Primitives;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// The command creates a cube and adds it to the scene
    /// </summary>
    public class AddCubeCommand : AddSceneObjectCommandBase
    {
        private int _x = 0;
        private int _y = 0;
        /// <summary>
        /// Creates a new instance of the AddCubeCommand
        /// </summary>
        /// <param name="sceneContent"></param>
        public AddCubeCommand(SceneContent sceneContent): base(sceneContent)
        {
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        protected override void Execute()
        {
            _x += 1;
            _y += 1;
            BusinessManager.AddSceneElementInSpace<Cube>(SceneEngine.Scene, _x, _y, 0);
        }
    }
}
