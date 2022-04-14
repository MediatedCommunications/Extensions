using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace System.Text.Json {
    [JsonConverter(typeof(OptionalJsonConverterFactory))]
    public class Optional<T>
        : DisplayClass
        , IOptional {

        public bool IsPresent { get; init; }
        public bool IsMissing { get => !IsPresent; init => IsPresent = !value; }

        public T? Value { get; init; }

        bool IOptional.IsPresent => IsPresent;
        bool IOptional.IsMissing => IsMissing;
        object? IOptional.Value => Value;

        public Optional<T> Clone() {
            return new Optional<T>(Value, IsPresent);
        }

        public Optional() {
            this.Value = default;
            this.IsPresent = false;
        }

        public Optional(T? Value, bool IsPresent) {
            this.Value = Value;
            this.IsPresent = IsPresent;
        }

        public Optional(T? Value) : this(Value, true) {

        }

        public static implicit operator Optional<T>(T? Value) {
            return Optional.Create(Value);
        }

        public static explicit operator T?(Optional<T> Optional) {
            var ret = default(T);
            if (Optional.IsPresent) {
                ret = Optional.Value;
            }

            return ret;
        }


        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {

            var V = this.Value;

            return IGetDebuggerDisplayDefaults.GetDebuggerDisplayBuilder(this, Builder)
                .If(IsPresent, x => x.Data.Add(V), x => x.Status.IsNotPresent())
                ;
        }
    }

}
