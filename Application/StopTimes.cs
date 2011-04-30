using System;
using System.Collections.Generic;
using System.Text;

namespace StopWatch
{
  public class StopTimes
  {
    private Timetable[] mTimetables = new Timetable[Weekday.Count];
    private List<string> mExcludedBuses = new List<string>();

    public int Count
    {
      get
      {
        int count = 0;
        foreach(Timetable timetable in mTimetables)
        {
          count += timetable.Count;
        }
        return count;
      }
    }

    public StopTimes()
    {
      for (int i = 0; i < mTimetables.Length; i++)
      {
        mTimetables[i] = new Timetable(Weekday.FromOrdinal(i).ToString());
      }
    }

    public StopTime Add(Weekday weekDay, int hour, int minute, string bus)
    {
      return mTimetables[weekDay.Ordinal].Add(hour, minute, bus);
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

    private int AddNextStopTimes(List<StopTime> destination, Timetable source,
                                 int hour, int count)
    {
      int remainingCount = count;

      int endIndex = hour;
      for (hour = (hour + 1) % Timetable.HOURS_IN_DAY;
           remainingCount > 0 && hour != endIndex;
           hour = (hour + 1) % Timetable.HOURS_IN_DAY)
      {
        remainingCount -= AddIncluded(destination, source.Get(hour), 0, remainingCount);
      }

      return count - remainingCount;
    }

    public List<StopTime> GetNextStops(DateTime date, int count)
    {
      List<StopTime> stops = new List<StopTime>(count);

      TimeSpan spanToAdd = TimeSpan.FromMinutes(1);
      bool addSpan = date.Second > 0;
      if (addSpan)
      {
        date = date.Add(spanToAdd);
      }

      int index = Weekday.FromDayOfWeek(date.DayOfWeek).Ordinal;
      if (AddNextStopTimes(stops, mTimetables[index].Get(date.Hour),
                           date.Minute, count) < count)
      {
        AddNextStopTimes(stops, mTimetables[index], date.Hour, count - stops.Count);
      }

      if (addSpan)
      {
        date.Subtract(spanToAdd);
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
      foreach (Timetable timetable in mTimetables)
      {
        builder.Append(timetable.ToString());
      }
      return builder.ToString();
    }
  }
}
