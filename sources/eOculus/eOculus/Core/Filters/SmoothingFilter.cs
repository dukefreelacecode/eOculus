using System.Drawing;

namespace eOculus.Core.Filters
{
    public class SmoothingFilter : IFilter
    {
        private FilterConfig config;

        public SmoothingFilter(FilterConfig config)
        {
            this.config = config;
        }

        public Bitmap Filter(Bitmap bmp)
        {
            Bitmap result = new Bitmap(bmp.Width, bmp.Height);

            for (int i = 1; i < bmp.Width - 1; i++)
            {
                for (int j = 1; j < bmp.Height - 1; j++)
                {
                    int r, g, b;
                    r = g = b = 0;

                    for (int p = 0; p < 3; p++)
                    {
                        for (int q = 0; q < 3; q++)
                        {
                            r += config.Mask[p, q] * bmp.GetPixel(i + (p - 1), j + (q - 1)).R;
                            g += config.Mask[p, q] * bmp.GetPixel(i + (p - 1), j + (q - 1)).G;
                            b += config.Mask[p, q] * bmp.GetPixel(i + (p - 1), j + (q - 1)).B;
                        }
                    }

                    r /= config.K;
                    g /= config.K;
                    b /= config.K;

                    result.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }

            return result;
        }
    }
}
