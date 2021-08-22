using System.Text;

namespace System {
    public static class StringExtensions_Ellipsis {
        private const string Ellipsis = "...";


        public static string Ellipsize(this string? Value, int MaxLength) {
            return EllipsizeRight(Value, MaxLength);
        }

        private static string EllipsizeNone(string? Value, int MaxLength) {
            MaxLength.Ignore();

            return Value.Coalesce();
        }
        
        public static string EllipsizeLeft(this string? Value, int MaxLength) {
            return Ellipsize(Value, MaxLength, 0.0);
        }

        public static string EllipsizeMiddle(this string? Value, int MaxLength) {
            return Ellipsize(Value, MaxLength, .75);
        }

        public static string EllipsizeRight(this string? Value, int MaxLength) {
            return Ellipsize(Value, MaxLength, 1.0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="MaxLength"></param>
        /// <param name="RelativePosition">A value between 0 and 1 that positions where the ellipsis is inserted.</param>
        /// <returns></returns>
        public static string Ellipsize(this string? Value, int MaxLength, double RelativePosition) {
            var CharacterPosition = (int)(MaxLength * RelativePosition);

            return Ellipsize(Value, MaxLength, CharacterPosition);
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

                if (CharacterPosition >= 0) {

                    SB.Append(ret[..CharacterPosition]);
                    SB.Append(Ellipsis);

                    if (ret.Length > SB.Length && SB.Length < MaxLength) {
                        SB.Append(ret[(ret.Length - MaxLength + SB.Length)..]);
                    }
                } else {
                    SB.Append(Ellipsis[0..MaxLength]);
                }

                ret = SB.ToString();

            }


            return ret;
        }

        public static string Ellipsize(this string? Value, int MaxLength, EllipsizePosition Position) {
            var ret = Position switch {
                EllipsizePosition.Left => EllipsizeLeft(Value, MaxLength),
                EllipsizePosition.Middle => EllipsizeMiddle(Value, MaxLength),
                EllipsizePosition.Right => EllipsizeRight(Value, MaxLength),
                _ => EllipsizeNone(Value, MaxLength),
            };

            return ret;
        }


    }

    public enum EllipsizePosition {
        None,
        Right,
        Middle,
        Left
    }

}
