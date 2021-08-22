using System.Collections.Generic;

namespace System.Text.RegularExpressions
{
    public static class Wildcard {

        private static readonly HashSet<char> MatchAll_FileSystem = new() { '*' };
        private static readonly HashSet<char> MatchOne_FileSystem = new() { };
        private static readonly HashSet<char> MatchZeroOrOne_FileSystem = new() { '?' };

        private static readonly HashSet<char> MatchAll_Sql = new() { '%' };
        private static readonly HashSet<char> MatchOne_Sql = new() { '_' };
        private static readonly HashSet<char> MatchZeroOrOne_Sql = new() { };

        public static Regex FileSystem(string Pattern) {
            return Create(Pattern, MatchAll_FileSystem, MatchOne_FileSystem, MatchZeroOrOne_FileSystem);
        }

        public static Regex Sql(string Pattern) {
            return Create(Pattern, MatchAll_Sql, MatchOne_Sql, MatchZeroOrOne_Sql);
        }

        private static Regex Create(string Input, HashSet<char> MatchAll, HashSet<char> MatchOne, HashSet<char> MatchZeroOrOne) {

            var Builder = new StringBuilder();

            foreach (var item in Input) {
                if (MatchAll.Contains(item)) {
                    Builder.Append("(.*)");
                } else if (MatchOne.Contains(item)) {
                    Builder.Append("(.)");
                } else if (MatchZeroOrOne.Contains(item)) {
                    Builder.Append("(.?)");
                } else {
                    Builder.Append(Regex.Escape(item.ToString()));
                }
            }

            var Pattern1 = Builder.ToString();
            var Pattern2 = $@"^{Pattern1}$";

            var Final = Pattern2;

            var ret = new Regex(Final, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            return ret;

        }

    }
}
