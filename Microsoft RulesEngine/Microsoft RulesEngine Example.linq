<Query Kind="Program">
  <NuGetReference>RulesEngine</NuGetReference>
  <Namespace>RulesEngine</Namespace>
  <Namespace>RulesEngine.Models</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
  <IncludeLinqToSql>true</IncludeLinqToSql>
  <RuntimeVersion>9.0</RuntimeVersion>
</Query>

/// <summary>
/// Newletter Article: Dynamic Business Logic Without Redeploys? Meet ASP .Net RulesEngine
/// Source URL: https://newsletter.kanaiyakatarmal.com/p/dynamic-business-logic-without-redeploys
/// </summary>
void Main()
{
  var json_rules = File.ReadAllText(@"D:\repos\randel-bjorkquist\LINQPad-Queries\Microsoft RulesEngine\discount-rules.json");
  
  var workflow_rules = JsonSerializer.Deserialize<Workflow[]>(json_rules, JsonHelper.options);
  
  var rules_engine = new RulesEngine.RulesEngine(workflow_rules, null);

  var input = new CustomerData { Country              = "india"
                                ,LoyaltyFactor        = 3
                                ,TotalPurchasesToDate = 15000 };
  
  //NOTE: The parameter name MUST MATCH what's in the json file, 
  //      the object value can be named anything/any variable.
  var inputs = new RuleParameter("input1", input);
  
  //NOTE: I'm not sure why this isn't working, LINQPad said 'ExecuteAllRulesAsync' 
  //      IS NOT async and thus I cannot use the 'await' keyword.
  //var results = await rules_engine.ExecuteAllRulesAsync("Discount", inputs);
  
  //NOTE: Work around for LINQPad not believe I can use the 'await' keyword ...
  var results = rules_engine.ExecuteAllRulesAsync("Discount", inputs)
                            .GetAwaiter()
                            .GetResult();
  
  foreach (var result in results)
  {
    Console.WriteLine($"Rule: {result.Rule.RuleName}, IsSucess: {result.IsSuccess}, Event: {result.Rule.SuccessEvent}, Message: {result.ExceptionMessage}");
  }
}

public static class JsonHelper
{
  public static JsonSerializerOptions options 
    => new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
}

public class CustomerData
{
  public string Country               { get; set; } = string.Empty;
  public int LoyaltyFactor            { get; set; }
  public decimal TotalPurchasesToDate { get; set; }
}