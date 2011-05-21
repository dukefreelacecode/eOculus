using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using eOculus.Core.Cameras;
using eOculus.Core.Filters;
using eOculus.Core.GraphicsObjects;
using eOculus.Core.Lights;
using eOculus.Core.Rendering;
using eOculus.Core.Scenes;
using eOculus.Core.Utils;
using eOculus.Primitives;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DrawingTests
    {
        private Size resolution = new Size(640, 480);
        private Renderer renderer = new Renderer();

        private Scene CreateScene1()
        {
            Point3D position = new Point3D(0, 0, 10000);
            double focus = 800;
            SizeF projectionWindow = new SizeF(160, 120);
            Rotation rotation = new Rotation(1, 0, -Math.PI / 3);
            PerspectiveCamera camera = new PerspectiveCamera(position, rotation, projectionWindow, resolution, focus);

            Scene scene = new Scene();
            scene.Camera = camera;

            Material mS = new Material(Color.Green, 0.5, 0.5, 0, 0.7, 5);
            Material mC1 = new Material(Color.Red, 0.5, 0.5, 0, 0.5, 5);
            Material mC2 = new Material(Color.Yellow, 0.5, 0.5, 0, 0.5, 5);
            Material mC3 = new Material(Color.Blue, 0.5, 0.5, 0, 0.5, 5);

            Circle C1 = new Circle(Point3D.Zero, Vector3D.OZ, 700, mC1);
            Circle C2 = new Circle(Point3D.Zero, Vector3D.OX, 700, mC2);
            Circle C3 = new Circle(Point3D.Zero, Vector3D.OY, 700, mC3);
            Sphere S = new Sphere(Point3D.Zero, 400, mS);

            scene.AddGraphicsObject(C1);
            scene.AddGraphicsObject(C2);
            scene.AddGraphicsObject(C3);
            scene.AddGraphicsObject(S);


            Light L1 = new Light(new Point3D(900, 0, 500), Color.White);
            Light L2 = new Light(new Point3D(500, 900, 500), Color.Wheat);
            Light L3 = new Light(new Point3D(0, 0, 900), Color.LightGoldenrodYellow);
            Light L4 = new Light(Point3D.Zero, Color.Red);
            scene.AddLight(L2);
            scene.AddLight(L3);

            return scene;
        }

        private Scene CreateScene2()
        {
            Point3D position = new Point3D(0, 0, 10000);
            double focus = 700;
            SizeF projectionWindow = new SizeF(160, 120);
            Rotation rotation = new Rotation(-Math.PI / 3, 0, -Math.PI / 3);
            PerspectiveCamera camera = new PerspectiveCamera(position, rotation, projectionWindow, resolution, focus);

            Scene scene = new Scene();
            scene.Camera = camera;
            scene.InfinityColor = Color.Black;

            Material mS = new Material(Color.Green, 0.5, 0.5, 0, 0.7, 5);
            Material mC = new Material(Color.Red, 0.5, 0.5, 0, 0, 5);

            Circle circle = new Circle(Point3D.Zero, Vector3D.OZ, 1000, mC);
            Sphere sphere = new Sphere(new Point3D(0, 0, 200), 200, mS);

            scene.AddGraphicsObject(circle);
            scene.AddGraphicsObject(sphere);


            Light L1 = new Light(new Point3D(2000, 2000, 2000), Color.White);
            //Light L2 = new Light(new Point3D(500, 900, 500), Color.Wheat);
            //Light L3 = new Light(new Point3D(0, 0, 900), Color.LightGoldenrodYellow);
            //Light L4 = new Light(Point3D.Zero, Color.Red);
            scene.AddLight(L1);

            return scene;
        }

        private Scene CreateScene3()
        {
            Point3D position = new Point3D(0, 0, 5000);
            double focus = 1000;
            SizeF projectionWindow = new SizeF(200, 150);
            Rotation rotation = new Rotation(-Math.PI * 0.25, 0, -Math.PI / 2.3);
            PerspectiveCamera camera = new PerspectiveCamera(position, rotation, projectionWindow, resolution, focus);

            Scene scene = new Scene();
            scene.Camera = camera;
            scene.InfinityColor = Color.Black;

            Material mS1 = new Material(Color.Green, 0.3, 0.3, 0.6, 0, 5);
            Material mS2 = new Material(Color.Red, 0.4, 0.4, 0, 0.4, 5);
            Material mS3 = new Material(Color.Yellow, 0.4, 0.3, 0.5, 0, 5);
            Material mS4 = new Material(Color.BlueViolet, 0.4, 0.3, 0.4, 0, 5);

            Material mC1 = new Material(Color.Black, 0.3, 0.3, 0.4, 0, 5);
            Material mC2 = new Material(Color.White, 0.3, 0.3, 0.4, 0, 5);

            Carpet carpet = new Carpet(new Point3D(0, 0, 0), new SizeF(5000, 5000), new Size(50, 50), mC1, mC2);
            double r = 100;
            double h = 2 * r * 0.866;
            double x = -h * 0.66 * 0.7;
            //шары в форме пирамиды
            Sphere sphere1 = new Sphere(new Point3D(x, x, r), r, mS1);
            Sphere sphere2 = new Sphere(new Point3D(x + h * 0.7 - r * 0.7, x + h * 0.7 + r * 0.7, r), r, mS2);
            Sphere sphere3 = new Sphere(new Point3D(x + h * 0.7 + r * 0.7, x + h * 0.7 - r * 0.7, r), r, mS3);
            Sphere sphere4 = new Sphere(new Point3D(x + h * 0.7 * 0.66, x + h * 0.7 * 0.66, 2.5 * r), r, mS4);

            scene.AddGraphicsObject(carpet);

            scene.AddGraphicsObject(sphere1);
            scene.AddGraphicsObject(sphere2);
            scene.AddGraphicsObject(sphere3);
            scene.AddGraphicsObject(sphere4);

            Light L1 = new Light(new Point3D(0, 0, 2000), Color.White);
            Light L2 = new Light(new Point3D(0, 2000, 3000), Color.White);
            Light L3 = new Light(new Point3D(2000, 0, 3000), Color.White);
            Light L4 = new Light(new Point3D(0, -2000, 3000), Color.White);
            Light L5 = new Light(new Point3D(-2000, 0, 3000), Color.White);
            //scene.AddLight(L1);
            scene.AddLight(L2);
            scene.AddLight(L3);
            scene.AddLight(L4);
            scene.AddLight(L5);

            return scene;
        }

        private Scene CreateScene4()
        {
            Point3D position = new Point3D(0, 0, 5000);
            double focus = 1000;
            SizeF projectionWindow = new SizeF(200, 150);
            Rotation rotation = new Rotation(-Math.PI * 0.25, 0, -Math.PI / 2.3);
            PerspectiveCamera camera = new PerspectiveCamera(position, rotation, projectionWindow, resolution, focus);

            Scene scene = new Scene();
            scene.Camera = camera;
            scene.InfinityColor = Color.Black;
            scene.Ambient = 0.3;

            Bitmap texture = Resources.EarthTexture;
            Material m = new Material(Color.White, 0.4, 0.4, 0, 0, 5);

            Sphere sphere = new TexturedSphere(Point3D.Zero, 200, m, texture);
            scene.AddGraphicsObject(sphere);

            Light L2 = new Light(new Point3D(0, 2000, 3000), Color.White);
            Light L3 = new Light(new Point3D(2000, 0, 3000), Color.White);

            scene.AddLight(L2);
            scene.AddLight(L3);

            return scene;
        }

        private void Save(Bitmap bmp, string filename)
        {
            string directory = "eOculusTests";
            Directory.CreateDirectory(directory);

            string path = directory + "/" + filename;
            FileStream file = new FileStream(path, FileMode.Create);

            bmp.Save(file, ImageFormat.Png);
            file.Close();

            Console.WriteLine(filename + " saved");
        }

        private void Save(Bitmap bmp)
        {
            string filename = DateTime.Now.Ticks.ToString() + ".png";
            Save(bmp, filename);
        }

        [Test]
        public void DrawScene1()
        {
            Save(CreateScene1().Draw(renderer, resolution));
        }

        [Test]
        public void DrawScene2()
        {
            Save(CreateScene2().Draw(renderer, resolution));
        }

        [Test]
        public void TestCarpet()
        {
            Save(CreateScene3().Draw(renderer, new Size(800, 600)));
        }

        [Test]
        public void TestTexturedSphere()
        {
            Save(CreateScene4().Draw(renderer, new Size(800, 600)));
        }

        [Test]
        public void TestFilter()
        {
            SmoothingFilter filter = new SmoothingFilter(FilterConfig.Config1);
            Bitmap bmp = CreateScene1().Draw(renderer, resolution);
            bmp = filter.Filter(bmp);
            Save(bmp);
        }

        [Test]
        public void Make100SlidesForVideo()
        {
            Size frameSize = new Size(800, 600);
            Scene s = CreateScene3();

            int count = 100;
            //анимация за счет смены положения камеры
            for (int i = 0; i < count; i++)
            {
                double aroundOZ = - (Math.PI / 2.0) * (((double)i) / count);
                Rotation r = new Rotation(aroundOZ, 0, -Math.PI / 2.3);
                s.Camera.Rotation = r;
                Save(s.Draw(renderer, frameSize));
            }
        }

        [Test]
        public void Make1000SlidesForVideo()
        {
            Size frameSize = new Size(800, 600);
            Func<int, string> CreateFileName = x => ("frame" + x.ToString("0000") + ".png");
            Action<Bitmap, string> SaveToFile= (bmp, filename) => {
                string directory = "E:\\ray-tracing\\frames001";
                //Directory.CreateDirectory(directory);

                string path = directory + "\\" + filename;
                FileStream file = new FileStream(path, FileMode.Create);

                bmp.Save(file, ImageFormat.Png);
                file.Close();

                Console.WriteLine(path + " saved");
            };


            Action[] actions = new Action[5];
            actions[0] = () =>
            {
                Scene s = CreateScene3();
                Renderer r = new Renderer();
                int count = 200;
                for (int i = 0; i < count; i++)
                {
                    double aroundOZ = -Math.PI / 4.0 + Math.PI * (((double)i) / count);
                    Rotation rot = new Rotation(aroundOZ, 0, -Math.PI / 2.3);
                    s.Camera.Rotation = rot;
                    SaveToFile(s.Draw(r, frameSize), CreateFileName(i));
                }
            };

            actions[1] = () =>
            {
                Scene s = CreateScene3();
                Renderer r = new Renderer();
                int count = 200;
                double dX = 2 * (Math.PI / 2.3);
                for (int i = 0; i < count; i++)
                {
                    double aroundOX = -Math.PI / 2.3 + dX * (((double)i) / count);
                    double aroundOY = Math.PI * (((double)i) / count);
                    Rotation rot = new Rotation(Math.PI * 0.75, aroundOY, aroundOX);
                    s.Camera.Rotation = rot;
                    SaveToFile(s.Draw(r, frameSize), CreateFileName(i + 200));
                }
            };

            actions[2] = () =>
            {
                Scene s = CreateScene3();
                Renderer r = new Renderer();
                int count = 400;
                for (int i = 0; i < count; i++)
                {
                    double aroundOZ = -Math.PI / 4.0 - 2 * Math.PI * (((double)i) / count);
                    Rotation rot = new Rotation(aroundOZ, 0, -Math.PI / 2.3);
                    s.Camera.Rotation = rot;
                    SaveToFile(s.Draw(r, frameSize), CreateFileName(i + 400));
                }
            };

            actions[3] = () =>
            {
                Scene s = CreateScene3();
                Renderer r = new Renderer();
                int count = 200;
                double dX = 2 * (Math.PI / 2.3);
                for (int i = 0; i < count; i++)
                {
                    double aroundOX = -Math.PI / 2.3 + dX * (((double)i) / count);
                    double aroundOY = Math.PI * (((double)i) / count);
                    Rotation rot = new Rotation(-Math.PI / 4.0, aroundOY, aroundOX);
                    s.Camera.Rotation = rot;
                    SaveToFile(s.Draw(r, frameSize), CreateFileName(i + 800));
                }
            };

            actions[4] = () =>
            {
                Scene s = CreateScene3();
                Renderer r = new Renderer();
                int count = 200;
                for (int i = 0; i < count; i++)
                {
                    double aroundOZ = Math.PI * 0.75 + Math.PI * (((double)i) / count);
                    Rotation rot = new Rotation(aroundOZ, 0, -Math.PI / 2.3);
                    s.Camera.Rotation = rot;
                    SaveToFile(s.Draw(r, frameSize), CreateFileName(i + 1000));
                }
            };


            Task[] tasks = new Task[actions.Length];

            for (int i = 0; i < actions.Length; i++)
            {
                tasks[i] = new Task(actions[i]);
                tasks[i].Start();
            }

            for (int i = 0; i < actions.Length; i++)
            {
                tasks[i].Wait();
            }
        }
    }
}
