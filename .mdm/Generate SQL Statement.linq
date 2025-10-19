<Query Kind="Program" />

void Main()
{
  var ids = new List<string> { "GUID-0001" ,"GUID-0002"
                              ,"GUID-0003" ,"GUID-0004"
                              ,"GUID-0005" ,"GUID-0006"
                              ,"GUID-0007" ,"GUID-0008"
                              ,"GUID-0009" ,"GUID-0010"};
  
  var table_name  = "employee";
  var table_alias = "E";
//  var table_dictionary = new Dictionary<string, string>() {{ table_name, table_alias }};
  
  var select      = "SELECT";  
  var field_list  = new List<string> { $"{table_alias}.ID"
                                      ,$"{table_alias}.FirstName"
                                      ,$"{table_alias}.LastName"
                                      ,$"{table_alias}.DOB" };
                                      
  var fields      = field_list.Any() 
                  ? string.Join(", ", field_list) 
                  : "*";
                  
  var from  = $"FROM dbo.{table_name} AS {table_alias}";
  var where = $"WHERE {table_alias}.ID IN ('{string.Join("', '", ids)}')";
  
  var sql = select 
          + " "
          + fields
          + " \n"
          + from
          + " \n"
          + where
          + ";";
  
  sql.Dump();  
}

