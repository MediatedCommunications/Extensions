using System.Collections.Immutable;

namespace System.Text.Matching {
    public record DefaultTokenizerOptions {

        public CharacterAction Letters { get; init; } = CharacterAction.IncludeInWord;
        public CharacterAction Digits { get; init; } = CharacterAction.IncludeInWord;
        public CharacterAction Punctuation { get; init; } = CharacterAction.StartNewWord;
        public CharacterAction Whitespace { get; init; } = CharacterAction.StartNewWord;
        public CharacterAction Other { get; init; } = CharacterAction.IncludeInWord;

        public ImmutableDictionary<char, CharacterAction> Overrides { get; init; } = ImmutableDictionary<char, CharacterAction>.Empty;

        public CharacterAction GetAction(char C) {
            var ret = Other;

            if(Overrides.TryGetValue(C, out var Override)) {
                ret = Override;

            } else if (C.IsLetter()) {
                ret = Letters;

            } else if (C.IsDigit()) {
                ret = Digits;

            } else if (C.IsPunctuation()) {
                ret = Punctuation;

            } else if (C.IsWhiteSpace()) {
                ret = Whitespace;

            }

            return ret;
        }

    }

}
