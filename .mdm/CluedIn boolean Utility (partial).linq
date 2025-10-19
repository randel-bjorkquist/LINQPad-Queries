<Query Kind="Program" />

void Main()
{
	string blah = null;
	var x = blah?.ToString().Dump("blah");
	
	var vocab = new Vocabulary { Key = "blah", Value = null };
//	var @bool = vocab.ToBoolean();
	var @bool = vocab?.Value.ToBoolean() ?? null;
	@bool.Dump();
}

public class Vocabulary
{
	public string Key 	{ get; set; }
	public object Value { get; set; }
}

public static class VocabularyExtension
{
	public static bool ToBoolean(this Vocabulary vocab)
	{
//		var input = vocab?.Value;
		var input = (vocab?.Value
										  .ToString()
//											.Trim()
//											.ToLower()
											) ?? null;
		
		if(input == null)
		{
			throw new ArgumentNullException();
		}
		
		if(input == string.Empty || 
			!bool.TryParse(vocab.Value.ToString(), out bool result))
		{
			throw new ArgumentOutOfRangeException();
		}
		
		return result;
	}
}

public static class ObjectExtensions
{
	public static bool ToBoolean(this object entity)
	{
		bool.TryParse(entity?.ToString(), out bool result);
		
		return result;
	}
}