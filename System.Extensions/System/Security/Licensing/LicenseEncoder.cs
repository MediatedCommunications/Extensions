using System.Diagnostics;
using System.IO.Compression;
using System.Text;

namespace System.Security.Licensing {

    public class LicenseEncoder : LicenseEncoderBase
    {

        public LicenseEncoder()
        {
            this.Compressor = Compressor_Create();
            this.Encoding = Encoding_Create();
            this.Encryptor = Encryptor_Create();
            this.ByteEncoder = ByteEncoder_Create();
        }


        protected Compressor Compressor { get; }
        protected Encoding Encoding { get; }
        protected IEncryptor Encryptor { get; }
        protected IByteEncoder ByteEncoder { get; }

        protected virtual Compressor Compressor_Create()
        {
            return LicenseFormatDefaults.Compressor_Create();
        }

        protected virtual Encoding Encoding_Create()
        {
            return LicenseFormatDefaults.Encoding_Create();
        }

        protected virtual IEncryptor Encryptor_Create()
        {
            return LicenseFormatDefaults.Encryptor_Create();
        }
        
        protected virtual IByteEncoder ByteEncoder_Create() {
            return LicenseFormatDefaults.ByteEncoder_Create();
        }

        public override string Encode<T>(T License)
        {
            var DecryptedJson = Serialization_Add(License);
            var DecryptedBytes = Encoding_ToBytes(DecryptedJson);
            var CompressedBytes = Compression_Add(DecryptedBytes);
            var EncryptedBytes = Encryption_Add(CompressedBytes);
            var EncryptedString = Encoding_Add(EncryptedBytes);
            var ret = EncryptedString;

            return ret;

        }

        protected byte[] Encoding_ToBytes(string Content)
        {
            return Encoding.GetBytes(Content);
        }

        protected byte[] Encryption_Add(byte[] Input)
        {
            return Encryptor.Encrypt(Input);
        }

       
        protected byte[] Compression_Add(byte[] Input)
        {
            return Compressor.Compress(Input);
        }

        protected string Encoding_Add(byte[] Input)
        {
            return ByteEncoder.Encode(Input);
        }

        protected virtual string Serialization_Add<T>(T License)
        {
            return LicenseFormatDefaults.Serialization_Add(License);
        }

    }
    
}
