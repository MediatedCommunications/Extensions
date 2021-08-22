using PostSharp.Serialization;

namespace PostSharp.Extensions {
    [PSerializable]
    public class IgnoreNullAttribute : AllowNullAttribute {

        public IgnoreNullAttribute(bool Ignore = true) : base(!Ignore) {
            
        }

    }

}
