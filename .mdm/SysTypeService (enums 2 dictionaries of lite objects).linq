<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
</Query>

void Main()
{
  ISysTypeService service = new SysTypeService();
  var show_secret = new Dictionary<string, bool>(){ {SysTypes.DaysOfMonth         ,false }
                                                   ,{SysTypes.DaysOfWeek          ,false }
                                                   ,{SysTypes.Frequencies         ,false }
                                                   ,{SysTypes.PlaywrightLogins    ,true }
                                                   ,{SysTypes.QuestionCategories  ,false }
                                                   ,{SysTypes.QuestionTypes       ,false }
                                                   ,{SysTypes.SurveyStatuses      ,false }
                                                   ,{SysTypes.TemplateTypes       ,false } };
  #region SysTypeService Method Calls
  
  if(show_secret[SysTypes.DaysOfMonth])
    service.DaysOfMonth().Dump("DaysOfMonth()", 0);
  
  if(show_secret[SysTypes.DaysOfWeek])
    service.DaysOfWeek().Dump("DaysOfWeek()", 0);
  
  if(show_secret[SysTypes.Frequencies])
    service.Frequencies().Dump("Frequencies()", 0);
  
  
  if(show_secret[SysTypes.PlaywrightLogins])
  {
    var systype = service.PlaywrightLogins().Dump("PlaywrightLogins()", 0);
    string value = JsonConvert.SerializeObject(systype);
    
    value.Dump("value");
  }
    
  
  if(show_secret[SysTypes.QuestionCategories])
    service.QuestionCategories() .Dump("QuestionCategories()", 0);
    
  if(show_secret[SysTypes.QuestionTypes])
    service.QuestionTypes().Dump("QuestionTypes()", 0);
  
  if(show_secret[SysTypes.SurveyStatuses])
    service.SurveyStatuses().Dump("SurveyStatuses()", 0);
  
  if(show_secret[SysTypes.TemplateTypes])
    service.TemplateTypes().Dump("TemplateTypes()", 0);
    
  #endregion
}

#region SysTypes enum(s)

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

[Flags]
public enum QuestionCategoryEnum : short
{
  Unknown         =  0,
  Clarity         =  1,
  Custom          =  2,
  eNPS            =  4,
  Maximization    =  8,
  Rapport         = 16
}

public enum QuestionTypeEnum : short
{
  Unknown         = 0,
  CustomScale     = 1,
  ShortAnswer     = 2,
  MultipleChoice  = 4
}

public enum SurveyStatusEnum : short
{
  Unknown     =  0,
  Draft       = 10,
  Active      = 20,
  Completed   = 30,
  Closed      = 40
}

[Flags]
public enum TemplateTypeEnum : short
{
  Unknown         =  0,
  Curated         =  1,
  Custom          =  2,
  Pulse           =  4
}

#endregion

public class LiteObject
{
  public int Id             { get; set; }
  public string Code        { get; set; }
  public string Description { get; set; }
}

public class LoginModel
{
  public string Username { get; set; }
  public string Password { get; set; }
}

public class PlaywriteLoginModel
{
  //public string Title   { get; set; }
  public string BaseURL { get; set; }
  
  public Dictionary<string, string> Paths             { get; set; }
  public Dictionary<string, List<LoginModel>> Loggins { get; set; }
}

public static class SysTypes
{
  public static string DaysOfMonth        => nameof(DaysOfMonth);
  public static string DaysOfWeek         => nameof(DaysOfWeek);
                                          
  public static string Frequencies        => nameof(Frequencies);                                          
  public static string PlaywrightLogins   => nameof(PlaywrightLogins);
                                          
  public static string QuestionCategories => nameof(QuestionCategories);
  public static string QuestionTypes      => nameof(QuestionTypes);
  
  public static string SurveyStatuses     => nameof(SurveyStatuses);
  public static string TemplateTypes      => nameof(TemplateTypes);
}

public interface ISysTypeService
{
  Dictionary<string, LiteObject> DaysOfMonth();
  Dictionary<string, LiteObject> DaysOfWeek();

