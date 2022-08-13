namespace AviMa
{
    partial class DBIntialiser
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
            this.buttonConnect = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonShowBrowseDialog = new System.Windows.Forms.Button();
            this.labelSelectedPath = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.txtErrorLogger = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(13, 98);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(231, 38);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonShowBrowseDialog
            // 
            this.buttonShowBrowseDialog.Location = new System.Drawing.Point(17, 44);
            this.buttonShowBrowseDialog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonShowBrowseDialog.Name = "buttonShowBrowseDialog";
            this.buttonShowBrowseDialog.Size = new System.Drawing.Size(227, 46);
            this.buttonShowBrowseDialog.TabIndex = 1;
            this.buttonShowBrowseDialog.Text = "Select Drive";
            this.buttonShowBrowseDialog.UseVisualStyleBackColor = true;
            this.buttonShowBrowseDialog.Click += new System.EventHandler(this.buttonShowBrowseDialog_Click);
            // 
            // labelSelectedPath
            // 
            this.labelSelectedPath.AutoSize = true;
            this.labelSelectedPath.Location = new System.Drawing.Point(14, 11);
            this.labelSelectedPath.Name = "labelSelectedPath";
            this.labelSelectedPath.Size = new System.Drawing.Size(129, 20);
            this.labelSelectedPath.TabIndex = 2;
            this.labelSelectedPath.Text = "No path selected";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(16, 154);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(227, 40);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // txtErrorLogger
            // 
            this.txtErrorLogger.Location = new System.Drawing.Point(251, 44);
            this.txtErrorLogger.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtErrorLogger.Multiline = true;
            this.txtErrorLogger.Name = "txtErrorLogger";
            this.txtErrorLogger.ReadOnly = true;
            this.txtErrorLogger.Size = new System.Drawing.Size(635, 479);
            this.txtErrorLogger.TabIndex = 4;
            // 
            // DBIntialiser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 539);
            this.Controls.Add(this.txtErrorLogger);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.labelSelectedPath);
            this.Controls.Add(this.buttonShowBrowseDialog);
            this.Controls.Add(this.buttonConnect);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DBIntialiser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DB Intialiser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button buttonShowBrowseDialog;
        private System.Windows.Forms.Label labelSelectedPath;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.TextBox txtErrorLogger;
    }
}

