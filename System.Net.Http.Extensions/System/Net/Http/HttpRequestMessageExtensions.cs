namespace System.Net.Http {
    public static partial class HttpRequestMessageExtensions {
        public static void SetAuthorizationBearer(this HttpRequestMessage This, string? Bearer) {
            if (Bearer.IsNotBlank()) {
                This.Headers.Add("Authorization", $@"Bearer {Bearer}");
            }
        }
    }

}
