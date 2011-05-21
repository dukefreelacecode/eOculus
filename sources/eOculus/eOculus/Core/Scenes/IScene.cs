using System.Collections.Generic;
using System.Drawing;
using eOculus.Core.Cameras;
using eOculus.Core.GraphicsObjects;
using eOculus.Core.Lights;
using eOculus.Core.Rendering;

namespace eOculus.Core.Scenes
{
    public interface IScene
    {
        void AddGraphicsObject(IGraphicsObject3D obj);

        void AddLight(Light light);

        IEnumerator<IGraphicsObject3D> GraphicsObjects { get; }

        IEnumerator<Light> Lights { get; }

        ICamera Camera { get; set; }

        double Ambient { get; set; }

        Color InfinityColor { get; set; }

        Bitmap Draw(IRenderer renderer, Size size);
    }
}
