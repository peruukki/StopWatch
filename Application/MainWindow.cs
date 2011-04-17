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
    private List<Label> mStopTimesView;
    private Timer mTimer;
    private TableLayoutPanel mLayout;

    private string mTimetableFile = TIMETABLE_DIR + "1310137.html";
    private int mStopTimeCount = STOP_TIME_DEFAULT_COUNT;
    private int mStopTimeDelayMin = STOP_TIME_DEFAULT_DELAY_MIN;

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
      mLayout.Controls.Add(mTimeNowLabel);
    }

    private void InitializeStopTimesView()
    {
      mStopTimesView = new List<Label>(mStopTimeCount);
      for (int i = 0; i < mStopTimeCount; ++i)
      {
        Label stopTimeLabel = new Label();
        stopTimeLabel.AutoSize = true;
        mStopTimesView.Add(stopTimeLabel);
        mLayout.Controls.Add(stopTimeLabel);
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
      mStopTimes = new StopTimeParser().Parse(mTimetableFile);
      UpdateView();
      mTimeNowLabel.Focus();
      mTimeNowLabel.Update();
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
        mStopTimesView[i].Text = String.Format("{0}  {1}",
                                               stops[i].ToString(),
                                               stops[i].GetDifference(nowSpan, i == 0));
      }
    }

    private void mTimer_Tick(object sender, EventArgs e)
    {
      UpdateView();
    }
  }
}