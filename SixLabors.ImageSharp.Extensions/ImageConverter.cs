using System.IO;

namespace SixLabors.ImageSharp.Extensions {
    public abstract class ImageConverter {

        public abstract void SaveAsPng(Image bitmap, MemoryStream MS);

        public byte[] SaveAsPng(Image bitmap) {
            var MS = new MemoryStream();
            
            SaveAsPng(bitmap, MS);

            MS.Position = 0;
            var Bytes = MS.GetBuffer();
            var B2 = Bytes[0..(int)(MS.Length)];

            var ret = B2;

            return ret;

        }

    }

}
