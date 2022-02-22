using System.Collections.Generic;
using System.Collections.Immutable;

namespace System.Text.RegularExpressions
{
    public static class Wildcard {

        private static readonly ImmutableHashSet<char> MatchAll_FileSystem;
        private static readonly ImmutableHashSet<char> MatchOne_FileSystem;
        private static readonly ImmutableHashSet<char> MatchZeroOrOne_FileSystem;

        private static readonly ImmutableHashSet<char> MatchAll_Sql;
        private static readonly ImmutableHashSet<char> MatchOne_Sql;
        private static readonly ImmutableHashSet<char> MatchZeroOrOne_Sql;

        static Wildcard() {
            MatchAll_FileSystem = new char[] { '*' }.ToImmutableHashSet();
            MatchOne_FileSystem = new char[] { }.ToImmutableHashSet();
            MatchZeroOrOne_FileSystem = new char[] { '?' }.ToImmutableHashSet();
            MatchAll_Sql = new char[] { '%' }.ToImmutableHashSet();
            MatchOne_Sql = new char[] { '_' }.ToImmutableHashSet();
            MatchZeroOrOne_Sql = new char[] { }.ToImmutableHashSet();

        }

        public static Regex FileSystem(string Pattern) {
            return Create(Pattern, MatchAll_FileSystem, MatchOne_FileSystem, MatchZeroOrOne_FileSystem);
        }

        public static Regex Sql(string Pattern) {
            return Create(Pattern, MatchAll_Sql, MatchOne_Sql, MatchZeroOrOne_Sql);
        }

        private static Regex Create(string Input, ImmutableHashSet<char> MatchAll, ImmutableHashSet<char> MatchOne, ImmutableHashSet<char> MatchZeroOrOne) {

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
