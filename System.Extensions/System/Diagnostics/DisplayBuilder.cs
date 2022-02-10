using System.Collections.Generic;
using System.Linq;

namespace System.Diagnostics
{

    [DebuggerDisplay(DebuggerDisplay)]
    public class DisplayBuilder : IGetDebuggerDisplay {
        public const string DebuggerDisplay = "{" + nameof(IGetDebuggerDisplay.GetDebuggerDisplay) + "(),nq}";

        public static DisplayBuilder Create() {
            return new DisplayBuilder();
        }

        public DisplayBuilder() {
            this.Id = new DisplayBuilderRegion("[{0}]", this);
            this.Type = new DisplayBuilderRegion("<{0}>", this);
            this.Prefix = new DisplayBuilderRegion("({0})", this);
            this.Data = new DisplayBuilderRegion("{0}", this);
            this.Postfix = new DisplayBuilderRegion("({0})", this);
            this.Status = new DisplayBuilderStatusRegion("<{0}>", this);
        }

        public DisplayBuilder Add(params IGetDebuggerDisplayBuilder?[] Items) {
            return Add(Items.AsEnumerable());
        }

        public DisplayBuilder Add(IEnumerable<IGetDebuggerDisplayBuilder?> Items) {
            foreach (var item in Items.WhereIsNotNull()) {
                item.GetDebuggerDisplayBuilder(this);
            }

            return this;
        }

        public virtual DisplayBuilder If(
            bool Condition,
            Func<DisplayBuilder, DisplayBuilder> Then
        ) {
            return If(Condition, Then, x => x);
        }

        public virtual DisplayBuilder If(
            bool Condition,
            Func<DisplayBuilder, DisplayBuilder> Then,
            Func<DisplayBuilder, DisplayBuilder> Else
        ) {

            if (Condition) {
                return Then(this);
            } else {
                return Else(this);
            }

        }

        public DisplayBuilderRegion Id { get; }
        public DisplayBuilderRegion Type { get; }
        public DisplayBuilderRegion Prefix { get; }
        public DisplayBuilderRegion Data { get; }
        public DisplayBuilderRegion Postfix { get; }
        public DisplayBuilderStatusRegion Status { get; }

        public virtual string GetDebuggerDisplay() {
            var ret = new [] {
                Id.GetDebuggerDisplay(),
                Type.GetDebuggerDisplay(),
                Prefix.GetDebuggerDisplay(),
                Data.GetDebuggerDisplay(),
                Postfix.GetDebuggerDisplay(),
                Status.GetDebuggerDisplay(),
            }.WhereIsNotBlank().JoinSpace();

            return ret;
        }

        public override string ToString() {
            return GetDebuggerDisplay();
        }

    }

}
