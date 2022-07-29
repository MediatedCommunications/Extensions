using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace System.Dynamic {
    public static class ExpandoObjectExtensions {

        public static ExpandoObject AddIdProperty<T>(this ExpandoObject This, string PropertyPath, Optional<T> Value) {

            var NewPath = PropertyPath;
            if (Value.Value is { }) {
                NewPath += ".id";
            }

            This.AddProperty(NewPath, Value.Value, Value.IsPresent);

            return This;
        }

        public static ExpandoObject AddProperty<T>(this ExpandoObject This, Optional<T> Value, [CallerArgumentExpression("Value")] string? PropertyPath = default) {
            if (PropertyPath.IsBlank()) {
                throw new NullReferenceException();
            } else {
                This.AddProperty(PropertyPath, Value);
            }
            
            return This;
        }

        public static ExpandoObject AddProperty<T>(this ExpandoObject This, string PropertyPath, Optional<T> Value) {
            This.AddProperty(PropertyPath, Value.Value, Value.IsPresent);

            return This;
        }

        public static ExpandoObject AddProperty<T, U>(this ExpandoObject This, string PropertyPath, Optional<T> Value, Func<T?, U> Converter, bool Condition = true) {
            if (Value.IsPresent && Condition) {
                var NewValue = Converter(Value.Value);
                This.AddProperty(PropertyPath, NewValue);
            }

            return This;
        }

    }
}
