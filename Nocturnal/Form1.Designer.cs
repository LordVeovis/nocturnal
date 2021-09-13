
namespace Kveer.Nocturnal
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.tbxBitlockerKey = new System.Windows.Forms.TextBox();
			this.cbxBitlockerKey = new System.Windows.Forms.ComboBox();
			this.cbxComputerName = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(13, 42);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(467, 93);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.button2);
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(459, 65);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "RDP";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Enabled = false;
			this.button2.Location = new System.Drawing.Point(4, 35);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(136, 25);
			this.button2.TabIndex = 0;
			this.button2.Text = "Connect as local user";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.AutoSize = true;
			this.button1.Enabled = false;
			this.button1.Location = new System.Drawing.Point(4, 4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(135, 25);
			this.button1.TabIndex = 0;
			this.button1.Text = "Connect as ";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.label2);
			this.tabPage2.Controls.Add(this.tbxBitlockerKey);
			this.tabPage2.Controls.Add(this.cbxBitlockerKey);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(459, 65);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Bitlocker key";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 37);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(108, 15);
			this.label2.TabIndex = 2;
			this.label2.Text = "Reovery password: ";
			// 
			// tbxBitlockerKey
			// 
			this.tbxBitlockerKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbxBitlockerKey.Location = new System.Drawing.Point(118, 34);
			this.tbxBitlockerKey.MaxLength = 255;
			this.tbxBitlockerKey.Name = "tbxBitlockerKey";
			this.tbxBitlockerKey.ReadOnly = true;
			this.tbxBitlockerKey.Size = new System.Drawing.Size(338, 23);
			this.tbxBitlockerKey.TabIndex = 1;
			this.tbxBitlockerKey.Enter += new System.EventHandler(this.textBox1_Enter);
			// 
			// cbxBitlockerKey
			// 
			this.cbxBitlockerKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbxBitlockerKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxBitlockerKey.FormattingEnabled = true;
			this.cbxBitlockerKey.Location = new System.Drawing.Point(4, 4);
			this.cbxBitlockerKey.Name = "cbxBitlockerKey";
			this.cbxBitlockerKey.Size = new System.Drawing.Size(452, 23);
			this.cbxBitlockerKey.TabIndex = 0;
			this.cbxBitlockerKey.SelectedValueChanged += new System.EventHandler(this.comboBox2_SelectedValueChanged);
			// 
			// cbxComputerName
			// 
			this.cbxComputerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbxComputerName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cbxComputerName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbxComputerName.FormattingEnabled = true;
			this.cbxComputerName.Location = new System.Drawing.Point(115, 13);
			this.cbxComputerName.Name = "cbxComputerName";
			this.cbxComputerName.Size = new System.Drawing.Size(365, 23);
			this.cbxComputerName.TabIndex = 1;
			this.cbxComputerName.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "Computer name:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(492, 147);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cbxComputerName);
			this.Controls.Add(this.tabControl1);
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Form1";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ComboBox cbxComputerName;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxBitlockerKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxBitlockerKey;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}

