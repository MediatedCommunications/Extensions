using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace System {
    public abstract record EntityName : DisplayRecord {
    
        public static ImmutableHashSet<string> CompanyTypes { get; }
        public static ImmutableHashSet<string> CompanyWords { get; }
        
        public abstract string Full { get; }

        static EntityName() {
            CompanyTypes = new[] {
                "AG",
                "Co",
                "Corp",
                "DBA",
                "Inc",
                "LLC",
                "LLP",
                "LTD",
                "PA",
            }.ToImmutableHashSet(StringComparer.InvariantCultureIgnoreCase);

            CompanyWords = new[] {
                "a", "an", "the",
                
                "and", "but", "or",
                "of",
                "district",
                "court",
                
            }.ToImmutableHashSet(StringComparer.InvariantCultureIgnoreCase);




        }
        
        private static bool IsCompanyName(string Input) {
            var ret = false;

            var Parts = (
                from x in Input.Split(new string[] { " ", "," })
                let y = x.WhereIs(CharType.LetterOrDigit).Join()
                select y
                ).WhereIsNotBlank().ToList();


            if(ret == false) {
                //If we have any company words
                ret = Parts
                    .Where(x => CompanyWords.Contains(x))
                    .Any()
                    ;
            }

            if (ret == false) {
                //If we have any company types
                ret = Parts
                    .Where(x => CompanyTypes.Contains(x))
                    .Any()
                    ;
            }

            if (ret == false) {
                //If we have any punctuations that aren't person punctuations
                ret = Input
                    .WhereIs(CharType.Punctuation)
                    .Where(x => !PersonNameFormat.PersonPunctuations.Contains(x))
                    .Any()
                    ;
            }

            if (ret == false) {
                //If we have any punctuations that aren't person punctuations
                ret = Input
                    .WhereIs(CharType.DigitOrNumber)
                    .Any()
                    ;
            }


            return ret;
        }

        private static CompanyName ParseCompany(string Input) {
            var ret = new CompanyName() {
                Name = Input,
            };

            return ret;
        }

        public static EntityName Parse(string? Input, IEnumerable<PersonNameFormat>? Formats = default) {
            Formats ??= PersonNameFormats.All;

            var ret = default(EntityName?);
            Input = Input.TrimSafe();

            if (Input.IsBlank()) {
                ret = EntityNames.Empty;
            } else {

                var IsCompany = IsCompanyName(Input);

                if (IsCompany) {
                    ret = ParseCompany(Input);
                } else if(Formats.TryParse(Input, out var tret)){
                    ret = tret;
                } else {
                    ret = ParseCompany(Input);
                }
            }
            return ret;

        }

    }

}
