﻿using System.Collections.Generic;

namespace System
{
    public static class StringExtensions_Join {
        private const string Join_DefaultSeparator = "";
        public static string Join<T>(this IEnumerable<T>? This, string Separator = Join_DefaultSeparator, int RepeatSeparator = 1) {
            var NewSeparator = string.Empty;
            for (var i = 0; i < RepeatSeparator; i++) {
                NewSeparator += Separator;
            }

            var ret = string.Join(NewSeparator, This.Coalesce());

            return ret;
        }

        public static string JoinSpace<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(StringExtensions.Space, RepeatSeparator);
        }

        public static string JoinComma<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(StringExtensions.Comma, RepeatSeparator);
        }

        public static string JoinSeparator<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(StringExtensions.Separator, RepeatSeparator);
        }

        public static string JoinDot<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(StringExtensions.Dot, RepeatSeparator);
        }

        public static string JoinDash<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(StringExtensions.Dash, RepeatSeparator);
        }

        public static string JoinLine<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(StringExtensions.CR, RepeatSeparator);
        }

        public static string JoinPathWindows<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(StringExtensions.SlashWindows, RepeatSeparator);
        }

        public static string JoinPathUnix<T>(this IEnumerable<T>? This, int RepeatSeparator = 1) {
            return This.Join(StringExtensions.SlashUnix, RepeatSeparator);
        }

    }
}
