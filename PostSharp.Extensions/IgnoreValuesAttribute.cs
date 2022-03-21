using PostSharp.Aspects;
using PostSharp.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace PostSharp.Extensions {
    [PSerializable]
    public class IgnoreValuesAttribute : LocationInterceptionAspect {
        private List<object?> Values;

        public IgnoreValuesAttribute(params object?[] Values) {
            this.AspectPriority = 1;
            this.Values = Values.ToList();
        }

        public override void OnSetValue(LocationInterceptionArgs args) {
            var Allowed = false
                || !Values.Contains(args.Value)
                ;

            if (Allowed) {
                args.ProceedSetValue();
            }
        }
    }

}
