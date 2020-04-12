using System.IO;
using System.Net;
using System.Threading;
using System.Web;

namespace Translate.Translator
{
    static class TextTranslator
    {
        public static string[] GetTranslatedPhrase(Language language, Conversation conversation, string phrase, string paddedPhraseCounter)
        {
            var encodedPhrase = HttpUtility.UrlEncode(phrase);
            var translateUri = string.Format(Uris.TranslateApi, language.Code, encodedPhrase);
            var jsonFileInfo = new FileInfo(
                PathUtils.RemoveEmptyDirectory(@$"{TranslationPaths.RootDirectory}\{conversation.Group}\{conversation.Name}\{language.Code}\{paddedPhraseCounter}_{language.Code}.json"));
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