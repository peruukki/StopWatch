using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using StopWatch.Properties;
using System.Collections.Specialized;

namespace StopWatch
{
  public partial class WebFileChooser : Form
  {
    private string mWebFileContent = string.Empty;
    public string WebFileContent
    {
      get { return mWebFileContent; }
    }

    private string mWebFileName = string.Empty;
    public string WebFileName
    {
      get { return mWebFileName; }
    }

    public WebFileChooser()
    {
      InitializeComponent();

      if (Settings.Default.WebAutoComplete == null)
      {
        Settings.Default.WebAutoComplete = new StringCollection();
      }

      if (Settings.Default.WebAutoComplete.Count > 0)
      {
        foreach (string url in Settings.Default.WebAutoComplete)
        {
          mAddressText.AutoCompleteCustomSource.Add(url);
        }
        mAddressText.Text = mAddressText.AutoCompleteCustomSource[0];
      }
    }

    private void mButtonOpen_Click(object sender, EventArgs args)
    {
      if (!string.IsNullOrEmpty(mAddressText.Text))
      {
        try
        {
          Uri uri = new Uri(mAddressText.Text, UriKind.Absolute);
          if (uri.Segments.Length > 0)
          {
            mWebFileName = uri.Segments[uri.Segments.Length - 1];
            mWebFileContent = DownloadWebPage(uri);

            Settings.Default.WebAutoComplete.Add(mAddressText.Text);
            Settings.Default.Save();

            Close();
          }
        }
        catch (UriFormatException e)
        {
          MessageBox.Show(e.Message, "Invalid internet address",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }

    private static string DownloadWebPage(Uri uri)
    {
      string pageContent = string.Empty;

      HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
      try
      {
        using (WebResponse response = request.GetResponse())
        {
          using (StreamReader reader = new StreamReader(response.GetResponseStream()))
          {
            pageContent = reader.ReadToEnd();
          }
        }
      }
      catch (WebException e)
      {
        MessageBox.Show(e.Message, "Web file open failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      return pageContent;
    }

    private void mButtonCancel_Click(object sender, EventArgs e)
    {
      mWebFileContent = string.Empty;
    }
  }
}