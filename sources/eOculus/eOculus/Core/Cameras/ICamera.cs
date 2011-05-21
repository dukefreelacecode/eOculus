using System.Drawing;
using eOculus.Core.Rendering;
using eOculus.Core.Utils;

namespace eOculus.Core.Cameras
{
    public interface ICamera
    {
        Point3D Position { get; set; }

        SizeF ProjectionWindow { get; set; }

        double Focus { get; set; }

        Size Resolution { get; set; }

        Rotation Rotation { get; set; }

        Ray GetRay(int i, int j);
    }
}
