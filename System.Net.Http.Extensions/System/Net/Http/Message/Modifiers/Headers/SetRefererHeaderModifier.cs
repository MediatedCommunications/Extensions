using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers {
    public record SetRefererHeaderModifier : SetHeadersModifier {


        private static IEnumerable<KeyValuePair<string, string>> GenerateValues(string? Input) {

            var Values = new[]{
                Input ?? UserAgents.Default
            };

            foreach (var Value in Values) {
                var ret = KeyValuePair.Create("Referer", Value);

                yield return ret;
            }


        }

        public SetRefererHeaderModifier(string? Value = default, bool? RemoveFirst = null, bool? Enabled = null) : base(GenerateValues(Value), RemoveFirst, Enabled) {
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

}
