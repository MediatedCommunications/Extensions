using System.Linq;

namespace System
{
    public static class StringExtensions_Split {
        private const StringSplitOptions Split_DefaultOptions = StringSplitOptions.RemoveEmptyEntries;

        public static string[] Split(this string? This, string[] Separators, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            var ret = This
                .Coalesce()
                .Split(Separators, SplitOptions)
                ;

            return ret;
        }

        public static string[] SplitSpace(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { StringExtensions.Space }, SplitOptions)
                ;
        }


        public static string[] SplitComma(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { StringExtensions.Comma }, SplitOptions)
                ;
        }

        public static string[] SplitDot(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { StringExtensions.Dot }, SplitOptions)
                ;
        }

        public static string[] SplitDash(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { StringExtensions.Dash }, SplitOptions)
                ;
        }

        public static string[] SplitUnderscore(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { StringExtensions.Underscore }, SplitOptions)
                ;
        }

        public static string[] SplitLine(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(StringExtensions.NewLines.ToArray(), SplitOptions)
                ;
        }

        public static string[] SplitPath(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(StringExtensions.Slashes.ToArray(), SplitOptions)
                ;
        }

        public static string[] SplitPathWindows(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { StringExtensions.SlashWindows }, SplitOptions)
                ;
        }

        public static string[] SplitPathUnix(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { StringExtensions.SlashUnix }, SplitOptions)
                ;
        }

        public static string[] SplitNull(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This.Coalesce()
                .Split(new[] { StringExtensions.Null }, SplitOptions);
        }


    }
}
