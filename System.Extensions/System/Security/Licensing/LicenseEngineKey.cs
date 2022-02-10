using System.Diagnostics;

namespace System.Security.Licensing {
    public record LicenseEngineKey : DisplayRecord {
        public string Key { get; init; } = Strings.Empty;
    }

}
