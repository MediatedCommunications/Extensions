using System;
using System.Runtime.InteropServices;

namespace System.Windows.UiAutomation {
    public class RootWindowFinder : WindowFinder {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumWindows(WindowEnumProc callback, IntPtr lParam);
        protected override bool EnumWindows(WindowEnumProc callback) => EnumWindows(callback, IntPtr.Zero);


    }

}
