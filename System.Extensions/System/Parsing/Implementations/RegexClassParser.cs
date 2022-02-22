﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace System
{
    public abstract record RegexClassParser<TResult> : ListParser<TResult> {
        public Regex Regex { get; init; } = RegularExpressions.None;

        protected abstract bool TryGetValue(Match Input, [NotNullWhen(true)] out TResult? result);

        public override bool TryGetValue([NotNullWhen(true)] out LinkedList<TResult>? Result) {
            var ret = false;
            Result = default;

            var MatchParser = new RegexMatchParser() {
                Regex = Regex,
                Input = Input,
            };

            if (MatchParser.TryGetValue(out var Matches)) {
                var TResult = new LinkedList<TResult>();

                foreach (var Match in Matches) {

                    if(TryGetValue(Match, out var TItem)) {
                        TResult.Add(TItem);
                    }

                }

                if(TResult.Count > 0) {
                    ret = true;
                    Result = TResult;
                }
            }



            return ret;
        }

    }


}
