namespace System.Security.Licensing {
    public class InvalidUserException : LicenseException {
        public InvalidUserException() : base("The provided license is not valid for this user.") {

        }
    }

    public class InvalidUserException<T> : InvalidUserException, IHasLicense<T> {
        public T License { get; }
        public InvalidUserException(T License) {
            this.License = License;
        }
    }


}
