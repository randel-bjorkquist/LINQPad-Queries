<Query Kind="Program" />

void Main()
{
  Data.Fields.Dump(0);
  Data.Fields["Survey"].Dump(0);
  Data.Fields[Data.TableNames.Survey].Dump(0);
  
  foreach(var item in Data.Fields[Data.TableNames.Survey])
  {
    $"index: {item.index} | field name: {item.field_name}".Dump();
  }
}

public static class Data
{
  public static class TableNames 
  {
    public static string Survey               { get; } = "Survey";
    public static string SurveyParticipant    { get; } = "SurveyParticipant";
    public static string SurveyQuestion       { get; } = "SurveyQuestion";
    public static string Answer               { get; } = "Answer";
    public static string Question             { get; } = "Question";
    public static string TemplateParticipant  { get; } = "TemplateParticipant";
    public static string TemplateQuestion     { get; } = "TemplateQuestion";
  }
  
  public static Dictionary<string, List<(short index, string field_name)>> Fields 
  { 
    get
    {
      return new Dictionary<string, List<(short index, string field_name)>>
                  {
                    { "Survey" , new List<(short index, string field_name)> 
                                      { 
                                        (index: 0, field_name: "ID"),
                                        (index: 1, field_name: "SurveyID"),
                                        (index: 2, field_name: "QuestionID"),
                                        (index: 3, field_name: "Type"),
                                        (index: 4, field_name: "Configuration"),
                                        (index: 5, field_name: "Description"),
                                        (index: 6, field_name: "Question"),
                                        (index: 7, field_name: "Rank"),
                                      }},
                  };
    }
  }
}