using System.Diagnostics;

namespace System.Data {
    public record IdResultJson<TKey> : DisplayRecord, IIdResult<TKey> {
        public TKey Id { get; init; }

        public IdResultJson() {
            this.Id = DefaultId();
        }

        protected virtual TKey DefaultId() {
            var ret = default(TKey);
            if (ret is null) {
                if (typeof(TKey) == typeof(string)) {
                    ret = (TKey)(object)string.Empty;
                }
            }
            if(ret is null) {
                throw new NullReferenceException(nameof(Id));
            }

            return ret;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Id.Add(Id)
                ;
        }
    }

    public record OptionalIdResultJson<TKey> : DisplayRecord, IIdResult<TKey?> {
        public TKey? Id { get; init; }

        public OptionalIdResultJson() {
            this.Id = DefaultId();
        }

        protected virtual TKey? DefaultId() {
            var ret = default(TKey?);
            if (ret is null) {
                if (typeof(TKey) == typeof(string)) {
                    ret = (TKey)(object)string.Empty;
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