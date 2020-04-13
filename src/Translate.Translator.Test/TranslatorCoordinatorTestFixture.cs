using NUnit.Framework;

namespace Translate.Translator.Test
{
    [TestFixture]
    public class TranslatorCoordinatorTestFixture
    {
        const string conversationName = "L’école"; 
        const string conversationGroup = "elearningFrench.com";

        [TestCase("Bengali", "bn", TestName = "Generate_Bengali")]
        [TestCase("German", "de",  TestName = "Generate_German")]
        [TestCase("French", "fr",  TestName = "Generate_French")]
        public void TranslateAndAudio(string languageName, string isoCode)
        {
            var conversation = ConversationLoader.Load(conversationName, conversationGroup);
            var language = new Language() { Name = languageName, Code = isoCode };
            new TranslatorCoordinator().TranslateAndAudio(conversation , language );
        }
    }
}
