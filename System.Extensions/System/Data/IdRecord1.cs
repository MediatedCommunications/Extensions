using System.Diagnostics;

namespace System.Data {

    public record IdRecord<TKey> : DisplayRecord, IHasId<TKey> {
        public TKey Id { get; init; } = DatabaseKey<TKey>.Default;

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Id.Add(Id)
                ;
        }
    }

    public record OptionalIdRecord<TKey> : DisplayRecord, IHasId<TKey?> {
        public TKey? Id { get; init; }

        public OptionalIdRecord() {
            this.Id = DefaultId();
        }

        protected virtual TKey? DefaultId() {
            var ret = default(TKey?);
            if (ret is null) {
                if (typeof(TKey) == typeof(string)) {
                    ret = (TKey)(object)Strings.Empty;
                }
            }

            return ret;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Id.Add(Id)
                ;
        }
    }
}