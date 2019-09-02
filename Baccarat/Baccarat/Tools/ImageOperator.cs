using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baccarat.Tools
{
    class ImageOperator
    {
        public static Color GetImageRgb(Image image, Int32 x, Int32 y)
        {
            Bitmap bitmap = new Bitmap(image);
            Color color = bitmap.GetPixel(x, y);
            bitmap.Dispose();
            return color;
        }
    }
}
