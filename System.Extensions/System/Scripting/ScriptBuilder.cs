using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Scripting
{
    public abstract class ScriptBuilder : DisplayClass
    {
        public ProcessStartUser RunAs { get; set; } = ProcessStartUser.AsCurrentUser;

        public StringBuilder Script { get; } = new StringBuilder();
        

        public override string ToString()
        {
            return Script.ToString();
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder)
        {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Script.ToString())
                ;
        }

        public abstract Task InvokeAsync(CancellationToken token = default);

        protected static async Task<Process> InvokeAsync(string ScriptContents, string Extension, ProcessStartUser User, CancellationToken token)
        {
            var FN = System.IO.TemporaryFile.Create(Extension: Extension);
            System.IO.File.WriteAllText(FN.FullPath, ScriptContents);

            var PSI = new ProcessStartInfo()
            {
                FileName = FN.FullPath,
            };

            var ret = await PSI.RunAsAsync(User)
                .DefaultAwait()
                ;

            if(ret is null)
            {
                throw new ArgumentNullException(nameof(ret));
            }

            await ret.WaitForExitAsync(token)
                .DefaultAwait()
                ;


            return ret;
        }

    }

}
