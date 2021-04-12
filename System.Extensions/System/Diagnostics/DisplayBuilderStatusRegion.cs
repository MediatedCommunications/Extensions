using Microsoft.VisualBasic;

namespace System.Diagnostics {
    public class DisplayBuilderStatusRegion : DisplayBuilderRegion {
        public DisplayBuilderStatusRegion(string Format, DisplayBuilder Parent) : base(Format, Parent) {
        
        }

        public DisplayBuilder IsEnabled(bool Value) {
            return IsNotEnabled(!Value);
        }

        public DisplayBuilder IsNotEnabled(bool Value) {
            return AddIf(Value, "Disabled");
        }

        public DisplayBuilder IsVisible(bool Value) {
            return IsNotVisible(!Value);
        }

        public DisplayBuilder IsNotVisible(bool Value) {
            return AddIf(Value, "Hidden");
        }

        public DisplayBuilder IsNotDeleted(bool Value) {
            return IsDeleted(!Value);
        }

        public DisplayBuilder IsDeleted(bool Value) {
            return AddIf(Value, "Deleted");
        }

        public DisplayBuilder IsNotRequired(bool Value) {
            return IsRequired(!Value);
        }

        public DisplayBuilder IsRequired(bool Value) {
            return AddIf(Value, "Required");
        }

        public DisplayBuilder IsRedacted(bool Value) {
            return AddIf(Value, "Redacted");
        }

        public DisplayBuilder IsNotRedacted(bool Value) {
            return IsRedacted(!Value);
        }

        public DisplayBuilder IsDefault(bool Value) {
            return AddIf(Value, "Default");
        }

        public DisplayBuilder IsNotDefault(bool Value) {
            return IsDefault(!Value);
        }

    }

}
