using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace System.Security {

    public class AES {

        public static AES Default { get; } = new();

        protected virtual byte[] SafeSalt(byte[]? Value) {
            var Size = Math.Max(8, Value?.Length ?? 0);
            var ret = new byte[Size];
            if (Value is { }) {
                Value.CopyTo(ret, 0);
            }

            return ret;
        }

        protected virtual string SafePassword(string? Value) {
            return Value.Coalesce();
        }

        protected string Password { get; }
        protected byte[] Salt { get; }

        public AES(string? Password = default, byte[]? Salt = default) {
            this.Password = SafePassword(Password);
            this.Salt = SafeSalt(Salt);
        }


        public string Encrypt(string plainText) {
            var Bytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            var EncryptedBytes = Encrypt(Bytes);
            var EncryptedString = Convert.ToBase64String(EncryptedBytes);
            var ret = EncryptedString;
            return ret;
        }

        public string Decrypt(string cipherText) {
            var EncryptedBytes = Convert.FromBase64String(cipherText);
            var DecryptedBytes = Decrypt(EncryptedBytes);
            var DecryptedString = System.Text.Encoding.UTF8.GetString(DecryptedBytes);
            var ret = DecryptedString;
            return ret;
        }
        public byte[] Encrypt(byte[] Value) {
            var NewSafeSalt = Salt;
            var NewSafePassword = Password;
            using var Key = new Rfc2898DeriveBytes(NewSafePassword, NewSafeSalt);

            using var aes = new RijndaelManaged();
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

        public byte[] Decrypt(byte[] Value) {
            var NewSafeSalt = Salt;
            var NewSafePassword = Password;
            using var Key = new Rfc2898DeriveBytes(NewSafePassword, NewSafeSalt);

            using var aes = new RijndaelManaged();
            using var ms = Value.ToMemoryStream();

            aes.Key = Key.GetBytes(aes.KeySize / 8);
            aes.IV = ReadByteArray(ms);

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var csDecrypt = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            var plaintext = csDecrypt.ReadAllBytes();

            return plaintext;
        }

        private static byte[] ReadByteArray(Stream s) {
            var rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length) {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            var buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length) {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }

    }

}

