using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace System {
    public record ListParserContext<TParser, TParse> : DefaultClassParserContext<TParser, ImmutableList<TParse>>, IEnumerable<TParse>
        where TParser : ListParser<TParse>
        {

        public ListParserContext(string? Value, TParser Parser) : base(Value, Parser) {
            
        }

        public IEnumerator<TParse> GetEnumerator() {
            if(this.Parser.TryGetValue(Value, out var Result)) {
                foreach (var item in Result) {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }

    public static class ListParserContext {
        public static ListParserContext<TParser, TParse> Create<TParser, TParse>(
            ParseValue? Input,
            TParser Parser
            )
            where TParser : ListParser<TParse>
            {
            return Create<TParse, TParser>(Input?.Value, Parser);
        }

        public static ListParserContext<TParser, TParse> Create<TParse, TParser>(
            string? Input,
            TParser Parser
            )
            where TParser : ListParser<TParse>
            {
            return new ListParserContext<TParser, TParse>(Input, Parser);
        }


    }
}
