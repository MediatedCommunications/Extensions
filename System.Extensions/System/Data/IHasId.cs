namespace System.Data {
    public interface IHasId<TKey> : IHasId {
        new TKey Id { get; }

        object? IHasId.Id => Id;
    }

    public interface IHasId {
        object? Id { get; }
    }

    public static class HasId {

        public static bool TryGetId<TId>(object? Instance, out TId? Value) {
            var ret = false;
            Value = default;

            if (Instance is IHasId<TId> { } V1) {
                ret = true;
                Value = V1.Id;
            }

            return ret;
        }

        public static bool TryGetId(object? Instance, out object? Value) {
            var ret = false;
            Value = default;

            if(Instance is IHasId { } V1) {
                ret = true;
                Value = V1.Id;
            }

            return ret;
        }
    }

}