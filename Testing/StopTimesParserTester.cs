using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using StopWatch;

namespace StopWatch
{
  [TestFixture]
  public class StopTimesParserTester
  {
    StopTimes times;

    [SetUp]
    public void Init()
    {
      times = new StopTimeParser().Parse(@"..\..\1310137.html");
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
      times = new StopTimeParser().Parse(@"StopWatch.exe");
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
      DateTime date = DateTime.Now;

      Console.WriteLine("Time is {0}", date);
      List<StopTime> stops = times.GetNextStops(date, 5);
      bool showSeconds = true;
      foreach (StopTime stop in stops)
      {
        Console.WriteLine("{0} {1}", stop, stop.GetDifference(date, showSeconds));
        showSeconds = false;
      }
    }
  }
}
