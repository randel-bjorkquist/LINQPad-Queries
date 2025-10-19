<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Mvc</Namespace>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
  //AppDomain.CurrentDomain.GetAssemblies().Dump();
  
  //[CallerMemberName] string calling_method_name = string.Empty;
  
  ShowMessage("Boo");
  //
  //ISurveyService _survey = new SurveyService();
  //_survey.Dump();
  
  //System.Reflection.Assembly.GetExecutingAssembly().Dump();
  var assembly = Assembly.GetExecutingAssembly().Dump("Assembly.GetExecutingAssembly()");
  //Assembly.GetCallingAssembly().Dump("Assembly.GetCallingAssembly()");
  //Assembly.GetEntryAssembly().Dump("Assembly.GetEntryAssembly()");
  
  //var index = assembly.FullName.Split(',').First();
  assembly.FullName.Split(',').First().Dump();
  
  //var result = Assembly.GetExecutingAssembly()
  //            .GetTypes()
  //            .Where(type => typeof(SurveyService).IsAssignableFrom(type))
  //            .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
  //            .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
  //            .GroupBy(x => x.DeclaringType.Name)
  //            .Select(x => new { Controller = x.Key, Actions = x.Select(s => s.Name).ToList() })
  //            .ToList()
  //            .Dump();
  //
  //Assembly asm = Assembly.GetExecutingAssembly();
  //var controllerlist = asm.GetTypes()
  //                        .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
  //                        .Select(type => type.GetTypeInfo())
  //                        .Select(x => new {
  //                          Controller = x.Name,
  //                          ControllerDisplayName = x.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName
  //                         })
  //                        .OrderBy(o => o.Controller)
  //                        .ToList()
  //                        .Dump();
}

//[Flags]
public enum SurveyFillOptionEnum : short
{
    None                          = 0,
    Participants                  = 1,
    ParticipantsQuestions         = 2,
    QuestionsAnswers              = 3,
    ParticipantsQuestionsAnswers  = 5
}

public class SurveyDTO 
{
  public int ID           { get; set; }
  public string Title     { get; set; }
  public string Objective { get; set; }
}

public interface ISurveyService 
{
  public IEnumerable<SurveyDTO> GetSurveys();
  public SurveyDTO GetSurveyById(string id);
  public SurveyDTO GetSurveyById(int id);
}

public class SurveyService : ISurveyService 
{
  public IEnumerable<SurveyDTO> GetSurveys()
  {
    return Enumerable.Empty<SurveyDTO>();
  }
  
  public SurveyDTO GetSurveyById(string uid)
  {
    return new SurveyDTO();
  }

  public SurveyDTO GetSurveyById(int id)
  {
    return new SurveyDTO();
  }
}

[DisplayName("Survey Controller")]
public class SurveyController : Controller {  
}

//[DisplayName("People Name")]
public class PeopleController : Controller {
}

//public static void SomeMethodSomewhere() {
//  ShowMessage("Boo");
//}

public static void ShowMessage( string                    message
                               ,[CallerLineNumber] int    line_number = 0
                               ,[CallerMemberName] string member_name = null
                               ,[CallerFilePath]   string file_path   = "") {
  $"{message} at line '{line_number}' [{member_name} | {file_path}]".Dump();
}

