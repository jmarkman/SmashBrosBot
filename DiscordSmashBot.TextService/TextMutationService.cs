using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

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

            regionalIndicatorOutput = ConcatenateFormattedWords(wordsInInput);

            return regionalIndicatorOutput;
        }

        /// <summary>
        /// Given multiple words in a string, split the string into the words that
        /// make up said string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private List<string> SplitInputIntoSingularWords(string input)
        {
            return input.Split(' ').ToList();
        }

        /// <summary>
        /// The meat and potatoes: converts each character in the word to its regional
        /// indicator variant
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string ConvertCharactersToRegionalIndicator(string input)
        {
            var regionalEmoteResult = string.Empty;

            // regex to detect that input string contains alphanumeric chars and acceptable symbols
            // accept: letters a-z (in caps as well, doesn't matter), digits 0-9, dash, underscore, exclamation, question mark
            Match inputMatch = Regex.Match(input, @"^[a-zA-Z0-9\s\-\\_\!\?]*$", RegexOptions.Multiline);

            // if regex is not valid, throw exception 
            // this should? prevent people trying to send unsupported characters, i.e.,
            // turkish i (don't think most discord users know this one), 日本 (discord users
            // might be able to send japanese/chinese/korean chars)
            if (!inputMatch.Success)
            {
                // TODO: throw in the future?
                return regionalEmoteResult;
            }

            // for each char in word (since we KNOW it's an acceptable word as defined by our criteria)
            for (int i = 0; i < input.Length; i++)
            {
                var currentCharacter = input[i];

                if (char.IsLetter(currentCharacter))
                {
                    // if char = azAZ, replace with regional indicator
                    regionalEmoteResult += $"{_regionalIndicator}{char.ToLower(currentCharacter)}:";
                }
                else if (char.IsDigit(currentCharacter))
                {
                    // if char = number 0-9, replace with 0-9 emoticon
                    regionalEmoteResult += $"{ConvertDigitToEmoticon(currentCharacter)}";
                }
                else if (char.IsPunctuation(currentCharacter))
                {
                    regionalEmoteResult += $"{ConvertPunctuationToEmoticon(currentCharacter)}";
                }
                else
                {
                    // TODO: test what happens here if it's something like "Banjo-Kazooie"
                    regionalEmoteResult += currentCharacter;
                }
            }

            return regionalEmoteResult;
        }

        /// <summary>
        /// Given a number 0-9, return the emoticon text that represents that number
        /// </summary>
        /// <param name="numericChar">The character from the current word being parsed</param>
        /// <returns>The emoticon textual shortcut for the provided number</returns>
        private string ConvertDigitToEmoticon(char numericChar)
        {
            Dictionary<char, string> charToEmoticonDict = new Dictionary<char, string>()
            {
                { '0', ":zero:" },
                { '1', ":one:" },
                { '2', ":two:" },
                { '3', ":three:" },
                { '4', ":four:" },
                { '5', ":five:" },
                { '6', ":six:" },
                { '7', ":seven:" },
                { '8', ":eight:" },
                { '9', ":nine:" },
            };

            return charToEmoticonDict[numericChar];
        }

        /// <summary>
        /// Given a valid punctuation character, return the emoticon that represents said character
        /// </summary>
        /// <param name="punctuationChar">The character from the word being parsed</param>
        /// <returns>The emoticon textual shortcut for the provided punctuation mark</returns>
        private string ConvertPunctuationToEmoticon(char punctuationChar)
        {
            Dictionary<char, string> punctuationToEmoticonDict = new Dictionary<char, string>()
            {
                { '!', ":exclamation" },
                { '?', ":question:" },
                { '-', ":heavy_minus_sign:" }
            };

            return punctuationToEmoticonDict[punctuationChar];
        }

        /// <summary>
        /// Discord messages have a character limit of 2000, check the final message and
        /// make sure that
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool IsEndResultWithinCharacterLimit(string input)
        {
            if (input.Length < 2000)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// After converting each word's letters into regional indicator version if applicable,
        /// build the result by attaching each fomratted word together
        /// </summary>
        /// <param name="formattedWords"></param>
        /// <returns></returns>
        private string ConcatenateFormattedWords(List<string> formattedWords)
        {
            var concatenatedWord = string.Empty;

            // If no words were provided (somehow), catch this
            if (formattedWords.Count == 0)
            {
                // throw exception or return? will temporarily just return empty string for now
                return string.Empty;
            }           

            // For each word in the list of formatted words, convert it to its regional indicator form,
            // and concatenate it to the empty string; we'll use to return the final product
            for (int i = 0; i < formattedWords.Count; i++)
            {
                var formattedWord = ConvertCharactersToRegionalIndicator(formattedWords[i]);

                if (i == 0)
                {
                    concatenatedWord += formattedWord;
                }
                else
                {
                    concatenatedWord += $"\t{formattedWord}";
                }
            }

            // Make sure that our final product is within the 2k character discord limit
            // TOOD: Probably should balk earlier at input that might hit the 2k limit
            if (!IsEndResultWithinCharacterLimit(concatenatedWord))
            {
                return string.Empty;
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
