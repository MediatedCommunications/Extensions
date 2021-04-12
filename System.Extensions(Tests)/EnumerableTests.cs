using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions {

    [TestFixture]
    public class EnumerableTests {

        [Test]
        public Task TestRange() {
            static void TestRange(int[] Items, Range R) {
                var V1 = Items[R].ToList();
                var V2 = Items.GetRange(R).ToList();
                Assert.AreEqual(V1, V2);
            }

            var Items = new[] { 1, 2, 3, 4 };

            TestRange(Items, 1..2);
            TestRange(Items, 1..^1);


            return Task.CompletedTask;
        }

    }

}
