namespace System.Security.Licensing {

    public class InvalidLicenseException : LicenseException {
        public InvalidLicenseException(string Message) : base(Message) { 
        
        }

        public InvalidLicenseException() : base("The provided value is not a valid license.") {

        }
    }

}
