


using System;

    static internal partial class InternalAssemblyInfo {

		public const string AssemblyBuildDateString = "06/23/2022 16:03:00 -05:00" ;
		public static DateTimeOffset AssemblyBuildDate {get; }

        public const string AssemblyVersionString = "22.06.23.1603";
        public static Version AssemblyVersion {get; } 


        static InternalAssemblyInfo(){
            AssemblyBuildDate = DateTimeOffset.Parse(AssemblyBuildDateString, System.Globalization.DateTimeFormatInfo.InvariantInfo);
            AssemblyVersion = new Version(AssemblyVersionString);
        }

	}
