namespace System.Security.Licensing {
    public static class LicenseEncoders
    {
        public static LicenseEncoderBase Default { get; } = new LicenseEncoder();
    }

}
