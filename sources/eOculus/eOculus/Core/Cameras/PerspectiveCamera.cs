using System;
using System.Drawing;
using eOculus.Core.Rendering;
using eOculus.Core.Utils;

namespace eOculus.Core.Cameras
{
    [Serializable]
    public class PerspectiveCamera : ICamera
    {
        private Point3D position;       //координаты камеры в системе с камерой
        private SizeF projectionWindow; //размер окна проектирования
        private double focus;           //расстояние от камеры до плоскости проектирования
        private Size resolution;        //кол-во пикселей по горизонтали и вертикали
        private Rotation rotation;      //преобразование, переводящее мировую сист. коорд в систему с камерой
        
        
        public PerspectiveCamera(Point3D position, Rotation rotation, SizeF projectionWindow, Size resolution, double focus)
        {
            this.position = position;
            this.projectionWindow = projectionWindow;
            this.focus = focus;
            this.resolution = resolution;
            this.rotation = rotation;
        }

        public Point3D Position
        {
            get { return position; }
            set { position = value; }
        }

        public SizeF ProjectionWindow
        {
            get { return projectionWindow; }
            set { projectionWindow = value; }
        }

        public double Focus
        {
            get { return focus; }
            set { focus = value; }
        }

        public Size Resolution
        {
            get { return resolution; }
            set { resolution = value; }
        }

        public Rotation Rotation
        {
            set { rotation = value; }
            get { return rotation;  }
        }

        /// <summary>
        /// вычисление луча (i, j) в мировой системе координат
        /// </summary>
        /// <param name="i">строка</param>
        /// <param name="j">столбец</param>
        /// <returns>луч (i, j)</returns>
        public Ray GetRay(int i, int j)
        {
            double Hx = projectionWindow.Width / resolution.Width;
            double Hy = projectionWindow.Height / resolution.Height;

            Point3D Pij = new Point3D(projectionWindow.Width / 2 - (i + 0.5) * Hx,
                                     -projectionWindow.Height / 2 + (j + 0.5) * Hy, 
                                     position.Z - focus
                                     );
            Vector3D v = rotation.Transform(new Vector3D(position, Pij));
            v *= (1 / v.Length);
            Point3D cameraInWorldCoord = rotation.Transform(position);
            return new Ray(cameraInWorldCoord, v);   
        }
    }
}
