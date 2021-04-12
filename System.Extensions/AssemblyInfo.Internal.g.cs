


using System;

    static partial class InternalAssemblyInfo {

		public const string AssemblyBuildDateString = "01/11/2021 05:59:15 -06:00" ;
		public static DateTimeOffset AssemblyBuildDate {get; private set;} = DateTimeOffset.Parse(AssemblyBuildDateString, System.Globalization.DateTimeFormatInfo.InvariantInfo);

        public const string AssemblyVersionString = "21.01.11";
        public static Version AssemblyVersion {get; private set;} = new Version(AssemblyVersionString);

	}
