using System.Collections.Immutable;

namespace System {
    public static class EntityNameFormatters {
        public static EntityNameFormatter Default { get; }
        
        static EntityNameFormatters() {
            Default = new EntityNameFormatter() {
                PersonNameFields = new[] {
                    PersonNameFields.FirstMiddleLast,
                    PersonNameFields.FirstLast,
                    PersonNameFields.PrefixFirstLast,
                    PersonNameFields.PrefixFirstMiddleLast,
                    PersonNameFields.All,
                }.ToImmutableArray(),

                PersonNameFormats = PersonNameFormats.All,
            };
        }
    }


}
