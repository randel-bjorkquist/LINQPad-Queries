<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
  //data.ParseToList<Departments>().Dump();
  data.ParseToList<Department>().Dump("data.ParseToList<Department>()", 0);
  //data.ParseToList<Team>().Dump();
  //data.ParseToList<AccountType>().Dump();

  //data.Parse<Application>().Dump();
  //data.Parse<Status>().Dump();
  //
  //JsonConvert.DeserializeObject<Status>(data).Dump();
  //JsonConvert.DeserializeObject(data).Dump();
  //
  //var status = new Status {success =  true};
  //
  //JsonConvert.SerializeObject(status).Dump();
  
  var depts = new List<Department> { new Department { value = "blah"
                                                     ,total = 1 }
                                    ,new Department { value = "blah blah"
                                                     ,total = 2 }
                                    ,new Department { value = "boo"
                                                     ,total = 4 }
                                    ,new Department { value = "boo hoo"
                                                     ,total = 8 }};

  var teams = new List<Team> { new Team { teamId = "0x01"
                                         ,name = "Team - 0x01"}
                              ,new Team { teamId = "0x02"
                                         ,name = "Team - 0x02"}
                              ,new Team { teamId = "0x03"
                                         ,name = "Team - 0x03"}};

  var types = new List<AccountType> { new AccountType{ typeId = "0xA1"
                                                      ,name   = "Type = 1"
                                                      ,total  = 11 }
                                     ,new AccountType{ typeId = "0xA2"
                                                      ,name   = "Type = 2"
                                                      ,total  = 22 }
                                     ,new AccountType{ typeId = "0xA3"
                                                      ,name   = "Type = 3"
                                                      ,total  = 33 }};

  var status = new Status {success = true};
  
  var app = new Application { data = new Data { departments = depts
                                               ,teams = teams
                                               ,accountTypes = types}
                             ,status = status};

  JsonConvert.SerializeObject(app).Dump("app");
}

