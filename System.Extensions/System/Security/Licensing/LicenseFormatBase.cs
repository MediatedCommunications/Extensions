using System.Diagnostics;

namespace System.Security.Licensing
{
    public abstract class LicenseFormatBase<TCompiled>
        where TCompiled : DisplayRecord
    {

        public abstract TCompiled? Parse(string LicenseText);
        public abstract string Create(TCompiled License);

    }

    public abstract class LicenseFormatBase<TJson, TCompiled> 
        : LicenseFormatBase<TCompiled>
        where TJson : DisplayRecord
        where TCompiled : DisplayRecord
    {
        protected LicenseEncoderBase Encoder { get; }

        public LicenseFormatBase(LicenseEncoderBase Encoder)
        {
            this.Encoder = Encoder;
        }

    }

}
