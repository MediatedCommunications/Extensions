namespace System.Security.Licensing {
    public class InvalidDeviceException : LicenseException
    {
        public InvalidDeviceException() : base("The provided license is not valid on this device.")
        {

        }
    }

}
