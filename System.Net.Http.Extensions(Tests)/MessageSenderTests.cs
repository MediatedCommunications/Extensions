using NUnit.Framework;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Net.Http.Message.Senders;
using System.Net.Http.Message;

namespace System.Net.Http {
    [TestFixture]
    public class MessageSenderTests {

        [Test]
        public Task TestChaining() {

            var C = HttpMessageSenders.Default
                .RetryOnHttpException()
                .RetryOnHttpStatusErrors()
                .SendUsingHttpClient()
                ;

            C.Ignore();

            return Task.CompletedTask;
        }


    }


}
