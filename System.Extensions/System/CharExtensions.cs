using System.Collections.Generic;
using System.Linq;

namespace System {

    public static class CharExtensions {
        public static IEnumerable<char> WhereIsDigit(this IEnumerable<char>? This) {
            return This.EmptyIfNull().Where(x => char.IsDigit(x));
        }

        public static string AsString(this IEnumerable<char>? This) {
            var Array = This.EmptyIfNull().ToArray();

            var ret = new string(Array);

            return ret;
        }

        public static bool IsUpper(this char This) {
            return char.IsUpper(This);
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


    }

}
