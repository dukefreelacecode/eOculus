using System;
using System.Drawing;
using eOculus.Core.GraphicsObjects;
using eOculus.Core.Rendering;
using eOculus.Core.Utils;

namespace eOculus.Primitives
{
    /// <summary>
    /// Прямоугольник в пространстве, лежащий в плоскости, перпендикулярной оси OZ.
    /// Стороны прямоугольника параллельны осям OX и OY.
    /// Прямоугольник закрашивается как шахматная доска.
    /// </summary>
    [Serializable]
    public class Carpet : IGraphicsObject3D
    {
        private Point3D center;
        private SizeF size;
        private Size resolution;
        private Material material1;
        private Material material2;

        public Carpet(Point3D center, SizeF size, Size resolution, Material material1, Material material2)
        {
            this.center = center;
            this.size = size;
            this.resolution = resolution;
            this.material1 = material1;
            this.material2 = material2;
        }

        public IntersectionInfo Intersection(Ray r)
        {
            Vector3D V = new Vector3D(r.Origin.X - center.X,
                                      r.Origin.Y - center.Y,
                                      r.Origin.Z - center.Z);

            Vector3D normal = Vector3D.OZ;

            double t = -(V * normal) / (r.Direction * normal);

            if (t <= 1e-2 || Double.IsNaN(t) || Double.IsInfinity(t))
                return null;

            Point3D crossPoint = new Point3D(r.Origin.X + r.Direction.X * t,
                                             r.Origin.Y + r.Direction.Y * t,
                                             r.Origin.Z + r.Direction.Z * t);

            if (Math.Abs(center.X - crossPoint.X) > size.Width / 2.0 || Math.Abs(center.Y - crossPoint.Y) > size.Height / 2.0)
            {
                return null;
            }

            double dX = crossPoint.X - (center.X - size.Width / 2.0);
            double dY = crossPoint.Y - (center.Y - size.Height / 2.0);
            double hX = size.Width / resolution.Width;
            double hY = size.Height / resolution.Height;
            int i = (int)(dX / hX);
            int j = (int)(dY / hY);

            Material m = (j % 2 == i % 2) ? material1 : material2;

            return new IntersectionInfo(crossPoint, normal, m);
        }
    }
}
