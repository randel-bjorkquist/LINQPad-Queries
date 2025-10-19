<Query Kind="Program" />

void Main()
{
  
}

// You can define other methods, fields, classes and namespaces here

public static class DTOExtensions
{
        
//  public static QuestionAnswersDTO ToDTO(this SurveyQuestionModel model)
//  {
//    QuestionAnswersDTO dto = null;
//     
//    if(model != null)
//    {
//      dto = new QuestionAnswersDTO
//                {
//                  Id       = model.Id,                              
//                  SurveyId = model.SurveyId,
//                  
//                  QuestionId = model?.QuestionId,
//
//                  //Converting an enum to kvp is meant to help both frontend and backend developers debug
//                  Type          = new KeyValuePair<short, string>((short)model.Type, model.Type.ToString()),
//                  Configuration = model.Configuration,
//                   
//                  RichText  = model.RichText,
//                  PlainText = model.PlainText,
//                  
//                  Rank    = model.Rank,
//                  Answers = model.Answers.ToDTOs()
//                };
//    }
//     
//    return dto;
//  }
//
//  public static IEnumerable<QuestionAnswersDTO> ToDTOs(this IEnumerable<SurveyQuestionModel> models)
//  {
//    var dtos = models?.Select(model => model.ToDTO());
//       
//    return dtos ?? Enumerable.Empty<QuestionAnswersDTO>();
//  }  
  
  public static QuestionAnswerDTO ToDTO(this SurveyQuestionModel model)
  {
    QuestionAnswerDTO dto = null;

    if (model != null)
    {
      dto = new QuestionAnswerDTO
                {
                  Id = model.Id,
                  
                  SurveyId   = model.SurveyId,
                  QuestionId = model?.QuestionId,

                  //Converting an enum to kvp is meant to help both frontend and backend developers debug
                  Type = new KeyValuePair<short, string>((short)model.Type, model.Type.ToString()),
                  Configuration = model.Configuration,

                  RichText = model.RichText,
                  PlainText = model.PlainText,

                  Rank = model.Rank,
                  Answer = model.Answers
                                .SingleOrDefault()
                                .ToDTO()
                };
    }

    return dto;
  }

  public static IEnumerable<QuestionAnswerDTO> ToDTOs(this IEnumerable<SurveyQuestionModel> models)
  {
    var dtos = models?.Select(model => model.ToDTO());

    return dtos ?? Enumerable.Empty<QuestionAnswerDTO>();
  }

//  public static T ToDTO<T>(this SurveyQuestionModel model) where T : QuestionAnswerDTO
//  {
//    T dto = null;
//
//    if (model != null)
//    {
//      dto = new QuestionAnswerDTO();
//    }
//
//    return dto;
//  }
//
//  public static IEnumerable<T> ToDTOs<T>(this IEnumerable<SurveyQuestionModel> models) where T : QuestionAnswerDTO
//  {
//    var dtos = models?.Select(model => model.ToDTO<T>());
//
//    return dtos ?? Enumerable.Empty<T>();
//  }

  public static AnswerDTO ToDTO(this AnswerModel model)
  {
    AnswerDTO dto = null;
     
    if(model != null)
    {
      dto = new AnswerDTO
                {
                    Id = model.Id,
                     
                    SurveyQuestionId    = model.SurveyQuestionId,
                    SurveyParticipantId = model.SurveyParticipantId,
                     
                    Response = model.Response
                };
    }
     
    return dto;
  }

  public static IEnumerable<AnswerDTO> ToDTOs(this IEnumerable<AnswerModel> models)
  {
    var dtos = models?.Select(model => model.ToDTO());
     
    return dtos ?? Enumerable.Empty<AnswerDTO>();
  }
}

#region DTO(s)

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

public class QuestionAnswersDTO : BaseSurveyQuestionDTO 
{
  public IEnumerable<AnswerDTO> Answers { get; set; }
}

public class QuestionAnswerDTO : BaseSurveyQuestionDTO 
{
  public AnswerDTO Answer { get; set; }
}

public class AnswerDTO 
{
  public int Id                               { get; set; }

  public int SurveyQuestionId                 { get; set; }
  public int SurveyParticipantId              { get; set; }
  
  public string Response                      { get; set; } = string.Empty;
}

#endregion 

#region Model(s)

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

public class AnswerModel 
{
  public int Id                                   { get; set; }
  
  public int SurveyQuestionId                     { get; set; }
  public int SurveyParticipantId                  { get; set; }
  
  public string Response                          { get; set; } = string.Empty;
}

public enum QuestionTypeEnum : short 
{
  Unknown         = 0,
  CustomScale     = 1,
  Freeform        = 2,
  MultipleChoice  = 4
}

#endregion