using System.Collections.Generic;
using System.Collections.Immutable;

namespace System {
    public static class KeyValueFields {
        public static string Key { get; }
        public static string Value { get; }
        public static string Value1 { get; }
        public static string Value2 { get; }
        public static string Value3 { get; }
        public static string Value4 { get; }
        public static string Value5 { get; }

        public static ImmutableList<string> Values1 { get; }
        public static ImmutableList<string> Values2 { get; }
        public static ImmutableList<string> Values3 { get; }
        public static ImmutableList<string> Values4 { get; }
        public static ImmutableList<string> Values5 { get; }

        public static ImmutableList<string> Values1_2 { get; }
        public static ImmutableList<string> Values1_3 { get; }
        public static ImmutableList<string> Values1_4 { get; }
        public static ImmutableList<string> Values1_5 { get; }

        static KeyValueFields() {
            Key = nameof(KeyValuePair<string, string>.Key);
            Value = nameof(KeyValuePair<string, string>.Value);

            Value1 = ValueIndex(1);
            Value2 = ValueIndex(2);
            Value3 = ValueIndex(3);
            Value4 = ValueIndex(4);
            Value5 = ValueIndex(5);

            Values1 = new[] { Value1 }.ToImmutableList();
            Values2 = new[] { Value2 }.ToImmutableList();
            Values3 = new[] { Value3 }.ToImmutableList();
            Values4 = new[] { Value4 }.ToImmutableList();
            Values5 = new[] { Value5 }.ToImmutableList();

            Values1_2 = Values(2).ToImmutableList();
            Values1_3 = Values(3).ToImmutableList();
            Values1_4 = Values(4).ToImmutableList();
            Values1_5 = Values(5).ToImmutableList();

        }

        public static string ValueIndex(int Index) {
            return $@"{Value}{Index}";
        }

        public static IEnumerable<string> Values(int Count) {
            for (var i = 1; i <= Count; i++) {
                yield return ValueIndex(i);
            }
        }
    }

}
