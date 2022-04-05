using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.RegularExpressions {

    public static class RegularExpressions
    {
        

        public static RegexOptions Options { get; }

        /// <summary>
        /// A regex that will never match anything.
        /// </summary>
        public static Regex None { get; }
        public static string NonePattern { get; }

        /// <summary>
        /// A regex that will always match everything
        /// </summary>
        public static Regex Any { get; }
        public static string AnyPattern { get; }

        /// <summary>
        /// A regex that only matches an empty string
        /// </summary>
        public static Regex Empty { get; }
        public static string EmptyPattern { get; }

        static RegularExpressions()
        {
            Options = RegexOptions.None
                | RegexOptions.IgnoreCase 
                | RegexOptions.IgnorePatternWhitespace 
                | RegexOptions.Compiled
                ;

            NonePattern = $@"(?!)";
            AnyPattern = $@".*";
            EmptyPattern = $@"^$";

            None = new Regex(NonePattern, Options);

            Any = new Regex(AnyPattern, Options);

            Empty = new Regex(EmptyPattern, Options);
        }

    }
}
