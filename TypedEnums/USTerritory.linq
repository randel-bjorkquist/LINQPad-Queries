<Query Kind="Statements" />

//---------------------------------------------------------------------------------------
#load "TypedEnums\TypedEnum"

//---------------------------------------------------------------------------------------
/// <summary>
/// TypedEnum for United States Territory
///   ID          = Territory Abbreviation (e.g., "AS", "GU", "PR")
///   Description = Full Territory Name (e.g., "American Samoa", " Guam")
///   Code        = Field Name (e.g., "NorthernMarianaIslands", "VirginIslands")
/// </summary>
public sealed class USTerritory : TypedEnumString<USTerritory>
{
  private USTerritory(string id, string description, string code)
      : base(id, description, code) { }

  #region US States (5 Territories)

  public static readonly USTerritory AmericanSamoa           = new("AS" ,"American Samoa"            ,nameof(AmericanSamoa));
  public static readonly USTerritory Guam                    = new("GU" ,"Guam"                      ,nameof(Guam));
  public static readonly USTerritory NorthernMarianaIslands  = new("MP" ,"Northern Mariana Islands"  ,nameof(NorthernMarianaIslands));
  public static readonly USTerritory PuertoRico              = new("PR" ,"Puerto Rico"               ,nameof(PuertoRico));
  public static readonly USTerritory VirginIslands           = new("VI" ,"Virgin Islands"            ,nameof(VirginIslands));

  #endregion
}
