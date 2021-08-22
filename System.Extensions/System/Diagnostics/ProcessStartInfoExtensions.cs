using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Diagnostics
{
    public static class ProcessStartInfoExtensions
    {
        public static Process? ShellExecute(this ProcessStartInfo This)
        {
            var PSI = This;
            PSI.UseShellExecute = true;

            return System.Diagnostics.Process.Start(PSI);
            
        }

        public static Task<Process?> RunAsAsync(this ProcessStartInfo This, ProcessStartUser User)
        {
            return User switch
            {
                ProcessStartUser.AsCurrentUser => RunAsCurrentUserAsync(This),
                ProcessStartUser.AsAdministrator => RunAsAdministratorAsync(This),
                _ => throw new NotImplementedException(),
            };
        }

        public static Task<Process?> RunAsAdministratorAsync(this ProcessStartInfo This)
        {
            This.Verb = "runas";
            This.UseShellExecute = true;

            var ret = RunInThreadAsync(This);

            return ret;
        }

        public static Task<Process?> RunAsCurrentUserAsync(this ProcessStartInfo This)
        {
            This.UseShellExecute = true;


            var ret = RunInThreadAsync(This);
            return ret;
        }

        private static async Task<Process?> RunInThreadAsync(this ProcessStartInfo This)
        {
            Process? Invoker()
            {

                try
                {
                    var MainWindow = Process.GetCurrentProcess().MainWindowHandle;
                    //We set the focus to ourselves so that the app we launch opens up with focus.
                    SetFocus(MainWindow);
                } catch (Exception ex)
                {
                    ex.Ignore();
                }

                var tret = default(Process);
                try
                {
                    tret = Process.Start(This);
                } catch (Exception ex)
                {
                    ex.Ignore();
                }

                return tret;
            }

            var Invoker2 = Invoke.Sta(Invoker);

            var ret = await Invoker2.InvokeAsync()
                .DefaultAwait()
                ;

            return ret;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr SetFocus(IntPtr hWnd);

    }

}
