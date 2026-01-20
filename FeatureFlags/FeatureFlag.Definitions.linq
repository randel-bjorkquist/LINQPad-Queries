<Query Kind="Program" />

//-------------------------------------------------------------------------------------------------
#load "..\Class-Based Enums & Flags\TypedEnum (EventType)"
#load ".\FeatureFlag"

//-------------------------------------------------------------------------------------------------
void Main()
{  
}

//-------------------------------------------------------------------------------------------------
public sealed partial class FeatureFlag : TypedEnumGuid<FeatureFlag>
{  
  #region COMMENTED OUT: specific FeatureFlag definitions moved to 'FeatureFlags.linq'
  
  public static readonly FeatureFlag CustomReports      = new(Guid.Parse("00000000-0000-0000-0000-000000000000") ,"Customer Reports"   ,nameof(CustomReports));
  public static readonly FeatureFlag NewPaymentFlow     = new(Guid.Parse("00000000-0000-0000-0000-000000000001") ,"New Payment Flow"   ,nameof(NewPaymentFlow));
  public static readonly FeatureFlag AdvancedReporting  = new(Guid.Parse("00000000-0000-0000-0000-000000000002") ,"Advanced Reporting" ,nameof(AdvancedReporting));
  
  //public static readonly FeatureFlag CustomReports      = new(Guid.Parse("00000000-0000-0000-0000-000000000000") ,"Customer Reports"   ,nameof(CustomReports)     ,true);
  //public static readonly FeatureFlag NewPaymentFlow     = new(Guid.Parse("00000000-0000-0000-0000-000000000001") ,"New Payment Flow"   ,nameof(NewPaymentFlow)    ,true);
  //public static readonly FeatureFlag AdvancedReporting  = new(Guid.Parse("00000000-0000-0000-0000-000000000002") ,"Advanced Reporting" ,nameof(AdvancedReporting) ,true);
  
  #endregion
}
