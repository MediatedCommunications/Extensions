using PostSharp.Serialization;

namespace PostSharp.Extensions {
    [PSerializable]
    public class IgnoreTrueAttribute : IgnoreValuesAttribute {
        public IgnoreTrueAttribute() : base(true) { }
    }

}
