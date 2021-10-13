using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace System {
    public abstract class PersonNameFormat {
        public static ImmutableHashSet<string> PersonPrefixes { get; }
        public static ImmutableHashSet<string> PersonSuffixes { get; }
        public static ImmutableHashSet<char> PersonPunctuations { get; }

        static PersonNameFormat() {
            PersonPrefixes = new[] {
                "Mr",
                "Mrs",
                "Ms",
                "Miss",
                "Dr",
                "Hon",
                "Honorable",
                "Prof",
                "Professor",
            }.ToImmutableHashSet(StringComparer.InvariantCultureIgnoreCase);

            PersonSuffixes = new[] {
                "Jr",
                "Sr",
                "MD",
                "II",
                "III",
            }.ToImmutableHashSet();

            PersonPunctuations = new[] {
                '.',
                ',',
                ' ',
                '\'',//Single quote for names like O'Leary
            }.ToImmutableHashSet();
        }

        protected virtual bool IsValid(PersonName Name) {
            var ret = true
                && Name.First.IsNotBlank() 
                && Name.Last.IsNotBlank()
                ;

            return ret;
        }

        public abstract bool TryParse(string Input, [NotNullWhen(true)] out PersonName? Name);
    }

}
