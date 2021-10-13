using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System {
    public static class PersonNameFormatExtensions {
        public static bool TryParse(this IEnumerable<PersonNameFormat>? This, string Input, [NotNullWhen(true)] out PersonName? Name) {
            var List = This ?? PersonNameFormats.All;
            var ret = false;
            Name = default;

            foreach (var item in List) {
                if(item.TryParse(Input, out Name)) {
                    ret = true;
                    break;
                }
            }

            return ret;
        }
    }

}
