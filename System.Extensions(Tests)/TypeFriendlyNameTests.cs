using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace System
{

    [TestFixture]
    public class TypeFriendlyNameTests {

        [Test]
        public Task TestWithoutConverter() {

            var Types = new Dictionary<Type, string> {
                { typeof(Dictionary<long, List<string>>[])      , "Dictionary<long, List<string>>[]" },
                { typeof(Dictionary<long, List<string>>[][])    , "Dictionary<long, List<string>>[][]" },
                { typeof(Dictionary<long, List<string>>[,])     , "Dictionary<long, List<string>>[,]" },
                { typeof(string)                                , "string" },
                { typeof(List<string>)                          , "List<string>" },
                { typeof(Dictionary<long, string>)              , "Dictionary<long, string>" },
                { typeof(Dictionary<long, List<string>>)        , "Dictionary<long, List<string>>" },
                { typeof(TypeFriendlyNameTests)                 , "TypeFriendlyNameTests" },
                { typeof(long[])                                , "long[]" },
                { typeof(long?[])                               , "long?[]" },
            };

            foreach (var item in Types) {
                var Name1 = item.Key.GetFriendlyName();
                var Name2 = item.Value;
                Assert.AreEqual(Name1, Name2);
            }

            return Task.CompletedTask;
        }


    }

}
