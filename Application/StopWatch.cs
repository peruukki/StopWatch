using System;
using System.IO;
using System.Windows.Forms;

namespace StopWatch
{
  public class StopWatch
  {
    private const string TIMETABLE_DIR = @"..\..\";
    private const string TIMETABLE_DEFAULT_FILE = TIMETABLE_DIR + "1310137.html";
    //private const string TIMETABLE_DEFAULT_FILE = "1434180.html";

    [STAThread]
    static void Main(string[] args)
    {
      string timetableFile = args.Length > 0 ? args[0] : null;

      StopTimes stopTimes =
        ParseTimetableFile((timetableFile != null) ? timetableFile
                                                   : TIMETABLE_DEFAULT_FILE);

      if (stopTimes != null)
      {
        Application.Run(new MainWindow(stopTimes));
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
