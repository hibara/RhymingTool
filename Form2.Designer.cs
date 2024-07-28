using System.Drawing;
using System.Windows.Forms;

namespace RhymingTool
{
  partial class Form2
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
      this.buttonOk = new System.Windows.Forms.Button();
      this.pictureBoxMainIcon = new System.Windows.Forms.PictureBox();
      this.labelAppName = new System.Windows.Forms.Label();
      this.labelVersion = new System.Windows.Forms.Label();
      this.labelCopyright = new System.Windows.Forms.Label();
      this.pictureBoxHome = new System.Windows.Forms.PictureBox();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.linkLabelHome = new System.Windows.Forms.LinkLabel();
      this.linkLabelGitHub = new System.Windows.Forms.LinkLabel();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMainIcon)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHome)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // buttonOk
      // 
      this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonOk.Location = new System.Drawing.Point(342, 121);
      this.buttonOk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.buttonOk.Name = "buttonOk";
      this.buttonOk.Size = new System.Drawing.Size(64, 24);
      this.buttonOk.TabIndex = 0;
      this.buttonOk.Text = "&OK";
      this.buttonOk.UseVisualStyleBackColor = true;
      this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
      // 
      // pictureBoxMainIcon
      // 
      this.pictureBoxMainIcon.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxMainIcon.Image")));
      this.pictureBoxMainIcon.Location = new System.Drawing.Point(17, 14);
      this.pictureBoxMainIcon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.pictureBoxMainIcon.Name = "pictureBoxMainIcon";
      this.pictureBoxMainIcon.Size = new System.Drawing.Size(64, 64);
      this.pictureBoxMainIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBoxMainIcon.TabIndex = 1;
      this.pictureBoxMainIcon.TabStop = false;
      // 
      // labelAppName
      // 
      this.labelAppName.AutoSize = true;
      this.labelAppName.Location = new System.Drawing.Point(104, 14);
      this.labelAppName.Name = "labelAppName";
      this.labelAppName.Size = new System.Drawing.Size(78, 12);
      this.labelAppName.TabIndex = 2;
      this.labelAppName.Text = "labelAppName";
      // 
      // labelVersion
      // 
      this.labelVersion.AutoSize = true;
      this.labelVersion.Location = new System.Drawing.Point(104, 34);
      this.labelVersion.Name = "labelVersion";
      this.labelVersion.Size = new System.Drawing.Size(68, 12);
      this.labelVersion.TabIndex = 3;
      this.labelVersion.Text = "labelVersion";
      // 
      // labelCopyright
      // 
      this.labelCopyright.AutoSize = true;
      this.labelCopyright.Location = new System.Drawing.Point(104, 53);
      this.labelCopyright.Name = "labelCopyright";
      this.labelCopyright.Size = new System.Drawing.Size(78, 12);
      this.labelCopyright.TabIndex = 4;
      this.labelCopyright.Text = "labelCopyright";
      // 
      // pictureBoxHome
      // 
      this.pictureBoxHome.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxHome.Image")));
      this.pictureBoxHome.Location = new System.Drawing.Point(96, 76);
      this.pictureBoxHome.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.pictureBoxHome.Name = "pictureBoxHome";
      this.pictureBoxHome.Size = new System.Drawing.Size(16, 16);
      this.pictureBoxHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBoxHome.TabIndex = 5;
      this.pictureBoxHome.TabStop = false;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
      this.pictureBox1.Location = new System.Drawing.Point(96, 98);
      this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(16, 16);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox1.TabIndex = 6;
      this.pictureBox1.TabStop = false;
      // 
      // linkLabelHome
      // 
      this.linkLabelHome.AutoSize = true;
      this.linkLabelHome.Cursor = System.Windows.Forms.Cursors.Hand;
      this.linkLabelHome.Location = new System.Drawing.Point(114, 78);
      this.linkLabelHome.Name = "linkLabelHome";
      this.linkLabelHome.Size = new System.Drawing.Size(224, 12);
      this.linkLabelHome.TabIndex = 7;
      this.linkLabelHome.TabStop = true;
      this.linkLabelHome.Text = "https://hibara.dev/software/RhymingTool/";
      // 
      // linkLabelGitHub
      // 
      this.linkLabelGitHub.AutoSize = true;
      this.linkLabelGitHub.Cursor = System.Windows.Forms.Cursors.Hand;
      this.linkLabelGitHub.Location = new System.Drawing.Point(114, 100);
      this.linkLabelGitHub.Name = "linkLabelGitHub";
      this.linkLabelGitHub.Size = new System.Drawing.Size(214, 12);
      this.linkLabelGitHub.TabIndex = 8;
      this.linkLabelGitHub.TabStop = true;
      this.linkLabelGitHub.Text = "https://github.com/hibara/RhymingTool/";
      // 
      // Form2
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.buttonOk;
      this.ClientSize = new System.Drawing.Size(418, 156);
      this.Controls.Add(this.linkLabelGitHub);
      this.Controls.Add(this.linkLabelHome);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.pictureBoxHome);
      this.Controls.Add(this.labelCopyright);
      this.Controls.Add(this.labelVersion);
      this.Controls.Add(this.labelAppName);
      this.Controls.Add(this.pictureBoxMainIcon);
      this.Controls.Add(this.buttonOk);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Form2";
      this.Text = "バージョン情報";
      this.Load += new System.EventHandler(this.Form2_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMainIcon)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHome)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Button buttonOk;
    private PictureBox pictureBoxMainIcon;
    private Label labelAppName;
    private Label labelVersion;
    private Label labelCopyright;
    private PictureBox pictureBoxHome;
    private PictureBox pictureBox1;
    private LinkLabel linkLabelHome;
    private LinkLabel linkLabelGitHub;
  }
}