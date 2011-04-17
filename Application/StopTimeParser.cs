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

    private StopTimes mStopTimes = new StopTimes();

    private const string CLASS_TITLE = "stoptitle1";
    private const string CLASS_TIMETABLE = "media_width";
    private const string CLASS_HOUR = "stop_hour";
    private const string CLASS_MINUTE = "stop_small_min";
    private const string CLASS_BUS = "stop_small_codes";

    private const string TITLE_WEEKDAYS = "Ma-pe";
    private const string HOUR_PREVIOUS = "&nbsp;";
    private const string BUS_PREFIX = "&#47;";

    public StopTimes Parse(string path)
    {
      HtmlDocument doc = new HtmlDocument();
      doc.Load(path);

      StringWriter sw = new StringWriter();
      HtmlNode timeTable = FindTimetable(doc.DocumentNode, sw);
      if (timeTable != null)
      {
        ParseTimetable(timeTable, sw);
      }
      sw.Flush();
      Log(sw.ToString());
      return mStopTimes;
    }

    private void Log(string content)
    {
      StreamWriter writer = new StreamWriter("1310137.txt");
      writer.Write(content);
      writer.Flush();
      writer.Close();
    }

    private HtmlNode FindTimetable(HtmlNode node, TextWriter outText)
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
              if (CLASS_TITLE.Equals(attribute.Value) &&
                  TITLE_WEEKDAYS.Equals(node.InnerText))
              {
                outText.WriteLine("Found " + TITLE_WEEKDAYS + "!");
                mTitleFound = true;
                break;
              }
            }
            break;
          
          default:
            break;
        }
      }

      foreach (HtmlNode subnode in node.ChildNodes)
      {
        HtmlNode tableNode = FindTimetable(subnode, outText);
        if (tableNode != null)
        {
          return tableNode;
        }
      }

      return null;
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

    private void ParseRowNode(HtmlNode node, TextWriter outText)
    {
      if (HtmlNodeType.Element.Equals(node.NodeType))
      {
        foreach (HtmlAttribute attribute in node.Attributes.AttributesWithName("class"))
        {
          switch (attribute.Value)
          {
            case CLASS_HOUR:
              mHour = ParseHour(node.InnerText, mHour, outText);
              break;

            case CLASS_MINUTE:
              mMinute = ParseMinute(node.InnerText, outText);
              break;

            case CLASS_BUS:
              if (mHour != INVALID_VALUE && mMinute != INVALID_VALUE)
              {
                try
                {
                  mStopTimes.Add(mHour, mMinute, ParseBus(node.InnerText));
                  outText.Write(mStopTimes.GetLatestAddition() + "   ");
                }
                catch (ArgumentException e)
                {
                  outText.WriteLine("Failed to add stop time: " + e.Message);
                }
              }
              break;

            default:
              break;
          }
        }
      }

      foreach (HtmlNode subnode in node.ChildNodes)
      {
        ParseRowNode(subnode, outText);
      }
    }

    private void ParseTimetable(HtmlNode node, TextWriter outText)
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
    }
  }
}