//public string data = "{\"data\":{\"departments\":[{\"value\":\"IT\",\"total\":3},{\"value\":\"QA SV\",\"total\":3},{\"value\":\"No Department\",\"total\":82},{\"value\":\"Engineer\",\"total\":1},{\"value\":\"Board\",\"total\":1},{\"value\":\"Department of Huge Inconveninces\",\"total\":2},{\"value\":\"Head of Optimism\",\"total\":1},{\"value\":\"Yes\",\"total\":1},{\"value\":\"0101010100/1001001\",\"total\":1},{\"value\":\"RPGs\",\"total\":1},{\"value\":\"Engineering/Product\",\"total\":3},{\"value\":\"Product\",\"total\":6},{\"value\":\"Test\",\"total\":5},{\"value\":\"aaa\",\"total\":1},{\"value\":\"QA\",\"total\":34},{\"value\":\"Team\",\"total\":1},{\"value\":\"Engineering\",\"total\":4},{\"value\":\"Marketing\",\"total\":1},{\"value\":\"Egg.\",\"total\":1},{\"value\":\"Leadr\",\"total\":2},{\"value\":\"GSM\",\"total\":1},{\"value\":\"Worship\",\"total\":2},{\"value\":\"Pastor\",\"total\":3},{\"value\":\"Maintenance\",\"total\":1},{\"value\":\"Communications\",\"total\":1},{\"value\":\"Grace Kids\",\"total\":2},{\"value\":\"Development\",\"total\":2},{\"value\":\"Management and test\",\"total\":1},{\"value\":\"QA Type\",\"total\":1},{\"value\":\"qa\",\"total\":2},{\"value\":\"Department\",\"total\":4},{\"value\":\"Widgets, Gidgets, and other stuff\",\"total\":1},{\"value\":\"Labor\",\"total\":1},{\"value\":\"SV TEST DEPT.\",\"total\":3},{\"value\":\"Electronics\",\"total\":5},{\"value\":\"Edu\",\"total\":1},{\"value\":\"Peony\",\"total\":1},{\"value\":\"Testing\",\"total\":1},{\"value\":\"CS\",\"total\":10},{\"value\":\"Technical Department Technical Department Technica\",\"total\":1},{\"value\":\"Man\",\"total\":1},{\"value\":\"Email-land\",\"total\":1},{\"value\":\"Metrics\",\"total\":1},{\"value\":\"Department X\",\"total\":1},{\"value\":\"Development Testing\",\"total\":5},{\"value\":\"Infra\",\"total\":1},{\"value\":\"Storage\",\"total\":1},{\"value\":\"Compartmentalized\",\"total\":1},{\"value\":\"Prod\",\"total\":1},{\"value\":\"WS\",\"total\":1},{\"value\":\"QAA\",\"total\":1},{\"value\":\"Game\",\"total\":1},{\"value\":\"Rocket\",\"total\":1},{\"value\":\"Automation Guild\",\"total\":1},{\"value\":\"The Automatic Sledgehammer dept\",\"total\":1},{\"value\":\"Beans\",\"total\":2},{\"value\":\"Code Warehouse\",\"total\":2},{\"value\":\"Nodes\",\"total\":1}],\"teams\":[{\"name\":\"New Test group\",\"teamId\":\"0x9800348\"},{\"name\":\"Test group with description\",\"teamId\":\"0x98005e8\"},{\"name\":\"1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890\",\"teamId\":\"0x98005e9\"},{\"name\":\"1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 \",\"teamId\":\"0x98005ed\"},{\"name\":\"Test group - description is long and has no spaces\",\"teamId\":\"0x98005ee\"},{\"name\":\"Test - ultra long description, spaces\",\"teamId\":\"0x98005ef\",\"total\":1},{\"name\":\"           test\",\"teamId\":\"0x980066f\"},{\"name\":\" test\",\"teamId\":\"0x9800670\"},{\"name\":\",./;'[]\\-=\",\"teamId\":\"0x9800671\"},{\"name\":\"Test group with existing members\",\"teamId\":\"0x9800674\"},{\"name\":\"Test Group - existing members\",\"teamId\":\"0x9800680\"},{\"name\":\"Test autorefresh group\",\"teamId\":\"0x98006d4\"},{\"name\":\"Group with a large number of users\",\"teamId\":\"0x98006dd\",\"total\":78},{\"name\":\"Test of group with invited user\",\"teamId\":\"0x98006e8\"},{\"name\":\"Jordan team member group\",\"teamId\":\"0x98006e9\",\"total\":1},{\"name\":\"Test group 5157\",\"teamId\":\"0x9800d4e\"},{\"name\":\"Test group 5157A\",\"teamId\":\"0x9800d4f\"},{\"name\":\"JF Direct reports\",\"teamId\":\"0x9801094\"},{\"name\":\"Group for add / remove member testing\",\"teamId\":\"0x98010a9\",\"total\":6},{\"name\":\"Test Group - for line number thingy\",\"teamId\":\"0x98010bc\"},{\"name\":\"Group 2 for add / remove user testing\",\"teamId\":\"0x9801243\",\"total\":6},{\"name\":\"Tyler's Test For Understanding\",\"teamId\":\"0x9801249\",\"total\":1},{\"name\":\"Group 3 for adding / removing users\",\"teamId\":\"0x980127b\",\"total\":6},{\"name\":\"Kaly's Test Group\",\"teamId\":\"0x98012e1\",\"total\":6},{\"name\":\"9876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210\",\"teamId\":\"0x98016bf\"},{\"name\":\"9876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210\",\"teamId\":\"0x980180d\"},{\"name\":\"New Test Group\",\"teamId\":\"0x9803815\"},{\"name\":\"Group without mandatory description\",\"teamId\":\"0x9803992\"},{\"name\":\"Group with over 100 users\",\"teamId\":\"0x98039b7\",\"total\":222},{\"name\":\"Test duplicate group name\",\"teamId\":\"0x9804115\"},{\"name\":\"Test Duplicate Group Name 1\",\"teamId\":\"0x9804116\"}],\"accountTypes\":[{\"name\":\"Admin\",\"total\":55,\"typeId\":\"0x83244c\"},{\"name\":\"Executive\",\"total\":21,\"typeId\":\"0x83244d\"},{\"name\":\"Limited Access\",\"total\":12,\"typeId\":\"0x83244e\"},{\"name\":\"Member\",\"total\":134,\"typeId\":\"0x832450\"}]},\"status\":{\"success\":true}}";

