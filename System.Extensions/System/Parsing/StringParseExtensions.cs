using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace System {
    public static class Parsers {
        public static BoolParser Bool { get; }
        public static DateOnlyParser DateOnly { get; }
        public static DateTimeOffsetParser DateTimeOffset { get; }
        public static DateTimeParser DateTime { get; }
        public static DecimalParser Decimal { get; }
        public static DoubleParser Double { get; }
        public static EmailAddressParser EmailAddress { get; }
        public static EntityNameParser EntityName { get; }
        public static FloatParser Float { get; }
        public static GuidParser Guid { get; }
        public static IntParser Int { get; }
        public static KeyValueNamedCaptureParser KeyValue { get; }
        public static LongParser Long { get; }
        public static MemoryStreamParser MemoryStream { get; }
        public static PhoneNumberParser PhoneNumber { get; }
        public static RegexParser Regex { get; }
        public static RegexMatchParser RegexMatch { get; }
        public static RegexStringMatchParser RegexStringMatch { get; }
        public static StringParser StringValue { get; }
        public static StringSplitParser StringSplit { get; }
        public static TimeOnlyParser TimeOnly { get; }
        public static TimeSpanParser TimeSpan { get; }
        public static UriParser Uri { get; }

        public static AggregateValueNamedCaptureRegexParser AggregateValueNamedCapture { get; }
        public static ValueNamedCaptureParser ValueNamedCapture { get; }
        
        public static EnumParser<T> Enum<T>() where T :struct => DefaultEnum<T>.Default;

        private static class DefaultEnum<T> where T : struct{
            public static EnumParser<T> Default { get; }
            static DefaultEnum() {
                Default = new();
            }

        }

        static Parsers() {

            Bool = new();
            DateOnly = new();
            DateTimeOffset = new();
            DateTime = new();
            Decimal = new();
            Double = new();
            EmailAddress = new();
            EntityName = new();
            Float = new();
            Guid = new();
            Int = new();
            KeyValue = new();
            Long = new();
            MemoryStream = new();
            PhoneNumber = new();
            Regex = new();
            RegexMatch = new();
            RegexStringMatch = new();
            StringValue = new();
            StringSplit = new();
            TimeOnly = new();
            TimeSpan = new();
            Uri = new();

            AggregateValueNamedCapture = new();
            ValueNamedCapture = new();
        }

    }

    public static class StringParseExtensions { 

        public static ParseValue Parse(this string? This) {
            return new ParseValue(This);
        }

        public static DefaultClassParserContext<StringParser, string> AsString(this ParseValue This) {
            return DefaultClassParserContext.Create<StringParser, string>(This, Parsers.StringValue);
        }

        public static ListParserContext<StringSplitParser, string> AsSplitString(this ParseValue This) {
            return ListParserContext.Create<StringSplitParser, string>(This, Parsers.StringSplit);
        }

        public static StructParserContext<GuidParser, Guid> AsGuid(this ParseValue This) {
            return StructParserContext.Create<GuidParser, Guid>(This, Parsers.Guid);
        }

        public static StructParserContext<BoolParser, bool> AsBool(this ParseValue This) {
            return StructParserContext.Create<BoolParser, bool>(This, Parsers.Bool);
        }

        public static StructParserContext<IntParser, int> AsInt(this ParseValue This) {
            return StructParserContext.Create<IntParser, int>(This, Parsers.Int);
        }

        public static StructParserContext<LongParser, long> AsLong(this ParseValue This) {
            return StructParserContext.Create<LongParser, long>(This, Parsers.Long);
        }

        public static StructParserContext<FloatParser, float> AsFloat(this ParseValue This) {
            return StructParserContext.Create<FloatParser, float>(This, Parsers.Float);
        }

        public static StructParserContext<DecimalParser, decimal> AsDecimal(this ParseValue This) {
            return StructParserContext.Create<DecimalParser, decimal>(This, Parsers.Decimal);
        }

        public static StructParserContext<DoubleParser, double> AsDouble(this ParseValue This) {
            return StructParserContext.Create<DoubleParser, double>(This, Parsers.Double);
        }

        public static StructParserContext<EnumParser<T>, T> AsEnum<T>(this ParseValue This) where T : struct {
            return StructParserContext.Create<EnumParser<T>, T>(This, Parsers.Enum<T>());
        }

        public static StructParserContext<TimeSpanParser, TimeSpan> AsTimeSpan(this ParseValue This) {
            return StructParserContext.Create<TimeSpanParser, TimeSpan>(This, Parsers.TimeSpan);
        }

        public static StructParserContext<TimeSpanParser, TimeSpan> AsTimeSpan(this ParseValue This, TimeSpanFormat Format) {
            var Parser = Parsers.TimeSpan with
            {
                Format = Format,
            };
            return StructParserContext.Create<TimeSpanParser, TimeSpan>(This, Parser);
        }

        public static StructParserContext<DateTimeParser, DateTime> AsDateTime(this ParseValue This) {
            return StructParserContext.Create<DateTimeParser, DateTime>(This, Parsers.DateTime);
        }

        public static StructParserContext<DateOnlyParser, DateOnly> AsDateOnly(this ParseValue This) {
            return StructParserContext.Create<DateOnlyParser, DateOnly>(This, Parsers.DateOnly);
        }

        public static StructParserContext<TimeOnlyParser, TimeOnly> AsTimeOnly(this ParseValue This) {
            return StructParserContext.Create<TimeOnlyParser, TimeOnly>(This, Parsers.TimeOnly);
        }

        public static StructParserContext<DateTimeOffsetParser, DateTimeOffset> AsDateTimeOffset(this ParseValue This) {
            return StructParserContext.Create<DateTimeOffsetParser, DateTimeOffset>(This, Parsers.DateTimeOffset);
        }

        public static StructParserContext<DateTimeOffsetParser, DateTimeOffset> AsDateTimeOffset(this ParseValue This, DateTimeStyles DateTimeStyles) {
            var Parser = Parsers.DateTimeOffset with
            {
                Style = DateTimeStyles,
            };
            
            return StructParserContext.Create<DateTimeOffsetParser, DateTimeOffset>(This, Parser);
        }

        public static ListParserContext<KeyValueNamedCaptureParser, KeyValuePair<string, string>> KeyValues(this ParseValue This, Regex RX) {
            var Parser = Parsers.KeyValue with
            {
                Regex = RX,
            };

            return ListParserContext.Create<KeyValueNamedCaptureParser, KeyValuePair<string, string>>(This, Parser);

        }

        public static ListParserContext<KeyValueNamedCaptureParser, KeyValuePair<string, string>> KeyValues(this ParseValue This, string RX, RegexOptions ? Options = default) {
            var Parser = Parsers.KeyValue with
            {
                Regex = CreateRegex(RX, Options),
            };

            return ListParserContext.Create<KeyValueNamedCaptureParser, KeyValuePair<string, string>>(This, Parser);

        }

        public static ListParserContext<ValueNamedCaptureParser, string> Values(this ParseValue This, Regex RX) {
            var Parser = Parsers.ValueNamedCapture with
            {
                Regex = RX,
            };

            return ListParserContext.Create<ValueNamedCaptureParser, string>(This, Parser);

        }

        public static ListParserContext<ValueNamedCaptureParser, string> Values(this ParseValue This, string RX, RegexOptions? Options = default) {
            var Parser = Parsers.ValueNamedCapture with
            {
                Regex = CreateRegex(RX, Options),
            };

            return ListParserContext.Create<ValueNamedCaptureParser, string>(This, Parser);
        }

        public static ListParserContext<AggregateValueNamedCaptureRegexParser, string> AggregateValues(this ParseValue This, Regex RX) {
            var Parser = Parsers.AggregateValueNamedCapture with
            {
                Regex = RX,
            };

            return ListParserContext.Create<AggregateValueNamedCaptureRegexParser, string>(This, Parser);
        }

        public static ListParserContext<AggregateValueNamedCaptureRegexParser, string> AggregateValues(this ParseValue This, string RX, RegexOptions? Options = default) {
            var Parser = Parsers.AggregateValueNamedCapture with
            {
                Regex = CreateRegex(RX, Options),
            };

            return ListParserContext.Create<AggregateValueNamedCaptureRegexParser, string>(This, Parser);

        }


        public static DefaultClassParserContext<RegexParser, Regex> AsRegex(this ParseValue This) {
            return DefaultClassParserContext.Create<RegexParser, Regex>(This, Parsers.Regex);

        }

        public static ListParserContext<RegexMatchParser, Match> Matches(this ParseValue This, Regex RX) {
            var Parser = Parsers.RegexMatch with
            {
                Regex = RX,
            };

            return ListParserContext.Create<RegexMatchParser, Match>(This, Parser);

        }

        public static ListParserContext<RegexMatchParser, Match> Matches(this ParseValue This, string RX, RegexOptions? Options = default) {
            var Parser = Parsers.RegexMatch with
            {
                Regex = CreateRegex(RX, Options),
            };

            return ListParserContext.Create<RegexMatchParser, Match>(This, Parser);
        }

        public static ListParserContext<RegexStringMatchParser, string> StringMatches(this ParseValue This, Regex RX) {
            var Parser = Parsers.RegexStringMatch with
            {
                Regex = RX
            };

            return ListParserContext.Create<RegexStringMatchParser, string>(This, Parser);
        }

        private static Regex CreateRegex(string RX, RegexOptions? Options) {
            return new Regex(RX, Options ?? RegularExpressions.Options);
        }

        public static ListParserContext<RegexStringMatchParser, string> StringMatches(this ParseValue This, string RX, RegexOptions? Options = default) {
            var Parser = Parsers.RegexStringMatch with
            {
                Regex = CreateRegex(RX, Options),
            };

            return ListParserContext.Create<RegexStringMatchParser, string>(This, Parser);
        }

        public static ListParserContext<EmailAddressParser, EmailAddress> EmailAddresses(this ParseValue This) {
            return ListParserContext.Create<EmailAddressParser, EmailAddress>(This, Parsers.EmailAddress);
        }

        public static DefaultClassParserContext<PhoneNumberParser, PhoneNumber> AsPhoneNumber(this ParseValue This) {
            return DefaultClassParserContext.Create<PhoneNumberParser, PhoneNumber>(This, Parsers.PhoneNumber);
        }

        public static DefaultClassParserContext<EntityNameParser, EntityName> AsName(this ParseValue This) {
            return DefaultClassParserContext.Create<EntityNameParser, EntityName>(This, Parsers.EntityName);
        }

        public static DefaultClassParserContext<EntityNameParser, EntityName> AsName(this ParseValue This, params PersonNameFormat[] Formats) {
            var Parser = Parsers.EntityName with
            {
                Formats = Formats.ToImmutableArray(),
            };
            return DefaultClassParserContext.Create<EntityNameParser, EntityName>(This, Parser);
        }

        public static ClassParserContext<UriParser, Uri> AsUri(this ParseValue This) {
            return ClassParserContext.Create<UriParser, Uri>(This, Parsers.Uri);
        }


        public static DefaultClassParserContext<MemoryStreamParser, MemoryStream> AsMemoryStream(this ParseValue This) {
            return DefaultClassParserContext.Create<MemoryStreamParser, MemoryStream>(This, Parsers.MemoryStream);
        }

        public static DefaultClassParserContext<MemoryStreamParser, MemoryStream> AsMemoryStream(this ParseValue This, System.Text.Encoding Encoding) {
            var Parser = Parsers.MemoryStream with
            {
                Encoding = Encoding
            };
            
            return DefaultClassParserContext.Create<MemoryStreamParser, MemoryStream>(This, Parser);
        }

        public static PathParser AsPath(this ParseValue This) {
            return PathParser.FromPath(This.Value);
        }



    }


}
