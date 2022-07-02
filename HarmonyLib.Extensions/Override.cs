using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib {
    public static class Override {
        public static void Method(MethodBase OldMethod, MethodInfo NewMethod, [CallerMemberName] string? Caller = default) {
            var Id = new[] {
                Caller,
                $@"{Guid.NewGuid()}"
            }.WhereIsNotBlank().Coalesce();
            
            
            var H = new Harmony(Id);
            var HM = new HarmonyMethod(NewMethod);
            H.Patch(OldMethod, HM);
        }
    }

}
