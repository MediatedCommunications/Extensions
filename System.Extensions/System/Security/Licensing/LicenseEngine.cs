using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace System.Security.Licensing {
    public class LicenseEngine<T> 
        where T : DisplayRecord
        {

        public ImmutableList<T> Licenses { get; private set; } = ImmutableList<T>.Empty;

        private void AddLicense(T? License) {
            if(License is { }) {
                Licenses = Licenses.Add(License);
            }
        }

        private void RemoveLicense(T? License) {
            if (License is { }) {
                Licenses = Licenses.Remove(License);
            }
        }


        public T Load(string Key) {
            var ret = Preview(Key);

            AddLicense(ret);

            return ret;
        }

        public bool TryLoad(string LicenseText, [NotNullWhen(true)] out T? License) {
            var ret = TryPreview(LicenseText, out License);

            AddLicense(License);

            return ret;
        }


        public void Unload(T? License) {
            RemoveLicense(License);
        }

        
        public bool TryPreview(string LicenseText, [NotNullWhen(true)] out T? License) {
            var ret = false;
            License = default(T?);

            try {
                var EncryptedString = LicenseText;
                var EncryptedBytes = Decode(EncryptedString);
                var DecryptedBytes = Decrypt(EncryptedBytes);
                var DecryptedJson = Encoding.UTF8.GetString(DecryptedBytes);
                License = Text.Json.JsonSerializer.Deserialize<T>(DecryptedJson);
            } catch(Exception ex) {
                ex.Ignore();
            }

            if(License is { }) {
                ret = true;
            }


            return ret;
        }

        public T Preview(string LicenseText) {
            if (TryPreview(LicenseText, out var ret)) {
                return ret;
            } else {
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


        protected string Create(T License) {
            var DecryptedJson = Text.Json.JsonSerializer.Serialize(License);
            var DecryptedBytes = Encoding.UTF8.GetBytes(DecryptedJson);
            var EncryptedBytes = Encrypt(DecryptedBytes);
            var EncryptedString = Encode(EncryptedBytes);
            var ret = EncryptedString;

            return ret;

        }

    }

}
