using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using StopWatch;
using StopWatchTester.Properties;

namespace StopWatch
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
    public void GetDifference()
    {
      DateTime date = new DateTime(2011, 4, 29, 5, 25, 12);

      Console.WriteLine("Time is {0}", date);
      List<StopTime> stops = StopTimesTester.GetNextStops(times, date, 5, 0);
      bool showSeconds = true;
      foreach (StopTime stop in stops)
      {
        Console.WriteLine("{0} {1}", stop, stop.GetDifference(date, showSeconds));
        showSeconds = false;
        Assert.That(stop.CompareTo(date), Is.GreaterThan(0),
                    "Stop time > current time");
      }
    }

    [Test]
    public void GetDifferenceNow()
    {
      DateTime date = DateTime.Now;

      Console.WriteLine("Time is {0}", date);
      List<StopTime> stops = StopTimesTester.GetNextStops(times, date, 5, 1);
      bool showSeconds = true;
      foreach (StopTime stop in stops)
      {
        Console.WriteLine("{0} {1}", stop, stop.GetDifference(date, showSeconds));
        showSeconds = false;
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
