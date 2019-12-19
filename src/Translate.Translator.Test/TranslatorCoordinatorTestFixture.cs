using NUnit.Framework;

namespace Translate.Translator.Test
{
    [TestFixture]
    public class TranslatorCoordinatorTestFixture
    {
        [Test]
        public void TestMe()
        {
            var conversation = new ConversationLoader().Load("Buying a train ticket");
            var language = new Language() { Name = "French", Code = "fr" };
            TranslatorCoordinator.Download(new[] { conversation }, new[] { language});
        }
    }
}
