using System.Diagnostics.CodeAnalysis;
using System.Runtime.Versioning;

namespace System {

    [RequiresPreviewFeatures]
    public static class ShapeExtensions {

        public static bool IsShapeOf<TOutput>(this object? This, [NotNullWhen(true)] out TOutput? Output) where TOutput : IShape<TOutput> {
            var Provider = TOutput.GetShapeProvider();
            return IsShapeOf(This, Provider, out Output);
        }

        public static bool IsShapeOf<TOutput>(this object? This, ShapeProvider<TOutput> Provider, [NotNullWhen(true)] out TOutput? Output) {
            return Provider.IsShape(This, out Output);
        }

        public static TOutput? IsShapeOf<TOutput>(this object? This) where TOutput : IShape<TOutput> {
            var Provider = TOutput.GetShapeProvider();
            var ret = IsShapeOf(This, Provider);
            return ret;
        }

        public static TOutput? IsShapeOf<TOutput>(this object? This, ShapeProvider<TOutput> Provider) {
            return Provider.IsShape(This);
        }

    }

}