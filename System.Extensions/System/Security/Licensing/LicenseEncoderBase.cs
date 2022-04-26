using System.Diagnostics;

namespace System.Security.Licensing {
    public abstract class LicenseEncoderBase
    {

        public abstract string Encode<T>(T License) 
            where T : class;
        
    }

}
