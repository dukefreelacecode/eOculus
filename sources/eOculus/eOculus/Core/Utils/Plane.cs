namespace eOculus.Core.Utils
{
    public class Plane
    {
        private Point3D pointOnPlane;
        private Vector3D normal;

        public Plane(Point3D pointOnPlane, Vector3D normal)
        {
            this.pointOnPlane = pointOnPlane;
            this.normal = normal;
        }

        public Vector3D Normal
        {
            get { return normal; }
        }

        public Point3D Point
        {
            get { return pointOnPlane; }
        }
    }
}
