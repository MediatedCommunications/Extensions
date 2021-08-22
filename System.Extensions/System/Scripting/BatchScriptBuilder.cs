using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace System.Scripting
{
    public class BatchScriptBuilder : ScriptBuilder
    {

        public override Task InvokeAsync(CancellationToken token = default)
        {
            return InvokeAsync(Script.ToString(), ".bat", RunAs, token);
        }

    }

}
