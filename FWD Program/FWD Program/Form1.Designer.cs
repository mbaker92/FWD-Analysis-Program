namespace FWD_Program
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
            this.DirectInstructLabel = new System.Windows.Forms.Label();
            this.Browser = new System.Windows.Forms.Button();
            this.DirectoryPath = new System.Windows.Forms.Label();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.InstructionLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // DirectInstructLabel
            // 
            this.DirectInstructLabel.AutoSize = true;
            this.DirectInstructLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.DirectInstructLabel.Location = new System.Drawing.Point(12, 54);
            this.DirectInstructLabel.Name = "DirectInstructLabel";
            this.DirectInstructLabel.Size = new System.Drawing.Size(136, 20);
            this.DirectInstructLabel.TabIndex = 0;
            this.DirectInstructLabel.Text = "Select A Directory";
            // 
            // Browser
            // 
            this.Browser.Location = new System.Drawing.Point(274, 51);
            this.Browser.Name = "Browser";
            this.Browser.Size = new System.Drawing.Size(75, 23);
            this.Browser.TabIndex = 1;
            this.Browser.Text = "Browse";
            this.Browser.UseVisualStyleBackColor = true;
            this.Browser.Click += new System.EventHandler(this.Browser_Click);
            // 
            // DirectoryPath
            // 
            this.DirectoryPath.AutoSize = true;
            this.DirectoryPath.Location = new System.Drawing.Point(12, 98);
            this.DirectoryPath.Name = "DirectoryPath";
            this.DirectoryPath.Size = new System.Drawing.Size(74, 13);
            this.DirectoryPath.TabIndex = 2;
            this.DirectoryPath.Text = "Directory Path";
            // 
            // InstructionLabel
            // 
            this.InstructionLabel.AutoSize = true;
            this.InstructionLabel.Location = new System.Drawing.Point(13, 22);
            this.InstructionLabel.Name = "InstructionLabel";
            this.InstructionLabel.Size = new System.Drawing.Size(61, 13);
            this.InstructionLabel.TabIndex = 4;
            this.InstructionLabel.TabStop = true;
            this.InstructionLabel.Text = "Instructions";
            this.InstructionLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.InstructionLabel_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 122);
            this.Controls.Add(this.InstructionLabel);
            this.Controls.Add(this.DirectoryPath);
            this.Controls.Add(this.Browser);
            this.Controls.Add(this.DirectInstructLabel);
            this.Name = "Form1";
            this.Text = "FWD Program";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DirectInstructLabel;
        private System.Windows.Forms.Button Browser;
        private System.Windows.Forms.Label DirectoryPath;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.LinkLabel InstructionLabel;
    }
}

