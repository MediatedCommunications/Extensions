using System.Text;

namespace System {
    public static class StringExtensions_Ellipsis {
        private const string Ellipsis = "...";

        public static string EllipsizeMiddle(this string? Value, int MaxLength) {
            return Ellipsize(Value, MaxLength, (int)(MaxLength * .75));
        }

        public static string EllipsizeStart(this string? Value, int MaxLength) {
            return Ellipsize(Value, MaxLength, 0);
        }

        public static string Ellipsize(this string? Value, int MaxLength, int CharacterPosition) {
            if (CharacterPosition > MaxLength) {
                CharacterPosition = MaxLength;
            }

            var ret = Value.Coalesce();

            if (ret.Length > MaxLength) {
                if (CharacterPosition + Ellipsis.Length > MaxLength) {
                    CharacterPosition = MaxLength - Ellipsis.Length;
                }
                var SB = new StringBuilder();

                SB.Append(ret[..CharacterPosition]);
                SB.Append(Ellipsis);

                if (ret.Length > SB.Length && SB.Length < MaxLength) {
                    SB.Append(ret[(ret.Length - MaxLength + SB.Length)..]);
                }

                ret = SB.ToString();

            }


            return ret;
        }

        public static string Ellipsize(this string? Value, int MaxLenth, EllipsizePosition Position) {
            var ret = Position switch {
                EllipsizePosition.Start => EllipsizeStart(Value, MaxLenth),
                EllipsizePosition.Middle => EllipsizeMiddle(Value, MaxLenth),
                _ => EllipsizeEnd(Value, MaxLenth),
            };

            return ret;
        }

        public static string EllipsizeEnd(this string? Value, int MaxLength) {
            return Ellipsize(Value, MaxLength, MaxLength);
        }

        public static string Ellipsize(this string? Value, int MaxLength) {
            return EllipsizeEnd(Value, MaxLength);
        }
    }

    public enum EllipsizePosition {
        Unknown,
        Default,
        End,
        Middle,
        Start
    }

}
