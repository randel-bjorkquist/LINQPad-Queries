<Query Kind="Program" />

void Main()
{
  var creator_uid_list = new List<List<string>>();
  creator_uid_list.Add(new List<string> {"0x001", "0x003", "0x005", "0x007", "0x009"});
  creator_uid_list.Add(new List<string> {"0x002", "0x004", "0x006", "0x008"});
  creator_uid_list.Add(new List<string> {"0x00A", "0x00B", "0x00C" });

  var surveys = BuildSurveys_Return(creator_uid_list, 0, 10)
//                  .Dump()
                  ;
  
  Console.WriteLine($"BuildSurveys_Return");
  
  foreach(var survey in surveys)
  {
    Console.WriteLine($"ID: {survey.ID} | OrgUID: {survey.OrgUID} | Title: {survey.Title}");
  }  
  
  surveys = BuildSurveys_YieldReturn(creator_uid_list, 0, 10)
//              .Dump()
              ;
  
  Console.WriteLine("");
  Console.WriteLine($"BuildSurveys_YieldReturn");
  
  foreach(var survey in surveys)
  {
    Console.WriteLine($"ID: {survey.ID} | OrgUID: {survey.OrgUID} | Title: {survey.Title}");
  }  
}

public class SurveyModel
{
  public int ID         { get; set; }
  public string OrgUID  { get; set; }
  public string Title   { get; set; }
}

public static IEnumerable<SurveyModel> BuildSurveys_YieldReturn( IEnumerable<IEnumerable<string>> creatorUids
                                                                ,int minCount = 0
                                                                ,int maxCount = 10)
{
  if(creatorUids == null || !creatorUids.Any())
  {
    yield break;
  }
  
  var count = 0;

  foreach(var uidList in creatorUids)
  {
    string orgUid = $"0x{count}";
    
    foreach(var uid in uidList)
    {
      yield return new SurveyModel{ ID     = count
                                   ,OrgUID = $"UID: {count}"
                                   ,Title  = $"Title: {count}" };
    }
    
    count++;
  }
}


public static IEnumerable<SurveyModel> BuildSurveys_Return( IEnumerable<IEnumerable<string>> creatorUids
                                                           ,int minCount = 0
                                                           ,int maxCount = 10)
{
  if(creatorUids == null || !creatorUids.Any())
  {
    return Enumerable.Empty<SurveyModel>();
  }

  var surveys = new List<SurveyModel>();
  var count   = 0;

  foreach(var uidList in creatorUids)
  {
    string orgUid = $"0x{count}";
    
    foreach(var uid in uidList)
    {
      surveys.Add( new SurveyModel{ ID     = count
                                   ,OrgUID = $"UID: {count}"
                                   ,Title  = $"Title: {count}" });
    }
    
    count++;
  }
  
  return surveys;
}
