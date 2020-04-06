using System.IO;

namespace Translate.Translator
{
    public class ConversationLoader
    {
        public Conversation Load(string conversationName, string conversationGroup)
        {
            var pathFormat = @"C:\Users\Riky\Documents\Code\Translate\Conversations\{0}\{1}.txt";
            var path = PathUtils.RemoveEmptyDirectory(string.Format(pathFormat, conversationGroup, conversationName));
            var phrases = File.ReadAllLines(path);
            return new Conversation(conversationName, conversationGroup, phrases);
        }
    }
}