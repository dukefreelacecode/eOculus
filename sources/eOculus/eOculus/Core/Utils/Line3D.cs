namespace eOculus.Core.Utils
{
    public class Line3D
    {
        private Point3D pointOnLine;
        private Vector3D direction;

        public Line3D(Point3D pointOnLine, Vector3D direction)
        {
            this.pointOnLine = pointOnLine;
            this.direction = direction;
        }

        public Vector3D Direction
        {
            get { return direction; }
        }

        public Point3D Point
        {
            get { return pointOnLine; }
        }
    }
}
