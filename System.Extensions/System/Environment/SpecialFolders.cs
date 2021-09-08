namespace System {
    public class SpecialFolders {

        public static SpecialFolders Local { get; } = new();

        public SpecialFolder AdminTools { get; } = new EnvironmentSpecialFolder(Environment.SpecialFolder.AdminTools);
        public SpecialFolder ApplicationData { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.ApplicationData);
        public SpecialFolder CDBurning { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.CDBurning);
        public SpecialFolder CommonAdminTools { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.CommonAdminTools);
        public SpecialFolder CommonApplicationData { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.CommonApplicationData);
        public SpecialFolder CommonDesktopDirectory { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.CommonDesktopDirectory);
        public SpecialFolder CommonDocuments { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.CommonDocuments);
        public SpecialFolder CommonMusic { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.CommonMusic);
        public SpecialFolder CommonOemLinks { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.CommonOemLinks);
        public SpecialFolder CommonPictures { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.CommonPictures);
        public SpecialFolder CommonProgramFiles { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.CommonProgramFiles);
        public SpecialFolder CommonProgramFilesX86 { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.CommonProgramFilesX86);
        public SpecialFolder CommonPrograms { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.CommonPrograms);
        public SpecialFolder CommonStartMenu { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.CommonStartMenu);
        public SpecialFolder CommonStartup { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.CommonStartup);
        public SpecialFolder CommonTemplates { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.CommonTemplates);
        public SpecialFolder CommonVideos { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.CommonVideos);
        public SpecialFolder Cookies { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.Cookies);
        public SpecialFolder Desktop { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.Desktop);
        public SpecialFolder DesktopDirectory { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.DesktopDirectory);
        public SpecialFolder Favorites { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.Favorites);
        public SpecialFolder Fonts { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.Fonts);
        public SpecialFolder History { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.History);
        public SpecialFolder InternetCache { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.InternetCache);
        public SpecialFolder LocalApplicationData { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.LocalApplicationData);
        public SpecialFolder LocalizedResources { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.LocalizedResources);
        public SpecialFolder MyComputer { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.MyComputer);
        public SpecialFolder MyDocuments { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.MyDocuments);
        public SpecialFolder MyMusic { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.MyMusic);
        public SpecialFolder MyPictures { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.MyPictures);
        public SpecialFolder MyVideos { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.MyVideos);
        public SpecialFolder NetworkShortcuts { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.NetworkShortcuts);
        public SpecialFolder Personal { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.Personal);
        public SpecialFolder PrinterShortcuts { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.PrinterShortcuts);
        public SpecialFolder ProgramFiles { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.ProgramFiles);
        public SpecialFolder ProgramFilesX86 { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.ProgramFilesX86);
        public SpecialFolder Programs { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.Programs);
        public SpecialFolder Recent { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.Recent);
        public SpecialFolder Resources { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.Resources);
        public SpecialFolder SendTo { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.SendTo);
        public SpecialFolder StartMenu { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.StartMenu);
        public SpecialFolder Startup { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.Startup);
        public SpecialFolder System { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.System);
        public SpecialFolder SystemX86 { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.SystemX86);
        public SpecialFolder Templates { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.Templates);
        public SpecialFolder UserProfile { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.UserProfile);
        public SpecialFolder Windows { get;} = new EnvironmentSpecialFolder(Environment.SpecialFolder.Windows);

        public SpecialFolder MyTemp { get; } = new MyTempFolder();
        public SpecialFolder CommonTemp { get; } = new CommonTempFolder();


    }



}
