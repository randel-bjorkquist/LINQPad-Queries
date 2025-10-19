<Query Kind="Program" />

void Main()
{
  var postal_code = SplitPostalCode("12345-6789", out var postal_code_extension, true);
  //var postal_code = SplitPostalCode("12345-6789", out var postal_code_extension, false);
  //var postal_code = SplitPostalCode("00005-0004", out var postal_code_extension, true);
  //var postal_code = SplitPostalCode("00005-0004", out var postal_code_extension, false);
  //var postal_code = SplitPostalCode("12345-123", out var postal_code_extension, false);
  //var postal_code = SplitPostalCode("12345", out var postal_code_extension, true);
  
  postal_code.Dump("postal_code");
  postal_code_extension.Dump("postal_code_extension");
}

public static string SplitPostalCode( string original
                                     ,out string postalCodeExtension
                                     ,bool RetainInvalidValues = false )
{
  var result          = string.Empty;
  postalCodeExtension = string.Empty;
  
  if(string.IsNullOrWhiteSpace(original))
  {
    return string.Empty;
  }
  
  original = Regex.Replace(original, @"[^0-9$,]", "");

  if(original.Length >= 5)
  {
    result = original[..5];
    
    if(original.Length > 5)
    {
      if(!RetainInvalidValues)
      {
        //var validExtentionLength = 4;
        //
        //if(original.Length - 5 == validExtentionLength)
        //{
        //  postalCodeExtension = original.Substring(5, validExtentionLength);
        //}
        
        if(original.Length - 5 == 4)
          postalCodeExtension = original.Substring(5, 4);
      }
      else
      {
        postalCodeExtension = original.Substring(5);
      }
    }
  }
  else
  {
    result = RetainInvalidValues ? original : result;
  }
  
  return result;
}

