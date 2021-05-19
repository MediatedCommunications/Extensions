using System.Diagnostics;

namespace System {
    public record RegionalizedString : DisplayRecord {
        public string? Value { get; init; }
        public StringComparison Comparison { get; init; } = StringComparison.CurrentCulture;

        public static implicit operator string(RegionalizedString R) => R.Value.Coalesce();

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Value)
                ;
        }

        public static bool operator==(RegionalizedString R, string S) {
            return R.Equals(S);
        }

        public static bool operator!=(RegionalizedString R, string S) {
            return !R.Equals(S);
        }

        public static bool operator ==(string S, RegionalizedString R) {
            return R.Equals(S);
        }

        public static bool operator !=(string S, RegionalizedString R) {
            return !R.Equals(S);
        }

        private static bool TextOperation(string? This, string?[] Values, Func<string, string, bool> Condition) {
            var ret = false;

            foreach (var Value in Values) {
                if (This is { } V1 && Value is { } V2 && Condition(V1, V2)) {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        public bool StartsWith(params string?[] CompareTo) {
            return TextOperation(Value, CompareTo, (x, y) => x.StartsWith(y, Comparison));
        }

        public bool EndsWith(params string?[] CompareTo) {
            return TextOperation(Value, CompareTo, (x, y) => x.EndsWith(y, Comparison));
        }

        public bool Contains(params string?[] CompareTo) {
            return TextOperation(Value, CompareTo, (x, y) => x.Contains(y, Comparison));
        }

        public bool Equals(params string?[] CompareTo) {
            return TextOperation(Value, CompareTo, (x, y) => x.Equals(y, Comparison));
        }


    }

}
