namespace System.Security.Licensing {
    public static class LicenseParsers<TCompiled>
        where TCompiled : class
        {
        public static LicenseParserBase<TCompiled> Default { get; }

        static LicenseParsers() {
            Default = new DecoderLicenseParser<TCompiled>(LicenseDecoders.Default);
        }
    }

}
