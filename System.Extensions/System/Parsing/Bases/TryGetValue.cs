using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System {
    public delegate bool TryGetValue<TInput, TResult>(TInput Input, [NotNullWhen(true)] out TResult? Result);


    //public static class TryGetValueExtensions {
    //    public static TryGetResult<TResult>? TryGetValue<TInput, TResult>(this TryGetValue<TInput, TResult> This, TInput Input) {
    //        var ret = default(TryGetResult<TResult>?);
    //
    //        if(This(Input, out var Result)) {
    //            ret = new TryGetResult<TResult>(Result);
    //        }
    //
    //        return ret;
    //    }
    //}
    //
    //public record TryGetResult : DisplayRecord {
    //    
    //}
    //
    //public record TryGetResult<TResult> : DisplayRecord {
    //
    //    public TResult Value { get; init; }
    //
    //    public TryGetResult(TResult Value) {
    //        this.Value = Value;
    //    }
    //}

}
