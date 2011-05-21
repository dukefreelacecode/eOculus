using System;
using System.Drawing;
using eOculus.Core.Utils;

namespace eOculus.Core.Lights
{
    [Serializable]
    public class Light
    {
        private Point3D position;  //коорд. в мировой системе координат
        private Color color;

        public Light(Point3D position, Color color)
        {
            this.position = position;
            this.color = color;
        }

        public Point3D Position
        {
            get { return position; }
        }

        public Color Color
        {
            get { return color; }
        }
    }
}
