<Query Kind="Program" />

void Main()
{
  
  var creator = new Participant { ID  = 14
                                 ,UID = "0xf5de9ce" 
                                 ,FirstName = "Randel"
                                 ,LastName  = "Bjorkquist" };
  var survey = Data.Survey;
  
  survey.Creator = creator;
  survey.Dump(1);
  
  var details = survey.ToSurveyDetails();
  
  details.Creator = creator;
  details.Dump(1);
}

#region Models

public enum SurveyStatusEnum : short 
{
  Unknown     =  0,
  Draft       = 10,
  Active      = 20,
  Completed   = 30,
  Closed      = 40
}

public enum QuestionTypeEnum : short 
{
    Unknown         = 0,
    CustomScale     = 1,
    Freeform        = 2,
    MultipleChoice  = 4
}

public class ParticipantModel 
{
  public string Uid                   { get; set; } = string.Empty;

  public string FirstName             { get; set; } = string.Empty;
  public string LastName              { get; set; } = string.Empty;
  public string FullName              { get => $"{FirstName} {LastName}"; }

  public string EmailAddress          { get; set; } = string.Empty;
  public string ProfileImageUrl       { get; set; } = string.Empty;
}

public class SurveyParticipantModel 
{
  public int Id                                   { get; set; }
  public int SurveyId                             { get; set; }
   
  public string ParticipantUid                    { get; set; }
  public ParticipantModel Participant             { get; set; }
   
  public DateTime? CompletedOn                    { get; set; }
  public bool IsDeleted                           { get; set; }
}

public class AnswerModel 
{
    public int Id                                   { get; set; }
    
    public int SurveyQuestionId                     { get; set; }
    public int SurveyParticipantId                  { get; set; }
    
    public string Response                          { get; set; } = string.Empty;
}

public class SurveyQuestionModel 
{
    public int Id                               { get; set; }
    
    public int SurveyId                         { get; set; }
    public int? QuestionId                      { get; set; }

    public QuestionTypeEnum Type                { get; set; }
    public string Configuration                 { get; set; }
    
    public string RichText                      { get; set; }
    public string PlainText                     { get; set; }
    
    public short Rank                           { get; set; }
    public IEnumerable<AnswerModel> Answers     { get; set; }
}

public class SurveyModel 
{
  public int Id                                               { get; set; }
  public string OrganizationUid                               { get; set; }
  
  public string Title                                         { get; set; }
  public string Objective                                     { get; set; }

  public DateTime? CompleteBy                                 { get; set; }
  public DateTime? CompletedOn                                { get; set; }

  public string CreatedByUid                                  { get; set; }
  public ParticipantModel CreatedBy                           { get; set; }
  public DateTime? CreatedOn                                  { get; set; }

  public string ModifiedByUid                                 { get; set; }
  public ParticipantModel ModifiedBy                          { get; set; }
  public DateTime? ModifiedOn                                 { get; set; }

  public bool IsAnonymous                                     { get; set; }
  public bool IsDeleted                                       { get; set; }
  public SurveyStatusEnum Status                              { get; set; }

  public IEnumerable<SurveyParticipantModel> Participants     { get; set; }
  public IEnumerable<SurveyQuestionModel> Questions           { get; set; }
}

#endregion

#region DTOs

public abstract class BaseSurveyDTO 
{
  public int Id                                               { get; set; }
  public string OrganizationUid                               { get; set; }
  
  public string Title                                         { get; set; }
  public string Objective                                     { get; set; }

  public DateTime? CompleteBy                                 { get; set; }
  public DateTime? CompletedOn                                { get; set; }

  public string CreatedByUid                                  { get; set; }
  public ParticipantDTO CreatedBy                             { get; set; }

  public bool IsAnonymous                                     { get; set; }
  public bool IsDeleted                                       { get; set; }
  public KeyValuePair<short, string> Status                   { get; set; }
}

public abstract class BaseSurveyQuestionDTO 
{
  public int Id                               { get; set; }
   
  public int SurveyId                         { get; set; }
  public int? QuestionId                      { get; set; }
   
  public KeyValuePair<short, string> Type     { get; set; }
  public string Configuration                 { get; set; }
   
  public string RichText                      { get; set; }
  public string PlainText                     { get; set; }
   
  public short Rank                           { get; set; }
}