  Dictionary<string, LiteObject> Frequencies();
  Dictionary<string, List<PlaywriteLoginModel>> PlaywrightLogins();

  Dictionary<string, LiteObject> QuestionCategories();
  Dictionary<string, LiteObject> QuestionTypes();

  Dictionary<string, LiteObject> SurveyStatuses();
  Dictionary<string, LiteObject> TemplateTypes();
}

public class SysTypeService : ISysTypeService
{
  public Dictionary<string, LiteObject> Frequencies()
  {
    //NOTE: At this time, I am ONLY returning the individual values: 'Yearly', 'Monthly', 'Weekly',
    //      'Daily', 'Hourly', 'Minutely', and 'Secondly'. ALL combinations, like
    //      'Yearly_Monthly' or 'Weekly_Daily' are hidden.

    var systypes = Enum.GetValues(typeof(FrequencyEnum))
                       .Cast<FrequencyEnum>()
                       .Select(f => new LiteObject{ Id = (int)f
                                                   ,Code = f.ToString()
                                                   ,Description = $"Occurs every 'x' {f.ToString().TrimEnd('y', 'l').ToLower()}(s)." })
                       .Where(sys => sys.Id > 0)
                       .GroupBy(sys => sys.Code)
                       .ToDictionary(sys => sys.Key,
                                     sys => sys.Single());

    //Replaces the descripton 'dai' (created above) with 'day'
    var systype = systypes.Single(sys => sys.Value.Id == (short)FrequencyEnum.Daily);
    systype.Value.Description = systype.Value.Description.Replace("dai", "day");

    return systypes;
  }

  public Dictionary<string, LiteObject> DaysOfMonth()
  {
    //NOTE: At this time, I am ONLY returning the individual values: 'First', 'Second', 'Third',
    //      'Fourth', and 'Last'. ALL combinations, like 'First_Second' or 'Second_Last' are hidden.

    var systypes = Enum.GetValues(typeof(DayOfMonthEnum))
                       .Cast<DayOfMonthEnum>()
                       .Select(dom => new LiteObject { Id = (int)dom
                                                      ,Code = dom.ToString()
                                                      ,Description = $"The {dom.ToString().ToLower()} 'x' of every month." })
                       .Where(sys => sys.Id > 0)
                       .GroupBy(sys => sys.Code)
                       .ToDictionary(sys => sys.Key,
                                     sys => sys.Single());

    return systypes;
  }

  public Dictionary<string, LiteObject> DaysOfWeek()
  {
    //NOTE: At this time, I am ONLY returning the individual values: 'Sunday', 'Monday', 'Tuesday',
    //      'Wednesday', 'Thursday', 'Friday,, and 'Saturday'. ALL combinations, like
    //      'Monday_Wednesday_Friday' or 'Tuesday_Thursday' are hidden.

    var systypes = Enum.GetValues(typeof(DayOfWeekEnum))
                       .Cast<DayOfWeekEnum>()
                       .Select(dow => new LiteObject { Id = (int)dow
                                                      ,Code = dow.ToString()
                                                      ,Description = Regex.Replace(dow.ToString(), "(\\B[A-Z])", " $1").ToString() })
                       .Where(sys => sys.Id > 0)
                       .GroupBy(sys => sys.Code)
                       .ToDictionary(sys => sys.Key,
                                     sys => sys.Single());

    return systypes;
  }
  
  public Dictionary<string, List<PlaywriteLoginModel>> PlaywrightLogins()
  {
    var systypes = new Dictionary<string, List<PlaywriteLoginModel>>();
    
    systypes.Add("empty", Data.PlaywrightLogins);
    
    return systypes;
  }
  
