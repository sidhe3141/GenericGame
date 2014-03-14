using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sidhe3141utils.WordWrap
{
    /// <summary>
    /// A word that can be displayed.
    /// Author: James Yakura/sidhe3141.
    /// </summary>
    public class Word
    {
        string contents;
        int charsDisplayed;
        bool wordComplete;

        /// <summary>
        /// Creates a new word.
        /// </summary>
        /// <param name="contents">The string to be displayed.</param>
        public Word(string contents)
        {
            this.contents = contents;
            charsDisplayed = 0;
            wordComplete = false;
        }

        /// <summary>
        /// Gets the part of the word that is displayed.
        /// </summary>
        public string dataDisplayed
        {
            get
            {
                string result = "";
                for (int i = 1; i <= charsDisplayed; i++)
                {
                    result += contents[i - 1];
                }
                return result;
            }
        }
        /// <summary>
        /// Gets whether the word is finished.
        /// </summary>
        public bool WordComplete
        {
            get
            {
                return wordComplete;
            }
        }

        /// <summary>
        /// Advances the word display by one character.
        /// </summary>
        public void Advance()
        {
            charsDisplayed++;
            if (charsDisplayed >= contents.Length)
            {
                wordComplete = true;
            }
        }
        /// <summary>
        /// Advances the word display to its end.
        /// </summary>
        public void advanceToEnd()
        {
            while (!wordComplete)
            {
                Advance();
            }
        }
    }
}
