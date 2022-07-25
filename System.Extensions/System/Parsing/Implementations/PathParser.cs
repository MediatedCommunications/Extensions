using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace System
{
    public record PathParser : DisplayRecord {

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(FullPath)
                ;
        }

        public PathParser ParentPath() {
            return FromPath(Directory);
        }

        public static PathParser FromPath(string? NewValue) {
            
            
            var ret = new PathParser(NewValue);

            return ret;
        }

        protected PathParser(string? FullPath) {
            var ActualPath = FullPath.TrimEnd(Strings.Slashes);
            this.FullPath = ActualPath;
            this.FileName = Path.GetFileName(ActualPath).Coalesce();
            this.Name = Path.GetFileNameWithoutExtension(ActualPath).Coalesce();
            this.Directory = Path.GetDirectoryName(ActualPath).Coalesce();
            this.DotExtension = Path.GetExtension(ActualPath).Coalesce();
            this.Extension = DotExtension.TrimStart(Strings.Dots);

            if(this.FileName.IsBlank() && this.Directory.IsBlank()) {
                this.Directory = ActualPath;
            }

        }

        /// <summary>
        /// The full path to the item.
        /// </summary>
        public string FullPath { get; private set; }

        /// <summary>
        /// The actual file name
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// The filename without the extension
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The directory name
        /// </summary>
        public string Directory { get; private set; }

        /// <summary>
        /// The DotExtension
        /// </summary>
        public string DotExtension { get; private set; }

        /// <summary>
        /// The extension without the leading dot
        /// </summary>
        public string Extension { get; private set; }


        public PathParser GetFullPath(out string Value) {
            Value = FullPath;
            return this;
        }

        public PathParser GetFileName(out string Value) {
            Value = FileName;
            return this;
        }

        public PathParser GetName(out string Value) {
            Value = Name;
            return this;
        }

        public PathParser GetDirectory(out string Value) {
            Value = Directory;
            return this;
        }


        public PathParser GetDotExtension(out string Value) {
            Value = DotExtension;
            return this;
        }


        public PathParser GetExtension(out string Value) {
            Value = Extension;
            return this;
        }

        public PathParser WithFullPath(Func<string, string?> NewValue) {
            return WithFullPath(NewValue(FullPath));
        }

        public PathParser WithFullPath(string? NewValue) {
            return FromPath(NewValue);
        }

        public PathParser WithFileName(Func<string, string?> NewValue) {
            return WithFileName(NewValue(FileName));
        }

        public PathParser WithFileName(string? NewValue) {
            var NewPath = NewValue;
            if (Directory is { } V1 && NewValue is { } V2) {
                NewPath = Path.Combine(V1, V2);
            }
            NewValue = NewPath;

            return FromPath(NewValue);

        }

        public PathParser WithName(Func<string, string?> NewValue) {
            return WithName(NewValue(Name));
        }

        public PathParser WithName(string? NewValue) {
            return WithFileName($@"{NewValue}{DotExtension}");
        }

        public PathParser AppendExtension(Func<string, string?> NewValue, bool? IgnoreIfPresent = default, StringComparer? Comparer = default) {
            return AppendExtension(NewValue(DotExtension), IgnoreIfPresent, Comparer);
        }

        public PathParser AppendExtension(string? NewValue, bool? IgnoreIfPresent = default, StringComparer? Comparer = default) {
            return AppendDotExtension($@".{NewValue}", IgnoreIfPresent, Comparer);
        }

        public PathParser AppendDotExtension(Func<string, string?> NewValue, bool? IgnoreIfPresent = default, StringComparer? Comparer = default) {
            return AppendDotExtension(NewValue(DotExtension), IgnoreIfPresent, Comparer);
        }

        public PathParser AppendDotExtension(string? NewValue, bool? IgnoreIfPresent = default, StringComparer? Comparer = default) {
            var ret = this;

            if (IgnoreIfPresent ?? true) {
                var C = Comparer ?? StringComparer.InvariantCultureIgnoreCase;

                if (C.Compare(DotExtension, NewValue) == 0) {
                    ret = WithFileName($@"{FileName}{NewValue}");
                }

            }

            return ret;
        }


        public PathParser WithDotExtension(Func<string, string?> NewValue) {
            return WithDotExtension(NewValue(DotExtension));
        }

        public PathParser WithDotExtension(string? NewValue) {
            return WithFileName($@"{Name}{NewValue}");
        }

        public PathParser WithExtension(Func<string, string?> NewValue) {
            return WithExtension(NewValue(Extension));
        }

        public PathParser WithExtension(string? NewValue) {
            if (NewValue is { }) {
                if(NewValue.Length > 0) {
                    if (!NewValue.StartsWith(".")) {
                        NewValue = "." + NewValue;
                    }
                }
                
            }

            return WithFileName($@"{Name}{NewValue}");
        }


        public PathParser Combine(params string?[] Values) {
            return Combine(Values.AsEnumerable());
        }

        public PathParser Combine(IEnumerable<string?> Values) {
            var ret = this.FullPath;
            foreach (var item in Values.WhereIsNotBlank()) {
                ret = Path.Combine(ret, item);
            }


            return FromPath(ret);
        }

    }
}
