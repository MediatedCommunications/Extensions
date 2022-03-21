using System.Diagnostics;
using System.IO.Compression;
using System.Text;

namespace System.Security.Licensing
{
    
    public class LicenseEncoder : LicenseEncoderBase
    {

        public LicenseEncoder()
        {
            this.Compressor = Compressor_Create();
            this.Encoding = Encoding_Create();
            this.Encryptor = Encryptor_Create();
        }


        protected Compressor Compressor { get; }
        protected Encoding Encoding { get; }
        protected Encryptor Encryptor { get; }

        protected virtual Compressor Compressor_Create()
        {
            return Compressors.ZLib;
        }

        protected virtual Encoding Encoding_Create()
        {
            return Encoding.UTF8;
        }

        protected virtual Encryptor Encryptor_Create()
        {
            return AesEncryptor.Default;
        }

        public override string Create<T>(T License)
        {
            var DecryptedJson = Serialization_Add(License);
            var DecryptedBytes = Encoding_ToBytes(DecryptedJson);
            var CompressedBytes = Compression_Add(DecryptedBytes);
            var EncryptedBytes = Encryption_Add(CompressedBytes);
            var EncryptedString = Encoding_Add(EncryptedBytes);
            var ret = EncryptedString;

            return ret;

        }

        public override T? Parse<T>(string LicenseText)
            where T : class
        {
            var EncryptedString = LicenseText;
            var EncryptedBytes = Encoding_Remove(EncryptedString);
            var DecryptedBytes = Encryption_Remove(EncryptedBytes);
            var DecompressedBytes = Compression_Remove(DecryptedBytes);
            var DecryptedJson = Encoding_ToString(DecompressedBytes);
            var License = Serialization_Remove<T>(DecryptedJson);

            var ret = License;

            return ret;
        }

        protected byte[] Encoding_ToBytes(string Content)
        {
            return Encoding.GetBytes(Content);
        }

        protected string Encoding_ToString(byte[] Content)
        {
            return Encoding.GetString(Content);
        }

        protected byte[] Encryption_Add(byte[] Input)
        {
            return Encryptor.Encrypt(Input);
        }

        protected byte[] Encryption_Remove(byte[] Input)
        {
            return Encryptor.Decrypt(Input);
        }

        protected byte[] Compression_Add(byte[] Input)
        {
            return Compressor.Compress(Input);
        }

        protected byte[] Compression_Remove(byte[] Input)
        {
            return Compressor.Decompress(Input);
        }

        protected virtual string Encoding_Add(byte[] Input)
        {
            var ret = Base64Encoding.ConvertToStringFormatted(Input);
            return ret;
        }

        protected virtual byte[] Encoding_Remove(string Input)
        {
            var ret = Base64Encoding.ConvertFromStringFormatted(Input);
            return ret;
        }


        protected virtual string Serialization_Add<T>(T License)
        {
            return System.Text.Json.JsonSerializer.Serialize(License);
        }

        protected virtual T? Serialization_Remove<T>(string Value)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(Value);
        }

    }
    
}
