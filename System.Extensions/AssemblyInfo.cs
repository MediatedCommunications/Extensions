using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System {
    public static partial class AssemblyInfo {
        public const string AssemblyVersionString = InternalAssemblyInfo.AssemblyVersionString;
        public const string AssemblyBuildDateString = InternalAssemblyInfo.AssemblyBuildDateString;

        public static DateTimeOffset AssemblyBuildDate => InternalAssemblyInfo.AssemblyBuildDate;
        public static Version AssemblyVersion => InternalAssemblyInfo.AssemblyVersion;
    }
}
