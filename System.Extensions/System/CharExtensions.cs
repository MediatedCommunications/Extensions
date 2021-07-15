using System.Collections.Generic;
using System.Linq;

namespace System {

    public static class CharExtensions {

        public static IEnumerable<char> WhereIs(this IEnumerable<char>? This, params CharType[] Types) {
            return This.Coalesce().Where(x => x.Is(Types));
        }

        public static string WhereIs(this string? This, params CharType[] Types) {
            return This.Coalesce().Where(x => x.Is(Types)).Join();
        }

        public static string Join(this IEnumerable<char>? This) {
            var Array = This.Coalesce().ToArray();

            var ret = new string(Array);

            return ret;
        }

        public static bool IsUpper(this char This) {
            return char.IsUpper(This);
        }

        public static bool IsControl(this char This) {
            return char.IsControl(This);
        }

        public static bool IsLower(this char This) {
            return char.IsLower(This);
        }

        public static bool IsWhiteSpace(this char This) {
            return char.IsWhiteSpace(This);
        }

        public static bool IsLetter(this char This) {
            return char.IsLetter(This);
        }

        public static bool IsDigit(this char This) {
            return char.IsDigit(This);
        }

        public static bool IsLetterOrDigit(this char This) {
            return char.IsLetterOrDigit(This);
        }

        public static bool IsPunctuation(this char This) {
            return char.IsPunctuation(This);
        }

        public static bool IsHighSurrogate(this char This) {
            return char.IsHighSurrogate(This);
        }

        public static bool IsLowSurrogate(this char This) {
            return char.IsLowSurrogate(This);
        }

        public static bool IsNumber(this char This) {
            return char.IsNumber(This);
        }

        public static bool IsSymbol(this char This) {
            return char.IsSymbol(This);
        }

        public static bool Is(this char This, params CharType[] Types) {
            
            var Mapping = new Dictionary<CharType, Func<char, bool>>() {
                [CharType.Control] = IsControl,
                [CharType.Digit] = IsDigit,
                [CharType.HighSurrogate] = IsHighSurrogate,
                [CharType.Letter] = IsLetter,
                [CharType.LetterOrDigit] = IsLetterOrDigit,
                [CharType.Lower] = IsLower,
                [CharType.LowSurrogate] = IsLowSurrogate,
                [CharType.Number] = IsNumber,
                [CharType.Punctuation] = IsPunctuation,
                [CharType.Symbol] = IsSymbol,
                [CharType.Upper] = IsUpper,
                [CharType.Whitespace] = IsWhiteSpace,
            };

            var ret = false;

            foreach (var item in Types) {
                if(Mapping.TryGetValue(item, out var Method)) {
                    ret = Method(This);
                    if (ret) {
                        break;
                    }
                }
            }
            
            return ret;
        }


    }

}