public class ParticipantDTO 
{
  //TODO: decide whether or not this should stay 'uid' or if it should be 'id'
  public string Uid                           { get; set; } = string.Empty;

  public string FirstName                     { get; set; } = string.Empty;
  public string LastName                      { get; set; } = string.Empty;
  public string FullName                      { get => $"{FirstName} {LastName}"; }

  public string EmailAddress                  { get; set; } = string.Empty;
  public string ProfileImageUrl               { get; set; } = string.Empty;
}

public class SurveyParticipantDTO 
{
  public int Id                               { get; set; }
  public int SurveyId                         { get; set; }
   
  public string ParticipantUid                { get; set; }
  public ParticipantDTO Participant           { get; set; }
   
  public DateTime? CompletedOn                { get; set; }
  public bool IsDeleted                       { get; set; }
}

public class QuestionDTO 
{
  public int Id                               { get; set; }
   
  public KeyValuePair<short, string> Type     { get; set; }
  public string Configuration                 { get; set; }
  
  public string RichText                      { get; set; }
  public string PlainText                     { get; set; }
  public bool IsDeleted                       { get; set; }
}

public class AnswerDTO 
{
  public int Id                               { get; set; }

  public int SurveyQuestionId                 { get; set; }
  public int SurveyParticipantId              { get; set; }
//  public ParticipantDTO Participant           { get; set; }
  
  public string Response                      { get; set; } = string.Empty;
}

public class QuestionAnswerDTO : BaseSurveyQuestionDTO 
{
  public AnswerDTO Answer { get; set; }
}

//public class SurveyDetailsDTO : BaseSurveyDTO 
//{
//  public IEnumerable<SurveyParticipantDTO> Participants                                                   { get; set; }
//  
//  public Dictionary<QuestionDTO, IEnumerable<KeyValuePair<SurveyParticipantDTO, AnswerDTO>>> ByQuestions  { get; set; }
//  
//  public Dictionary<SurveyParticipantDTO, IEnumerable<QuestionAnswerDTO>> ByParticipants                  { get; set; }
//}

#endregion

public class BaseSurvey 
{
  public int ID                                 { get; set; }
  public string Title                           { get; set; }
                                                
  public int CreatorID                          { get; set; }
  public Participant Creator                    { get; set; }  
  
  public IEnumerable<Participant> Participants  { get; set; }
}

public class Survey : BaseSurvey 
{
  public IEnumerable<QuestionAnswers> Questions { get; set; }
}

public class SurveyDetails : BaseSurvey 
{
  public IEnumerable<Question> Questions  { get; set; }
    
  public Dictionary<Question, IEnumerable<(Participant, Answer)>> ByQuestion    { get; set; }
  public Dictionary<Participant, IEnumerable<(Question, Answer)>> ByParticipant { get; set; }
//  public Dictionary<Participant, IEnumerable<QuestionAnswer>>     ByParticipant { get; set; }
}

public class Participant 
{
  public int ID           { get; set; }
  
  public string UID       { get; set; }
  public string FirstName { get; set; }
  public string LastName  { get; set; }
  public string FullName  { get => $"{this.FirstName} {this.LastName}"; }
}

public class Question 
{
  public int ID      { get; set; }
  public string Text { get; set; }
}

public class Answer 
{
  public int ID             { get; set; }

  public int QuestionID     { get; set; }
  public int ParticipantID  { get; set; }
  
  public string Text        { get; set; }
}

public class QuestionAnswer : Question
{
  public Answer Answer { get; set; }
}

public class QuestionAnswers : Question 
{
  public IEnumerable<Answer> Answers { get; set; }
}

