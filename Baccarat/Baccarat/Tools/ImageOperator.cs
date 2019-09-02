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
        public void GetImageRgb(Image image, Int32 x, Int32 y, out Int32 r, out Int32 g, out Int32 b)
        {
            Bitmap bitmap = new Bitmap(image);
            Color color = bitmap.GetPixel(x, y);
            r = color.R;
            g = color.G;
            b = color.B;
        }
    }
}
