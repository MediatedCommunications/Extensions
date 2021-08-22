using System;
using System.Runtime.InteropServices;

namespace System.Windows.UiAutomation {
    public class ChildWindowFinder : WindowFinder {
        public IntPtr Handle { get; private set; }

        public ChildWindowFinder(IntPtr Handle) {
            this.Handle = Handle;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc callback, IntPtr lParam);
        protected override bool EnumWindows(WindowEnumProc callback) => EnumChildWindows(Handle, callback, IntPtr.Zero);
    }

}
