using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace System.Text {
    public class Localizer {
        public static Localizer Default { get; } = new();

        protected ImmutableDictionary<Type, string> FriendlyTypeNames { get; }

        public virtual string LocalizeObjectType(object? O) {
            var Type = O.GetTypeSafe();

            var ret = Localize(Type);

            return ret;
        }

        public virtual string Localize<T>() {
            return Localize(typeof(T));
        }

        public virtual string Localize(Type Type) {
            string? ret;
            if(!FriendlyTypeNames.TryGetValue(Type, out ret)) {
                var NewName = Type.GetFriendlyName();
                ret = NewName;
            }

            return ret;
        }


        public Localizer() {
            this.FriendlyTypeNames = CreateFriendlyTypeNames().ToImmutableDictionary();
        }

        protected virtual IDictionary<Type, string> CreateFriendlyTypeNames() {
            var ret = new Dictionary<Type, string>();
            return ret;
        }
    }

}
