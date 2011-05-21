using System;

namespace eOculus.Core.Utils
{
    [Serializable]
    public class Vector3D
    {
        private double x;
        private double y;
        private double z;

        public Vector3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3D(Point3D p)
        {
            this.x = p.X;
            this.y = p.Y;
            this.z = p.Z;
        }

        public double Length
        {
            get { return Math.Sqrt(LengthSquare); }
        }

        public double LengthSquare
        {
            get { return (x * x + y * y + z * z); }
        }

        public double X
        {
            get { return x; }
        }

        public double Y
        {
            get { return y; }
        }

        public double Z
        {
            get { return z; }
        }

        public static Vector3D OX
        {
            get { return new Vector3D(1, 0, 0); }
        }

        public static Vector3D OY
        {
            get { return new Vector3D(0, 1, 0); }
        }

        public static Vector3D OZ
        {
            get { return new Vector3D(0, 0, 1); }
        }

        public override bool Equals(object other)
        {
            if (other is Vector3D)
            {
                return (this - (Vector3D)other).Length < Constants.EPSILON;
            }

            return false;
        }

        public Vector3D(Point3D p1, Point3D p2)
        {
            x = p2.X - p1.X;
            y = p2.Y - p1.Y;
            z = p2.Z - p1.Z;
        }

        public static double operator *(Vector3D v1, Vector3D v2)
        {
            return (v1.x * v2.x + v1.y * v2.y + v1.z * v2.z);
        }

        public static Vector3D operator +(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vector3D operator *(double d, Vector3D v)
        {
            return new Vector3D(d * v.x, d * v.y, d * v.z);
        }

        public static Vector3D operator *(Vector3D v, double d)
        {
            return d * v;
        }
    }
}
