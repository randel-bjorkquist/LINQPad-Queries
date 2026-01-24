<Query Kind="Program" />

//-------------------------------------------------------------------------------------------------
//NOTE: This is a C# program because 'FeatureFlag' is a sealed partial class, with the specific, 
//      FeatureFlag instance declarations here, and its associated sealed partial class found in the 
//      'FeatureFlag' file.
//
//ERROR: pressing 'F5' in an attempt to 'run' this file will generate an error on line 104, in the
//       'FeatureFlag.linq' file, saying the type of namespace named 'FlagResolution' could not be
//        found.

//-------------------------------------------------------------------------------------------------
#load "..\TypedEnums\TypedEnum"
#load ".\FeatureFlag"

//-------------------------------------------------------------------------------------------------
public sealed partial class FeatureFlag : TypedEnumGuid<FeatureFlag>
{  
  #region Specific FeatureFlag definitions moved here from 'FeatureFlag.linq'
  
  public static readonly FeatureFlag CustomReports      = new(Guid.Parse("00000000-0000-0000-0000-000000000000") ,"Customer Reports"   ,nameof(CustomReports));
  public static readonly FeatureFlag NewPaymentFlow     = new(Guid.Parse("00000000-0000-0000-0000-000000000001") ,"New Payment Flow"   ,nameof(NewPaymentFlow));
  public static readonly FeatureFlag AdvancedReporting  = new(Guid.Parse("00000000-0000-0000-0000-000000000002") ,"Advanced Reporting" ,nameof(AdvancedReporting));
  
  //public static readonly FeatureFlag CustomReports      = new(Guid.Parse("00000000-0000-0000-0000-000000000000") ,"Customer Reports"   ,nameof(CustomReports)     ,true);
  //public static readonly FeatureFlag NewPaymentFlow     = new(Guid.Parse("00000000-0000-0000-0000-000000000001") ,"New Payment Flow"   ,nameof(NewPaymentFlow)    ,true);
  //public static readonly FeatureFlag AdvancedReporting  = new(Guid.Parse("00000000-0000-0000-0000-000000000002") ,"Advanced Reporting" ,nameof(AdvancedReporting) ,true);
  
  #endregion
}
