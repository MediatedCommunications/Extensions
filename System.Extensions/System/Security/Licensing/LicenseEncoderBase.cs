using System.Diagnostics;

namespace System.Security.Licensing
{
    public abstract class LicenseEncoderBase
    {

        public abstract string Create<T>(T License) 
            where T : class;
        
        public virtual T? Parse<T>(string LicenseText) 
            where T : class
        {
            throw new NotImplementedException();
        }

    }

}
