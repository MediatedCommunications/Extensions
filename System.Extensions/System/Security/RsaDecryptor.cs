using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace System.Security {
    public class RsaDecryptor : DisplayClass, IDecryptor {
        private byte[] PrivateKey { get; }
        private RSAEncryptionPadding Padding { get; }

        public RsaDecryptor(byte[] PrivateKey, RSAEncryptionPadding? Padding = default) {
            this.PrivateKey = PrivateKey.ToArray();
            this.Padding = Padding ?? RsaDefaults.Padding;
        }

        public RsaDecryptor(string PrivateKey_Base64, RSAEncryptionPadding? Padding = default) {
            this.PrivateKey = Base64Encoding.ConvertFromStringFormatted(PrivateKey_Base64);
            this.Padding = Padding ?? RsaDefaults.Padding;
        }

        public string Decrypt(string cipherText) {
            var EncryptedBytes = Convert.FromBase64String(cipherText);
            var DecryptedBytes = Decrypt(EncryptedBytes);
            var DecryptedString = Encoding.UTF8.GetString(DecryptedBytes);
            var ret = DecryptedString;
            return ret;
        }

        public byte[] Decrypt(byte[] Value) {

            using var Decryptor = RSA.Create();
            Decryptor.ImportRSAPrivateKey(PrivateKey, out var PrivateKeyBytes);

            var ret = Decryptor.Decrypt(Value, Padding);

            return ret;
        }
    }

}

