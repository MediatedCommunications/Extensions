namespace System {
    [Flags]
    public enum CharType {
        None            = 0b0000_0000_0000,
        Whitespace      = 0b0000_0000_0001,
        Upper           = 0b0000_0000_0010,
        Lower           = 0b0000_0000_0100,
        Letter          = 0b0000_0000_1000,
        Digit           = 0b0000_0001_0000,
        LetterOrDigit   = 0b0000_0001_1000,
        Number          = 0b0000_0010_0000,
        Punctuation     = 0b0000_0100_0000,
        Symbol          = 0b0000_1000_0000,
        HighSurrogate   = 0b0001_0000_0000,
        LowSurrogate    = 0b0010_0000_0000,
        Control         = 0b0100_0000_0000,
    }

}
