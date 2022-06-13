using System.Collections.Generic;

namespace System.Reflection
{

    public static class LocalAssemblies {
        
        public static List<string> List() {
            var ret = new List<string>();
            var Folders = new[] {
                Entry.Assembly.FolderPath
            };
            
            foreach (var Folder in Folders) {
                if (IO.Directory.Exists(Folder)) {
                    try {
                        ret.Add(IO.Directory.GetFiles(Entry.Assembly.FolderPath, "*.dll"));
                    } catch (Exception ex) {
                        ex.Ignore();
                    }
                }
            }

            return ret;
        }
        
    }
}
