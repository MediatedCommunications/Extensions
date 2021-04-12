using System.Diagnostics;

namespace System {
    public abstract record StructParser<T> : DisplayRecord where T : struct {
        public string? Value { get; init; }

        public StructParser(string? Value) {
            this.Value = Value;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Value)
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
                Value.Ignore();
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
