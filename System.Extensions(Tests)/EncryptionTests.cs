using NUnit.Framework;
using System.Security;
using System.Threading.Tasks;

namespace System.Extensions
{
    [TestFixture]
    public class EncryptionTests {

        [Test]
        public Task Test1() {
            var OriginalPassword = "12345";
            var OriginalText = "This is a test";
            var OriginalBytes = Text.Encoding.UTF8.GetBytes(OriginalText);
            var OriginalSalt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, };

            for (var i = 0; i < OriginalPassword.Length; i++) {
                var Password = OriginalPassword[0..i];

                for (var j = 0; j < OriginalText.Length; j++) {
                    var Text = OriginalText[0..j];
                    var Bytes = System.Text.Encoding.UTF8.GetBytes(Text);


                    for (var k = 0; k < OriginalSalt.Length; k++) {
                        var Salt = OriginalSalt[0..k];

                        var MyAES = new AesEncryptor(Password, Salt);

                        {
                            var Encrypted = MyAES.Encrypt(Bytes);
                            var Decrypted = MyAES.Decrypt(Encrypted);

                            Assert.AreEqual(Text, Decrypted);
                        }

                        {
                            var Encrypted = MyAES.Encrypt(Text);
                            var Decrypted = MyAES.Decrypt(Encrypted);

                            Assert.AreEqual(Text, Decrypted);
                        }

                    }

                }

                
                
            }




           

            return Task.CompletedTask;
        }
    }

}
