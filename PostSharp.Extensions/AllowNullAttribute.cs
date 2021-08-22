using PostSharp.Aspects;
using PostSharp.Serialization;

namespace PostSharp.Extensions {
    [PSerializable]
    public class AllowNullAttribute : LocationInterceptionAspect {
        private bool Allow;

        public AllowNullAttribute(bool Allow = true) {
            this.Allow = Allow;
            this.AspectPriority = 1;
        }

        public override void OnSetValue(LocationInterceptionArgs args) {
            var Allowed = false
                || Allow
                || args.Value is { }
                ;

            if (Allowed) {
                args.ProceedSetValue();
            }
        }
    }

}
