using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Licensing;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions.Tests
{
    [TestFixture]
    public class Base64EncodingTests {

        [Test]
        public Task GetValidSections() {
            var InputItems = new[] {
                "",
                "FF",
                "FF!",
                "FF!FF",
                "FF!FF!",

                "!",
                "!FF",
                "!FF!",
                "!FF!FF",
                "!FF!FF!",
                
                "!!",
                "!!FF",
                "!!FF!!",
                "!!FF!!FF",
                "!!FF!!FF!!",

                "\n",
                "\nFF",
                "\nFF!\n",
                "\nFF!\nFF",
                "\nFF!\nFF!\n",

            };

            var Item0 = Array.Empty<string>();
            var Item1 = new[] { "FF" };
            var Item2 = new[] { "FF", "FF", };

            var ExpectedItems = new List<string[]>() {
               Item0, Item1, Item1, Item2, Item2,
               Item0, Item1, Item1, Item2, Item2,
               Item0, Item1, Item1, Item2, Item2,
               Item0, Item1, Item1, Item2, Item2,
            };

            for (var i = 0; i < InputItems.Length; i++) {
                var Input = InputItems[i];
                var Actual = Base64Encoding.GetValidSections(Input).ToList();
                var Expected = ExpectedItems[i];

                Assert.AreEqual(Expected, Actual);
            }

            return Task.CompletedTask;
        }
    }


}
