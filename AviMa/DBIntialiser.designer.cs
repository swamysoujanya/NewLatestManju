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
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(12, 116);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(205, 30);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonShowBrowseDialog
            // 
            this.buttonShowBrowseDialog.Location = new System.Drawing.Point(15, 35);
            this.buttonShowBrowseDialog.Name = "buttonShowBrowseDialog";
            this.buttonShowBrowseDialog.Size = new System.Drawing.Size(202, 37);
            this.buttonShowBrowseDialog.TabIndex = 1;
            this.buttonShowBrowseDialog.Text = "Select Drive";
            this.buttonShowBrowseDialog.UseVisualStyleBackColor = true;
            this.buttonShowBrowseDialog.Click += new System.EventHandler(this.buttonShowBrowseDialog_Click);
            // 
            // labelSelectedPath
            // 
            this.labelSelectedPath.AutoSize = true;
            this.labelSelectedPath.Location = new System.Drawing.Point(12, 9);
            this.labelSelectedPath.Name = "labelSelectedPath";
            this.labelSelectedPath.Size = new System.Drawing.Size(109, 16);
            this.labelSelectedPath.TabIndex = 2;
            this.labelSelectedPath.Text = "No path selected";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(15, 161);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(202, 32);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // txtErrorLogger
            // 
            this.txtErrorLogger.Location = new System.Drawing.Point(223, 35);
            this.txtErrorLogger.Multiline = true;
            this.txtErrorLogger.Name = "txtErrorLogger";
            this.txtErrorLogger.ReadOnly = true;
            this.txtErrorLogger.Size = new System.Drawing.Size(565, 384);
            this.txtErrorLogger.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(202, 32);
            this.button1.TabIndex = 5;
            this.button1.Text = "Skip";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 431);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtErrorLogger);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.labelSelectedPath);
            this.Controls.Add(this.buttonShowBrowseDialog);
            this.Controls.Add(this.buttonConnect);
            this.Name = "Form";
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
        private System.Windows.Forms.Button button1;
    }
}

