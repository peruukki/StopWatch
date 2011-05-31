using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StopWatch.Properties;
using System.IO;

namespace StopWatch
{
  public partial class MainWindow : Form
  {
    private StopTimes mStopTimes;

    private List<Label[]> mStopTimesView;
    private Timer mTimer;

    private int mStopTimeCount = Settings.Default.StopTimeCount;
    private int mStopTimeDelayMin = Settings.Default.StopTimeDelay;
    private int mLabelHeight;
    private int mLastHeight;

    public static StopTimes ParseTimetableFile(string fileName)
    {
      StopTimes stopTimes = null;

      try
      {
        stopTimes = new StopTimeParser().Parse(fileName);
      }
      catch (FileNotFoundException)
      {
        MessageBox.Show(String.Format("The timetable file '{0}' doesn't exist.",
                                      fileName),
                        "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      if (stopTimes != null && stopTimes.Count == 0)
      {
        MessageBox.Show(String.Format("Couldn't find timetable information from file '{0}'.",
                                      fileName),
                        "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Error);
        stopTimes = null;
      }

      return stopTimes;
    }

    public MainWindow(StopTimes stopTimes)
    {
      InitializeComponent();
      SetStopTimes(stopTimes);
      InitializeContent();
      mStopDelayChooser.Value = mStopTimeDelayMin;
    }

    private void InitializeContent()
    {
      InitializeStopTimesView();
      InitializeBusView();
    }

    private void InitializeStopTimesView()
    {
      CreateStopTimesView();
      if (mStopTimesView.Count > 0)
      {
        mLabelHeight = mStopTimesView[0][0].Height + 1;
      }
      else
      {
        mLabelHeight = mTimeNowLabel.Height + 1;
      }
    }

    private void InitializeTimer()
    {
      mTimer = new Timer();
      mTimer.Tick += new EventHandler(mTimer_Tick);
      mTimer.Interval = 1000;
      mTimer.Start();
    }

    private void MainWindow_Load(object sender, EventArgs e)
    {
      UpdateView();
      mTimeNowLabel.Focus();
      mTimeNowLabel.Update();
      UpdateHeight();
      MinimumSize = new Size(Width, MinimumSize.Height);

      InitializeTimer();
      ResizeEnd += new System.EventHandler(this.MainWindow_Resize);
    }

    private void MainWindow_Resize(object sender, EventArgs e)
    {
      int rowDiff = (ClientSize.Height - mLastHeight) / mLabelHeight;
      int stopTimeCount = mStopTimeCount + rowDiff;
      if (stopTimeCount < 1)
      {
        stopTimeCount = 1;
      }

      if (stopTimeCount != mStopTimeCount)
      {
        mStopTimeCount = stopTimeCount;
        CreateStopTimesView();
        UpdateView();
      }
      UpdateHeight();
    }

    private void InitializeBusView()
    {
      mBusPanel.Controls.Clear();
      foreach (string bus in mStopTimes.Buses)
      {
        CheckBox busButton = new CheckBox();
        busButton.AutoSize = true;
        busButton.Text = bus;
        busButton.Checked = mStopTimes.IsIncluded(bus);
        busButton.CheckedChanged += new EventHandler(busButton_CheckedChanged);

        mBusPanel.Controls.Add(busButton);
      }
    }

    private void busButton_CheckedChanged(object sender, EventArgs e)
    {
      CheckBox button = sender as CheckBox;
      mStopTimes.SetIncluded(button.Text, button.Checked);
      CreateStopTimesView();
      UpdateView();
    }

    private void CreateStopTimesView()
    {
      mStopTimesPanel.Controls.Clear();
      mStopTimesView = new List<Label[]>(mStopTimeCount);
      for (int i = 0; i < mStopTimeCount; ++i)
      {
        Label[] stopTimeLabels = new Label[2];

        for (int j = 0; j < stopTimeLabels.Length; j++)
        {
          stopTimeLabels[j] = new Label();
          stopTimeLabels[j].AutoSize = true;
        }

        mStopTimesView.Add(stopTimeLabels);
        mStopTimesPanel.Controls.AddRange(stopTimeLabels);
      }
    }

    private void SetStopTimes(StopTimes stopTimes)
    {
      mStopTimes = stopTimes;
      mStopNameLabel.Text = stopTimes.StopName;
    }

    private void UpdateHeight()
    {
      int height = (mStopTimesPanel.Top + mStopTimesPanel.Padding.Vertical) +
                   (mStopTimeCount * mLabelHeight) + Padding.Bottom;
      ClientSize = new Size(ClientSize.Width, height);
      mLastHeight = height;
    }

    private void UpdateView()
    {
      DateTime nowTime = DateTime.Now;
      mTimeNowLabel.Text = String.Format("Time is {0}", nowTime.ToLongTimeString());

      List<StopTimeDifference> stops = mStopTimes.GetNextStops(nowTime, mStopTimeDelayMin,
                                                               mStopTimeCount);
      for (int i = 0; i < stops.Count && i < mStopTimesView.Count; ++i)
      {
        mStopTimesView[i][0].Text = String.Format("{0}", stops[i].ToString());
        mStopTimesView[i][1].Text = String.Format("{0}",
                                                  stops[i].GetDifference(nowTime, i == 0));
      }
    }

    private void mTimer_Tick(object sender, EventArgs e)
    {
      UpdateView();
    }

    private void mStopDelayChooser_ValueChanged(object sender, EventArgs e)
    {
      mStopTimeDelayMin = (int)(sender as NumericUpDown).Value;
    }

    private void SetTimetableFile(string fileName)
    {
      StopTimes stopTimes = ParseTimetableFile(fileName);
      if (stopTimes != null)
      {
        SetStopTimes(stopTimes);
        InitializeContent();
        UpdateView();
      }
    }

    private void mButtonOpenFile_Click(object sender, EventArgs e)
    {
      mOpenFileDialog.ShowDialog();
    }

    private void mOpenFileDialog_FileOk(object sender, CancelEventArgs e)
    {
      SetTimetableFile((sender as OpenFileDialog).FileName);
    }

    private void mButtonOpenWeb_Click(object sender, EventArgs e)
    {
      WebFileChooser chooser = new WebFileChooser();
      chooser.ShowDialog();
      if (!string.IsNullOrEmpty(chooser.WebFileContent))
      {
        string fileName = SaveWebFile(chooser.WebFileName, chooser.WebFileContent);
        SetTimetableFile(fileName);
      }
    }

    private string SaveWebFile(string fileName, string content)
    {
      string fileNameFullPath = Settings.Default.TimetableDir + fileName;
      using (TextWriter writer = new StreamWriter(fileNameFullPath))
      {
        writer.Write(content);
      }
      return fileNameFullPath;
    }
  }
}