using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace Translate.Translator
{
    static class AudioProvider
    {
        public static void GetAudio(Language language, Conversation conversation, List<FileInfo> m3uBuilder, StringBuilder translatedConversationTextBuilder, string paddedPhraseCounter, TranslatedPhrase translatedPhrase)
        {
            var sourceLanguageCode = "en";
            var targetLanguageCode = language.Code;

            translatedConversationTextBuilder.AppendLine(translatedPhrase.Target);
            var urlEncodedTranslatedSourcePhrase = HttpUtility.UrlEncode(translatedPhrase.Source);
            var urlEncodedTranslatedTargetPhrase = HttpUtility.UrlEncode(translatedPhrase.Target);

            var mp3InfoSource = AudioConverter.GetAudio(language, conversation, paddedPhraseCounter, sourceLanguageCode, urlEncodedTranslatedSourcePhrase);
            m3uBuilder.Add(mp3InfoSource);

            var mp3InfoTarget = AudioConverter.GetAudio(language, conversation, paddedPhraseCounter, targetLanguageCode, urlEncodedTranslatedTargetPhrase);
            m3uBuilder.Add(mp3InfoTarget);
        }
    }
}