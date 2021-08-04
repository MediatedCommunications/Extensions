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

        public DisplayBuilder IsNotLocked(bool Value) {
            return IsLocked(!Value);
        }

        public DisplayBuilder IsApproved(bool Value) {
            return AddIf(Value, "Approved");
        }

        public DisplayBuilder IsNotApproved(bool Value) {
            return IsApproved(!Value);
        }

        public DisplayBuilder IsCurrent(bool Value) {
            return AddIf(Value, "Current");
        }

        public DisplayBuilder IsNotCurrent(bool Value) {
            return IsCurrent(!Value);
        }

        public DisplayBuilder IsFavorite(bool Value) {
            return AddIf(Value, "Favorite");
        }

        public DisplayBuilder IsNotFavorite(bool Value) {
            return IsFavorite(!Value);
        }

        public DisplayBuilder IsSigned(bool Value) {
            return AddIf(Value, "Signed");
        }

        public DisplayBuilder IsNotSigned(bool Value) {
            return AddIf(Value, "UNSIGNED");
        }


        public DisplayBuilder IsLocked(bool Value) {
            return AddIf(Value, "Locked");
        }

        public DisplayBuilder IsNotDeleted(bool Value) {
            return IsDeleted(!Value);
        }

        public DisplayBuilder IsDeleted(bool Value) {
            return AddIf(Value, "Deleted");
        }

        public DisplayBuilder IsNotPrivate(bool Value) {
            return IsPrivate(!Value);
        }

        public DisplayBuilder IsPrivate(bool Value) {
            return AddIf(Value, "Private");
        }

        public DisplayBuilder IsNotArchived(bool Value) {
            return IsArchived(!Value);
        }

        public DisplayBuilder IsArchived(bool Value) {
            return AddIf(Value, "Archived");
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

        public DisplayBuilder IsOwner(bool Value) {
            return AddIf(Value, "Owner");
        }

        public DisplayBuilder IsNotOwner(bool Value) {
            return IsOwner(!Value);
        }

        public DisplayBuilder IsDefault(bool Value) {
            return AddIf(Value, "Default");
        }

        public DisplayBuilder IsNotDefault(bool Value) {
            return IsDefault(!Value);
        }

    }

}
