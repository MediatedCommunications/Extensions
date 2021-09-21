using System.Diagnostics;

namespace System {
    public abstract record StructParser<T> : DisplayRecord where T : struct {
        public string? Input { get; init; }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Input)
                ;
        }



        public abstract bool TryGetValue(out T Result);

        public virtual bool TryGetValue(out T Result, T Default = default) {
            var ret = false;
            Result = Default;

            if (TryGetValue(out var NewResult)) {
                ret = true;
                Result = NewResult;
            } else {
                Input.Ignore();
            }

            return ret;
        }

        public virtual T GetValue(T Default = default) {
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
