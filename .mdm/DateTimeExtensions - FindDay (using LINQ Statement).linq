<Query Kind="Program" />

void Main()
{  
  DateTime.Now.Dump()
          .AddMonths(1).Dump()
          .GetDayOfMonth(5, 
                         DayOfWeek.Monday)
          .Dump();
           
//  DayFinder.FindDay( DateTime.Now.Year.Dump()
//                    ,DateTime.Now.Month.Dump()
////                    ,DayOfWeek.Friday.Dump()
//                    ,DayOfWeekEnum.Friday.Dump()
//                    ,DayOfMonthEnum.Last.Dump() )
//           .Dump();
}


public static class DateTimeExtensions
{
  public static DateTime GetDayOfMonth(this DateTime dt, int n, DayOfWeek weekday)
  {
    return GetNthWeekdayOfMonth(dt, n, weekday);
  }
  
  // dt: The date to start from (usually DateTime.Now)
  // n: The nth occurance (3rd)
  // weekday: the day of the week to look for
  public static DateTime GetNthWeekdayOfMonth(DateTime dt, int n, DayOfWeek weekday)
  {
    if(n < 1 || n > 5)
    {
      throw new ArgumentOutOfRangeException("Invalid Argument - occurance is invalid");
    }

    var days_in_month = Enumerable.Range(1, DateTime.DaysInMonth(dt.Year, dt.Month))
                                  .Select(day => new DateTime(dt.Year, dt.Month, day));
                                  
    var days_of_week = days_in_month.Where(d => d.DayOfWeek == weekday)
                                    .OrderBy(d => d.Day)
                                    .Select(d => d)
                                    .ToArray()
                                    .Dump();
    
    //NOTE: The elements of the days_of_week are zero based and 'n' is 1 base,
    //      so subtract 1 from the count or occurance to get the correct index.
    var index = days_of_week.Count() > n - 1
              ? n - 1
              : days_of_week.Count() - 1;
    
    var day = days_of_week.ElementAt(index);
    return day;

#region COMMENTED OUT: R&D CODE
//    var days = Enumerable.Range(1, DateTime.DaysInMonth(dt.Year, dt.Month))
//                         .Select(day => new DateTime(dt.Year, dt.Month, day));
//
//    var weekdays = from day in days
//                   where day.DayOfWeek == weekday
//                   orderby day.Day ascending
//                   select day;
//
//    int index = n - 1;
//
//    if(index >= 0 && index < weekdays.Count())
//    {
//      return weekdays.ElementAt(index);
//    }
//    else
//    {
//      throw new InvalidOperationException("The specified day does not exist in this month!");
//    }
#endregion
  }
}

[Flags]
public enum DayOfMonthEnum : short
{
    Unknown =   0,
    First   =   1,
    Second  =   2,
    Third   =   4,
    Fourth  =   8,
    Last    =  16
}

[Flags]
public enum DayOfWeekEnum : short
{
    Unknown     =   0,
    Sunday      =   1,
    Monday      =   2,
    Tuesday     =   4,
    Wednesday   =   8,
    Thursday    =  16,
    Friday      =  32,
    Saturday    =  64
}

[Flags]
public enum FrequencyEnum : short
{
    Unknown         =   0,
    Yearly          =   1,
    Monthly         =   2,
    Weekly          =   4,
    Daily           =   8,
    Hourly          =  16,
    Minutely        =  32,
    Secondly        =  64
}
