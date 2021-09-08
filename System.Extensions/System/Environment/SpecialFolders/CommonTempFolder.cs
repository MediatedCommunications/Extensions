using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System {

    internal class CommonTempFolder : SpecialFolder {
        public override string GetPath() {
            var ret = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\Temp");
            return ret;
        }
    }



}
