using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.Extensions {

    [TestFixture]
    public class RangeTests {

        [Test]
        public Task GetEnumerator() {

            {
                var Expected = new[] { 1, 2 };
                var Actual = (1..3).AsEnumerable().ToList();
                
                Assert.AreEqual(Expected, Actual);
            }

            {
                var Expected = new[] { 1, 2, 3 };
                var Actual = (1..3).AsEnumerable().EndIs(RangeEndpoint.Inclusive).ToList();

                Assert.AreEqual(Expected, Actual);
            }

            {
                var Expected = new[] { 3, 2 };
                var Actual = (3..1).AsEnumerable().ToList();

                Assert.AreEqual(Expected, Actual);
            }

            {
                var Expected = new[] { 3, 2, 1 };
                var Actual = (3..1).AsEnumerable().EndIs(RangeEndpoint.Inclusive).ToList();

                Assert.AreEqual(Expected, Actual);
            }


            return Task.CompletedTask;
        }

    }

}
