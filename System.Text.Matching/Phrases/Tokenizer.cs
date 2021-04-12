using System.Collections.Immutable;

namespace System.Text.Matching {
    public abstract class Tokenizer {
        public abstract ImmutableList<string> Tokenize(string Input);
    }

}
