using System;
using System.Collections.Generic;
using System.Text;
using StopWatch;
using StopWatchNUnitTester;

namespace StopWatchExecutableTester
{
  class Program
  {
    static void Main(string[] args)
    {
      // Create wanted testers
      StopTimesTester tester = new StopTimesTester();
      tester.Init();
      StopTimesParserTester parserTester = new StopTimesParserTester();
      parserTester.Init();

      // Execute wanted tests
    }
  }
}
