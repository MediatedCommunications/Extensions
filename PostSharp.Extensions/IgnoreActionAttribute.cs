using PostSharp.Aspects;
using PostSharp.Serialization;

namespace PostSharp.Extensions {

    [PSerializable]
    public abstract class IgnoreActionAttribute : LocationInterceptionAspect {

        protected abstract bool IsIgnored(object? Value);

        public IgnoreActionAttribute() {
            this.AspectPriority = 1;
        }

        public override void OnSetValue(LocationInterceptionArgs args) {
            var Allowed = !IsIgnored(args.Value);

            if (Allowed) {
                args.ProceedSetValue();
            }
        }
    }

}