  public Dictionary<string, LiteObject> QuestionCategories()
  {
    //NOTE: At this time, I am ONLY returning the individual values: 'Clarity', 'Custom', 'eNPS',
    //      'Maximization' and 'Rapport'. ALL combinations, like 'Clarity_Custom' or 'Custom_eNPS' 
    //      are hidden.

    #region COMMENTED OUT: Enums w/Flags Attribute
    //
    //var count    = 0;
    //var systypes = new Dictionary<string, LiteObject>();
    //    
    //foreach(var e in Enum.GetValues(typeof(QuestionCategoryEnum)))
    //{
    //    count += (short)e;
    //}
    //
    //for(int value = 1; value <= count; value++)
    //{
    //    var systype = new LiteObject { Id = value,
    //                                   Code = ((QuestionCategoryEnum)value).ToString(),
    //                                   Description = Regex.Replace(((QuestionCategoryEnum)value).ToString(), "(\\B[A-Z])", " $1").ToString() };
    //
    //    systypes.Add(((QuestionCategoryEnum)value).ToString()
    //                                              .Replace(", ", "_")
    //                 ,systype);
    //}
    //
    //foreach(var systype in systypes)
    //{
    //    if(systype.Value.Code.Contains(QuestionCategoryEnum.eNPS.ToString()))
    //    {
    //        systype.Value.Description = systype.Value
    //                                           .Description
    //                                           .Replace( Regex.Replace(QuestionCategoryEnum.eNPS.ToString(), "(\\B[A-Z])", " $1").ToString(),
    //                                                    QuestionCategoryEnum.eNPS.ToString());
    //    }
    //}
    //
    //return systypes;
    //
    #endregion

    #region Enums w/o the Flags Attribute

    var systypes = Enum.GetValues(typeof(QuestionCategoryEnum))
                       .Cast<QuestionCategoryEnum>()
                       .Select(qqe => new LiteObject { Id = (int)qqe
                                                      ,Code = qqe.ToString()
                                                      ,Description = Regex.Replace(qqe.ToString(), "(\\B[A-Z])", " $1").ToString() })
                       .Where(sys => sys.Id > 0)
                       .GroupBy(sys => sys.Code)
                       .ToDictionary(sys => sys.Key,
                                     sys => sys.Single());

    //Replaces the descripton 'e N P S' (created by the above Regex) with 'eNPS'
    var systype = systypes.Single(sys => sys.Value.Id == (short)QuestionCategoryEnum.eNPS);
    systype.Value.Description = QuestionCategoryEnum.eNPS.ToString();

    return systypes;

    #endregion
  }

  public Dictionary<string, LiteObject> QuestionTypes()
  {
    var systypes = Enum.GetValues(typeof(QuestionTypeEnum))
                       .Cast<QuestionTypeEnum>()
                       .Select(qte => new LiteObject{ Id = (int)qte
                                                     ,Code = qte.ToString()
                                                     ,Description = Regex.Replace(qte.ToString(), "(\\B[A-Z])", " $1").ToString() })
                       .Where(sys => sys.Id > 0)
                       .GroupBy(sys => sys.Code)
                       .ToDictionary(sys => sys.Key,
                                     sys => sys.Single());

    return systypes;
  }

  public Dictionary<string, LiteObject> SurveyStatuses()
  {
    var systypes = Enum.GetValues(typeof(SurveyStatusEnum))
                       .Cast<SurveyStatusEnum>()
                       .Select(sse => new LiteObject{ Id = (int)sse
                                                     ,Code = sse.ToString()
                                                     ,Description = Regex.Replace(sse.ToString(), "(\\B[A-Z])", " $1").ToString() })
                       .Where(sys => sys.Id > 0)
                       .GroupBy(sys => sys.Code)
                       .ToDictionary(sys => sys.Key,
                                     sys => sys.Single());

    return systypes;
  }

