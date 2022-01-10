namespace System.IO {
    public class SpecialDirectories {

        public static SpecialDirectories Local { get; } = new();

        public SpecialDirectory AdminTools { get; } = new EnvironmentSpecialDirectory(Environment.SpecialFolder.AdminTools);
        public SpecialDirectory ApplicationData { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.ApplicationData);
        public SpecialDirectory CDBurning { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.CDBurning);
        public SpecialDirectory CommonAdminTools { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.CommonAdminTools);
        public SpecialDirectory CommonApplicationData { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.CommonApplicationData);
        public SpecialDirectory CommonDesktopDirectory { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.CommonDesktopDirectory);
        public SpecialDirectory CommonDocuments { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.CommonDocuments);
        public SpecialDirectory CommonMusic { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.CommonMusic);
        public SpecialDirectory CommonOemLinks { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.CommonOemLinks);
        public SpecialDirectory CommonPictures { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.CommonPictures);
        public SpecialDirectory CommonProgramFiles { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.CommonProgramFiles);
        public SpecialDirectory CommonProgramFilesX86 { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.CommonProgramFilesX86);
        public SpecialDirectory CommonPrograms { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.CommonPrograms);
        public SpecialDirectory CommonStartMenu { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.CommonStartMenu);
        public SpecialDirectory CommonStartup { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.CommonStartup);
        public SpecialDirectory CommonTemplates { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.CommonTemplates);
        public SpecialDirectory CommonVideos { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.CommonVideos);
        public SpecialDirectory Cookies { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.Cookies);
        public SpecialDirectory Desktop { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.Desktop);
        public SpecialDirectory DesktopDirectory { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.DesktopDirectory);
        public SpecialDirectory Favorites { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.Favorites);
        public SpecialDirectory Fonts { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.Fonts);
        public SpecialDirectory History { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.History);
        public SpecialDirectory InternetCache { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.InternetCache);
        public SpecialDirectory LocalApplicationData { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.LocalApplicationData);
        public SpecialDirectory LocalizedResources { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.LocalizedResources);
        public SpecialDirectory MyComputer { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.MyComputer);
        public SpecialDirectory MyDocuments { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.MyDocuments);
        public SpecialDirectory MyMusic { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.MyMusic);
        public SpecialDirectory MyPictures { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.MyPictures);
        public SpecialDirectory MyVideos { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.MyVideos);
        public SpecialDirectory NetworkShortcuts { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.NetworkShortcuts);
        public SpecialDirectory Personal { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.Personal);
        public SpecialDirectory PrinterShortcuts { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.PrinterShortcuts);
        public SpecialDirectory ProgramFiles { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.ProgramFiles);
        public SpecialDirectory ProgramFilesX86 { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.ProgramFilesX86);
        public SpecialDirectory Programs { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.Programs);
        public SpecialDirectory Recent { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.Recent);
        public SpecialDirectory Resources { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.Resources);
        public SpecialDirectory SendTo { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.SendTo);
        public SpecialDirectory StartMenu { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.StartMenu);
        public SpecialDirectory StartMenuPrograms { get; } = new EnvironmentSpecialDirectory(Environment.SpecialFolder.StartMenu, "Programs");
        public SpecialDirectory Startup { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.Startup);
        public SpecialDirectory System { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.System);
        public SpecialDirectory SystemX86 { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.SystemX86);
        public SpecialDirectory Templates { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.Templates);
        public SpecialDirectory UserProfile { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.UserProfile);
        public SpecialDirectory Windows { get;} = new EnvironmentSpecialDirectory(Environment.SpecialFolder.Windows);

        public SpecialDirectory MyTemp { get; } = new MyTempDirectory();
        public SpecialDirectory CommonTemp { get; } = new CommonTempDirectory();


    }



}
