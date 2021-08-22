using NUnit.Framework;
using System.Dynamic;
using System.Threading.Tasks;

namespace Framework.Tests
{
    [TestFixture]
    public class ExpandoObjectTests {

        [Test]
        public Task Test1() {

            var C = new ExpandoObject()
                .Merge(new {
                    Name = "Tony",
                    Age = 21,
                    Numbers = new[] {1,2,3,4,5 },
                    Strings = new[] {"A", "B", "C", },
                    Wife = new {
                        Name = "Bethany"
                    },
                    Children = new[] {
                        new {
                            Name = "Maya"
                        }, 
                        new {
                            Name = "Aurora",
                        }

                    }
                });


            return Task.CompletedTask;
        }


    }


}
