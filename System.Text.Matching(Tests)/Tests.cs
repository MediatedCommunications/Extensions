using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace System.Text.Matching
{

    [TestFixture]
    public class Tests {

        
        [Test]
        public Task TestWithoutConverter() {
            var Name = "John Smith";
            var Options = new DefaultTokenizerOptions() {
            };

            var Tokenizer = new DefaultTokenizer(Options);

            var MatchOptions = new DefaultPhraseMatchProviderOptions() {
                Providers = new MatchProvider?[] {
                        ExactMatchProvider.Instance,
                        CambridgeMatchProvider.Instance,
                        HammingMatchProvider.Instance,
                        SoundsLikeMatchProvider.Instance,
                }.WhereIsNotNull().ToImmutableList()
            };

            var MatchProvider = new DefaultPhraseMatchProvider(MatchOptions);

            var Words1 = Tokenizer.Tokenize(Name);
            var Words2 = Tokenizer.Tokenize(Name);

            var Match = MatchProvider.Match(Words1, Words2, StringComparer.InvariantCultureIgnoreCase);

            Match.Ignore();


            return Task.CompletedTask;
        }

    }

}
