namespace Translate.Translator
{
    static class TranslatedPhraseProvider
    {
        public static TranslatedPhrase GetTranslatedPhrase(Language language, Conversation conversation, string phrase, string paddedPhraseCounter)
        {
            var translatedLines = TextTranslator.GetTranslatedPhrase(language, conversation, phrase, paddedPhraseCounter);
            var translatedPhrase = TranslatedPhraseParser.ExtractTranslatedPhrases(translatedLines);
            return translatedPhrase;
        }
    }
}