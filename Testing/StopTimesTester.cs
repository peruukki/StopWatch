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
      Assert.That(stopTimes.GetNextStops(date, 0).Count, Is.EqualTo(0));
      Assert.AreEqual(stopTimes.GetNextStops(date, 1).Count, 1);
      Assert.AreEqual(stopTimes.GetNextStops(date, 2).Count, 1);
      stopTimes.Add(weekDay, 2, 7, "B");
      stopTimes.Add(weekDay, 2, 7, "C");
      Assert.AreEqual(stopTimes.GetNextStops(date, 2).Count, 2);
      Assert.AreEqual(stopTimes.GetNextStops(date, 3).Count, 3);

      List<StopTime> stops = stopTimes.GetNextStops(date, 1);
      Assert.That(stops.Count, Is.EqualTo(1));
      Console.WriteLine("Time is " + date + ", next stop " + stops[0]);
    }

    [Test]
    public void ExcludeBus()
    {
      DateTime date = DateTime.Now;
      Weekday weekDay = Weekday.FromDayOfWeek(date.DayOfWeek);

      stopTimes.Add(weekDay, 0, 1, "A");
      Assert.AreEqual(stopTimes.GetNextStops(date, 5).Count, 1);
      stopTimes.ExcludeBus("A");
      Assert.AreEqual(stopTimes.GetNextStops(date, 5).Count, 0);
      stopTimes.IncludeBus("A");
      Assert.AreEqual(stopTimes.GetNextStops(date, 5).Count, 1);

      stopTimes.Add(weekDay, 21, 36, "A");
      stopTimes.ExcludeBus("A");
      Assert.AreEqual(stopTimes.GetNextStops(date, 5).Count, 0);
      stopTimes.IncludeBus(null);
      Assert.AreEqual(stopTimes.GetNextStops(date, 5).Count, 2);

      stopTimes.Add(weekDay, 14, 59, "B");
      Assert.AreEqual(stopTimes.GetNextStops(date, 5).Count, 3);
      stopTimes.ExcludeBus("A");
      Assert.AreEqual(stopTimes.GetNextStops(date, 5).Count, 1);
      stopTimes.ExcludeBus("B");
      Assert.AreEqual(stopTimes.GetNextStops(date, 5).Count, 0);
      stopTimes.IncludeBus(null);
      Assert.AreEqual(stopTimes.GetNextStops(date, 5).Count, 3);
    }
  }
}
