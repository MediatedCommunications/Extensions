using System;
using System.Collections.Generic;

namespace System.Scripting
{

    public static class BatchScriptBuilderExtensions_Services
    {
        public static BatchScriptBuilder ConfigureService(this BatchScriptBuilder This, ConfigureServiceParameters P, params string[] ServiceNames)
        {
            return ConfigureService(This, P, (IEnumerable<string>)ServiceNames);
        }

        public static BatchScriptBuilder ConfigureService(this BatchScriptBuilder This, ConfigureServiceParameters P, IEnumerable<string> ServiceNames)
        {
            foreach (var ServiceName in ServiceNames)
            {

                var ret = $@"sc config ""{ServiceName}""";
                if (P?.Type != null)
                {
                    ret += " type= " + P.Type.ToStringSafe().ToLower();
                }

                if (P?.Start != null)
                {
                    ret += " start= " + P.Start.ToStringSafe().ToLower();
                }

                This.Script.AppendLine(ret);
            }

            return This;
        }

        public static BatchScriptBuilder AutoStartService(this BatchScriptBuilder This, params string[] ServiceNames)
        {
            return AutoStartService(This, (IEnumerable<string>)ServiceNames);
        }

        public static BatchScriptBuilder AutoStartService(this BatchScriptBuilder This, IEnumerable<string> ServiceNames)
        {
            return ConfigureService(This, new ConfigureServiceParameters() { Start = ServiceStart.Auto }, ServiceNames);
        }


        public static BatchScriptBuilder DisableService(this BatchScriptBuilder This, params string[] ServiceNames)
        {
            return DisableService(This, (IEnumerable<string>)ServiceNames);
        }

        public static BatchScriptBuilder DisableService(this BatchScriptBuilder This, IEnumerable<string> ServiceNames)
        {
            return ConfigureService(This, new ConfigureServiceParameters() { Start = ServiceStart.Disabled }, ServiceNames);
        }



        public static BatchScriptBuilder StartService(this BatchScriptBuilder This, params string[] ServiceNames)
        {
            return StartService(This, (IEnumerable<string>)ServiceNames);
        }

        public static BatchScriptBuilder StartService(this BatchScriptBuilder This, IEnumerable<string> ServiceNames)
        {
            return ControlService(This, ServiceRunMode.Start, ServiceNames);
        }

        public static BatchScriptBuilder StopService(this BatchScriptBuilder This, params string[] ServiceNames)
        {
            return StopService(This, (IEnumerable<string>)ServiceNames);
        }

        public static BatchScriptBuilder StopService(this BatchScriptBuilder This, IEnumerable<string> ServiceNames)
        {
            return ControlService(This, ServiceRunMode.Stop, ServiceNames);
        }

        public static BatchScriptBuilder PauseService(this BatchScriptBuilder This, params string[] ServiceNames)
        {
            return PauseService(This, (IEnumerable<string>)ServiceNames);
        }

        public static BatchScriptBuilder PauseService(this BatchScriptBuilder This, IEnumerable<string> ServiceNames)
        {
            return ControlService(This, ServiceRunMode.Pause, ServiceNames);
        }

        public static BatchScriptBuilder ControlService(this BatchScriptBuilder This, ServiceRunMode P, IEnumerable<string> ServiceNames)
        {
            foreach (var ServiceName in ServiceNames)
            {
                var ret = $@"net {P} ""{ServiceName}""";
                This.Script.AppendLine(ret);
            }

            return This;
        }
    }
}
