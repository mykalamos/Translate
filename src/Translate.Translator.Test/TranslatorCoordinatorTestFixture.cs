using NUnit.Framework;

namespace Translate.Translator.Test
{
    [TestFixture]
    public class TranslatorCoordinatorTestFixture
    {
        [Test]
        public void French()
        {
            var conversation = new ConversationLoader().Load("Buying a train ticket");
            var language = new Language() { Name = "French", Code = "fr" };
            TranslatorCoordinator.Download(new[] { conversation }, new[] { language});
        }

        [Test]
        public void German()
        {
            var conversation = new ConversationLoader().Load("Buying a train ticket");
            var language = new Language() { Name = "German", Code = "de" };
            TranslatorCoordinator.Download(new[] { conversation }, new[] { language });
        }

        [Test]
        public void Bengali()
        {
            var conversation = new ConversationLoader().Load("Buying a train ticket");
            var language = new Language() { Name = "Bengali", Code = "bn" };
            TranslatorCoordinator.Download(new[] { conversation }, new[] { language });
        }
    }
}
