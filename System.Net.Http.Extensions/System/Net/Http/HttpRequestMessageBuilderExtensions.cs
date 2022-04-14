using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net.Http.Message;
using System.Net.Http.Message.Modifiers;
using System.Text.Json;
using System.Threading.Tasks;

namespace System.Net.Http
{
    public static class HttpRequestMessageBuilderExtensions {

        public static async Task<HttpRequestMessage> ToMessageAsync(this IHttpRequestMessageBuilder This) {
            var ret = new HttpRequestMessage();

            await This.ModifyAsync(ret)
                .DefaultAwait()
                ;

            return ret;
        }

        public static HttpRequestMessageBuilder AsMessageBuilder(this IHttpRequestMessageBuilder This) {
            var ret = This as HttpRequestMessageBuilder;
            if(ret == default) {
                ret = new HttpRequestMessageBuilder()
                    .Add(This)
                    ;
            }

            return ret;
        }

        public static HttpRequestMessageBuilder Cookie(this HttpRequestMessageBuilder This, IEnumerable<KeyValuePair<string, string>>? Values = default, bool Encode = true, bool? Enabled = default) {
            return This.Add(new AddCookieModifier(Values, Encode, Enabled));
        }

        public static HttpRequestMessageBuilder Cookie(this HttpRequestMessageBuilder This, string Name, string Value, bool Encode = true, bool? Enabled = default) {
            var Values = new Dictionary<string, string>() {
                [Name] = Value,
            };

            return Cookie(This, Values, Encode, Enabled);
        }

        public static HttpRequestMessageBuilder AcceptJson(this HttpRequestMessageBuilder This, bool? RemoveFirst = default, bool? Enabled = default) {
            var Value = "application/json, text/plain, */*";

            return Accept(This, new string[] { Value }, RemoveFirst, Enabled);
        }

        public static HttpRequestMessageBuilder Accept(this HttpRequestMessageBuilder This, string Value, bool? RemoveFirst = default, bool? Enabled = default) {
            return Accept(This, new string[] { Value }, RemoveFirst, Enabled);
        }

        public static HttpRequestMessageBuilder UserAgent(this HttpRequestMessageBuilder This, string? Value = default, bool? RemoveFirst = default, bool? Enabled = default) {
            return This.Add(new SetUserAgentHeaderModifier(Value, RemoveFirst, Enabled));
        }

        public static HttpRequestMessageBuilder Referer(this HttpRequestMessageBuilder This, string? Value = default, bool? RemoveFirst = default, bool? Enabled = default) {
            return This.Add(new SetRefererHeaderModifier(Value, RemoveFirst, Enabled));
        }

        public static HttpRequestMessageBuilder Accept(this HttpRequestMessageBuilder This, IEnumerable<string> Values, bool? RemoveFirst = default, bool? Enabled = default) {
            return This.Add(new SetAcceptHeaderModifier(Values, RemoveFirst, Enabled));
        }

        public static HttpRequestMessageBuilder MessageHeaders(this HttpRequestMessageBuilder This, string Name, string Value, bool? RemoveFirst = default, bool? Enabled = default) {
            return This.Add(new SetMessageHeadersModifier(Name, Value, RemoveFirst, Enabled));
        }

        public static HttpRequestMessageBuilder MessageHeaders(this HttpRequestMessageBuilder This, IEnumerable<KeyValuePair<string, string>> Values, bool? RemoveFirst = default, bool? Enabled = default) {
            return This.Add(new SetMessageHeadersModifier(Values, RemoveFirst, Enabled));
        }

        public static HttpRequestMessageBuilder ContentHeaders(this HttpRequestMessageBuilder This, string Header, string Value, bool? RemoveFirst = default, bool? Enabled = default) {
            return This.Add(new SetContentHeadersModifier(Header, Value, RemoveFirst, Enabled));
        }

        public static HttpRequestMessageBuilder ContentHeaders(this HttpRequestMessageBuilder This, IEnumerable<KeyValuePair<string, string>> Values, bool? RemoveFirst = default, bool? Enabled = default) {
            return This.Add(new SetContentHeadersModifier(Values, RemoveFirst, Enabled));
        }

        public static HttpRequestMessageBuilder BearerAuthorization(this HttpRequestMessageBuilder This, string? Value, bool? Enabled = default) {
            return This.Add(new SetAuthorizationBearerMessageModifier(Value, Enabled));
        }

        public static HttpRequestMessageBuilder BasicAuthorization(this HttpRequestMessageBuilder This, string? Value, bool? Enabled = default) {
            return This.Add(new SetAuthorizationBasicMessageModifier(Value, Enabled));
        }

        public static HttpRequestMessageBuilder ActionAuthorization(this HttpRequestMessageBuilder This, Func<HttpRequestMessage, Task> Action, bool? Enabled = default) {
            return This.Add(new SetAuthorizationActionMessageModifier(Action, Enabled));
        }


        public static HttpRequestMessageBuilder JsonContent(this HttpRequestMessageBuilder This, object? Content, ConfiguredJsonSerializer? Serializer = default, bool? Enabled = default) {
            return This.Add(new SetJsonContentMessageModifier(Content, Serializer, Enabled));
        }

