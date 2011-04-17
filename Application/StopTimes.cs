using System;
using System.Collections.Generic;
using System.Text;

namespace StopWatch
{
  public class StopTimes
  {
    private static int HOURS_IN_DAY = 24;
    private static int MINUTES_IN_HOUR = 60;

    private List<StopTime>[] mStopTimes = new List<StopTime>[HOURS_IN_DAY];
    private StopTime mLatestAddition = null;
    private List<string> mExcludedBuses = new List<string>();

    public int Count
    {
      get
      {
        int count = 0;
        foreach(List<StopTime> hourStops in mStopTimes)
        {
          count += hourStops.Count;
        }
        return count;
      }
    }

    public StopTimes()
    {
      for (int i = 0; i < mStopTimes.Length; i++)
      {
        mStopTimes[i] = new List<StopTime>();
      }
    }

    public void Add(int hour, int minute, string bus)
    {
      if (hour < 0 || hour >= HOURS_IN_DAY)
      {
        throw new ArgumentOutOfRangeException("Invalid hour argument " + hour);
      }
      if (minute < 0 || minute >= MINUTES_IN_HOUR)
      {
        throw new ArgumentOutOfRangeException("Invalid minute argument " + minute);
      }
      if (bus == null)
      {
        throw new ArgumentNullException("Null bus argument not permitted");
      }

      mLatestAddition = new StopTime(hour, minute, bus);
      mStopTimes[hour].Add(mLatestAddition);
    }

    public StopTime GetLatestAddition()
    {
      return mLatestAddition;
    }

    private bool IsExcluded(StopTime stopTime)
    {
      return mExcludedBuses.Contains(stopTime.Bus);
    }

    private int AddIncluded(List<StopTime> destination, List<StopTime> source,
                            int startIndex, int count)
    {
      int addCount = 0;
      for (int i = startIndex; i < source.Count && addCount < count; i++)
      {
        if (!IsExcluded(source[i]))
        {
          destination.Add(source[i]);
          addCount++;
        }
      }
      return addCount;
    }

    private int AddNextStopTimes(List<StopTime> destination, List<StopTime> source,
                                 int minutes, int count)
    {
      int addCount = 0;

      int index = 0;
      while (index < source.Count && (minutes > source[index].Minute ||
                                      IsExcluded(source[index])))
      {
        index++;
      }
      if (index < source.Count)
      {
        addCount = AddIncluded(destination, source, index, count);
      }

      return addCount;
    }

    private int AddNextStopTimes(List<StopTime> destination, List<StopTime>[] source,
                                 int hour, int count)
    {
      int remainingCount = count;

      int endIndex = hour;
      for (hour = (hour + 1) % source.Length;
           remainingCount > 0 && hour != endIndex;
           hour = (hour + 1) % source.Length)
      {
        remainingCount -= AddIncluded(destination, source[hour], 0, remainingCount);
      }

      return count - remainingCount;
    }

    public List<StopTime> GetNextStops(TimeSpan time, int count)
    {
      List<StopTime> stops = new List<StopTime>(count);

      if (time.Seconds > 0)
      {
        time = time.Add(new TimeSpan(0, 1, 0));
      }
      if (AddNextStopTimes(stops, mStopTimes[time.Hours], time.Minutes, count) < count)
      {
        AddNextStopTimes(stops, mStopTimes, time.Hours, count - stops.Count);
      }

      return stops;
    }

    public void ExcludeBus(string bus)
    {
      if (!mExcludedBuses.Contains(bus))
      {
        mExcludedBuses.Add(bus);
      }
    }

    public void IncludeBus(string bus)
    {
      if (bus != null)
      {
        mExcludedBuses.Remove(bus);
      }
      else
      {
        mExcludedBuses.Clear();
      }
    }

    public override string ToString()
    {
      StringBuilder builder = new StringBuilder();

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
  }
}