//public string data = "{\"accountTypes\":[{\"name\":\"Admin\",\"total\":55,\"typeId\":\"0x83244c\"},{\"name\":\"Executive\",\"total\":21,\"typeId\":\"0x83244d\"},{\"name\":\"Limited Access\",\"total\":12,\"typeId\":\"0x83244e\"},{\"name\":\"Member\",\"total\":134,\"typeId\":\"0x832450\"}]}";

//public string data = "{\"departments\":[{\"value\":\"IT\",\"total\":3},{\"value\":\"QA SV\",\"total\":3},{\"value\":\"No Department\",\"total\":82},{\"value\":\"Engineer\",\"total\":1},{\"value\":\"Board\",\"total\":1},{\"value\":\"Department of Huge Inconveninces\",\"total\":2},{\"value\":\"Head of Optimism\",\"total\":1},{\"value\":\"Yes\",\"total\":1},{\"value\":\"0101010100/1001001\",\"total\":1},{\"value\":\"RPGs\",\"total\":1},{\"value\":\"Engineering/Product\",\"total\":3},{\"value\":\"Product\",\"total\":6},{\"value\":\"Test\",\"total\":5},{\"value\":\"aaa\",\"total\":1},{\"value\":\"QA\",\"total\":34},{\"value\":\"Team\",\"total\":1},{\"value\":\"Engineering\",\"total\":4},{\"value\":\"Marketing\",\"total\":1},{\"value\":\"Egg.\",\"total\":1},{\"value\":\"Leadr\",\"total\":2},{\"value\":\"GSM\",\"total\":1},{\"value\":\"Worship\",\"total\":2},{\"value\":\"Pastor\",\"total\":3},{\"value\":\"Maintenance\",\"total\":1},{\"value\":\"Communications\",\"total\":1},{\"value\":\"Grace Kids\",\"total\":2},{\"value\":\"Development\",\"total\":2},{\"value\":\"Management and test\",\"total\":1},{\"value\":\"QA Type\",\"total\":1},{\"value\":\"qa\",\"total\":2},{\"value\":\"Department\",\"total\":4},{\"value\":\"Widgets, Gidgets, and other stuff\",\"total\":1},{\"value\":\"Labor\",\"total\":1},{\"value\":\"SV TEST DEPT.\",\"total\":3},{\"value\":\"Electronics\",\"total\":5},{\"value\":\"Edu\",\"total\":1},{\"value\":\"Peony\",\"total\":1},{\"value\":\"Testing\",\"total\":1},{\"value\":\"CS\",\"total\":10},{\"value\":\"Technical Department Technical Department Technica\",\"total\":1},{\"value\":\"Man\",\"total\":1},{\"value\":\"Email-land\",\"total\":1},{\"value\":\"Metrics\",\"total\":1},{\"value\":\"Department X\",\"total\":1},{\"value\":\"Development Testing\",\"total\":5},{\"value\":\"Infra\",\"total\":1},{\"value\":\"Storage\",\"total\":1},{\"value\":\"Compartmentalized\",\"total\":1},{\"value\":\"Prod\",\"total\":1},{\"value\":\"WS\",\"total\":1},{\"value\":\"QAA\",\"total\":1},{\"value\":\"Game\",\"total\":1},{\"value\":\"Rocket\",\"total\":1},{\"value\":\"Automation Guild\",\"total\":1},{\"value\":\"The Automatic Sledgehammer dept\",\"total\":1},{\"value\":\"Beans\",\"total\":2},{\"value\":\"Code Warehouse\",\"total\":2},{\"value\":\"Nodes\",\"total\":1}]}";
public string data = "[{\"value\":\"IT\",\"total\":3},{\"value\":\"QA SV\",\"total\":3},{\"value\":\"No Department\",\"total\":82},{\"value\":\"Engineer\",\"total\":1},{\"value\":\"Board\",\"total\":1},{\"value\":\"Department of Huge Inconveninces\",\"total\":2},{\"value\":\"Head of Optimism\",\"total\":1},{\"value\":\"Yes\",\"total\":1},{\"value\":\"0101010100/1001001\",\"total\":1},{\"value\":\"RPGs\",\"total\":1},{\"value\":\"Engineering/Product\",\"total\":3},{\"value\":\"Product\",\"total\":6},{\"value\":\"Test\",\"total\":5},{\"value\":\"aaa\",\"total\":1},{\"value\":\"QA\",\"total\":34},{\"value\":\"Team\",\"total\":1},{\"value\":\"Engineering\",\"total\":4},{\"value\":\"Marketing\",\"total\":1},{\"value\":\"Egg.\",\"total\":1},{\"value\":\"Leadr\",\"total\":2},{\"value\":\"GSM\",\"total\":1},{\"value\":\"Worship\",\"total\":2},{\"value\":\"Pastor\",\"total\":3},{\"value\":\"Maintenance\",\"total\":1},{\"value\":\"Communications\",\"total\":1},{\"value\":\"Grace Kids\",\"total\":2},{\"value\":\"Development\",\"total\":2},{\"value\":\"Management and test\",\"total\":1},{\"value\":\"QA Type\",\"total\":1},{\"value\":\"qa\",\"total\":2},{\"value\":\"Department\",\"total\":4},{\"value\":\"Widgets, Gidgets, and other stuff\",\"total\":1},{\"value\":\"Labor\",\"total\":1},{\"value\":\"SV TEST DEPT.\",\"total\":3},{\"value\":\"Electronics\",\"total\":5},{\"value\":\"Edu\",\"total\":1},{\"value\":\"Peony\",\"total\":1},{\"value\":\"Testing\",\"total\":1},{\"value\":\"CS\",\"total\":10},{\"value\":\"Technical Department Technical Department Technica\",\"total\":1},{\"value\":\"Man\",\"total\":1},{\"value\":\"Email-land\",\"total\":1},{\"value\":\"Metrics\",\"total\":1},{\"value\":\"Department X\",\"total\":1},{\"value\":\"Development Testing\",\"total\":5},{\"value\":\"Infra\",\"total\":1},{\"value\":\"Storage\",\"total\":1},{\"value\":\"Compartmentalized\",\"total\":1},{\"value\":\"Prod\",\"total\":1},{\"value\":\"WS\",\"total\":1},{\"value\":\"QAA\",\"total\":1},{\"value\":\"Game\",\"total\":1},{\"value\":\"Rocket\",\"total\":1},{\"value\":\"Automation Guild\",\"total\":1},{\"value\":\"The Automatic Sledgehammer dept\",\"total\":1},{\"value\":\"Beans\",\"total\":2},{\"value\":\"Code Warehouse\",\"total\":2},{\"value\":\"Nodes\",\"total\":1}]";

