<Query Kind="Statements" />

//-------------------------------------------------------------------------------------------------
public class FeatureFlagConfig
{
  public Dictionary<string, bool> Global                      { get; set; } = new();  // key = TypedEnum.Code (field name)
  public Dictionary<string, Dictionary<string, bool>> Tenants { get; set; } = new();  // key = TypedEnum.Code (field name)
}