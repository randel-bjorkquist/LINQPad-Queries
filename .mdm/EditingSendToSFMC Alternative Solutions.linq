<Query Kind="Program">
  <Namespace>System.Text.Json</Namespace>
</Query>

void Main()
{
	var result = EditingSendToSFMC();
	Console.WriteLine("var result = EditingSendToSFMC();");
	Console.Write($"result = {result}");
	Console.WriteLine();
	Console.WriteLine();

	string _null = null;
	_null.IsTrue()
			 .Dump("_null.IsTrue()");

//	result = _null.IsTrue(out var _out_param_1);
	result = _null.TryParse(out var _out_param_1);
	result.Dump("_null.TryParse(out var _out_param_1)");
	_out_param_1.Dump("_out_param_1");

	string _true = "true";
	_true.IsTrue().Dump("_true.IsTrue()");

	result = _true.TryParse(out var _out_param_2);
	result.Dump("_true.TryParse(out var _out_param_2)");
	_out_param_2.Dump("_out_param_2");

	string _false = "false";
	_false.IsTrue().Dump("_false.IsTrue()");

	result = _false.TryParse(out var _out_param_3);
	result.Dump("_false.TryParse(out var _out_param_3)");
	_out_param_3.Dump("_out_param_3");
}

private static readonly List<Property> _properties = new List<Property> { new Property { name = "item 1", value = "true"      }
																																				 ,new Property { name = "item 3", value = "false"     }
																																				 ,new Property { name = "item 5", value = "not true"  }
																																				 ,new Property { name = "item 6", value = "not false" }};

private bool EditingSendToSFMC()
{
	var value = (_properties.SingleOrDefault(p => p.name == "")  ?? new Property())
												 .ToString();
	
	return StringExtensions.IsTrue(value);
	
//	return _properties.SingleOrDefault(p => p.name == "item 2")
//					 				 ?.value
//									  .IsTrue() ?? false;
}

#region COMMENTED OUT:

//private bool EditingSendToSFMC()
//{
//	foreach(var property in _properties.properties)
//	{
//		if(property.name == SWTXVocabularies.HCP.SendToSFMC)
//		{
//			//Possible Change: 
//			//	1. Always returns false if null; if not null, returns actual property value
//			//	2. Does not introduce changes to the arguement when simply searching or looking
//			//		 up property values.
//			return property?.value ?? false;
//			
//			#region COMMENTED OUT: ORGINAL CODE
//			//
//			//if (property.value == null)
//			//{
//			//	// This is so we can reverse the action from true to false.
//			//	property.value = "false";
//			//}
//			//return true;
//			//
//			#endregion
//		}
//	}
//
//	return false;
//}

#endregion

public static class StringExtensions
{
	public static bool IsTrue(this string input)
	{
		return input.TryParse(out var result) && result;
	}
	
	[Obsolete("I would suggest not using this one, rather using the TryParse edition")]
	public static bool IsTrue(this string input, out bool result)
	{
		input.TryParse(out result);
		return result;
		
#region COMMENTED OUT
/*	
//		result = false;
//		
//		if(string.IsNullOrEmpty(input)) 
//		{ 
//			return result; 
//		}
//		
//		var temp = input.ToLower();
//		
//		if(temp != "yes" && temp != "true"  && temp != "on" &&
//			 temp != "no"  && temp != "false" && temp != "off")
//		{
//			throw new ArgumentOutOfRangeException("Only values of: yes/no, true/false, on/off are valid.");
//		}
//		
//		if(temp == "yes" || temp == "true" || temp == "on") 
//		{
//			result = true;
//		}		
//		else if(temp == "no" || temp == "false" || temp == "off")
//		{
//			result = false;
//		}
//
//		return result;
*/	
#endregion
	}
	
	public static bool TryParse(this string input, out bool result)
	{		
		result = false;
		var success = false;
		
		if(string.IsNullOrEmpty(input)) 
		{ 
			return true;
		}

		var temp = input.Trim()
										.ToLower();
		
		switch(temp)
		{
			case "yes":
			case "true":
			case "on":
				success = true;
				result  = true;
				break;
				
			case "no":
			case "false":
			case "off":
				success = true;
				result  = false;
				break;
		}

		return success;
	}
}

public static class BooleanExtension
{
	public static bool TryParse(this bool input, out bool result)
	{
		result = false;
		var success = false;
		
		return success;
	}
}

public class Property
{
	public string name  { get; set; }

	public string value { get; set; }
}
