using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace System.Extensions
{
    [TestFixture]
    public class FileSystemTests {
        [Test]
        public Task TestAttributes() {
            var Drive = FileSystem.GetAttributesFromPath(@"C:\");
            var Folder = FileSystem.GetAttributesFromPath(@"C:\Temp\");
            var File = FileSystem.GetAttributesFromPath(@"C:\DBs\Needles.log");


            return Task.CompletedTask;
        }
    }
}