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

        public static GuidValueParser AsGuid(this ParseValue This) {
            return new GuidValueParser(This.Value);
        }

        public static BoolValueParser AsBool(this ParseValue This) {
            return new BoolValueParser(This.Value);
        }

        public static IntValueParser AsInt(this ParseValue This) {
            return new IntValueParser(This.Value);
        }

        public static LongValueParser AsLong(this ParseValue This) {
            return new LongValueParser(This.Value);
        }

        public static FloatValueParser AsFloat(this ParseValue This) {
            return new FloatValueParser(This.Value);
        }

        public static DecimalValueParser AsDecimal(this ParseValue This) {
            return new DecimalValueParser(This.Value);
        }

        public static DoubleValueParser AsDouble(this ParseValue This) {
            return new DoubleValueParser(This.Value);
        }

        public static EnumValueParser<T> AsEnum<T>(this ParseValue This) where T : struct {
            return new EnumValueParser<T>(This.Value);
        }

        public static TimeSpanValueParser AsTimeSpan(this ParseValue This, TimeSpanFormat Format = TimeSpanFormat.TimeSpan) {
            return new TimeSpanValueParser(Format, This.Value);
        }

        public static DateTimeValueParser AsDateTime(this ParseValue This) {
            return new DateTimeValueParser(This.Value);
        }

        public static DateTimeOffsetValueParser AsDateTimeOffset(this ParseValue This, DateTimeStyles Style = default, IFormatProvider? FormatProvider = default) {
            return new DateTimeOffsetValueParser(This.Value, Style, FormatProvider);
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
