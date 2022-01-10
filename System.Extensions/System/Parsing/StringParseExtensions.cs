using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace System {
    public static class StringParseExtensions { 

        public static ParseValue Parse(this string? This) {
            return new ParseValue(This);
        }

        public static StringValueParser AsString(this ParseValue This) {
            return new StringValueParser() {
                Input = This.Value,
            };
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

        public static KeyValueNamedCaptureRegexParser AsKeyValueRegexMatches(this ParseValue This, Regex RX) {
            return new KeyValueNamedCaptureRegexParser() {
                Input = This.Value,
                Regex = RX,
            };
        }

        public static KeyValueNamedCaptureRegexParser AsKeyValueRegexMatches(this ParseValue This, string RX, RegexOptions? Options = default) {
            return new KeyValueNamedCaptureRegexParser() {
                Input = This.Value,
                Regex = new Regex(RX, Options ?? RegularExpressions.Options),
            };
        }

        public static ValueNamedCaptureRegexParser AsValueRegexMatches(this ParseValue This, Regex RX) {
            return new ValueNamedCaptureRegexParser() {
                Input = This.Value,
                Regex = RX,
            };
        }

        public static ValueNamedCaptureRegexParser AsValueRegexMatches(this ParseValue This, string RX, RegexOptions? Options = default) {
            return new ValueNamedCaptureRegexParser() {
                Input = This.Value,
                Regex = new Regex(RX, Options ?? RegularExpressions.Options),
            };
        }


        public static RegexParser AsRegex(this ParseValue This, RegexOptions? Options = default) {
            return new RegexParser() {
                Input = This.Value,
                Options = Options ?? RegularExpressions.Options,
            };
        }

        public static RegexMatchParser AsRegexMatches(this ParseValue This, Regex RX) {
            return new RegexMatchParser() {
                Input = This.Value,
                Regex = RX,
            };
        }

        public static RegexMatchParser AsRegexMatches(this ParseValue This, string RX, RegexOptions? Options = default) {
            return new RegexMatchParser() {
                Input = This.Value,
                Regex = new Regex(RX, Options ?? RegularExpressions.Options),
            };
        }

        public static RegexValuesParser AsRegexValues(this ParseValue This, Regex RX) {
            return new RegexValuesParser() {
                Input = This.Value,
                Regex = RX
            };
        }

        public static RegexValuesParser AsRegexValues(this ParseValue This, string RX, RegexOptions? Options = default) {
            return new RegexValuesParser() {
                Input = This.Value,
                Regex = new Regex(RX, Options ?? RegularExpressions.Options),
            };
        }

        public static EmailAddressParser AsEmails(this ParseValue This) {
            return new EmailAddressParser() {
                Input = This.Value,
            };
        }

        public static PhoneNumberParser AsPhoneNumber(this ParseValue This) {
            return new PhoneNumberParser() {
                Input = This.Value
            };
        }

        public static EntityNameParser AsName(this ParseValue This, IEnumerable<PersonNameFormat>? Formats = default) {
            return new EntityNameParser() {
                Input = This.Value,
                Formats = (Formats ?? PersonNameFormats.All).ToImmutableArray()
            };
        }

        public static UriValueParser AsUri(this ParseValue This) {
            return new UriValueParser() {
                Input = This.Value,
            };
        }

        public static MemoryStreamParser AsMemoryStream(this ParseValue This, Encoding? Encoding = default) {
            return new MemoryStreamParser() {
                Input = This.Value,
                Encoding = Encoding ?? Encoding.Default,
            };
        }

        public static PathParser AsPath(this ParseValue This) {
            return PathParser.FromPath(This.Value);
        }



    }


}
