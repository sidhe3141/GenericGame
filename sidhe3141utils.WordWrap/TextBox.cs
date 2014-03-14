using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sidhe3141utils.WordWrap
{
    /// <summary>
    /// A text box that wraps words and displays words character-by-character.
    /// Author: James Yakura/sidhe3141.
    /// </summary>
    public class TextBox
    {
        char[] endLines = new char[]{' ', '\n', '-' };
        string contents;
        int width;
        List<Word> parsedContents;

        /// <summary>
        /// Creates a text box that can handle word wrapping.
        /// </summary>
        /// <param name="contents">The text to be displayed.</param>
        /// <param name="width">The number of characters that can fit on a line.</param>
        public TextBox(string contents, int width)
        {
            this.width = width;
            Contents = contents;
        }
        /// <summary>
        /// Gets the portion of the text box that is displayed.
        /// </summary>
        public string DisplayedContents
        {
            get
            {
                string value = "";
                string currentLine="";
                foreach (Word x in parsedContents)
                {
                    if (currentLine.Length + x.dataDisplayed.Length > width)
                    {
                        value += currentLine+"\n";
                        currentLine = "";
                    }
                    currentLine += x.dataDisplayed;
                }
                value += currentLine;
                return value;
            }
        }

        public string Contents
        {
            get
            {
                return contents;
            }
            set
            {
                contents = value;
                parsedContents = new List<Word>();
                string currentString = "";
                for (int i = 0; i < contents.Length; i++)
                {
                    currentString += contents[i];
                    bool endthisword = false;
                    foreach (char x in endLines)
                    {
                        if (contents[i] == x | i==contents.Length-1)
                        {
                            endthisword = true;
                        }
                    }
                    if (endthisword)
                    {
                        parsedContents.Add(new Word((string)currentString.Clone()));
                        currentString = "";
                    }
                }
            }
        }

        /// <summary>
        /// Completes the current word.
        /// </summary>
        public void CompleteWord()
        {
            foreach (Word x in parsedContents)
            {
                if (!x.WordComplete)
                {
                    x.advanceToEnd();
                    break;
                }
            }
        }

        /// <summary>
        /// Advances the current word.
        /// </summary>
        public void Advance()
        {
            foreach (Word x in parsedContents)
            {
                if (!x.WordComplete)
                {
                    x.Advance();
                    break;
                }
            }
        }
        /// <summary>
        /// Completes the text box.
        /// </summary>
        public void Complete()
        {
            foreach (Word x in parsedContents)
            {
                x.advanceToEnd();
            }
        }
        /// <summary>
        /// Gets a list of words in the text.
        /// </summary>
        public List<Word> ParsedContents
        {
            get
            {
                return parsedContents;
            }
        }

    }
}
