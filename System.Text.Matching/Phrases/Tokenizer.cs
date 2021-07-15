using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace System.Text.Matching {
    public abstract class Tokenizer {
        protected abstract ImmutableList<string> TokenizeInternal(string Input);

        public TokenizeResult<string> Tokenize(string Input) {
            var ret = Tokenize(Input, x => new string[] { x });
            return ret;
        }

        public TokenizeResult<T> Tokenize<T>(T Input, Func<T, IEnumerable<string?>?> GetTexts) {
            var tret = new List<string>();
            foreach (var Text in GetTexts(Input).Coalesce<string>().WhereIsNotBlank()) {
                var Value = this.TokenizeInternal(Text);
                tret.Add(Value);
            }

            var ret = TokenizeResult.Create(Input, tret.ToImmutableList());

            return ret;
        }

        public ImmutableList<TokenizeResult<T>> Tokenize<T>(IEnumerable<T>? Input, Func<T, IEnumerable<string?>?> GetTexts) {
            var SW = System.Diagnostics.Stopwatch.StartNew();
            
            var ret = (
                from x in Input.Coalesce()
                let v = Tokenize(x, GetTexts)
                select v
                ).ToImmutableList();

            SW.Stop();

            return ret;
        }
    }


    public abstract record TokenizeResult : DisplayRecord {
        public ImmutableList<string> Tokens { get; init; } = ImmutableList<string>.Empty;

        public static TokenizeResult<T> Create<T>(T Value, ImmutableList<string> Tokens) {
            var ret = new TokenizeResult<T>(Value, Tokens);

            return ret;
        }

    }

    public record TokenizeResult<T> : TokenizeResult {
        public T Value { get; init; }
        
        public TokenizeResult(T Value, ImmutableList<string> Tokens) {
            this.Value = Value;
            this.Tokens = Tokens;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Value)
                .Postfix.Add(Tokens)
                ;
        }


    }


}
