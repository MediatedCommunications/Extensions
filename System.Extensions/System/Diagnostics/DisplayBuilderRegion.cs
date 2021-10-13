using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{

    [DebuggerDisplay(DisplayBuilder.DebuggerDisplay)]
    public class DisplayBuilderRegion : IGetDebuggerDisplay {
        public List<string> Values { get; private set; } = new List<string>();
        protected DisplayBuilder Parent { get; private set; }
        protected string Format { get; private set; }

        public DisplayBuilderRegion(string Format, DisplayBuilder Parent) {
            this.Format = Format;
            this.Parent = Parent;
        }

        public virtual DisplayBuilderRegion If(bool Condition) {
            var ret = Condition
                ? this
                : new DisplayBuilderRegion(Format, Parent)
                ;

            return ret;
        }



        public DisplayBuilder AddPhrase(params object?[] Values) {
            var Phrase = Values.Select(x => StringValue(x).TrimSafe())
                .WhereIsNotBlank()
                .JoinSpace()
                ;

            Add(Phrase);

            return Parent;
        }

        public DisplayBuilder Add(params object?[] Values) {
            foreach (var Value in Values)
            {
                Add(Value);
            }

            return Parent;
        }

        public DisplayBuilder Add<T>(ICollection<T>? Values) {
            foreach (var Value in Values.Coalesce())
            {
                Add(Value);
            }

            return Parent;
        }

        private DisplayBuilder Add(ICollection? Values) {
            if (Values is { }) {
                foreach (var Value in Values) {
                    Add(Value);
                }
            }

            return Parent;
        }


        public DisplayBuilder AddCount(long Count, [CallerArgumentExpression("Count")] string? Name = default) {

            var ActualCount = Count;
            
            var ActualName = new[] {
                Name,
                "Items"
            }.WhereIsNotBlank().Coalesce();

            return Add($@"{ActualCount} {ActualName}");
        }

        public DisplayBuilder AddCount<T>(IEnumerable<T> Collection, [CallerArgumentExpression("Collection")] string? Name = default)
        {
            var ActualCount = Collection.Count();

            return AddCount(ActualCount, Name);
        }

        public DisplayBuilder AddNameValue<TValue>(IEnumerable<KeyValuePair<string, TValue>>? Values) {
            foreach (var item in Values.Coalesce()) {
                AddNameValue(item.Key, item.Value);
            }

            return Parent;
        }


        public DisplayBuilder AddNameValue(string? Name, object? Value) {
            return AddExpression(Value, Name);
        }

        public DisplayBuilder AddExpression(object? Value, [CallerArgumentExpression("Value")] string? Name = default) {

            if (Name.IsBlank()) {
                Add(Value);

            } else if (Value is { } V1 && StringValue(V1) is { } V2) {
                Add($@"{Name}: {V2}");

            }
            
            return Parent;
        }

        public DisplayBuilder AddMapping(object? Before, object? After) {
            var V1 = StringValue(Before);
            var V2 = StringValue(After);
            
            Add($@"'{V1}' => '{V2}'");

            return Parent;
        }

        public DisplayBuilder Add(object? Value) {
            if (Value is { } V1){

                if(V1 is ICollection IC) {
                    Add(IC);

                } else if (StringValue(V1) is { } V2) {
                    var V3 = V2.Trim();
                    if (V3.IsNotBlank()) {
                        Values.Add(V3);
                    }
                }
            }


            return Parent;
        }

        public DisplayBuilder Add<T>() {
            return Add(typeof(T));
        }

        public DisplayBuilder AddCondition(bool Condition, object? True, object? False) {
            if (Condition) {
                Add(True);
            } else {
                Add(False);
            }

            return Parent;
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
