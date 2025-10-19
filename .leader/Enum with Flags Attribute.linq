<Query Kind="Program" />

void Main()
{
  //var options = SurveyFillOptionEnum.None;
  //options.Dump("options = SurveyFillOptionEnum.None");
  //
  //var participants  = true;
  //var questions     = true;
  //var answers       = true;
  //
  //options = participants ? options | SurveyFillOptionEnum.Participants : options;
  //options = questions    ? options | SurveyFillOptionEnum.Questions    : options;
  //options = answers      ? options | SurveyFillOptionEnum.Answers      : options;
  //
  //options.Dump();
  
  //var options = SurveyFillOptionEnum.Participants | SurveyFillOptionEnum.Questions | SurveyFillOptionEnum.Answers;
  //
  //for(int value = 0; value < 8; value++)
  //{
  //  ($"{value,3} - {(SurveyFillOptionEnum)value:G}").Dump("(SurveyFillOptionEnum)value");
  //  //((SurveyFillOptionEnum)value).Dump("(SurveyFillOptionEnum)value");
  //}
  
//  Enum.GetNames(typeof(ETemplateType)).Dump();
//  Enum.GetValues(typeof(ETemplateType)).Dump();
  
  var count = 0;
  
  foreach(var e in Enum.GetValues(typeof(ETemplateType)))
  {
    count += (short)e;
  }
  
  for(int value = 0; value <= count; value++)
  {
    ($"{value,3} - {(ETemplateType)value:G}").Dump("(ETemplateType)value");
  }
  
//  var option = ETemplateType.Custom_Pulse;
//  ($"{option,3} - {(ETemplateType)option:G}").Dump("(ETemplateType)value");
//  
//  option = ETemplateType.Curated_Pulse;
//  ($"{option,3} - {(ETemplateType)option:G}").Dump("(ETemplateType)value");

#region COMMENTED OUT: R&D  
//  
//  ($"{options,3} - {options:G}").Dump("(SurveyFillOptionEnum)value");  
//  options.Dump("options");
//  
//  var values = Enum.GetValues(typeof(SurveyFillOptionEnum))
//                   .Cast<short>()
//                   .Where(f => f & 0 == f)
//                   .ToList();
//  values.Dump("values");
//  
//  var values = new List<short>(Enum.GetValues(typeof(SurveyFillOptionEnum)).Cast<short>())
//                                   .Where(enumValue => enumValue != 0 && (enumValue & x) == enumValue).ToList();
//  
//  var values = new List<short>((SurveyFillOptionEnum[])(Enum.GetValues(typeof(SurveyFillOptionEnum)))
//                               .Where(v => v.HasFlag(v))
//                               .Select(v => (int)v)
//                               .ToList());
//
//  var values = new List<short>(Enum.GetValues(typeof(SurveyFillOptionEnum))
//                                 .Cast<short>()
//                               .Where(v => v.HasFlag(v))
//                               .Select(v => (int)v)
//                               .ToList());
//
#endregion
  
  
//  var opts = Enum.GetValues(typeof(QuestionTypeEnum))
//                 .Cast<QuestionTypeEnum>()
//                 .Select(e => (e, (int)e))
//                 .ToList();
//  opts.Dump("opts");

  var opts = Enum.GetValues(typeof(ETemplateType))
                 .Cast<ETemplateType>()
                 .Select(e => new LiteObjectDTO{ Id = (int)e
                                                ,Code = e.ToString()
                                                ,Description = e.ToString() })
                 //.ToList()
                 ;
                 
    opts.Dump();

}

// You can define other methods, fields, classes and namespaces here
[Flags]
public enum SurveyFillOptionEnum : short
{
   None         = 0
  ,Questions    = 1
  ,Participants = 2
  ,Answers      = 4
}

[Flags]
public enum ETemplateType : short
{
   Unknown        = 0
  ,Curated        = 1
  ,Custom         = 2
  ,Pulse          = 4
//  ,Custom_Pulse   = Custom  | Pulse
//  ,Curated_Pulse  = Curated | Pulse
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
  public int ID                 { get; set; }
                                
  public string Title           { get; set; } = String.Empty;  
  public string Objective       { get; set; } = String.Empty;
  
  public DateTime? CompleteBy   { get; set; }
  public DateTime? CompletedOn  { get; set; }
  
  public string CreatedBy       { get; set; } = String.Empty;
  public DateTime CreatedOn     { get; set; }
  
  public string ModifiedBy      { get; set; } = String.Empty;
  public DateTime? ModifiedOn   { get; set; }
  
  public bool IsAnonymous       { get; set; }
  public bool IsDeleted         { get; set; }

  public IEnumerable<SurveyParticipantModel> Participants { get; set; }
  public IEnumerable<SurveyQuestionModel>    Questions    { get; set; }
  public IEnumerable<SurveyAnswerModel>      Answers      { get; set; }
}

public class SurveyQuestionModel 
{
  public int ID                 { get; set; }
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