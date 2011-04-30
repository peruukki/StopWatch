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

    public static List<StopTime> GetNextStops(StopTimes stopTimes, DateTime date,
                                              int stopCount, int maxDayChanges)
    {
      List<StopTime> stops = stopTimes.GetNextStops(date, stopCount);
      Assert.That(ValidateNextStops(stops, maxDayChanges), Is.True);
      return stops;
    }

    public static bool ValidateNextStops(List<StopTime> stops, int maxDayChanges)
    {
      int dayChangeCount = 0;
      for (int i = 0; i < stops.Count - 1; i++)
      {
        if (stops[i].CompareTo(stops[i + 1]) > 0)
        {
          dayChangeCount++;
        }
      }
      return dayChangeCount <= maxDayChanges;
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
      Assert.That(GetNextStops(stopTimes, date, 0, 1).Count, Is.EqualTo(0));
      Assert.That(GetNextStops(stopTimes, date, 1, 1).Count, Is.EqualTo(1));
      Assert.That(GetNextStops(stopTimes, date, 2, 1).Count, Is.EqualTo(1));
      stopTimes.Add(weekDay, 2, 7, "B");
      stopTimes.Add(weekDay, 2, 7, "C");
      Assert.That(GetNextStops(stopTimes, date, 2, 1).Count, Is.EqualTo(2));
      Assert.That(GetNextStops(stopTimes, date, 3, 1).Count, Is.EqualTo(3));

      List<StopTime> stops = GetNextStops(stopTimes, date, 1, 1);
      Assert.That(stops.Count, Is.EqualTo(1));
      Console.WriteLine("Time is " + date + ", next stop " + stops[0]);
    }

    [Test]
    public void ExcludeBus()
    {
      DateTime date = DateTime.Now;
      Weekday weekDay = Weekday.FromDayOfWeek(date.DayOfWeek);

      stopTimes.Add(weekDay, 0, 1, "A");
      Assert.That(GetNextStops(stopTimes, date, 5, 1).Count, Is.EqualTo(1));
      stopTimes.ExcludeBus("A");
      Assert.That(GetNextStops(stopTimes, date, 5, 0).Count, Is.EqualTo(0));
      stopTimes.IncludeBus("A");
      Assert.That(GetNextStops(stopTimes, date, 5, 1).Count, Is.EqualTo(1));

      stopTimes.Add(weekDay, 21, 36, "A");
      stopTimes.ExcludeBus("A");
      Assert.That(GetNextStops(stopTimes, date, 5, 0).Count, Is.EqualTo(0));
      stopTimes.IncludeBus(null);
      Assert.That(GetNextStops(stopTimes, date, 5, 1).Count, Is.EqualTo(2));

      stopTimes.Add(weekDay, 14, 59, "B");
      Assert.That(GetNextStops(stopTimes, date, 5, 1).Count, Is.EqualTo(3));
      stopTimes.ExcludeBus("A");
      Assert.That(GetNextStops(stopTimes, date, 5, 1).Count, Is.EqualTo(1));
      stopTimes.ExcludeBus("B");
      Assert.That(GetNextStops(stopTimes, date, 5, 0).Count, Is.EqualTo(0));
      stopTimes.IncludeBus(null);
      Assert.That(GetNextStops(stopTimes, date, 5, 1).Count, Is.EqualTo(3));
    }
  }
}
