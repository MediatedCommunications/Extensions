using SixLabors.ImageSharp.PixelFormats;
using System.Collections.Immutable;
using System.IO;

namespace SixLabors.ImageSharp {
    public static class ImageSharpExtensions {

        public static ImmutableArray<Image<Rgba32>> ToImageSharpImage(this IEnumerable<System.Drawing.Bitmap> This, bool Dispose = true) {
            var ret = This.Select(x => x.ToImageSharpImage(Dispose)).ToImmutableArray();

            return ret;
        }

        public static ImmutableArray<Image<TPixel>> ToImageSharpImage<TPixel>(this IEnumerable<System.Drawing.Bitmap> This, bool Dispose = true) where TPixel : unmanaged, IPixel<TPixel> {
            var ret = This.Select(x => x.ToImageSharpImage<TPixel>(Dispose)).ToImmutableArray();

            return ret;
        }



        public static Image<Rgba32> ToImageSharpImage(this System.Drawing.Bitmap This, bool Dispose = true) {
            return This.ToImageSharpImage<Rgba32>(Dispose);
        }

        public static Image<TPixel> ToImageSharpImage<TPixel>(this System.Drawing.Bitmap This, bool Dispose = true) where TPixel : unmanaged, IPixel<TPixel> {
            
            using var memoryStream = new MemoryStream();
            
            This.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

            memoryStream.Seek(0, SeekOrigin.Begin);
            
            var ret = Image.Load<TPixel>(memoryStream);

            if (Dispose) {
                This.Dispose();
            }

            return ret;
        }
    }
}
