using SixLabors.ImageSharp.PixelFormats;
using System.IO;

namespace SixLabors.ImageSharp.Extensions {
    public static class ImageSharpExtensions {

        public static Image<Rgba32> ToImageSharpImage(this System.Drawing.Bitmap This) {
            return This.ToImageSharpImage<Rgba32>();
        }

        public static Image<TPixel> ToImageSharpImage<TPixel>(this System.Drawing.Bitmap This) where TPixel : unmanaged, IPixel<TPixel> {
            
            using var memoryStream = new MemoryStream();
            
            This.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

            memoryStream.Seek(0, SeekOrigin.Begin);
            
            return Image.Load<TPixel>(memoryStream);
        }
    }
}
