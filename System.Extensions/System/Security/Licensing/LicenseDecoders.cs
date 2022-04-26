namespace System.Security.Licensing {
    public static class LicenseDecoders {
        public static LicenseDecoderBase Default { get; } = new LicenseDecoder();
    }

}
