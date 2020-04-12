using System.IO;
using System.Net;
using System.Threading;

namespace Translate.Translator
{
    static class AudioConverter 
    {
        public static FileInfo GetAudio(Language language, Conversation conversation, string paddedPhraseCounter, string lang, string urlEncodedTranslatedPhrase)
        {
            var mp3FileInfo = new FileInfo(
               PathUtils.RemoveEmptyDirectory(@$"{TranslationPaths.RootDirectory}\{conversation.Group}\{conversation.Name}\{language.Code}\{paddedPhraseCounter}_{lang}.mp3"));
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
    }

}