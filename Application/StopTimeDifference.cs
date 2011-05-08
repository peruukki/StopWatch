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

      if (obj is StopTimeDifference)
      {
        StopTimeDifference other = obj as StopTimeDifference;
        if (DayDifference != other.DayDifference)
        {
          result = DayDifference - other.DayDifference;
        }
        else
        {
          result = mStopTime.CompareTo(other.mStopTime);
        }
      }
      else if (obj is DateTime)
      {
        DateTime other = (DateTime)obj;
        if (DayDifference > 0)
        {
          result = DayDifference;
        }
        else
        {
          result = mStopTime.CompareTo(other);
        }
      }
      else
      {
        throw new ArgumentException(GetType() + " is not comparable to " + obj.GetType());
      }

      return result;
    }

    #endregion
  }
}
