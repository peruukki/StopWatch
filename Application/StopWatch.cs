using System;
using System.IO;
using System.Windows.Forms;
using StopWatch.Properties;

namespace StopWatch
{
  public class StopWatch
  {
    [STAThread]
    static void Main(string[] args)
    {
      ParseCmdLineArguments(args);

      StopTimes stopTimes = ParseTimetableFile(Settings.Default.TimetableFile);
      if (stopTimes != null)
      {
        Application.Run(new MainWindow(stopTimes));
      }
    }

    private static void ParseCmdLineArguments(string[] args)
    {
      for (int i = 0; i < args.Length - 1; i += 2)
      {
        switch (args[i])
        {
          case "-f":
            Settings.Default.TimetableFile = args[i + 1];
            break;

          default:
            HandleInvalidCmdLineArgument(args[i]);
            break;
        }
      }

      if ((args.Length % 2) == 1)
      {
        HandleInvalidCmdLineArgument(args[args.Length - 1]);
      }
    }

    private static void HandleInvalidCmdLineArgument(string arg)
    {
      MessageBox.Show("Ignoring unknown command line argument '" + arg + "'.",
                      "Invalid argument", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private static StopTimes ParseTimetableFile(string fileName)
    {
      StopTimes stopTimes = null;

      try
      {
        stopTimes = new StopTimeParser().Parse(fileName);
      }
      catch (FileNotFoundException)
      {
        MessageBox.Show(String.Format("The timetable file '{0}' doesn't exist.",
                                      fileName),
                        "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      if (stopTimes != null && stopTimes.Count == 0)
      {
        MessageBox.Show(String.Format("Couldn't find timetable information from file '{0}'.",
                                      fileName),
                        "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Error);
        stopTimes = null;
      }

      return stopTimes;
    }
  }
}
