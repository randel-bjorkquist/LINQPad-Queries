<Query Kind="Statements" />

//-------------------------------------------------------------------------------------------------
#load "TypedEnums\TypedEnum"
#load "TypedFlags\TypedFlag"

//-------------------------------------------------------------------------------------------------
/// <summary>
/// Example: Feature flags for a web application
/// </summary>
public class FeatureFlags : TypedFlagLong<FeatureFlags>
{
  public static readonly FeatureFlags None              = new( 0L ,"None"               ,nameof(None));
  public static readonly FeatureFlags DarkMode          = new( 1L ,"Dark Mode"          ,nameof(DarkMode));
  public static readonly FeatureFlags BetaFeatures      = new( 2L ,"Beta Features"      ,nameof(BetaFeatures));
  public static readonly FeatureFlags AdvancedAnalytics = new( 4L ,"Advanced Analytics" ,nameof(AdvancedAnalytics));
  public static readonly FeatureFlags ExperimentalUI    = new( 8L ,"Experimental UI"    ,nameof(ExperimentalUI));
  public static readonly FeatureFlags All               = new(15L ,"All"                ,nameof(All));

  private FeatureFlags(long id, string description, string code)
    : base(id, description, code) { }
}
