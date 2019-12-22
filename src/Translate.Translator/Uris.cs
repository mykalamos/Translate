namespace Translate.Translator
{
    public static class Uris
    {
        public const string VoiceApi = "https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&q={0}&tl={1}&total=1&idx=0&textlen={2}";
        public const string TranslateApi = "https://translate.googleapis.com/translate_a/single?client=gtx&sl=EN&tl={0}&dt=t&q={1}";
    }
}