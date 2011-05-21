using System;

namespace eOculus.Core.Utils
{
    [Serializable]
    public class Rotation
    {
        //углы поворота системы вокруг своих координатных осей 
        private double A;
        private double B;
        private double C;

        private double[,] matrix = new double[3, 3];
        private double[,] matrixInverse = new double[3, 3];

        public Rotation(double A, double B, double C)
        {
            this.A = A; //1. вокруг оси Z
            this.B = B; //2. вокруг оси Y
            this.C = C; //3. вокруг оси X

            matrix[0,0] =  Math.Cos(A) * Math.Cos(B);
            matrix[0,1] = -Math.Sin(A) * Math.Cos(C) + Math.Cos(A) * Math.Sin(B) * Math.Sin(C);
            matrix[0,2] =  Math.Sin(A) * Math.Sin(C) + Math.Cos(A) * Math.Sin(B) * Math.Cos(C);

            matrix[1,0] =  Math.Sin(A) * Math.Cos(B);
            matrix[1,1] =  Math.Cos(A) * Math.Cos(C) + Math.Sin(A) * Math.Sin(B) * Math.Sin(C);
            matrix[1,2] = -Math.Cos(A) * Math.Sin(C) + Math.Sin(A) * Math.Sin(B) * Math.Cos(C);

            matrix[2,0] = -Math.Sin(B);
            matrix[2,1] =  Math.Sin(C) * Math.Cos(B);
            matrix[2,2] =  Math.Cos(C) * Math.Cos(B);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrixInverse[i, j] = matrix[j, i];
                }
            }
        }

        public double AroundZ
        {
            get { return A; }
        }

        public double AroundY
        {
            get { return B; }
        }

        public double AroundX
        {
            get { return C; }
        }

        public Vector3D Transform(Vector3D v)   //из видовых координат в мировые
        {
            double[] x = new double[3];
            for (int i = 0; i < 3; i++)
            {
                x[i] = matrix[i, 0] * v.X + matrix[i, 1] * v.Y + matrix[i, 2] * v.Z;
            }
            Vector3D res = new Vector3D(x[0], x[1], x[2]);
            return res;
        }

        public Point3D Transform(Point3D P)
        {
            Vector3D v = this.Transform(new Vector3D(P));
            return new Point3D(v.X, v.Y, v.Z);
        }

        public Vector3D TransformInverse(Vector3D v)    //из мировых координат в видовые
        {
            double[] x = new double[3];
            for (int i = 0; i < 3; i++)
            {
                x[i] = matrixInverse[i, 0] * v.X + matrixInverse[i, 1] * v.Y + matrixInverse[i, 2] * v.Z;
            }
            Vector3D res = new Vector3D(x[0], x[1], x[2]);
            return res;
        }

        public Point3D TransformInverse(Point3D P)
        {
            Vector3D v = this.TransformInverse(new Vector3D(P));
            return new Point3D(v.X, v.Y, v.Z);
        }
    }
}
