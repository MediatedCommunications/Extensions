using System.Collections.Generic;

namespace System.Diagnostics {
    [DebuggerDisplay(DisplayBuilder.DebuggerDisplay)]
    public class DisplayBuilderRegion : IGetDebuggerDisplay {
        public List<string> Values { get; private set; } = new List<string>();
        protected DisplayBuilder Parent { get; private set; }
        protected string Format { get; private set; }

        public DisplayBuilderRegion(string Format, DisplayBuilder Parent) {
            this.Format = Format;
            this.Parent = Parent;
        }

        public DisplayBuilder Add(params object?[] Values) {
            foreach (var Value in Values) {
                Add(Value);
            }

            return Parent;
        }

        public DisplayBuilder Add(object? Value) {
            if (Value is { } V1 && StringValue(V1) is { } V2) {
                var V3 = V2.Trim();
                if (V3.IsNotBlank()) {
                    Values.Add(V3);
                }
            }


            return Parent;
        }

        public DisplayBuilder Add<T>() {
            return Add(typeof(T));
        }

        public DisplayBuilder AddIf(bool Condition, object? True, object? False) {
            if (Condition) {
                Add(True);
            } else {
                Add(False);
            }

            return Parent;
        }

        public DisplayBuilder AddIf(bool Condition, object? True) {
            return AddIf(Condition, True, default);
        }




        private static string? StringValue(object? Value) {
            var ret = default(string?);

            if (Value is IGetDebuggerDisplay V1) {
                ret = V1.GetDebuggerDisplay();

            } else if (Value is Type V2) {
                ret = V2.GetFriendlyName();
            } else if(Value is decimal V3) {
                ret = V3.Format().AsCurrency();
            } else if (Value is { } V9) {
                ret = V9.ToString();
            }

            ret = ret.TrimSafe();

            return ret;
        }

        public string GetDebuggerDisplay() {
            var ret = string.Empty;
            var V = Values.JoinSeparator();
            if(V.Length > 0) {
                ret = string.Format(Format, V);
            }

            return ret;
        }

        public override string ToString() {
            return GetDebuggerDisplay();
        }

    }

}
