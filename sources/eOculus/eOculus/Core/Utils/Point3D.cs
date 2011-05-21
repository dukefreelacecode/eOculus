using System;

namespace eOculus.Core.Utils
{
    [Serializable]
    public class Point3D
    {
        private static Point3D zero = new Point3D(0, 0, 0);

        private double x;
        private double y;
        private double z;

        public Point3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
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

        public static Point3D Zero
        {
            get { return zero; }
        }

        public override bool Equals(object other)
        {
            if (other is Point3D)
            {
                return (new Vector3D(this, (Point3D)other)).Length < Constants.EPSILON;
            }

            return false;
        }
    }
}
