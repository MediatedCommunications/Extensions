using System.Reflection;


namespace HarmonyLib {

    public abstract class ActionHandler<TThis> {

        protected abstract void Invoke();
        protected TThis? This { get; private set; }

        public static MethodInfo Create<THandler>() where THandler : ActionHandler<TThis>, new() {
            var MethodBase = AccessTools.Method(typeof(ActionHandler<TThis>), nameof(ProcessRequest));
            var GenericMethod = MethodBase.MakeGenericMethod(typeof(THandler));

            var ret = GenericMethod;

            return ret;
        }

        private static bool ProcessRequest<THandler>(TThis? __instance) where THandler : ActionHandler<TThis>, new() {
            var Handler = new THandler()
            {
                This = __instance,
            };

            Handler.Invoke();

            return false;
        }
    }

}
