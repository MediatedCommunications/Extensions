


using System;

    static internal partial class InternalAssemblyInfo {

		public const string AssemblyBuildDateString = "11/22/2021 10:52:51 -06:00" ;
		public static DateTimeOffset AssemblyBuildDate {get; private set;} = DateTimeOffset.Parse(AssemblyBuildDateString, System.Globalization.DateTimeFormatInfo.InvariantInfo);

        public const string AssemblyVersionString = "21.11.22.0652";
        public static Version AssemblyVersion {get; private set;} = new Version(AssemblyVersionString);

	}
