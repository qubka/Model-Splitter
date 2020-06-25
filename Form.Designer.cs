namespace Model_Splitter
{
    partial class FormSplitter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSplitter));
            this.buttonFolderSelect = new System.Windows.Forms.Button();
            this.textBoxSelect = new System.Windows.Forms.TextBox();
            this.buttonTarget = new System.Windows.Forms.Button();
            this.textBoxTarget = new System.Windows.Forms.TextBox();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.labelCount = new System.Windows.Forms.Label();
            this.buttonSelectFile = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.checkBoxCompress = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonFolderSelect
            // 
            this.buttonFolderSelect.Location = new System.Drawing.Point(12, 25);
            this.buttonFolderSelect.Name = "buttonFolderSelect";
            this.buttonFolderSelect.Size = new System.Drawing.Size(86, 20);
            this.buttonFolderSelect.TabIndex = 0;
            this.buttonFolderSelect.Text = "Select Folder";
            this.buttonFolderSelect.UseVisualStyleBackColor = true;
            this.buttonFolderSelect.Click += new System.EventHandler(this.buttonSelectFolder_Click);
            // 
            // textBoxSelect
            // 
            this.textBoxSelect.Location = new System.Drawing.Point(196, 25);
            this.textBoxSelect.Name = "textBoxSelect";
            this.textBoxSelect.Size = new System.Drawing.Size(358, 20);
            this.textBoxSelect.TabIndex = 2;
            this.textBoxSelect.TextChanged += new System.EventHandler(this.textBoxSelect_TextChanged);
            // 
            // buttonTarget
            // 
            this.buttonTarget.Location = new System.Drawing.Point(104, 51);
            this.buttonTarget.Name = "buttonTarget";
            this.buttonTarget.Size = new System.Drawing.Size(86, 20);
            this.buttonTarget.TabIndex = 1;
            this.buttonTarget.Text = "Target Folder";
            this.buttonTarget.UseVisualStyleBackColor = true;
            this.buttonTarget.Click += new System.EventHandler(this.buttonTarget_Click);
            // 
            // textBoxTarget
            // 
            this.textBoxTarget.Location = new System.Drawing.Point(196, 52);
            this.textBoxTarget.Name = "textBoxTarget";
            this.textBoxTarget.Size = new System.Drawing.Size(358, 20);
            this.textBoxTarget.TabIndex = 3;
            // 
            // textBoxCount
            // 
            this.textBoxCount.AccessibleDescription = "";
            this.textBoxCount.AccessibleName = "";
            this.textBoxCount.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.textBoxCount.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxCount.Enabled = false;
            this.textBoxCount.Location = new System.Drawing.Point(560, 25);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.ReadOnly = true;
            this.textBoxCount.Size = new System.Drawing.Size(75, 20);
            this.textBoxCount.TabIndex = 4;
            this.textBoxCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxCount.TextChanged += new System.EventHandler(this.textBoxTarget_TextChanged);
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(569, 48);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(67, 13);
            this.labelCount.TabIndex = 5;
            this.labelCount.Text = "Model Count";
            this.labelCount.Click += new System.EventHandler(this.labelCount_Click);
            // 
            // buttonSelectFile
            // 
            this.buttonSelectFile.Location = new System.Drawing.Point(104, 25);
            this.buttonSelectFile.Name = "buttonSelectFile";
            this.buttonSelectFile.Size = new System.Drawing.Size(86, 20);
            this.buttonSelectFile.TabIndex = 6;
            this.buttonSelectFile.Text = "Select File";
            this.buttonSelectFile.UseVisualStyleBackColor = true;
            this.buttonSelectFile.Click += new System.EventHandler(this.buttonSelectFile_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 80);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(178, 28);
            this.buttonStart.TabIndex = 7;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(197, 80);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(439, 28);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 8;
            // 
            // checkBoxCompress
            // 
            this.checkBoxCompress.AutoSize = true;
            this.checkBoxCompress.Checked = true;
            this.checkBoxCompress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCompress.Location = new System.Drawing.Point(12, 52);
            this.checkBoxCompress.Name = "checkBoxCompress";
            this.checkBoxCompress.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBoxCompress.Size = new System.Drawing.Size(86, 17);
            this.checkBoxCompress.TabIndex = 9;
            this.checkBoxCompress.Text = "Compressing";
            this.checkBoxCompress.UseVisualStyleBackColor = true;
            this.checkBoxCompress.CheckedChanged += new System.EventHandler(this.checkBoxCompress_CheckedChanged);
            // 
            // FormSplitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 113);
            this.Controls.Add(this.checkBoxCompress);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonSelectFile);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.textBoxTarget);
            this.Controls.Add(this.textBoxSelect);
            this.Controls.Add(this.buttonTarget);
            this.Controls.Add(this.buttonFolderSelect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSplitter";
            this.Text = "MDL Splitter 1.0 by qubka";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonFolderSelect;
        private System.Windows.Forms.TextBox textBoxSelect;
        private System.Windows.Forms.Button buttonTarget;
        private System.Windows.Forms.TextBox textBoxTarget;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Button buttonSelectFile;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.CheckBox checkBoxCompress;
    }
}

