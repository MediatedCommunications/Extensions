using System.Collections.Generic;
using System.Linq;

namespace System.Reflection
{
    public static class AssemblyExtensions {

        public static List<Type> GetTypesSafe(this IEnumerable<Assembly> This) {
            var ret = (
                from x in This.EmptyIfNull()
                from y in x.GetTypesSafe()
                select y
                ).ToList();

            return ret;
        }

        public static List<Type> GetTypesSafe(this Assembly This) {
            var ret = new List<Type>();

            var Array = default(Type?[]);

            try {
                Array = This.GetTypes();
            } catch (ReflectionTypeLoadException ex) {
                Array = ex.Types;
            } catch (Exception ex) {
                ex.Ignore();
            }

            ret.Add(Array.EmptyIfNull().WhereIsNotNull());

            return ret;
        }
        
    }

    

    



    

}
