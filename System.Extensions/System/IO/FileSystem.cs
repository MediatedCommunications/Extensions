using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace System.IO
{
    public static class FileSystem {
        
        static FileSystem() {
            InvalidFileNameChars = Path.GetInvalidFileNameChars().Select(x => x.ToString()).ToImmutableHashSet();
            InvalidPathNameChars = Path.GetInvalidPathChars().Select(x => x.ToString()).ToImmutableHashSet();

            InvalidCharacterReplacements = new Dictionary<string, string>() {
                [$@"\"] = "-",
                [$@"/"] = "-",
                [$@"|"] = "-",
                [$@":"] = ".",
                [$@"?"] = "-",
                [$@"*"] = "@",
                [$@""""] = "'",
                [$@"<"] = "[",
                [$@">"] = "]",
                [$"\t"] = " ",
            }.ToImmutableDictionary();

            InvalidNameReplacements = new[] {
                $@"",
                $@"CON", $@"PRN", $@"AUX", $@"NUL",
                $@"COM1", $@"COM2",$@"COM3",$@"COM4", $@"COM5", $@"COM6", $@"COM7", $@"COM8", $@"COM9",
                $@"LPT1", $@"LPT2", $@"LPT3", $@"LPT4", $@"LPT5", $@"LPT6", $@"LPT7", $@"LPT8", $@"LPT9",
            }.ToImmutableHashSet(StringComparer.InvariantCultureIgnoreCase);

        }


        public static ImmutableHashSet<string> InvalidFileNameChars { get; } 
        public static ImmutableHashSet<string> InvalidPathNameChars { get; } 

        public static ImmutableDictionary<string, string> InvalidCharacterReplacements { get; } 

        public static ImmutableHashSet<string> InvalidNameReplacements { get; } 

        private static string RemoveInvalidCharacters(string FileName) {
            var ret = FileName;
            foreach (var item in InvalidCharacterReplacements) {
                ret = ret.Replace(item.Key, item.Value);
            }

            foreach (var item in InvalidFileNameChars) {
                ret = ret.Replace(item, Strings.Empty);
            }

            foreach (var item in InvalidPathNameChars) {
                ret = ret.Replace(item, Strings.Empty);
            }

            ret = ret.Trim();

            return ret;
        }

        private static string ReplaceInvalidNames(string FileName) {
            var ret = FileName;
            
            if (InvalidNameReplacements.Contains(ret)) {
                ret = $@"_{ret}";
            }

            return ret;
        }

        public static bool IsSafePath(string FullPath) {
            var Parts = FullPath.SplitPath();

            var Parts2 = Parts.Skip(1).ToLinkedList();

            var ret = !Parts2.Any(x => !IsSafeFileName(x));

            return ret;
        }


        public static bool IsSafeFileName(string FileName) {
            var ret = FileName == SafeFileName(FileName);

            return ret;
        }

        public static string SafeFileName(string FileName) {
            var ret = FileName;

            ret = RemoveInvalidCharacters(ret);
            ret = ReplaceInvalidNames(ret);

            return ret;
        }

        public static List<string> SafeFilePath(params string[] Values) {
            return SafeFilePath(Values.AsEnumerable());
        }

        public static List<string> SafeFilePath(IEnumerable<string> Values) {
            var ret = (
                from x in Values
                let v = SafeFileName(x)
                select v
                ).ToList();

            return ret;
        }

        public static string SafeFileName(params string[] Values) {
            var ret = (
                from x in Values
                let v = SafeFileName(x)
                where v.IsNotBlank()
                select v
            ).Coalesce();

            return ret;
        }


        public static FileSystemAttributes GetAttributesFromName(string Name, string Ext) {
            var NewName = new[] { Name, Ext }.JoinDot();

            var ret = GetAttributesFromName(NewName);

            return ret;
        }

        public static FileSystemAttributes GetAttributesFromName(string Name) {
            var ret = GetAttributesFromNameInternal(Name);
            
            ret = ret.ComputeNormal();

            return ret;
        }

        private static FileSystemAttributes GetAttributesFromNameInternal(string Name) {
            var ret = FileSystemAttributes.None;
            
            if (IsTemporaryName(Name)) {
                ret |= FileSystemAttributes.Temporary;
            }

            if (IsSystemGeneratedName(Name)) {
                ret |= FileSystemAttributes.SystemGenerated;
            }

            if (IsHiddenName(Name)) {
                ret |= FileSystemAttributes.Hidden;
            }

            if (IsExecutableName(Name)) {
                ret |= FileSystemAttributes.Executable;
            }

            return ret;
        }

        public static FileSystemAttributes GetAttributesFromPath(string Path) {
            var ret = FileSystemAttributes.None;

            if (File.Exists(Path)) {
                var Info = new FileInfo(Path);
                var Attribs = Info.Attributes;

                ret = Attribs.ToFileSystemAttributes();

                if(Info.Length == 0) {
                    ret |= FileSystemAttributes.Empty;
                }

            } else if (Directory.Exists(Path)) {
                var Info = new DirectoryInfo(Path);
                var Attribs = Info.Attributes;

                ret = Attribs.ToFileSystemAttributes();

                if (!Directory.EnumerateFileSystemEntries(Path, "*", SearchOption.TopDirectoryOnly).Any()) {
                    ret |= FileSystemAttributes.Empty | FileSystemAttributes.EmptyTree;
                } else if (!Directory.EnumerateFiles(Path, "*", SearchOption.AllDirectories).Any()) {
                    ret |= FileSystemAttributes.EmptyTree;
                }

            } else {
                throw new FileNotFoundException(default, Path);
            }

            ret |= GetAttributesFromNameInternal(Path);
            
            ret = ret.ComputeNormal();

            return ret;
        }

        public static bool IsTemporaryName(string FileName) {

            var ext = FileName.Parse().AsPath().DotExtension.AsText();

            var ret = false
                || ext.EndsWith(".crdownload")
                || ext.EndsWith(".tmp")
                || ext.EndsWith(".temp")
                || ext.StartsWith("~")
                || ext.StartsWith("._")
                ;

            return ret;
        }

        public static bool IsSystemGeneratedName(string FileName) {
            var FN = FileName.Parse().AsPath().FileName.AsText();

            var ret = false
                || FN.Equals("desktop.ini")
                || FN.Equals("thumbs.db")
                || FN.Equals(".ds_store")
                ;

            return ret;
        }

        public static bool IsExecutableName(string FileName) {
            var FN = FileName.Parse().AsPath().Extension.AsText();

            var ret = false
                || FN.Equals("exe")
                || FN.Equals("com")
                || FN.Equals("bat")
                || FN.Equals("cmd")
                || FN.Equals("dll")
                ;

            return ret;
        }

        public static bool IsHiddenName(string FileName) {
            var FN = FileName.Parse().AsPath().FileName.AsText();

            var ret = false
                || FN.StartsWith(".")
                ;

            return ret;
        }


    }
}
