using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers
{

    public record SetContentHeadersModifier : SetHeadersModifier {
        public SetContentHeadersModifier(IEnumerable<KeyValuePair<string, string>> Values, bool? RemoveFirst = default, bool? Enabled = default) : base(Values, RemoveFirst, Enabled) {

        }


        public SetContentHeadersModifier(string Name, string Value, bool? RemoveFirst = null, bool? Enabled = null) : base(Name, Value, RemoveFirst, Enabled) {
        
        }

        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {
            if (Message.Content is { } V1) {
                if (RemoveFirst) {
                    foreach (var item in this.Values) {
                        V1.Headers.Remove(item.Key);
                    }
                }

                foreach (var item in this.Values) {
                    V1.Headers.Add(item.Key, item.Value);
                }

            }
            
            return base.ModifyEnabledAsync(Message);
        }

    }

}
