namespace G_Code_Formatter
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.formatButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(1206, 568);
            this.openButton.Margin = new System.Windows.Forms.Padding(4);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(100, 28);
            this.openButton.TabIndex = 0;
            this.openButton.Text = "Open File";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(1098, 568);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(100, 28);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "LOAD";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // textBox
            // 
            this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox.Location = new System.Drawing.Point(13, 14);
            this.textBox.Margin = new System.Windows.Forms.Padding(4);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(659, 547);
            this.textBox.TabIndex = 2;
            this.textBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // formatButton
            // 
            this.formatButton.Location = new System.Drawing.Point(990, 568);
            this.formatButton.Margin = new System.Windows.Forms.Padding(4);
            this.formatButton.Name = "formatButton";
            this.formatButton.Size = new System.Drawing.Size(100, 28);
            this.formatButton.TabIndex = 3;
            this.formatButton.Text = "Format";
            this.formatButton.UseVisualStyleBackColor = true;
            this.formatButton.Click += new System.EventHandler(this.FormatButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(686, 14);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(620, 547);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1319, 609);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.formatButton);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.openButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button formatButton;
        private System.Windows.Forms.TextBox textBox1;
    }
}

