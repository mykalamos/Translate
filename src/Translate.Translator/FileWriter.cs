using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Translate.Translator
{
    static class FileWriter {
        public static void WriteM3u(Language language, Conversation conversation, List<FileInfo> m3uBuilder)
        {
            var m3uFileInfo = new FileInfo(
               PathUtils.RemoveEmptyDirectory(@$"{TranslationPaths.RootDirectory}\{conversation.Group}\{conversation.Name}\{language.Code}\{conversation.Name}_{language.Code}.m3u"));
            if (!m3uFileInfo.Exists)
            {
                m3uFileInfo.Directory.Create();
                File.WriteAllLines(m3uFileInfo.FullName, m3uBuilder.Select(x => x.FullName).ToArray());
            }
        }

        public static void WriteTranslatedText(Language language, Conversation conversation, StringBuilder stringBuilder)
        {
            var translatedTFileInfo = new FileInfo(
                PathUtils.RemoveEmptyDirectory(@$"{TranslationPaths.RootDirectory}\{conversation.Group}\{conversation.Name}\{language.Code}\{conversation.Name}_{language.Code}.txt"));
            if (!translatedTFileInfo.Exists)
            {
                translatedTFileInfo.Directory.Create();
                File.WriteAllText(translatedTFileInfo.FullName, stringBuilder.ToString());
            }
        }
    }

}