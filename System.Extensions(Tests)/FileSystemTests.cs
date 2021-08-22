using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace System.Extensions
{
    [TestFixture]
    public class FileSystemTests {
        [Test]
        public Task TestAttributes() {
            var Drive = FileSystem.GetAttributes(@"C:\");
            var Folder = FileSystem.GetAttributes(@"C:\Temp\");
            var File = FileSystem.GetAttributes(@"C:\DBs\Needles.log");


            return Task.CompletedTask;
        }
    }
}