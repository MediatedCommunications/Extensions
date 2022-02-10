using NUnit.Framework;
using System.Diagnostics;
using System.Threading.Tasks;

namespace System
{
    [TestFixture]
    public class DebuggerDisplayTests {

        [Test]
        public Task TestWithoutConverter() {
            var V = new DisplayTester() {
                Id = 1,
                Enabled = false,
                Hidden = false,
                Name = "Name",
                Total = 23.0m,
                Rate = 14.0m,
            };

            var Display = V.GetDebuggerDisplay();


            return Task.CompletedTask;
        }

        public class DisplayTester : DisplayClass {
            public long Id { get; set; }
            public bool Enabled { get; set; }
            public bool Hidden { get; set; }
            public string Name { get; set; } = Strings.Empty;
            public decimal Total { get; set; }
            public decimal Rate { get; set; }


            public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
                return base.GetDebuggerDisplayBuilder(Builder)
                    .Id.Add(Id)
                    .Status.IsEnabled(Enabled)
                    .Status.IsNotVisible(Hidden)
                    .Prefix.Add(Total)
                    .Data.Add(Name)
                    .Postfix.Add(Rate)
                    ;
            }



        }


    }

}
