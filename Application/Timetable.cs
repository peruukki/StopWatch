using System;
using System.Collections.Generic;
using System.Text;

namespace StopWatch
{
  public class Timetable
  {
    public static readonly int DAYS_IN_WEEK = 7;
    public static readonly int HOURS_IN_DAY = 24;
    public static readonly int MINUTES_IN_HOUR = 60;
    public static readonly int FIRST_HOUR_OF_DAY = 5;

    private List<StopTime>[] mStopTimes = new List<StopTime>[HOURS_IN_DAY];
    private string mTitle;

    public int Count
    {
      get
      {
        int count = 0;
        foreach (List<StopTime> hourStops in mStopTimes)
        {
          count += hourStops.Count;
        }
        return count;
      }
    }

    public Timetable(string title)
    {
      for (int i = 0; i < mStopTimes.Length; i++)
      {
        mStopTimes[i] = new List<StopTime>();
      }
      mTitle = title;
    }

    public StopTime Add(int hour, int minute, string bus)
    {
      ValidateHour(hour);
      ValidateMinute(minute);
      ValidateBus(bus);

      StopTime stopTime = new StopTime(hour, minute, bus);
      mStopTimes[hour].Add(stopTime);
      return stopTime;
    }

    public List<StopTime> Get(int hour)
    {
      ValidateHour(hour);
      return mStopTimes[hour];
    }

    public override string ToString()
    {
      StringBuilder builder = new StringBuilder();

      builder.AppendLine("Timetable for " + mTitle + ":");
      foreach (List<StopTime> stopTimes in mStopTimes)
      {
        if (stopTimes.Count > 0)
        {
          foreach (StopTime stopTime in stopTimes)
          {
            builder.AppendFormat("{0} ", stopTime);
          }
          builder.AppendLine();
        }
      }

      return builder.ToString();
    }

    private static void ValidateHour(int hour)
    {
      if (hour < 0 || hour >= HOURS_IN_DAY)
      {
        throw new ArgumentOutOfRangeException("Invalid hour argument " + hour);
      }
    }

    private static void ValidateMinute(int minute)
    {
      if (minute < 0 || minute >= MINUTES_IN_HOUR)
      {
        throw new ArgumentOutOfRangeException("Invalid minute argument " + minute);
      }
    }

    private static void ValidateBus(string bus)
    {
      if (bus == null)
      {
        throw new ArgumentNullException("Null bus argument not permitted");
      }
    }
  }
}
