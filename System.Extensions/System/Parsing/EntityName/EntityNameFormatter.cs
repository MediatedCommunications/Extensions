using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace System {
    public record EntityNameFormatter : DisplayRecord {
        public ImmutableArray<PersonNameFields> PersonNameFields { get; init; } = ImmutableArray<PersonNameFields>.Empty;
        public ImmutableArray<PersonNameFormat> PersonNameFormats { get; init; } = ImmutableArray<PersonNameFormat>.Empty;

        public HashSet<string> Format(EntityName Name) {
            var ret = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

            if(Name is CompanyName { } V1) {
                ret.Add(V1.Name);
            } else if (Name is PersonName { } V2) {
                ret.Add(
                    from Format in PersonNameFormats
                    from Fields in PersonNameFields
                    select Format.FormatName(V2, Fields)
                    );
            }


            return ret;
        }

    }


}
