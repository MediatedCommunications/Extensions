using System.Collections.Generic;
using System.Diagnostics;

namespace System
{
    public record PhoneNumberCountryCode : DisplayRecord {
        public string Name { get; init; } = Strings.Empty;
        public string Number { get; init; } = Strings.Empty;

        public static string DefaultIdentifier => "+";

        private static HashSet<string> Identifiers = new() { DefaultIdentifier };

        public static bool IsIdentifier(char V1) {
            return IsIdentifier(V1.ToString());
        }

        public static bool IsIdentifier(string V1) {
            return Identifiers.Contains(V1);
        }


        public PhoneNumberCountryCode(string Number, string Name) {
            this.Number = Number.WhereIs(CharType.Digit);
            this.Name = Name.TrimSafe();
        }
        public static PhoneNumberCountryCode Create(string Number, string Name) {
            return new PhoneNumberCountryCode(Number, Name);
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add($@"+{Number}")
                .Postfix.Add(Name)
                ;
        }

    }


}
