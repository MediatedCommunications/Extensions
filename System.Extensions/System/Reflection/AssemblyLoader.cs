using System.Collections.Generic;

namespace System.Reflection {
    public static class AssemblyLoader {
        public static List<Assembly> Load(params string[] FileNames) {
            var ret = new List<Assembly>();

            foreach (var FileName in FileNames) {
                if (Load(FileName) is { } V1) {
                    ret.Add(V1);
                }

            }

            return ret;
        }

        public static Assembly? Load(string FileName) {
            var ret = default(Assembly?);

            try {
                if (Assembly.LoadFile(FileName) is { } V1) {
                    ret = V1;
                }
            } catch (Exception ex) {
                ex.Ignore();
            }

            return ret;
        }

    }
}
