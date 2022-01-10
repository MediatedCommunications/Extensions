using System.Collections.Immutable;
using System.Linq;

namespace System {
    public static class Strings {
        public const string Space = " ";
        public const string Dot = ".";
        public const string Pipe = "|";
        public const string Comma = ",";
        public const string Dash = "-";
        public const string Underscore = "_";
        public const string Separator = " - ";
        public static readonly ImmutableArray<string> Dots = new[] { Dot }.ToImmutableArray();

        public const string SlashWindows = @"\";
        public const string SlashUnix = @"/";
        public const string SlashWeb = @"/";
        public static readonly ImmutableArray<string> Slashes = new[] { SlashWindows, SlashUnix, SlashWeb }.Distinct().ToImmutableArray();

        public const string CR = "\r";
        public const string LF = "\n";
        public const string CRLF = CR + LF;
        public const string TAB = "\t";

        public const string Null = "\0";
        public static readonly ImmutableArray<string> NewLines = new[] { CRLF, CR, LF }.ToImmutableArray();
    }

}
