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

        public static string AsString(this FormatValue<PersonName> This) {
            return This.AsString(default, default);
        }

        public static string AsString(this FormatValue<PersonName> This, PersonNameFormat? Format) {
            return This.AsString(Format, default);
        }

        public static string AsString(this FormatValue<PersonName> This, PersonNameFields? Fields) {
            return This.AsString(default, Fields);
        }

        public static string AsString(this FormatValue<PersonName> This, PersonNameFormat? Format = default, PersonNameFields? Fields = default) {
            Format ??= PersonNameFormats.LastFirstMiddle;
            var NewFields = Fields ?? PersonNameFields.All;

            var ret = Format.FormatName(This.Value, NewFields);

            return ret;
        }


        public static string AsSortable(this FormatValue<DateTime> This, IFormatProvider? Culture = default) {
            return This.Value.ToString("s", Culture.Coalesce());
        }

        public static string AsSortable(this FormatValue<DateTimeOffset> This, IFormatProvider? Culture = default) {
            return This.Value.ToString("s", Culture.Coalesce());
        }

        public static string AsSortableStandard(this FormatValue<DateTimeOffset> This) {
            var ret = $@"{This.Value:yyyy-MM-dd @ HH:mm.ss}";

            return ret;
        }

        public static string AsSortableExtended(this FormatValue<DateTimeOffset> This) {
            var ret = $@"{This.Value:yyyy-MM-dd @ HH:mm.ss.ffff}";

            return ret;
        }


        public static string AsSortableStandard(this FormatValue<DateTimeOffset?> This) {
            var ret = Strings.Empty;
            if(This.Value is { } Value) {
                ret = Value.Format().AsSortableStandard();
            }
            

            return ret;
        }

        public static string AsCurrency(this FormatValue<decimal> This, IFormatProvider? Culture = default) {
            return This.Value.ToString($@"C", Culture.Coalesce());
        }

    }

}
