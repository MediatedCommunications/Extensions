using System.Collections.Generic;
using System.Linq;

namespace System {


    public static class StringExtensions {

       


        public static string ToStringSafe(this object? This) {
            var ret = (This?.ToString()).Coalesce();
            
            return ret;
        }

        public static string SafeSubstring(this string This, Range Range) {
            var StartAt = Range.Start.GetOffset(This.Length);
            var EndAt = Range.End.GetOffset(This.Length);

            var Length = EndAt - StartAt;

            return SafeSubstring(This, StartAt, Length);
        }

        public static string SafeSubstring(this string This, int StartIndex, int? Length = default) {

            var ActualStartIndex = StartIndex;
            if (ActualStartIndex < 0) {
                ActualStartIndex = 0;
            }
            if (ActualStartIndex > This.Length) {
                ActualStartIndex = This.Length;
            }

            var MaxLength = Length ?? This.Length;

            if (ActualStartIndex + MaxLength > This.Length) {
                MaxLength = This.Length - ActualStartIndex;
            }

            if (MaxLength < 0) {
                MaxLength = 0;
            }

            var ret = This.Substring(ActualStartIndex, MaxLength);

            return ret;
        }

        public static string Coalesce(this IEnumerable<string?>? This) {
            var ret = EnumerableExtensions.EmptyIfNull(This)
                .WhereIsNotNull()
                .FirstOrDefault(Strings.Empty)
                ;

            return ret;
        }


        public static string Coalesce(this string? This, params string?[] Values) {
            var ret = This is { } V1
                ? V1
                : Values.Coalesce()
                ;

            return ret;
        }

        public static string TrimSafe(this string? This) {
            var ret = This
                .Coalesce()
                .Trim()
                ;

            return ret;
        }


        private delegate bool TrimMethod(string Element, ref string Input);

        private static string TrimInternal(this string? This, IEnumerable<string?>? ItemsToTrim, TrimMethod Method) {
            var ret = This.Coalesce();
            while (true) {
                var Changed = false;

                foreach (var item in ItemsToTrim.EmptyIfNull<string?>().WhereIsNotNullOrEmpty()) {

                    if (Method(item, ref ret)) {
                        Changed = true;
                    }
                }

                if (!Changed) {
                    break;
                }

            }

            return ret;
        }


        public static string TrimStart(this string? This, IEnumerable<string?>? ItemsToTrim = default, StringComparison Comparison = default) {
            bool TrimMethod(string Element, ref string Input) {
                var ret = false;
                while (Input.StartsWith(Element, Comparison)) {
                    Input = Input[Element.Length..];
                    ret = true;
                }

                return ret;
            }

            return TrimInternal(This, ItemsToTrim, TrimMethod);
        }

        public static string TrimEnd(this string? This, IEnumerable<string?>? ItemsToTrim = default, StringComparison Comparison = default) {
            bool TrimMethod(string Element, ref string Input) {
                var ret = false;
                while (Input.EndsWith(Element, Comparison)) {
                    Input = Input[..^Element.Length];
                    ret = true;
                }

                return ret;
            }

            return TrimInternal(This, ItemsToTrim, TrimMethod);
        }

        public static string Trim(this string? This, IEnumerable<string?>? ItemsToTrim = default, StringComparison Comparison = default) {
            return This
                .TrimStart(ItemsToTrim, Comparison)
                .TrimEnd(ItemsToTrim, Comparison)
                ;

        }

        public static string Replace(this string? This, RegionalizedString Find, string Replace) {
            var tret = This?.Replace(Find.Value.Coalesce(), Replace, Find.Comparison);
            var ret = tret.Coalesce();
            return ret;
        }

        public static string Replace(this string? This, Func<string, string> Adjuster) {
            return Replace(This, (x, y) => Adjuster(y));
        }

        public static string Replace(this string? This, Func<long, string, string> Adjuster) {
            var ret = This.Coalesce();

            var Continue = true;
            var Index = 0;
            while (Continue) {
                var NewValue = Adjuster(Index, ret);
                if (NewValue != ret) {
                    ret = NewValue;
                    Index += 1;
                } else {
                    break;
                }
            }
            return ret;
        }

        public static string AsSingleLine(this string? This) {
            return This.Replace(x => x
                .TrimSafe()
                .Replace("  ", " ")
                .Replace(Strings.CR, " ")
                .Replace(Strings.LF, " ")
                .Replace(Strings.Tab, " ")
            );
        }

        public static string WithoutDuplicateSpaces(this string? This) {
            return This.Replace(x => x
                .TrimSafe()
                .Replace("  ", " ")
                .Replace(Strings.Tab, " ")
            );
        }

     
        public static IEnumerable<Tuple<string, int>> LastIndexOfAny(this string This, IEnumerable<string> Values) {
            var ToFind = new List<Tuple<string, int>>();

            var LastResult = int.MaxValue;

            var ActualValues = Values.OrderByDescending(x => x.Length).Distinct().ToList();
            foreach (var Value in ActualValues) {
                var NewTuple = Tuple.Create(Value, LastResult);
                ToFind.Add(NewTuple);
            }

            while (ToFind.Count > 0) {
                for (var i = ToFind.Count - 1; i >= 0; i--) {
                    var (Key, Index) = ToFind[i];
                    var NewIndex = Index;

                    if (Index >= LastResult) {
                        var ShouldRemove = false;
                        var StartSearchAt = Math.Min(This.Length, LastResult - 1);

                        if (StartSearchAt < 0) {
                            ShouldRemove = true;
                        } else {
                            NewIndex = This.LastIndexOf(Key, StartSearchAt);

                            if(NewIndex < 0) {
                                ShouldRemove = true;
                            }
                        }

                        if (ShouldRemove) {
                            ToFind.RemoveAt(i);
                        } else {
                            ToFind[i] = Tuple.Create(Key, NewIndex);
                        }

                    }
                    
                }

                if (ToFind.Count > 0) {
                    var Best = (
                        from x in ToFind
                        orderby x.Item1.Length + x.Item2 descending, x.Item1.Length descending
                        select x
                        ).First();

                    yield return Best;

                    LastResult = Best.Item2;
                        
                }
            }

        }

    }

}
