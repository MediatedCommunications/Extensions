using System.Diagnostics;

namespace System.Windows.Registry.Uninstaller
{
    public record UninstallerRegistryConfiguration : DisplayRecord
    {
        public string? DisplayIcon { get; set; }
        public string? DisplayName { get; set; }
        public string? DisplayVersion { get; set; }
        public long? EstimatedSizeInKb { get; set; }
        public DateTime? InstallDate { get; set; }
        public string? InstallLocation { get; set; }
        public long? Language { get; set; }
        public CanModifyValue? CanModify { get; set; }
        public CanRepairValue? CanRepair { get; set; }
        public string? Publisher { get; set; }
        public string? QuietUninstallString { get; set; }
        public string? UninstallString { get; set; }
        public string? URLUpdateInfo { get; set; }
    }
}
