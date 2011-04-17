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
      Assert.That(stopTimes.GetLatestAddition(), Is.Null);
      Assert.That(stopTimes.Count, Is.EqualTo(0));
    }

    [Test]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AddInvalidHour()
    {
      stopTimes.Add(-1, 0, "A");
    }

    [Test]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AddInvalidMinute()
    {
      stopTimes.Add(0, -1, "A");
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AddInvalidBus()
    {
      stopTimes.Add(0, 0, null);
    }

    [Test]
    public void GetNextStopTimes()
    {
      TimeSpan time = new TimeSpan(0, 0, 0);
      stopTimes.Add(0, 0, "A");
      Assert.That(stopTimes.GetNextStops(time, 0).Count, Is.EqualTo(0));
      Assert.AreEqual(stopTimes.GetNextStops(time, 1).Count, 1);
      Assert.AreEqual(stopTimes.GetNextStops(time, 2).Count, 1);
      stopTimes.Add(2, 7, "B");
      stopTimes.Add(2, 7, "C");
      Assert.AreEqual(stopTimes.GetNextStops(time, 2).Count, 2);
      Assert.AreEqual(stopTimes.GetNextStops(time, 3).Count, 3);

      DateTime now = DateTime.Now;
      TimeSpan nowTime = new TimeSpan(now.Hour, now.Minute, now.Second);
      List<StopTime> stops = stopTimes.GetNextStops(nowTime, 1);
      Assert.That(stops.Count, Is.EqualTo(1));
      Console.WriteLine("Time is " + now + ", next stop " + stops[0]);
    }

    [Test]
    public void ExcludeBus()
    {
      TimeSpan time = new TimeSpan(0, 0, 0);

      stopTimes.Add(0, 1, "A");
      Assert.AreEqual(stopTimes.GetNextStops(time, 5).Count, 1);
      stopTimes.ExcludeBus("A");
      Assert.AreEqual(stopTimes.GetNextStops(time, 5).Count, 0);
      stopTimes.IncludeBus("A");
      Assert.AreEqual(stopTimes.GetNextStops(time, 5).Count, 1);

      stopTimes.Add(21, 36, "A");
      stopTimes.ExcludeBus("A");
      Assert.AreEqual(stopTimes.GetNextStops(time, 5).Count, 0);
      stopTimes.IncludeBus(null);
      Assert.AreEqual(stopTimes.GetNextStops(time, 5).Count, 2);

      stopTimes.Add(14, 59, "B");
      Assert.AreEqual(stopTimes.GetNextStops(time, 5).Count, 3);
      stopTimes.ExcludeBus("A");
      Assert.AreEqual(stopTimes.GetNextStops(time, 5).Count, 1);
      stopTimes.ExcludeBus("B");
      Assert.AreEqual(stopTimes.GetNextStops(time, 5).Count, 0);
      stopTimes.IncludeBus(null);
      Assert.AreEqual(stopTimes.GetNextStops(time, 5).Count, 3);
    }
  }
}
