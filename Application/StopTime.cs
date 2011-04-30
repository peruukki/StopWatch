using System;
using System.Collections.Generic;
using System.Text;

namespace StopWatch
{
  public class StopTime : IComparable
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

    public string GetDifference(DateTime date, bool showSeconds)
    {
      int hourAddition = (date.Hour > mHour) ? 24 : 0;
      TimeSpan diff = new TimeSpan(mHour + hourAddition, mMinute, 0)
                        .Subtract(new TimeSpan(date.Hour, date.Minute, date.Second));

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

    #region IComparable Members

    public int CompareTo(object obj)
    {
      int result;

      int hour;
      int minute;
      if (obj is StopTime)
      {
        StopTime other = (StopTime)obj;
        hour = other.Hour;
        minute = other.Minute;
      }
      else if (obj is DateTime)
      {
        DateTime other = (DateTime)obj;
        hour = other.Hour;
        minute = other.Minute;
      }
      else
      {
        throw new ArgumentException("StopTime is not comparable to " + obj.GetType());
      }

      if (mHour < hour)
      {
        result = -1;
      }
      else if (mHour > hour)
      {
        result = 1;
      }
      else if (mMinute < minute)
      {
        result = -1;
      }
      else if (mMinute > minute)
      {
        result = 1;
      }
      else
      {
        result = 0;
      }

      return result;
    }

    #endregion
  }
}
