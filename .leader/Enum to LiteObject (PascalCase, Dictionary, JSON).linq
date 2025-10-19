<Query Kind="Program">
  <NuGetReference>Microsoft.AspNetCore.Mvc.NewtonsoftJson</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
  var _service = new SysTypeService();
  var systypes = new Dictionary<string, Dictionary<string, LiteObject>>
                     { { "QuestionCategories" ,_service.QuestionCategories() }
                      ,{ "QuestionTypes"      ,_service.QuestionTypes()      }
                      ,{ "SurveyStatuses"     ,_service.SurveyStatuses()     }
                      ,{ "TemplateTypes"      ,_service.TemplateTypes()      }
                     }.Dump("SysTypes", 0);
  
  //JsonConvert.SerializeObject(systypes).Dump("SysTypes");
}

[Flags]
public enum QuestionCategoryEnum : short
{
  Unknown         =  0
 ,Clarity         =  1
 ,Custom          =  2
 ,eNPS            =  4
 ,Maximization    =  8
 ,Rapport         = 16
// ,new_category    = 32
// ,new_category    = 64
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
  Unknown         =  0
 ,Curated         =  1
 ,Custom          =  2
 ,Pulse           =  4
 //,new_type        =  8
 //,new_type        = 16
}

public class LiteObject
{
  public int Id                       { get; set; }
  public string Code                  { get; set; }
  public string Description           { get; set; }
}

public interface ISysTypeService
{
  Dictionary<string, LiteObject> QuestionCategories();
  Dictionary<string, LiteObject> QuestionTypes();
  Dictionary<string, LiteObject> SurveyStatuses();
  Dictionary<string, LiteObject> TemplateTypes();
}

public class SysTypeService : ISysTypeService
{
  public Dictionary<string, LiteObject> QuestionCategories()
  {
    #region Enums w/Flag Attribute
    
    var count = 0;
    var systypes = new Dictionary<string, LiteObject>();
    
    foreach(var e in Enum.GetValues(typeof(QuestionCategoryEnum)))
    {
      count += (short)e;
    }
    
    for(int value = 1; value <= count; value++)
    {
      var systype = new LiteObject { Id = value
                                    ,Code = ((QuestionCategoryEnum)value).ToString()
                                    ,Description = Regex.Replace(((QuestionCategoryEnum)value).ToString(), "(\\B[A-Z])", " $1").ToString() };
      
      systypes.Add(((QuestionCategoryEnum)value).ToString()
                                                .Replace(", ", "_")
                   ,systype);
    }
    
    foreach(var systype in systypes)
    {
      
      //if(systype.Value.Code.Contains("eNPS"))
      //{
      //  systype.Value.Description = systype.Value.Description.Replace("e N P S", "eNPS");
      //}
      
      //if(systype.Value.Code.Contains(QuestionCategoryEnum.eNPS.ToString()))
      if(systype.Value.Code.Contains("eNPS"))
      {
        systype.Value.Description = systype.Value.Description.Replace( Regex.Replace((QuestionCategoryEnum.eNPS).ToString(), "(\\B[A-Z])", " $1").ToString()
                                                                      ,QuestionCategoryEnum.eNPS.ToString());
      }
    }
    
    return systypes;
    
    #endregion
    
    #region COMMENTED OUT: Enums w/o the Flags Attribute
    //
    //var systypes = Enum.GetValues(typeof(QuestionCategoryEnum))
    //                   .Cast<QuestionCategoryEnum>()
    //                   .Select(qqe => new LiteObject { Id = (int)qqe,
    //                                                   Code = qqe.ToString(),
    //                                                   Description = Regex.Replace(qqe.ToString(), "(\\B[A-Z])", " $1").ToString() })
    //                   .Where(sys => sys.Id > 0)
    //                   .GroupBy(sys => sys.Code)
    //                   .ToDictionary(sys => sys.Key,
    //                                 sys => sys.Single());
    //
    ////Replaces the descripton 'e N P S' (created by the above Regex) with 'eNPS'
    //var systype = systypes.Single(sys => sys.Value.Id == (short)QuestionCategoryEnum.eNPS);
    //systype.Value.Description = QuestionCategoryEnum.eNPS.ToString();
    //
    //return systypes;
    //
    #endregion
  }       
  
  public Dictionary<string, LiteObject> QuestionTypes()
  {
    var systypes = Enum.GetValues(typeof(QuestionTypeEnum))
                       .Cast<QuestionTypeEnum>()
                       .Select(qte => new LiteObject { Id = (int)qte,
                                                       Code = qte.ToString(),
                                                       Description = Regex.Replace(qte.ToString(), "(\\B[A-Z])", " $1").ToString() })
                       .Where(sys => sys.Id > 0)
                       .GroupBy(sys => sys.Code)
                       .ToDictionary(sys => sys.Key,
                                     sys => sys.Single());
    
    return systypes;    
    
    #region Undesirable UI Option
    //
    //COMMENT: I REALLY DON'T like this; but UI wants 'Ranking' instead of "Custom Scale" 
    //and I really don't want to deal with what UI wants in the backend
    //
    //var systypes = Enum.GetValues(typeof(QuestionTypeEnum))
    //                   .Cast<QuestionTypeEnum>()
    //                   .Select(qte => new LiteObject { Id = (int)qte,
    //                                                   Code = qte.ToString(),
    //                                                   Description = Regex.Replace(qte.ToString(), "(\\B[A-Z])", " $1").ToString() })
    //                   .Where(sys => sys.Id > 0)
    //                   .ToList();
    //                   
    //// systypes.Where(s => s.Id == 1)
    ////         .ToList()
    ////         .ForEach(s => s.Description = "Rank");
    //var systype = systypes.Single(s => s.Id == (short)QuestionTypeEnum.CustomScale);
    //systype.Description = "Ranking";    
    //
    //return systypes;
    //
    #endregion    
  }
  
