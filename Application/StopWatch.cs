using System;
using System.Windows.Forms;
using StopWatch.Properties;
using System.Collections.Specialized;

namespace StopWatch
{
  public class StopWatch
  {
    [STAThread]
    static void Main(string[] args)
    {
      ParseCmdLineArguments(args);

      StopTimes stopTimes = MainWindow.ParseTimetableFile(Settings.Default.TimetableFile);
      if (stopTimes != null)
      {
        foreach (string bus in Settings.Default.ExcludedBuses)
        {
          if (!String.IsNullOrEmpty(bus))
          {
            try
            {
              stopTimes.ExcludeBus(bus);
            }
            catch (ArgumentOutOfRangeException e)
            {
              MessageBox.Show(e.Message, "Invalid setting");
            }
          }
        }
        Application.EnableVisualStyles();
        Application.Run(new MainWindow(stopTimes));
      }
    }

    private static void ParseCmdLineArguments(string[] args)
    {
      for (int i = 0; i < args.Length - 1; i += 2)
      {
        string arg = args[i];
        string value = args[i + 1];
        switch (arg)
        {
          case "-c":
            int count = 0;
            if (Int32.TryParse(value, out count))
            {
              Settings.Default.StopTimeCount = count;
            }
            else
            {
              HandleInvalidCmdLineValue(arg, value, "The value is not a valid integer");
            }
            break;

          case "-d":
            int delay = 0;
            if (Int32.TryParse(value, out delay))
            {
              Settings.Default.StopTimeDelay = delay;
            }
            else
            {
              HandleInvalidCmdLineValue(arg, value, "The value is not a valid integer");
            }
            break;

          case "-e":
            StringCollection excludedBuses = new StringCollection();
            excludedBuses.AddRange(value.Split(','));
            Settings.Default.ExcludedBuses = excludedBuses;
            break;

          case "-f":
            Settings.Default.TimetableFile = value;
            break;

          default:
            HandleInvalidCmdLineArgument(arg);
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

    private static void HandleInvalidCmdLineValue(string arg, string value, string message)
    {
      MessageBox.Show("Ignoring invalid value '" + value +
                      "' for command line argument '" + arg + "': " +
                      message + ".",
                      "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
  }
}
