using System.Collections.Generic;

namespace System {
    public static class StringExtensions_Join {
        private const string Join_DefaultSeparator = "";
        public static string Join(this IEnumerable<string>? This, string Separator = Join_DefaultSeparator, int RepeatSeparator = 1) {
            var NewSeparator = string.Empty;
            for (var i = 0; i < RepeatSeparator; i++) {
                NewSeparator += Separator;
            }

            var ret = string.Join(NewSeparator, This.EmptyIfNull());

            return ret;
        }

        public static string JoinSpace(this IEnumerable<string>? This, int RepeatSeparator = 1) {
            return This.Join(StringExtensions.Space, RepeatSeparator);
        }

        public static string JoinComma(this IEnumerable<string>? This, int RepeatSeparator = 1) {
            return This.Join(StringExtensions.Comma, RepeatSeparator);
        }

        public static string JoinSeparator(this IEnumerable<string>? This, int RepeatSeparator = 1) {
            return This.Join(StringExtensions.Separator, RepeatSeparator);
        }

        public static string JoinDot(this IEnumerable<string>? This, int RepeatSeparator = 1) {
            return This.Join(StringExtensions.Dot, RepeatSeparator);
        }

        public static string JoinDash(this IEnumerable<string>? This, int RepeatSeparator = 1) {
            return This.Join(StringExtensions.Dash, RepeatSeparator);
        }

        public static string JoinLine(this IEnumerable<string>? This, int RepeatSeparator = 1) {
            return This.Join(StringExtensions.CR, RepeatSeparator);
        }

        public static string JoinPathWindows(this IEnumerable<string>? This, int RepeatSeparator = 1) {
            return This.Join(StringExtensions.SlashWindows, RepeatSeparator);
        }

        public static string JoinPathUnix(this IEnumerable<string>? This, int RepeatSeparator = 1) {
            return This.Join(StringExtensions.SlashUnix, RepeatSeparator);
        }

    }
}
