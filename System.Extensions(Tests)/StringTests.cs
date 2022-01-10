using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.Extensions
{

    [TestFixture]
    public class StringTests {

        [Test]
        public Task TestSafeSubstring() {
            var Data = "0123456789";

            var Expected1 = Data[5..];
            var Expected2 = Data.Substring(5, 2);

            Assert.AreEqual(Data.SafeSubstring(5), Expected1);
            Assert.AreEqual(Data.SafeSubstring(5, 100), Expected1);
            Assert.AreEqual(Data.SafeSubstring(5, 2), Expected2);

            Assert.AreEqual(Data.SafeSubstring(5..), Expected1);
            Assert.AreEqual(Data.SafeSubstring(5..105), Expected1);
            Assert.AreEqual(Data.SafeSubstring(5..7), Expected2);

            return Task.CompletedTask;
        }

        [Test]
        public Task TestEllipsize() {
            var Text = "This is a test";
            var Start = Text.EllipsizeLeft(7);
            var End = Text.EllipsizeRight(7);
            var Short4 = Text.Ellipsize(4);
            var Short3 = Text.Ellipsize(3);
            var Short2 = Text.Ellipsize(2);
            var Short1 = Text.Ellipsize(1);
            var Short0 = Text.Ellipsize(0);

            Assert.AreEqual(Start, "...test");
            Assert.AreEqual(End, "This...");

            Assert.AreEqual(Short4, "T...");
            Assert.AreEqual(Short3, "...");
            Assert.AreEqual(Short2, "..");
            Assert.AreEqual(Short1, ".");
            Assert.AreEqual(Short0, "");

            return Task.CompletedTask;
        }

        [Test]
        public Task LastIndexOfAny() {
            var Tests = new Dictionary<string, int[]>() {
                { "this\nis\na\ntest", new[] { 9, 7, 4 }},
                { "this\r\nis\r\na\r\ntest", new[] { 11, 8, 4 }},
                { "XXX\r\nXXX\r\nXXX\r\n\r\n", new[]{ 15, 13, 8, 3} },
            };

            foreach (var (Input, Expected) in Tests) {
                var Indexes = Input.LastIndexOfAny(Strings.NewLines).ToList();
                var Actual = Indexes.Select(x => x.Item2).ToArray();

                Assert.AreEqual(Expected, Actual);

            }


            return Task.CompletedTask;
        }

    }

}
