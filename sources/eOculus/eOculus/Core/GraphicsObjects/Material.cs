using System;
using System.Drawing;

namespace eOculus.Core.GraphicsObjects
{
    /// <summary>
    /// определяет свойства предмета, такие как цвет, зеркальность, прозрачность и т д
    /// </summary>
    [Serializable]
    public class Material
    {
        private Color color;
        private double Kd;
        private double Ks; 
        private double Kr;
        private double Kt;
        private double pFong;
        private double refractiveIndex = 1;

        public Material(Color color, double Kd, double Ks, double Kr, double Kt, double pFong)
        {
            this.color = color;
            this.Kd = Kd;
            this.Ks = Ks;
            this.Kr = Kr;
            this.Kt = Kt;
            this.pFong = pFong;
        }

        public Color Color
        {
            get { return color; }
        }

        public double FongCoeff
        {
            get { return pFong; }
        }

        public double Diffusion
        {
            get { return Kd; }
        }

        public double Specularity
        {
            get { return Ks; }
        }

        public double Transparency
        {
            get { return Kt; }
        }

        public double Reflection
        {
            get { return Kr; }
        }

        public double RefractiveIndex
        {
            get { return refractiveIndex; }
            set { refractiveIndex = value; }
        }
   }
}
