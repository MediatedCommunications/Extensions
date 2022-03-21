using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System {

    public abstract record ClassParser<T> : DisplayRecord where T : class {

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                ;
        }


        public abstract bool TryGetValue(string? Input, [NotNullWhen(true)] out T? Result);

        public virtual bool TryGetValue(string? Input, out T Result, T Default) {
            var ret = false;
            Result = Default;

            if (TryGetValue(Input, out var NewResult)) {
                ret = true;
                Result = NewResult;
            }

            return ret;
        }

        public virtual T GetValue(string? Input, T Default) {
            TryGetValue(Input, out var ret, Default);

            return ret;
        }

        public virtual T? TryGetValue(string? Input, T? Default = default) {
            var ret = Default;

            if (TryGetValue(Input, out var NewResult)) {
                ret = NewResult;
            }

            return ret;
        }


    }


}
