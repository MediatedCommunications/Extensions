using System.Reflection;

namespace HarmonyLib {
    public sealed class AlwaysFalse<TThis> : Always<bool, TThis> {

        public AlwaysFalse() : base(false) {
        }

    }

}
