using System;
using eOculus.Core.GraphicsObjects;
using eOculus.Core.Utils;
using eOculus.Core.Rendering;

namespace eOculus.Primitives
{
    [Serializable]
    public class Triangle3D : IGraphicsObject3D
    {
        private Point3D A;
        private Point3D B;
        private Point3D C;
        private Material material_front;
        private Material material_back;

        private double epsilon = 1e-2;

        public Triangle3D(Point3D v1, Point3D v2, Point3D v3, Material material)
        {
            this.A = v1;
            this.B = v2;
            this.C = v3;
            this.material_front = material;
            this.material_back = null;
        }

        public Triangle3D(Point3D v1, Point3D v2, Point3D v3, Material material_front, Material material_back)
        {
            this.A = v1;
            this.B = v2;
            this.C = v3;
            this.material_front = material_front;
            this.material_back = material_back;
        }

        public Point3D VertexA
        {
            get { return A; }
        }

        public Point3D VertexB
        {
            get { return B; }
        }

        public Point3D VertexC
        {
            get { return C; }
        }

        public IntersectionInfo Intersection(Ray r)
        {
            Vector3D N = VectorOperations.VectorProduct(new Vector3D(A, B), new Vector3D(A, C));
            //нормаль определяет внешнюю и внутреннюю сторону треугольника
            N *= (1 / N.Length);
            Vector3D V = new Vector3D(r.Origin.X - A.X,
                                      r.Origin.Y - A.Y,
                                      r.Origin.Z - A.Z);
            double t = -VectorOperations.ScalarProduct(V, N) / VectorOperations.ScalarProduct(r.Direction, N);

            if (t <= epsilon || Double.IsNaN(t) || Double.IsInfinity(t))
                return null;

            Point3D crossPoint = new Point3D(r.Origin.X + r.Direction.X * t,
                                             r.Origin.Y + r.Direction.Y * t,
                                             r.Origin.Z + r.Direction.Z * t);

            Vector3D OX = new Vector3D(A, B);
            Vector3D AC = new Vector3D(A, C);
            Vector3D AD = new Vector3D(A, crossPoint);
            Vector3D OY = VectorOperations.VectorProduct(N, OX);

            Line3D axis = new Line3D(A, OX);
            Line3D ordinate = new Line3D(A, OY);

            double Bx = VectorOperations.Distance(ordinate, B);
            double By = 0;

            double Cx = VectorOperations.Distance(ordinate, C);
            if (VectorOperations.Cos(OX, AC) < 0)
                Cx *= -1;

            double Cy = VectorOperations.Distance(axis, C);
            if (VectorOperations.Cos(OY, AC) < 0)
                Cy *= -1;

            double Dx = VectorOperations.Distance(ordinate, crossPoint);
            if (VectorOperations.Cos(OX, AD) < 0)
                Dx *= -1;

            double Dy = VectorOperations.Distance(axis, crossPoint);
            if (VectorOperations.Cos(OY, AD) < 0)
                Dy *= -1;

           
            int s1 = Math.Sign(Bx * Dy - By * Dx);
            int s2 = Math.Sign((Cx - Bx) * (Dy - By) - (Cy - By) * (Dx - Bx));
            int s3 = Math.Sign(-Cx * (Dy - Cy) + Cy * (Dx - Cx));

            if (s1 == s2 && s1 == s3 && s2 == s3)
            {
                Material m = material_front;
                if (VectorOperations.Cos(N, r.Direction) >= -epsilon && (material_back != null))
                {
                    N *= (-1);
                    m = material_back;
                }

                return new IntersectionInfo(crossPoint, N, m);
            }

            return null;
        }
    }
}
