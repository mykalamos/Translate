using System.IO;

namespace Translate.Translator
{
    public static class ConversationLoader
    {
        public static Conversation Load(string conversationName, string conversationGroup)
        {
            var pathFormat = @"C:\Users\Riky\Documents\Code\Translate\Conversations\{0}\{1}.txt";
            var path = PathUtils.RemoveEmptyDirectory(string.Format(pathFormat, conversationGroup, conversationName));
            var phrases = File.ReadAllLines(path);
            return new Conversation(conversationName, conversationGroup, phrases);
        }

        public static Conversation LoadByLanguage(Conversation conversation, Language language)
        {
            var pathFormat = @"C:\Users\Riky\Documents\Code\Translate\Conversations\{0}\{1}_{2}.txt";
            var path = PathUtils.RemoveEmptyDirectory(
                    string.Format(pathFormat, conversation.Group, conversation.Name, language.Code));
            if (File.Exists(path))
            {
                var phrases = File.ReadAllLines(path);
                return new Conversation(conversation.Name, conversation.Group, phrases);
            }
            return null;
        }
    }
}