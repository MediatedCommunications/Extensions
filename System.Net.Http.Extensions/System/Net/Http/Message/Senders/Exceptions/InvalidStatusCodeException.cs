namespace System.Net.Http.Message.Senders {
    public class InvalidStatusCodeException : HttpRequestException {
        public InvalidStatusCodeException(HttpStatusCode Code) : base($@"The Status code {Code} is not allowed") {

        }
    }

}
