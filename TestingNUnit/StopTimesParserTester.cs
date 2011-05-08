using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using StopWatch;
using StopWatchNUnitTester.Properties;

namespace StopWatchNUnitTester
{
  [TestFixture]
  public class StopTimesParserTester
  {
    StopTimes times;

    [SetUp]
    public void Init()
    {
      times = new StopTimeParser().Parse(Settings.Default.TimetableDir + "1310137.html");
    }

    [Test]
    [ExpectedException(typeof(System.IO.FileNotFoundException))]
    public void ParseNonExistent()
    {
      times = new StopTimeParser().Parse(@"Non-existent-file.html");
    }

    [Test]
    public void ParseInvalid()
    {
      times = new StopTimeParser().Parse(@"C#\StopWatch\StopWatch.sln");
      Assert.That(times.Count, Is.EqualTo(0));
    }

    [Test]
    public void ParseValid()
    {
      Assert.That(times.Count, Is.EqualTo(457));
      Console.WriteLine(times.Count + " stop times parsed:");
      Console.Write(times);
    }

    [Test]
    public void GetDifferenceLastHourOfDay()
    {
      GetDifferenceFrom(new DateTime(2011, 4, 29, 23, 25, 12), 0,
                        new TimeSpan(6, 14, 48), 1);
    }

    [Test]
    public void GetDifferenceLastMinuteOfDay()
    {
      GetDifferenceFrom(new DateTime(2011, 4, 29, 23, 59, 12), 0,
                        new TimeSpan(5, 40, 48), 1);
    }

    [Test]
    public void GetDifferenceNow()
    {
      GetDifferenceFrom(DateTime.Now, 0, TimeSpan.Zero, -1);
    }

    [Test]
    public void GetDifferenceWithDelays()
    {
      DateTime nowTime = DateTime.Now;
      GetDifferenceFrom(nowTime, 5, TimeSpan.Zero, -1);
      GetDifferenceFrom(nowTime, 84, TimeSpan.Zero, -1);
      GetDifferenceFrom(nowTime, 380, TimeSpan.Zero, -1);
      GetDifferenceFrom(nowTime, 1023, TimeSpan.Zero, -1);
    }

    private void GetDifferenceFrom(DateTime date, int stopTimeDelay,
                                   TimeSpan expectedDifference, int expectedDayDifference)
    {
      Console.WriteLine("Time is {0}, delay is {1} s", date, stopTimeDelay);
      List<StopTimeDifference> stops = StopTimesTester.GetNextStops(times, date,
                                                                    stopTimeDelay, 5);
      if (expectedDifference != TimeSpan.Zero)
      {
        Assert.That(stops[0].GetDifference(date), Is.EqualTo(expectedDifference));
      }

      bool showSeconds = true;
      foreach (StopTimeDifference stop in stops)
      {
        Console.WriteLine("{0} {1}", stop, stop.GetDifference(date, showSeconds));
        showSeconds = false;
        Assert.That(stop.CompareTo(date), Is.GreaterThan(0),
                    "Stop time > current time");
        if (expectedDayDifference != -1)
        {
          Assert.That(stop.DayDifference, Is.EqualTo(expectedDayDifference));
        }
      }
    }

    private void GetDifferenceWithDelay(DateTime nowTime, int stopTimeDelay)
    {
      Console.WriteLine("Time is {0}, stop time delay is {1}", nowTime.ToLongTimeString(),
                        stopTimeDelay);

      DateTime limitTime = nowTime.AddMinutes(stopTimeDelay);
      List<StopTimeDifference> stops = StopTimesTester.GetNextStops(times, limitTime, 3);
      for (int i = 0; i < stops.Count; ++i)
      {
        Console.WriteLine("{0} {1}", stops[i].ToString(),
                          stops[i].GetDifference(nowTime, i == 0));
        Assert.That(stops[0].GetDifference(nowTime),
                    Is.GreaterThanOrEqualTo(TimeSpan.Zero));
      }
    }

    [Test]
    public void Buses()
    {
      string[] buses = times.Buses;
      Console.Write("Buses:");
      foreach (string bus in buses)
      {
        Console.Write(" " + bus);
        Assert.That("65A".Equals(bus) || "66A".Equals(bus) || "501".Equals(bus) ||
                    "21V".Equals(bus), Is.True);
      }
      Console.WriteLine("");
      Assert.That(buses.Length, Is.EqualTo(4));
    }
  }
}
