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
  var job_id = "";

  var client = new RestClient("https://swtxcrm--full.sandbox.my.salesforce.com/services/data/v52.0/jobs/ingest");
//  var client = new RestClient("https://swtxcrm--full.sandbox.my.salesforce.com/services/data/v52.0/jobs/ingest/batches");
//  var client = new RestClient($"https://swtxcrm--full.sandbox.my.salesforce.com/services/data/v52.0/jobs/ingest/{job_id}");  
//  var client = new RestClient($"https://swtxcrm--full.sandbox.my.salesforce.com/services/data/v52.0/jobs/ingest/{job_id}/results");  
  client.Timeout = -1;
  
//  var request = new RestRequest(Method.PUT);
  var request = new RestRequest(Method.POST);

  request.AddHeader("Authorization", bearer_token);
//  request.AddHeader("Content-Type", "text/csv");
  request.AddHeader("Content-Type", "application/json");
  request.AddHeader("Cookie", "BrowserId=8TszxHy_Ee2idU0jA3cNiQ; CookieConsentPolicy=0:1; LSKey-c$CookieConsentPolicy=0:1");

//  var body = BuildBody(5);            
//  var body = @"{"                            + "\n" +
//             @"  ""object"" : ""Account"""   + "\n" +
//             @" ,""contentType"" : ""CSV"""  + "\n" +
//             @" ,""operation"" : ""update""" + "\n" +
//             @" ,""lineEnding"" : ""CRLF"""  + "\n" +
//             @"}";
//

  var body = UpdateJobBody;


//  request.AddParameter("text/csv", body,  ParameterType.RequestBody);
  request.AddParameter("application/json", body,  ParameterType.RequestBody);
  IRestResponse response = client.Execute(request);
  
  response.Content.Dump("response.Content");
  response.Dump("response", 0);
}

private string bearer_token 
  => $"Bearer {token}";
  
private string token 
  => "00DR00000027Qik!ARwAQGosSenQTzajubw1isAZV1Ac6kVlCDt8VFNH54sYGcabdUHQCM.YRD_tZGWNlRfFsmZ_bwU9fkPg_1CQKii3oTIDAkJU";

private static string GetHeader
  => @"Id"                          + "," + 
     @"Springworks_Specialty__c"    + "," + 
     @"Springworks_Specialty_2__c"  + "\n";

private static string GetData
  => @"0014P00002CvEsTQAV, Oncology, Other"   + "\n" +
//     @"0014P00002CvEsVQAV, Surgery, Oncology" + "\n" +
//     @"0014P00002CvEsXQAV, Oncology, Surgery" + "\n" +
//     @"0014P00002CvEsZQAV, Surgery, Other"    + "\n" +
//     @"0014P00002CvEsbQAF, Surgery, Other"    + "\n" +
//     @"0014P00002CvEsdQAF, Oncology, Other"   + "\n" +
     @"";
     
private static string UpdateJobBody
  => @"{"
   + @"  ""object"" : ""Account"""
   + @" ,""contentType"" : ""CSV"""
   + @" ,""operation"" : ""update"""
   + @" ,""lineEnding"" : ""CRLF"""
   + @"}";

private static string UpsertJobBody
  => @"{"
   + @"  ""object"" : ""Account"""
   + @"  ""externalIdFieldName"" : ""Id"""
   + @" ,""contentType"" : ""CSV"""
   + @" ,""operation"" : ""upsert"""
   + @" ,""lineEnding"" : ""CRLF"""
   + @"}";

private static string AbortJobBody
  => @"{"
   + @"  ""state"" : ""Aborted"""
   + @"}";