//public string data = "{\"teams\":[{\"name\":\"New Test group\",\"teamId\":\"0x9800348\"},{\"name\":\"Test group with description\",\"teamId\":\"0x98005e8\"},{\"name\":\"1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890\",\"teamId\":\"0x98005e9\"},{\"name\":\"1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 \",\"teamId\":\"0x98005ed\"},{\"name\":\"Test group - description is long and has no spaces\",\"teamId\":\"0x98005ee\"},{\"name\":\"Test - ultra long description, spaces\",\"teamId\":\"0x98005ef\",\"total\":1},{\"name\":\"           test\",\"teamId\":\"0x980066f\"},{\"name\":\" test\",\"teamId\":\"0x9800670\"},{\"name\":\",./;'[]\\-=\",\"teamId\":\"0x9800671\"},{\"name\":\"Test group with existing members\",\"teamId\":\"0x9800674\"},{\"name\":\"Test Group - existing members\",\"teamId\":\"0x9800680\"},{\"name\":\"Test autorefresh group\",\"teamId\":\"0x98006d4\"},{\"name\":\"Group with a large number of users\",\"teamId\":\"0x98006dd\",\"total\":78},{\"name\":\"Test of group with invited user\",\"teamId\":\"0x98006e8\"},{\"name\":\"Jordan team member group\",\"teamId\":\"0x98006e9\",\"total\":1},{\"name\":\"Test group 5157\",\"teamId\":\"0x9800d4e\"},{\"name\":\"Test group 5157A\",\"teamId\":\"0x9800d4f\"},{\"name\":\"JF Direct reports\",\"teamId\":\"0x9801094\"},{\"name\":\"Group for add / remove member testing\",\"teamId\":\"0x98010a9\",\"total\":6},{\"name\":\"Test Group - for line number thingy\",\"teamId\":\"0x98010bc\"},{\"name\":\"Group 2 for add / remove user testing\",\"teamId\":\"0x9801243\",\"total\":6},{\"name\":\"Tyler's Test For Understanding\",\"teamId\":\"0x9801249\",\"total\":1},{\"name\":\"Group 3 for adding / removing users\",\"teamId\":\"0x980127b\",\"total\":6},{\"name\":\"Kaly's Test Group\",\"teamId\":\"0x98012e1\",\"total\":6},{\"name\":\"9876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210\",\"teamId\":\"0x98016bf\"},{\"name\":\"9876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765432109876543210\",\"teamId\":\"0x980180d\"},{\"name\":\"New Test Group\",\"teamId\":\"0x9803815\"},{\"name\":\"Group without mandatory description\",\"teamId\":\"0x9803992\"},{\"name\":\"Group with over 100 users\",\"teamId\":\"0x98039b7\",\"total\":222},{\"name\":\"Test duplicate group name\",\"teamId\":\"0x9804115\"},{\"name\":\"Test Duplicate Group Name 1\",\"teamId\":\"0x9804116\"}]}";

