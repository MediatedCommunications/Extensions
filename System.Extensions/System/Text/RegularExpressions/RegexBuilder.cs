using System.Diagnostics;

namespace System.Text.RegularExpressions {
    public class RegexBuilder : DisplayClass {
        public StringBuilder Script { get; }
        public RegexBuilderFormatter Literal { get; }
        public RegexBuilderFormatter Regex { get; }

        public RegexBuilder() {
            Script = new StringBuilder();
            Literal = new RegexBuilderFormatter_Literal(this);
            Regex = new RegexBuilderFormatter_Regex(this);
        }

        public Regex Compile() {
            var ret = new Regex(Script.ToString(), RegularExpressions.Options);

            return ret;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Script.ToString())
                ;
        }

        public override string ToString() {
            return Script.ToString();
        }


    }
}
