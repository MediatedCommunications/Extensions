namespace System.Security.Licensing {
    public class InvalidDomainException : LicenseException {
        public InvalidDomainException() : base("The provided license is not valid for this domain.") {

        }
    }

}
