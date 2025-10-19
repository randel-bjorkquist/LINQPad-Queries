<Query Kind="Program" />

void Main()
{
  var Regex2Apply = "[^0-9]";
  var regex = new Regex("Regex2Apply");

  var property_value = "123.456.7890";
  //var property_value = "0123456789";
  
  //var regex_result = Regex.Replace(property_value, Regex2Apply);
  var regex_result = regex.Replace(property_value, Regex2Apply);
  
  regex_result.Dump();

  property_value.Dump("BEFORE: Regex.IsMatch()");

  if(Regex.IsMatch(property_value, Regex2Apply))
  {
    "INSIDE: Regex.IsMatch()".Dump();
    property_value = Regex.Replace(property_value, Regex2Apply, "");
  }
  
  property_value.Dump("AFTER: Regex.IsMatch()");

}
