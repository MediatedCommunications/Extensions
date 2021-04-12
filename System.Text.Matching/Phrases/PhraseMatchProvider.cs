using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Matching {
    public abstract class PhraseMatchProvider {
        public PhraseMatchResult Match(string Left, string Right, StringComparer? OptionalComparer = default, Tokenizer? OptionalTokenizer = default) {
            var Tokenizer = Defaults.GetTokenizer(OptionalTokenizer);

            var LeftTokens = Tokenizer.Tokenize(Left);
            var RightTokens = Tokenizer.Tokenize(Right);

            var Comparer = Defaults.GetComparer(OptionalComparer);

            return Match(LeftTokens, RightTokens, Comparer);

        }

        public abstract PhraseMatchResult Match(ImmutableList<string> Left, ImmutableList<string> Right, StringComparer Comparer);

    }


}
