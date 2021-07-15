using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System {
    public record PhoneNumberParser : DefaultClassParser<PhoneNumber> {
        public PhoneNumberParser(string? Value) : base(Value) {
        
        }

        public override PhoneNumber GetValue() {
            var ret = new PhoneNumber();
            if(TryGetValue(out var NewValue)) {
                ret = NewValue;
            }
            return ret;
        }

        public override bool TryGetValue([NotNullWhen(true)] out PhoneNumber? Result) {
            var ret = false;
            Result = default;

            var FoundCode = "";
            var FoundNumber = "";
            var FoundExtension = "";

            var CleanedUpText = Input
                .TrimSafe()
                .Replace(@"(", "")
                .Replace(@")", "")
                .Replace(@"-", "")
                .Replace(@".", "")
                .Replace(@"\", "")
                .Replace(@"/", "")
                .Replace(@" ", "")
                ;

            var RemainingText = CleanedUpText;

            //Parse the country code
            if (RemainingText.Length > 0 && PhoneNumberCountryCode.IsIdentifier(RemainingText[0])) {
                RemainingText = RemainingText[1..];

                var Found = (
                    from x in PhoneNumberCountryCodes.Numbers
                    where RemainingText.StartsWith(x)
                    select x
                    ).FirstOrDefault();

                if (Found.IsNotBlank()) {
                    FoundCode = Found;
                    RemainingText = RemainingText[FoundCode.Length..];
                }
            }

            //Parse the number
            if (RemainingText.Length > 0) {
                var FoundNumbers = new List<char>(20);
                var NeedsShortening = true;

                for (var i = 0; i < RemainingText.Length; i++) {
                    var Letter = RemainingText[i];
                    if (Letter.IsDigit()) {
                        FoundNumbers.Add(Letter);
                    } else if (Letter.IsLetter()) {
                        RemainingText = RemainingText[i..];
                        NeedsShortening = false;
                        break;
                    }
                }
                FoundNumber = new string(FoundNumbers.ToArray());

                if (NeedsShortening) {
                    RemainingText = "";
                }

            }

            //Parse the extension

            if (RemainingText.Length > 0) {
                FoundExtension = RemainingText.WhereIs(CharType.Digit);
            }


            if (FoundNumber.IsNotBlank()) {

                var Code = FoundCode.Coalesce();
                var Number = FoundNumber.Coalesce();
                var Extension = FoundExtension.Coalesce();

                Result = new PhoneNumber() {
                    CountryCode = Code,
                    Number = Number,
                    Extension = Extension,
                };
            }

            Result = Result?.Normalize();

            if (Result is { } R1 && R1.ToString().Length < 7) {
                Result = default;
            }

            if(Result is { }) {
                ret = true;
            }


            return ret;

        }
    }
}
