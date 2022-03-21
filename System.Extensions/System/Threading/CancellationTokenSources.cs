using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Threading {
    public static class CancellationTokenSources {

        public static CancellationTokenSource Create(params CancellationToken[] DependsOn) {
            var ret = DependsOn.Length == 0
                ? new CancellationTokenSource()
                : CancellationTokenSource.CreateLinkedTokenSource(DependsOn)
                ;

            return ret;
        }

    }
}
