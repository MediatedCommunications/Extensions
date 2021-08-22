using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers
{
    public record SetMessageHeadersModifier : SetHeadersModifier {

        public SetMessageHeadersModifier(IEnumerable<KeyValuePair<string, string?>> Values, bool? RemoveFirst = null, bool? Enabled = null) : base(Values, RemoveFirst, Enabled) {
        }

        public SetMessageHeadersModifier(string Name, string? Value, bool? RemoveFirst = null, bool? Enabled = null) : base(Name, Value, RemoveFirst, Enabled) {
        }

        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {
            if (RemoveFirst) {
                foreach (var item in this.Values) {
                    Message.Headers.Remove(item.Key);
                }
            }

            foreach (var item in this.Values) {
                Message.Headers.Add(item.Key, item.Value);
            }


            return base.ModifyEnabledAsync(Message);
        }

    }

    public record SetAcceptHeaderModifier : SetHeadersModifier {


        private static IEnumerable<KeyValuePair<string, string?>> GenerateValues(IEnumerable<string?> Values) {

            foreach (var Value in Values) {
                var ret = KeyValuePair.Create("Accept", Value);

                yield return ret;
            }


        }

        public SetAcceptHeaderModifier(IEnumerable<string?> Values, bool? RemoveFirst = null, bool? Enabled = null) : base(GenerateValues(Values), RemoveFirst, Enabled) {
        }

        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {
            if (RemoveFirst) {
                foreach (var item in this.Values) {
                    Message.Headers.Remove(item.Key);
                }
            }

            foreach (var item in this.Values) {
                Message.Headers.Add(item.Key, item.Value);
            }


            return base.ModifyEnabledAsync(Message);
        }

    }

    public record SetUserAgentHeaderModifier : SetHeadersModifier {


        private static IEnumerable<KeyValuePair<string, string?>> GenerateValues(string? Input) {

            var Values = new[]{
                Input ?? UserAgents.Default
            };

            foreach (var Value in Values) {
                var ret = KeyValuePair.Create("User-Agent", Value);

                yield return ret;
            }


        }

        public SetUserAgentHeaderModifier(string? Value = default, bool? RemoveFirst = null, bool? Enabled = null) : base(GenerateValues(Value), RemoveFirst, Enabled) {
        }

        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {
            if (RemoveFirst) {
                foreach (var item in this.Values) {
                    Message.Headers.Remove(item.Key);
                }
            }

            foreach (var item in this.Values) {
                Message.Headers.Add(item.Key, item.Value);
            }


            return base.ModifyEnabledAsync(Message);
        }

    }


    public static class UserAgents {
        public static string Default => Chrome91;
        public static string Chrome91 { get; } = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.101 Safari/537.36 Edg/91.0.864.48";
    }

}
