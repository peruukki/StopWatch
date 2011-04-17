using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StopWatch
{
  public partial class MainWindow : Form
  {
    private const int STOP_TIME_DEFAULT_COUNT = 5;
    private const int STOP_TIME_DEFAULT_DELAY_MIN = 5;
    private const string TIMETABLE_DIR = @"..\..\";

    private StopTimes mStopTimes;

    private Label mTimeNowLabel;
    private List<Label[]> mStopTimesView;
    private Panel mStopTimesPanel;
    private Timer mTimer;

    private string mTimetableFile = TIMETABLE_DIR + "1310137.html";
    //private string mTimetableFile = TIMETABLE_DIR + "1434180.html";
    
    private int mStopTimeCount = STOP_TIME_DEFAULT_COUNT;
    private int mStopTimeDelayMin = STOP_TIME_DEFAULT_DELAY_MIN;
    private int mLabelHeight;
    private int mLastHeight;

    public MainWindow()
    {
      InitializeComponent();
      InitializeTimeNowLabel();
      InitializeStopTimesView();
      InitializeTimer();
    }

    private void InitializeTimeNowLabel()
    {
      mTimeNowLabel = new Label();
      mTimeNowLabel.AutoSize = true;
      Controls.Add(mTimeNowLabel);
    }

    private void InitializeStopTimesView()
    {
      TableLayoutPanel stopTimesPanel = new TableLayoutPanel();
      stopTimesPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top;
      stopTimesPanel.Location = new Point(0, 20);
      stopTimesPanel.AutoSize = true;
      stopTimesPanel.ColumnCount = 2;
      Controls.Add(stopTimesPanel);
      mStopTimesPanel = stopTimesPanel;

      CreateStopTimesView();

      mLabelHeight = mStopTimesView[0][0].Height + 1;
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
      mStopTimes = new StopTimeParser().Parse(mTimetableFile);
      UpdateView();
      mTimeNowLabel.Focus();
      mTimeNowLabel.Update();
      UpdateHeight();

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

      TimeSpan nowSpan = new TimeSpan(nowTime.Hour, nowTime.Minute, nowTime.Second);
      TimeSpan nextStopsSpan = nowSpan.Add(new TimeSpan(0, mStopTimeDelayMin, 0));
      List<StopTime> stops = mStopTimes.GetNextStops(nextStopsSpan, mStopTimeCount);
      for (int i = 0; i < stops.Count && i < mStopTimesView.Count; ++i)
      {
        mStopTimesView[i][0].Text = String.Format("{0}", stops[i].ToString());
        mStopTimesView[i][1].Text = String.Format("{0}",
                                                  stops[i].GetDifference(nowSpan, i == 0));
      }
    }

    private void mTimer_Tick(object sender, EventArgs e)
    {
      UpdateView();
    }
  }
}