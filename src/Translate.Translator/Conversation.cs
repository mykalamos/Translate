namespace Translate.Translator
{
    public class Conversation
    {
        public Conversation(string name, string group, string[] phrases)
        {
            Name = name;
            Group = group;
            Phrases = phrases;
        }
        public string Name { get; }
        public string Group { get; }
        public string[] Phrases { get; }
    }
}