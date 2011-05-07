using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace StopWatch
{
  [TestFixture]
  public class StopTimesTester
  {
    StopTimes stopTimes;

    public static List<StopTimeDifference> GetNextStops(StopTimes stopTimes, DateTime date,
                                                        int stopCount)
    {
      List<StopTimeDifference> stops = stopTimes.GetNextStops(date, stopCount);
      ValidateNextStops(stops, date);
      return stops;
    }

    public static void ValidateNextStops(List<StopTimeDifference> stops, DateTime date)
    {
      for (int i = 0; i < stops.Count - 1; i++)
      {
        Assert.That(stops[i].CompareTo(stops[i + 1]), Is.LessThanOrEqualTo(0));
        Assert.That(stops[i].GetDifference(date),
                    Is.LessThanOrEqualTo(stops[i + 1].GetDifference(date)));
      }
      if (stops.Count > 0)
      {
        Assert.That(stops[0].GetDifference(date),
                    Is.GreaterThanOrEqualTo(new TimeSpan()));
      }
    }

    [SetUp]
    public void Init()
    {
      stopTimes = new StopTimes();
      Assert.That(stopTimes.Count, Is.EqualTo(0));
    }

    [Test]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AddInvalidHour()
    {
      stopTimes.Add(Weekday.Weekdays, -1, 0, "A");
    }

    [Test]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AddInvalidMinute()
    {
      stopTimes.Add(Weekday.Saturday, 0, -1, "A");
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AddInvalidBus()
    {
      stopTimes.Add(Weekday.Sunday, 0, 0, null);
    }

    [Test]
    public void GetNextStopTimes()
    {
      DateTime date = DateTime.Now;
      Weekday weekDay = Weekday.FromDayOfWeek(date.DayOfWeek);

      stopTimes.Add(weekDay, 0, 0, "A");
      Assert.That(GetNextStops(stopTimes, date, 0).Count, Is.EqualTo(0));
      Assert.That(GetNextStops(stopTimes, date, 1).Count, Is.EqualTo(1));
      Assert.That(GetNextStops(stopTimes, date, 2).Count, Is.EqualTo(2));
      stopTimes.Add(weekDay, 2, 7, "B");
      stopTimes.Add(weekDay, 2, 7, "C");
      Assert.That(GetNextStops(stopTimes, date, 2).Count, Is.EqualTo(2));
      Assert.That(GetNextStops(stopTimes, date, 3).Count, Is.EqualTo(3));

      List<StopTimeDifference> stops = GetNextStops(stopTimes, date, 1);
      Assert.That(stops.Count, Is.EqualTo(1));
      Console.WriteLine("Time is " + date + ", next stop " + stops[0]);
    }

    [Test]
    public void ExcludeBus()
    {
      DateTime date = DateTime.Now;
      Weekday weekDay = Weekday.FromDayOfWeek(date.DayOfWeek);
      int stopCount = 5;

      stopTimes.Add(weekDay, 0, 1, "A");
      Assert.That(GetNextStops(stopTimes, date, stopCount).Count, Is.EqualTo(stopCount));
      stopTimes.ExcludeBus("A");
      Assert.That(GetNextStops(stopTimes, date, stopCount).Count, Is.EqualTo(0));
      stopTimes.IncludeBus("A");
      Assert.That(GetNextStops(stopTimes, date, stopCount).Count, Is.EqualTo(stopCount));

      stopTimes.Add(weekDay, 21, 36, "A");
      stopTimes.ExcludeBus("A");
      Assert.That(GetNextStops(stopTimes, date, stopCount).Count, Is.EqualTo(0));
      stopTimes.IncludeBus(null);
      Assert.That(GetNextStops(stopTimes, date, stopCount).Count, Is.EqualTo(stopCount));

      stopTimes.Add(weekDay, 14, 59, "B");
      Assert.That(GetNextStops(stopTimes, date, stopCount).Count, Is.EqualTo(stopCount));
      stopTimes.ExcludeBus("A");
      Assert.That(GetNextStops(stopTimes, date, stopCount).Count, Is.EqualTo(stopCount));
      stopTimes.ExcludeBus("B");
      Assert.That(GetNextStops(stopTimes, date, stopCount).Count, Is.EqualTo(0));
      stopTimes.IncludeBus(null);
      Assert.That(GetNextStops(stopTimes, date, stopCount).Count, Is.EqualTo(stopCount));
    }

    [Test]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ExcludeInvalidBus()
    {
      stopTimes.ExcludeBus("A");
    }

    [Test]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void IncludeInvalidBus()
    {
      stopTimes.IncludeBus("A");
    }

    [Test]
    public void Buses()
    {
      DateTime date = DateTime.Now;
      Weekday weekDay = Weekday.FromDayOfWeek(date.DayOfWeek);

      Assert.That(stopTimes.Buses.Length, Is.EqualTo(0));

      stopTimes.Add(weekDay, 0, 1, "A");
      Assert.That(stopTimes.Buses.Length, Is.EqualTo(1));
      stopTimes.Add(weekDay, 21, 36, "A");
      Assert.That(stopTimes.Buses.Length, Is.EqualTo(1));

      stopTimes.Add(weekDay, 14, 59, "B");
      Assert.That(stopTimes.Buses.Length, Is.EqualTo(2));

      stopTimes.ExcludeBus("A");
      Assert.That(stopTimes.Buses.Length, Is.EqualTo(2));
    }

    [Test]
    public void ChangingDays()
    {
      // Add stop from each weekday
      for (int i = 0; i < Weekday.Count; i++)
      {
        StopTime stop = stopTimes.Add(Weekday.FromOrdinal(i), i, i, "A");
        Console.WriteLine("Added stop " + stop);
      }

      // Verify that stops from successive weekdays are returned
      DateTime date = DateTime.Now;
      int dayCount = 10;
      Console.WriteLine("Time is " + date);
      List<StopTimeDifference> stops = GetNextStops(stopTimes, date, dayCount);
      Assert.That(stops.Count, Is.EqualTo(dayCount));
      foreach (StopTimeDifference stop in stops)
      {
        Console.WriteLine("Retrieved stop " + stop + " " + stop.GetDifference(date, false));
        int weekdayOrdinal = Weekday.FromDayOfWeek(date.DayOfWeek).Ordinal;
        Assert.That(stop.Hour, Is.EqualTo(weekdayOrdinal));
        Assert.That(stop.Minute, Is.EqualTo(weekdayOrdinal));
        date = date.AddDays(1);
      }
    }
  }
}
