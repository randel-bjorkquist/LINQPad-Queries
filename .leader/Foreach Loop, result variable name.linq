<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Diagnostics.CodeAnalysis</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
  var template_configuration = "[{\"Category\":\"1\",\"Count\":\"1\",\"Frequency\":\"1\"},{\"Category\":\"4\",\"Count\":\"1\",\"Frequency\":\"4\"},{\"Category\":\"8\",\"Count\":\"1\",\"Frequency\":\"1\"},{\"Category\":\"16\",\"Count\":\"1\",\"Frequency\":\"1\"}]";
  var configs = template_configuration.ParseToIEnumerable<DynamicQuestionConfiguration>()
                                      .Dump("template_configuration", 0);
  
  foreach(var config in configs)
  {
    var category  = $"Category: {config.Category}";
    var count     = $"Count: {config.Count}";
    var frequency = $"Frequency: {config.Frequency}";
    
    $"{category} {count} {frequency}".Dump();
  }
  
  "".Dump();
  
  foreach(var config in configs)
  {
    var category  = $"Category: {(short)config.Category}";
    var count     = $"Count: {config.Count}";
    var frequency = $"Frequency: {config.Frequency}";
    
    $"{category} {count} {frequency}".Dump();
  }
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
