using System.IO;
using System.Text;

namespace System.Security {
    public class HashCodeAlgorithm {

        protected Func<Cryptography.HashAlgorithm> AlgorithmFactory { get; }
        
        public HashCodeAlgorithm(Func<Cryptography.HashAlgorithm> AlgorithmFactory) {
            this.AlgorithmFactory = AlgorithmFactory;
        }

        private static string MyRetry(Func<string> Action) {
            var ret = Retry.WithAction(Action)
                .MaxAttemptsIs(1)
                .DefaultIs(string.Empty)
                .Invoke()
                ;

            return ret;
        }

        public string TryHashPath(string FilePath) {
            return MyRetry(() => HashPath(FilePath));
        }

        public string TryHash(Stream Content) {
            return MyRetry(()=>Hash(Content));
        }

        public string TryHash(byte[] Content) {
            return MyRetry(() => Hash(Content));
        }

        public string TryHashString(string Content) {
            return MyRetry(()=>TryHashString(Content));
        }

        public string TryHashString(string Content, Encoding Encoding) {
            return MyRetry(()=>TryHashString(Content, Encoding));
        }

        public string HashPath(string filePath) {
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            return Hash(stream);
        }

        public string Hash(Stream Content) {
            using var alg = AlgorithmFactory();

            return Format(alg.ComputeHash(Content));

        }

        public string Hash(byte[] Content) {
            using var alg = AlgorithmFactory();

            return Format(alg.ComputeHash(Content));
        }

        public string HashString(string Content) {
            return HashString(Content, Encoding.Default);
        }

        public string HashString(string Content, Encoding Encoding) {
            return Hash(Encoding.GetBytes(Content));
        }

        private static string Format(byte[] Hash) {
            var ret = BitConverter.ToString(Hash)
                .Replace("-", string.Empty)
                ;

            return ret;
        }


    }
}
