namespace System {
    public record ParserContext<TParser> : ParseValue

        where TParser : class {
        
        public TParser Parser { get; init; }

        public ParserContext(string? Value, TParser Parser) : base(Value) {
            this.Parser = Parser;
        }
  
    }


    public static class ParserContext {

        public static ParserContext<TParser> Create<TParser>(
            ParseValue? Input,
            TParser Parser
            )
            where TParser : class {
            return Create(Input?.Value, Parser);
        }

        public static ParserContext<TParser> Create<TParser>(
            string? Input,
            TParser Parser
            ) 
            where TParser : class
            {
            return new ParserContext<TParser>(Input, Parser);
        }



    }

}
