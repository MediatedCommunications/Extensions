using System.Runtime.CompilerServices;

namespace System.Diagnostics {
    public class DisplayBuilderStatusRegion : DisplayBuilderRegion {
        public DisplayBuilderStatusRegion(string Format, DisplayBuilder Parent) : base(Format, Parent) {
        
        }

        public override DisplayBuilderStatusRegion If(bool Condition) {
            var ret = Condition
                ? this
                : new DisplayBuilderStatusRegion(Format, Parent)
                ;

            return ret;
        }

        public DisplayBuilder Is(bool Value, [CallerArgumentExpression("Value")] string? Name = default) {
            return this.If(Value).Add(Name);
        }


        public DisplayBuilder IsConflict(bool Value) {
            return this.If(Value).Add("Conflict");
        }

        public DisplayBuilder IsNotConflict(bool Value) {
            return IsConflict(!Value);
        }

        public DisplayBuilder IsEnabled(bool Value) {
            return IsNotEnabled(!Value);
        }

        public DisplayBuilder IsNotEnabled(bool Value) {
            return this.If(Value).Add("Disabled");
        }

        public DisplayBuilder IsVisible(bool Value) {
            return IsNotVisible(!Value);
        }

        public DisplayBuilder IsNotVisible(bool Value) {
            return this.If(Value).Add("Hidden");
        }

        public DisplayBuilder IsNotLocked(bool Value) {
            return IsLocked(!Value);
        }

        public DisplayBuilder IsApproved(bool Value) {
            return this.If(Value).Add("Approved");
        }

        public DisplayBuilder IsNotApproved(bool Value) {
            return IsApproved(!Value);
        }

        public DisplayBuilder IsCurrent(bool Value) {
            return this.If(Value).Add("Current");
        }

        public DisplayBuilder IsNotCurrent(bool Value) {
            return IsCurrent(!Value);
        }

        public DisplayBuilder IsFavorite(bool Value) {
            return this.If(Value).Add("Favorite");
        }

        public DisplayBuilder IsNotFavorite(bool Value) {
            return IsFavorite(!Value);
        }

        public DisplayBuilder IsSigned(bool Value) {
            return this.If(Value).Add("Signed");
        }

        public DisplayBuilder IsNotSigned(bool Value) {
            return this.If(Value).Add("UNSIGNED");
        }


        public DisplayBuilder IsLocked(bool Value) {
            return this.If(Value).Add("Locked");
        }

        public DisplayBuilder IsNotDeleted(bool Value) {
            return IsDeleted(!Value);
        }

        public DisplayBuilder IsDeleted(bool Value) {
            return this.If(Value).Add("Deleted");
        }


        public DisplayBuilder IsNotPrimary(bool Value) {
            return IsPrimary(!Value);
        }

        public DisplayBuilder IsPrimary(bool Value) {
            return this.If(Value).Add("Primary");
        }

        public DisplayBuilder IsNotPrivate(bool Value) {
            return IsPrivate(!Value);
        }

        public DisplayBuilder IsPrivate(bool Value) {
            return this.If(Value).Add("Private");
        }

        public DisplayBuilder IsNotArchived(bool Value) {
            return IsArchived(!Value);
        }

        public DisplayBuilder IsArchived(bool Value) {
            return this.If(Value).Add("Archived");
        }

        public DisplayBuilder IsNotRequired(bool Value) {
            return IsRequired(!Value);
        }

        public DisplayBuilder IsRequired(bool Value) {
            return this.If(Value).Add("Required");
        }

        public DisplayBuilder IsRedacted(bool Value) {
            return this.If(Value).Add("Redacted");
        }

        public DisplayBuilder IsNotRedacted(bool Value) {
            return IsRedacted(!Value);
        }

        public DisplayBuilder IsOwner(bool Value) {
            return this.If(Value).Add("Owner");
        }

        public DisplayBuilder IsNotOwner(bool Value) {
            return IsOwner(!Value);
        }

        public DisplayBuilder IsDefault(bool Value) {
            return this.If(Value).Add("Default");
        }

        public DisplayBuilder IsNotDefault(bool Value) {
            return IsDefault(!Value);
        }

        public DisplayBuilder IsSuccessOrError(bool Value) {
            this.IsSuccess(Value);
            this.IsError(!Value);
            return Parent;
        }

        public DisplayBuilder IsErrorOrSuccess(bool Value) {
            this.IsSuccess(!Value);
            this.IsError(Value);
            return Parent;
        }


        public DisplayBuilder IsSuccess(bool Value) {
            return this.If(Value).Add("Success");
        }

        public DisplayBuilder IsNotSuccess(bool Value) {
            return IsSuccess(!Value);
        }

        public DisplayBuilder IsError(bool Value) {
            return this.If(Value).Add("Error");
        }

        public DisplayBuilder IsNotError(bool Value) {
            return IsError(!Value);
        }


    }

}