//public string data = "\"status\":{\"success\":true}";
//public string data = "{\"success\":true}";

//-------------------------------------------------------------------------------------------
public class Departments {
  public List<Department> departments { get; set; }
}

public class Department {
  public string value { get; set; }
  public int    total { get; set; }

}

public class Team {
 public string name   { get; set; }
 public string teamId { get; set; }

}

public class AccountType {
 public string name   { get; set; }
 public int total     { get; set; }
 public string typeId { get; set; }

}

public class Data {
 public IList<Department>  departments   { get; set; }
 public IList<Team>        teams         { get; set; }
 public IList<AccountType> accountTypes  { get; set; }

}

public class Status {
 public bool success { get; set; }
}

public class Application {
  public Data   data    { get; set; } 
  public Status status  { get; set; } 
}




//-------------------------------------------------------------------------------------------
public class LiteObject
{
    public int Id                       { get; set; }
    public string Code                  { get; set; }
    public string Description           { get; set; }
}

public static class StringExtensions
{
    #region JsonConvert Method(s)
    
    public static IList<T> ParseToList<T>(this string config)
    {
        var items = JsonConvert.DeserializeObject<IEnumerable<T>>(config);

        return (items ?? Enumerable.Empty<T>()).ToList();
    }
    
    public static T Parse<T>(this string config)
    {
        return JsonConvert.DeserializeObject<T>(config);
    }
    
    #endregion
        
    #region LiteObject Method(s)

    private static readonly string _LiteObjectFormat = "{{ Id = {1}{0} Code = \"{2}\"{0} Description = \"{3}\" }}";

    public static string ToString(this LiteObject dto)
    {
      return dto.ToString( multiline: false );
    }

    public static string ToMultiLineString(this LiteObject dto)
    {
      return dto.ToString( multiline: true );
    }

    public static string ToString(this LiteObject dto, bool multiline )
    {
      return string.Format( _LiteObjectFormat
                           ,multiline ? Environment.NewLine + " " : ","
                           ,dto.Id
                           ,dto.Code.Trim()
                           ,dto.Description.Trim() );
    }

    #endregion
}
