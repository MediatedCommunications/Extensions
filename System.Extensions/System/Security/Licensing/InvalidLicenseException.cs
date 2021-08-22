namespace System.Security.Licensing
{

    public class InvalidLicenseException : LicenseException {
        public InvalidLicenseException(string Message) : base(Message) { 
        
        }

        public InvalidLicenseException() : base("The provided license could not be loaded.") {

        }
    }

    public class NoLicenseException : InvalidLicenseException
    {
        public NoLicenseException() : base("No license was provided.")
        {

        }
    }

}
