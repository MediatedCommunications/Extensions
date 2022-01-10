namespace System.Data {
    public interface IIdResult<TKey> {
        TKey Id { get; }
    }
}