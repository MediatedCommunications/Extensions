using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO {

    internal class CommonTempDirectory : SpecialDirectory {
        public override string GetPath() {
            var ret = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\Temp");
            return ret;
        }
    }



}
