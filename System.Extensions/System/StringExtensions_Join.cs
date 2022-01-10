using System.Collections.Generic;

namespace System
{
    public static class StringExtensions_Join {
        private const string Join_DefaultSeparator = "";
        public static string Join<T>(this IEnumerable<T>? This, string Separator = Join_DefaultSeparator, int RepeatSeparator = 1) {
            var NewSeparator = string.Empty;
            for (var i = 0; i < RepeatSeparator; i++) {
                NewSeparator += Separator;
            }

            var ret = string.Join(NewSeparator, This.EmptyIfNull());

            return ret;
        }

        public static string JoinSpace<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Space, RepeatSeparator);
        }

        public static string JoinComma<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Comma, RepeatSeparator);
        }

        public static string JoinSeparator<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Separator, RepeatSeparator);
        }

        public static string JoinDot<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Dot, RepeatSeparator);
        }

        public static string JoinDash<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Dash, RepeatSeparator);
        }

        public static string JoinPipe<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Pipe, RepeatSeparator);
        }

        public static string JoinLine<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.CR, RepeatSeparator);
        }

        public static string JoinPathWindows<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.SlashWindows, RepeatSeparator);
        }

        public static string JoinPathUnix<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.SlashUnix, RepeatSeparator);
        }

        public static string JoinPathWeb<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.SlashWeb, RepeatSeparator);
        }

        public static string JoinUnderscore<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Underscore, RepeatSeparator);
        }

    }
}
