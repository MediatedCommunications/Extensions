namespace System.Security.Licensing {
    public abstract class LicenseParserBase<TCompiled> where TCompiled : class {
        public abstract TCompiled? Parse(string LicenseText);
    }

}
