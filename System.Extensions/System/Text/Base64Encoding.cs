using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace System.Text
{
    public static class Base64Encoding {
        public static ImmutableHashSet<char> ValidCharacters { get; }
        public static ImmutableHashSet<char> IgnoredCharacters { get; }

        static Base64Encoding() {
            ValidCharacters = new List<int>() {
                ('a'..'z').AsEnumerable().EndIs(RangeEndpoint.Inclusive),
                ('A'..'Z').AsEnumerable().EndIs(RangeEndpoint.Inclusive),
                ('0'..'9').AsEnumerable().EndIs(RangeEndpoint.Inclusive),
                new int[]{
                    '+', 
                    '/', 
                    '=',
                }
            }.Select(x => (char)x).ToImmutableHashSet();

            IgnoredCharacters = new[] {
                ' ',
                '\r',
                '\n', 
                '\t',
            }.ToImmutableHashSet();

        }

        public static bool IsValidCharacter(char Input) {
            var ret = Input switch {
                >= 'A' and <= 'Z' => true,
                >= 'a' and <= 'z' => true,
                >= '0' and <= '9' => true,
                '+' => true,
                '/' => true,
                '=' => true,
                _ => false
            };

            return ret;
        }

        public static bool IsIgnoredCaracter(char Input) {
            var ret = Input switch {
                ' ' => true,
                '\r' => true,
                '\n' => true,
                '\t' => true,
                _ => false,
            };

            return ret;
        }

        /// <summary>
        /// Returns a list of non-empty Base64 seconds (split by invalid characters)
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetValidSections(string Input) {
            var tret = new StringBuilder();
            
            for (var i = 0; i < Input.Length; i++) {
                var Item = Input[i];

                var ShouldReturn = false;

                if (!IsValidCharacter(Item)) {
                    if (IsIgnoredCaracter(Item)) {
                        //Do Nothing
                    } else {
                        ShouldReturn = true;
                    }
                } else {
                    tret.Append(Item);
                }

                if (ShouldReturn) {
                    var ret = tret.ToString();

                    if (ret.IsNotBlank()) {
                        yield return ret;
                    }

                    tret = new();
                }
            }

            {
                var ret = tret.ToString();

                if (ret.IsNotBlank()) {
                    yield return ret;
                }
            }



        }

        public static string RemoveInvalidCharacters(string Input) {
            var ret = StringExtensions_Join.Join((
                from x in Input
                where IsValidCharacter(x)
                select x
                ));

            return ret;
        }


        public static byte[] ConvertFromStringFormatted(string Input) {
            var SafeInput = RemoveInvalidCharacters(Input);
            var ret = ConvertFromString(SafeInput);
            return ret;
        }

        public static byte[] ConvertFromString(string Input) {
            var ret = Convert.FromBase64String(Input);
            return ret;
        }

        public static string ConvertToStringFormatted(byte[] Input, int LineLength = 64) {
            var Values = new List<string>();
            var RawValue = ConvertToString(Input);
            var Remaining = RawValue;
            while(Remaining.Length > 0) {
                var Part = Remaining.SafeSubstring(0..LineLength);
                Part += new string(' ', LineLength - Part.Length);

                Remaining = Remaining.SafeSubstring(LineLength..);

                Values.Add(Part);
            }

            var ret = Values.JoinLine();
            return ret;
        }

        public static string ConvertToString(byte[] Input) {
            var ret = Convert.ToBase64String(Input);
            return ret;
        }


    }
}
