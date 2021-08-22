using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System
{

    [Flags]
    public enum PhoneNumberFields {
        None,
        CountryCode = 1,
        Number = 2,
        Extension = 4,

        CountryCodeAndNumber = CountryCode | Number,
        All = CountryCode | Number | Extension
    }

    public record PhoneNumber  : DisplayRecord {

        /// <summary>
        /// + 555 ...
        /// </summary>
        public string CountryCode { get; init; } = string.Empty;

        /// <summary>
        /// ... 555-555-555 x ...
        /// </summary>
        public string Number { get; init; } = string.Empty;

        /// <summary>
        /// ... x 555
        /// </summary>
        public string Extension { get; init; } = string.Empty;

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(ToString())
                ;
        }

        public string ToString(params PhoneNumberFields[] NumberParts) {
            var Aggregated = PhoneNumberFields.None;

            foreach (var NumberPart in NumberParts) {
                Aggregated |= NumberPart;
            }


            var Parts = new List<string>();

            if (CountryCode.IsNotBlank() && Aggregated.HasFlag(PhoneNumberFields.CountryCode)) {
                Parts.Add($@"+{CountryCode}");
            }

            if (Number.IsNotBlank() && Aggregated.HasFlag(PhoneNumberFields.Number)) {
                Parts.Add($@"{Number}");
            }

            if (Extension.IsNotBlank() && Aggregated.HasFlag(PhoneNumberFields.Extension)) {
                Parts.Add($@"x {Extension}");
            }

            var ret = Parts.JoinSpace();

            return ret;

        }

        public override string ToString() {
            return ToString(PhoneNumberFields.All);
        }

        public PhoneNumber Normalize() {
            var NewCountryCode = CountryCode.WhereIs(CharType.Digit);
            var NewNumber = Number.WhereIs(CharType.Digit);
            var NewExtension = Extension.WhereIs(CharType.Digit);

            if (NewNumber.IsNotBlank()) {
                var SegmentSizes = new Dictionary<int, int>() {
                    [8] = 4,
                    [7] = 4,
                    [6] = 3,
                    [5] = 3,
                    [4] = 4,
                    [3] = 3,
                    [2] = 2,
                    [1] = 1,
                };
                
                var NumberSegments = new LinkedList<string>();
                var NumberParts = NewNumber;

                while (NumberParts.IsNotBlank()) {
                    var SegmentSize = (from x in SegmentSizes where NumberParts.Length >= x.Key select x.Value).FirstOrDefault();

                    var NumberSegment = NumberParts[^SegmentSize..];
                    NumberParts = NumberParts[..^SegmentSize];

                    NumberSegments.AddFirst(NumberSegment);
                }

                NewNumber = NumberSegments.JoinDash();
            }


            var ret = new PhoneNumber() {
                CountryCode = NewCountryCode,
                Number = NewNumber,
                Extension = NewExtension,
            };

            return ret;

        }


    }

    


}
