using System;
using System.Collections.Generic;
using System.Text;

namespace StopWatch
{
  public class StopTimes
  {
    private Timetable[] mTimetables = new Timetable[Weekday.Count];
    private List<string> mBuses = new List<string>();
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

    public string[] Buses
    {
      get
      {
        return mBuses.ToArray();
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
      if (!mBuses.Contains(bus))
      {
        mBuses.Add(bus);
      }
      return mTimetables[weekDay.Ordinal].Add(hour, minute, bus);
    }

    public bool IsIncluded(string bus)
    {
      return !mExcludedBuses.Contains(bus);
    }

    public void SetIncluded(string bus, bool isIncluded)
    {
      if (isIncluded)
      {
        IncludeBus(bus);
      }
      else
      {
        ExcludeBus(bus);
      }
    }

    private bool IsExcluded(StopTime stopTime)
    {
      return !IsIncluded(stopTime.Bus);
    }

    private int AddIncluded(List<StopTimeDifference> destination, List<StopTime> source,
                            int startIndex, int count, int dayDifference)
    {
      int addCount = 0;
      for (int i = startIndex; i < source.Count && addCount < count; i++)
      {
        if (!IsExcluded(source[i]))
        {
          destination.Add(new StopTimeDifference(source[i], dayDifference));
          addCount++;
        }
      }
      return addCount;
    }

    private int AddNextStopTimes(List<StopTimeDifference> destination,
                                 List<StopTime> source, int minutes, int count)
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
        addCount = AddIncluded(destination, source, index, count, 0);
      }

      return addCount;
    }

    private int AddNextStopTimes(List<StopTimeDifference> destination, DateTime date,
                                 int count)
    {
      int remainingCount = count;
      int dayDifference = 0;
      bool keepAdding = true;

      while (keepAdding)
      {
        int startCount = remainingCount;
        for (int i = 0; (remainingCount > 0) && (i < Timetable.DAYS_IN_WEEK); i++)
        {
          int hour = (date.Hour + 1) % Timetable.HOURS_IN_DAY;
          for (int j = 0; (remainingCount > 0) && (j < Timetable.HOURS_IN_DAY); j++)
          {
            if (hour == Timetable.FIRST_HOUR_OF_DAY)
            {
              date = date.AddDays(1);
            }
            if (hour == 0)
            {
              dayDifference++;
            }

            Timetable table = mTimetables[Weekday.FromDayOfWeek(date.DayOfWeek).Ordinal];
            remainingCount -= AddIncluded(destination, table.Get(hour), 0, remainingCount,
                                          dayDifference);

            hour = (hour + 1) % Timetable.HOURS_IN_DAY;
          }
        }
        keepAdding = (remainingCount < startCount);
      }

      return count - remainingCount;
    }

    public List<StopTimeDifference> GetNextStops(DateTime date, int count)
    {
      List<StopTimeDifference> stops = new List<StopTimeDifference>(count);

      if (date.Second > 0)
      {
        date = date.Add(TimeSpan.FromMinutes(1));
      }
      int weekdayIndex = Weekday.FromDayOfWeek(date.DayOfWeek).Ordinal;
      if (AddNextStopTimes(stops, mTimetables[weekdayIndex].Get(date.Hour),
                           date.Minute, count) < count)
      {
        AddNextStopTimes(stops, date, count - stops.Count);
      }

      return stops;
    }

    public void ExcludeBus(string bus)
    {
      if (!mBuses.Contains(bus))
      {
        throw new ArgumentOutOfRangeException("bus", "Given bus '" + bus +
                                              "' has no stops in the timetable");
      }

      if (!mExcludedBuses.Contains(bus))
      {
        mExcludedBuses.Add(bus);
      }
    }

    public void IncludeBus(string bus)
    {
      if (bus != null)
      {
        if (!mBuses.Contains(bus))
        {
          throw new ArgumentOutOfRangeException("bus", "Given bus '" + bus +
                                                "' has no stops in the timetable");
        }

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
