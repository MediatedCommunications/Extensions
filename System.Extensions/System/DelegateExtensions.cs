using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class DelegateExtensions {

        public static IEnumerable<T> GetInvocations<T>(this T? This) where T : Delegate {
            return (This?.GetInvocationList()).Coalesce().WhereIsNotNull().OfType<T>();
        }

    }
}
