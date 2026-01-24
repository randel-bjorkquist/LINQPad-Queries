<Query Kind="Program">
  <NuGetReference>System.Text.Json</NuGetReference>
  <Namespace>System.Text.Json</Namespace>
</Query>

//-------------------------------------------------------------------------------------------------
#load "..\TypedEnums\TypedEnum"
#load "..\TypedEnums\HousingAuthority"
#load ".\FeatureFlag"
#load ".\FeatureFlag.Definitions"
#load ".\FeatureFlagConfig"
#load ".\FlagValueSource"
#load ".\FlagResolution"

//-------------------------------------------------------------------------------------------------
void Main()
{
  //NOTE: Make sure the JSON file exists in the same folder as your 
  //      LINQPad query (or adjust the path/filename below if needed) ...
  //FeatureFlag.Initialize("featureflags.json");
  //FeatureFlag.Initialize();
  
  #region general examples ...

  var flag = FeatureFlag.CustomReports;
  flag.Dump("var flag = mcsFeatureFlag.CustomReports;", 0);
  
  //flag = FeatureFlag.CustomReports["TenantA"];
  
  if (FeatureFlag.CustomReports)
    $"FeatureFlag.CustomReports is active."
      .Dump("if (FeatureFlag.CustomReports)");
  else
    $"FeatureFlag.CustomReports is NOT active."
      .Dump("if (FeatureFlag.CustomReports)");
  
  FeatureFlag.NewPaymentFlow.Dump("FeatureFlag.NewPaymentFlow");

  if(FeatureFlag.NewPaymentFlow)
    $"FeatureFlag.NewPaymentFlow is active."
      .Dump("if (FeatureFlag.NewPaymentFlow)");
  else
    $"FeatureFlag.NewPaymentFlow is NOT active."
      .Dump("if (FeatureFlag.NewPaymentFlow)");



  var tenant_code = "TenantA";
  tenant_code.Dump("var tenant_code = \"TenantA\";");

  if (FeatureFlag.NewPaymentFlow[tenant_code])
    $"FeatureFlag.NewPaymentFlow for '{tenant_code}' is active."
      .Dump($"if (FeatureFlag.NewPaymentFlow[{tenant_code}])");
  else
    $"FeatureFlag.NewPaymentFlow for '{tenant_code}' is 'not' active."
      .Dump($"if (FeatureFlag.NewPaymentFlow[{tenant_code}])");


  if (FeatureFlag.NewPaymentFlow[null])
    $"FeatureFlag.NewPaymentFlow for 'null' is active."
      .Dump($"if (FeatureFlag.NewPaymentFlow['null'])");
  else
    $"FeatureFlag.NewPaymentFlow for 'null' is 'not' active."
      .Dump($"if (FeatureFlag.NewPaymentFlow['null'])");



  var pha_id = 1190;  // pulled from state ...
  var event_type = HousingAuthority.GetByID(pha_id);
  var code = event_type.Code;
  var event_type_code = HousingAuthority.GetByID(pha_id).Code;

  var feature_flag = FeatureFlag.NewPaymentFlow[code];
  feature_flag.Dump($"var feature_flag = FeatureFlag.NewPaymentFlow[{code}];");

  //if(FeatureFlag.NewPaymentFlow[HousingAuthority.GetByID(1190).Code])
  if (FeatureFlag.NewPaymentFlow[code])
    $"FeatureFlag.NewPaymentFlow for '{event_type.ToString()}' is active."
      .Dump($"if (FeatureFlag.NewPaymentFlow[{code}])");
  else
    $"FeatureFlag.NewPaymentFlow for '{event_type.ToString()}' is 'not' active."
      .Dump($"if (FeatureFlag.NewPaymentFlow[{code}])");
  
  
//  if (FeatureFlag.NewPaymentFlow[pha_id])
  if (FeatureFlag.NewPaymentFlow[1190])
    $"FeatureFlag.NewPaymentFlow for '{event_type.ToString()}' is active."
      .Dump("if (FeatureFlag.NewPaymentFlow[1190])");
  else
    $"FeatureFlag.NewPaymentFlow for '{event_type.ToString()}' is 'not' active."
      .Dump("if (FeatureFlag.NewPaymentFlow[1190])");
  
  
  var flags = FeatureFlag.GetAll();
  flags.Dump("var flags = mcsFeatureFlag.GetAll();", 0);  

  FeatureFlag.CustomReports.Resolve("TenantA").Dump("FeatureFlag.CustomReports.Resovle('TenantA')", 0);
  FeatureFlag.NewPaymentFlow.Resolve("TenantA").Dump("FeatureFlag.NewPaymentFlow.Resovle('TenantA')", 0);
  FeatureFlag.AdvancedReporting.Resolve("TenantA").Dump("FeatureFlag.AdvancedReporting.Resovle('TenantA')", 0);
  
  FeatureFlag.CustomReports.Resolve("TenantB").Dump("FeatureFlag.CustomReports.Resovle('TenantB')", 0);
  FeatureFlag.NewPaymentFlow.Resolve("TenantB").Dump("FeatureFlag.NewPaymentFlow.Resovle('TenantB')", 0);
  FeatureFlag.AdvancedReporting.Resolve("TenantB").Dump("FeatureFlag.AdvancedReporting('TenantB')", 0);

  FeatureFlag.CustomReports.Resolve(1190).Dump("FeatureFlag.CustomReports.Resovle(1190)", 0);
  FeatureFlag.NewPaymentFlow.Resolve(1190).Dump("FeatureFlag.NewPaymentFlow.Resovle(1190)", 0);
  
  FeatureFlag.CustomReports.Resolve("Demonstration").Dump("FeatureFlag.CustomReports.Resovle('Demonstration')", 0);

  #endregion
  
  #region COMMENTED OUT: looping via while(true) ...
//  
//  "Feature Flag Test - Press Enter to refresh, Q to quit".Dump();
//  
//  while (true)
//  {
//    Console.WriteLine("\nCurrent 'global' state (as of " + DateTime.Now.ToLongTimeString() + "):");
//    Console.WriteLine("--------------------------------------------------");
//  
//    DumpFlag(FeatureFlag.CustomReports);
//    DumpFlag(FeatureFlag.NewPaymentFlow);
//    DumpFlag(FeatureFlag.AdvancedReporting);
//  
//    Console.WriteLine("\nTenant-specific examples:");
//    Console.WriteLine("--------------------------------------------------");
//    
//    Console.WriteLine($"CustomReports     for TenantA: {FeatureFlag.CustomReports["TenantA"]}");
//    Console.WriteLine($"NewPaymentFlow    for TenantA: {FeatureFlag.NewPaymentFlow["TenantA"]}");
//    Console.WriteLine($"AdvancedReporting for TenantA: {FeatureFlag.AdvancedReporting["TenantA"]}");
//    
//    Console.WriteLine("-------------------------------------");
//    
//    Console.WriteLine($"CustomReports     for TenantB: {FeatureFlag.CustomReports["TenantB"]}");
//    Console.WriteLine($"NewPaymentFlow    for TenantB: {FeatureFlag.NewPaymentFlow["TenantB"]}");
//    Console.WriteLine($"AdvancedReporting for TenantB: {FeatureFlag.AdvancedReporting["TenantB"]}");
//
//    //Console.WriteLine("--------------------------------------------------");
//    //
//    //FeatureFlag.CustomReports.Resovle("TenantA").Dump("FeatureFlag.CustomReports.Resovle(\"TenantA\")");
//    //FeatureFlag.NewPaymentFlow.Resovle("TenantA").Dump("FeatureFlag.CustomReports.Resovle(\"TenantA\")");
//    //FeatureFlag.AdvancedReporting.Resovle("TenantA").Dump("FeatureFlag.CustomReports.Resovle(\"TenantA\")");
//    //
//    //Console.WriteLine("-------------------------------------");
//    //
//    //FeatureFlag.CustomReports.Resovle("TenantB").Dump("FeatureFlag.CustomReports.Resovle(\"TenantB\")");
//    //FeatureFlag.NewPaymentFlow.Resovle("TenantB").Dump("FeatureFlag.CustomReports.Resovle(\"TenantB\")");
//    //FeatureFlag.AdvancedReporting.Resovle("TenantB").Dump("FeatureFlag.CustomReports.Resovle(\"TenantB\")");
//
//    Console.Write("\nPress Enter to refresh, Q to quit: ");
//    var key = Console.Read();
//    if (key == 'q' || key == 'Q') break;
//    
//    Console.WriteLine();
//    
//    // Optional: force reload (mainly for debugging)
//    //FeatureFlag.Initialize();
//  }
//  
//  "Done. You can now edit featureflags.json and re-run or continue watching.".Dump();  
//
  #endregion
}
