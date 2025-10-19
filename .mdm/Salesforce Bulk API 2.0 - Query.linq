<Query Kind="Program">
  <NuGetReference Version="106.15.0">RestSharp</NuGetReference>
  <Namespace>RestSharp</Namespace>
  <Namespace>RestSharp.Authenticators</Namespace>
  <Namespace>RestSharp.Authenticators.OAuth</Namespace>
  <Namespace>RestSharp.Extensions</Namespace>
  <Namespace>RestSharp.Serializers</Namespace>
</Query>

void Main()
{
  var query_job_id = "750R0000009QNdUIAW";
  
  var client = new RestClient("https://swtxcrm--full.sandbox.my.salesforce.com/services/data/v52.0/jobs/query");  
//  var client = new RestClient($"https://swtxcrm--full.sandbox.my.salesforce.com/services/data/v52.0/jobs/query/{query_job_id}");  
//  var client = new RestClient($"https://swtxcrm--full.sandbox.my.salesforce.com/services/data/v52.0/jobs/query/{query_job_id}/results");  
  client.Timeout = -1;
  
  var request = new RestRequest(Method.GET);
//  var request = new RestRequest(Method.POST);
//  var request = new RestRequest(Method.PATCH);    //aborts a query job
//  var request = new RestRequest(Method.DELETE);   //deletes a query job

  request.AddHeader("Authorization", bearer_token);
  request.AddHeader("Content-Type", "application/json");
  request.AddHeader("Cookie", "BrowserId=8TszxHy_Ee2idU0jA3cNiQ; CookieConsentPolicy=0:1; LSKey-c$CookieConsentPolicy=0:1");

//  var body = BuildQueryBody;
//         
//  request.AddParameter("application/json", body,  ParameterType.RequestBody);
  IRestResponse response = client.Execute(request);

  response.Dump("response", 0);
  response.Content.Dump("response.Content");
}

private string bearer_token 
  => $"Bearer {token}";
  
private string token 
  => "00DR00000027Qik!ARwAQGosSenQTzajubw1isAZV1Ac6kVlCDt8VFNH54sYGcabdUHQCM.YRD_tZGWNlRfFsmZ_bwU9fkPg_1CQKii3oTIDAkJU";

private static string BuildQueryBody
  => @"{"
   + @"  ""operation"" : ""query"""
   + @" ,""query"" : ""SELECT Id FROM Account WHERE VeevaID_vod__c != NULL ORDER BY Id ASC"""
   + @" ,""contentType"" : ""CSV"""
   + @" ,""columnDelimiter"" : ""PIPE"""
   + @" ,""lineEnding"" : ""CRLF"""
   + @"}";

private static string BuildAbortBody
  => @"{"
   + @"  ""state"" : ""Aborted"""
   + @"}";
