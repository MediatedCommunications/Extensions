namespace System.Security {
    public interface IEncryptor {
        string Encrypt(string Value);
        byte[] Encrypt(byte[] Value);
    }
}

