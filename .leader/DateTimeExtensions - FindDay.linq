<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

void Main()
{
  DateTimeExtensions.FindDay( DateTime.Now.Year.Dump()
                             ,DateTime.Now.Month.Dump()
                             ,DayOfWeekEnum.Friday.Dump()
                             ,DayOfMonthEnum.First.Dump() )
                    .Dump();
           
  "".Dump();
  
  DateTime.Now.Dump()
          .AddMonths(1).Dump()
          .FindDay(DayOfWeekEnum.Thursday.Dump(), 
                   DayOfMonthEnum.Second.Dump())
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
  public static int FindDay(this DateTime day, DayOfWeekEnum day_of_week, DayOfMonthEnum day_of_month)
  {
    return FindDay(day.Year, day.Month, day_of_week, day_of_month);
  }
  
  //For example to find the day for 2nd Friday, February, 2016
  //  =>call FindDay(2016, 2, DayOfWeek.Friday, 2)
  public static int FindDay(int year, int month, DayOfWeekEnum day_of_week, DayOfMonthEnum day_of_month)
  {
    var day       = 0;
    var occurance = 0;
    
    #region
    
    switch(day_of_week)
    {
      default:  
      case DayOfWeekEnum.Unknown: break;
      case DayOfWeekEnum.Sunday:    day = (int)DayOfWeek.Sunday;     break;
      case DayOfWeekEnum.Monday:    day = (int)DayOfWeek.Monday;     break;
      case DayOfWeekEnum.Tuesday:   day = (int)DayOfWeek.Tuesday;    break;
      case DayOfWeekEnum.Wednesday: day = (int)DayOfWeek.Wednesday;  break;
      case DayOfWeekEnum.Thursday:  day = (int)DayOfWeek.Thursday;   break;
      case DayOfWeekEnum.Friday:    day = (int)DayOfWeek.Friday;     break;
      case DayOfWeekEnum.Saturday:  day = (int)DayOfWeek.Saturday;   break;
    }
    
    switch(day_of_month)
    {
      case DayOfMonthEnum.First:  occurance = 1;  break;        
      case DayOfMonthEnum.Second: occurance = 2;  break;
      case DayOfMonthEnum.Third:  occurance = 3;  break;
      case DayOfMonthEnum.Fourth: occurance = 4;  break;
      case DayOfMonthEnum.Last:   occurance = 5;  break;
    }
    
    #endregion
    
    if(occurance < 1 || occurance > 5)
    {
      throw new Exception("Occurance is invalid");
    }

    DateTime firstDayOfMonth = new DateTime(year, month, 1);
    
    //Substract first day of the month with the required day of the week 
    var daysneeded = (int)day - (int)firstDayOfMonth.DayOfWeek;

    //if it is less than zero we need to get the next week day (add 7 days)
    if(daysneeded < 0)
    {
      daysneeded = daysneeded + 7;
    }
    
    //DayOfWeek is zero index based; multiply by the Occurance to get the day
    var resultedDay = (daysneeded + 1) + (7 * (occurance - 1));

    if(resultedDay > (firstDayOfMonth.AddMonths(1) - firstDayOfMonth).Days)
    {
      //throw new Exception(String.Format("No {0} occurance(s) of {1} in the required month", occurance, day_of_week.ToString()));
      //$"No {occurance} occurance(s) of {day.ToString()} in the required month".Dump();
      
      return FindDay(year, month, day_of_week, DayOfMonthEnum.Fourth);
    }
    
    return resultedDay;
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
