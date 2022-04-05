namespace System.Text.Json {
    public interface IOptional {
        bool IsPresent { get; }
        bool IsMissing { get; }
        object? Value { get; }
    }


}
