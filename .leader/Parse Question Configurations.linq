<Query Kind="Program">
  <NuGetReference>Microsoft.AspNetCore.Mvc.NewtonsoftJson</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Diagnostics.CodeAnalysis</Namespace>
</Query>

void Main()
{
  var publish_frequency = "{\"StartOn\":\"12/20/2022\", \"DayOfMonth\":\"1\", \"DayOfWeek\":\"8\", \"Frequency\":\"2\"}";
  var frequency = publish_frequency.Parse<SurveySeries>();
  frequency.Dump("publish_frequency", 0);
  
  
  var template_configuration = "[{\"Category\":\"1\",\"Count\":\"1\",\"Frequency\":\"1\"},{\"Category\":\"4\",\"Count\":\"1\",\"Frequency\":\"4\"},{\"Category\":\"8\",\"Count\":\"1\",\"Frequency\":\"1\"},{\"Category\":\"16\",\"Count\":\"1\",\"Frequency\":\"1\"}]";
  var configs = template_configuration.ParseToIEnumerable<DynamicQuestionConfiguration>()
                                      .Dump("template_configuration", 0);
  
  string config = string.Empty;
  
  config = "[{\"option\":\"Family\",\"rank\":\"0\"},{\"option\":\"Friends\",\"rank\":\"1\"},{\"option\":\"Life\",\"rank\":\"2\"},{\"option\":\"Money\",\"rank\":\"3\"},{\"option\":\"Car\",\"rank\":\"4\"}]";
  config.Dump("config - MultipleChoiceQuestionConfiguration", 0);
  
  var items = config.ParseToIEnumerable<MultiplChoiceQuestionConfiguration>();
  items.Dump("items", 0);
  items.Single(e => e.Rank == "2")
       .Option
       .Dump("items.Single(e => e.Rank == \"2\").Option");
  
  config = "{\"minLabel\":\"Worst\",\"minValue\":\"1\",\"maxLabel\":\"Best\",\"maxValue\":\"3\"}";
  config.Dump("config - CustomScaleQuestionConfiguration", 0);
  
  var item = config.Parse<CustomScaleQuestionConfiguration>();
  item.Dump("CustomScaleQuestionConfiguration", 0);

  //question/count/frequency broke out
  //config = "[{\"Category\":\"1\",\"Count\":\"1\",\"Frequency\":\"1\"},{\"Category\":\"4\",\"Count\":\"1\",\"Frequency\":\"4\"},{\"Category\":\"8\",\"Count\":\"1\",\"Frequency\":\"1\"},{\"Category\":\"16\",\"Count\":\"1\",\"Frequency\":\"1\"}]";
  config = "["
         + " {\"Category\":\"1\",\"Count\":\"1\",\"Frequency\":\"1\"}"
         + ",{\"Category\":\"4\",\"Count\":\"1\",\"Frequency\":\"4\"}"
         + ",{\"Category\":\"8\",\"Count\":\"1\",\"Frequency\":\"1\"}"
         + ",{\"Category\":\"16\",\"Count\":\"1\",\"Frequency\":\"1\"}"
         + "]";
  
  var values = config.ParseToIEnumerable<DynamicQuestionConfiguration>();
  values.OrderByDescending(v => v.Category)
        .Dump("DynamicQuestionConfiguration (individual entries)", 0);

  //question/count/frequency similar combined
  //config = "[{\"Category\":\"25\",\"Count\":\"1\",\"Frequency\":\"1\"},{\"Category\":\"4\",\"Count\":\"1\",\"Frequency\":\"4\"}]";
  config = "["
         + " {\"Category\":\"25\",\"Count\":\"1\",\"Frequency\":\"1\"}"
         + ",{\"Category\":\"4\",\"Count\":\"1\",\"Frequency\":\"4\"}"
         + "]";

  values = config.ParseToIEnumerable<DynamicQuestionConfiguration>();
  values.OrderByDescending(v => v.Category)
        .Dump("DynamicQuestionConfiguration (similar entries combined)", 0);

  #region R&D CODE
  
  #region Hide this stuff
  
  //var data = JsonConvert.DeserializeObject(configuration);
//  var data = JsonConvert.DeserializeObject<IEnumerable<QuestionConfiguration>>(configuration);
//  data.Dump("JsonConvert.DeserializeObject<IEnumerable<QuestionConfiguration>>(configuration)");
//  data.First(d => d.Rank == "2")
//      .Dump("data.First(d => d.Rank == \"2\")");
  
  
//  configuration.ParseQuestionConfiguration()
//               .Dump("Question Configuration KeyValuePair(s)");
//  
//  configuration.ParseQuestionConfiguration()
//               .Single(kvp => kvp.Key == "Money")
//               .Dump("Question Configuration KeyValuePair w/Key == \"Money\"");
//
//  configuration.TryParseQuestionOption("UNKNOWN", out string key1);
//  key1.Dump("configuration.TryParseQuestionOption(\"UNKNOWN\", out string key1);");
//
//  configuration.TryParseQuestionOption("2", out string key2);
//  key2.Dump("configuration.TryParseQuestionOption(\"2\", out string key2);");

  //configuration.ParseQuestionConfiguration()
  //             .SingleOrDefault(kvp => kvp.Key == "UNKNOWN")
  //             .Dump("Question Configuration KeyValuePair w/Key == \"UNKNOWN\"");

  //configuration.ParseQuestionConfiguration(option: "option")
  //             .Dump("Question Configuration Elements");
  //
  //configuration.ParseQuestionConfiguration(rank: "rank")
  //             .Dump("Question Configuration Elements");
  //
  //configuration.ParseQuestionConfiguration("option", "rank")
  //             .Dump("Question Configuration Elements");
  
//  configuration.ParseQuestionOption("2")
//               .Dump("Question Option w/Value == \"2\"");
  
  //configuration.ParseQuestionOption("2", "option", "rank")
  //             .Dump("Question Option w/Rank == 2");
  
//  configuration.ParseQuestionValue("Life")
//               .Dump("Question Value w/Option == \"Life\"");
  
  //configuration.ParseQuestionRank("Life", "option", "rank")
  //             .Dump("Question Rank w/Option == Life");
  
//  int.TryParse("0", out int blah);
//  blah.Dump("int.TryParse(\"0\", out int blah)");
//  
//  var x = int.Parse("a");
//  x.Dump("int.Parse(\"0\")");

  #endregion

//  //char[] start = new char[] {'[', '{'};
//  //char[] end   = new char[] {']', '}'};
//  //
//  //configuration = configuration.TrimStart(start);
//  //configuration = configuration.TrimEnd(end);
//  configuration = configuration.Replace("\"", "");
//  configuration.Dump();
//
//  configuration = configuration.Replace("option:", "");
//  configuration.Dump();
//  
//  configuration = configuration.Replace("rank:", "");
//  configuration.Dump();
//
//  configuration = configuration.TrimStart(new char[] {'[', '{'});
//  configuration.Dump();
//
//  configuration = configuration.TrimEnd(new char[] {']', '}'});
//  configuration.Dump();
//
////  var elements = configuration.Split(",");
//  var elements = configuration.Split("},{");
//  elements.Dump();
//  
////  var kps = elements.Select(e => e.Contains("\"rank\":\"2\""));
////  var kps = elements.Where(e => e.Contains("\"rank\":\"2\""));
////  var kps = elements.Where(e => e.Contains("rank:2"))
//  var kps = elements.Single(e => e.Contains("2"))
//                    .Split(",")
//                    .First();
////                    .Select(e => new KeyValuePair<string, string>(key: e.Split("option:")[1],
////                                                                  value:e.Split("rank:,")[1]))
////                    .Select(e => new KeyValuePair(e.Split(",")[0], e.Split(",")[1]))
////                    .SelectMany(e => e.Split(","))
////                    .SelectMany(e => e.Split(":"))
//                    ;
//  kps.Dump();

  #endregion
}

