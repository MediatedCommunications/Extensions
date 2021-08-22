namespace System.Text.RegularExpressions {
    internal class RegexBuilderFormatter_Literal : RegexBuilderFormatter {
        public RegexBuilderFormatter_Literal(RegexBuilder Builder) : base(Builder) {
        }

        protected override string Encode(string Value) {
            var ret = Regex.Escape(Value);

            return ret;
        }

    }
}
