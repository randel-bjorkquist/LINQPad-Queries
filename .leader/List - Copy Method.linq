<Query Kind="Program" />

void Main()
{
  var org_names = new List<string> { "Disneyland (America/New_York)"
                                    ,"Wendy's Old Fashion Hamburgers"
                                    ,"Subway" };
  
  var questions = Data.Questions;
  questions.Dump("questions - before", 0);
  
  foreach(var org_name in org_names)
  {
    var template_questions = questions.Copy()
                                      .ToList();
    
    template_questions.Dump("template_questions", 1);
    
    for(int index = 0; index < template_questions.Count(); index++)
    {
      var question = template_questions.ElementAt(index);
      question.Question = question.Question.Replace("{OrganizationName}", org_name);
    }

    template_questions.Single(tq => tq.Category == QuestionCategoryEnum.eNPS)
                      .Question
                      .Dump($"{org_name} => {QuestionCategoryEnum.eNPS}");
    
//    template_questions.GroupBy(tq => tq.Category)
//                      .ToDictionary(grp => grp.Key,
//                                    grp => grp)                                    
//                      .Dump("Dictionary");
  }
  
  questions.Dump("questions - after", 0);
}

public static class Data
{
  public static IEnumerable<QuestionModel> Questions
    => new List<QuestionModel> {
             new QuestionModel{
                   Id = 5
                  ,TemplateId = 1
                  ,Category = QuestionCategoryEnum.Clarity
                  ,Type     = QuestionTypeEnum.MultipleChoice
                  ,Question = "The work I do every day directly contributes to the success of our team."
                 },
             new QuestionModel{
                   Id = 7
                  ,TemplateId = 1
                  ,Category = QuestionCategoryEnum.eNPS
                  ,Type     = QuestionTypeEnum.CustomScale
                  ,Question = "How likely are you to recommend {OrganizationName} as a place to work?"
                 },
             new QuestionModel{
                   Id = 10
                  ,TemplateId = 1
                  ,Category = QuestionCategoryEnum.Maximization
                  ,Type     = QuestionTypeEnum.MultipleChoice
                  ,Question = "I am regularly recognized and appreciated for the work I do."
                 },
             new QuestionModel{
                   Id = 17
                  ,TemplateId = 1
                  ,Category = QuestionCategoryEnum.Rapport
                  ,Type     = QuestionTypeEnum.MultipleChoice
                  ,Question = "I am comfortable giving honest feedback to my team."
                 }
           };
}

public static class ExtensionMethods
{
  public static QuestionModel Copy(this QuestionModel oldModel)
  {
    QuestionModel newModel = null;
     
    if(oldModel != null)
    {
      newModel = new QuestionModel
                     {
                         Id         = oldModel.Id,
                         TemplateId = oldModel.TemplateId,
                         Category   = (QuestionCategoryEnum)oldModel.Category,
                         
                         Type = (QuestionTypeEnum)oldModel.Type,
                         Configuration = oldModel.Configuration,
                          
                         Description = oldModel.Description,
                         Question    = oldModel.Question,
                         
                         CreatedByUid  = oldModel.CreatedByUid,
                         CreatedOn     = oldModel.CreatedOn,
                         
                         ModifiedByUid = oldModel.ModifiedByUid,
                         ModifiedOn    = oldModel.ModifiedOn,
                         
                         DeletedByUid  = oldModel.DeletedByUid,
                         DeletedOn     = oldModel.DeletedOn
                     };
    }
     
    return newModel;
  }

  public static IEnumerable<QuestionModel> Copy(this IEnumerable<QuestionModel> oldModels)
  {
    var newModels = oldModels?.Select(model => model.Copy());
     
    return newModels ?? Enumerable.Empty<QuestionModel>();
  }  
}

public interface IModel
{
    int Id { get; set; }
}

public interface IDeletable : IModel
{
    /// <summary>The UID of the user that deleted the record.</summary>
    string DeletedByUid { get; set; }
    
    /// <summary>The date that the record was deleted.</summary>
    DateTime? DeletedOn { get; set; }
}

public interface IEntity : IDeletable
{
    /// <summary>The UID of the user that created this object.</summary>
    string CreatedByUid                 { get; set; }

    /// <summary>The date that this object was created.</summary>
    DateTime CreatedOn                  { get; set; }

    /// <summary>The Uid of the user that last modified this object.</summary>
    string ModifiedByUid                  { get; set; }

    /// <summary>The date that this object was last modified.</summary>
    DateTime? ModifiedOn                { get; set; }
}

public class QuestionModel : Entity
{
    public int TemplateId                           { get; set; }
    public QuestionCategoryEnum Category            { get; set; }
    
    public QuestionTypeEnum Type                    { get; set; }
    public string Configuration                     { get; set; }
    
    public string Description                       { get; set; }
    public string Question                          { get; set; }
}

public abstract class Entity : DeletableEntity, IEntity
{
    public string CreatedByUid              { get; set; }
    public DateTime CreatedOn               { get; set; }

    public string ModifiedByUid             { get; set; }
    public DateTime? ModifiedOn             { get; set; }
}

public abstract class DeletableEntity : Model, IDeletable
{
    public bool IsDeleted => DeletedOn.HasValue;

    /// <summary>The Uid of the user that deleted the record.</summary>
    public string DeletedByUid { get; set; }

    /// <summary>The date that the record was deleted.</summary>
    public DateTime? DeletedOn { get; set; }
}

public abstract class Model : IModel
{
    /// <summary>The primary key or identity column.</summary>
    public int Id { get; set; }

    public bool IsNew => Id.Equals(0);
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
    //new_category    = 32,
    //new_category    = 64,
}

public enum QuestionTypeEnum : short
{
    Unknown         = 0,
    CustomScale     = 1,
    ShortAnswer     = 2,
    MultipleChoice  = 4
}