  public Dictionary<string, LiteObject> TemplateTypes()
  {
    #region Enums w/Flags Attribute
    
    var count = 0;
    var systypes = new Dictionary<string, LiteObject>();
    
    foreach(var e in Enum.GetValues(typeof(TemplateTypeEnum)))
    {
      count += (short)e;
    }
    
    for(int value = 1; value <= count; value++)
    {
        var systype = new LiteObject { Id = value
                                      ,Code = ((TemplateTypeEnum)value).ToString()
                                      ,Description = Regex.Replace(((TemplateTypeEnum)value).ToString(), "(\\B[A-Z])", " $1").ToString() };
      
        systypes.Add(((TemplateTypeEnum)value).ToString()
                                              .Replace(", ", "_")
                     ,systype);
    }
    
    return systypes;
    
    #endregion

    #region COMMENTED OUT: Enums w/o the Flags Attribute (DOES NOT return all possibilities)
    //
    //var systypes = Enum.GetValues(typeof(TemplateTypeEnum))
    //                   .Cast<TemplateTypeEnum>()
    //                   .Select(tte => new LiteObject { Id = (int)tte,
    //                                                   Code = tte.ToString(),
    //                                                   Description = Regex.Replace(tte.ToString(), "(\\B[A-Z])", " $1").ToString() })
    //                   .Where(sys => sys.Id > 0)
    //                   .GroupBy(sys => sys.Code)
    //                   .ToDictionary(sys => sys.Key,
    //                                 sys => sys.Single());
    //
    //return systypes;
    //
    #endregion
  }
}

public static class Data
{
  public static List<PlaywriteLoginModel> PlaywrightLogins
    => new List<PlaywriteLoginModel> { 
          new PlaywriteLoginModel { 
             BaseURL = "https://swtxcrm--FULL--.lightning.force.com/lightning/"
            ,Paths = new Dictionary<string, string> { {"dev"  ,@"C:\\Users\\RyanCaster\\source\\repos\\PlaywrightTests\\assets\\videos\dev"  }
                                                     ,{"qa"   ,@"C:\\Users\\RyanCaster\\source\\repos\\PlaywrightTests\\assets\\videos\qa"   }
                                                     ,{"uat"  ,@"C:\\Users\\RyanCaster\\source\\repos\\PlaywrightTests\\assets\\videos\uat"  }
                                                     ,{"prod" ,@"C:\\Users\\RyanCaster\\source\\repos\\PlaywrightTests\\assets\\videos\prod" }}
            ,Loggins = new Dictionary<string, List<LoginModel>> {
                          { "Playwright", new List<LoginModel> { 
                                            new LoginModel { Username = "user_1_name@playwrite.com", Password = "e2iuvQgsjIZT4oorTvhv" }
                                           ,new LoginModel { Username = "user_2_name@playwrite.com", Password = "qxd1tvx!knw0YWP5ufps" }
                                          }}
//                         ,{"", new List<LoginModel> {}}
//                         ,{"", new List<LoginModel> {}}
//                         ,{"", new List<LoginModel> {}}
                       }
          }
         ,new PlaywriteLoginModel { 
             BaseURL = "https://swtxcrm--DEV--.lightning.force.com/lightning/"
            ,Paths = new Dictionary<string, string> { {"PBI-0000"  ,@"C:\\Users\\RyanCaster\\source\\repos\\PlaywrightTests\\assets\\videos\pbi-0000" }
                                                     ,{"PBI-1111"  ,@"C:\\Users\\RyanCaster\\source\\repos\\PlaywrightTests\\assets\\videos\pbi-1111" }
                                                     ,{"PBI-3333"  ,@"C:\\Users\\RyanCaster\\source\\repos\\PlaywrightTests\\assets\\videos\pbi-3333" }
                                                     ,{"PBI-6666"  ,@"C:\\Users\\RyanCaster\\source\\repos\\PlaywrightTests\\assets\\videos\pbi-6666" }}
            ,Loggins = new Dictionary<string, List<LoginModel>> { 
                          { "Playwright", new List<LoginModel> { 
                                            new LoginModel { Username = "user_1_name@playwrite.com", Password = "e2iuvQgsjIZT4oorTvhv" }
                                           ,new LoginModel { Username = "user_2_name@playwrite.com", Password = "qxd1tvx!knw0YWP5ufps" }
                                          }}
                         ,{ "Salesforce", new List<LoginModel> { 
                                             new LoginModel { Username = "user_a_name@salesforce.com", Password = "e2iuvQgsjIZT4oorTvhv" }
                                            ,new LoginModel { Username = "user_b_name@salesforce.com", Password = "qxd1tvx!knw0YWP5ufpx" }
                                          }}
                       }
          }
//         ,new PlaywriteLoginModel {} 
       };
}
