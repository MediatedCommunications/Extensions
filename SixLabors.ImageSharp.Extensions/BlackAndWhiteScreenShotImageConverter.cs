using System.IO;

namespace SixLabors.ImageSharp.Extensions {
    public class BlackAndWhiteScreenShotImageConverter : ImageConverter {
        public override void SaveAsPng(Image bitmap, MemoryStream MS) {
            
            bitmap.SaveAsPng(MS, new Formats.Png.PngEncoder() {
                BitDepth = Formats.Png.PngBitDepth.Bit2,
                CompressionLevel = Formats.Png.PngCompressionLevel.BestCompression,
                ColorType = Formats.Png.PngColorType.Grayscale,
                IgnoreMetadata = true,
                FilterMethod = Formats.Png.PngFilterMethod.Adaptive,
                InterlaceMethod = Formats.Png.PngInterlaceMode.None,
            });
        }

    }

}
