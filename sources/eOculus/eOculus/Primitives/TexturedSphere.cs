using System;
using System.Drawing;
using eOculus.Core.GraphicsObjects;
using eOculus.Core.Rendering;
using eOculus.Core.Utils;

namespace eOculus.Primitives
{
    [Serializable]
    public class TexturedSphere : Sphere
    {
        private Bitmap texture;

        public TexturedSphere(Point3D origin, double radius, Material material, Bitmap texture) : base(origin, radius, material)
        {
            this.texture = texture;
        }

        public override IntersectionInfo Intersection(Ray r)
        {
            IntersectionInfo result =  base.Intersection(r);
            if (result == null)
            {
                return null;
            }

            Material material = result.Material;
            Point3D P = result.CrossPoint;

            Point3D E = new Point3D(P.X - Center.X, P.Y - Center.Y, P.Z - Center.Z);
            double psi, fi;
            psi = Math.Asin(E.Z / Radius);

            if (Math.Abs(psi) == Math.PI / 2)
                fi = 0;
            else
            {
                double cosfi = E.X / (Radius * Math.Cos(psi));
                if (cosfi > 1)
                    cosfi = 1;
                if (cosfi < -1)
                    cosfi = -1;
                fi = Math.Acos(cosfi);
                if (E.Y < 0)
                    fi = 2 * Math.PI - fi;
            }

            Material m;
            lock (texture)
            {
                int x = (int)((fi / (Math.PI * 2)) * (texture.Width - 1));
                int y = (int)((0.5 - psi / Math.PI) * (texture.Height - 1));
                m = new Material(texture.GetPixel(x, y), material.Diffusion, material.Specularity,
                                        material.Reflection, material.Transparency, material.FongCoeff);
            }

            material = m;
            return new IntersectionInfo(P, result.Normal, material);
        }
   }
}
