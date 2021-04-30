namespace System.Diagnostics {

    [DebuggerDisplay(DebuggerDisplay)]
    public class DisplayBuilder : IGetDebuggerDisplay {
        public const string DebuggerDisplay = "{" + nameof(IGetDebuggerDisplay.GetDebuggerDisplay) + "(),nq}";

        public static DisplayBuilder Create() {
            return new DisplayBuilder();
        }

        public DisplayBuilder() {
            this.Id = new DisplayBuilderRegion("[{0}]", this);
            this.Type = new DisplayBuilderRegion("<{0}>", this);
            this.Status = new DisplayBuilderStatusRegion("***{0}***", this);
            this.Prefix = new DisplayBuilderRegion("({0})", this);
            this.Data = new DisplayBuilderRegion("{0}", this);
            this.Postfix = new DisplayBuilderRegion("({0})", this);
        }

        public DisplayBuilder Add(params IGetDebuggerDisplayBuilder[] Items) {
            foreach (var item in Items) {
                item.GetDebuggerDisplayBuilder(this);
            }

            return this;
        }


        public DisplayBuilderRegion Id { get; private set; }
        public DisplayBuilderRegion Type { get; private set; }
        public DisplayBuilderStatusRegion Status { get; private set; }
        public DisplayBuilderRegion Prefix { get; private set; }
        public DisplayBuilderRegion Data { get; private set; }
        public DisplayBuilderRegion Postfix { get; private set; }

        public virtual string GetDebuggerDisplay() {
            var ret = new [] {
                Id.GetDebuggerDisplay(),
                Type.GetDebuggerDisplay(),
                Status.GetDebuggerDisplay(),
                Prefix.GetDebuggerDisplay(),
                Data.GetDebuggerDisplay(),
                Postfix.GetDebuggerDisplay()
            }.WhereIsNotBlank().JoinSpace();

            return ret;
        }

        public override string ToString() {
            return GetDebuggerDisplay();
        }

    }

}
