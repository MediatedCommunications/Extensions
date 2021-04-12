using System.Globalization;

namespace System {
    public static class FormatExtensions {
        public static FormatValue<T> Format<T>(this T This) {
            return new FormatValue<T>(This);
        }

        private static IFormatProvider Coalesce(this IFormatProvider? This) {
            var ret = This ?? CultureInfo.InvariantCulture;

            return ret;
        }

        public static string AsSortable(this FormatValue<DateTime> This, IFormatProvider? Culture = default) {
            return This.Value.ToString("s", Culture.Coalesce());
        }

        public static string AsSortable(this FormatValue<DateTimeOffset> This, IFormatProvider? Culture = default) {
            return This.Value.ToString("s", Culture.Coalesce());
        }

        public static string AsCurrency(this FormatValue<decimal> This, IFormatProvider? Culture = default) {
            return This.Value.ToString($@"C", Culture.Coalesce());
        }

    }

}
