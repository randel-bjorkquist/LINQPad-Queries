<Query Kind="Program" />

void Main()
{
  Console.WriteLine("Variable(s)");
//  var now = DateTime.Now;
  var now = new DateTime(2022, 7, 7);
  now.Year.Dump();
  now.Month.Dump();
  
//  var day_of_month = DayOfMonthEnum.First.Dump();
//  var day_of_month = DayOfMonthEnum.Second.Dump();
//  var day_of_month = DayOfMonthEnum.Third.Dump();
//  var day_of_month = DayOfMonthEnum.Fourth.Dump();
  var day_of_month = DayOfMonthEnum.Last.Dump();
//  var day_of_week  = DayOfWeekEnum.Wednesday.Dump();
  var day_of_week  = DayOfWeekEnum.Thursday.Dump();
  
  Console.WriteLine("---------------------------------------------------");
  Console.WriteLine("DateTimeExtensions.GetDayOfMonth( .... )");
  
  DateTimeExtensions.GetDayOfMonth( now.Year.Dump()
                                   ,now.Month.Dump()
                                   ,day_of_month.Dump() 
                                   ,day_of_week.Dump())
                    .Dump();
           
  Console.WriteLine("---------------------------------------------------");
  Console.WriteLine("GetDayOfMonth( .. )");
  
  now.Dump()
     .AddMonths(1).Dump()
     .GetDayOfMonth(day_of_month.Dump(),
                    day_of_week.Dump())
     .Dump();

  Console.WriteLine("---------------------------------------------------");
  Console.WriteLine("TryGetDayOfMonth( .. )");

  now.Dump()
     .AddMonths(1).Dump()
     .TryGetDayOfMonth(day_of_month.Dump(), 
                       day_of_week.Dump())
     .Dump();

  Console.WriteLine("---------------------------------------------------");
  Console.WriteLine("TryGetDayOfMonth( ... )");

  DateTime.Now.Dump()
          .AddMonths(1).Dump()
          .TryGetDayOfMonth(day_of_month.Dump(),
                            day_of_week.Dump(),
                            out var date_of_month)
          .Dump();
  
  date_of_month.Dump("date_of_month");
}


public static class DateTimeExtensions
{
  #region COMMENTED OUT: initial version
  //
  //public static int GetDayOfMonth(this DateTime day, DayOfMonthEnum day_of_month, DayOfWeekEnum day_of_week)
  //{
  //  return GetDayOfMonth(day.Year, day.Month, day_of_month, day_of_week);
  //}
  //
  ////For example to find the day for 2nd Friday, February, 2016
  ////  =>call FindDay(2016, 2, DayOfWeek.Friday, 2)
  //public static int GetDayOfMonth(int year, int month, DayOfMonthEnum day_of_month, DayOfWeekEnum day_of_week)
  //{
  //  var week_day  = 0;
  //  var occurance = 0;
  //  
  //  #region
  //  
  //  switch(day_of_week)
  //  {
  //    default:  
  //    case DayOfWeekEnum.Unknown: throw new ArgumentOutOfRangeException("Invalid Argument - occurance is invalid");
  //    
  //    case DayOfWeekEnum.Sunday:    week_day = (int)DayOfWeek.Sunday;     break;
  //    case DayOfWeekEnum.Monday:    week_day = (int)DayOfWeek.Monday;     break;
  //    case DayOfWeekEnum.Tuesday:   week_day = (int)DayOfWeek.Tuesday;    break;
  //    case DayOfWeekEnum.Wednesday: week_day = (int)DayOfWeek.Wednesday;  break;
  //    case DayOfWeekEnum.Thursday:  week_day = (int)DayOfWeek.Thursday;   break;
  //    case DayOfWeekEnum.Friday:    week_day = (int)DayOfWeek.Friday;     break;
  //    case DayOfWeekEnum.Saturday:  week_day = (int)DayOfWeek.Saturday;   break;
  //  }
  //  
  //  switch(day_of_month)
  //  {
  //    default:
  //    case DayOfMonthEnum.Unknown: throw new ArgumentOutOfRangeException("Invalid Argument - occurance is invalid");
  //    
  //    case DayOfMonthEnum.First:  occurance = 1;  break;        
  //    case DayOfMonthEnum.Second: occurance = 2;  break;
  //    case DayOfMonthEnum.Third:  occurance = 3;  break;
  //    case DayOfMonthEnum.Fourth: occurance = 4;  break;
  //    case DayOfMonthEnum.Last:   occurance = 5;  break;
  //  }
  //  
  //  #endregion
  //
  //  DateTime firstDayOfMonth = new DateTime(year, month, 1);
  //  
  //  //Substract first day of the month with the required day of the week 
  //  var daysneeded = (int)week_day - (int)firstDayOfMonth.DayOfWeek;
  //
  //  //if it is less than zero we need to get the next week day (add 7 days)
  //  if(daysneeded < 0)
  //  {
  //    daysneeded = daysneeded + 7;
  //  }
  //  
  //  //DayOfWeek is zero index based; multiply by the Occurance to get the day
  //  var day = (daysneeded + 1) + (7 * (occurance - 1));
  //
  //  if(day > (firstDayOfMonth.AddMonths(1) - firstDayOfMonth).Days)
  //  {
  //    //throw new Exception(String.Format("No {0} occurance(s) of {1} in the required month", occurance, day_of_week.ToString()));
  //    //$"No {occurance} occurance(s) of {day.ToString()} in the required month".Dump();
  //    
  //    return GetDayOfMonth(year, month, DayOfMonthEnum.Fourth, day_of_week);
  //  }
  //  
  //  return day;
  //}  
  //
  #endregion

