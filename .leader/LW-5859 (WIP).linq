<Query Kind="Program" />

void Main()
{
  var surveys = new List<SurveyModel> 
                { 
                    new SurveyModel { ID            = 1
                                     ,Title         = "Survey 1"
                                     ,CreatedByUid  = "0x01"
                                     ,CreatedBy     = new ParticipantModel { UID = "0x01"
                                                                            ,FirstName = "Fred" }},
                    new SurveyModel { ID            = 2
                                     ,Title         = "Survey 2"
                                     ,CreatedByUid  = "0x02" },
                    new SurveyModel { ID            = 3
                                     ,Title         = "Survey 3"
                                     ,CreatedByUid  = "0x03"
                                     ,CreatedBy     = new ParticipantModel { UID = "0x03"
                                                                            ,FirstName = "Barney" }},
                    new SurveyModel { ID            = 4
                                     ,Title         = "Survey 4"
                                     ,CreatedByUid  = "0x04" },
                    new SurveyModel { ID            = 5
                                     ,Title         = "Survey 5"
                                     ,CreatedByUid  = "0x05" },
                    new SurveyModel { ID            = 6
                                     ,Title         = "Survey 6"
                                     ,CreatedBy     = new ParticipantModel { UID = "0x06"
                                                                            ,FirstName = "Bate" }},
                    new SurveyModel { ID            = 7
                                     ,Title         = "Survey 7"
                                     ,CreatedByUid  = "0x07" },
                    new SurveyModel { ID            = 8
                                     ,Title         = "Survey 8"
                                     ,CreatedByUid  = "0x08" },
                    new SurveyModel { ID            = 9
                                     ,Title         = "Survey 9"
                                     ,CreatedByUid  = "0x09"
                                     ,CreatedBy     = new ParticipantModel { UID = "0x09"
                                                                            ,FirstName = "Leadr" }} 
                };

  //surveys.Dump("surveys - all", 0);
  Print(surveys, "surveys - all", 0);
  
  //------------------------------------------------------------------
  surveys = surveys.Where(s => s.CreatedBy != null)
                   .ToList();

  //surveys.Dump("surveys - no CreatedBy", 0);
  Print(surveys, "surveys - no CreatedBy", 0);

  //------------------------------------------------------------------
  surveys = surveys.Where(s => s.CreatedBy == new ParticipantModel())
                   .ToList();

  //surveys.Dump("surveys - CreatedBy = new ParticipantModel", 0);
  Print(surveys, "surveys - CreatedBy = new ParticipantModel", 0);
  
  //------------------------------------------------------------------  
}


public void Print(IEnumerable<SurveyModel> surveys, string description, int? collapse_to)
{
  //items.Dump(description, collapse_to);
  
  $"{description}".Dump();

  foreach (var survey in surveys)
  {
    $"ID: {survey.ID} | Title: {survey.Title} | Created By: {survey.CreatedBy?.FullName ?? ""}".Dump();
  }
  
  "".Dump();
}

//public void Print<T>( IEnumerable<T> items, string description, int? collapse_to)
//{
//  items.Dump(description, collapse_to);
//  
//  foreach(var item in items)
//  {
//    
//  }
//}



// You can define other methods, fields, classes and namespaces here
[Flags]
public enum SurveyFillOptionEnum : short
{
   None         = 0
  ,Questions    = 1
  ,Participants = 2
  ,Answers      = 4
}

public enum QuestionTypeEnum 
{
   Uknown         = 0
  ,CustomScale    = 1
  ,Freeform       = 2
  ,MultipleChoice = 4  
}

public class SurveyModel 
{
  public int ID                       { get; set; }
                                      
  public string Title                 { get; set; } = String.Empty;  
  public string Objective             { get; set; } = String.Empty;
                                    
  public DateTime? CompleteBy         { get; set; }
  public DateTime? CompletedOn        { get; set; }
                                      
  public string CreatedByUid          { get; set; } = String.Empty;
  public ParticipantModel CreatedBy   { get; set; }
  public DateTime CreatedOn           { get; set; }
  
  public string ModifiedByUid         { get; set; } = String.Empty;
  public ParticipantModel ModifiedBy  { get; set; }
  public DateTime? ModifiedOn         { get; set; }
                                      
  public bool IsAnonymous             { get; set; }
  public bool IsDeleted               { get; set; }

  public IEnumerable<SurveyParticipantModel> Participants { get; set; }
  public IEnumerable<SurveyQuestionModel> Questions { get; set; }
  public IEnumerable<SurveyAnswerModel> Answers { get; set; }
}

public class SurveyQuestionModel
{
  public int ID { get; set; }
  public int SurveyID           { get; set; }
  
  public int? QuestionID        { get; set; }
  public QuestionTypeEnum Type  { get; set; }
  
  public string Configuration   { get; set; } = String.Empty;
  public string RichText        { get; set; } = String.Empty;
  public string PlainText       { get; set; } = String.Empty;
  
  public short Rank             { get; set; }
}

public class SurveyParticipantModel 
{
  public int ID                       { get; set; }
  public int SurveyID                 { get; set; }
  
  public string ParticipantID         { get; set; } = String.Empty;
  public ParticipantModel Participant { get; set; }
  
  public bool IsCreator               { get; set; }
  public DateTime? CompletedOn        { get; set; }
}

public class SurveyAnswerModel 
{
  public int ID                       { get; set; }
  public int SurevyID                 { get; set; }
                                      
  public string ParticipantID         { get; set; } = String.Empty;
  public ParticipantModel Participant { get; set; }
  
  public string Response              { get; set; } = String.Empty;
}

public class ParticipantModel 
{
  public string UID             { get; set; } = String.Empty;
  
  public string FirstName       { get; set; } = String.Empty;
  public string LastName        { get; set; } = String.Empty;
  public string FullName        { get => $"{this.FirstName} {this.LastName}"; }
  
  public string EmailAddress    { get; set; } = String.Empty;
  public string ProfileImageURL { get; set; } = String.Empty;
}

public class QuestionModel 
{
  public int ID                 { get; set; }
  public QuestionTypeEnum Type  { get; set; }
  
  public string Configuration   { get; set; } = String.Empty;
  public string RichText        { get; set; } = String.Empty;
  public string PlainText       { get; set; } = String.Empty;
  
  public short Rank             { get; set; }
}

public interface ISurveyService 
{
  
}

public class SurveyService : ISurveyService
{
  
}

public class LiteObjectDTO
{
  public int Id             { get; set; }
  public string Code        { get; set; }
  public string Description { get; set; }
}