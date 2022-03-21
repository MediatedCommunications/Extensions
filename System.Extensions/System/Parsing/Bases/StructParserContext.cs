namespace System {
    public record StructParserContext<TParser, TParse> : ParserContext<TParser>
        where TParser : StructParser<TParse>
        where TParse : struct {

        public StructParserContext(string? Value, TParser Parser) : base(Value, Parser) {
            
        }

        public bool TryGetValue(out TParse Result) {
            return Parser.TryGetValue(Value, out Result);
        }

        public bool TryGetValue(out TParse Result, TParse Default = default) {
            return Parser.TryGetValue(Value, out Result, Default);
        }

        public TParse GetValue(TParse Default = default) {
            return Parser.GetValue(Value, Default);
        }

        public TParse? TryGetValue(TParse? Default = default) {
            return Parser.TryGetValue(Value, Default);
        }

    }

    public static class StructParserContext {
        public static StructParserContext<TParser, TParse> Create<TParser, TParse>(
            ParseValue? Input,
            TParser Parser
            )
            where TParser : StructParser<TParse>
            where TParse : struct {
            return Create<TParse, TParser>(Input?.Value, Parser);
        }

        public static StructParserContext<TParser, TParse> Create<TParse, TParser>(
            string? Input,
            TParser Parser
            )
            where TParser : StructParser<TParse>
            where TParse : struct {
            return new StructParserContext<TParser, TParse>(Input, Parser);
        }


    }
}
