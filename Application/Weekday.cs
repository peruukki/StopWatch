using System;
using System.Collections.Generic;
using System.Text;

namespace StopWatch
{
  public class Weekday
  {
    public static Weekday Weekdays = new Weekday(0, "Weekdays");
    public static Weekday Saturday = new Weekday(1, "Saturday");
    public static Weekday Sunday = new Weekday(2, "Sunday");
    public static int Count = 3;

    private int mOrdinal;
    public int Ordinal { get { return mOrdinal; } }

    private string mTitle;

    private Weekday(int ordinal, string title)
    {
      mOrdinal = ordinal;
      mTitle = title;
    }

    public static Weekday FromDayOfWeek(DayOfWeek dayOfWeek)
    {
      Weekday weekDay;
      switch (dayOfWeek)
      {
        case DayOfWeek.Sunday:
          weekDay = Weekday.Sunday;
          break;
        case DayOfWeek.Saturday:
          weekDay = Weekday.Saturday;
          break;
        default:
          weekDay = Weekday.Weekdays;
          break;
      }
      return weekDay;
    }

    public static Weekday FromOrdinal(int ordinal)
    {
      Weekday weekDay;
      switch (ordinal)
      {
        case 0:
          weekDay = Weekday.Weekdays;
          break;
        case 1:
          weekDay = Weekday.Saturday;
          break;
        case 2:
          weekDay = Weekday.Sunday;
          break;
        default:
          throw new ArgumentOutOfRangeException("Invalid ordinal " + ordinal);
      }
      return weekDay;
    }

    public override string ToString()
    {
      return mTitle;
    }
  }
}
