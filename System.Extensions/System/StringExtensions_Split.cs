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

        public static string[] SplitColon(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { Strings.Colon }, SplitOptions)
                ;
        }

        public static string[] SplitComma(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { Strings.Comma }, SplitOptions)
                ;
        }

        public static string[] SplitDash(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { Strings.Dash }, SplitOptions)
                ;
        }

        public static string[] SplitDot(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { Strings.Dot }, SplitOptions)
                ;
        }

        public static string[] SplitNull(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { Strings.Null }, SplitOptions)
                ;
        }

        public static string[] SplitPipe(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { Strings.Pipe }, SplitOptions)
                ;
        }

        public static string[] SplitSemicolon(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { Strings.Semicolon }, SplitOptions)
                ;
        }

        public static string[] SplitSpace(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { Strings.Space }, SplitOptions)
                ;
        }

        public static string[] SplitTab(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { Strings.Tab }, SplitOptions)
                ;
        }

        public static string[] SplitUnderscore(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { Strings.Underscore }, SplitOptions)
                ;
        }

        public static string[] SplitSeparator(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { Strings.Separator }, SplitOptions)
                ;
        }

        public static string[] SplitLine(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(Strings.NewLines.ToArray(), SplitOptions)
                ;
        }

        public static string[] SplitPath(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(Strings.Slashes.ToArray(), SplitOptions)
                ;
        }

        public static string[] SplitPathWindows(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { Strings.SlashWindows }, SplitOptions)
                ;
        }

        public static string[] SplitPathUnix(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { Strings.SlashUnix }, SplitOptions)
                ;
        }

        public static string[] SplitPathWeb(this string? This, StringSplitOptions SplitOptions = Split_DefaultOptions) {
            return This
                .Coalesce()
                .Split(new[] { Strings.SlashWeb }, SplitOptions)
                ;
        }




    }
}