  public static DateTime GetDayOfMonth(this DateTime  d8, 
                                       DayOfMonthEnum day_of_month,
                                       DayOfWeekEnum  day_of_week)
  {
    TryGetDayOfMonth(d8, day_of_month, day_of_week, out var date_of_month);
    
    return date_of_month;
  }
  
  public static DateTime GetDayOfMonth(int year, 
                                       int month,
                                       DayOfMonthEnum day_of_month,
                                       DayOfWeekEnum  day_of_week)
  {
    var d8 = new DateTime(year, month, 1);
    TryGetDayOfMonth(d8, day_of_month, day_of_week, out var date_of_month);
    
    return date_of_month;
  }
  
  public static bool TryGetDayOfMonth(this DateTime  d8, 
                                      DayOfMonthEnum day_of_month,
                                      DayOfWeekEnum  day_of_week)
  {
//    DateTime date_of_month;
//    return TryGetDayOfMonth(d8, day_of_month, day_of_week, out date_of_month);
    return TryGetDayOfMonth(d8, day_of_month, day_of_week, out _);
  }
  
  // d8           : The date to start from (usually DateTime.Now)
  // day_of_month : The nth occurance (3rd)
  // day_of_week  : the day of the week to look for
  public static bool TryGetDayOfMonth(this DateTime  d8, 
                                      DayOfMonthEnum day_of_month, 
                                      DayOfWeekEnum  day_of_week, 
                                      out DateTime   date_of_month)
  {
    var index    = 0;
    var week_day = DayOfWeek.Sunday;
    
    #region
    
    switch(day_of_week)
    {
      default:  
      case DayOfWeekEnum.Unknown: throw new ArgumentOutOfRangeException("Invalid Argument - occurance is invalid");
      
      case DayOfWeekEnum.Sunday:    week_day = DayOfWeek.Sunday;     break;
      case DayOfWeekEnum.Monday:    week_day = DayOfWeek.Monday;     break;
      case DayOfWeekEnum.Tuesday:   week_day = DayOfWeek.Tuesday;    break;
      case DayOfWeekEnum.Wednesday: week_day = DayOfWeek.Wednesday;  break;
      case DayOfWeekEnum.Thursday:  week_day = DayOfWeek.Thursday;   break;
      case DayOfWeekEnum.Friday:    week_day = DayOfWeek.Friday;     break;
      case DayOfWeekEnum.Saturday:  week_day = DayOfWeek.Saturday;   break;
    }
    
    switch(day_of_month)
    {
      default:
      case DayOfMonthEnum.Unknown: throw new ArgumentOutOfRangeException("Invalid Argument - occurance is invalid");
      
      case DayOfMonthEnum.First:  index = 0;  break;        
      case DayOfMonthEnum.Second: index = 1;  break;
      case DayOfMonthEnum.Third:  index = 2;  break;
      case DayOfMonthEnum.Fourth: index = 3;  break;
      case DayOfMonthEnum.Last:   index = 4;  break;
    }
    
    #endregion

    var days_in_month = Enumerable.Range(1, DateTime.DaysInMonth(d8.Year, d8.Month))
                                  .Select(day => new DateTime(d8.Year, d8.Month, day));
                                  
    var days_of_week = days_in_month.Where(d => d.DayOfWeek == week_day)
                                    .OrderBy(d => d.Day)
                                    .Select(d => d)
                                    .ToArray();
    
    var days_of_week_count = days_of_week.Count();
    
    index = days_of_week_count > index 
          ? index
          : days_of_week_count - 1;

    var day = days_of_week.ElementAt(index);

    // make sure this occurance is within the original month
    var result = day.Month == d8.Month &&
                 day.Year  == d8.Year;
    
    date_of_month = result ? day : new DateTime();

    return result;
  }
  
