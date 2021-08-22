using System.Diagnostics;

namespace System.Security
{
    public abstract record Encryptor : DisplayRecord
    {
        public abstract string Encrypt(string Value);
        public abstract string Decrypt(string Value);

        public abstract byte[] Encrypt(byte[] Value);
        public abstract byte[] Decrypt(byte[] Value);
    }

}

