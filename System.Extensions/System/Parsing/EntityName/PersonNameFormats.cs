using System.Collections.Generic;
using System.Collections.Immutable;

namespace System {
    public static class PersonNameFormats {
        public static PersonNameFormat FirstMiddleLast { get; }
        public static PersonNameFormat LastFirstMiddle { get; }

        public static ImmutableArray<PersonNameFormat> All { get; }

        static PersonNameFormats() {
            FirstMiddleLast = new FirstMiddleLastPersonNameFormat();
            LastFirstMiddle = new LastFirstMiddlePersonNameFormat();
            
            All = new[] {
                FirstMiddleLast,
                LastFirstMiddle,
            }.ToImmutableArray();

        }
    }

}
