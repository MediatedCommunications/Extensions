namespace System.Data {
    public interface ICustomContent<T> where T : class {
        object? GetNextContent();
    }


}