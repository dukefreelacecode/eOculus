using eOculus.Core.Rendering;

namespace eOculus.Core.GraphicsObjects
{
    public interface IGraphicsObject3D
    {
        IntersectionInfo Intersection(Ray r);
    }
}
