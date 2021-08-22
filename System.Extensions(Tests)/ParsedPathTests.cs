using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Framework.Tests
{

    [TestFixture]
    public class ParsedPathTests {



        [Test]
        public Task TestParsedPAths() {
            var Values = new[] {
                PathParser.FromPath(null),
                PathParser.FromPath(@""),
                PathParser.FromPath(@"C:"),
                PathParser.FromPath(@"C:\"),
                PathParser.FromPath(@"C:\Something"),
                PathParser.FromPath(@"C:\Something\"),
                PathParser.FromPath(@"C:\Something\Else\"), 
                PathParser.FromPath(@"C:\Something\Else"),
                PathParser.FromPath(@"C:\Something\Else.txt"),
                PathParser.FromPath(@"\\Server"),
                PathParser.FromPath(@"\\Server\"),
                PathParser.FromPath(@"\\Server\Something"),
                PathParser.FromPath(@"\\Server\Something\"),
                PathParser.FromPath(@"\\Server\Something\Else\"),
                PathParser.FromPath(@"\\Server\Something\Else"),
                PathParser.FromPath(@"\\Server\Something\Else.txt"),
                PathParser.FromPath(@"Else\"),
                PathParser.FromPath(@"Else"),
                PathParser.FromPath(@"Else.txt"),
                
            };

            return Task.CompletedTask;
        }

    }


}
