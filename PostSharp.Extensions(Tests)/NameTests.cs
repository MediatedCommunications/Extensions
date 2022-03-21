using NUnit.Framework;
using PostSharp.Extensions;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace PostSharp.Extensions
{

    public class IgnoreNullObject {
        [IgnoreNull]
        public object? Value { get; set; } = new();

    }

    public class IgnoreFalseObject {
        [IgnoreFalse]
        public bool Value { get; set; } = true;
    }

    public class NameTests {


        [Test]
        public async Task IgnoreNull() {
            var V = new IgnoreNullObject();
            V.Value = null;

            Assert.IsNotNull(V.Value);

        }


        [Test]
        public async Task IgnoreFalse() {
            var V = new IgnoreFalseObject();
            V.Value = false;

            Assert.IsTrue(V.Value);

        }




    }

}
