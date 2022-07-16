using System.Reflection;


namespace HarmonyLib {



    public static class Handlers {

        public static MethodInfo Nothing() {
            return Nothing<object>();
        }

        public static MethodInfo Nothing<TThis>() {
            return ActionHandler<TThis>.Create<Nothing<TThis>>();
        }

        public static MethodInfo AlwaysTrue() {
            return AlwaysTrue<object>();
        }

        public static MethodInfo AlwaysFalse() {
            return AlwaysFalse<object>();
        }

        public static MethodInfo AlwaysTrue<TThis>(){
            return FuncHandler<bool, TThis>.Create<AlwaysTrue<TThis>>();
        }

        public static MethodInfo AlwaysFalse<TThis>() {
            return FuncHandler<bool, TThis>.Create<AlwaysFalse<TThis>>();
        }

    }

}
