using PostSharp.Serialization;

namespace PostSharp.Extensions {

    [PSerializable]
    public class IgnoreFalseAttribute : IgnoreValuesAttribute {
        public IgnoreFalseAttribute() : base(false) { }
    }

}