  #region COMMENTED OUT: ORIGINAL CODE FROM STACK OVERFLOW
  //  https://stackoverflow.com/questions/5421972/how-to-find-the-3rd-friday-in-a-month-with-c
  //
  //public static bool TryGetDayOfMonth(this DateTime instance, 
  //                                    DayOfMonthEnum day_of_month, 
  //                                    DayOfWeekEnum day_of_week, 
  //                                    out DateTime date_of_month)
  //{
  //  var week_day  = DayOfWeek.Sunday;
  //  var occurance = 0;
  //  
  //  #region
  //  
  //  switch(day_of_week)
  //  {
  //    default:  
  //    case DayOfWeekEnum.Unknown: throw new ArgumentOutOfRangeException("Invalid Argument - occurance is invalid");
  //    
  //    case DayOfWeekEnum.Sunday:    week_day = DayOfWeek.Sunday;     break;
  //    case DayOfWeekEnum.Monday:    week_day = DayOfWeek.Monday;     break;
  //    case DayOfWeekEnum.Tuesday:   week_day = DayOfWeek.Tuesday;    break;
  //    case DayOfWeekEnum.Wednesday: week_day = DayOfWeek.Wednesday;  break;
  //    case DayOfWeekEnum.Thursday:  week_day = DayOfWeek.Thursday;   break;
  //    case DayOfWeekEnum.Friday:    week_day = DayOfWeek.Friday;     break;
  //    case DayOfWeekEnum.Saturday:  week_day = DayOfWeek.Saturday;   break;
  //  }
  //  
  //  switch(day_of_month)
  //  {
  //    default:
  //    case DayOfMonthEnum.Unknown: throw new ArgumentOutOfRangeException("Invalid Argument - occurance is invalid");
  //    
  //    case DayOfMonthEnum.First:  occurance = 1;  break;        
  //    case DayOfMonthEnum.Second: occurance = 2;  break;
  //    case DayOfMonthEnum.Third:  occurance = 3;  break;
  //    case DayOfMonthEnum.Fourth: occurance = 4;  break;
  //    case DayOfMonthEnum.Last:   occurance = 5;  break;
  //  }
  //  
  //  #endregion
  //  
  //  if(instance == null)
  //  {
  //    throw new ArgumentNullException("instance");
  //  }
  //
  //  if(occurance <= 0 || occurance > 5)
  //  {
  //    throw new ArgumentOutOfRangeException("occurance", "Occurance must be greater than zero and less than 6.");
  //  }
  //
  //  bool result;
  //  date_of_month = new DateTime();
  //
  //  // Change to first day of the month
  //  DateTime day = instance.AddDays(1 - instance.Day);
  //
  //  // Find first dayOfWeek of this month;
  //  if(day.DayOfWeek > week_day)
  //  {
  //    day = day.AddDays(7 - (int)day.DayOfWeek + (int)week_day);
  //  }
  //  else
  //  {
  //    day = day.AddDays((int)week_day - (int)day.DayOfWeek);
  //  }
  //
  //  // add 7 days per occurance
  //  day = day.AddDays(7 * (occurance - 1));
  //
  //  // make sure this occurance is within the original month
  //  result = day.Month == instance.Month &&
  //           day.Year  == instance.Year;
  //
  //  if(result)
  //  {
  //    date_of_month = day;
  //  }
  //
  //  return result;
  //}
  //
  #endregion
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
