using System.Diagnostics;

namespace System.Security.Licensing
{
    public class DefaultLicenseFormat<TJsonCompiled> 
        : LicenseFormatBase<TJsonCompiled, TJsonCompiled>
        where TJsonCompiled : DisplayRecord
    {

        public DefaultLicenseFormat() : base(LicenseEncoders.Default) { 
        
        }

        public DefaultLicenseFormat(LicenseEncoderBase Encoder) : base(Encoder)
        {

        }

        public override string Create(TJsonCompiled License)
        {
            return Encoder.Create<TJsonCompiled>(License);
        }

        public override TJsonCompiled? Parse(string LicenseText)
        {
            return Encoder.Parse<TJsonCompiled>(LicenseText);
        }
    }

}
