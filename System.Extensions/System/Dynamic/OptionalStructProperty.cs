using System.Diagnostics;

namespace System.Dynamic {

    public static class OptionalStructProperty {

        public static OptionalStructProperty<T> Create<T>() 
            where T:struct
            {
            var ret = new OptionalStructProperty<T>();

            return ret;
        }

        public static OptionalStructProperty<T> Set<T>(T? Value)
            where T : struct {
            var ret = new OptionalStructProperty<T>() {
                Value = Value
            };

            return ret;
        }

        public static OptionalStructProperty<T> SetOrClear<T>(T? Value)
            where T : struct {
            var ret = new OptionalStructProperty<T>() {

            };
            if(Value is { } V1) {
                ret.Value = V1;
            }

            return ret;
        }

    }

    [DebuggerDisplay(Debugger2.DebuggerDisplay)]
    public struct OptionalStructProperty<T> : IGetDebuggerDisplayBuilder 
        where T:struct
        {

        public void Clear() {
            Value = default;
            HasBeenSet = false;
        }

        private T? __Value;
        public T? Value {
            get => __Value;
            set {
                __Value = value;
                HasBeenSet = true;
            }
        }

        public bool HasBeenSet { get; private set; }

        public bool TryGetValue(out T? Value) {
            Value = this.Value;
            return this.HasBeenSet;
        }

        public bool GetValue(out T Value) {
            var ret = false;
            Value = default;

            if (this.HasBeenSet && this.Value is { } V1) {
                ret = true;
                Value = V1;
            }

            return ret;
        }

        public static implicit operator OptionalStructProperty<T>(T? Value) {
            var ret = new OptionalStructProperty<T>() {
                Value = Value,
            };

            return ret;
        }

        public override string ToString() {
            return $@"{Value}";
        }

        public string GetDebuggerDisplay() {
            return IGetDebuggerDisplayDefaults.GetDebuggerDisplay(this);
        }

        public DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return Builder
                .Data.Add(Value)
                .Postfix.If(HasBeenSet).Add("HasBeenSet")
                ;
        }
    }



}
