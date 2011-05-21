using System;
using System.Drawing;
using eOculus.Core.GraphicsObjects;
using eOculus.Core.Utils;
using eOculus.Core.Rendering;

namespace eOculus.Primitives
{
    [Serializable]
    public class Sphere : IGraphicsObject3D
    {
        private double radius;
        private Point3D center;
        private Material material;

        public Sphere(Point3D origin, double radius, Material material)
        {
            this.radius = radius;
            this.center = origin;
            this.material = material;
        }

        public Point3D Center
        {
            get { return center; }
        }

        public double Radius
        {
            get { return radius; }
        }

        public virtual IntersectionInfo Intersection(Ray r)
        {
            double epsilon = 0.01;
            Vector3D v = new Vector3D(r.Origin.X - center.X, r.Origin.Y - center.Y, r.Origin.Z - center.Z);
            double A = r.Direction.LengthSquare;
            double B = VectorOperations.ScalarProduct(r.Direction, v);
            double C = v.LengthSquare - radius * radius;
            double D = B * B - A * C;

            if (D <= 0.0)
                return null;

            double t1 = (-B - Math.Sqrt(D)) / A;
            double t2 = (-B + Math.Sqrt(D)) / A;
            double t = t1;

            if (t2 <= epsilon)
                return null;

            if (t1 <= epsilon)    //луч идет из шара
                t = t2;

            Point3D P = new Point3D(r.Origin.X + r.Direction.X * t,
                                    r.Origin.Y + r.Direction.Y * t,
                                    r.Origin.Z + r.Direction.Z * t);    //первая точка пересечения

            Vector3D N = new Vector3D(center,  P);   //вектор нормали единичной длины
            N *= (1 / N.Length);
            if (t1 <= epsilon)    //луч идет из шара
                N *= (-1.0);

            return new IntersectionInfo(P, N, material);
        }
    }
}
