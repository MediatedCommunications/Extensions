namespace System.Security.Licensing {
    public class InvalidLicenseException : Exception {
        public InvalidLicenseException() : base("The provided license could not be loaded.") {

        }
    }

}
