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

        static RegularExpressions()
        {
            Options = RegexOptions.None
                | RegexOptions.IgnoreCase 
                | RegexOptions.IgnorePatternWhitespace 
                | RegexOptions.Compiled
                ;

            None = new Regex($@"(?!)", Options);
        }

    }
}
