using System.Drawing;

namespace InwersjaTomograficzna.Core.Extensions
{
    public static class ImageExtesions
    {
        public static Image ResizeImage(this Image image, int width, int height)
        {
            return (new Bitmap(image, new Size(width, height)));
        }
    }
}
