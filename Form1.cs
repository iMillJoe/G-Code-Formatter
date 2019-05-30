using System;
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
using G_Code_Formatter;

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

            text = 
@"G00 Z=VPPLZ
G00 G90 G15 H1
(THIS IS A COMMENT)
G4 F       12.0
G1 X = 1.0(MULTI)   Y0.0(BECAUSE PEOPLE)F1.2(DO ?  )
N1000 G3 X2.Y 5.
(NESTED(COMMENT)       BECAUSE      WHY        NOT ?)
N5 G1 P100 G50
B200
VC[1] = 100
NAT200
G0 Z = VPPLZ Y = VPPLY
M2";
            textBox.Text = text;
            textBox1.Text = text;

 
        }


        private string formatGCode(string gCode)
        {

            string stringToFormat = textBox.Text;
            G_CodeFormatter formatter = new G_CodeFormatter();
            textBox.Text = formatter.formatCode(stringToFormat);






        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {

        }



        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
