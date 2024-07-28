using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ude;
using static RhymingTool.DictionaryCreator;

namespace RhymingTool
{
  public partial class Form1 : Form
  {
    private const string dbName = "dictionary.db";
    private string dictionaryDBFilePath;

    private int currentLines;

    private Color borderColor = Color.FromArgb(200, 200, 200); // 初期枠線色
    private readonly Color dragOverBorderColor = Color.FromArgb(60, 60, 60); // ドラッグ中の枠線色（ちょっと濃くなる）


    // 実行ファイルのパスを取得
    private static readonly string executablePath = Application.StartupPath;

    // Program FilesまたはProgram Files(x86)に存在するかをチェック
    private static readonly bool isProgramFiles =
      executablePath.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)) ||
      executablePath.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));

    public static string MeCabDicPath;

    public Form1()
    {
      InitializeComponent();


      if (isProgramFiles)
      {
        // AppDataフォルダーのパスを取得
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        MeCabDicPath = Path.Combine(appDataPath, "RhymingTool", "dic");
      }
      else
      {
        // 実行ファイル直下のdicフォルダーのパスを設定
        MeCabDicPath = Path.Combine(executablePath, "dic");
      }

      if (!Directory.Exists(MeCabDicPath))
      {
        MessageBox.Show($@"dicフォルダーが見つかりません。適切な場所にdicフォルダーを配置してください。\n現在のdicパス: {MeCabDicPath}",
          @"エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Application.Exit();
      }



    }

    private void Form1_Load(object sender, EventArgs e)
    {
      //-----------------------------------
      // 形態素解析用辞書があるかをチェック

      // 実行ファイル直下に辞書データがある
      dictionaryDBFilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty, dbName);
      if (File.Exists(dictionaryDBFilePath))
      {
        // DBファイルある
      }
      else
      {
        // アプリデータフォルダーに辞書データがある
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        dictionaryDBFilePath = Path.Combine(appDataPath, "RhymingTool", dbName);
        if (File.Exists(dictionaryDBFilePath))
        {
          // DBファイルある
        }
        else
        {
          // どこにも辞書データが存在しない
          MessageBox.Show(
            @"ライミング・ツールが使用する辞書ファイルが見つかりません。" + Environment.NewLine +
            Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty, dbName) + Environment.NewLine +
            @"または、" +
            Path.Combine(appDataPath, "RhymingTool", dbName) + Environment.NewLine + @"アプリケーションを起動できません。",
            @"エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

          // 辞書がないので起動しない
          Application.Exit();
        }
      }

      //-----------------------------------
      // フォームコントロールの配置

      tabControl1.Dock = DockStyle.Fill;

      // サンプル・リリック
      //textBoxInput.Text = @"俺は東京生まれヒップポップ育ち 悪そうな奴は大体友達";
      textBoxInput.Text = Properties.Settings.Default.InputText;

      //-----------------------------------
      // テーブルレイアウト（辞書情報の表示）
      tableLayoutPanel1.Dock = DockStyle.Fill;
      dataGridView1.Dock = DockStyle.Fill;
      dataGridView2.Dock = DockStyle.Fill;

      SetupDataGridView(dataGridView1);
      SetupDataGridView(dataGridView2);

      //-----------------------------------
      // ウィンドウの表示位置
      var posX = Properties.Settings.Default.FormLeft;
      var posY = Properties.Settings.Default.FormTop;
      if (posX < 0 || posY < 0)
      {
        // 無効な場合はスクリーン中央に表示
        this.StartPosition = FormStartPosition.CenterScreen;
      }
      else
      {
        this.StartPosition = FormStartPosition.Manual;
        this.Location = new Point(posX, posY);
      }

      //-----------------------------------
      // ウィンドウサイズ
      var width = Properties.Settings.Default.FormWidth;
      var height = Properties.Settings.Default.FormHeight;

      if (width < this.MinimumSize.Width || height < this.MinimumSize.Height)
      {
        this.Width = this.MinimumSize.Width;
        this.Height = this.MinimumSize.Height;
      }
      else
      {
        this.Width = width;
        this.Height = height;
      }

      //-----------------------------------
      // 設定画面

      // 一致率（％）
      numericUpDownMatchThreshold.Value = Properties.Settings.Default.MatchThreshold;
      // 母音に一致する検索結果件数の最大値
      numericUpDownMaxVowelWords.Value = Properties.Settings.Default.MaxVowelWords;
      // 発音に一致する検索結果件数の最大値
      numericUpDownMaxPronunciationWords.Value = Properties.Settings.Default.MaxPronunciationWords;

      // 検索結果合計値
      textBoxTotalMatchWords.Text = (numericUpDownMaxVowelWords.Value + numericUpDownMaxPronunciationWords.Value).ToString(CultureInfo.CurrentCulture);

      //-----------------------------------
      // 開いているタブ
      //var tabIndex = Properties.Settings.Default.TabIndex;
      //tabControl1.SelectedIndex = tabIndex < tabControl1.TabCount ? tabIndex : 0;

      // バックグラウンドでDB詳細情報を取得しているため
      tabControl1.SelectedIndex = 0;

    }

    private void Form1_Shown(object sender, EventArgs e)
    {
      // 非同期タスクをバックグラウンドで実行
      _ = Task.Run(ShowDataBaseSummary);

      // いったん表示してから
      this.Show();

      // リサイズイベントを一回呼ぶ
      Form1_Resize(sender, e);

      // 入力ボックスにフォーカス
      textBoxInput.Focus();
      // 文字列を選択状態にする
      textBoxInput.SelectAll();

      // 非同期でデータベースの詳細情報を取得する（GUIをロックしないため）
      //Task.Run(ShowDataBaseSummary);

    }

    private void Form1_Resize(object sender, EventArgs e)
    {
      pictureBoxArrowDown.Left = tabPageMain.Width / 2 - pictureBoxArrowDown.Width / 2;
      pictureBoxArrowDown.Top = tabPageMain.Height / 2 - pictureBoxArrowDown.Height / 2;

      textBoxInput.Top = 16;
      textBoxInput.Left = 16;
      textBoxInput.Width = tabPageMain.Width - 32;
      textBoxInput.Height = tabPageMain.Height / 2 - pictureBoxArrowDown.Height / 2 - 16;

      textBoxOutput.Top = pictureBoxArrowDown.Top + pictureBoxArrowDown.Height + 8;
      textBoxOutput.Left = 16;
      textBoxOutput.Width = tabPageMain.Width - 32;
      textBoxOutput.Height = tabPageMain.Height / 2 - pictureBoxArrowDown.Height / 2 - 16;

      // 「追加したい辞書(CSV形式)ファイルをここにドラッグ＆ドロップしてください」文字列
      labelDragAndDropHere.Top = panelDragAndDropArea.Height / 2 - labelDragAndDropHere.Height / 2;
      labelDragAndDropHere.Left = panelDragAndDropArea.Width / 2 - labelDragAndDropHere.Width / 2;

    }

    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
      // ウィンドウ位置を保存
      Properties.Settings.Default.FormTop = this.Top;
      Properties.Settings.Default.FormLeft = this.Left;
      // ウィンドウサイズを保存
      Properties.Settings.Default.FormWidth = this.Width;
      Properties.Settings.Default.FormHeight = this.Height;
      // タブインデックスを保存
      //Properties.Settings.Default.TabIndex = tabControl1.SelectedIndex;

      Properties.Settings.Default.Save();
    }

    private void buttonExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private static void SetupDataGridView(DataGridView dataGridView)
    {
      dataGridView.RowHeadersVisible = false; // 行ヘッダーを非表示
      dataGridView.ColumnHeadersVisible = false; // カラムヘッダーを非表示
      dataGridView.ColumnCount = 2; // カラム数を2に設定
      dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      dataGridView.ReadOnly = true; // 編集不可に設定
      dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // 行全体を選択
      dataGridView.MultiSelect = false; // 複数選択を無効
      dataGridView.AllowUserToAddRows = false; // ユーザーによる行追加を無効

      // 背景色を白に設定
      dataGridView.BackgroundColor = Color.White;
      dataGridView.DefaultCellStyle.BackColor = Color.White;
      dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

      // 外枠の色とスタイルを設定
      dataGridView.BorderStyle = BorderStyle.None;
      dataGridView.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
    }


    private static int GetVisibleRowCount(DataGridView dataGridView)
    {
      // DataGridViewのクライアント領域の高さからヘッダーの高さを引いた値を行の高さで割る
      var rowHeight = dataGridView.RowTemplate.Height;
      var clientHeight = dataGridView.ClientSize.Height;
      var headerHeight = dataGridView.ColumnHeadersVisible ? dataGridView.ColumnHeadersHeight : 0;
      return (clientHeight - headerHeight) / rowHeight;
    }

    private void Creator_EncodingDetected(object sender, EncodingDetectedEventArgs e)
    {
      // UI スレッドで実行する必要がある場合
      this.Invoke((MethodInvoker)delegate
      {
        toolStripStatusLabelEncoding.Text = e.EncodingName;
      });
    }

    /// <summary>
    /// データベースの詳細情報を非同期で取得して表示する
    /// </summary>
    async void ShowDataBaseSummary()
    {
      //-----------------------------------
      // 辞書の詳細情報を取得する
      var _dbInfo = new DatabaseInfo(dictionaryDBFilePath);
      try
      {
        await _dbInfo.LoadInfoAsync();

        // サマリーを Key-value で取得する
        var dic = _dbInfo.GetSummary();
        var i = 0;
        // 各列のデータグリッドビューの最大値を取得して折り返しを判定
        var maxRows = GetVisibleRowCount(dataGridView1);
        foreach (var kvp in dic)
        {
          if (i < maxRows)
          {
            dataGridView1.Rows.Add(kvp.Key, kvp.Value);
          }
          else
          {
            dataGridView2.Rows.Add(kvp.Key, kvp.Value);
          }
          i++;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(@$"データベース情報の取得中にエラーが発生しました: {ex.Message}", @"エラー", MessageBoxButtons.OK,
          MessageBoxIcon.Error);
      }

      //-----------------------------------
      // デフォルトの選択状態を解除
      dataGridView1.ClearSelection();
      dataGridView2.ClearSelection();
    }

    /// <summary>
    /// CSV形式の辞書ファイルをインポートする
    /// </summary>
    /// <param name="csvFilePaths"></param>
    private async void ImportCsvFiles(List<string> csvFilePaths)
    {
      if (File.Exists(dictionaryDBFilePath) && checkBoxDataBaseInit.Checked)
      {
        var ret =
          MessageBox.Show(this, @"すでにデータベースファイルが存在しています。削除して新たに作成しますか？" + Environment.NewLine +
            dictionaryDBFilePath, @"問い合わせ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

        if (ret == DialogResult.Yes)
        {
          try
          {
            // データベースファイルを削除
            File.Delete(dictionaryDBFilePath);
          }
          catch (Exception ex)
          {
            string MsgText;
            switch (ex)
            {
              case ArgumentNullException:
                MsgText = "長さ 0 の文字列、空白のみを含む、または 1 つ以上の無効な文字を含みます。" + Environment.NewLine + ex.Message;
                break;
              case DirectoryNotFoundException:
                MsgText = "指定されたパスが正しくありません (たとえば、マップされていないドライブにあるなど)。" + Environment.NewLine + ex.Message;
                break;
              case IOException:
                MsgText = "指定されたファイルは、使用されています。" + Environment.NewLine + ex.Message;
                break;
              case NotSupportedException:
                MsgText = "指定されたパスの形式が正しくありません。" + Environment.NewLine + ex.Message;
                break;
              case UnauthorizedAccessException:
                MsgText = "呼び出し元に、必要なアクセス許可がありません。" + Environment.NewLine + ex.Message;
                break;
              default:
                MsgText = ex.Message;
                break;
            }
            // エラーメッセージ
            MessageBox.Show(this, MsgText, @"データベース削除失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }
        }
        else
        {
          return;
        }
      }

      toolStripProgressBar.Value = 0;
      var totalLines = 0;
      toolStripStatusLabelFileName.Text = @"処理する合計を数えています...";

      // 最初に処理する全行の値を取得する
      await Task.Run(() =>
      {
        totalLines += csvFilePaths.Sum(filePath => File.ReadLines(filePath).Count());
      });

      toolStripProgressBar.Maximum = totalLines;

      currentLines = 0;

      try
      {
        var progress = new Progress<int>(value =>
        {
          // プログレスバーの更新
          toolStripProgressBar.Value = value;

          // パーセント表示
          var percentage = (double)value / totalLines * 100;
          toolStripStatusLabelPercent.Text = @$"{percentage:F0} %";

          statusStrip1.Update();

        });

        toolStripStatusLabelFileName.Text = @"データベースを新規作成しています...";

        var _creator = new DictionaryCreator(dictionaryDBFilePath);

        await _creator.CreateDatabaseAsync();

        toolStripStatusLabelFileName.Text = @"データベースは正常に作成されました。";

        foreach (var filePath in csvFilePaths)
        {
          toolStripStatusLabelFileName.Text = @"辞書を作成しています...";
          currentLines = await _creator.ImportFromCsvAsync(filePath, progress, currentLines, totalLines);
        }

        toolStripStatusLabelFileName.Text = @"辞書の作成が完了しました。";

        // プログレスバーを直接操作して「100％」にする
        toolStripProgressBar.Value = toolStripProgressBar.Maximum;
        toolStripStatusLabelPercent.Text = @"100 %";

      }
      catch (Exception ex)
      {
        MessageBox.Show(@$"An error occurred: {ex.Message}");
      }

    }

    /// <summary>
    /// 投入されたテキストファイルがCSVファイル形式かを簡易的にチェックする
    /// </summary>
    /// <param name="filePath">CSVファイル（テキストファイル）と思しきファイルパス</param>
    /// <returns></returns>
    public bool IsSimpleCheckCsvFile(string filePath)
    {
      // 最低でも「単語」「品詞」「読み方（全角カタカナ）」「発音（全角カタカナ）」の
      //「4つ」のデータが記載されているかをチェック
      try
      {
        // ファイルをバイト配列として読み込む
        var fileBytes = File.ReadAllBytes(filePath);

        // ファイルがテキストファイルかどうかを判断
        if (!IsTextFile(fileBytes))
        {
          return false;
        }

        // ファイルのエンコーディングを検出する
        var encoding = DetectEncoding(fileBytes);
        if (encoding == null)
        {
          return false;
        }

        // ファイルを行ごとに読み込む
        var fileContent = encoding.GetString(fileBytes);
        using var reader = new StringReader(fileContent);
        while (reader.ReadLine() is { } line)
        {
          // カンマ区切りで分割し、フィールド数をチェック
          var fields = line.Split(',');
          if (fields.Length >= 4)
          {
            return true;
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($@"Error reading file: {ex.Message}");
        return false;
      }

      return false;
    }

    private static bool IsTextFile(byte[] fileBytes)
    {
      const int MaxBytesToCheck = 1024; // 最初の1KBをチェック
      var len = fileBytes.Length > MaxBytesToCheck ? MaxBytesToCheck : fileBytes.Length;

      for (var i = 0; i < len; i++)
      {
        var b = fileBytes[i];
        if (b > 0x7F && b != 0x85 && b != 0xA0 && b != 0xA1 && b != 0xAD && b is < 0xC2 or > 0xF4)
        {
          return false; // 非テキストバイトが見つかった場合
        }
      }
      return true;
    }

    /// <summary>
    /// テキストエンコーディングのチェック
    /// </summary>
    /// <param name="fileBytes">検査するバイト列</param>
    /// <returns></returns>
    private Encoding DetectEncoding(byte[] fileBytes)
    {
      var detector = new CharsetDetector();
      detector.Feed(fileBytes, 0, fileBytes.Length);
      detector.DataEnd();
      return detector.Charset != null ? Encoding.GetEncoding(detector.Charset) : null;
    }

    /// <summary>
    ///「検索結果を保存する(S)...」メニュー
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ToolStripMenuItemSaveResultText_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(textBoxOutput.Text)) return;

      // SaveFileDialogを表示します
      if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

      // テキストボックスの内容をファイルに書き込みます
      try
      {
        File.WriteAllText(saveFileDialog1.FileName, textBoxOutput.Text);
      }
      catch (Exception ex)
      {
        MessageBox.Show($@"テキスト保存エラー: {ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    /// <summary>
    ///「辞書ファイルのインポート(I)」メニュー
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ToolStripMenuItemImportDicFile_Click(object sender, EventArgs e)
    {
      if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

      var csvFiles = openFileDialog1.FileNames;

      // 読み込んだファイルがCSVファイル形式かを簡易チェックする
      foreach (var csvFile in csvFiles.Where(csvFile => !IsSimpleCheckCsvFile(csvFile)))
      {
        // 一個でも怪しいのがあればインポートは中止
        MessageBox.Show(@"CSV形式ではないファイルが含まれています。インポートを中止します。" + Environment.NewLine + csvFile,
          @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      // string[] を List<string> に変換
      var fileList = csvFiles.ToList();
      // 辞書ファイルのインポート開始
      ImportCsvFiles(fileList);
    }

    /// <summary>
    ///「アプリケーションの終了」メニュー
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ToolStripMenuItemAppExit_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    /// <summary>
    ///「ヘルプ」メニュー
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ToolStripMenuItemHelpContent_Click(object sender, EventArgs e)
    {
      var dirPath = Application.StartupPath;
      var readmeTextFilePath = Path.Combine(dirPath ?? string.Empty, "README.md");
      var manualPdfFilePath = Path.Combine(dirPath ?? string.Empty, "ライミング・ツール説明書.pdf");

      try
      {
        if (File.Exists(manualPdfFilePath))
        {
          // PDFファイルをデフォルトのアプリケーションで開く
          Process.Start(manualPdfFilePath);
        }
        else if (File.Exists(readmeTextFilePath))
        {
          // README.md をデフォルトのアプリケーションで開く
          Process.Start(readmeTextFilePath);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($@"Error opening file: {ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

    }

    /// <summary>
    ///「バージョン情報(A)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
    {
      var frm2 = new Form2
      {
        StartPosition = FormStartPosition.CenterParent
      };
      frm2.ShowDialog();
      frm2.Dispose();
    }

    private void textBoxInput_TextChanged(object sender, EventArgs e)
    {
      // 入力テキストボックスが空のときはボタンが押せない
      buttonMorphologicalAnalysis.Enabled = textBoxInput.Text != string.Empty;
      buttonRhymingSearch.Enabled = buttonMorphologicalAnalysis.Enabled;
    }

    private void buttonMorphologicalAnalysis_Click(object sender, EventArgs e)
    {

      buttonRhymingSearch.Enabled = false;
      textBoxInput.Enabled = false;

      // 入力したテキストを設定に保存する
      Properties.Settings.Default.InputText = textBoxInput.Text;
      Properties.Settings.Default.Save();

      MorphologicalAnalysis analyzer = null;

      try
      {
        // 形態素解析クラスのインスタンス
        analyzer = new MorphologicalAnalysis();
      }
      catch (Exception ex)
      {
        MessageBox.Show($@"MorphologicalAnalysisの初期化に失敗しました: {ex.Message}", @"エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }


      if (analyzer == null)
      {
        MessageBox.Show(@"MorphologicalAnalysisが初期化されていません。", @"エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      var inputText = textBoxInput.Text;
      var result = analyzer.Analyze(inputText);
      textBoxOutput.Text = result;

      buttonRhymingSearch.Enabled = true;
      textBoxInput.Enabled = true;

    }

    /// <summary>
    /// ライム検索ボタン
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void buttonRhymingSearch_Click(object sender, EventArgs e)
    {
      // 検索中に使用されては困るコントロールを無効化
      ToolStripMenuItemSaveResultText.Enabled = false;
      ToolStripMenuItemImportDicFile.Enabled = false;
      buttonRhymingSearch.Enabled = false;

      // 入力したテキストを設定に保存する
      Properties.Settings.Default.InputText = textBoxInput.Text;
      Properties.Settings.Default.Save();

      var _rhymingAnalyzer = new RhymingAnalyzer(dictionaryDBFilePath)
      {
        // 各プロパティの入力
        MatchThreshold = (int)numericUpDownMatchThreshold.Value, // 一致率（％）
        MaxVowelWords = (int)numericUpDownMaxVowelWords.Value, // 母音の検索結果件数の最大値
        MaxPronunciationWords = (int)numericUpDownMaxPronunciationWords.Value // 発音の検索結果件数の最大値
      };

      var inputText = textBoxInput.Text;

      // プログレスバーの設定
      toolStripProgressBar.Minimum = 0;
      toolStripProgressBar.Maximum = 100;
      toolStripProgressBar.Value = 0;

      // 進捗報告のデリゲートを設定
      _rhymingAnalyzer.ProgressReport = (percentage, status) =>
      {
        this.Invoke((MethodInvoker)delegate
        {
          toolStripProgressBar.Value = percentage;
          toolStripStatusLabelPercent.Text = percentage.ToString() + @"%";
          toolStripStatusLabelFileName.Text = status;
          statusStrip1.Update();
        });
      };

      try
      {
        var result = await _rhymingAnalyzer.AnalyzeAndRhymeAsync(inputText);
        textBoxOutput.Text = result;
      }
      catch (Exception ex)
      {
        var message = $"エラーの詳細:\n" +
                         $"メッセージ: {ex.Message}\n" +
                         $"スタックトレース: {ex.StackTrace}\n" +
                         $"データベースパス: {dictionaryDBFilePath}";
        MessageBox.Show(message, @"詳細エラー情報", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      finally
      {
        // 検索中に無効化したコントロールを有効に戻す
        ToolStripMenuItemSaveResultText.Enabled = true;
        ToolStripMenuItemImportDicFile.Enabled = true;
        buttonRhymingSearch.Enabled = true;
      }
    }

    private void panelDragAndDropArea_Paint(object sender, PaintEventArgs e)
    {
      // ペンの色、太さ、スタイルを設定
      using var pen = new Pen(borderColor);
      pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot; // 点線スタイル
      pen.Width = 4;
      // 点線の間隔をカスタマイズ
      pen.DashPattern = [4.0f, 1.0f]; // 4ピクセルの線と2ピクセルのスペース

      // パネルのクライアント矩形を取得
      var rect = panelDragAndDropArea.ClientRectangle;

      // 点線を描画
      e.Graphics.DrawRectangle(pen, rect);
    }

    private void panelDragAndDropArea_DragDrop(object sender, DragEventArgs e)
    {
      var arrayFiles = (string[])e.Data?.GetData(DataFormats.FileDrop, false)!;
      var csvFiles = (arrayFiles ?? []).Where(filePath => Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase)).ToList();

      // 読み込んだファイルがCSVファイル形式かを簡易チェックする
      foreach (var csvFile in csvFiles.Where(csvFile => !IsSimpleCheckCsvFile(csvFile)))
      {
        // 一個でも怪しいのがあればインポートは中止
        MessageBox.Show(@"CSV形式ではないファイルが含まれています。インポートを中止します。" + Environment.NewLine + csvFile,
          @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      // インポート中に使用されると困るコンポーネントの無効化
      ToolStripMenuItemSaveResultText.Enabled = true;
      ToolStripMenuItemImportDicFile.Enabled = true;
      buttonRhymingSearch.Enabled = true;

      if (csvFiles.Count > 0)
      {
        ImportCsvFiles(csvFiles);
      }

      // 有効に戻す
      ToolStripMenuItemSaveResultText.Enabled = true;
      ToolStripMenuItemImportDicFile.Enabled = true;
      buttonRhymingSearch.Enabled = true;

      panelDragAndDropArea.BackColor = Color.Transparent;
      borderColor = Color.FromArgb(60, 60, 60); // 枠線色を元に戻す
      panelDragAndDropArea.Invalidate(); // 再描画

    }

    private void panelDragAndDropArea_DragEnter(object sender, DragEventArgs e)
    {
      // ドラッグ＆ドロップできるファイル形式を指定する場合は、以下を利用する
      // e.g. ここの例では「ファイル」のみを指定している
      var arrayFiles = (string[])e.Data?.GetData(DataFormats.FileDrop, false)!;
      if (arrayFiles == null) return;

      var csvFiles = arrayFiles.Where(filePath => Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase)).ToList();

      if (csvFiles.Count > 0)
      { // 投げ込まれたファイルすべてがCSVファイルなら受け付ける
        e.Effect = DragDropEffects.Copy;
        panelDragAndDropArea.BackColor = Color.Honeydew; // 薄緑色
        borderColor = dragOverBorderColor; // 枠線色を変更
        panelDragAndDropArea.Invalidate(); // 再描画
      }
      else
      {
        e.Effect = DragDropEffects.None;
      }
    }

    private void panelDragAndDropArea_DragLeave(object sender, EventArgs e)
    {
      //panelDragAndDropArea.BackColor = SystemColors.Control; // 元のフォーム色に戻す
      borderColor = Color.FromArgb(200, 200, 200);
      panelDragAndDropArea.BackColor = Color.Transparent;

    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
      // 選択されているタブのインデックスを取得
      var selectedIndex = tabControl1.SelectedIndex;
      // 選択されているタブの名前を取得
      //var selectedTabName = tabControl1.TabPages[selectedIndex].Name;

      if (selectedIndex == 0)
      {
        // 形態素解析ボタン
        buttonMorphologicalAnalysis.Enabled = true;
        // ライム検索ボタン
        buttonRhymingSearch.Enabled = true;

        // 入力ボックスにフォーカス
        textBoxInput.Focus();
        // 文字列を選択状態にする
        textBoxInput.SelectAll();

      }
      else
      {
        // 形態素解析ボタン
        buttonMorphologicalAnalysis.Enabled = false;
        // ライム検索ボタン
        buttonRhymingSearch.Enabled = false;

      }



    }

    private void numericUpDownMatchThreshold_ValueChanged(object sender, EventArgs e)
    {
      // 一致率（％）
      Properties.Settings.Default.MatchThreshold = (int)numericUpDownMatchThreshold.Value;

      Properties.Settings.Default.Save();

    }

    private void numericUpDownMaxVowelWords_ValueChanged(object sender, EventArgs e)
    {
      // 母音の検索結果件数の最大値
      Properties.Settings.Default.MaxVowelWords = (int)numericUpDownMaxVowelWords.Value;
      Properties.Settings.Default.Save();

      textBoxTotalMatchWords.Text = (numericUpDownMaxVowelWords.Value + numericUpDownMaxPronunciationWords.Value).ToString(CultureInfo.CurrentCulture);

    }

    private void numericUpDownMaxPronunciationWords_ValueChanged(object sender, EventArgs e)
    {
      // 発音の検索結果件数の最大値
      Properties.Settings.Default.MaxPronunciationWords = (int)numericUpDownMaxPronunciationWords.Value;
      Properties.Settings.Default.Save();

      textBoxTotalMatchWords.Text = (numericUpDownMaxVowelWords.Value + numericUpDownMaxPronunciationWords.Value).ToString(CultureInfo.CurrentCulture);

    }

  }

}
