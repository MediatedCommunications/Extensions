using System.Reflection;

namespace HarmonyLib {
    public sealed class AlwaysTrue<TThis> : Always<bool, TThis> {
        public AlwaysTrue() : base(true) {
        }

    }

}
