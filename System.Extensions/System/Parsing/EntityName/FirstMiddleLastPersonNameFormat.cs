using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System {
    public class FirstMiddleLastPersonNameFormat : PersonNameFormat {

        public override string FormatName(PersonName Name, PersonNameFields Fields) {
            var First = new List<string>() {
                Fields.HasFlag(PersonNameFields.Prefix) ? Name.Prefix : string.Empty,
                Fields.HasFlag(PersonNameFields.FirstName) ? Name.First: string.Empty,
                Fields.HasFlag(PersonNameFields.MiddleName) ? Name.Middle: Array.Empty<string>(),
                Fields.HasFlag(PersonNameFields.LastName) ? Name.Last: string.Empty,
            }.WhereIsNotBlank().JoinSpace();

            var Second = new List<string>() {
                First,
                Fields.HasFlag(PersonNameFields.Suffix) ? Name.Suffix : string.Empty,
            }.WhereIsNotBlank().Join(", ");

            var ret = Second;

            return ret;
        }


        public override bool TryParse(string Input, [NotNullWhen(true)] out PersonName? Name) {
            var ret = false;
            Name = default;

            var Sections = Input.SplitComma()
                .Select(x => x.SplitSpace())
                .ToArray()
                ;
            var Parts = Sections.SelectMany().ToList();
            var IsValidInput = false
                || (Parts.Count >= 2 && Sections.Length == 1)
                || (Parts.Count >= 2 && Sections.Length == 2 && Sections[0].Length >= 2 && Sections[1].Length == 1)
                ;


            
            if (IsValidInput) {

                var Prefix = string.Empty;
                var Suffix = string.Empty;

                {
                    var FirstOnlyLetters = Parts.FirstOrDefault().Coalesce().WhereIs(CharType.Letter).Join();
                    if (PersonPrefixes.TryGetValue(FirstOnlyLetters, out var NewPrefix)) {
                        Prefix = NewPrefix;
                        Parts.GetConsumingEnumerableFirst().FirstOrDefault();
                    }
                }

                {
                    var LastOnlyLetters = Parts.LastOrDefault().Coalesce().WhereIs(CharType.Letter).Join();
                    if (PersonSuffixes.TryGetValue(LastOnlyLetters, out var NewSuffix)) {
                        Suffix = NewSuffix;
                        Parts.GetConsumingEnumerableLast().FirstOrDefault();
                    }
                }

                var First = Parts.GetConsumingEnumerableFirst().Coalesce();
                var Last = Parts.GetConsumingEnumerableLast().Coalesce();
                var Middle = Parts.GetConsumingEnumerableFirst().ToImmutableArray();

                var tret = new PersonName() {
                    Prefix = Prefix,
                    First = First,
                    Middle = Middle,
                    Last = Last,
                    Suffix = Suffix,
                };

                if (IsValid(tret)) {
                    Name = tret;
                    ret = true;
                }

            }



            return ret;
        }

    }

}
