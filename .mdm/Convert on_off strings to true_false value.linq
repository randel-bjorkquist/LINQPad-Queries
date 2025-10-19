<Query Kind="Program" />

void Main()
{
	string key 	 = "ConvertOnOff2TrueFalse";
	object value = "on";
//	object value = "off";
//	object value = "blah";
	
  var t = bool.TrueString;
  t.Dump("t");
  
  var f = bool.FalseString;
  f.Dump("f");
  
	switch(value)
	{
		case "on":		value = true;		break;			
		case "off":		value = false;	break;			
		default:
			throw new ArgumentOutOfRangeException($"Translating '{key}' value '{value}' failed.");
	}
	
	value.Dump("value");
}
