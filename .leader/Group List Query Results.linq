<Query Kind="Program" />

//resource => https://stackoverflow.com/questions/40017880/convert-row-values-into-columns-using-linq-in-c-sharp
//middle answer by => 'ItamarG3'
static void Main()
{
  List<temp> list = new List<temp> { new temp {PillarID = 1, Quarter= "Q12106", Feature = "France"     }
                                    ,new temp {PillarID = 1, Quarter= "Q12106", Feature = "Germany"    }
                                    ,new temp {PillarID = 1, Quarter= "Q22016", Feature = "Italy"      }
                                    ,new temp {PillarID = 1, Quarter= "Q32016", Feature = "Russia"     }
                                    ,new temp {PillarID = 2, Quarter= "Q22016", Feature = "India"      }
                                    ,new temp {PillarID = 2, Quarter= "Q32016", Feature = "USA"        }
                                    ,new temp {PillarID = 3, Quarter= "Q22016", Feature = "China"      }
                                    ,new temp {PillarID = 3, Quarter= "Q32016", Feature = "Australia"  }
                                    ,new temp {PillarID = 3, Quarter= "Q32016", Feature = "New Zeland" }
                                    ,new temp {PillarID = 3, Quarter= "Q42016", Feature = "Japan"      }};

  list.Dump("list", 0);                                    
                                    
  IEnumerable<IGrouping<string, temp>> byQuarter = list.GroupBy(x => x.Quarter);  
  byQuarter.Dump("byQuarter", 0);
  
  list.GroupBy(t => t.Quarter)
      .Select(t => (t.Key, t.Select(s => (s.PillarID, s.Feature))))
      .Dump("list.GroupBy(t => t.Quarter)", 0);
}

public struct temp
{
  public int    PillarID;
  public string Quarter;
  public string Feature;
}
