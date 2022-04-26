using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace System.Security {
    public class RsaEncryptor : DisplayClass, IEncryptor {
        private byte[] PublicKey { get; }
        private RSAEncryptionPadding Padding { get; }

        public RsaEncryptor(byte[] PublicKey, RSAEncryptionPadding? Padding = default) {
            this.PublicKey = PublicKey.ToArray();
            this.Padding = Padding ?? RsaDefaults.Padding;
        }

        public RsaEncryptor(string PublicKey_Base64, RSAEncryptionPadding? Padding = default) {
            this.PublicKey = Base64Encoding.ConvertFromStringFormatted(PublicKey_Base64);
            this.Padding = Padding ?? RsaDefaults.Padding;
        }

        public string Encrypt(string plainText) {
            var Bytes = Encoding.UTF8.GetBytes(plainText);
            var EncryptedBytes = Encrypt(Bytes);
            var EncryptedString = Convert.ToBase64String(EncryptedBytes);
            var ret = EncryptedString;
            return ret;
        }

        public byte[] Encrypt(byte[] Value) {
            using var Encryptor = RSA.Create();
            Encryptor.ImportRSAPublicKey(PublicKey, out var PublicKeyBytes);

            var ret = Encryptor.Encrypt(Value, Padding);

            return ret;
        }

    }

}

