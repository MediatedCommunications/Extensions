namespace System.Security.Licensing {
    public static class LicenseParsers {
        public static LicenseParserBase<TCompiled> Default<TCompiled>() where TCompiled : class {
            return LicenseParsers<TCompiled>.Default;
        }
    }

}
