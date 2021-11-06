using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace System.Net.Http.Message.Modifiers
{
    public record AddCookieModifier : MessageModifier {


        public ImmutableList<KeyValuePair<string, string>> Values { get; init; }

        public bool Encode { get; init; }

        public AddCookieModifier(IEnumerable<KeyValuePair<string, string>>? Values, bool Encode, bool? Enabled = default) : base(Enabled) {
            this.Values = Values.EmptyIfNull().ToImmutableList();
            this.Encode = Encode;
        }

        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {

            var Separator = "; ";

            var StringValues = (
                from x in this.Values
                let Key = Encode ? HttpUtility.UrlEncode(x.Key) : x.Key
                let Value = Encode ? HttpUtility.UrlEncode(x.Value) : x.Value
                let v = $@"{Key}={Value}"
                select v
                ).ToList();

            var CookieValue = StringValues.Join(Separator);

            Message.Headers.TryAddWithoutValidation("Cookie", CookieValue);
            
            return base.ModifyEnabledAsync(Message);
        }
    }

}
