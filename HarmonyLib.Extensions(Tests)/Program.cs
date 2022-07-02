using NUnit.Framework;

namespace HarmonyLib.Extensions.Tests {

    [TestFixture]
    public class Test {

        [Test]
        public void StaticTestClass_CanOverride() { 
            var V1 = StaticTestClass.AlwaysFalse();

            var H = new Harmony("MyTest");
            var Original = AccessTools.Method(typeof(StaticTestClass), nameof(StaticTestClass.AlwaysFalse));
            var Replace = Handlers.AlwaysTrue();

            var HM = new HarmonyMethod(Replace);


            H.Patch(Original, HM);

            var V2 = StaticTestClass.AlwaysFalse();

            Assert.IsFalse(V1);
            Assert.IsTrue(V2);

        }

        [Test]
        public void InstanceTestClass_CanOverride() {
            var Instance = new InstanceTestClass();
            var V1 = Instance.AlwaysFalse();

            var H = new Harmony("MyTest");
            var Original = AccessTools.Method(typeof(InstanceTestClass), nameof(StaticTestClass.AlwaysFalse));
            var Replace = Handlers.AlwaysTrue<InstanceTestClass>();

            var HM = new HarmonyMethod(Replace);


            H.Patch(Original, HM);

            var V2 = Instance.AlwaysFalse();

            Assert.IsFalse(V1);
            Assert.IsTrue(V2);

        }
    }

    public static class StaticTestClass {
        public static string Name { get; set; } = "My Name";
        public static bool AlwaysFalse() {
            return false;
        }
    }

    public class InstanceTestClass {
        public string Name { get; set; } = "My Name";
        public bool AlwaysFalse() {
            return false;
        }
    }

}