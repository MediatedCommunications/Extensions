namespace System.Net.Http {
    public static partial class HttpRequestMessageExtensions {
        public static void SetAuthorization(this HttpRequestMessage This, string? Value) {
            if (Value.IsNotBlank()) {
                This.Headers.Add("Authorization", $@"{Value}");
            }
        }

        public static void SetAuthorization(this HttpRequestMessage This, string? Kind, string? Value) {
            if (Kind.IsNotBlank() && Value.IsNotBlank()) {
                This.SetAuthorization($@"{Kind} {Value}");
            }
        }

        public static void SetAuthorizationBearer(this HttpRequestMessage This, string? Value) {
            This.SetAuthorization("Bearer", Value);
        }

        public static void SetAuthorizationOauth(this HttpRequestMessage This, string? Value) {
            This.SetAuthorization("OAuth", Value);
        }

        public static void SetAuthorizationBasic(this HttpRequestMessage This, string? Value) {
            This.SetAuthorization("Basic", Value);
        }
    }

}
