using System.Collections.Generic;
using System.Collections.Immutable;

namespace System.Text.Matching {
    public class DefaultTokenizer : Tokenizer {

        public static DefaultTokenizer Instance { get; private set; } = new();

        public DefaultTokenizerOptions Options { get; private set; }

        public DefaultTokenizer(DefaultTokenizerOptions? Options = default) {
            this.Options = Options ?? new();
        }

        /// <summary>
        /// Split text into a series of words using the specified settings.
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        protected override ImmutableList<string> TokenizeInternal(string Input) {
            var ret = new List<string>();
            var Buffer = new StringBuilder();

            void AddBuffer() {
                var V = Buffer.ToString();
                if(V.Length > 0) {
                    ret.Add(V);
                    Buffer = new StringBuilder();
                }
            }

            foreach (var item in Input) {
                var Action = Options.GetAction(item);

                if(Action == CharacterAction.IncludeInWord) {
                    Buffer.Append(item);
                } else if (Action == CharacterAction.IgnoreInWord) {
                    //Do nothing;
                } else if (Action == CharacterAction.StartNewWord) {
                    AddBuffer();
                }
            }
            AddBuffer();


            return ret.ToImmutableList();
        }
    }

}
