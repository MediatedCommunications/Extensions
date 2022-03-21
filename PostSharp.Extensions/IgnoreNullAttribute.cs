using PostSharp.Serialization;

namespace PostSharp.Extensions {
    [PSerializable]
    public class IgnoreNullAttribute : IgnoreValuesAttribute {

        public IgnoreNullAttribute() : base(new object?[] { null }) {
            
        }

    }

}
