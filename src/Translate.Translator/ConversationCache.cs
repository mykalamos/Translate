using System.Collections.Generic;

namespace Translate.Translator
{
    static class ConversationCache { 
        
        public static string GetTranslation(Conversation conversation, Language language , string phrase)
        {
            var preexistingTranslation = ConversationLoader.LoadByLanguage(conversation, language);
            if (preexistingTranslation != null)
            {
                Dictionary<string, string> lookup = new Dictionary<string, string>();
                for (int i = 0; i < conversation.Phrases.Length; i++)
                {
                    lookup[conversation.Phrases[i]] = preexistingTranslation.Phrases[i];
                }
                return lookup[phrase];
                
            }
            return null;
        }
    }

}