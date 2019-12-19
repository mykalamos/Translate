using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace Translate.Translator
{
    public static class TranslatorCoordinator
    {
        private static Regex jsonRegex = new Regex("\"(?<h>[^\"]+)\"", RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static void Download(Conversation[] conversations, Language[] languages)
        {
            foreach (var language in languages)
                foreach (var conversation in conversations)
                {
                    int phraseCounter = 1;
                    foreach (var phrase in conversation.Phrases)
                    {
                        var encodedPhrase = HttpUtility.UrlEncode(phrase);
                        var translateUri = string.Format(Uris.TranslateApi, language.Code, encodedPhrase);
                        var paddedPhraseCounter = phraseCounter.ToString().PadLeft(3, '0');
                        var jsonFileInfo = new FileInfo(
                            @$"C:\Users\Riky\Documents\Code\Translate\Translations\{conversation.Name}\{language.Code}\{paddedPhraseCounter}_{language.Code}.json");
                        if (!jsonFileInfo.Exists)
                        {
                            var r = WebRequest.Create(translateUri);
                            var response = r.GetResponse();

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
                        phraseCounter++;

                        var translatedLines = File.ReadAllLines(jsonFileInfo.FullName);

                        var translatedPhrases = new List<string>();
                        var matches = jsonRegex.Matches(translatedLines[0]);
                        foreach (Match match in matches)
                        {
                            foreach (Group group in match.Groups)
                            {
                                if (group.Name == "0")
                                {
                                    continue;
                                }
                                foreach (Capture capture in group.Captures)
                                {
                                    translatedPhrases.Add(capture.Value);
                                    if (translatedPhrases.Count == 2)
                                    {
                                        break;
                                    }
                                }
                            }
                        }

                        int c = 0;
                        foreach (var translatedPhrase in translatedPhrases)
                        {
                            var lang = (c == 0) ? language.Code : "en";
                            var urlEncodedTranslatedPhrase = HttpUtility.UrlEncode(translatedPhrase);

                            var mp3FileInfo = new FileInfo(
                                @$"C:\Users\Riky\Documents\Code\Translate\Translations\{conversation.Name}\{language.Code}\{paddedPhraseCounter}_{lang}.mp3");
                            if (!mp3FileInfo.Exists)
                            {
                                var voiceUri = string.Format(Uris.VoiceApi, urlEncodedTranslatedPhrase, lang, lang.Length);
                                var r = WebRequest.Create(voiceUri);
                                var response = r.GetResponse();

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
                            c++;
                        }
                    }
                }
        }
    }
}