using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http.Message.Modifiers;
using System.Text.Json;
using System.Threading.Tasks;

namespace System.Net.Http.Message {
    public static class MessageBuilderExtensions { 

        public static MessageBuilder AsMessageBuilder(this IMessageModifier This) {
            var ret = This as MessageBuilder;
            if(ret == default) {
                ret = new MessageBuilder()
                    .Add(This)
                    ;
            }

            return ret;
        }

        public static MessageBuilder Cookie(this MessageBuilder This, IEnumerable<KeyValuePair<string, string?>>? Values = default, bool Encode = true, bool? Enabled = default) {
            return This.Add(new AddCookieModifier(Values, Encode, Enabled));
        }

        public static MessageBuilder Cookie(this MessageBuilder This, string Name, string? Value, bool Encode = true, bool? Enabled = default) {
            var Values = new Dictionary<string, string?>() {
                [Name] = Value,
            };

            return Cookie(This, Values, Encode, Enabled);
        }

        

        public static MessageBuilder Accept(this MessageBuilder This, string Value, bool? RemoveFirst = default, bool? Enabled = default) {
            return Accept(This, new string[] { Value }, RemoveFirst, Enabled);
        }

        public static MessageBuilder UserAgent(this MessageBuilder This, string? Value = default, bool? RemoveFirst = default, bool? Enabled = default) {
            return This.Add(new SetUserAgentHeaderModifier(Value, RemoveFirst, Enabled));
        }


        public static MessageBuilder Accept(this MessageBuilder This, IEnumerable<string> Values, bool? RemoveFirst = default, bool? Enabled = default) {
            return This.Add(new SetAcceptHeaderModifier(Values, RemoveFirst, Enabled));
        }

        public static MessageBuilder MessageHeaders(this MessageBuilder This, string Name, string? Value, bool? RemoveFirst = default, bool? Enabled = default) {
            return This.Add(new SetMessageHeadersModifier(Name, Value, RemoveFirst, Enabled));
        }

        public static MessageBuilder MessageHeaders(this MessageBuilder This, IEnumerable<KeyValuePair<string, string?>> Values, bool? RemoveFirst = default, bool? Enabled = default) {
            return This.Add(new SetMessageHeadersModifier(Values, RemoveFirst, Enabled));
        }

        public static MessageBuilder ContentHeaders(this MessageBuilder This, string Header, string Value, bool? RemoveFirst = default, bool? Enabled = default) {
            return This.Add(new SetContentHeadersModifier(Header, Value, RemoveFirst, Enabled));
        }

        public static MessageBuilder ContentHeaders(this MessageBuilder This, IEnumerable<KeyValuePair<string, string?>> Values, bool? RemoveFirst = default, bool? Enabled = default) {
            return This.Add(new SetContentHeadersModifier(Values, RemoveFirst, Enabled));
        }

        public static MessageBuilder BearerAuthorization(this MessageBuilder This, string? Value, bool? Enabled = default) {
            return This.Add(new SetAuthorizationBearerMessageModifier(Value, Enabled));
        }

        public static MessageBuilder BasicAuthorization(this MessageBuilder This, string? Value, bool? Enabled = default) {
            return This.Add(new SetAuthorizationBasicMessageModifier(Value, Enabled));
        }

        public static MessageBuilder ActionAuthorization(this MessageBuilder This, Func<HttpRequestMessage, Task> Action, bool? Enabled = default) {
            return This.Add(new SetAuthorizationActionMessageModifier(Action, Enabled));
        }


        public static MessageBuilder JsonContent(this MessageBuilder This, object? Content, ConfiguredJsonSerializer? Serializer = default, bool? Enabled = default) {
            return This.Add(new SetJsonContentMessageModifier(Content, Serializer, Enabled));
        }

        public static MessageBuilder BinaryContent(this MessageBuilder This, ArraySegment<byte> Content, bool? Enabled = default) {
            return This.Add(new SetBinaryContentMessageModifier(Content, Enabled));
        }

        public static MessageBuilder FormContent(this MessageBuilder This, IEnumerable<KeyValuePair<string, string>> Content, bool? Enabled = default) {
            return This.Add(new SetFormContentMessageModifier(Content, Enabled));
        }

        public static MessageBuilder MultiPartContent(this MessageBuilder This, IEnumerable<KeyValuePair<string, string>> Content, bool? Enabled = default) {
            return This.Add(new SetMultiPartFormContentMessageModifier(Content, Enabled));
        }

        public static MessageBuilder MultiPartContent(this MessageBuilder This, Func<MultipartFormDataContent> Content, bool? Enabled = default) {
            return This.Add(new SetMultiPartFormContentMessageModifier(Content, Enabled));
        }

        public static MessageBuilder NoContent(this MessageBuilder This, bool? Enabled = default) {
            return This.Add(new SetNoContentMessageModifier(Enabled));
        }


        public static MessageBuilder HttpMethod(this MessageBuilder This, HttpMethod Method, bool? Enabled = default) {
            return This.Add(new SetHttpMethodModifier(Method, Enabled));
        }

        public static MessageBuilder HttpGet(this MessageBuilder This, bool? Enabled = default) {
            return This.HttpMethod(Http.HttpMethod.Get, Enabled).NoContent();
        }

        public static MessageBuilder HttpPost(this MessageBuilder This, bool? Enabled = default) {
            return This.HttpMethod(Http.HttpMethod.Post, Enabled);
        }

        public static MessageBuilder HttpPut(this MessageBuilder This, bool? Enabled = default) {
            return This.HttpMethod(Http.HttpMethod.Put, Enabled);
        }

        public static MessageBuilder HttpPatch(this MessageBuilder This, bool? Enabled = default) {
            return This.HttpMethod(Http.HttpMethod.Patch, Enabled);
        }

        public static MessageBuilder HttpDelete(this MessageBuilder This, bool? Enabled = default) {
            return This.HttpMethod(Http.HttpMethod.Delete, Enabled);
        }


        public static MessageBuilder Uri(this MessageBuilder This, string Uri, bool? Enabled = default) {
            return This.Add(new SetUriMessageModifier(Uri, Enabled));
        }

        public static MessageBuilder Uri(this MessageBuilder This, Uri Uri, bool? Enabled = default) {
            return This.Add(new SetUriMessageModifier(Uri, Enabled));
        }

        public static MessageBuilder Uri(this MessageBuilder This, UriBuilder Uri, bool? Enabled = default) {
            return This.Add(new SetUriMessageModifier(Uri, Enabled));
        }

        public static MessageBuilder Add(this MessageBuilder This, IMessageModifier Action) {
            var NewActions = This.Actions.Add(Action);

            var ret = This with
            {
                Actions = NewActions
            };

            return ret;
        }

        public static MessageBuilder Add(this MessageBuilder This, SetContentMessageModifier Action) {
            return AddOnlyOne(This, Action);
        }

        public static MessageBuilder Add(this MessageBuilder This, SetHttpMethodModifier Action) {
            return AddOnlyOne(This, Action);
        }

        public static MessageBuilder Add(this MessageBuilder This, SetAuthorizationMessageModifier Action) {
            return AddOnlyOne(This, Action);
        }

        public static MessageBuilder AddOnlyOne<T>(this MessageBuilder This, T Action) where T : MessageModifier {
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
