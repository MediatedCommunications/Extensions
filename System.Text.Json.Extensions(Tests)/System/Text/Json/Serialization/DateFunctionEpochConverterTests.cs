using NUnit.Framework;
using System.Threading.Tasks;

namespace System.Text.Json.Serialization {

    [TestFixture]
    public class DateFunctionEpochConverterTests {

        record OptionalDateProperty {
            public DateTimeOffset? Date { get; set; }
        }

        record RequiredDateProperty {
            public DateTimeOffset Date { get; set; }
        }

        DateTimeOffset Now => new DateTimeOffset(2020, 1, 2, 3, 4, 5, TimeSpan.FromHours(6));

        static JsonSerializerOptions WithConverter() {
            var ret = new JsonSerializerOptions() {
                Converters = {
                   new DateFunctionEpochConverter(),
               }
            };
            return ret;
        }
        
        static JsonSerializerOptions WithoutConverter() {
            var ret = new JsonSerializerOptions() {

            };

            return ret;
        }

        [Test]
        public Task TestWithoutConverter() {
            {
                var V1 = new OptionalDateProperty { Date = default };
                var Content = JsonSerializer.Serialize(V1, WithoutConverter());

                var V2 = JsonSerializer.Deserialize<OptionalDateProperty>(Content, WithConverter());

                Assert.AreEqual(V1.Date, V2?.Date);
            }

            {
                var V1 = new OptionalDateProperty { Date = Now };
                var Content = JsonSerializer.Serialize(V1, WithoutConverter());

                var V2 = JsonSerializer.Deserialize<OptionalDateProperty>(Content, WithConverter());

                Assert.AreEqual(V1.Date, V2?.Date);
            }

            {
                var V1 = new RequiredDateProperty { Date = default };
                var Content = JsonSerializer.Serialize(V1, WithoutConverter());

                var V2 = JsonSerializer.Deserialize<RequiredDateProperty>(Content, WithConverter());

                Assert.AreEqual(V1.Date, V2?.Date);
            }

            {
                var V1 = new RequiredDateProperty { Date = Now };
                var Content = JsonSerializer.Serialize(V1, WithoutConverter());

                var V2 = JsonSerializer.Deserialize<RequiredDateProperty>(Content, WithConverter());

                Assert.AreEqual(V1.Date, V2?.Date);
            }

            return Task.CompletedTask;
        }
    

        [Test]
        public Task Test1() {

            var Options = WithConverter();
            

            {
                var V1 = new OptionalDateProperty { Date = default };
                var Content = JsonSerializer.Serialize(V1, Options);

                var V2 = JsonSerializer.Deserialize<OptionalDateProperty>(Content, Options);
                
                Assert.AreEqual(V1.Date, V2?.Date);
                Assert.IsFalse(Content.AsText().Contains("date("));
                
            }

            {
                var V1 = new OptionalDateProperty { Date = Now };
                var Content = JsonSerializer.Serialize(V1, Options);

                var V2 = JsonSerializer.Deserialize<OptionalDateProperty>(Content, Options);

                Assert.AreEqual(V1.Date, V2?.Date);
                Assert.IsTrue(Content.AsText().Contains("date("));
            }

            {
                var V1 = new RequiredDateProperty { Date = default };
                var Content = JsonSerializer.Serialize(V1, Options);

                var V2 = JsonSerializer.Deserialize<RequiredDateProperty>(Content, Options);

                Assert.AreEqual(V1.Date, V2?.Date);
                Assert.IsTrue(Content.AsText().Contains("date("));
            }

            {
                var V1 = new RequiredDateProperty { Date = Now };
                var Content = JsonSerializer.Serialize(V1, Options);

                var V2 = JsonSerializer.Deserialize<RequiredDateProperty>(Content, Options);

                Assert.AreEqual(V1.Date, V2?.Date);
                Assert.IsTrue(Content.AsText().Contains("date("));
            }


            return Task.CompletedTask;
        }

    }

}
