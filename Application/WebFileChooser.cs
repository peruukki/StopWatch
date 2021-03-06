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

    private const int URL_LIST_CAPACITY = 5;
    private readonly LRUList mUrlList;

    public WebFileChooser()
    {
      InitializeComponent();

      if (Settings.Default.WebAutoComplete == null)
      {
        Settings.Default.WebAutoComplete = new StringCollection();
      }
      mUrlList = new LRUList(URL_LIST_CAPACITY, Settings.Default.WebAutoComplete);

      if (mUrlList.Items.Count > 0)
      {
        foreach (string url in mUrlList.Items)
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
            if (!mWebFileName.EndsWith(".html"))
            {
              throw new UriFormatException("The internet address must end with '.html'.");
            }
            mWebFileContent = DownloadWebPage(uri);

            mUrlList.Add(mAddressText.Text);
            Settings.Default.Save();

            Close();
          }
        }
        catch (UriFormatException e)
        {
          MessageBox.Show(e.Message, "Invalid internet address",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (WebException e)
        {
          MessageBox.Show(e.Message, "Web file open failed",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }

    private static string DownloadWebPage(Uri uri)
    {
      string pageContent = string.Empty;

      HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
      using (WebResponse response = request.GetResponse())
      {
        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
          pageContent = reader.ReadToEnd();
        }
      }

      return pageContent;
    }

    private void mButtonCancel_Click(object sender, EventArgs e)
    {
      mWebFileContent = string.Empty;
    }
  }
}