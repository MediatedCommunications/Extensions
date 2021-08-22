using System.Collections.Immutable;
using System.Linq;

namespace System.Text.Matching
{
    public static class Defaults {
        public static StringComparer GetComparer(StringComparer? OptionalComparer) {
            var ret = OptionalComparer ?? StringComparer.InvariantCultureIgnoreCase;
            return ret;
        }

        public static ImmutableList<string> GetLetters(string Value) {
            var ret = Value.Select(x => x.ToString()).ToImmutableList();

            return ret;
        }

        public static Tokenizer GetTokenizer(Tokenizer? OptionalTokenizer) {
            return OptionalTokenizer ?? Tokenizers.Default;
        }
    }

}
