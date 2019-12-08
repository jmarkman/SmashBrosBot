using System;
using System.Collections.Generic;

namespace DiscordSmashBot.TextService
{
    public class TextMutationService
    {
        private const string _regionalIndicator = ":regional_indicator_";

        public TextMutationService()
        {

        }

        /// <summary>
        /// Converts a sentence or word into a "regional indicator" form where each letter in the sentence or word
        /// is represented by a regional indicator emoticon
        /// </summary>
        /// <param name="input">A sentence or word from a user in the chat</param>
        /// <returns>The provided sentence or word</returns>
        public string ConvertInputToRegionalIndicator(string input)
        {
            var regionalIndicatorOutput = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine($"{nameof(TextMutationService)}.{nameof(ConvertInputToRegionalIndicator)}(): The provided input ('{input}') was either null or empty!");
                return string.Empty;
            }

            if (InputIsSingleWord(input))
            {
                regionalIndicatorOutput = ConvertCharactersToRegionalIndicator(input);
                return regionalIndicatorOutput;
            }

            var wordsInInput = SplitInputIntoSingularWords(input);



            return regionalIndicatorOutput;
        }

        private List<string> SplitInputIntoSingularWords(string input)
        {
            throw new NotImplementedException();
        }

        private string ConvertCharactersToRegionalIndicator(string input)
        {
            throw new NotImplementedException();
        }

        private string ConcatenateFormattedWords(List<string> formattedWords)
        {
            var concatenatedWord = string.Empty;

            if (formattedWords.Count == 0)
            {

            }           

            for (int i = 0; i < formattedWords.Count; i++)
            {
                var formattedWord = ConvertCharactersToRegionalIndicator(formattedWords[i]);

                if (i == 0)
                {
                    concatenatedWord += formattedWord;
                }

                concatenatedWord += $" {formattedWord}";
            }

            return concatenatedWord;
        }

        /// <summary>
        /// Determine if the string input provided by the chat user is a single word.
        /// Currently, so long as the input doesn't have spaces, it's considered
        /// a single word. This should allow for non-alphanumeric characters
        /// </summary>
        /// <param name="input">The string from the user</param>
        /// <returns><see cref="true"/> if the input is one word, <see cref="false"/> otherwise</returns>
        private bool InputIsSingleWord(string input)
        {
            if (input.Contains(' '))
            {
                return false;
            }

            return true;
        }
    }
}
