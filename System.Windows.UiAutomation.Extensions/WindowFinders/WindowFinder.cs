using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Windows.UiAutomation {

    public delegate bool WindowEnumProc(IntPtr hwnd, IntPtr lparam);

    public delegate bool WindowEnumProcSearch<T>(IntPtr Handle, out bool ContinueSearch, out T Result);

    public class WindowEnumProcSearchResult<T> { }


    public abstract class WindowFinder {
        public static RootWindowFinder Root { get; private set; } = new RootWindowFinder();
        public static ChildWindowFinder ForWindow(IntPtr Hwnd) => new ChildWindowFinder(Hwnd);

        protected abstract bool EnumWindows(WindowEnumProc callback);


        public LinkedList<IntPtr> FindAll(Func<IntPtr, Boolean> Filter) {
            return Find((IntPtr Handle, out bool ContinueSearch, out IntPtr Result) => {
                ContinueSearch = true;

                Result = Handle;

                return Filter(Handle);
            });
        }

        public IntPtr FindFirst(Func<IntPtr, Boolean> Filter) {
            return Find((IntPtr Handle, out bool ContinueSearch, out IntPtr Result) => {

                var HasResult = false;
                ContinueSearch = false;

                Result = Handle;
                if (Filter(Handle)) {
                    HasResult = true;
                } else {
                    ContinueSearch = true;
                }

                return HasResult;
            }).FirstOrDefault();
        }

        public LinkedList<T> Find<T>(WindowEnumProcSearch<T> Condition) {
            var ret = new LinkedList<T>();

            var Success = (EnumWindows((Handle, _) => {
                if (Condition(Handle, out var ContinueSearch, out var Result)) {
                    ret.AddLast(Result);
                }

                return ContinueSearch;
            }));

            return ret;
        }



    }

}
