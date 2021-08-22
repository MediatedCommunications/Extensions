namespace System.Security.Licensing
{
    public class LicenseExpiredException : LicenseException
    {
        public LicenseExpiredException() : base("The provided license has expired.")
        {

        }
    }

}
