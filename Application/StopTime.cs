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
        StopTime other = obj as StopTime;
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
        throw new ArgumentException(GetType() + " is not comparable to " + obj.GetType());
      }

      if (Hour != hour)
      {
        result = Hour - hour;
      }
      else
      {
        result = Minute - minute;
      }

      return result;
    }

    #endregion
  }
}
