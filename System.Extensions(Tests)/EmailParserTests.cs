using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Tests
{
    [TestFixture]
    public class EmailParserTests {

        [Test]
        public Task TestEmailParsing() {
            var Tests = new Dictionary<string, IEnumerable<string>> {
                ["John@SmithLaw.com"] = new string[] { "John@SmithLaw.com" },
                ["John@SmithLaw..com"] = Array.Empty<string>(),
                ["John.@SmithLaw.com"] = Array.Empty<string>(),
            };

            foreach (var Test in Tests) {
                TestEmailParsing(Test.Key, Test.Value);
            }

            return Task.CompletedTask;
        }

        private static void TestEmailParsing(string Input, IEnumerable<string> Expected) {
            var V1 = Input.Parse().AsEmails().Select(x => x.Address).ToArray();
            var V2 = Expected.ToArray();


            Assert.AreEqual(V1, V2);
        }


        [Test]
        public Task TestPhoneNumber1() {


            var Tests = new Dictionary<string, PhoneNumber?>() {
                ["+1 123 456 7890"] 
                = new PhoneNumber() { CountryCode = "1", Number = "123-456-7890", Extension = "" },
                
                ["+1 123 456 7890 x 555"] 
                = new PhoneNumber() { CountryCode = "1", Number = "123-456-7890", Extension = "555" },
                
                ["123 456 7890"] 
                = new PhoneNumber() { CountryCode = "", Number = "123-456-7890", Extension = "" },
                
                ["123 456 7890 x 5555"] 
                = new PhoneNumber() { CountryCode = "", Number = "123-456-7890", Extension = "5555" },
                
                ["+44 07769187685"]
                = new PhoneNumber() { CountryCode = "44", Number = "077-6918-7685", Extension = "" },

                ["+4407769187685"]
                = new PhoneNumber() { CountryCode = "44", Number = "077-6918-7685", Extension = "" },

                ["  ( ) - "] 
                = default,
            };

            foreach (var Test in Tests) {
                var NumberToTest = Test.Key;
                var Expected = Test.Value;

                var Value = NumberToTest.Parse().AsPhoneNumber().TryGetValue();

                Assert.AreEqual(Expected, Value);

            }

            return Task.CompletedTask;
        }


    }


}
