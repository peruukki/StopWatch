using System;
using System.Collections.Generic;
using System.Text;

namespace StopWatch
{
  public class StopTime
  {
    private int mHour;
    public int Hour { get { return mHour; } }

    private int mMinute;
    public int Minute { get { return mMinute; } }

    private string mBus;
    public string Bus { get { return mBus; } }

    public StopTime(int hour, int minute, string bus)
    {
      mHour = hour;
      mMinute = minute;
      mBus = bus;
    }

    public string GetDifference(TimeSpan time, bool showSeconds)
    {
      int hourAddition = (time.Hours > mHour) ? 24 : 0;
      TimeSpan diff = new TimeSpan(mHour + hourAddition, mMinute, 0).Subtract(time);

      StringBuilder builder = new StringBuilder();
      builder.Append(String.Format("{0,3} min", 60 * diff.Hours + diff.Minutes));
      if (showSeconds)
      {
        builder.Append(String.Format(" {0:0#} s", diff.Seconds));
      }
      return builder.ToString();
    }

    public override string ToString()
    {
      return String.Format("{0:0#}:{1:0#} {2}", mHour, mMinute, mBus);
    }
  }
}
