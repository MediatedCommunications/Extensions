using System.IO.Compression;
using System.Text;

namespace System.Security.Licensing {
    public class LicenseDecoder : LicenseDecoderBase {
        public LicenseDecoder() {
            this.Decompressor = Decompressor_Create();
            this.Decoding = Decoding_Create();
            this.Decryptor = Decryptor_Create();
        }


        protected Compressor Decompressor { get; }
        protected Encoding Decoding { get; }
        protected IDecryptor Decryptor { get; }

        protected virtual Compressor Decompressor_Create() {
            return Compressors.ZLib;
        }

        protected virtual Encoding Decoding_Create() {
            return Encoding.UTF8;
        }

        protected virtual IDecryptor Decryptor_Create() {
            return AesEncryptor.Default;
        }

        public override T? Decode<T>(string LicenseText)
            where T : class {
            var EncryptedString = LicenseText;
            var EncryptedBytes = Encoding_Remove(EncryptedString);
            var DecryptedBytes = Encryption_Remove(EncryptedBytes);
            var DecompressedBytes = Compression_Remove(DecryptedBytes);
            var DecryptedJson = Encoding_ToString(DecompressedBytes);
            var License = Serialization_Remove<T>(DecryptedJson);

            var ret = License;

            return ret;
        }

        protected string Encoding_ToString(byte[] Content) {
            return Decoding.GetString(Content);
        }

        protected byte[] Encryption_Remove(byte[] Input) {
            return Decryptor.Decrypt(Input);
        }

        protected byte[] Compression_Remove(byte[] Input) {
            return Decompressor.Decompress(Input);
        }

        protected virtual byte[] Encoding_Remove(string Input) {
            var ret = Base64Encoding.ConvertFromStringFormatted(Input);
            return ret;
        }

        protected virtual T? Serialization_Remove<T>(string Value) {
            return System.Text.Json.JsonSerializer.Deserialize<T>(Value);
        }
    }
    
}
