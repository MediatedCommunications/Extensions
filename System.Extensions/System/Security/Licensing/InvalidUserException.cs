namespace System.Security.Licensing {
    public class InvalidUserException : LicenseException {
        public InvalidUserException() : base("The provided license is not valid for this user.") {

        }
    }

}
