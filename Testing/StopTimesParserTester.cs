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
      StopTimeParser parser = new StopTimeParser();
      times = parser.Parse(@"..\..\1310137.html");
    }

    [Test]
    [Ignore("Decide how to test invalid HTML content")]
    public void ParseInvalid()
    {
    }

    [Test]
    public void ParseValid()
    {
      Assert.That(times.Count, Is.EqualTo(272));
      Console.WriteLine(times.Count + " stop times parsed:");
      Console.Write(times);
    }

    [Test]
    public void GetDifference()
    {
      TimeSpan span = new TimeSpan(16, 5, 54);

      Console.WriteLine("Time is {0}", span);
      List<StopTime> stops = times.GetNextStops(span, 5);
      bool showSeconds = true;
      foreach (StopTime stop in stops)
      {
        Console.WriteLine("{0} {1}", stop, stop.GetDifference(span, showSeconds));
        showSeconds = false;
      }
    }
  }
}
