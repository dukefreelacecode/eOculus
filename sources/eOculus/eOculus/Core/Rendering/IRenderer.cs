using System.Drawing;
using eOculus.Core.Scenes;

namespace eOculus.Core.Rendering
{
    public interface IRenderer
    {
        Bitmap Render(IScene scene, Size size);
    }
}
