#region

using System.IO;
using System.Collections.Generic;
using HtmlAgilityPack;
using System;

#endregion

namespace StopWatch
{
  public class StopTimeParser
  {
    private static int INVALID_VALUE = -1;
    private bool mTitleFound = false;
    private int mHour = 0;
    private int mMinute = 0;
    private Weekday mWeekday;

    private StopTimes mStopTimes = new StopTimes();

    private const string CLASS_TITLE = "stoptitle1";
    private const string CLASS_TIMETABLE = "media_width";
    private const string CLASS_HOUR = "stop_hour";
    private const string CLASS_MINUTE = "stop_small_min";
    private const string CLASS_BUS = "stop_small_codes";

    private const string TITLE_WEEKDAYS = "Ma-pe";
    private const string TITLE_SATURDAY = "Lauantaisin";
    private const string TITLE_SUNDAY = "Sunnuntaisin";
    private const string HOUR_PREVIOUS = "&nbsp;";
    private const string BUS_PREFIX = "&#47;";
    private const string STOP_NAME_PREFIX = "HSL - Aikataulut - ";

    public StopTimes Parse(string path)
    {
      HtmlDocument doc = new HtmlDocument();
      doc.Load(path);

      StringWriter sw = new StringWriter();
      HtmlNode rootNode = doc.DocumentNode;

      mStopTimes.StopName = GetStopName(rootNode, sw);
      for (HtmlNode timeTable = FindTimetable(rootNode, sw);
           timeTable != null;
           timeTable = FindTimetable(rootNode, sw))
      {
        rootNode = ParseTimetable(timeTable, sw);
      }
      sw.Flush();
      //Log(sw.ToString());
      return mStopTimes;
    }

    private void Log(string content)
    {
      StreamWriter writer = new StreamWriter("1310137.txt");
      writer.Write(content);
      writer.Flush();
      writer.Close();
    }

    private string GetStopName(HtmlNode node, TextWriter outText)
    {
      string stopName = null;
      if (HtmlNodeType.Element.Equals(node.NodeType) &&
          "title".Equals(node.Name))
      {
        stopName = ParseStopName(node.InnerText);
      }
      else
      {
        foreach (HtmlNode subnode in node.ChildNodes)
        {
          stopName = GetStopName(subnode, outText);
          if (stopName != null)
          {
            return stopName;
          }
        }
      }
      return stopName;
    }

    private HtmlNode FindTimetable(HtmlNode node, TextWriter outText)
    {
      mTitleFound = false;
      return FindTimetableRecursive(node, outText);
    }

    private HtmlNode FindTimetableRecursive(HtmlNode node, TextWriter outText)
    {
      if (HtmlNodeType.Element.Equals(node.NodeType))
      {
        switch (node.Name)
        {
          case "table":
            if (mTitleFound)
            {
              foreach (HtmlAttribute attribute in node.Attributes.AttributesWithName("class"))
              {
                if (CLASS_TIMETABLE.Equals(attribute.Value))
                {
                  outText.WriteLine("Found timetable!");
                  return node;
                }
              }
            }
            break;

          case "td":
            foreach (HtmlAttribute attribute in node.Attributes.AttributesWithName("class"))
            {
              if (CLASS_TITLE.Equals(attribute.Value))
              {
                switch (node.InnerText)
                {
                  case TITLE_WEEKDAYS:
                  case TITLE_SATURDAY:
                  case TITLE_SUNDAY:
                    outText.WriteLine("Found " + node.InnerText + "!");
                    mWeekday = GetWeekdayEnum(node.InnerText);
                    mTitleFound = true;
                    break;
                }
              }
            }
            break;
          
          default:
            break;
        }
      }

      foreach (HtmlNode subnode in node.ChildNodes)
      {
        HtmlNode tableNode = FindTimetableRecursive(subnode, outText);
        if (tableNode != null)
        {
          return tableNode;
        }
      }

      node.RemoveAllChildren();
      return null;
    }

    private Weekday GetWeekdayEnum(string timetableTitle)
    {
      Weekday weekday;
      switch (timetableTitle)
      {
        case TITLE_WEEKDAYS:
          weekday = Weekday.Weekdays;
          break;

        case TITLE_SATURDAY:
          weekday = Weekday.Saturday;
          break;

        case TITLE_SUNDAY:
          weekday = Weekday.Sunday;
          break;

        default:
          throw new ArgumentOutOfRangeException("Invalid timetable title " +
                                                timetableTitle);
      }
      return weekday;
    }

    private static int ParseHour(string text, int currentHour, TextWriter outText)
    {
      int hour = currentHour;
      if (!HOUR_PREVIOUS.Equals(text) && !Int32.TryParse(text, out hour))
      {
        outText.WriteLine("Invalid hour value '" + text + "'");
        hour = INVALID_VALUE;
      }
      return hour;
    }

    private static int ParseMinute(string text, TextWriter outText)
    {
      int minute;
      if (!Int32.TryParse(text, out minute))
      {
        outText.WriteLine("Invalid minute value '" + text + "'");
        minute = INVALID_VALUE;
      }
      return minute;
    }

    private static string ParseBus(string text)
    {
      return text.Replace(BUS_PREFIX, "");
    }

    private static string ParseStopName(string text)
    {
      return text.Replace(STOP_NAME_PREFIX, "").Trim();
    }

    private void ParseRowNode(HtmlNode node, TextWriter outText)
    {
      if (HtmlNodeType.Element.Equals(node.NodeType))
      {
        foreach (HtmlAttribute attribute in node.Attributes.AttributesWithName("class"))
        {
          IList<string> classes = new List<string>(attribute.Value.Split());
          if (classes.Contains(CLASS_HOUR))
          {
            mHour = ParseHour(node.InnerText, mHour, outText);
          }
          else if (classes.Contains(CLASS_MINUTE))
          {
            mMinute = ParseMinute(node.InnerText, outText);
          }
          else if (classes.Contains(CLASS_BUS))
          {
            if (mHour != INVALID_VALUE && mMinute != INVALID_VALUE)
            {
              try
              {
                StopTime stopTime = mStopTimes.Add(mWeekday, mHour, mMinute,
                                                   ParseBus(node.InnerText));
                outText.Write(stopTime + "   ");
              }
              catch (ArgumentException e)
              {
                outText.WriteLine("Failed to add stop time: " + e.Message);
              }
            }
          }
        }
      }

      foreach (HtmlNode subnode in node.ChildNodes)
      {
        ParseRowNode(subnode, outText);
      }
    }

    private HtmlNode ParseTimetable(HtmlNode node, TextWriter outText)
    {
      foreach (HtmlNode subnode in node.ChildNodes)
      {
        if ("tr".Equals(subnode.Name))
        {
          foreach (HtmlNode rowSubnode in subnode.ChildNodes)
          {
            ParseRowNode(rowSubnode, outText);
          }
        }
      }
      return node.ParentNode.ParentNode.ParentNode.ParentNode;
    }
  }
}