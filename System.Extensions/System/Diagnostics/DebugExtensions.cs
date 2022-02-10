using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.Diagnostics
{

    public static class DebugUtils
    {
        private static IEnumerable<PropertyInfo> Props(object x)
        {
            return x.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.GetIndexParameters().Length == 0).OrderBy(p => p.Name);
        }

        /// <summary>
        /// Returns multiline string that contains property names and their values for <see cref="@this"/> object
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ShowFields(object? @this)
        {
            var ret = Strings.Empty;
            if (@this is not { })
            {
                ret = "N.U.L.L";
            } else
            {
                var sb = new StringBuilder();

                foreach (var p in Props(@this))
                {
                    sb.Append($"{p.Name}:\t{p.GetValue(@this)}\n");
                }

                ret = sb.ToString();

            }

            return ret;
        }
    }

}
