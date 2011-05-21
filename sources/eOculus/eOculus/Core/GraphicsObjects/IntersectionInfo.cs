using eOculus.Core.Utils;

namespace eOculus.Core.GraphicsObjects
{
    public class IntersectionInfo
    {
        private Point3D crossPoint; //точка пересечения
        private Vector3D N;         //внешняя нормаль в точке пересечения
        private Material material;  //материал в точке пересечения

        public IntersectionInfo(Point3D crossPoint, Vector3D N, Material material)
        {
            this.crossPoint = crossPoint;
            this.N = N;
            this.material = material;
        }

        public Point3D CrossPoint
        {
            get { return crossPoint; }
        }

        public Vector3D Normal
        {
            get { return N; }
        }

        public Material Material
        {
            get { return material; }
        }
    }
}
