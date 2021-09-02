namespace System.Security.Licensing {
    public class NoLicenseException : InvalidLicenseException
    {
        public NoLicenseException() : base("No license was provided.")
        {

        }
    }

}
