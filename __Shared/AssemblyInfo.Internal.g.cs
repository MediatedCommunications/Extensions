


using System;

    static internal partial class InternalAssemblyInfo {

		public const string AssemblyBuildDateString = "04/24/2022 16:47:27 -05:00" ;
		public static DateTimeOffset AssemblyBuildDate {get; }

        public const string AssemblyVersionString = "22.04.24.1647";
        public static Version AssemblyVersion {get; } 


        static InternalAssemblyInfo(){
            AssemblyBuildDate = DateTimeOffset.Parse(AssemblyBuildDateString, System.Globalization.DateTimeFormatInfo.InvariantInfo);
            AssemblyVersion = new Version(AssemblyVersionString);
        }

	}
