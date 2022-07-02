using System.Reflection;


namespace HarmonyLib {

    public abstract class FuncHandler<TValue, TThis> {

        protected abstract TValue Invoke();
        protected TThis? This { get; private set; }

        public static MethodInfo Create<THandler>() where THandler : FuncHandler<TValue, TThis>, new() {
            var MethodBase = AccessTools.Method(typeof(FuncHandler<TValue, TThis>), nameof(ProcessRequest));
            var GenericMethod = MethodBase.MakeGenericMethod(typeof(THandler));

            var ret = GenericMethod;

            return ret;
        }

        private static bool ProcessRequest<THandler>(TThis? __instance, ref TValue __result) where THandler : FuncHandler<TValue, TThis>, new() {
            var Handler = new THandler()
            {
                This = __instance,
            };

            __result = Handler.Invoke();

            return false;
        }
    }

}
