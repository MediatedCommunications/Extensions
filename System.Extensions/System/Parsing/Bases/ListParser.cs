using System.Collections;
using System.Collections.Immutable;

namespace System {
    public abstract record ListParser<T> : DefaultClassParser<ImmutableList<T>> {

        protected override ImmutableList<T> GetDefaultValue() {
            return ImmutableList<T>.Empty;
        }

    }

}
