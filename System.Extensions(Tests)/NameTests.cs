using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace System.Extensions
{

    [TestFixture]
    public class NameTests {

        [Test]
        public Task TestNames() {

            var Names = new Dictionary<string, EntityName> {
                [""] = EntityNames.Empty,
                ["John Smith"] = new PersonName() { First = "John", Last = "Smith"},
                ["Mr. John Smith"] = new PersonName() { Prefix = "Mr", First = "John", Last = "Smith" },
                ["John Smith, III"] = new PersonName() { First = "John", Last = "Smith", Suffix = "III" },
                ["John Jacob Smith, III"] = new PersonName() { First = "John", Middle = new[] { "Jacob" }.ToImmutableArray(), Last = "Smith", Suffix = "III" },
                ["Mr. John Smith III"] = new PersonName() { Prefix = "Mr", First = "John", Last = "Smith", Suffix = "III" },
                ["Mr. John Jacob Jingle Smith, III"] = new PersonName() { Prefix = "Mr", First = "John", Middle = new[] { "Jacob", "Jingle" }.ToImmutableArray(), Last = "Smith", Suffix = "III" },

                ["Smith, John "] = new PersonName() { First = "John", Last = "Smith" },
                ["Smith, John Jacob"] = new PersonName() { First = "John", Middle = new[] { "Jacob" }.ToImmutableArray(), Last = "Smith" },
                ["Smith, John Jacob Jingle Sr"] = new PersonName() { First = "John", Middle = new[] { "Jacob", "Jingle" }.ToImmutableArray(), Last = "Smith", Suffix = "Sr" },

                ["John & Mary Smith"] = new CompanyName() {  Name = "John & Mary Smith"},

                ["SuperCorp"] = new CompanyName() { Name = "SuperCorp"},
                ["SuperCorp Partners LLC"] = new CompanyName() { Name = "SuperCorp Partners LLC" }

            };

            foreach (var item in Names) {
                var Text = item.Key;
                var Expected = item.Value;
                
                var Actual = Text.Parse().AsName().GetValue();

                var ExpectedText = Expected.GetDebuggerDisplay();
                var ActualText = Actual.GetDebuggerDisplay();
                
                Assert.AreEqual(ExpectedText, ActualText);


            }

            return Task.CompletedTask;
        }



    }

}
