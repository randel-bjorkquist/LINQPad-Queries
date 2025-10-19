<Query Kind="Program">
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
  var type = ESurveyType.Anonymous;

  type.ToString().Dump();

  //var x = Enum.Parse(type, "just try this, it's unknown");
  //var y = Enum.Parse(ESurveyType, "just try this, it's unknown");
  //var z = Enum.Parse(ESurveyType.Public, "just try this, it's unknown");
  
  bool parsed;
  
  //string hex = "0x674891".Substring(2);
  string hex = "0xf937149".Substring(2);

  Int64 int64_uid;
  UInt64 uint64_uid;

//  Int64 int64_uid   = Convert.ToInt64(hex);
//  UInt64 uint64_uid = Convert.ToUInt64(hex);
//
//  int64_uid.Dump("int64_uid");
//  uint64_uid.Dump("uint64_uid");

  #region Int32
  
  Int32 int32_uid;
  UInt32 uint32_uid;
  
  parsed = Int32.TryParse( hex
                          ,System.Globalization.NumberStyles.HexNumber
                          ,CultureInfo.CurrentCulture
                          ,out int32_uid);
  
  parsed.Dump("parsed");
  int32_uid.Dump("int32_uid");
  
  parsed = UInt32.TryParse( hex
                           ,System.Globalization.NumberStyles.HexNumber
                           ,CultureInfo.CurrentCulture
                           ,out uint32_uid);
  
  parsed.Dump("parsed");
  uint32_uid.Dump("uint32_uid");
  
  #endregion
  
  #region UInt64
  
  parsed = Int64.TryParse( hex
                          ,System.Globalization.NumberStyles.HexNumber
                          ,CultureInfo.CurrentCulture
                          ,out int64_uid);
  
  parsed.Dump("parsed");
  int64_uid.Dump("int64_uid");
  
  parsed = UInt64.TryParse( hex
                           ,System.Globalization.NumberStyles.HexNumber
                           ,CultureInfo.CurrentCulture
                           ,out uint64_uid);
  
  parsed.Dump("parsed");
  uint64_uid.Dump("uint64_uid");
  
  #endregion
}

// You can define other methods, fields, classes and namespaces here
public enum ESurveyType : short
{
  Unknown = 0,
  Anonymous = 1,
  Public = 2
}