using System;
using System.Collections.Generic;
using System.Text;

namespace StopWatch
{
  public class StopTimeDifference : IComparable
  {
    private StopTime mStopTime;
    public int Hour { get { return mStopTime.Hour; } }
    public int Minute { get { return mStopTime.Minute; } }
    public string Bus { get { return mStopTime.Bus; } }

    private int mDayDifference;
    public int DayDifference { get { return mDayDifference; } }

    public StopTimeDifference(StopTime stopTime, int dayDifference)
    {
      mStopTime = stopTime;
      mDayDifference = dayDifference;
    }

    public TimeSpan GetDifference(DateTime date)
    {
      return new TimeSpan(DayDifference, Hour, Minute, 0)
               .Subtract(new TimeSpan(date.Hour, date.Minute, date.Second));
    }

    public string GetDifference(DateTime date, bool showSeconds)
    {
      StringBuilder builder = new StringBuilder();
      TimeSpan diff = GetDifference(date);
      builder.Append(String.Format("{0,3} min",
                                   Timetable.HOURS_IN_DAY * Timetable.MINUTES_IN_HOUR * diff.Days +
                                   Timetable.MINUTES_IN_HOUR * diff.Hours +
                                   diff.Minutes));
      if (showSeconds)
      {
        builder.Append(String.Format(" {0:0#} s", diff.Seconds));
      }
      return builder.ToString();
    }

    public override string ToString()
    {
      return mStopTime.ToString();
    }

    #region IComparable Members

    public int CompareTo(object obj)
    {
      int result;

      int dayDifference;
      int hour;
      int minute;
      if (obj is StopTimeDifference)
      {
        StopTimeDifference other = (StopTimeDifference)obj;
        dayDifference = other.DayDifference;
        hour = other.Hour;
        minute = other.Minute;
      }
      else if (obj is DateTime)
      {
        DateTime other = (DateTime)obj;
        dayDifference = 0;
        hour = other.Hour;
        minute = other.Minute;
      }
      else
      {
        throw new ArgumentException(GetType() + " is not comparable to " + obj.GetType());
      }

      result = DayDifference - dayDifference;
      if (result == 0)
      {
        result = Hour - hour;
      }
      if (result == 0)
      {
        result = Minute - minute;
      }

      return result;
    }

    #endregion
  }
}
