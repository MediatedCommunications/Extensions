using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace System.Text.Json.Serialization
{

    [TestFixture]
    public class OptionalTests {

        private Person TestPerson() {
            var ret = new Person() {
                Id = 24,
                FirstName = "John",
                //Middle Name intentionally not set.
                LastName = null,
                Children = new[] {
                    new Person() {
                        Id = 21,
                        FirstName = "Jane"
                    },
                    new Person() {
                        Id = 0,
                        FirstName = "James",
                    }
                }.ToImmutableList(),
            };

            return ret;
        }
      



        [Test]
        public Task RoundTrip() {

            var Input = TestPerson();

            var InputText = JsonSerializer.Serialize(Input);
            var Output = JsonSerializer.Deserialize<Person>(InputText);
            var OutputText = JsonSerializer.Serialize(Output);

            Assert.AreEqual(InputText, OutputText);

            return Task.CompletedTask;
        }


        [Test]
        public Task Values_Are_Not_Nodes() {
            var Input = TestPerson();
            var InputText = JsonSerializer.Serialize(Input);
            var Output = JsonSerializer.Deserialize<Dictionary<string, object>>(InputText);

            Assert.IsNotNull(Output);
            if (Output is { }) {
                //Ensure that the values exist
                Assert.IsTrue(Output.ContainsKey(nameof(Person.Id)));
                Assert.IsTrue(Output.ContainsKey(nameof(Person.FirstName)));
                Assert.IsTrue(Output.ContainsKey(nameof(Person.LastName)));
                Assert.IsTrue(Output.ContainsKey(nameof(Person.Children)));
                
                Assert.IsFalse(Output.ContainsKey(nameof(Person.MiddleName)));
            }

            if(Output is { }) {
                //And that they got deserialized right
                Assert.AreEqual(((JsonElement)Output[nameof(Person.Id)]).GetInt32(), Input.Id.Value);
                Assert.AreEqual(((JsonElement)Output[nameof(Person.FirstName)]).GetString(), Input.FirstName.Value);
                Assert.AreEqual(Output[nameof(Person.LastName)], Input.LastName.Value);
            }


            return Task.CompletedTask;
        }

        [Test]
        public Task Derived_Types_Deserialize_Correctly() {

            var Input = new SuperPerson() {
                SuperHeroName = "Batman"
            };

            var InputText = JsonSerializer.Serialize(Input);
            var Output = JsonSerializer.Deserialize<Dictionary<string, object>>(InputText);
            
            Assert.IsNotNull(Output);
            
            if (Output is { }) {
                Assert.AreEqual(((JsonElement)Output[nameof(SuperPerson.SuperHeroName)]).GetString(), Input.SuperHeroName.Value);
            }

            return Task.CompletedTask;
        }

        public record SuperPerson : Person {
            public Optional<string?> SuperHeroName { get; init; }
        }

        [OptionalJsonIgnore]
        public record Person {
            public Optional<long?> Id { get; init; }
            public Optional<string?> FirstName { get; init; }
            public Optional<string?> MiddleName { get; init; }
            public Optional<string?> LastName { get; init; }
            public Optional<int> Age { get; init; }
            public Optional<ImmutableList<Person>> Children { get; init; }
            public Optional<string?> NEVER_SET_THIS_PROPERTY { get; init; }
        }


    }

}
