using System.Security.Cryptography;

namespace System.Security {
    internal static class RsaDefaults {
        public static RSAEncryptionPadding Padding { get; } = RSAEncryptionPadding.Pkcs1;
    }

}

