using System.Diagnostics;

namespace System {

    public abstract record StructParser<T> : DisplayRecord where T : struct {

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                
                ;
        }



        public abstract bool TryGetValue(string? Input, out T Result);

        public virtual bool TryGetValue(string? Input, out T Result, T Default = default) {
            var ret = false;
            Result = Default;

            if (TryGetValue(Input, out var NewResult)) {
                ret = true;
                Result = NewResult;
            } else {
                Input.Ignore();
            }

            return ret;
        }

        public virtual T GetValue(string? Input, T Default = default) {
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
