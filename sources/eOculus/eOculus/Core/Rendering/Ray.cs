using eOculus.Core.Utils;

namespace eOculus.Core.Rendering
{
    public class Ray
    {
        private Point3D origin;
        private Vector3D direction;
        private RayType type = RayType.Primary;

        public Ray(Point3D origin, Vector3D direction)
        {
            this.origin = origin;
            this.direction = direction;
        }

        public Point3D Origin
        {
            get { return origin; }
        }

        public Vector3D Direction
        {
            get { return direction; }
        }

        public RayType Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
