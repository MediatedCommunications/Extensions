using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace System
{

    public static class StringExtensions {

        public const string Space = " ";
        public const string Dot = ".";
        public const string Comma = ",";
        public const string Dash = "-";
        public const string Underscore = "_";
        public const string Separator = " - ";
        public static readonly ImmutableArray<string> Dots = new[] { Dot }.ToImmutableArray();

        public const string SlashWindows = @"\";
        public const string SlashUnix = @"/";
        public static readonly ImmutableArray<string> Slashes = new[] { SlashWindows, SlashUnix }.ToImmutableArray();

        public const string CR = "\n";
        public const string LF = "\r";
        public const string CRLF = CR + LF;
        public const string TAB = "\t";

        public const string Null = "\0";
        public static readonly ImmutableArray<string> NewLines = new[] { CRLF, CR, LF }.ToImmutableArray();


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
            if(ActualStartIndex < 0) {
                ActualStartIndex = 0;
            }
            if(ActualStartIndex > This.Length) {
                ActualStartIndex = This.Length;
            }

            var MaxLength = Length ?? This.Length;

            if(ActualStartIndex + MaxLength > This.Length) {
                MaxLength = This.Length - ActualStartIndex;
            }

            if(MaxLength < 0) {
                MaxLength = 0;
            }

            var ret = This.Substring(ActualStartIndex, MaxLength);
            
            return ret;
        }

        public static string Coalesce(this IEnumerable<string?>? This) {
            var ret = EnumerableExtensions.Coalesce(This)
                .WhereIsNotNull()
                .FirstOrDefault(string.Empty)
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

                foreach (var item in ItemsToTrim.Coalesce<string?>().WhereIsNotNullOrEmpty()) {

                    if(Method(item, ref ret)) {
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
                while(Input.StartsWith(Element, Comparison)){
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
                while(Input.EndsWith(Element, Comparison)) {
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
                .Replace(CR, " ")
                .Replace(LF, " ")
                .Replace(TAB, " ")
            );
        }

        public static string WithoutDuplicateSpaces(this string? This) {
            return This.Replace(x => x
                .TrimSafe()
                .Replace("  ", " ")
                .Replace(TAB, " ")
            );
        }


    }

}
