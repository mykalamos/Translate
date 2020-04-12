using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Translate.Translator
{
    public class TranslatorCoordinator
    {
        public void TranslateAndAudio(Conversation[] conversations, Language[] languages)
        {
            foreach (var language in languages)
            {
                foreach (var conversation in conversations)
                {
                    int phraseCounter = 1;
                    var m3uBuilder = new List<FileInfo>();
                    var translatedConversationTextBuilder = new StringBuilder();
                    foreach (var phrase in conversation.Phrases)
                    {
                        var paddedPhraseCounter = phraseCounter.ToString().PadLeft(3, '0');
                        var translatedPhrase = TranslatedPhraseProvider.GetTranslatedPhrase(language, conversation, phrase, paddedPhraseCounter);

                        AudioProvider.GetAudio(language, conversation, m3uBuilder, translatedConversationTextBuilder, paddedPhraseCounter, translatedPhrase);

                        phraseCounter++;
                    }
                    FileWriter.WriteM3u(language, conversation, m3uBuilder);
                    FileWriter.WriteTranslatedText(language, conversation, translatedConversationTextBuilder);
                }
            }
        }
    }
}