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

      StopTimes stopTimes = ParseTimetableFile(Settings.Default.TimetableDir +
                                               Settings.Default.TimetableFile);
      if (stopTimes != null)
      {
        Application.Run(new MainWindow(stopTimes));
      }
    }

    private static void ParseCmdLineArguments(string[] args)
    {
      string timetableFile = args.Length > 0 ? args[0] : null;
      if (timetableFile != null)
      {
        Settings.Default.TimetableFile = timetableFile;
      }
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
