namespace MSBTEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.msgList = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFile = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtU1 = new System.Windows.Forms.TextBox();
            this.txtU2 = new System.Windows.Forms.TextBox();
            this.txtU3 = new System.Windows.Forms.TextBox();
            this.txtU7 = new System.Windows.Forms.TextBox();
            this.txtU5 = new System.Windows.Forms.TextBox();
            this.txtU6 = new System.Windows.Forms.TextBox();
            this.txtU4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.openLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // msgList
            // 
            this.msgList.FormattingEnabled = true;
            this.msgList.Location = new System.Drawing.Point(12, 27);
            this.msgList.Name = "msgList";
            this.msgList.Size = new System.Drawing.Size(174, 342);
            this.msgList.Sorted = true;
            this.msgList.TabIndex = 0;
            this.msgList.SelectedIndexChanged += new System.EventHandler(this.MsgList_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.openLogToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(578, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFile,
            this.SaveFile});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // openFile
            // 
            this.openFile.Name = "openFile";
            this.openFile.ShowShortcutKeys = false;
            this.openFile.Size = new System.Drawing.Size(98, 22);
            this.openFile.Text = "Open";
            this.openFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // SaveFile
            // 
            this.SaveFile.Name = "SaveFile";
            this.SaveFile.Size = new System.Drawing.Size(98, 22);
            this.SaveFile.Text = "Save";
            this.SaveFile.Click += new System.EventHandler(this.SaveFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "*.msbt";
            this.openFileDialog1.Filter = "MSBT|*.msbt";
            this.openFileDialog1.Title = "Pick a msbt file";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(193, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Type";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(260, 268);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(100, 20);
            this.txtType.TabIndex = 4;
            this.txtType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 299);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Unknown 1";
            // 
            // txtU1
            // 
            this.txtU1.Location = new System.Drawing.Point(260, 295);
            this.txtU1.Name = "txtU1";
            this.txtU1.Size = new System.Drawing.Size(100, 20);
            this.txtU1.TabIndex = 6;
            this.txtU1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChanged);
            // 
            // txtU2
            // 
            this.txtU2.Location = new System.Drawing.Point(260, 322);
            this.txtU2.Name = "txtU2";
            this.txtU2.Size = new System.Drawing.Size(100, 20);
            this.txtU2.TabIndex = 7;
            this.txtU2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChanged);
            // 
            // txtU3
            // 
            this.txtU3.Location = new System.Drawing.Point(260, 349);
            this.txtU3.Name = "txtU3";
            this.txtU3.Size = new System.Drawing.Size(100, 20);
            this.txtU3.TabIndex = 8;
            this.txtU3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChanged);
            // 
            // txtU7
            // 
            this.txtU7.Location = new System.Drawing.Point(445, 349);
            this.txtU7.Name = "txtU7";
            this.txtU7.ReadOnly = true;
            this.txtU7.Size = new System.Drawing.Size(100, 20);
            this.txtU7.TabIndex = 9;
            this.txtU7.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChanged);
            // 
            // txtU5
            // 
            this.txtU5.Location = new System.Drawing.Point(445, 295);
            this.txtU5.Name = "txtU5";
            this.txtU5.Size = new System.Drawing.Size(100, 20);
            this.txtU5.TabIndex = 10;
            this.txtU5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChanged);
            // 
            // txtU6
            // 
            this.txtU6.Location = new System.Drawing.Point(445, 322);
            this.txtU6.Name = "txtU6";
            this.txtU6.Size = new System.Drawing.Size(100, 20);
            this.txtU6.TabIndex = 11;
            this.txtU6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChanged);
            // 
            // txtU4
            // 
            this.txtU4.Location = new System.Drawing.Point(445, 268);
            this.txtU4.Name = "txtU4";
            this.txtU4.Size = new System.Drawing.Size(100, 20);
            this.txtU4.TabIndex = 12;
            this.txtU4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 325);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Unknown 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(192, 352);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Unknown 3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(377, 271);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Unknown 4";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(377, 298);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Unknown 5";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(377, 325);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Unknown 6";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(377, 352);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Unknown 7";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "MSBT files|*.msbt";
            this.saveFileDialog1.Title = "Pick a location for the file to be saved";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(196, 28);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(349, 234);
            this.textBox1.TabIndex = 19;
            // 
            // openLogToolStripMenuItem
            // 
            this.openLogToolStripMenuItem.Name = "openLogToolStripMenuItem";
            this.openLogToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.openLogToolStripMenuItem.Text = "Open Log";
            this.openLogToolStripMenuItem.Click += new System.EventHandler(this.OpenLogToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 383);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtU4);
            this.Controls.Add(this.txtU6);
            this.Controls.Add(this.txtU5);
            this.Controls.Add(this.txtU7);
            this.Controls.Add(this.txtU3);
            this.Controls.Add(this.txtU2);
            this.Controls.Add(this.txtU1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.msgList);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "MSBT attribute editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox msgList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem SaveFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtU1;
        private System.Windows.Forms.TextBox txtU2;
        private System.Windows.Forms.TextBox txtU3;
        private System.Windows.Forms.TextBox txtU7;
        private System.Windows.Forms.TextBox txtU5;
        private System.Windows.Forms.TextBox txtU6;
        private System.Windows.Forms.TextBox txtU4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem openLogToolStripMenuItem;
    }
}

