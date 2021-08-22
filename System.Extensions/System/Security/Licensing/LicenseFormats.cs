using System.Diagnostics;

namespace System.Security.Licensing
{
    public static class LicenseFormats
    {
        public static class Default<TJsonCompiled> where TJsonCompiled : DisplayRecord
        {
            public static LicenseFormatBase<TJsonCompiled> Instance { get; } = new DefaultLicenseFormat<TJsonCompiled>();
        }

    }

}
