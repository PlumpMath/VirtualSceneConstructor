using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.Version;
using VirtualScene.BusinessComponents.Core.Validators;

namespace VirtualScene.BusinessComponents.Core.Factories
{
    /// <summary>
    /// Factory creating scenes
    /// </summary>
    public class SceneFactory
    {
        private readonly OpenGL _gl = new OpenGL();

        /// <summary>
        /// Creates a new scene
        /// </summary>
        /// <param name="width">Width of the scene</param>
        /// <param name="height">Height of the scene</param>
        /// <param name="bitDepth">Color depth of the scene</param>
        /// <returns></returns>
        public virtual Scene Create(int width = Constants.Scene.MinimumWidth, int height = Constants.Scene.MinimumHeight, int bitDepth = Constants.Scene.MaximumColorDepth)
        {
            SceneArgumentValidator.ValidateArguments(width, height, bitDepth);
            _gl.Create(OpenGLVersion.OpenGL4_4, RenderContextType.DIBSection, width, height, bitDepth, null);
            var scene = new Scene { OpenGL = _gl };
            SharpGL.SceneGraph.Helpers.SceneHelper.InitialiseModelingScene(scene);
            return scene;
        }
    }
}