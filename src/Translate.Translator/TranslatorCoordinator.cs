using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace Translate.Translator
{
    public class TranslatorCoordinator
    {

        private const string rootDirectory = @"C:\Users\Riky\Documents\Code\Translate\Translations"; // TODO make this relative

        public void Download(Conversation[] conversations, Language[] languages)
        {
            foreach (var language in languages)
                foreach (var conversation in conversations)
                {
                    int phraseCounter = 1;
                    var m3uBuilder = new List<FileInfo>();
                    var translatedConversationTextBuilder = new StringBuilder();
                    foreach (var phrase in conversation.Phrases)
                    {
                        var paddedPhraseCounter = phraseCounter.ToString().PadLeft(3, '0');

                        var translatedLines = GetTranslatedPhrase(language, conversation, phrase, paddedPhraseCounter);
                        var translatedPhrases = TranslatedPhraseParser.MatchTranslatedPhrases(translatedLines);

                        int c = 0;
                        foreach (var translatedPhrase in translatedPhrases)
                        {
                            var lang = (c == 0) ? "en" : language.Code;
                            if (c == 1)
                                translatedConversationTextBuilder.AppendLine(translatedPhrase);
                            var urlEncodedTranslatedPhrase = HttpUtility.UrlEncode(translatedPhrase);

                            var mp3Info = GetAudio(language, conversation, paddedPhraseCounter, lang, urlEncodedTranslatedPhrase);
                            m3uBuilder.Add(mp3Info);
                            c++;
                        }
                        phraseCounter++;
                    }
                    WriteM3u(language, conversation, m3uBuilder);
                    WriteTranslatedText(language, conversation, translatedConversationTextBuilder);
                }
        }

        private static void WriteM3u(Language language, Conversation conversation, List<FileInfo> m3uBuilder)
        {
            var m3uFileInfo = new FileInfo(
               PathUtils.RemoveEmptyDirectory(@$"{rootDirectory}\{conversation.Group}\{conversation.Name}\{language.Code}\{conversation.Name}_{language.Code}.m3u"));
            if (!m3uFileInfo.Exists)
            {
                m3uFileInfo.Directory.Create();
                File.WriteAllLines(m3uFileInfo.FullName, m3uBuilder.Select(x => x.FullName).ToArray());
            }
        }

        private static void WriteTranslatedText(Language language, Conversation conversation, StringBuilder stringBuilder)
        {
            var translatedTFileInfo = new FileInfo(
                PathUtils.RemoveEmptyDirectory(@$"{rootDirectory}\{conversation.Group}\{conversation.Name}\{language.Code}\{conversation.Name}_{language.Code}.txt"));
            if (!translatedTFileInfo.Exists)
            {
                translatedTFileInfo.Directory.Create();
                File.WriteAllText(translatedTFileInfo.FullName, stringBuilder.ToString());
            }
        }

        private static FileInfo GetAudio(Language language, Conversation conversation, string paddedPhraseCounter, string lang, string urlEncodedTranslatedPhrase)
        {
            var mp3FileInfo = new FileInfo(
               PathUtils.RemoveEmptyDirectory(@$"{rootDirectory}\{conversation.Group}\{conversation.Name}\{language.Code}\{paddedPhraseCounter}_{lang}.mp3"));
            if (!mp3FileInfo.Exists)
            {
                var voiceUri = string.Format(Uris.VoiceApi, urlEncodedTranslatedPhrase, lang, lang.Length);
                var request = WebRequest.Create(voiceUri);
                var response = request.GetResponse();

                using (var stream = response.GetResponseStream())
                {
                    mp3FileInfo.Directory.Create();
                    using (var fs = new FileStream(mp3FileInfo.FullName, FileMode.Create))
                    {
                        stream.CopyTo(fs);
                    }
                }
                Thread.Sleep(5000);
            }
            return mp3FileInfo;

        }

 
        private static string[] GetTranslatedPhrase(Language language, Conversation conversation, string phrase, string paddedPhraseCounter)
        {
            var encodedPhrase = HttpUtility.UrlEncode(phrase);
            var translateUri = string.Format(Uris.TranslateApi, language.Code, encodedPhrase);
            var jsonFileInfo = new FileInfo(
                PathUtils.RemoveEmptyDirectory(@$"{rootDirectory}\{conversation.Group}\{conversation.Name}\{language.Code}\{paddedPhraseCounter}_{language.Code}.json"));
            if (!jsonFileInfo.Exists)
            {
                var request = WebRequest.Create(translateUri);
                var response = request.GetResponse();

                using (var stream = response.GetResponseStream())
                {
                    jsonFileInfo.Directory.Create();
                    using (var fs = new FileStream(jsonFileInfo.FullName, FileMode.Create))
                    {
                        stream.CopyTo(fs);
                    }
                }
                Thread.Sleep(5000);
            }

            var translatedLines = File.ReadAllLines(jsonFileInfo.FullName);
            return translatedLines;
        }
    }
}