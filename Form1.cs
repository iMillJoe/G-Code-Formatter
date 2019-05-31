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
G00 G90 G94 G15 H1
(THIS IS A COMMENT)
M130
G4 F    5.0 (DWELL 5 SECONDS)
G1 X=	-1.0(MULTI) Y - 1.0(COMMENTS) F = 100.0  (ON ONE LINE)
X1.0 Y= 0.
X= 2
VC[1] = 100
NAT200
G0 Z = VPPLZ Y = VPPLY
M2"; // that shit actually works :/ 
            textBox.Text = text;
            textBox1.Text = text;

 
        }

        private void FormatButton_Click(object sender, EventArgs e)
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
