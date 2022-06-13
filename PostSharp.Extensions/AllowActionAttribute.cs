using PostSharp.Aspects;
using PostSharp.Serialization;

namespace PostSharp.Extensions {

    [PSerializable]
    public abstract class AllowActionAttribute : LocationInterceptionAspect {

        protected abstract bool IsAllowed(object? Value);

        public AllowActionAttribute() {
            this.AspectPriority = 1;
        }

        public override void OnSetValue(LocationInterceptionArgs args) {
            var Allowed = IsAllowed(args.Value);

            if (Allowed) {
                args.ProceedSetValue();
            }
        }
    }

}
