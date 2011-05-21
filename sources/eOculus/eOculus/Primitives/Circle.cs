using System;
using eOculus.Core.GraphicsObjects;
using eOculus.Core.Rendering;
using eOculus.Core.Utils;

namespace eOculus.Primitives
{
    /// <summary>
    /// диск в пространстве
    /// </summary>
    [Serializable]
    public class Circle : IGraphicsObject3D
    {
        private Point3D center;
        private Vector3D normal;
        private double radius;
        private Material m;
        private double epsilon = 1e-2;

        public Circle(Point3D center, Vector3D normal, double radius, Material m)
        {
            this.center = center;
            this.normal = normal * (1.0 / normal.Length);
            this.radius = radius;
            this.m = m;
        }

        public IntersectionInfo Intersection(Ray r)
        {
            Vector3D V = new Vector3D(r.Origin.X - center.X,
                                      r.Origin.Y - center.Y,
                                      r.Origin.Z - center.Z);

            double t = -VectorOperations.ScalarProduct(V, normal) / VectorOperations.ScalarProduct(r.Direction, normal);

            if (t <= epsilon || Double.IsNaN(t) || Double.IsInfinity(t))
                return null;

            Point3D crossPoint = new Point3D(r.Origin.X + r.Direction.X * t,
                                             r.Origin.Y + r.Direction.Y * t,
                                             r.Origin.Z + r.Direction.Z * t);

            if (VectorOperations.Distance(crossPoint, center) > radius)
                return null;

            return new IntersectionInfo(crossPoint, normal, m);
        }
    }
}