public static class Data 
{
  public static Survey Survey 
    => new Survey { ID = 1
                   ,Title = "(RB = TEST) API Survey"
                   
                   ,CreatorID    = 14
                   ,Participants = new List<Participant> { new Participant { ID = 10
                                                                            ,UID = "0xf951eaf"
                                                                            ,FirstName = "Fred"
                                                                            ,LastName  = "Flintstone" },
                    
                                                           new Participant { ID = 11
                                                                            ,UID = "0xf937149"
                                                                            ,FirstName = "Barny"
                                                                            ,LastName  = "Rubble" },
                    
                                                           new Participant { ID = 12
                                                                            ,UID = "0xf94839c"
                                                                            ,FirstName = "Bat"
                                                                            ,LastName  = "Man" }}
                    
                   ,Questions = new List<QuestionAnswers> { new QuestionAnswers { ID = 20
                                                                                 ,Text = "On a scale from 1 to 10, how are you feeling?"
                                                                                 ,Answers = new List<Answer> { new Answer { ID = 30 
                                                                                                                           ,ParticipantID = 10
                                                                                                                           ,QuestionID = 20
                                                                                                                           ,Text = "5" },
                                                                                                               new Answer { ID = 33
                                                                                                                           ,ParticipantID = 11
                                                                                                                           ,QuestionID = 20
                                                                                                                           ,Text = "7" },
                                                                                                               new Answer { ID = 36
                                                                                                                           ,ParticipantID = 12
                                                                                                                           ,QuestionID = 20
                                                                                                                           ,Text = "3" }}},
                                                                                                                           
                                                            new QuestionAnswers { ID = 21
                                                                                 ,Text = "Which is your favorite Muppet?"
                                                                                 ,Answers = new List<Answer> { new Answer { ID = 31 
                                                                                                                           ,ParticipantID = 10
                                                                                                                           ,QuestionID = 21
                                                                                                                           ,Text = "Animal" },
                                                                                                               new Answer { ID = 34
                                                                                                                           ,ParticipantID = 11
                                                                                                                           ,QuestionID = 21
                                                                                                                           ,Text = "Scooter" },
                                                                                                               new Answer { ID = 37
                                                                                                                           ,ParticipantID = 12
                                                                                                                           ,QuestionID = 21
                                                                                                                           ,Text = "Gonzo" }}},
                                                                                                                           
                                                            new QuestionAnswers { ID = 22
                                                                                 ,Text = "Just a Freeform question here, nothing special to see here."
                                                                                 ,Answers = new List<Answer> { new Answer { ID = 32
                                                                                                                           ,ParticipantID = 10
                                                                                                                           ,QuestionID = 22
                                                                                                                           ,Text = "okaydokay ... just a freeform question" },
                                                                                                               new Answer { ID = 35
                                                                                                                           ,ParticipantID = 11
                                                                                                                           ,QuestionID = 22
                                                                                                                           ,Text = "okaydokay ... " },
                                                                                                               new Answer { ID = 38
                                                                                                                           ,ParticipantID = 12
                                                                                                                           ,QuestionID = 22
                                                                                                                           ,Text = "... just a freeform question" }}}}
                  };
  
}

public static class ExtensionMehtods 
{
  public static SurveyDetails ToSurveyDetails(this Survey survey)
  {
    SurveyDetails details = null;
    
    if(survey != null)
    {
      details = new SurveyDetails {
                       ID    = survey.ID
                      ,Title = survey.Title
                      
                      ,CreatorID    = survey.CreatorID
                      
                      ,Questions    = survey.Questions
                                            .Select(q => new Question { ID = q.ID
                                                                       ,Text = q.Text })
                      ,Participants = survey.Participants
                      
                      ,ByQuestion    = new Dictionary<Question, IEnumerable<(Participant participant, Answer answer)>>()
                      ,ByParticipant = new Dictionary<Participant, IEnumerable<(Question question, Answer answer)>>()
                    };
      
      foreach(var item in survey.Questions)
      {
        var question = new Question { ID = item.ID 
                                     ,Text = item.Text };
        
        details.ByQuestion.Add( question
                               ,new List<(Participant participant, Answer answer)>());


        var pas = new List<(Participant participant, Answer answer)>();
        
        foreach(var answer in item.Answers)
        {
          var participant = survey.Participants.Single(p => p.ID == answer.ParticipantID);
          
          pas.Add((participant, answer));                    
        }
        
        details.ByQuestion[question] = pas;
      }
    
      foreach(var item in survey.Participants)
      {
        var participant = item;
        
        details.ByParticipant.Add( participant
                                  ,new List<(Question, Answer)>());
        
        var qas = new List<(Question, Answer)>();
        
        foreach(var question in survey.Questions)
        {
          var _answer = question.Answers.SingleOrDefault(a => a.ParticipantID == participant.ID);
          var _question = new Question { ID = question.ID
                                        ,Text = question.Text };
          
          qas.Add((_question, _answer));          
        }
        
        details.ByParticipant[participant] = qas;
      }
    }
    
    return details;
  }
}