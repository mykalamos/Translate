using System.IO;

namespace Translate.Translator
{
    public class ConversationLoader
    {
        public Conversation Load(string conversationName)
        {
            var pathFormat = @"C:\Users\Riky\Documents\Code\Translate\Conversations\{0}.txt";
            var path = string.Format(pathFormat, conversationName);
            var phrases = File.ReadAllLines(path);
            return new Conversation(conversationName, phrases);
        }
    }
}