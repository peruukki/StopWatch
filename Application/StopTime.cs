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

    public override string ToString()
    {
      return String.Format("{0:0#}:{1:0#} {2}", mHour, mMinute, mBus);
    }
  }
}