public class MultiplChoiceQuestionConfiguration
{
  public string Option  { get; set; }
  public string Rank    { get; set; }
}

public class CustomScaleQuestionConfiguration
{
  [JsonProperty("MinLabel")]
  public string MinimumLabel  { get; set; }
  [JsonProperty("MinValue")]
  public string MinimumValue  { get; set; }
  
  [JsonProperty("MaxLabel")]
  public string MaximumLabel  { get; set; }
  [JsonProperty("MaxValue")]
  public string MaximumValue  { get; set; }
}

[Flags]
public enum FrequencyEnum : short
{
    Uknown      =  0,
    Yearly      =  1,
    Monthly     =  2,
    Weekly      =  4,
    Daily       =  8,
    Hourly      = 16,
    Minutely    = 32,
    Secondly    = 64
}

[Flags]
public enum DayOfMonthEnum : short
{
    Uknown  =   0,
    First   =   1,
    Second  =   2,
    Third   =   4,
    Fouth   =   8,
    Last    =  16
}

[Flags]
public enum DayOfWeekEnum : short
{
    Uknown      =   0,
    Sunday      =   1,
    Monday      =   2,
    Tuesday     =   4,
    Wednesday   =   8,
    Thursday    =  16,
    Friday      =  32,
    Saturday    =  64
}

