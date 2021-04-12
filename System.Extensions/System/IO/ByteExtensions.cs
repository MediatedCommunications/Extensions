namespace System.IO {
    public static class ByteExtensions {
        public static MemoryStream ToMemoryStream(this byte[] This) {
            var ret = new MemoryStream(This);

            return ret;
        }
    }
}
