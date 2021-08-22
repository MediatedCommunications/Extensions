using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

namespace System
{
    public static class TimeZones {

        public static ImmutableDictionary<string, TimeZoneInfo> SystemTimeZones { get; private set; }

        public static ImmutableDictionary<string, TimeZoneInfo> AllAmericanTimeZones { get; private set; }
        public static ImmutableDictionary<string, TimeZoneInfo> OnlyAmericanTimeZones { get; private set; }

        private static readonly Regex DisplayNameOnlyRegex;

        static TimeZones() {
            DisplayNameOnlyRegex = new Regex(@"^\((UTC)([+-]?)((\d+)\:(\d+))?\)\W*(?<Name>.*)$", System.Text.RegularExpressions.RegularExpressions.Options);

            SystemTimeZones = CreateSystemTimeZones();
            {
                CreateAmericanTimeZones(out var V1, out var V2);
                AllAmericanTimeZones = V1;
                OnlyAmericanTimeZones = V2;
            }
        }




        private static string DisplayNameOnly(this TimeZoneInfo This) {
            var ret = This.DisplayName;
            if (ret.Parse().AsRegexMatches(DisplayNameOnlyRegex).FirstOrDefault() is { } V) {
                ret = V.Groups["Name"].Value;
            }

            return ret;
        }

        private static ImmutableDictionary<string, TimeZoneInfo> CreateSystemTimeZones() {
            var ret = ImmutableDictionary.Create<string, TimeZoneInfo>(StringComparer.InvariantCultureIgnoreCase);

            foreach (var item in TimeZoneInfo.GetSystemTimeZones()) {
                var Names = new[] { item.DaylightName, item.StandardName, item.DisplayNameOnly() };
                foreach (var Name in Names) {
                    if (Name.IsNotBlank()) {

                        ret = ret.SetItem(Name, item);
                    }
                }
            }

            return ret;
        }


        private static void CreateAmericanTimeZones(out ImmutableDictionary<string, TimeZoneInfo> AllAmericanTimeZones, out ImmutableDictionary<string, TimeZoneInfo> OnlyAmericanTimeZones) {
            //I go through these hoops incase a system does not have all the timezones installed.
            AllAmericanTimeZones = ImmutableDictionary.Create<string, TimeZoneInfo>(StringComparer.InvariantCultureIgnoreCase);
            AllAmericanTimeZones = AllAmericanTimeZones.AddRange(SystemTimeZones);

            OnlyAmericanTimeZones = ImmutableDictionary.Create<string, TimeZoneInfo>(StringComparer.InvariantCultureIgnoreCase);

            var ToMap = new SortedDictionary<string, string>();

            var PST = "Pacific Standard Time";
            var CST = "Central Standard Time";
            var MST = "Mountain Standard Time";
            var EST = "Eastern Standard Time";
            var AST = "Alaskan Standard Time";
            var HST = "Hawaiian Standard Time";

            ToMap["PST"] = PST;
            ToMap["PDT"] = PST;

            ToMap["CST"] = CST;
            ToMap["CDT"] = CST;

            ToMap["MST"] = MST;
            ToMap["MDT"] = MST;

            ToMap["EST"] = EST;
            ToMap["EDT"] = EST;

            ToMap["AST"] = AST;
            ToMap["ADT"] = AST;

            ToMap["HST"] = HST;
            ToMap["HDT"] = HST;

            foreach (var item in ToMap) {
                if (AllAmericanTimeZones.TryGetValue(item.Value, out var Value)) {
                    AllAmericanTimeZones = AllAmericanTimeZones.SetItem(item.Key, Value);
                    OnlyAmericanTimeZones = OnlyAmericanTimeZones.SetItem(item.Key, Value);
                }

            }

        }

    }
}
