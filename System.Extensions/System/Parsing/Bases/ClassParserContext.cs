using System.Diagnostics.CodeAnalysis;

namespace System {
    public record ClassParserContext<TParser, TParse> : ParserContext<TParser>
        where TParser : ClassParser<TParse>
        where TParse : class {

        public ClassParserContext(string? Value, TParser Parser) : base(Value, Parser) {
            
        }

        public bool TryGetValue([NotNullWhen(true)] out TParse? Result) {
            return Parser.TryGetValue(Value, out Result);
        }

        public bool TryGetValue(out TParse Result, TParse Default) {
            return Parser.TryGetValue(Value, out Result, Default);
        }

        public TParse GetValue(TParse Default) {
            return Parser.GetValue(Value, Default);
        }

        public TParse? TryGetValue(TParse? Default = default) {
            return Parser.TryGetValue(Value, Default);
        }

    }

    public static class ClassParserContext {
        public static ClassParserContext<TParser, TParse> Create<TParser, TParse>(
            ParseValue? Input,
            TParser Parser
            )
            where TParser : ClassParser<TParse>
            where TParse : class {
            return Create<TParse, TParser>(Input?.Value, Parser);
        }

        public static ClassParserContext<TParser, TParse> Create<TParse, TParser>(
            string? Input,
            TParser Parser
            )
            where TParser : ClassParser<TParse>
            where TParse : class {
            return new ClassParserContext<TParser, TParse>(Input, Parser);
        }


    }
}
