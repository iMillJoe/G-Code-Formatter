﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace G_Code_Formatter
{
    public partial class Form1 : Form
    {
        private OpenFileDialog openFileDialog;
        private string text;
        public Form1()
        {
            InitializeComponent();
            openFileDialog = new OpenFileDialog();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var sr = new StreamReader(openFileDialog.FileName);
                    text = sr.ReadToEnd();
                    textBox.Text = text;
                    var path = openFileDialog.FileName;
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            loadFile();
        }

        private void loadFile()
        {
            string testFile = 
@"G00 Z=VPPLZ
G00 G90 G15 H1 
(THIS IS A COMMENT) 
G4 F       12.0
G1 X=1.0 (MULTI)   Y0.0(BECAUSE PEOPLE)F1.2( DO?  )
N1000 G3 X2. Y 5. 
(NESTED (COMMENT) BECAUSE WHY NOT?)
N5 G1 P100 G50
B200
VC[1]=100
NAT200
G0 Z=VPPLZ Y=VPPLY
M2";
            textBox.Text = testFile;
            textBox1.Text = testFile;
        }


        private string formatGCode(string gCode)
        {

            string wordValueBuilder = "";
            string wordLetterBuilder = "";
            Block builderBlock = new Block();
            Word builderWord = new Word();
            GCodeProgram gprog = new GCodeProgram();
            text = textBox.Text;
            int parens = 0;
            if (text != null)
            {


                for (int i = 0; i < text.Length; ++i)
                {

                    char c = text[i];


                    if (parens <= 0)
                    {

                        if (c == '\n' || c == '\r')
                        {
                            //Append Last Block
                            if (wordValueBuilder != "")
                            {
                                builderWord.value = wordValueBuilder;
                                builderBlock.addWord(builderWord);
                                wordValueBuilder = "";
                                wordLetterBuilder = "";
                                builderWord.empty();
                                gprog.addBlock(builderBlock);
                                builderBlock.empty();
                            }
                            else if (wordLetterBuilder != "")
                            {
                                builderWord.letter = wordLetterBuilder;
                                builderBlock.addWord(builderWord);
                                wordValueBuilder = "";
                                wordLetterBuilder = "";
                                builderWord.empty();
                                gprog.addBlock(builderBlock);
                                builderBlock.empty();
                            }
                        }
                        else if (c == ' ' || c == 9)
                        {
                            if (wordLetterBuilder != "")
                            {
                                builderWord.letter = wordLetterBuilder;
                                wordLetterBuilder = "";
                            }
                            else if (wordValueBuilder != "")
                            {
                                builderWord.value = wordValueBuilder;
                                builderBlock.addWord(builderWord);
                                wordValueBuilder = "";
                            }

                        }
                        else if (c == ')')
                        {
                            if (wordLetterBuilder != "")
                            {
                                builderWord.letter = wordLetterBuilder;
                                wordLetterBuilder = "does this ever get hit??";
                            }
                            else
                            {
                                wordValueBuilder = " ";
                            }
                        }
                        else if (c == '(')
                        {
                            parens += 1;
                            wordLetterBuilder += c;
                        }
                        else if (isLetter(c))
                        {
                            //new word?
                            if (wordValueBuilder != "" && builderWord.letter != "")
                            {
                                builderWord.value = wordValueBuilder;
                                builderBlock.addWord(builderWord);
                                builderWord.empty();
                                wordLetterBuilder += c;
                            }
                            else if (builderWord.letter != "" && wordValueBuilder != "")
                            {
                                wordValueBuilder += c;
                            }
                            else
                            {
                                wordLetterBuilder += c;
                            }
                        }
                        else if (isNumber(c))
                        {
                            //add value
                            wordValueBuilder += c;
                            if (wordLetterBuilder != "")
                            {
                                builderWord.letter = wordLetterBuilder;
                                wordLetterBuilder = "";
                            }
                        }
                        else if (isSymbol(c))
                        {
                            wordValueBuilder += c;
                            if (wordLetterBuilder != "")
                            {
                                builderWord.letter = wordLetterBuilder;
                                wordLetterBuilder = "";
                            }
                        }
                    }
                    else
                    {
                        if (!isParen(c))
                        {
                            // While inParen build untill closed paren
                            wordLetterBuilder += c;
                        }
                        else if (c == '(')
                        {
                            parens += 1;
                            wordLetterBuilder += '(';
                        }
                        else if (c == ')')
                        {
                            wordLetterBuilder += c;
                            builderWord.letter = wordLetterBuilder;
                            builderWord.value = "88888888888";
                            //builderBlock.addWord(builderWord);
                            //builderWord.empty();
                            //wordLetterBuilder = "";
                            //wordValueBuilder = "";
                            parens -= 1;
                        }
                    }
                }

                //Append Last Block
                if (wordValueBuilder != "")
                {
                    builderWord.value = wordValueBuilder;
                    builderBlock.addWord(builderWord);
                    wordValueBuilder = "";
                    wordLetterBuilder = "";
                    builderWord.empty();
                    gprog.addBlock(builderBlock);
                    builderBlock.empty();
                }
                else if (wordLetterBuilder != "")
                {
                    builderWord.letter = wordLetterBuilder;
                    builderBlock.addWord(builderWord);
                    wordValueBuilder = "";
                    wordLetterBuilder = "";
                    builderWord.empty();
                    gprog.addBlock(builderBlock);
                    builderBlock.empty();
                }


            }

            return gprog.text();
      
        }




        private void FormatButton_Click(object sender, EventArgs e)
        {

            //loadFile();
            textBox.Text = formatGCode(textBox.Text);
            MessageBox.Show("Thanks!, check debugger");

        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private bool isLetter(char c)
        {
            if ( c >= 65 && c <= 90)
            {
                return true;
            }
            else if (c >= 97 && c <= 122)
            {
                // Warn of lowercase letters
                MessageBox.Show("Avoid Lowercase Letters");
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

        class Word
        {
            public string letter;
            public string value;

            public bool isWord(Word word)
            {
                if ( this.letter != "" && this.value != "")
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
                return word;

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
            }

            public string text()
            {
                if (this.program != null)
                {
                    string output = "";
                    foreach (Block block in this.program)
                    {
                        output += block.text();
                    }
                    return output;
                }
                return null;
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
