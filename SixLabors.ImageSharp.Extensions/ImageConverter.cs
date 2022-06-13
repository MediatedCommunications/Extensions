using System.IO;

namespace SixLabors.ImageSharp {
    public abstract class ImageConverter {

        public abstract void SaveAsPng(Image bitmap, MemoryStream MS);

        public byte[] SaveAsPng(Image bitmap) {
            using var ms = new MemoryStream();
            
            SaveAsPng(bitmap, ms);

            ms.Position = 0;
            var Bytes = ms.GetBuffer();
            var B2 = Bytes[0..(int)(ms.Length)];

            var ret = B2;

            return ret;

        }

    }

}
