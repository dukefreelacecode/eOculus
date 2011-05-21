using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using eOculus.Core.Cameras;
using eOculus.Core.GraphicsObjects;
using eOculus.Core.Lights;
using eOculus.Core.Utils;
using eOculus.Core.Rendering;

namespace eOculus.Core.Scenes
{
    [Serializable]
    public class Scene : IScene
    {
        private readonly List<IGraphicsObject3D> graphicObjects = new List<IGraphicsObject3D>();
        private readonly List<Light> lights = new List<Light>();
        private ICamera camera;
        private Color defaultColor = Color.LightYellow;
        private double ambient = 0.2;    //фоновая составляющая

        public Scene()
        {
            this.camera = new PerspectiveCamera(new Point3D(0, 0, 1000), new Rotation(0, 0, 0), new SizeF(100, 100), new Size(640, 480), 500);
        }

        public Color InfinityColor
        {
            get { return defaultColor; }
            set { defaultColor = value; }
        }

        public ICamera Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        public double Ambient
        {
            get { return ambient; }
            set 
            {
                if (value > 0 && value < 1)
                    ambient = value;
                else
                    ambient = 0.2;
            }
        }

        public void AddGraphicsObject(IGraphicsObject3D obj)
        {
            graphicObjects.Add(obj);
        }

        public void AddLight(Light L)
        {
            lights.Add(L);
        }

        public IEnumerator<IGraphicsObject3D> GraphicsObjects
        {
            get { return graphicObjects.GetEnumerator(); }
        }

        public IEnumerator<Light> Lights
        {
            get { return lights.GetEnumerator(); }
        }

        public Bitmap Draw(IRenderer renderer, Size size)
        {
            return renderer.Render(this, size);
        }
    }
}
