using System.Collections.Generic;

namespace System
{
    public static class StringExtensions_Join {
        private const string Join_DefaultSeparator = "";
        public static string Join<T>(this IEnumerable<T>? This, string Separator = Join_DefaultSeparator, int RepeatSeparator = 1) {
            var NewSeparator = Strings.Empty;
            for (var i = 0; i < RepeatSeparator; i++) {
                NewSeparator += Separator;
            }
            var ret = string.Join(NewSeparator, This.EmptyIfNull());

            return ret;
        }

        public static string JoinColon<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Colon, RepeatSeparator);
        }

        public static string JoinComma<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Comma, RepeatSeparator);
        }

        public static string JoinDash<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Dash, RepeatSeparator);
        }
        
        public static string JoinDot<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Dot, RepeatSeparator);
        }

        public static string JoinNull<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Null, RepeatSeparator);
        }

        public static string JoinPipe<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Pipe, RepeatSeparator);
        }
        
        public static string JoinSemicolon<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Semicolon, RepeatSeparator);
        }

        public static string JoinSpace<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Space, RepeatSeparator);
        }

        public static string JoinTab<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Tab, RepeatSeparator);
        }

        public static string JoinUnderscore<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Underscore, RepeatSeparator);
        }



        public static string JoinSeparator<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.Separator, RepeatSeparator);
        }

        public static string JoinLine<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.CR, RepeatSeparator);
        }

        public static string JoinPath<T>(this IEnumerable<T>? This) {
            var Values = (
                from x in This.EmptyIfNull()
                let v = x.ToString()
                select v
                ).ToArray();

            return System.IO.Path.Combine(Values);
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

        public static string JoinPathUser<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(Strings.SlashUser, RepeatSeparator);
        }


    }
}
