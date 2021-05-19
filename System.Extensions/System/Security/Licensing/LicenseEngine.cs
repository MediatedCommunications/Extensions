using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;

namespace System.Security.Licensing {
    public class LicenseEngine<T> 
        where T : DisplayRecord
        {

        public ImmutableList<T> Licenses { get; private set; } = ImmutableList<T>.Empty;

        public T Load(string Key) {
            var Data = Preview(Key);

            Licenses = Licenses.Add(Data);

            return Data;
        }

        public void Unload(T License) {
            Licenses = Licenses.Remove(License);
        }


        private T Preview(string License) {
            try {
                var EncryptedString = License;
                var EncryptedBytes = Decode(EncryptedString);
                var DecryptedBytes = Decrypt(EncryptedBytes);
                var DecryptedJson = System.Text.Encoding.UTF8.GetString(DecryptedBytes);
                var ret = System.Text.Json.JsonSerializer.Deserialize<T>(DecryptedJson);

                if(ret == default) {
                    throw new InvalidLicenseException();
                }

                return ret;
            } catch {
                throw new InvalidLicenseException();
            }
        }

        protected virtual byte[] Encrypt(byte[] Input) {
            return AES.Default.Encrypt(Input);
        }

        protected virtual byte[] Decrypt(byte[] Input) {
            return AES.Default.Decrypt(Input);
        }

        protected virtual string Encode(byte[] Input) {
            var ret = Base64Encoding.ConvertToStringFormatted(Input);
            return ret;
        }

        protected virtual byte[] Decode(string Input) {
            var ret = Base64Encoding.ConvertFromStringFormatted(Input);
            return ret;
        }


        internal string Create(T License) {
            var DecryptedJson = System.Text.Json.JsonSerializer.Serialize(License);
            var DecryptedBytes = System.Text.Encoding.UTF8.GetBytes(DecryptedJson);
            var EncryptedBytes = Encrypt(DecryptedBytes);
            var EncryptedString = Encode(EncryptedBytes);
            var ret = EncryptedString;

            return ret;

        }

    }

}
