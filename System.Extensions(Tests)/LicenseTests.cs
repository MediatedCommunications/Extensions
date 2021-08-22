using NUnit.Framework;
using System.Diagnostics;
using System.Security.Licensing;
using System.Threading.Tasks;

namespace System.Extensions
{
    [TestFixture]
    public class LicenseTests {

        [Test]
        public Task Test1() {
            var Engine = new MyLicenseEngine();
            var Feature1 = new TestLicense() {
                Owner = "Tony Valenti",
                Id = "Abacus",
                From = DateTimeOffset.Now.AddDays(-1),
                Till = DateTimeOffset.Now.AddDays(+1),

            };

            var License = Engine.CreateInternal(Feature1);

            var Feature2 = Engine.Load(License);

            Assert.AreEqual(Feature1, Feature2);

            return Task.CompletedTask;
        }
    }

    public class MyLicenseEngine : LicenseEngine<TestLicense> {
        internal string CreateInternal(TestLicense License) {
            return this.Create(License);
        }
    }

    public record TestLicense : DisplayRecord {
        public string Id { get; init; } = string.Empty;
        public string Owner { get; init; } = string.Empty;
        public DateTimeOffset From { get; init; }
        public DateTimeOffset Till { get; init; }
    }

}
