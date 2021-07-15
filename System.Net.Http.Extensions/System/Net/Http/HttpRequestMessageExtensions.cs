namespace System.Net.Http {
    public static partial class HttpRequestMessageExtensions {
        public static void SetAuthorizationBearer(this HttpRequestMessage This, string? Value) {
            if (Value.IsNotBlank()) {
                This.Headers.Add("Authorization", $@"Bearer {Value}");
            }
        }

        public static void SetAuthorizationBasic(this HttpRequestMessage This, string? Value) {
            if (Value.IsNotBlank()) {
                This.Headers.Add("Authorization", $@"Basic {Value}");
            }
        }
    }

}
