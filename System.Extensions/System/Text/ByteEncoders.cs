namespace System.Text {
    public static class ByteEncoders {
        public static ByteEncoder Base64_Formatted { get; }
        public static ByteEncoder Base64_NotFormatted { get; }
        
        static ByteEncoders() {
            Base64_Formatted = new Base64ByteEncoder_Formatted();
            Base64_NotFormatted = new Base64ByteEncoder_NotFormatted();
        }
    }
}
