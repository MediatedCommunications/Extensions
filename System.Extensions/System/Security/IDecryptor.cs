using System.Diagnostics;

namespace System.Security {


    public interface IDecryptor {
        string Decrypt(string Value);
        byte[] Decrypt(byte[] Value);
    }
}

