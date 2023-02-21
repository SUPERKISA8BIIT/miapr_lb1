using System.Windows;
using System.Windows.Media.Imaging;

namespace miapr_lb1
{
    public static class Staticupperlevel
    {
        public static void AddPixel(this WriteableBitmap wb, Pixelcolor color, int x, int y)
        {
            Int32Rect rect = new Int32Rect(x, y, 1, 1);
            int stride = (wb.PixelWidth * wb.Format.BitsPerPixel) / 8;
            wb.WritePixels(rect, color.Core, stride, 0);
        }

        public static void AddRect(this WriteableBitmap wb, Pixelcolor color, int x, int y, int size)
        {
            int beg_x = x - size < 0 ? 0 : x - size;
            int beg_y = y - size < 0 ? 0 : y - size;
            int end_x = x + size > wb.PixelWidth ? wb.PixelWidth : x + size;
            int end_y = y + size > wb.PixelHeight ? wb.PixelHeight : y + size;

            for (int i = beg_x; i < end_x; i++)
                for (int j = beg_y; j < end_y; j++)
                    wb.AddPixel(color, i, j);
        }
    }
}
