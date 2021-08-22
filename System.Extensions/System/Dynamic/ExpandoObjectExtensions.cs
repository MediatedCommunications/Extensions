using System.Collections;
using System.Collections.Generic;

namespace System.Dynamic
{

    public static class ExpandoObjectExtensions {


        public static ExpandoObject AddProperty(this ExpandoObject This, string PropertyPath, object? Value, bool Condition) {
            if (Condition) {
                This.AddProperty(PropertyPath, Value: Value);
            }

            return This;
        }

        public static ExpandoObject AddProperty(this ExpandoObject This, string PropertyPath, object? Value) {
            return This.AddProperty(PropertyPath.SplitDot(), Value);
        }

        public static ExpandoObject AddProperty(this ExpandoObject This, string[] PropertyPath, object? Value) {
            var D = This as IDictionary<string, object?>;

            for (var i = 0; i < PropertyPath.Length - 1; i++) {

                if (D.TryGetValue(PropertyPath[i], out var PropVal) && PropVal is ExpandoObject V1) {
                    D = V1;
                } else {
                    var NewValue = new ExpandoObject();
                    D[PropertyPath[i]] = NewValue;
                    D = NewValue;
                }
            }

            D[PropertyPath[^1]] = Value;

            return This;
        }

        public static ExpandoObject Merge(params ExpandoObject[] This) {
            return Merge((IEnumerable<ExpandoObject>)This);
        }

        public static ExpandoObject Merge(this IEnumerable<ExpandoObject?>? This) {
            var ret = new ExpandoObject();

            var Values = This.Coalesce().WhereIsNotNull().ToLinkedList();

            foreach (var Value in Values) {
                MergeInto(ret, Value);
            }


            return ret;
        }

        private static void MergeInto(IDictionary<string, object?> Target, IDictionary<string, object?> Source) {
            foreach (var item in Source) {
                if(item.Value is IDictionary<string, object?> SourceChild) {

                    if (Target.TryGetValue(item.Key, out var V1) && V1 is IDictionary<string, object?> TargetChild) {
                        MergeInto(TargetChild, SourceChild);
                    } else {
                        var NewChild = new ExpandoObject();
                        MergeInto(NewChild, SourceChild);
                        Target[item.Key] = NewChild;
                    }

                } else {
                    Target[item.Key] = item.Value;
                }
            }
        }



        public static ExpandoObject Merge(this ExpandoObject This, params object?[] Values) {
            return Merge(This, (IEnumerable<object?>)Values);
        }

        public static ExpandoObject Merge(this ExpandoObject This, IEnumerable<object?>? Values) {
            var ret = new ExpandoObject();
            MergeInto(ret, This);

            foreach (var item in Values.Coalesce().WhereIsNotNull()) {
                var Converted = ConvertToExpandoObject(item);
                MergeInto(ret, Converted);
            }

            return ret;
        }


        private static List<object> ConvertToExpandoObjectArray(IEnumerable Values) {
            var ret = new List<object>();
            foreach (var Value in Values) {
                
                if(Value is { } V1) {
                    var Type = V1.GetType();
                    if(Type.IsPrimitive || Type == typeof(string)) {
                        ret.Add(Value);
                    } else {
                        var Converted = ConvertToExpandoObject(V1);
                        ret.Add(Converted);
                    }

                } else {
                    ret.Add(Value);
                }
            }


            return ret;
        }

        private static ExpandoObject ConvertToExpandoObject(object Item) {
            var ret = new ExpandoObject();
            var ret1 = (IDictionary<string, object?>)ret;

            if(Item is IDictionary<string, object?> V1) {
                MergeInto(ret, V1);
            } else {
                var Properties = Item.GetType().GetProperties();
                foreach (var Property in Properties) {
                    
                    var Name = Property.Name;
                    var Value = Property.GetValue(Item);
                    
                    if(Value is { }) {
                        var Type = Value.GetType();

                        if (Type.IsPrimitive || Type == typeof(string)) {
                            ret1[Name] = Value;

                        } else if (Value is IDictionary Dictionary) {
                            ret1[Name] = ConvertToExpandoObject(Dictionary);

                        } else if (Value is IEnumerable Array) {
                            ret1[Name] = ConvertToExpandoObjectArray(Array);

                        } else {
                            ret1[Name] = ConvertToExpandoObject(Value);
                        }
                        
                    }

                }

            }



            return ret;
        }

    }
}
