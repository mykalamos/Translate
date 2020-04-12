using NUnit.Framework;

namespace Translate.Translator.Test
{
    [TestFixture]
    public class TranslatorCoordinatorTestFixture
    {
        const string conversationName = "FullStops";
        const string conversationGroup = "TestHarness";

        [TestCase("Bengali", "bn", TestName = "Generate_Bengali")]
        [TestCase("German", "de",  TestName = "Generate_German")]
        [TestCase("French", "fr",  TestName = "Generate_French")]
        public void TestImpl(string languageName, string isoCode)
        {
            var conversation = new ConversationLoader().Load(conversationName, conversationGroup);
            var language = new Language() { Name = languageName, Code = isoCode };
            new TranslatorCoordinator().Download(new[] { conversation }, new[] { language });
        }
    }
}
