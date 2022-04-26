namespace System.Security.Licensing {
    public abstract class LicenseDecoderBase {
        public virtual T? Decode<T>(string LicenseText)
            where T : class {
            throw new NotImplementedException();
        }

    }

}
