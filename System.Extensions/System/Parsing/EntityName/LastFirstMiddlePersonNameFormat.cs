using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System {
    public class LastFirstMiddlePersonNameFormat : PersonNameFormat {

        public override string FormatName(PersonName Name, PersonNameFields Fields) {
            var First = new List<string>() {
                Fields.HasFlag(PersonNameFields.LastName) ? Name.Last: string.Empty,
            }.WhereIsNotBlank().JoinSpace();

            var Second = new List<string>() {
                Fields.HasFlag(PersonNameFields.Prefix) ? Name.Prefix : string.Empty,
                Fields.HasFlag(PersonNameFields.FirstName) ? Name.First: string.Empty,
                Fields.HasFlag(PersonNameFields.MiddleName) ? Name.Middle: Array.Empty<string>(),
                Fields.HasFlag(PersonNameFields.Suffix) ? Name.Suffix : string.Empty,
            }.WhereIsNotBlank().JoinSpace();


            var Third = new List<string>() {
                First,
                Second,
            }.WhereIsNotBlank().Join(", ");

            var ret = Third;

            return ret;
        }

        //Smith, John
        //Smith, Mr. John 
        //Smith, Mr John Jingle
        //Smith, Mr John Jingle III

        public override bool TryParse(string Input, [NotNullWhen(true)] out PersonName? Name) {
            var ret = false;
            Name = default;

            var Sections = Input.SplitComma()
                .Select(x => x.SplitSpace())
                .ToArray()
                ;
            var Parts = Sections.SelectMany().ToList();
            var IsValidInput = false
                || (Parts.Count >= 2 && Sections[0].Length == 1)
                ;

            if (IsValidInput) {
                var Order = new List<string>() {
                    Sections[1..],
                    Sections[0]
                };

                var Prefix = string.Empty;
                var Suffix = string.Empty;

                var NameParts = (from x in Order from y in x.Split(new[] { " ", "," }) select y).WhereIsNotBlank().ToList();

                for (var i = NameParts.Count - 1; i >= 0; i--) {
                    var OnlyLetters = NameParts[i].WhereIs(CharType.Letter).Join();


                    var Remove = false;

                    if(PersonPrefixes.TryGetValue(OnlyLetters, out var NewPrefix)) {
                        Prefix = NewPrefix;
                        Remove = true;
                    } else if (PersonSuffixes.TryGetValue(OnlyLetters, out var NewSuffix)) {
                        Suffix = NewSuffix;
                        Remove = true;
                    }
                    if (Remove) {
                        NameParts.RemoveAt(i);
                    }
                }

                var First = NameParts.GetConsumingEnumerableFirst().Coalesce();
                var Last = NameParts.GetConsumingEnumerableLast().Coalesce();
                var Middle = NameParts.GetConsumingEnumerableFirst().ToImmutableArray();

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
