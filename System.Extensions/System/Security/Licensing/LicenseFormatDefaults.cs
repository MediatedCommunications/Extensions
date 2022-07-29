using System.IO.Compression;
using System.Text;
using System.Text.Json.Serialization;

namespace System.Security.Licensing {
    internal static class LicenseFormatDefaults {

        static System.Text.Json.JsonSerializerOptions Options { get; }

        static LicenseFormatDefaults() {
            Options = new System.Text.Json.JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                WriteIndented = false,
                //Converters = { new JsonStringEnumConverter() }
            };
        }

        public static string Serialization_Add<T>(T Value) {
            return System.Text.Json.JsonSerializer.Serialize(Value, Options);
        }

        public static T? Serialization_Remove<T>(string Value) {
            return System.Text.Json.JsonSerializer.Deserialize<T>(Value, Options);
        }

        public static IDecryptor Decryptor_Create() {
            return AesEncryptor.Default;
        }

        public static IEncryptor Encryptor_Create() {
            return AesEncryptor.Default;
        }

        public static Encoding Decoding_Create() {
            return Encoding.UTF8;
        }

        public static Encoding Encoding_Create() {
            return Encoding.UTF8;
        }

        public static Compressor Compressor_Create() {
            return Compressors.ZLib;
        }

        public static Compressor Decompressor_Create() {
            return Compressors.ZLib;
        }

        public static IByteEncoder ByteEncoder_Create() {
            return ByteEncoders.Base64_Formatted;
        }

        public static IByteDecoder ByteDecoder_Create() {
            return ByteEncoders.Base64_Formatted;
        }

        public static byte[] Encoding_Remove(string Input) {
            var ret = Base64Encoding.ConvertFromStringFormatted(Input);
            return ret;
        }

        public static string Encoding_Add(byte[] Input) {
            var ret = Base64Encoding.ConvertToStringFormatted(Input);
            return ret;
        }

    }
}
