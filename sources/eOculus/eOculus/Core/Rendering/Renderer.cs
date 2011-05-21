using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using eOculus.Core.Scenes;
using eOculus.Core.GraphicsObjects;
using eOculus.Core.Utils;
using eOculus.Core.Lights;

namespace eOculus.Core.Rendering
{
    public class Renderer : IRenderer
    {
        private int MAX_DEPTH = 5;
        private IScene scene;
        private Size size;

        public Bitmap Render(IScene scene, Size size)
        {
            this.scene = scene;
            this.size = size;

            Bitmap bmp = new Bitmap(size.Width, size.Height);
            scene.Camera.Resolution = size;

            Trace(bmp);

            return bmp;
        }

        private void Trace(Bitmap bmp)
        {
            Color color;

            for (int i = 0; i < size.Width; i++)
            {
                for (int j = 0; j < size.Height; j++)
                {
                    color = TraceRay(scene.Camera.GetRay(i, j), 0);
                    bmp.SetPixel(i, j, color);
                }
            }
        }

        private Color TraceRay(Ray r, int depth)
        {
            if (depth > MAX_DEPTH)
                return Color.Black;

            IntersectionInfo info = GetFirstIntersection(r);
            if (info == null)   //луч не пересекает предмет
                return scene.InfinityColor;

            double R = 0, G = 0, B = 0;
            //луч пересекает предмет

            double IdR = 0, IdG = 0, IdB = 0;
            double IsR = 0, IsG = 0, IsB = 0;
            double pFong = info.Material.FongCoeff;   //

            IEnumerator<Light> lights = scene.Lights;
            lights.Reset();

            while (lights.MoveNext())
            {
                Light L = lights.Current;
                Vector3D V = new Vector3D(info.CrossPoint, L.Position); //направление на источник L
                V *= (1 / V.Length);

                if (IsVisible(L, info.CrossPoint))
                {
                    double distance = VectorOperations.Distance(L.Position, info.CrossPoint);
                    double attenuation = 1;// L.Attenuation(distance);

                    if (info.Material.Diffusion > 0)    //для предмета задано свойство диффузного отражения 
                    {
                        double cos = VectorOperations.Cos(info.Normal, V);
                        if (cos < 0)
                            cos = 0;

                        IdR += L.Color.R * cos / attenuation;
                        IdG += L.Color.G * cos / attenuation;
                        IdB += L.Color.B * cos / attenuation;
                    }

                    if (info.Material.Specularity > 0)  //для предмета задано свойство зеркального отражения 
                    {
                        Vector3D reflected;
                        if (VectorOperations.ReflectedVector(info.Normal, r.Direction, out reflected))
                        {
                            double cos = VectorOperations.Cos(V, reflected);
                            if (cos < 0)
                                cos = 0;

                            IsR += L.Color.R * Math.Pow(cos, pFong) / attenuation;
                            IsG += L.Color.G * Math.Pow(cos, pFong) / attenuation;
                            IsB += L.Color.B * Math.Pow(cos, pFong) / attenuation;
                        }
                    }
                }
            }

            R += info.Material.Diffusion * IdR * info.Material.Color.R / 255;
            G += info.Material.Diffusion * IdG * info.Material.Color.G / 255;
            B += info.Material.Diffusion * IdB * info.Material.Color.B / 255;

            R += info.Material.Specularity * IsR;
            G += info.Material.Specularity * IsG;
            B += info.Material.Specularity * IsB;

            if (info.Material.Reflection > 0)
            {
                Vector3D reflected;
                if (VectorOperations.ReflectedVector(info.Normal, r.Direction, out reflected))
                {
                    Ray reflectedRay = new Ray(info.CrossPoint, reflected);
                    Color reflectedColor = TraceRay(reflectedRay, depth + 1);

                    R += info.Material.Reflection * reflectedColor.R;
                    G += info.Material.Reflection * reflectedColor.G;
                    B += info.Material.Reflection * reflectedColor.B;
                }
            }

            if (info.Material.Transparency > 0)
            {
                double n12 = info.Material.RefractiveIndex;
                Vector3D refracted;

                if (VectorOperations.RefractedVector(info.Normal, r.Direction, n12, out refracted))
                {
                    Ray refractedRay = new Ray(info.CrossPoint, refracted);
                    Color refractedColor = TraceRay(refractedRay, depth + 1);

                    R += info.Material.Transparency * refractedColor.R;
                    G += info.Material.Transparency * refractedColor.G;
                    B += info.Material.Transparency * refractedColor.B;
                }
            }

            R += info.Material.Color.R * scene.Ambient;
            G += info.Material.Color.G * scene.Ambient;
            B += info.Material.Color.B * scene.Ambient;

            return Color.FromArgb(Math.Min((int)R, 255), Math.Min((int)G, 255), Math.Min((int)B, 255));
        }

        private IntersectionInfo GetFirstIntersection(Ray r)
        {
            IntersectionInfo result = null;
            double distance = Double.MaxValue;

            IEnumerator<IGraphicsObject3D> graphicsObjects = scene.GraphicsObjects;
            graphicsObjects.Reset();

            while (graphicsObjects.MoveNext())
            {
                IGraphicsObject3D obj = graphicsObjects.Current;
                IntersectionInfo info = obj.Intersection(r);
                if (info != null)
                {
                    if (VectorOperations.Distance(info.CrossPoint, r.Origin) < distance)
                    {
                        distance = VectorOperations.Distance(info.CrossPoint, r.Origin);
                        result = info;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// проверяет, видим ли источник света L из данной точки P
        /// </summary>
        /// <param name="L"></param>
        /// <param name="P"></param>
        /// <returns>
        /// true - источник L виден из точки P
        /// false - источник L не виден из точки P
        /// </returns>
        private bool IsVisible(Light L, Point3D P)
        {
            Vector3D v = new Vector3D(P, L.Position);
            //для упрощения: если источник закрыт прозрачным предметом, то считаем, что он в тени
            IntersectionInfo info = this.GetFirstIntersection(new Ray(P, v));
            if (info == null)
            {
                return true;
            }
            else
            {
                return (VectorOperations.Distance(P, L.Position) < VectorOperations.Distance(P, info.CrossPoint));
            }
        }
    }
}
