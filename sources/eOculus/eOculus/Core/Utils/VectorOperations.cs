using System;

namespace eOculus.Core.Utils
{
    public class VectorOperations
    {
        static public double ScalarProduct(Vector3D a, Vector3D b)
        {
            return a * b;
        }

        static public Vector3D VectorProduct(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.Y*b.Z - a.Z*b.Y,
                                a.Z*b.X - a.X*b.Z,
                                a.X*b.Y - a.Y*b.X);
        }

        static public double Distance(Point3D a, Point3D b)
        {
            return (new Vector3D(a, b)).Length;
        }

        static public double Distance(Plane plane, Point3D M)
        {
            return Math.Abs(ScalarProduct(plane.Normal, new Vector3D(M)) -
                            ScalarProduct(plane.Normal, new Vector3D(plane.Point))) / plane.Normal.Length;
        }

        static public double Distance(Line3D L, Point3D M)
        {
            return VectorProduct(new Vector3D(L.Point, M), L.Direction).Length / L.Direction.Length;
        }

        static public double Cos(Vector3D V1, Vector3D V2)  //косинус угла между векторами
        {
            double cos =  ScalarProduct(V1, V2) / (V1.Length * V2.Length);
            return (Math.Abs(cos) <= Constants.EPSILON) ? 0 : cos;
        }

        /// <summary>
        /// Вычисление отраженного луча
        /// </summary>
        /// <param name="N">внешняя нормаль к поверхности в точке падения луча</param>
        /// <param name="V">падающий луч</param>
        /// <param name="reflectedVector">отраженный луч</param>
        /// <returns>
        /// true - когда не false
        /// false - при падении луча на внутреннюю сторону поверхности
        /// </returns>
        static public bool ReflectedVector(Vector3D N, Vector3D V, out Vector3D reflectedVector)
        {
            double cosI = VectorOperations.Cos(N, V);
            Vector3D I = V * (1 / V.Length);  //направление падения луча
            Vector3D normal = N * (1 / N.Length);  //внешняя нормаль к поверхности раздела

            if (cosI >= 0) // считаем, что от внутренней поверхности луч не отражается
            {
                normal *= -1;
                cosI *= -1;
                reflectedVector = null;
                return false;
            }

            reflectedVector = I + 2 * (-cosI) * normal;
            return true;
        }

        /// <summary>
        /// Вычисление преломленного луча при переходе луча из среды 1 в среду 2
        /// </summary>
        /// <param name="N">внешняя нормаль к поверхности в точке раздела 2х сред, направлена в среду 1</param>
        /// <param name="V">падающий луч</param>
        /// <param name="n12">n12 = n2 / n1 
        /// луч переходит из среды 1 с показателем преломления n1 в среду 2 с показателем преломления n2
        /// </param>
        /// <param name="refractedVector">преломленный луч</param>
        /// <returns>
        /// true - когда не false
        /// false - при полном внутреннем отражении
        /// </returns>
        static public bool RefractedVector(Vector3D N, Vector3D V, double n12, out Vector3D refractedVector)
        {
            Vector3D normal;  //внешняя нормаль к поверхности раздела двух сред, направлена в среду 1
            Vector3D I = V * (1 / V.Length);  //направление падения луча

            double cosI = VectorOperations.Cos(N, V);
            if (cosI < 0) // луч идет из среды 1 в среду 2
            {
                cosI *= -1;
                normal = N * (1 / N.Length);
            }
            else    // луч идет из среды 2 в среду 1
            {
                normal = N * (-1 / N.Length);
                n12 = 1.0 / n12;
            }

            double sinR = Math.Sqrt(1.0 - cosI * cosI) / n12;
            if (sinR >= 1.0) //полное внутренне отражение
            {
                refractedVector = null;
                return false;
            }
            else
            {
                refractedVector = (1.0 / n12) * I + (cosI / n12 - Math.Sqrt(1 - sinR * sinR)) * normal;
                return true;
            }
        }
    }
}
