using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System {
    public class InitialsPersonNameFormat : PersonNameFormat {

        public override string FormatName(PersonName Name, PersonNameFields Fields) {

            var First = new List<string>() {
                Fields.HasFlag(PersonNameFields.Prefix) ? Name.Prefix : Strings.Empty,
                Fields.HasFlag(PersonNameFields.FirstName) ? Name.First: Strings.Empty,
                Fields.HasFlag(PersonNameFields.MiddleName) ? Name.Middle: Array.Empty<string>(),
                Fields.HasFlag(PersonNameFields.LastName) ? Name.Last: Strings.Empty,
            }.WhereIsNotBlank();

            var ret = (
                from x in First
                let v = x.SafeSubstring(0..1)
                where v.IsNotBlank()
                select v
                ).Join();

            return ret;
        }


        public override bool TryParse(string Input, [NotNullWhen(true)] out PersonName? Name) {
            Name = default;
            return false;
        }

    }

}
