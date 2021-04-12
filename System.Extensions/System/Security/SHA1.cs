using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace System.Security {
    public static class SHA1 {

        public static string FromFile(string filePath) {
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); 
            
            return From(stream);
        }

        public static string From(Stream Content) {
            using var sha1 = Cryptography.SHA1.Create();
            
            return Format(sha1.ComputeHash(Content));

        }

        public static string From(byte[] Content) {
            using var sha1 = Cryptography.SHA1.Create();
            
            return Format(sha1.ComputeHash(Content));
        }

        public static string From(string Content) {
            return From(Content, Encoding.Default);
        }

        public static string From(string Content, Encoding Encoding) {
            return From(Encoding.GetBytes(Content));
        }

        private static string Format(byte[] SHA1) {
            var ret = BitConverter.ToString(SHA1)
                .Replace("-", string.Empty)
                ;

            return ret;
        }

    }
}
