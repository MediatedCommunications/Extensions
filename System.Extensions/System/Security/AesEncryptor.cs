using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace System.Security
{

    public record AesEncryptor : Encryptor {

        public static AesEncryptor Default { get; } = new();

        protected virtual byte[] SafeSalt(IEnumerable<byte>? Value) {
            var VV = Value.Coalesce().ToArray();
            
            var Size = Math.Max(8, VV.Length);
            var ret = new byte[Size];

            VV.CopyTo(ret, 0);
            

            return ret;
        }

        protected virtual string SafePassword(string? Value) {
            return Value.Coalesce();
        }

        protected string Password { get; }
        protected byte[] Salt { get; }

        public AesEncryptor(string? Password = default, IEnumerable<byte>? Salt = default) {
            this.Password = SafePassword(Password);
            this.Salt = SafeSalt(Salt);
        }


        public override string Encrypt(string plainText) {
            var Bytes = Encoding.UTF8.GetBytes(plainText);
            var EncryptedBytes = Encrypt(Bytes);
            var EncryptedString = Convert.ToBase64String(EncryptedBytes);
            var ret = EncryptedString;
            return ret;
        }

        public override string Decrypt(string cipherText) {
            var EncryptedBytes = Convert.FromBase64String(cipherText);
            var DecryptedBytes = Decrypt(EncryptedBytes);
            var DecryptedString = Encoding.UTF8.GetString(DecryptedBytes);
            var ret = DecryptedString;
            return ret;
        }
        public override byte[] Encrypt(byte[] Value) {
            var NewSafeSalt = Salt;
            var NewSafePassword = Password;
            using var Key = new Rfc2898DeriveBytes(NewSafePassword, NewSafeSalt);

            using var aes = Aes.Create();
            using var ms = new MemoryStream();

            aes.Key = Key.GetBytes(aes.KeySize / 8);
            
            ms.Write(BitConverter.GetBytes(aes.IV.Length), 0, sizeof(int));
            ms.Write(aes.IV, 0, aes.IV.Length);

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var writer = new CryptoStream(ms, encryptor, CryptoStreamMode.Write)) {
                writer.Write(Value);
            }
            var ret = ms.ToArray();

            return ret;
        }

        public override byte[] Decrypt(byte[] Value) {
            var NewSafeSalt = Salt;
            var NewSafePassword = Password;

            using var Key = new Rfc2898DeriveBytes(NewSafePassword, NewSafeSalt);

            using var aes = Aes.Create();
            using var ms = Value.ToMemoryStream();

            aes.Key = Key.GetBytes(aes.KeySize / 8);
            aes.IV = ReadByteArray(ms);

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var csDecrypt = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            var plaintext = csDecrypt.ReadAllBytes();

            return plaintext;
        }

        private static byte[] ReadByteArray(Stream s) {
            byte[] ret;

            var rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length) {
                throw new FormatException("Stream did not contain properly formatted byte array");
            }

            var BufferSize = BitConverter.ToInt32(rawLength, 0);
            var MinSize = 0;
            var MaxSize = Array.MaxLength;

            var buffer = CreateBuffer(BufferSize, MinSize, MaxSize);

            if (s.Read(buffer, 0, buffer.Length) == buffer.Length) {
                ret = buffer;
            } else {
                throw new System.IO.IOException("Did not properly read byte array");
            }
            
            return ret;
        }

        private static byte[] CreateBuffer(int Size, int MinSize, int MaxSize) {
            if (Size < MinSize || Size > MaxSize) {
                throw new ArgumentOutOfRangeException(nameof(Size), Size, $@"Must be between {MinSize} and {MaxSize}");
            }

            var ret = new byte[Size];

            return ret;
        }

    }

}

