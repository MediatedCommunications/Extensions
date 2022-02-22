


using System;

    static internal partial class InternalAssemblyInfo {

		public const string AssemblyBuildDateString = "02/18/2022 06:42:59 -06:00" ;
		public static DateTimeOffset AssemblyBuildDate {get; }

        public const string AssemblyVersionString = "22.02.18.0642";
        public static Version AssemblyVersion {get; } 


        static InternalAssemblyInfo(){
            AssemblyBuildDate = DateTimeOffset.Parse(AssemblyBuildDateString, System.Globalization.DateTimeFormatInfo.InvariantInfo);
            AssemblyVersion = new Version(AssemblyVersionString);
        }

	}
