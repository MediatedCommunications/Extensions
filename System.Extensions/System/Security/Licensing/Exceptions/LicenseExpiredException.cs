namespace System.Security.Licensing {
    public class LicenseExpiredException : LicenseException
    {
        public LicenseExpiredException() : base("The provided license has expired.")
        {

        }
    }

    public class LicenseExpiredException<T> : LicenseExpiredException, IHasLicense<T> { 
        public T License { get; }
        public LicenseExpiredException(T License) {
            this.License = License;
        }
    }

}
