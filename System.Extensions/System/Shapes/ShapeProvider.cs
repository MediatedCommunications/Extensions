using System.Diagnostics.CodeAnalysis;
using System.Runtime.Versioning;

namespace System {

    
    public interface IShape<TOutput> where TOutput : IShape<TOutput> {
        [RequiresPreviewFeatures]
        static abstract ShapeProvider<TOutput> GetShapeProvider();
    }

    public abstract class ShapeProvider<TOutput> {
        public abstract bool IsShape(object? Input, [NotNullWhen(true)] out TOutput? Output);

        public TOutput? IsShape(object? Input) {
            var ret = default(TOutput?);

            if (IsShape(Input, out var tret)) {
                ret = tret;
            }

            return ret;
        }

    }

}