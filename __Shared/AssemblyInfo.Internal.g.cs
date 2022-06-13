


using System;

    static internal partial class InternalAssemblyInfo {

		public const string AssemblyBuildDateString = "06/13/2022 00:33:17 -05:00" ;
		public static DateTimeOffset AssemblyBuildDate {get; }

        public const string AssemblyVersionString = "22.06.13.0033";
        public static Version AssemblyVersion {get; } 


        static InternalAssemblyInfo(){
            AssemblyBuildDate = DateTimeOffset.Parse(AssemblyBuildDateString, System.Globalization.DateTimeFormatInfo.InvariantInfo);
            AssemblyVersion = new Version(AssemblyVersionString);
        }

	}
