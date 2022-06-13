using PostSharp.Serialization;

namespace PostSharp.Extensions {
    [PSerializable]
    public class IgnoreBlankAttribute : IgnoreActionAttribute {


        protected override bool IsIgnored(object? Value) {
            var ret = false
               || (Value is null)
               || (Value is string V1 && string.IsNullOrWhiteSpace(V1))
               ;

            return ret;
        }


    }

}
