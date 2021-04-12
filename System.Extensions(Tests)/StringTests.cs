using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions {

    [TestFixture]
    public class StringTests {

        [Test]
        public Task TestSafeSubstring() {
            var Data = "0123456789";

            var Expected1 = Data.Substring(5);
            var Expected2 = Data.Substring(5, 2);

            Assert.AreEqual(Data.SafeSubstring(5), Expected1);
            Assert.AreEqual(Data.SafeSubstring(5, 100), Expected1);
            Assert.AreEqual(Data.SafeSubstring(5, 2), Expected2);

            Assert.AreEqual(Data.SafeSubstring(5..), Expected1);
            Assert.AreEqual(Data.SafeSubstring(5..105), Expected1);
            Assert.AreEqual(Data.SafeSubstring(5..7), Expected2);

            return Task.CompletedTask;
        }

    }

}
