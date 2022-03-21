using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace System {
    public record DefaultClassParserContext<TParser, TParse> : ClassParserContext<TParser, TParse>
        where TParser : DefaultClassParser<TParse>
        where TParse : class 
        {

        public DefaultClassParserContext(string? Value, TParser Parser) : base(Value, Parser) {
            
        }

        public TParse GetValue() {
            return this.Parser.GetValue(Value);
        }
      
    }

    public static class DefaultClassParserContext {
        public static DefaultClassParserContext<TParser, TParse> Create<TParser, TParse>(
            ParseValue? Input,
            TParser Parser
            )
            where TParser : DefaultClassParser<TParse>
            where TParse: class
            {
            return Create<TParse, TParser>(Input?.Value, Parser);
        }

        public static DefaultClassParserContext<TParser, TParse> Create<TParse, TParser>(
            string? Input,
            TParser Parser
            )
            where TParser : DefaultClassParser<TParse>
            where TParse : class {
            return new DefaultClassParserContext<TParser, TParse>(Input, Parser);
        }


    }
}
