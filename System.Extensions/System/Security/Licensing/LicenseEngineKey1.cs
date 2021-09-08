using System.Diagnostics;

namespace System.Security.Licensing {
    public record LicenseEngineKey<TCompiled> : LicenseEngineKey
        where TCompiled : DisplayRecord {

    }

}
