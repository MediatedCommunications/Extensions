namespace System.Text.Json {
    public interface IOptional {
        bool IsPresent { get; }
        object? Value { get; }
    }


}
