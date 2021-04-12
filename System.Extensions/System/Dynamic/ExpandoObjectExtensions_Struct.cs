namespace System.Dynamic {
    public static class ExpandoObjectExtensions_Struct {

        public static ExpandoObject AddIdProperty<T>(this ExpandoObject This, string PropertyPath, OptionalStructProperty<T> Value)
            where T : struct {

            var NewPath = PropertyPath;
            if (Value.Value is { }) {
                NewPath += ".id";
            }

            This.AddProperty(NewPath, Value.Value, Value.HasBeenSet);

            return This;
        }

        public static ExpandoObject AddProperty<T>(this ExpandoObject This, string PropertyPath, OptionalStructProperty<T> Value) 
            where T : struct
            {
            This.AddProperty(PropertyPath, Value.Value, Value.HasBeenSet);

            return This;
        }

        public static ExpandoObject AddProperty<T, U>(this ExpandoObject This, string PropertyPath, OptionalStructProperty<T> Value, Func<T?,U> Converter)
            where T : struct {
            if (Value.HasBeenSet) {
                var NewValue = Converter(Value.Value);
                This.AddProperty(PropertyPath, NewValue);
            }

            return This;
        }

    }
}
