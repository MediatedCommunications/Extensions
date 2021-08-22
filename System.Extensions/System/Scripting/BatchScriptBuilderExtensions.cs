using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Scripting
{

    public static class BatchScriptBuilderExtensions
    {

        public static BatchScriptBuilder Kill(this BatchScriptBuilder This, params int[] ProcessIDs)
        {
            return Kill(This, (IEnumerable<int>)ProcessIDs);
        }


        public static BatchScriptBuilder Kill(this BatchScriptBuilder This, IEnumerable<int> ProcessIDs)
        {

            foreach (var ProcessID in ProcessIDs)
            {
                var ret = $@"TASKKILL /PID {ProcessID} /F 2> nul";

                This.Script.AppendLine(ret);
            }

            return This;
        }

        public static BatchScriptBuilder EchoOff(this BatchScriptBuilder This)
        {
            var ret = "@ECHO OFF";
            This.Script.AppendLine(ret);

            return This;
        }

        public static BatchScriptBuilder Echo(this BatchScriptBuilder This, string Value)
        {
            var ret = $"ECHO {Value}";
            This.Script.AppendLine(ret);

            return This;
        }

        public static BatchScriptBuilder StartApplication(this BatchScriptBuilder This, params string[] Arguments)
        {
            return StartApplication(This, (IEnumerable<string>)Arguments);
        }

        public static BatchScriptBuilder StartApplication(this BatchScriptBuilder This, IEnumerable<string> Arguments)
        {
            return StartApplication(This, true, true, Arguments);
        }


        public static BatchScriptBuilder StartApplication(this BatchScriptBuilder This, bool QuoteFirst, bool QuoteRest, params string[] Arguments)
        {
            return StartApplication(This, QuoteFirst, QuoteRest, (IEnumerable<string>)Arguments);
        }

        public static BatchScriptBuilder StartApplication(this BatchScriptBuilder This, bool QuoteFirst, bool QuoteRest, IEnumerable<string> Arguments)
        {
            var Converted = new LinkedList<string>();
            var Index = 0;
            foreach (var item in Arguments)
            {
                var NewArg = item;
                if ((Index == 0 && QuoteFirst) || (Index != 0 && QuoteRest))
                {
                    NewArg = $@"""{NewArg}""";
                }
                Converted.Add(NewArg);

                Index += 1;
            }


            var ret = Converted.JoinSpace();
            This.Script.AppendLine(ret);

            return This;
        }

    }
}
