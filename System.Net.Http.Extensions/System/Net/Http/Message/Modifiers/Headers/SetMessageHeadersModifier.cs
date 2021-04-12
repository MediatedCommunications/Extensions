using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers {
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

}
