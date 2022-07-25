namespace System.Security.Licensing {
    public class InvalidDeviceException : LicenseException
    {
        public InvalidDeviceException() : base("The provided license is not valid on this device.")
        {

        }
    }

    public class InvalidDeviceException<T> : InvalidDeviceException, IHasLicense<T> {
        public T License { get; }
        public InvalidDeviceException(T License) {
            this.License = License;
        }
    }

}
