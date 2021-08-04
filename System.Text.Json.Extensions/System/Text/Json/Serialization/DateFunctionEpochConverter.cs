﻿using System.Linq;
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

        public static bool TryParse(string Input, out DateTimeOffset Output) {
            var tret = default(DateTimeOffset);
            var Success = false;

            if (Input.Parse().AsRegexMatches(RX).FirstOrDefault() is { } M && M.Groups[Value].Value.Parse().AsLong().TryGetValue(out var MS)) {
                tret = DateTimeOffset.FromUnixTimeMilliseconds(MS);
                Success = true;
            } else if (Input.Parse().AsDateTimeOffset(Globalization.DateTimeStyles.AssumeUniversal).TryGetValue(out var SuccessValue)) {
                tret = SuccessValue;
                Success = true;
            }

            Output = tret;
            return Success;
        }

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            var ret = default(DateTimeOffset);

            var UseDefault = true;

            if(reader.GetString() is string V1) {

                if(TryParse(V1, out ret)) {
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
            var ValueToWrite = Write(value);
            writer.WriteStringValue(ValueToWrite);
        }

        public static string Write(DateTimeOffset value) {
            var V = value.ToUnixTimeMilliseconds();
            var ret = $@"/Date({V})/";

            return ret;
        }

    }
}
