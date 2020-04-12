using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Translate.Translator
{
    internal static class TranslatedPhraseParser
    {
        private static Regex jsonRegex = new Regex("\"(?<TGroup>[^\"]+)\"", RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex stopParsingRegex = new Regex(@"[a-z0-9]{32}|\w+.md", RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static TranslatedPhrase ExtractTranslatedPhrases(string[] translatedLines)
        {
            var translatedPhrases = new List<string>();

            var sbSource = new StringBuilder();
            var sbTarget = new StringBuilder();

            foreach (var translatedLine in translatedLines)
            {
                if (translatedLine == "]")
                {
                    break;
                }
                var matches = jsonRegex.Matches(translatedLine);
                foreach (Match match in matches)
                {
                    foreach (Group group in match.Groups)
                    {
                        if (group.Name != "TGroup")
                        {
                            continue;
                        }
                        foreach (Capture capture in group.Captures)
                        {
                            if (stopParsingRegex.IsMatch(capture.Value))
                            {
                                break;
                            }
                            translatedPhrases.Add(capture.Value);
                            //if (translatedPhrases.Count == 2)
                            //{
                            //    translatedPhrases.Reverse(); // make english first
                            //    return translatedPhrases;
                            //}
                        }
                    } // Target will be first but reverse this
                }
            }
            for (var i = 0; i < translatedPhrases.Count; i++)
            {
                var line = translatedPhrases[i];
                if (i % 2 == 0)
                {
                    sbTarget.Append(line);
                }
                else
                {
                    sbSource.Append(line);
                }
            }
            return new TranslatedPhrase {
                Source = sbSource.ToString(), 
                Target = sbTarget.ToString() 
            };
            //throw new System.Exception($"Not enough phrases found in {translatedLines[0]}");
        }
    }
}