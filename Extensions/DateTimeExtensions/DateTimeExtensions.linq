<Query Kind="Program" />

void Main()
{ 
  var now = DateTime.Now;
  now.Dump("var now = DateTime.Now;");
  
  now.TryGetDayOfMonth(DayOfMonthEnum.Second, DayOfWeekEnum.Friday, out var next);
  next.Dump("now.TryGetDayOfMonth(DayOfMonthEnum.Second, DayOfWeekEnum.Friday, out var next);");
  
  var day_of_month = DateTimeExtensions.GetDayOfMonth(2025, 12, DayOfMonthEnum.Last, DayOfWeekEnum.Friday);
  day_of_month.Dump("var day_of_month = DateTimeExtensions.GetDayOfMonth(2025, 12, DayOfMonthEnum.Last, DayOfWeekEnum.Friday);");
}

/// IMPORTANT: Note the 'Flags' attribute
/// Enum in c# supports the Flags attribute which can be used whenever the enum result 
/// represents a collection of values instead of a single value . This allows the developers 
/// to group the enum values. Assume that we have an enum with the following entries 
/// Resource URL: https://docs.microsoft.com/en-us/dotnet/api/system.flagsattribute?view=net-6.0
[Flags]
public enum FrequencyEnum : short
{
   Unknown = 0
  ,Yearly   = 1
  ,Monthly  = 2
  ,Weekly   = 4
  ,Daily    = 8
  ,Hourly   = 16
  ,Minutely = 32
  ,Secondly = 64
}

/// IMPORTANT: Note the 'Flags' attribute
/// Enum in c# supports the Flags attribute which can be used whenever the enum result 
/// represents a collection of values instead of a single value . This allows the developers 
/// to group the enum values. Assume that we have an enum with the following entries 
/// Resource URL: https://docs.microsoft.com/en-us/dotnet/api/system.flagsattribute?view=net-6.0
[Flags]
public enum DayOfMonthEnum : short
{
   Unknown  = 0
  ,First    = 1
  ,Second   = 2
  ,Third    = 4
  ,Fourth   = 8
  ,Last     = 16
}

/// IMPORTANT: Note the 'Flags' attribute
/// Enum in c# supports the Flags attribute which can be used whenever the enum result 
/// represents a collection of values instead of a single value . This allows the developers 
/// to group the enum values. Assume that we have an enum with the following entries 
/// Resource URL: https://docs.microsoft.com/en-us/dotnet/api/system.flagsattribute?view=net-6.0
[Flags]
public enum DayOfWeekEnum : short
{
   Unknown 	  = 0
  ,Sunday 	  = 1
  ,Monday 	  = 2
  ,Tuesday 	  = 4
  ,Wednesday  = 8
  ,Thursday   = 16
  ,Friday     = 32
  ,Saturday   = 64
}

// You can define other methods, fields, classes and namespaces here
public static class DateTimeExtensions
{
  public static DateTime GetDayOfMonth(this DateTime d8,
                                       DayOfMonthEnum day_of_month,
                                       DayOfWeekEnum day_of_week)
  {
    _ = TryGetDayOfMonth(d8, day_of_month, day_of_week, out var date_of_month);

    return date_of_month;
  }

  public static DateTime GetDayOfMonth(int year,
                                       int month,
                                       DayOfMonthEnum day_of_month,
                                       DayOfWeekEnum day_of_week)
  {
    var d8 = new DateTime(year, month, 1);
    TryGetDayOfMonth(d8, day_of_month, day_of_week, out var date_of_month);

    return date_of_month;
  }

  public static bool TryGetDayOfMonth(this DateTime d8,
                                      DayOfMonthEnum day_of_month,
                                      DayOfWeekEnum day_of_week)
  {
    return TryGetDayOfMonth(d8, day_of_month, day_of_week, out _);
  }

  public static bool TryGetDayOfMonth(this DateTime d8,
                                      DayOfMonthEnum day_of_month,
                                      DayOfWeekEnum day_of_week,
                                      out DateTime date_of_month)
  {
    var index = 0;
    var week_day = DayOfWeek.Sunday;

    switch (day_of_week)
    {
      default:
      case DayOfWeekEnum.Unknown: throw new ArgumentOutOfRangeException("Invalid Argument - occurance is invalid");

      case DayOfWeekEnum.Sunday:    week_day = DayOfWeek.Sunday;    break;
      case DayOfWeekEnum.Monday:    week_day = DayOfWeek.Monday;    break;
      case DayOfWeekEnum.Tuesday:   week_day = DayOfWeek.Tuesday;   break;
      case DayOfWeekEnum.Wednesday: week_day = DayOfWeek.Wednesday; break;
      case DayOfWeekEnum.Thursday:  week_day = DayOfWeek.Thursday;  break;
      case DayOfWeekEnum.Friday:    week_day = DayOfWeek.Friday;    break;
      case DayOfWeekEnum.Saturday:  week_day = DayOfWeek.Saturday;  break;
    }

    switch (day_of_month)
    {
      default:
      case DayOfMonthEnum.Unknown: throw new ArgumentOutOfRangeException("Invalid Argument - occurance is invalid");

      case DayOfMonthEnum.First:  index = 0; break;
      case DayOfMonthEnum.Second: index = 1; break;
      case DayOfMonthEnum.Third:  index = 2; break;
      case DayOfMonthEnum.Fourth: index = 3; break;
      case DayOfMonthEnum.Last:   index = 4; break;
    }

    var days_in_month = Enumerable.Range(1, DateTime.DaysInMonth(d8.Year, d8.Month))
                                  .Select(day => new DateTime(d8.Year, d8.Month, day));

    var days_of_week = days_in_month.Where(d => d.DayOfWeek == week_day)
                                    .OrderBy(d => d.Day)
                                    .Select(d => d)
                                    .ToArray();

    //NOTE: if the count equals the index, it could be either the 'last' or 'fourth' instance
    //      AND because 'Count' is not zero based, it could run off the end and throw a
    //      System.ArgumentOutOfRangeException exception with the message: "Index was out of
    //      range. Must be non-negative and less than the size of the collection."
    var days_of_week_count = days_of_week.Count();

    index = days_of_week_count > index
          ? index
          : days_of_week_count - 1;

    var day = days_of_week.ElementAt(index);

    // make sure this occurance is within the original month
    var result = day.Month == d8.Month &&
                 day.Year == d8.Year;

    date_of_month = result ? day : new DateTime();

    return result;
  }

  public static DateTime DateOnly(this DateTime dateTime)
  => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);

  public static DateTime TimeOnly(this DateTime dateTime)
  => DateTime.MinValue.AddHours(dateTime.Hour).AddMinutes(dateTime.Minute).AddSeconds(dateTime.Minute)
      .AddMilliseconds(dateTime.Millisecond);
}

