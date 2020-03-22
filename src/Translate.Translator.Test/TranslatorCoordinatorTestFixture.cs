using NUnit.Framework;

namespace Translate.Translator.Test
{
    [TestFixture]
    public class TranslatorCoordinatorTestFixture
    {
        //const string conversationName = "Numbers";
        //const string conversationName = "Buying a train ticket";
        const string conversationName = "Days of the week";
        [Test]
        public void French()
        {
            var conversation = new ConversationLoader().Load(conversationName);
            var language = new Language() { Name = "French", Code = "fr" };
            new TranslatorCoordinator().Download(new[] { conversation }, new[] { language});
        }

        [Test]
        public void German()
        {
            var conversation = new ConversationLoader().Load(conversationName);
            var language = new Language() { Name = "German", Code = "de" };
            new TranslatorCoordinator().Download(new[] { conversation }, new[] { language });
        }

        [Test]
        public void Bengali()
        {
            var conversation = new ConversationLoader().Load(conversationName);
            var language = new Language() { Name = "Bengali", Code = "bn" };
            new TranslatorCoordinator().Download(new[] { conversation }, new[] { language });
        }
    }
}
