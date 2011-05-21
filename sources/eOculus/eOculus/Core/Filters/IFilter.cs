using System.Drawing;

namespace eOculus.Core.Filters
{
    public interface IFilter
    {
        Bitmap Filter(Bitmap bmp);
    }
}
