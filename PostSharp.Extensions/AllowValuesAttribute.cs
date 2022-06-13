using PostSharp.Aspects;
using PostSharp.Serialization;

namespace PostSharp.Extensions {

    [PSerializable]
    public class AllowValuesAttribute : LocationInterceptionAspect {
        private List<object?> Values;

        public AllowValuesAttribute(params object?[] Values) {
            this.AspectPriority = 1;
            this.Values = Values.ToList();
        }

        public override void OnSetValue(LocationInterceptionArgs args) {
            var Allowed = false
                || Values.Contains(args.Value)
                ;

            if (Allowed) {
                args.ProceedSetValue();
            }
        }
    }

}
