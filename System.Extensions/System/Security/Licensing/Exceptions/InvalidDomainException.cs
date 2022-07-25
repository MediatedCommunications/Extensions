namespace System.Security.Licensing {
    public class InvalidDomainException : LicenseException {
        public InvalidDomainException() : base("The provided license is not valid for this domain.") {

        }
    }

    public class InvalidDomainException<T> : InvalidDomainException, IHasLicense<T> {
        public T License { get; }
        public InvalidDomainException(T License) {
            this.License = License;
        }
    }

}