  public Dictionary<string, LiteObject> SurveyStatuses()
  {
    var systypes = Enum.GetValues(typeof(SurveyStatusEnum))
                       .Cast<SurveyStatusEnum>()
                       .Select(qte => new LiteObject { Id = (int)qte,
                                                       Code = qte.ToString(),
                                                       Description = Regex.Replace(qte.ToString(), "(\\B[A-Z])", " $1").ToString() })
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
      var e = (TemplateTypeEnum)value;
      
      //ONLY RETURN (for now): 'Curated', 'Custom', and 'Curated_Pulse'
      if(value == 1 || value == 2 || value == 5)
      {
        var systype = new LiteObject { Id = value
                                      ,Code = ((TemplateTypeEnum)value).ToString()
                                      ,Description = Regex.Replace(((TemplateTypeEnum)value).ToString(), "(\\B[A-Z])", " $1").ToString() };
        
        systypes.Add(((TemplateTypeEnum)value).ToString()
                                              .Replace(", ", "_")
                     ,systype);
      }
    }
    
    return systypes;
    
    #endregion
    
    #region COMMENTED OUT: Enums w/o the Flags Attribute
    //
    //var systypes = Enum.GetValues(typeof(TemplateTypeEnum))
    //                   .Cast<QuestionTypeEnum>()
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
      
  public IEnumerable<LiteObject> QuestionTypes(bool depricated)
  {
    return Enum.GetValues(typeof(QuestionTypeEnum))
               .Cast<QuestionTypeEnum>()
               .Select(qte => new LiteObject { Id = (int)qte,
                                               Code = qte.ToString(),
                                               Description = Regex.Replace(qte.ToString(), "(\\B[A-Z])", " $1").ToString() })
               .Where(sys => sys.Id > 0)
               .ToList();
               
    #region Undesirable UI Option
    //
    //COMMENT: I REALLY DON'T like this; but UI wants 'Ranking' instead of "Custom Scale" 
    //and I really don't want to deal with what UI wants in the backend
    //
    //var systypes = Enum.GetValues(typeof(QuestionTypeEnum))
    //                   .Cast<QuestionTypeEnum>()
    //                   .Select(qte => new LiteObject { Id = (int)qte,
    //                                                   Code = qte.ToString(),
    //                                                   Description = Regex.Replace(qte.ToString(), "(\\B[A-Z])", " $1").ToString() })
    //                   .Where(sys => sys.Id > 0)
    //                   .ToList();
    //                   
    //// systypes.Where(s => s.Id == 1)
    ////         .ToList()
    ////         .ForEach(s => s.Description = "Rank");
    //var systype = systypes.Single(s => s.Id == (short)QuestionTypeEnum.CustomScale);
    //systype.Description = "Ranking";    
    //
    //return systypes;
    //
    #endregion
  }

  public IEnumerable<LiteObject> SurveyStatuses(bool depricated)
  {
    return Enum.GetValues(typeof(SurveyStatusEnum))
               .Cast<SurveyStatusEnum>()
               .Select(sse => new LiteObject { Id = (int)sse,
                                               Code = sse.ToString(),
                                               Description = Regex.Replace(sse.ToString(), "(\\B[A-Z])", " $1").ToString() })
               .Where(sys => sys.Id > 0)
               .ToList();
  }
  
  #region COMMENTED OUT: R&D CODE
  //
  //protected Dictionary<string, LiteObject> SysTypes<T>() //where T : enum
  //{
  //  var systypes = Enum.GetValues(typeof(T))
  //                     .Cast<T>()
  //                     .Select(t => new LiteObject { Id = (int)t,
  //                                                   //Int32.TryParse(t, this.Id),
  //                                                   Code = t.ToString(),
  //                                                   Description = Regex.Replace(t.ToString(), "(\\B[A-Z])", " $1").ToString() })
  //                     .Where(sys => sys.Id > 0)
  //                     .GroupBy(sys => sys.Code)
  //                     .ToDictionary(sys => sys.Key,
  //                                   sys => sys.Single());
  //                     
  //  return systypes;
  //}
  //
  //protected Dictionary<string, LiteObject> SysTypes<T>() where T : struct//, IConvertible
  //{
  //  var count = 0;
  //
  //  foreach (var e in Enum.GetValues(typeof(T)))
  //  {
  //    count += (short)e;
  //  }
  //
  //  var systypes = new Dictionary<string, LiteObject>();
  //  
  //  for(int value = 0; value <= count; value++)
  //  {
  //    systypes.Add( typeof(T).ToString()
  //                 ,new LiteObject{ Id  = value
  //                                 
  //                                  ,Code = ((T)Enum. //)value).ToString()
  //                                 // ,Code = (T)Enum.Parse(typeof(T), value).ToString()
  //                                 //,Code = ((T)value.ToString() ) 
  //                                 
  //                                });
  //
  //    ($"{value,3} - {value.ToString()}").Dump("(T)value");
  //  }
  //  
  //  return null;
  //}
  //
  #endregion
}
