namespace Translate.Translator
{
    public class Conversation
    {
        public Conversation(string name, string[] phrases)
        {
            Name = name;
            Phrases = phrases;
        }
        public string Name { get; }
        public string[] Phrases { get; }
    }
}