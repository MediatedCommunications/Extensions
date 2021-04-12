using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Matching {
    public abstract class MatchProvider {
        public abstract MatchResult Match(string Left, string Right, StringComparer? OptionalComparer = default);


    }

}
