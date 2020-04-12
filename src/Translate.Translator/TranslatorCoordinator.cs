using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

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

                        var translatedLines = TextTranslator.GetTranslatedPhrase(language, conversation, phrase, paddedPhraseCounter);
                        var translatedPhrase = TranslatedPhraseParser.ExtractTranslatedPhrases(translatedLines);

                        var sourceLanguageCode = "en";
                        var targetLanguageCode = language.Code;
                        
                        translatedConversationTextBuilder.AppendLine(translatedPhrase.Target);
                        var urlEncodedTranslatedSourcePhrase = HttpUtility.UrlEncode(translatedPhrase.Source);
                        var urlEncodedTranslatedTargetPhrase = HttpUtility.UrlEncode(translatedPhrase.Target);

                        var mp3InfoSource = AudioConverter.GetAudio(language, conversation, paddedPhraseCounter, sourceLanguageCode, urlEncodedTranslatedSourcePhrase);
                        m3uBuilder.Add(mp3InfoSource);

                        var mp3InfoTarget = AudioConverter.GetAudio(language, conversation, paddedPhraseCounter, targetLanguageCode, urlEncodedTranslatedTargetPhrase);
                        m3uBuilder.Add(mp3InfoTarget);

                        phraseCounter++;
                    }
                    FileWriter.WriteM3u(language, conversation, m3uBuilder);
                    FileWriter.WriteTranslatedText(language, conversation, translatedConversationTextBuilder);
                }
            }
        }
    }
}