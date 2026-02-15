<Query Kind="Statements">
  <Namespace>mcsCore.Utilities.Common.TypedEnums.Base</Namespace>
</Query>

//---------------------------------------------------------------------------------------
#load "TypedEnums\TypedEnum"

//---------------------------------------------------------------------------------------
/// <summary>
/// TypedEnum for United States
///   ID = State Abbreviation (e.g., "AL")
///   Description = Full State Name (e.g., "Alabama")
///   Code = Field Name (e.g., "Alabama")
/// </summary>
public sealed class USState : TypedEnumString<USState>
{
  private USState(string id, string description, string code)
      : base(id, description, code) { }

  #region US States (50 States)

  public static readonly USState Alabama        = new("AL" ,"Alabama"         ,nameof(Alabama));
  public static readonly USState Alaska         = new("AK" ,"Alaska"          ,nameof(Alaska));
  public static readonly USState Arizona        = new("AZ" ,"Arizona"         ,nameof(Arizona));
  public static readonly USState Arkansas       = new("AR" ,"Arkansas"        ,nameof(Arkansas));
  public static readonly USState California     = new("CA" ,"California"      ,nameof(California));
                                                  
  public static readonly USState Colorado       = new("CO" ,"Colorado"        ,nameof(Colorado));
  public static readonly USState Connecticut    = new("CT" ,"Connecticut"     ,nameof(Connecticut));
  public static readonly USState Delaware       = new("DE" ,"Delaware"        ,nameof(Delaware));
  public static readonly USState Florida        = new("FL" ,"Florida"         ,nameof(Florida));
  public static readonly USState Georgia        = new("GA" ,"Georgia"         ,nameof(Georgia));
                                                             
  public static readonly USState Hawaii         = new("HI" ,"Hawaii"          ,nameof(Hawaii));
  public static readonly USState Idaho          = new("ID" ,"Idaho"           ,nameof(Idaho));
  public static readonly USState Illinois       = new("IL" ,"Illinois"        ,nameof(Illinois));
  public static readonly USState Indiana        = new("IN" ,"Indiana"         ,nameof(Indiana));
  public static readonly USState Iowa           = new("IA" ,"Iowa"            ,nameof(Iowa));
                                                             
  public static readonly USState Kansas         = new("KS" ,"Kansas"          ,nameof(Kansas));
  public static readonly USState Kentucky       = new("KY" ,"Kentucky"        ,nameof(Kentucky));
  public static readonly USState Louisiana      = new("LA" ,"Louisiana"       ,nameof(Louisiana));
  public static readonly USState Maine          = new("ME" ,"Maine"           ,nameof(Maine));
  public static readonly USState Maryland       = new("MD" ,"Maryland"        ,nameof(Maryland));
                                                             
  public static readonly USState Massachusetts  = new("MA" ,"Massachusetts"   ,nameof(Massachusetts));
  public static readonly USState Michigan       = new("MI" ,"Michigan"        ,nameof(Michigan));
  public static readonly USState Minnesota      = new("MN" ,"Minnesota"       ,nameof(Minnesota));
  public static readonly USState Mississippi    = new("MS" ,"Mississippi"     ,nameof(Mississippi));
  public static readonly USState Missouri       = new("MO" ,"Missouri"        ,nameof(Missouri));
                                                             
  public static readonly USState Montana        = new("MT" ,"Montana"         ,nameof(Montana));
  public static readonly USState Nebraska       = new("NE" ,"Nebraska"        ,nameof(Nebraska));
  public static readonly USState Nevada         = new("NV" ,"Nevada"          ,nameof(Nevada));
  public static readonly USState NewHampshire   = new("NH" ,"New Hampshire"   ,nameof(NewHampshire));
  public static readonly USState NewJersey      = new("NJ" ,"New Jersey"      ,nameof(NewJersey));
                                                             
  public static readonly USState NewMexico      = new("NM" ,"New Mexico"      ,nameof(NewMexico));
  public static readonly USState NewYork        = new("NY" ,"New York"        ,nameof(NewYork));
  public static readonly USState NorthCarolina  = new("NC" ,"North Carolina"  ,nameof(NorthCarolina));
  public static readonly USState NorthDakota    = new("ND" ,"North Dakota"    ,nameof(NorthDakota));
  public static readonly USState Ohio           = new("OH" ,"Ohio"            ,nameof(Ohio));
                                                             
  public static readonly USState Oklahoma       = new("OK" ,"Oklahoma"        ,nameof(Oklahoma));
  public static readonly USState Oregon         = new("OR" ,"Oregon"          ,nameof(Oregon));
  public static readonly USState Pennsylvania   = new("PA" ,"Pennsylvania"    ,nameof(Pennsylvania));
  public static readonly USState RhodeIsland    = new("RI" ,"Rhode Island"    ,nameof(RhodeIsland));
  public static readonly USState SouthCarolina  = new("SC" ,"South Carolina"  ,nameof(SouthCarolina));
                                                             
  public static readonly USState SouthDakota    = new("SD" ,"South Dakota"    ,nameof(SouthDakota));
  public static readonly USState Tennessee      = new("TN" ,"Tennessee"       ,nameof(Tennessee));
  public static readonly USState Texas          = new("TX" ,"Texas"           ,nameof(Texas));
  public static readonly USState Utah           = new("UT" ,"Utah"            ,nameof(Utah));
  public static readonly USState Vermont        = new("VT" ,"Vermont"         ,nameof(Vermont));
                                                                                
  public static readonly USState Virginia       = new("VA" ,"Virginia"        ,nameof(Virginia));
  public static readonly USState Washington     = new("WA" ,"Washington"      ,nameof(Washington));
  public static readonly USState WestVirginia   = new("WV" ,"West Virginia"   ,nameof(WestVirginia));
  public static readonly USState Wisconsin      = new("WI" ,"Wisconsin"       ,nameof(Wisconsin));
  public static readonly USState Wyoming        = new("WY" ,"Wyoming"         ,nameof(Wyoming));

  #endregion
}
