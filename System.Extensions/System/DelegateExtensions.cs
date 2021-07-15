using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System {
    public static class DelegateExtensions {

        public static IEnumerable<T> GetInvocations<T>(this T? This) where T : Delegate {
            return (This?.GetInvocationList()).Coalesce().WhereIsNotNull().OfType<T>();
        }

    }
}
