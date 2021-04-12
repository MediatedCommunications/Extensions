using System.Linq;
using System.Text.RegularExpressions;

namespace System.Text.Json.Serialization {
    public class DateFunctionEpochConverter : JsonConverter<DateTimeOffset> {
        const string Value = "VALUE";
        static Regex RX = new Regex($@"Date \((?<{Value}> (-)? \d+  )\)", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);

        public override bool HandleNull {
            get {
                return base.HandleNull;
            }
        }

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            var ret = default(DateTimeOffset);

            var UseDefault = true;

            if(reader.GetString() is string V1) {

                if (V1.Parse().AsRegexMatches(RX).FirstOrDefault() is { } M && M.Groups[Value].Value.Parse().AsLong().TryGetValue(out var MS)) {
                    ret = DateTimeOffset.FromUnixTimeMilliseconds(MS);
                    UseDefault = false;
                } else if (V1.Parse().AsDateTimeOffset(Globalization.DateTimeStyles.AssumeUniversal).TryGetValue(out var SuccessValue)) {
                    ret = SuccessValue;
                    UseDefault = false;
                }
            }

            if (UseDefault) { 
                var tret = JsonSerializer.Deserialize(ref reader, typeToConvert);

                if(tret is DateTimeOffset value) {
                    ret = value;
                }
            }

            return ret;
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options) {
            var V = value.ToUnixTimeMilliseconds();
            var ValueToWrite = $@"/Date({V})/";
            writer.WriteStringValue(ValueToWrite);
        }
    }
}
