using System.Diagnostics;
using System.Linq;

namespace System.Text.RegularExpressions {
    public abstract class RegexBuilderFormatter : DisplayClass {
        protected RegexBuilder Builder { get; }

        public RegexBuilderFormatter(RegexBuilder Builder) {
            this.Builder = Builder;
        }

        protected virtual string Paren(string Value) {
            var ret = $@"({Value})";

            return ret;
        }

        protected virtual string Encode(string Value) {
            return Value;
        }


        public virtual RegexBuilder And(params string[] Values) {
            var ToAppend = Values.Select(x => Paren(Encode(x))).Join();

            Builder.Script.Append(Paren(ToAppend));

            return Builder;
        }

        public virtual RegexBuilder Or(params string[] Values) {

            var ToAppend = Values.Select(x => Paren(Encode(x))).Join("|");

            Builder.Script.Append(Paren(ToAppend));

            return Builder;
        }
    }
}
