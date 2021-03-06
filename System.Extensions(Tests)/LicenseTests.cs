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

            //var License = Engine.CreateInternal(Feature1);
            //
            //var Feature2 = Engine.Load(License);
            //
            //Assert.AreEqual(Feature1, Feature2);

            return Task.CompletedTask;
        }
    }

    public class MyLicenseEngine : LicenseEngine<TestLicense> {

    }

    public record TestLicense : DisplayRecord {
        public string Id { get; init; } = Strings.Empty;
        public string Owner { get; init; } = Strings.Empty;
        public DateTimeOffset From { get; init; }
        public DateTimeOffset Till { get; init; }
    }

}
