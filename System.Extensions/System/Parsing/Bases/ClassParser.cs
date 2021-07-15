using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System {

    public abstract record ClassParser<T> : DisplayRecord where T : class {

        public string? Input { get; init; }
        
        public ClassParser(string? Input) {
            this.Input = Input;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Input)
                ;
        }


        public abstract bool TryGetValue([NotNullWhen(true)] out T? Result);

        public virtual bool TryGetValue(out T Result, T Default) {
            var ret = false;
            Result = Default;

            if (TryGetValue(out var NewResult)) {
                ret = true;
                Result = NewResult;
            }

            return ret;
        }

        public virtual T GetValue(T Default) {
            TryGetValue(out var ret, Default);

            return ret;
        }

        public virtual T? TryGetValue(T? Default = default) {
            var ret = Default;

            if (TryGetValue(out var NewResult)) {
                ret = NewResult;
            }

            return ret;
        }


    }


}
