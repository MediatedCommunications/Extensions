namespace System.Security.Licensing {
    public class DecoderLicenseParser<TCompiled> : LicenseParserBase<TCompiled>
        where TCompiled : class
        {
        protected LicenseDecoderBase Decoder { get; }

        public DecoderLicenseParser(LicenseDecoderBase Decoder) {
            this.Decoder = Decoder;
        }
        
        public override TCompiled? Parse(string LicenseText) {
            return Decoder.Decode<TCompiled>(LicenseText);
        }
    }

}