        public static HttpRequestMessageBuilder BinaryContent(this HttpRequestMessageBuilder This, ArraySegment<byte> Content, bool? Enabled = default) {
            return This.Add(new SetBinaryContentMessageModifier(Content, Enabled));
        }

        public static HttpRequestMessageBuilder FormContent(this HttpRequestMessageBuilder This, IEnumerable<KeyValuePair<string, string>> Content, bool? Enabled = default) {
            return This.Add(new SetFormContentMessageModifier(Content, Enabled));
        }

        public static HttpRequestMessageBuilder MultiPartContent(this HttpRequestMessageBuilder This, IEnumerable<KeyValuePair<string, string>> Content, bool? Enabled = default) {
            return This.Add(new SetMultiPartFormContentMessageModifier(Content, Enabled));
        }

        public static HttpRequestMessageBuilder MultiPartContent(this HttpRequestMessageBuilder This, Func<MultipartFormDataContent> Content, bool? Enabled = default) {
            return This.Add(new SetMultiPartFormContentMessageModifier(Content, Enabled));
        }

        public static HttpRequestMessageBuilder MultiPartContent(this HttpRequestMessageBuilder This, Action<MultipartFormDataContent> Content, bool? Enabled = default) {
            return This.Add(new SetMultiPartFormContentMessageModifier(Content, Enabled));
        }

        public static HttpRequestMessageBuilder NoContent(this HttpRequestMessageBuilder This, bool? Enabled = default) {
            return This.Add(new SetNoContentMessageModifier(Enabled));
        }

        public static HttpRequestMessageBuilder StreamContent(this HttpRequestMessageBuilder This, Func<Stream> Content, bool? Enabled = default) {
            return This.Add(new SetStreamContentMessageModifier(Content, Enabled));
        }

        public static HttpRequestMessageBuilder HttpMethod(this HttpRequestMessageBuilder This, HttpMethod Method, bool? Enabled = default) {
            return This.Add(new SetHttpMethodModifier(Method, Enabled));
        }

        public static HttpRequestMessageBuilder HttpGet(this HttpRequestMessageBuilder This, bool? Enabled = default) {
            return This.HttpMethod(Http.HttpMethod.Get, Enabled).NoContent();
        }

        public static HttpRequestMessageBuilder HttpPost(this HttpRequestMessageBuilder This, bool? Enabled = default) {
            return This.HttpMethod(Http.HttpMethod.Post, Enabled);
        }

        public static HttpRequestMessageBuilder HttpPut(this HttpRequestMessageBuilder This, bool? Enabled = default) {
            return This.HttpMethod(Http.HttpMethod.Put, Enabled);
        }

        public static HttpRequestMessageBuilder HttpPatch(this HttpRequestMessageBuilder This, bool? Enabled = default) {
            return This.HttpMethod(Http.HttpMethod.Patch, Enabled);
        }

        public static HttpRequestMessageBuilder HttpDelete(this HttpRequestMessageBuilder This, bool? Enabled = default) {
            return This.HttpMethod(Http.HttpMethod.Delete, Enabled);
        }


        public static HttpRequestMessageBuilder Uri(this HttpRequestMessageBuilder This, string Uri, bool? Enabled = default) {
            return This.Add(new SetUriMessageModifier(Uri, Enabled));
        }

        public static HttpRequestMessageBuilder Uri(this HttpRequestMessageBuilder This, Uri Uri, bool? Enabled = default) {
            return This.Add(new SetUriMessageModifier(Uri, Enabled));
        }

        public static HttpRequestMessageBuilder Uri(this HttpRequestMessageBuilder This, UriBuilder Uri, bool? Enabled = default) {
            return This.Add(new SetUriMessageModifier(Uri, Enabled));
        }

        public static HttpRequestMessageBuilder Add(this HttpRequestMessageBuilder This, IHttpRequestMessageBuilder Action) {
            var NewActions = This.Actions.Add(Action);

            var ret = This with
            {
                Actions = NewActions
            };

            return ret;
        }

        public static HttpRequestMessageBuilder Add(this HttpRequestMessageBuilder This, SetContentMessageModifier Action) {
            return AddOnlyOne(This, Action);
        }

        public static HttpRequestMessageBuilder Add(this HttpRequestMessageBuilder This, SetHttpMethodModifier Action) {
            return AddOnlyOne(This, Action);
        }

        public static HttpRequestMessageBuilder Add(this HttpRequestMessageBuilder This, SetAuthorizationMessageModifier Action) {
            return AddOnlyOne(This, Action);
        }

        public static HttpRequestMessageBuilder AddOnlyOne<T>(this HttpRequestMessageBuilder This, T Action) where T : MessageModifier {
            var NewActions = This.Actions.Where(x => x is not T).ToList();
            NewActions.Add(Action);

            var ret = This with
            {
                Actions = NewActions.ToImmutableList()
            };

            return ret;
        }

    }
}
