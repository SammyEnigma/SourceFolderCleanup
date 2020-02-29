namespace SourceFolderCleanup
{
    partial class frmMain
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkDeleteBinObj = new System.Windows.Forms.CheckBox();
            this.chkDeletePackages = new System.Windows.Forms.CheckBox();
            this.cbDeleteMonths = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkArchive = new System.Windows.Forms.CheckBox();
            this.cbArchiveMonths = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbArchivePath = new WinForms.Library.Controls.BuilderTextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.tbSourcePath = new WinForms.Library.Controls.BuilderTextBox();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            this.pllPackagesSize = new SourceFolderCleanup.Controls.ProgressLinkLabel();
            this.pllBinObjSize = new SourceFolderCleanup.Controls.ProgressLinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Use this to delete or archive source file content.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Source Location:";
            // 
            // chkDeleteBinObj
            // 
            this.chkDeleteBinObj.AutoSize = true;
            this.chkDeleteBinObj.Location = new System.Drawing.Point(30, 120);
            this.chkDeleteBinObj.Name = "chkDeleteBinObj";
            this.chkDeleteBinObj.Size = new System.Drawing.Size(143, 17);
            this.chkDeleteBinObj.TabIndex = 3;
            this.chkDeleteBinObj.Text = "bin/ and obj/ folders";
            this.chkDeleteBinObj.UseVisualStyleBackColor = true;
            // 
            // chkDeletePackages
            // 
            this.chkDeletePackages.AutoSize = true;
            this.chkDeletePackages.Location = new System.Drawing.Point(30, 150);
            this.chkDeletePackages.Name = "chkDeletePackages";
            this.chkDeletePackages.Size = new System.Drawing.Size(85, 17);
            this.chkDeletePackages.TabIndex = 4;
            this.chkDeletePackages.Text = "packages/";
            this.chkDeletePackages.UseVisualStyleBackColor = true;
            // 
            // cbDeleteMonths
            // 
            this.cbDeleteMonths.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDeleteMonths.FormattingEnabled = true;
            this.cbDeleteMonths.Location = new System.Drawing.Point(167, 87);
            this.cbDeleteMonths.Name = "cbDeleteMonths";
            this.cbDeleteMonths.Size = new System.Drawing.Size(73, 21);
            this.cbDeleteMonths.TabIndex = 6;
            this.cbDeleteMonths.SelectedIndexChanged += new System.EventHandler(this.cbDeleteMonths_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(246, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "months:";
            // 
            // chkArchive
            // 
            this.chkArchive.AutoSize = true;
            this.chkArchive.Location = new System.Drawing.Point(12, 189);
            this.chkArchive.Name = "chkArchive";
            this.chkArchive.Size = new System.Drawing.Size(181, 17);
            this.chkArchive.TabIndex = 9;
            this.chkArchive.Text = "Archive projects older than";
            this.chkArchive.UseVisualStyleBackColor = true;
            this.chkArchive.CheckedChanged += new System.EventHandler(this.chkArchive_CheckedChanged);
            // 
            // cbArchiveMonths
            // 
            this.cbArchiveMonths.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbArchiveMonths.FormattingEnabled = true;
            this.cbArchiveMonths.Location = new System.Drawing.Point(199, 187);
            this.cbArchiveMonths.Name = "cbArchiveMonths";
            this.cbArchiveMonths.Size = new System.Drawing.Size(73, 21);
            this.cbArchiveMonths.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(278, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "months";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Archive Location:";
            // 
            // tbArchivePath
            // 
            this.tbArchivePath.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbArchivePath.Location = new System.Drawing.Point(30, 229);
            this.tbArchivePath.Name = "tbArchivePath";
            this.tbArchivePath.Size = new System.Drawing.Size(471, 26);
            this.tbArchivePath.Suggestions = null;
            this.tbArchivePath.TabIndex = 14;
            this.tbArchivePath.Value = "";
            this.tbArchivePath.BuilderClicked += new WinForms.Library.Controls.BuilderEventHandler(this.tbArchivePath_BuilderClicked);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(426, 272);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(345, 272);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 16;
            this.btnExecute.Text = "Run";
            this.btnExecute.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(9, 277);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(58, 13);
            this.linkLabel1.TabIndex = 17;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "History...";
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(333, 190);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(52, 13);
            this.linkLabel4.TabIndex = 20;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "preview";
            // 
            // tbSourcePath
            // 
            this.tbSourcePath.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSourcePath.Location = new System.Drawing.Point(12, 51);
            this.tbSourcePath.Name = "tbSourcePath";
            this.tbSourcePath.Size = new System.Drawing.Size(489, 26);
            this.tbSourcePath.Suggestions = null;
            this.tbSourcePath.TabIndex = 21;
            this.tbSourcePath.Value = "";
            this.tbSourcePath.BuilderClicked += new WinForms.Library.Controls.BuilderEventHandler(this.tbSourcePath_BuilderClicked);
            // 
            // chkDelete
            // 
            this.chkDelete.AutoSize = true;
            this.chkDelete.Location = new System.Drawing.Point(12, 89);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(152, 17);
            this.chkDelete.TabIndex = 22;
            this.chkDelete.Text = "Delete files older than";
            this.chkDelete.UseVisualStyleBackColor = true;
            this.chkDelete.CheckedChanged += new System.EventHandler(this.chkDelete_CheckedChanged);
            // 
            // pllPackagesSize
            // 
            this.pllPackagesSize.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pllPackagesSize.Location = new System.Drawing.Point(121, 148);
            this.pllPackagesSize.Mode = SourceFolderCleanup.Controls.ProgressLinkLabelMode.Text;
            this.pllPackagesSize.Name = "pllPackagesSize";
            this.pllPackagesSize.Size = new System.Drawing.Size(130, 19);
            this.pllPackagesSize.TabIndex = 24;
            // 
            // pllBinObjSize
            // 
            this.pllBinObjSize.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pllBinObjSize.Location = new System.Drawing.Point(179, 119);
            this.pllBinObjSize.Mode = SourceFolderCleanup.Controls.ProgressLinkLabelMode.Text;
            this.pllBinObjSize.Name = "pllBinObjSize";
            this.pllBinObjSize.Size = new System.Drawing.Size(130, 19);
            this.pllBinObjSize.TabIndex = 23;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 307);
            this.Controls.Add(this.pllPackagesSize);
            this.Controls.Add(this.pllBinObjSize);
            this.Controls.Add(this.chkDelete);
            this.Controls.Add(this.tbSourcePath);
            this.Controls.Add(this.linkLabel4);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tbArchivePath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbArchiveMonths);
            this.Controls.Add(this.chkArchive);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbDeleteMonths);
            this.Controls.Add(this.chkDeletePackages);
            this.Controls.Add(this.chkDeleteBinObj);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Source Folder Cleanup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkDeleteBinObj;
        private System.Windows.Forms.CheckBox chkDeletePackages;
        private System.Windows.Forms.ComboBox cbDeleteMonths;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkArchive;
        private System.Windows.Forms.ComboBox cbArchiveMonths;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private WinForms.Library.Controls.BuilderTextBox tbArchivePath;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private WinForms.Library.Controls.BuilderTextBox tbSourcePath;
        private System.Windows.Forms.CheckBox chkDelete;
        private Controls.ProgressLinkLabel pllBinObjSize;
        private Controls.ProgressLinkLabel pllPackagesSize;
    }
}

