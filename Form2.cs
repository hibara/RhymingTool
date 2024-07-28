using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RhymingTool
{
  public partial class Form2 : Form
  {
    public Form2()
    {
      InitializeComponent();
    }

    private void Form2_Load(object sender, EventArgs e)
    {
      // アプリケーションの製品名
      labelAppName.Text = ApplicationInfo.ProductName;
      // アプリケーションのバージョン
      labelVersion.Text = @"ver." + ApplicationInfo.Version;
      // アプリケーションの著作権
      labelCopyright.Text = ApplicationInfo.CopyrightHolder;
    }

    private void buttonOk_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void linkLabelHome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      linkLabelHome.LinkVisited = true;
      Process.Start(linkLabelHome.Text);
      this.Close();
    }

    private void linkLabelGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      linkLabelGitHub.LinkVisited = true;
      Process.Start(linkLabelGitHub.Text);
      this.Close();
    }
  }

  /// <summary>
  /// アセンブリ情報を取得する
  /// Get assembly information
  /// http://stackoverflow.com/questions/909555/how-can-i-get-the-assembly-file-version
  /// </summary>
  public static class ApplicationInfo
  {
    public static Version Version => Assembly.GetCallingAssembly().GetName().Version;

    public static string Title
    {
      get
      {
        var appName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
        var attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
        if (attributes.Length <= 0) return appName;
        var titleAttribute = (AssemblyTitleAttribute)attributes[0];
        return titleAttribute.Title.Length > 0 ? titleAttribute.Title : appName;
      }
    }

    public static string ProductName
    {
      get
      {
        var attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
        return attributes.Length == 0 ? "" : ((AssemblyProductAttribute)attributes[0]).Product;
      }
    }

    public static string Description
    {
      get
      {
        var attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
        return attributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute)attributes[0]).Description;
      }
    }

    public static string CopyrightHolder
    {
      get
      {
        var attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
        return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
      }
    }

    public static string CompanyName
    {
      get
      {
        var attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
        return attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute)attributes[0]).Company;
      }
    }

  }



}
