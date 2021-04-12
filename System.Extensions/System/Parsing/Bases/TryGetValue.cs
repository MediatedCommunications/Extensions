using System.Diagnostics.CodeAnalysis;

namespace System {
    public delegate bool TryGetValue<TInput, TResult>(TInput Input, [NotNullWhen(true)] out TResult? Result);
}
