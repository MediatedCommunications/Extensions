using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace System {
    public static class StringParseExtensions { 

        public static ParseValue Parse(this string? This) {
            return new ParseValue(This);
        }

        public static StringValueParser AsString(this ParseValue This) {
            return new StringValueParser(This.Value);
        }

        public static GuidStructParser AsGuid(this ParseValue This) {
            return new GuidStructParser() {
                Input = This.Value
            };
        }

        public static BoolStructParser AsBool(this ParseValue This) {
            return new BoolStructParser() {
                Input = This.Value
            };
        }

        public static IntStructParser AsInt(this ParseValue This) {
            return new IntStructParser() {
                Input = This.Value
            };
        }

        public static LongStructParser AsLong(this ParseValue This) {
            return new LongStructParser() {
                Input = This.Value
            };
        }

        public static FloatStructParser AsFloat(this ParseValue This) {
            return new FloatStructParser() {
                Input = This.Value,
            };
        }

        public static DecimalStructParser AsDecimal(this ParseValue This) {
            return new DecimalStructParser() {
                Input = This.Value
            };
        }

        public static DoubleValueParser AsDouble(this ParseValue This) {
            return new DoubleValueParser() {
                Input = This.Value
            };
        }

        public static EnumStructParser<T> AsEnum<T>(this ParseValue This) where T : struct {
            return new EnumStructParser<T>() {
                Input = This.Value
            };
        }

        public static TimeSpanStructParser AsTimeSpan(this ParseValue This, TimeSpanFormat Format = TimeSpanFormat.TimeSpan) {
            return new TimeSpanStructParser() {
                Input = This.Value,
                Format = Format,
            };
        }

        public static DateTimeValueParser AsDateTime(this ParseValue This) {
            return new DateTimeValueParser() {
                Input = This.Value
            };
        }

        public static DateTimeOffsetStructParser AsDateTimeOffset(this ParseValue This, DateTimeStyles Style = default, IFormatProvider? FormatProvider = default) {
            return new DateTimeOffsetStructParser() {
                Input = This.Value,
                Style = Style,
                FormatProvider = FormatProvider,
            };
        }

        public static KeyValueRegexParser AsKeyValueRegexMatches(this ParseValue This, Regex RX) {
            return new KeyValueRegexParser(RX, This.Value);
        }

        public static KeyValueRegexParser AsKeyValueRegexMatches(this ParseValue This, string RX) {
            return new KeyValueRegexParser(new Regex(RX), This.Value);
        }


        public static RegexParser AsRegex(this ParseValue This, RegexOptions Options = RegexOptions.None) {
            return new RegexParser(This.Value, Options);
        }

        public static RegexMatchParser AsRegexMatches(this ParseValue This, Regex RX) {
            return new RegexMatchParser(RX, This.Value);
        }

        public static RegexMatchParser AsRegexMatches(this ParseValue This, string RX) {
            return new RegexMatchParser(new Regex(RX), This.Value);
        }

        public static RegexValuesParser AsRegexValues(this ParseValue This, Regex RX) {
            return new RegexValuesParser(RX, This.Value);
        }

        public static RegexValuesParser AsRegexValues(this ParseValue This, string RX) {
            return new RegexValuesParser(new Regex(RX), This.Value);
        }

        public static EmailAddressParser AsEmails(this ParseValue This) {
            return new EmailAddressParser(This.Value);
        }

        public static PhoneNumberParser AsPhoneNumber(this ParseValue This) {
            return new PhoneNumberParser(This.Value);
        }

        public static UriValueParser AsUri(this ParseValue This) {
            return new UriValueParser(This.Value);
        }

        public static MemoryStreamParser AsMemoryStream(this ParseValue This, Encoding? Encoding = default) {
            return new MemoryStreamParser(Encoding ?? Encoding.Default, This.Value);
        }

        public static PathParser AsPath(this ParseValue This) {
            return PathParser.FromPath(This.Value);
        }



    }


}
