using System.Drawing;
using System.Windows.Forms;

namespace RhymingTool
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
      this.toolStripStatusLabelPercent = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabelEncoding = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabelFileName = new System.Windows.Forms.ToolStripStatusLabel();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.ToolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItemSaveResultText = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItemImportDicFile = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItemAppExit = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItemHelpContent = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPageMain = new System.Windows.Forms.TabPage();
      this.pictureBoxArrowDown = new System.Windows.Forms.PictureBox();
      this.textBoxOutput = new System.Windows.Forms.TextBox();
      this.textBoxInput = new System.Windows.Forms.TextBox();
      this.tabPageSettings = new System.Windows.Forms.TabPage();
      this.labelHits03 = new System.Windows.Forms.Label();
      this.labelHits02 = new System.Windows.Forms.Label();
      this.labelHits01 = new System.Windows.Forms.Label();
      this.labelTotalMatchWords = new System.Windows.Forms.Label();
      this.textBoxTotalMatchWords = new System.Windows.Forms.TextBox();
      this.labelMaxPronunciationWords = new System.Windows.Forms.Label();
      this.numericUpDownMaxPronunciationWords = new System.Windows.Forms.NumericUpDown();
      this.labelMaxVowelWords = new System.Windows.Forms.Label();
      this.numericUpDownMaxVowelWords = new System.Windows.Forms.NumericUpDown();
      this.labelPercent = new System.Windows.Forms.Label();
      this.labelMatchThreshold = new System.Windows.Forms.Label();
      this.numericUpDownMatchThreshold = new System.Windows.Forms.NumericUpDown();
      this.tabPageDictionary = new System.Windows.Forms.TabPage();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.dataGridView2 = new System.Windows.Forms.DataGridView();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.panelDragAndDropArea = new System.Windows.Forms.Panel();
      this.labelDragAndDropHere = new System.Windows.Forms.Label();
      this.checkBoxDataBaseInit = new System.Windows.Forms.CheckBox();
      this.panelButtons = new System.Windows.Forms.Panel();
      this.buttonExit = new System.Windows.Forms.Button();
      this.buttonRhymingSearch = new System.Windows.Forms.Button();
      this.buttonMorphologicalAnalysis = new System.Windows.Forms.Button();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.statusStrip1.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPageMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArrowDown)).BeginInit();
      this.tabPageSettings.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxPronunciationWords)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxVowelWords)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMatchThreshold)).BeginInit();
      this.tabPageDictionary.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.panelDragAndDropArea.SuspendLayout();
      this.panelButtons.SuspendLayout();
      this.SuspendLayout();
      // 
      // statusStrip1
      // 
      this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
      this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabelPercent,
            this.toolStripStatusLabelEncoding,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelFileName});
      this.statusStrip1.Location = new System.Drawing.Point(0, 459);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
      this.statusStrip1.Size = new System.Drawing.Size(784, 22);
      this.statusStrip1.TabIndex = 0;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // toolStripProgressBar
      // 
      this.toolStripProgressBar.Name = "toolStripProgressBar";
      this.toolStripProgressBar.Size = new System.Drawing.Size(110, 16);
      this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      // 
      // toolStripStatusLabelPercent
      // 
      this.toolStripStatusLabelPercent.Name = "toolStripStatusLabelPercent";
      this.toolStripStatusLabelPercent.Size = new System.Drawing.Size(25, 17);
      this.toolStripStatusLabelPercent.Text = "- %";
      // 
      // toolStripStatusLabelEncoding
      // 
      this.toolStripStatusLabelEncoding.Name = "toolStripStatusLabelEncoding";
      this.toolStripStatusLabelEncoding.Size = new System.Drawing.Size(12, 17);
      this.toolStripStatusLabelEncoding.Text = "-";
      // 
      // toolStripStatusLabel1
      // 
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new System.Drawing.Size(10, 17);
      this.toolStripStatusLabel1.Text = ":";
      // 
      // toolStripStatusLabelFileName
      // 
      this.toolStripStatusLabelFileName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripStatusLabelFileName.Name = "toolStripStatusLabelFileName";
      this.toolStripStatusLabelFileName.Size = new System.Drawing.Size(12, 17);
      this.toolStripStatusLabelFileName.Text = "-";
      this.toolStripStatusLabelFileName.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
      // 
      // menuStrip1
      // 
      this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemFile,
            this.ToolStripMenuItemHelp});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
      this.menuStrip1.Size = new System.Drawing.Size(784, 24);
      this.menuStrip1.TabIndex = 2;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // ToolStripMenuItemFile
      // 
      this.ToolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSaveResultText,
            this.toolStripMenuItem2,
            this.ToolStripMenuItemImportDicFile,
            this.toolStripMenuItem3,
            this.ToolStripMenuItemAppExit});
      this.ToolStripMenuItemFile.Name = "ToolStripMenuItemFile";
      this.ToolStripMenuItemFile.Size = new System.Drawing.Size(67, 20);
      this.ToolStripMenuItemFile.Text = "ファイル(&F)";
      // 
      // ToolStripMenuItemSaveResultText
      // 
      this.ToolStripMenuItemSaveResultText.Name = "ToolStripMenuItemSaveResultText";
      this.ToolStripMenuItemSaveResultText.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.ToolStripMenuItemSaveResultText.Size = new System.Drawing.Size(236, 22);
      this.ToolStripMenuItemSaveResultText.Text = "検索結果を保存する(&S)...";
      this.ToolStripMenuItemSaveResultText.Click += new System.EventHandler(this.ToolStripMenuItemSaveResultText_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(233, 6);
      // 
      // ToolStripMenuItemImportDicFile
      // 
      this.ToolStripMenuItemImportDicFile.Name = "ToolStripMenuItemImportDicFile";
      this.ToolStripMenuItemImportDicFile.Size = new System.Drawing.Size(236, 22);
      this.ToolStripMenuItemImportDicFile.Text = "辞書ファイルのインポート(&I)...";
      this.ToolStripMenuItemImportDicFile.Click += new System.EventHandler(this.ToolStripMenuItemImportDicFile_Click);
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(233, 6);
      // 
      // ToolStripMenuItemAppExit
      // 
      this.ToolStripMenuItemAppExit.Name = "ToolStripMenuItemAppExit";
      this.ToolStripMenuItemAppExit.Size = new System.Drawing.Size(236, 22);
      this.ToolStripMenuItemAppExit.Text = "終了する(&X)";
      this.ToolStripMenuItemAppExit.Click += new System.EventHandler(this.ToolStripMenuItemAppExit_Click);
      // 
      // ToolStripMenuItemHelp
      // 
      this.ToolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemHelpContent,
            this.toolStripMenuItem1,
            this.ToolStripMenuItemAbout});
      this.ToolStripMenuItemHelp.Name = "ToolStripMenuItemHelp";
      this.ToolStripMenuItemHelp.Size = new System.Drawing.Size(65, 20);
      this.ToolStripMenuItemHelp.Text = "ヘルプ(&H)";
      // 
      // ToolStripMenuItemHelpContent
      // 
      this.ToolStripMenuItemHelpContent.Name = "ToolStripMenuItemHelpContent";
      this.ToolStripMenuItemHelpContent.Size = new System.Drawing.Size(167, 22);
      this.ToolStripMenuItemHelpContent.Text = "ヘルプ(&H)";
      this.ToolStripMenuItemHelpContent.Click += new System.EventHandler(this.ToolStripMenuItemHelpContent_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(164, 6);
      // 
      // ToolStripMenuItemAbout
      // 
      this.ToolStripMenuItemAbout.Name = "ToolStripMenuItemAbout";
      this.ToolStripMenuItemAbout.Size = new System.Drawing.Size(167, 22);
      this.ToolStripMenuItemAbout.Text = "バージョン情報(&A)...";
      this.ToolStripMenuItemAbout.Click += new System.EventHandler(this.ToolStripMenuItemAbout_Click);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPageMain);
      this.tabControl1.Controls.Add(this.tabPageSettings);
      this.tabControl1.Controls.Add(this.tabPageDictionary);
      this.tabControl1.Location = new System.Drawing.Point(10, 22);
      this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(588, 331);
      this.tabControl1.TabIndex = 8;
      this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
      // 
      // tabPageMain
      // 
      this.tabPageMain.Controls.Add(this.pictureBoxArrowDown);
      this.tabPageMain.Controls.Add(this.textBoxOutput);
      this.tabPageMain.Controls.Add(this.textBoxInput);
      this.tabPageMain.Location = new System.Drawing.Point(4, 22);
      this.tabPageMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.tabPageMain.Name = "tabPageMain";
      this.tabPageMain.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.tabPageMain.Size = new System.Drawing.Size(580, 305);
      this.tabPageMain.TabIndex = 0;
      this.tabPageMain.Text = "ライミング解析";
      this.tabPageMain.UseVisualStyleBackColor = true;
      // 
      // pictureBoxArrowDown
      // 
      this.pictureBoxArrowDown.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.pictureBoxArrowDown.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxArrowDown.Image")));
      this.pictureBoxArrowDown.Location = new System.Drawing.Point(254, 134);
      this.pictureBoxArrowDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.pictureBoxArrowDown.Name = "pictureBoxArrowDown";
      this.pictureBoxArrowDown.Size = new System.Drawing.Size(32, 32);
      this.pictureBoxArrowDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBoxArrowDown.TabIndex = 10;
      this.pictureBoxArrowDown.TabStop = false;
      // 
      // textBoxOutput
      // 
      this.textBoxOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.textBoxOutput.Location = new System.Drawing.Point(19, 170);
      this.textBoxOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.textBoxOutput.Multiline = true;
      this.textBoxOutput.Name = "textBoxOutput";
      this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.textBoxOutput.Size = new System.Drawing.Size(523, 114);
      this.textBoxOutput.TabIndex = 9;
      this.textBoxOutput.WordWrap = false;
      // 
      // textBoxInput
      // 
      this.textBoxInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.textBoxInput.Location = new System.Drawing.Point(19, 20);
      this.textBoxInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.textBoxInput.Multiline = true;
      this.textBoxInput.Name = "textBoxInput";
      this.textBoxInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.textBoxInput.Size = new System.Drawing.Size(523, 110);
      this.textBoxInput.TabIndex = 8;
      this.textBoxInput.WordWrap = false;
      this.textBoxInput.TextChanged += new System.EventHandler(this.textBoxInput_TextChanged);
      // 
      // tabPageSettings
      // 
      this.tabPageSettings.Controls.Add(this.labelHits03);
      this.tabPageSettings.Controls.Add(this.labelHits02);
      this.tabPageSettings.Controls.Add(this.labelHits01);
      this.tabPageSettings.Controls.Add(this.labelTotalMatchWords);
      this.tabPageSettings.Controls.Add(this.textBoxTotalMatchWords);
      this.tabPageSettings.Controls.Add(this.labelMaxPronunciationWords);
      this.tabPageSettings.Controls.Add(this.numericUpDownMaxPronunciationWords);
      this.tabPageSettings.Controls.Add(this.labelMaxVowelWords);
      this.tabPageSettings.Controls.Add(this.numericUpDownMaxVowelWords);
      this.tabPageSettings.Controls.Add(this.labelPercent);
      this.tabPageSettings.Controls.Add(this.labelMatchThreshold);
      this.tabPageSettings.Controls.Add(this.numericUpDownMatchThreshold);
      this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
      this.tabPageSettings.Name = "tabPageSettings";
      this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageSettings.Size = new System.Drawing.Size(580, 305);
      this.tabPageSettings.TabIndex = 2;
      this.tabPageSettings.Text = "動作設定";
      this.tabPageSettings.UseVisualStyleBackColor = true;
      // 
      // labelHits03
      // 
      this.labelHits03.AutoSize = true;
      this.labelHits03.Location = new System.Drawing.Point(330, 193);
      this.labelHits03.Name = "labelHits03";
      this.labelHits03.Size = new System.Drawing.Size(17, 12);
      this.labelHits03.TabIndex = 11;
      this.labelHits03.Text = "件";
      // 
      // labelHits02
      // 
      this.labelHits02.AutoSize = true;
      this.labelHits02.Location = new System.Drawing.Point(331, 145);
      this.labelHits02.Name = "labelHits02";
      this.labelHits02.Size = new System.Drawing.Size(17, 12);
      this.labelHits02.TabIndex = 10;
      this.labelHits02.Text = "件";
      // 
      // labelHits01
      // 
      this.labelHits01.AutoSize = true;
      this.labelHits01.Location = new System.Drawing.Point(331, 101);
      this.labelHits01.Name = "labelHits01";
      this.labelHits01.Size = new System.Drawing.Size(17, 12);
      this.labelHits01.TabIndex = 9;
      this.labelHits01.Text = "件";
      // 
      // labelTotalMatchWords
      // 
      this.labelTotalMatchWords.AutoSize = true;
      this.labelTotalMatchWords.Location = new System.Drawing.Point(80, 193);
      this.labelTotalMatchWords.Name = "labelTotalMatchWords";
      this.labelTotalMatchWords.Size = new System.Drawing.Size(117, 12);
      this.labelTotalMatchWords.TabIndex = 8;
      this.labelTotalMatchWords.Text = "合計の検索結果件数: ";
      // 
      // textBoxTotalMatchWords
      // 
      this.textBoxTotalMatchWords.Location = new System.Drawing.Point(204, 189);
      this.textBoxTotalMatchWords.Name = "textBoxTotalMatchWords";
      this.textBoxTotalMatchWords.ReadOnly = true;
      this.textBoxTotalMatchWords.Size = new System.Drawing.Size(120, 19);
      this.textBoxTotalMatchWords.TabIndex = 7;
      this.textBoxTotalMatchWords.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // labelMaxPronunciationWords
      // 
      this.labelMaxPronunciationWords.AutoSize = true;
      this.labelMaxPronunciationWords.Location = new System.Drawing.Point(39, 145);
      this.labelMaxPronunciationWords.Name = "labelMaxPronunciationWords";
      this.labelMaxPronunciationWords.Size = new System.Drawing.Size(158, 12);
      this.labelMaxPronunciationWords.TabIndex = 6;
      this.labelMaxPronunciationWords.Text = "発音と一致する検索結果件数: ";
      // 
      // numericUpDownMaxPronunciationWords
      // 
      this.numericUpDownMaxPronunciationWords.Location = new System.Drawing.Point(204, 143);
      this.numericUpDownMaxPronunciationWords.Name = "numericUpDownMaxPronunciationWords";
      this.numericUpDownMaxPronunciationWords.Size = new System.Drawing.Size(120, 19);
      this.numericUpDownMaxPronunciationWords.TabIndex = 5;
      this.numericUpDownMaxPronunciationWords.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.numericUpDownMaxPronunciationWords.ValueChanged += new System.EventHandler(this.numericUpDownMaxPronunciationWords_ValueChanged);
      // 
      // labelMaxVowelWords
      // 
      this.labelMaxVowelWords.AutoSize = true;
      this.labelMaxVowelWords.Location = new System.Drawing.Point(39, 101);
      this.labelMaxVowelWords.Name = "labelMaxVowelWords";
      this.labelMaxVowelWords.Size = new System.Drawing.Size(159, 12);
      this.labelMaxVowelWords.TabIndex = 4;
      this.labelMaxVowelWords.Text = "母音に一致する検索結果件数: ";
      // 
      // numericUpDownMaxVowelWords
      // 
      this.numericUpDownMaxVowelWords.Location = new System.Drawing.Point(204, 96);
      this.numericUpDownMaxVowelWords.Name = "numericUpDownMaxVowelWords";
      this.numericUpDownMaxVowelWords.Size = new System.Drawing.Size(120, 19);
      this.numericUpDownMaxVowelWords.TabIndex = 3;
      this.numericUpDownMaxVowelWords.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.numericUpDownMaxVowelWords.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
      this.numericUpDownMaxVowelWords.ValueChanged += new System.EventHandler(this.numericUpDownMaxVowelWords_ValueChanged);
      // 
      // labelPercent
      // 
      this.labelPercent.AutoSize = true;
      this.labelPercent.Location = new System.Drawing.Point(264, 53);
      this.labelPercent.Name = "labelPercent";
      this.labelPercent.Size = new System.Drawing.Size(11, 12);
      this.labelPercent.TabIndex = 2;
      this.labelPercent.Text = "%";
      // 
      // labelMatchThreshold
      // 
      this.labelMatchThreshold.AutoSize = true;
      this.labelMatchThreshold.Location = new System.Drawing.Point(39, 53);
      this.labelMatchThreshold.Name = "labelMatchThreshold";
      this.labelMatchThreshold.Size = new System.Drawing.Size(93, 12);
      this.labelMatchThreshold.TabIndex = 1;
      this.labelMatchThreshold.Text = "検索語の一致率: ";
      // 
      // numericUpDownMatchThreshold
      // 
      this.numericUpDownMatchThreshold.Location = new System.Drawing.Point(138, 49);
      this.numericUpDownMatchThreshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numericUpDownMatchThreshold.Name = "numericUpDownMatchThreshold";
      this.numericUpDownMatchThreshold.Size = new System.Drawing.Size(120, 19);
      this.numericUpDownMatchThreshold.TabIndex = 0;
      this.numericUpDownMatchThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.numericUpDownMatchThreshold.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
      this.numericUpDownMatchThreshold.ValueChanged += new System.EventHandler(this.numericUpDownMatchThreshold_ValueChanged);
      // 
      // tabPageDictionary
      // 
      this.tabPageDictionary.Controls.Add(this.tableLayoutPanel1);
      this.tabPageDictionary.Controls.Add(this.panelDragAndDropArea);
      this.tabPageDictionary.Location = new System.Drawing.Point(4, 22);
      this.tabPageDictionary.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.tabPageDictionary.Name = "tabPageDictionary";
      this.tabPageDictionary.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.tabPageDictionary.Size = new System.Drawing.Size(580, 305);
      this.tabPageDictionary.TabIndex = 1;
      this.tabPageDictionary.Text = "辞書管理";
      this.tabPageDictionary.UseVisualStyleBackColor = true;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Controls.Add(this.dataGridView2, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 0);
      this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
      this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(466, 167);
      this.tableLayoutPanel1.TabIndex = 6;
      // 
      // dataGridView2
      // 
      this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView2.Location = new System.Drawing.Point(236, 2);
      this.dataGridView2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.dataGridView2.Name = "dataGridView2";
      this.dataGridView2.Size = new System.Drawing.Size(206, 120);
      this.dataGridView2.TabIndex = 1;
      // 
      // dataGridView1
      // 
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Location = new System.Drawing.Point(3, 2);
      this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.Size = new System.Drawing.Size(206, 120);
      this.dataGridView1.TabIndex = 0;
      // 
      // panelDragAndDropArea
      // 
      this.panelDragAndDropArea.AllowDrop = true;
      this.panelDragAndDropArea.Controls.Add(this.labelDragAndDropHere);
      this.panelDragAndDropArea.Controls.Add(this.checkBoxDataBaseInit);
      this.panelDragAndDropArea.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panelDragAndDropArea.Location = new System.Drawing.Point(3, 215);
      this.panelDragAndDropArea.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.panelDragAndDropArea.Name = "panelDragAndDropArea";
      this.panelDragAndDropArea.Size = new System.Drawing.Size(574, 88);
      this.panelDragAndDropArea.TabIndex = 5;
      this.panelDragAndDropArea.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelDragAndDropArea_DragDrop);
      this.panelDragAndDropArea.DragEnter += new System.Windows.Forms.DragEventHandler(this.panelDragAndDropArea_DragEnter);
      this.panelDragAndDropArea.DragLeave += new System.EventHandler(this.panelDragAndDropArea_DragLeave);
      this.panelDragAndDropArea.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDragAndDropArea_Paint);
      // 
      // labelDragAndDropHere
      // 
      this.labelDragAndDropHere.AutoSize = true;
      this.labelDragAndDropHere.Location = new System.Drawing.Point(116, 37);
      this.labelDragAndDropHere.Name = "labelDragAndDropHere";
      this.labelDragAndDropHere.Size = new System.Drawing.Size(334, 12);
      this.labelDragAndDropHere.TabIndex = 6;
      this.labelDragAndDropHere.Text = "追加したい辞書(CSV形式)ファイルをここにドラッグ＆ドロップしてください";
      // 
      // checkBoxDataBaseInit
      // 
      this.checkBoxDataBaseInit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.checkBoxDataBaseInit.AutoSize = true;
      this.checkBoxDataBaseInit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.checkBoxDataBaseInit.Location = new System.Drawing.Point(12, 62);
      this.checkBoxDataBaseInit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.checkBoxDataBaseInit.Name = "checkBoxDataBaseInit";
      this.checkBoxDataBaseInit.Size = new System.Drawing.Size(145, 16);
      this.checkBoxDataBaseInit.TabIndex = 5;
      this.checkBoxDataBaseInit.Text = "データベースを初期化する";
      this.checkBoxDataBaseInit.UseVisualStyleBackColor = true;
      // 
      // panelButtons
      // 
      this.panelButtons.Controls.Add(this.buttonExit);
      this.panelButtons.Controls.Add(this.buttonRhymingSearch);
      this.panelButtons.Controls.Add(this.buttonMorphologicalAnalysis);
      this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panelButtons.Location = new System.Drawing.Point(0, 410);
      this.panelButtons.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.panelButtons.Name = "panelButtons";
      this.panelButtons.Size = new System.Drawing.Size(784, 49);
      this.panelButtons.TabIndex = 10;
      // 
      // buttonExit
      // 
      this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonExit.Location = new System.Drawing.Point(663, 14);
      this.buttonExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.buttonExit.Name = "buttonExit";
      this.buttonExit.Size = new System.Drawing.Size(100, 24);
      this.buttonExit.TabIndex = 11;
      this.buttonExit.Text = "終了(&X)";
      this.buttonExit.UseVisualStyleBackColor = true;
      this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
      // 
      // buttonRhymingSearch
      // 
      this.buttonRhymingSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonRhymingSearch.Enabled = false;
      this.buttonRhymingSearch.Location = new System.Drawing.Point(546, 14);
      this.buttonRhymingSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.buttonRhymingSearch.Name = "buttonRhymingSearch";
      this.buttonRhymingSearch.Size = new System.Drawing.Size(100, 24);
      this.buttonRhymingSearch.TabIndex = 10;
      this.buttonRhymingSearch.Text = "ライム検索(&R)";
      this.buttonRhymingSearch.UseVisualStyleBackColor = true;
      this.buttonRhymingSearch.Click += new System.EventHandler(this.buttonRhymingSearch_Click);
      // 
      // buttonMorphologicalAnalysis
      // 
      this.buttonMorphologicalAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonMorphologicalAnalysis.Enabled = false;
      this.buttonMorphologicalAnalysis.Location = new System.Drawing.Point(14, 14);
      this.buttonMorphologicalAnalysis.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.buttonMorphologicalAnalysis.Name = "buttonMorphologicalAnalysis";
      this.buttonMorphologicalAnalysis.Size = new System.Drawing.Size(100, 24);
      this.buttonMorphologicalAnalysis.TabIndex = 7;
      this.buttonMorphologicalAnalysis.Text = "形態素解析(&A)";
      this.buttonMorphologicalAnalysis.UseVisualStyleBackColor = true;
      this.buttonMorphologicalAnalysis.Click += new System.EventHandler(this.buttonMorphologicalAnalysis_Click);
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.CheckPathExists = false;
      this.saveFileDialog1.DefaultExt = "txt";
      this.saveFileDialog1.FileName = "RymingResult.txt";
      this.saveFileDialog1.Filter = "テキストファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
      this.saveFileDialog1.Title = "検索結果をテキストファイルに保存";
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      this.openFileDialog1.Filter = "CSVファイル(*.csv)|*.csv|テキストファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
      this.openFileDialog1.Multiselect = true;
      this.openFileDialog1.Title = "辞書ファイルのインポート";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.buttonExit;
      this.ClientSize = new System.Drawing.Size(784, 481);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.panelButtons);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.menuStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip1;
      this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.MinimumSize = new System.Drawing.Size(600, 520);
      this.Name = "Form1";
      this.Text = "ライミング・ツール";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
      this.Load += new System.EventHandler(this.Form1_Load);
      this.Shown += new System.EventHandler(this.Form1_Shown);
      this.Resize += new System.EventHandler(this.Form1_Resize);
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.tabPageMain.ResumeLayout(false);
      this.tabPageMain.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArrowDown)).EndInit();
      this.tabPageSettings.ResumeLayout(false);
      this.tabPageSettings.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxPronunciationWords)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxVowelWords)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMatchThreshold)).EndInit();
      this.tabPageDictionary.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.panelDragAndDropArea.ResumeLayout(false);
      this.panelDragAndDropArea.PerformLayout();
      this.panelButtons.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private StatusStrip statusStrip1;
    private ToolStripProgressBar toolStripProgressBar;
    private ToolStripStatusLabel toolStripStatusLabelPercent;
    private ToolStripStatusLabel toolStripStatusLabelFileName;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem ToolStripMenuItemFile;
    private ToolStripMenuItem ToolStripMenuItemHelp;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripMenuItem ToolStripMenuItemAbout;
    private ToolStripStatusLabel toolStripStatusLabelEncoding;
    private TabControl tabControl1;
    private TabPage tabPageMain;
    private PictureBox pictureBoxArrowDown;
    private TextBox textBoxOutput;
    private TextBox textBoxInput;
    private TabPage tabPageDictionary;
    private Panel panelButtons;
    private Button buttonExit;
    private Button buttonRhymingSearch;
    private Button buttonMorphologicalAnalysis;
    private ToolStripStatusLabel toolStripStatusLabel1;
    private Panel panelDragAndDropArea;
    private CheckBox checkBoxDataBaseInit;
    private Label labelDragAndDropHere;
    private TableLayoutPanel tableLayoutPanel1;
    private DataGridView dataGridView2;
    private DataGridView dataGridView1;
    private TabPage tabPageSettings;
    private NumericUpDown numericUpDownMatchThreshold;
    private Label labelPercent;
    private Label labelMatchThreshold;
    private NumericUpDown numericUpDownMaxVowelWords;
    private NumericUpDown numericUpDownMaxPronunciationWords;
    private Label labelMaxVowelWords;
    private TextBox textBoxTotalMatchWords;
    private Label labelMaxPronunciationWords;
    private Label labelTotalMatchWords;
    private Label labelHits01;
    private Label labelHits02;
    private Label labelHits03;
    private ToolStripMenuItem ToolStripMenuItemSaveResultText;
    private ToolStripSeparator toolStripMenuItem2;
    private ToolStripMenuItem ToolStripMenuItemImportDicFile;
    private ToolStripSeparator toolStripMenuItem3;
    private ToolStripMenuItem ToolStripMenuItemAppExit;
    private ToolStripMenuItem ToolStripMenuItemHelpContent;
    private SaveFileDialog saveFileDialog1;
    private OpenFileDialog openFileDialog1;
  }
}
