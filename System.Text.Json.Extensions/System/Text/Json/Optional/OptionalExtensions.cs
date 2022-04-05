using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Text.Json {
    public static class OptionalExtensions {

        public static Optional<T> ToOptional<T>(this T This) {
            return Optional.Create(This);
        }

        public static bool TryGetValue<T>(this Optional<T> This, out T? Value) {
            Value = This.Value;

            return This.IsPresent;
        }

        public static bool GetValue<T>(this Optional<T> This, [NotNullWhen(true)] out T? Value) {
            var ret = false;
            Value = default;

            if (This.IsPresent && This.Value is { } V1) {
                Value = V1;
            }

            return ret;
        }




        public static Optional<T> NotNullable<T>(this Optional<T?> This) where T : struct {
            var ret = Optional.Missing<T>();
            if(This.IsPresent && This.Value is { } V1) {
                ret = Optional.Create(V1);
            }

            return ret;
        }

        public static Optional<T> NullIsMissing<T>(this Optional<T> This) where T : class {
            return ValueIsMissing(This, null);
        }

        public static Optional<T?> NullIsMissing<T>(this Optional<T?> This) where T : struct {
            return ValueIsMissing(This, null);
        }

        public static Optional<T> DefaultIsMissing<T>(this Optional<T> This, T? DefaultValue = default) {
            return ValueIsMissing(This, DefaultValue);
        }

        public static Optional<T> ValueIsMissing<T>(this Optional<T> This, T? MissingValue) {
            return IsMissingIf(This, x => !EqualityComparer<T>.Default.Equals(This.Value, MissingValue));
        }

        public static Optional<T> IsMissingIf<T>(this Optional<T> This, Func<T?, bool> IsMissing) {
            return IsPresentIf(This, x => !IsMissing(x));
        }

        public static Optional<T> IsPresentIf<T>(this Optional<T> This, Func<T?, bool> IsPresent) {
            var ret = Optional.Missing<T>();

            if (This.IsPresent && IsPresent(This.Value)) {
                ret = Optional.Create(This.Value);
            }

            return ret;
           
        }
    }


}