[Flags]
public enum QuestionCategoryEnum : short
{
   Unknow       =  0
  ,Clarity      =  1
  ,Custom       =  2
  ,eNPS         =  4
  ,Maximization =  8
  ,Rapport      = 16
}

[Flags]
public enum TemplateTypeEnum : short
{
  Unknown = 0
 ,Curated = 1
 ,Custom  = 2
 ,Pulse   = 4
}

[Serializable]
public class SurveySeries : PublishFrequency
{  
}

[Serializable]
public abstract class PublishFrequency
{
    public DateTime StartOn             { get; set; }
    
    public DayOfMonthEnum   DayOfMonth  { get; set; }
    public DayOfWeekEnum    DayOfWeek   { get; set; }
    
    public FrequencyEnum Frequency      { get; set; }
}

public class DynamicQuestionConfiguration 
{
  public QuestionCategoryEnum Category  { get; set; }
  public short Count                    { get; set; }
  public string Frequency               { get; set; }
//  public string Frequency_string        { get; set; }
//  public decimal Frequency_decimal      { get; set; }
//  public List<string> UserGroups        { get; set; }
}

public static class StringExtensions
{
  public static IEnumerable<T> ParseToIEnumerable<T>(this string configuration)
  {
    //return JsonConvert.DeserializeObject<IEnumerable<T>>(configuration);
    var items = JsonConvert.DeserializeObject<IEnumerable<T>>(configuration);
    return (items ?? Enumerable.Empty<T>()).ToList();
  }

  public static T Parse<T>(this string configuration)
  {
    return JsonConvert.DeserializeObject<T>(configuration);
    //var items = JsonConvert.DeserializeObject<IEnumerable<T>>(configuration);
    //return items ?? Enumerable.Empty<T>();
  }
  
#region COMMENTED OUT: R&D CODE
//  
//  public static IEnumerable<KeyValuePair<string, string>> ParseQuestionConfiguration(this string configuration, string option = "option", string rank = "rank")
//  {
//    configuration = configuration.Replace("\"", "");
//    
//    configuration = configuration.Replace($"{option}:", "");
//    configuration = configuration.Replace($"{rank}:", "");
//    
//    configuration = configuration.TrimStart(new char[] { '[', '{' });
//    configuration = configuration.TrimEnd(new char[] {']', '}'});   
//    
//    var kvps = configuration.Split("},{")
//                            .Select(e => new KeyValuePair<string, string>(key: e.Split(",")[0], 
//                                                                          value: e.Split(",")[1]));
//    
//    return kvps;
//  }
//  
//  public static string ParseQuestionOption(this string configuration, string value, string option = "option", string rank = "rank")
//  {
//    var kvp = configuration.ParseQuestionConfiguration(option, rank)
//                           .Single(c => c.Value == value);
//                           
//    return kvp.Key;
//  }
//  
//  public static string ParseQuestionValue(this string configuration, string key, string option = "option", string rank = "rank")
//  {
//    var kvp = configuration.ParseQuestionConfiguration(option, rank)
//                           .Single(c => c.Key == key);
//                           
//    return kvp.Value;
//  }
//  
//  public static bool TryParseQuestionOption(this string configuration, string value, out string key, string option = "option", string rank = "rank")
//  {
//    var parsed = true;
//    var kvp = configuration.ParseQuestionConfiguration(option, rank)
//                           .SingleOrDefault(c => c.Value == value);
//    
//    if(kvp.Equals(default(KeyValuePair<string, string>)))
//    {
//      parsed = false;
//    }
//    
//    key = kvp.Key;
//    
//    return parsed;
//  }
//  
//  public static bool TryParseQuestionValue(this string configuration, string key, out string value, string option = "option", string rank = "rank")
//  {
//    var parsed = true;
//    var kvp = configuration.ParseQuestionConfiguration(option, rank)
//                           .SingleOrDefault(c => c.Key == key);
//    
//    if(kvp.Equals(default(KeyValuePair<string, string>)))
//    {
//      parsed = false;
//    }
//    
//    value = kvp.Value;
//    
//    return parsed;
//  }
//  
#endregion
}

