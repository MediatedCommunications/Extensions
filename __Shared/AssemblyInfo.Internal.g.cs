


using System;

    static internal partial class InternalAssemblyInfo {

		public const string AssemblyBuildDateString = "10/11/2021 13:20:30 -05:00" ;
		public static DateTimeOffset AssemblyBuildDate {get; private set;} = DateTimeOffset.Parse(AssemblyBuildDateString, System.Globalization.DateTimeFormatInfo.InvariantInfo);

        public const string AssemblyVersionString = "21.10.11.0800";
        public static Version AssemblyVersion {get; private set;} = new Version(AssemblyVersionString);

	}
