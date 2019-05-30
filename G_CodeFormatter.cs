using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Code_Formatter
{
    class G_CodeFormatter
    {
        public string formatCode(string input)
        {
            if (input == null)
            {
                return "error";
            }
            string currentWordLetter = "";
            string currentWordValue = "";
            Block currentBlock = new Block();
            Word currentWord = new Word();
            GCodeProgram program = new GCodeProgram();
            int parens = 0;

            for (int i = 0; i < input.Length; ++i)
            {

                char c = input[i];

                if (parens <= 0)
                {
                    if (c == '\n' || c == '\r')
                    {
                        //Append Last Block
                        if (currentWordValue != "")
                        {
                            currentWord.value = currentWordValue;
                            currentBlock.addWord(currentWord);
                            currentWordValue = "";
                            currentWordLetter = "";
                            program.addBlock(currentBlock);
                        }
                        else if (currentWordLetter != "")
                        {
                            currentWord.letter = currentWordLetter;
                            currentBlock.addWord(currentWord);
                            currentWordValue = "";
                            currentWordLetter = "";
                            program.addBlock(currentBlock);
                        }
                    }
                    else if (c == ' ' || c == 9)
                    {
                        if (currentWordLetter != "")
                        {
                            currentWord.letter = currentWordLetter;
                            currentWordLetter = "";
                        }
                        else if (currentWordValue != "")
                        {
                            currentWord.value = currentWordValue;
                            currentBlock.addWord(currentWord);
                            currentWordValue = "";
                        }

                    }
                    else if (c == ')')
                    {
                        if (currentWordLetter != "")
                        {
                            currentWord.letter = currentWordLetter;
                            currentWordLetter = "does this ever get hit??";
                        }
                        else
                        {
                            currentWordValue = " ";
                        }
                    }
                    else if (c == '(')
                    {
                        parens += 1;
                        currentWordLetter += c;
                    }
                    else if (isLetter(c))
                    {
                        //new word?
                        if (currentWord.letter != "" && currentWordValue != "")
                        {
                            currentWordValue += c;
                        }
                        else if (currentWordValue != "" && currentWord.letter != "")
                        {
                            currentWord.value = currentWordValue;
                            currentBlock.addWord(currentWord);
                            currentWordLetter += c;
                        }
                        else
                        {
                            currentWordLetter += c;
                        }
                    }
                    else if (isNumber(c))
                    {
                        //add value
                        currentWordValue += c;
                        if (currentWordLetter != "")
                        {
                            currentWord.letter = currentWordLetter;
                            currentWordLetter = "";
                        }
                    }
                    else if (isSymbol(c))
                    {
                        currentWordValue += c;
                        if (currentWordLetter != "")
                        {
                            currentWord.letter = currentWordLetter;
                            currentWordLetter = "";
                        }
                    }
                }
                else
                {
                    if (!isParen(c))
                    {
                        // While inParen build untill closed paren
                        currentWordLetter += c;
                    }
                    else if (c == '(')
                    {
                        parens += 1;
                        currentWordLetter += '(';
                    }
                    else if (c == ')')
                    {
                        currentWordLetter += c;
                        currentWord.letter = currentWordLetter;
                        //currentWord.value = "888wwwooooowww888";
                        //currentBlock.addWord(currentWord);
                        //currentWord.empty();
                        //currentWordLetter = "";
                        //currentWordValue = "";
                        parens -= 1;
                    }
                }
            }

            //Append Last Block
            if (currentWordValue != "")
            {
                currentWord.value = currentWordValue;
                currentBlock.addWord(currentWord);
                currentWordValue = "";
                currentWordLetter = "";
                program.addBlock(currentBlock);
            }
            else if (currentWordLetter != "")
            {
                currentWord.letter = currentWordLetter;
                currentBlock.addWord(currentWord);
                currentWordValue = "";
                currentWordLetter = "";
                program.addBlock(currentBlock);
            }
            return program.text();
            
        }




        private bool isLetter(char c)
        {
            if (c >= 65 && c <= 90)
            {
                return true;
            }
            else if (c >= 97 && c <= 122)
            {
                // Warn of lowercase letters
                // MessageBox.Show("Avoid Lowercase letters");
                return false;
            }
            return false;
        }
        private bool isNumber(char c)
        {
            if ((c >= 48 && c <= 57) || c == '.')
            {
                return true;
            }
            return false;
        }
        private bool isSymbol(char c)
        {
            switch (c)
            {
                case '=':
                    return true;
                case '+':
                    return true;
                case '-':
                    return true;
                case '/':
                    return true;
                case '*':
                    return true;
                default:
                    return false;
            }
        }
        private bool isParen(char c)
        {
            if (c == '(' || c == ')')
            {
                return true;
            }
            return false;
        }
    }



    class Word
    {
        public string letter;
        public string value;

        public bool isWord(Word word)
        {
            if (this.letter != "" && this.value != "")
            {
                return true;
            }
            return false;
        }

        public void empty()
        {
            this.letter = "";
            this.value = "";
        }

        public string text()
        {
            return $"{this.letter}{this.value}";
        }

        public Word copy()
        {
            Word word = new Word();
            string value = this.value;
            string letter = this.letter;
            word.value = value;
            word.letter = letter;
            this.empty();
            return word;
            //return (Word) this.MemberwiseClone();

        }
    }
    class Block
    {
        List<Word> words;

        public void addWord(Word word)
        {
            if (this.words == null)
            {
                this.words = new List<Word>();
            }
            this.words.Add(word.copy());
        }

        public string text()
        {
            string output = "";

            foreach (Word word in this.words)
            {
                if (output != "")
                {
                    output += $" {word.text()}";
                }
                else
                {
                    output = $"{word.text()}";
                }
            }
            return output + "\r\n";
        }

        public Block copy()
        {
            Block block = new Block();
            foreach (Word word in this.words)
            {
                block.addWord(word.copy());
            }
            return block;
        }

        public void empty()
        {
            this.words = null;
            this.words = new List<Word>();
        }
    }
    class GCodeProgram
    {
        List<Block> program;

        public void addBlock(Block block)
        {
            if (this.program == null)
            {
                this.program = new List<Block>();
            }
            this.program.Add(block.copy());
            block.empty();
        }

        public string text()
        {
            string output = "";
            foreach (Block block in this.program)
            {
                output += block.text();
            }
            return output;
        }

    }


}