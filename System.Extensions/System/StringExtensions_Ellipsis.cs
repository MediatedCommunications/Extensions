using System.Text;

namespace System {
    public static class StringExtensions_Ellipsis {

        public static string Ellipsize(this string? Value, int MaxLength, string? Ellipsis = default) {
            return EllipsizeRight(Value, MaxLength, Ellipsis);
        }

        private static string EllipsizeNone(string? Value, int MaxLength, string? Ellipsis = default) {
            MaxLength.Ignore();
            Ellipsis.Ignore();

            return Value.Coalesce();
        }
        
        public static string EllipsizeLeft(this string? Value, int MaxLength, string? Ellipsis = default) {
            return Ellipsize(Value, MaxLength, 0.0, Ellipsis);
        }

        public static string EllipsizeMiddle(this string? Value, int MaxLength, string? Ellipsis = default) {
            return Ellipsize(Value, MaxLength, .75, Ellipsis);
        }

        public static string EllipsizeRight(this string? Value, int MaxLength, string? Ellipsis = default) {
            return Ellipsize(Value, MaxLength, 1.0, Ellipsis);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="MaxLength"></param>
        /// <param name="RelativePosition">A value between 0 and 1 that positions where the ellipsis is inserted.</param>
        /// <returns></returns>
        public static string Ellipsize(this string? Value, int MaxLength, double RelativePosition, string? Ellipsis = default) {
            var CharacterPosition = (int)(MaxLength * RelativePosition);

            return Ellipsize(Value, MaxLength, CharacterPosition, Ellipsis);
        }

        public static string Ellipsize(this string? Value, int MaxLength, int CharacterPosition, string? Ellipsis = default) {
            Ellipsis ??= Strings.Ellipsis3;

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

        public static string Ellipsize(this string? Value, int MaxLength, EllipsizePosition Position, string? Ellipsis = default) {
            var ret = Position switch {
                EllipsizePosition.Left => EllipsizeLeft(Value, MaxLength, Ellipsis),
                EllipsizePosition.Middle => EllipsizeMiddle(Value, MaxLength, Ellipsis),
                EllipsizePosition.Right => EllipsizeRight(Value, MaxLength, Ellipsis),
                _ => EllipsizeNone(Value, MaxLength, Ellipsis),
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
