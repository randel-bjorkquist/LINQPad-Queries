<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference Version="106.15.0">RestSharp</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>RestSharp</Namespace>
  <Namespace>RestSharp.Authenticators</Namespace>
  <Namespace>RestSharp.Authenticators.OAuth</Namespace>
  <Namespace>RestSharp.Extensions</Namespace>
  <Namespace>RestSharp.Serializers</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

void Main()
{
#region build 'url' ...
  
  //var url = $"{venues_url}";
  //var url = $"{venues_url}?per_page=100";
  //var url = $"{venues_url}?per_page=25&page=3";
  //var url = $"{venues_url}?geoip=98.213.245.205&range=12mi";
  //var url = $"{venues_url}?geoip=98.213.245.205&range=12mi&per_page=25";

  //var url = $"{events_url}";
  //var url = $"{events_url}?per_page=1";
  //var url = $"{events_url}?type=mlb&per_page=100";
  //var url = $"{events_url}?type=nascar&per_page=25";
  //var url = $"{events_url}?type=mlb&per_page=25&page=3";
  //var url = $"{events_url}?geoip=98.213.245.205&range=12mi";

  //url = $"{base_url}/{url_version}/{Recommendations(358, 10014)}";
  //url.Dump("url");

#endregion
  
#region get data from source ...
  
  //var source_client = new RestClient(url);
  var source_client = new RestClient($"{events_url}?type=nba");
  source_client.Timeout = -1;

  var source_request = new RestRequest(Method.GET);

  source_request.AddHeader("Authorization", basic_authentication);
  source_request.AddHeader("Content-Type", "application/json");
  source_request.AddHeader("Cookie", "BrowserId=8TszxHy_Ee2idU0jA3cNiQ; CookieConsentPolicy=0:1; LSKey-c$CookieConsentPolicy=0:1");

  IRestResponse source_response = source_client.Execute(source_request);

//  source_response.Dump("source response", 0);
  JsonConvert.DeserializeObject(source_response.Content)
//                            .ToString()
             .Dump("source content", 1);

  JObject content = JObject.Parse(source_response.Content);
  Meta meta = new Meta((JObject)content["meta"]);
  meta.Dump("meta");

  var per_page = 100;
  var pages    = (int)meta.total / (int)per_page;
  
  if((int)meta.total % (int)per_page != 0)
  {
    pages++;
  }
    
#endregion

#region process data ...

  JArray venues = new JArray();

  for(int i = 1; i < pages; i++)
  {
    source_client = new RestClient($"{events_url}?type=nba&per_page={per_page}&page={i}");
    source_client.Timeout = -1;

    source_request = new RestRequest(Method.GET);

    source_request.AddHeader("Authorization", basic_authentication);
    source_request.AddHeader("Content-Type", "application/json");
    source_request.AddHeader("Cookie", "BrowserId=8TszxHy_Ee2idU0jA3cNiQ; CookieConsentPolicy=0:1; LSKey-c$CookieConsentPolicy=0:1");

    source_response = source_client.Execute(source_request);
//    source_response.Dump("source response", 0);

//    JsonConvert.DeserializeObject(source_response.Content)
////               .ToString()
//               .Dump("source content", 1);

    content = JObject.Parse(source_response.Content);
    JArray events = (JArray)content["events"];
//    events.Dump("events", 0);

    foreach(JObject e in events.Children<JObject>())
    {
      var venue = e["venue"];      
      var id = (int)venue["id"];

      if(venues.Any(v => (int)v["id"] == id))
      {
        continue; 
      }
      
      #region Split 'locations' into separate lat/lon properties and add properties: 'latitude' and 'longitude'
      
      var location = ((JObject)venue["location"]).ToString();     
      
      //string manipulations
      //var coordinates = location.Replace("{", "")
      //                          .Replace("}", "")
      //                          .Split(',');
      //var latitude    = coordinates[0].Split(":")[1];
      //var longitude   = coordinates[1].Split(":")[1];
      
      //deserialize to type 'Dictionary<string, decimal>'
      //var coordinates = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(location);
      //var latitude    = coordinates["lat"].ToString();
      //var longitude   = coordinates["lon"].ToString();
      
      //deserialize to specific type 'LatitudeLongitude'
      var coordinates   = JsonConvert.DeserializeObject<LatitudeLongitude>(location);
      var latitude      = coordinates.Latidude;
      var longitude     = coordinates.Longitude;

      venue.AddAnnotation("latitude");
      venue["latitude"] = new JValue(latitude);
      
      venue.AddAnnotation("longitude");
      venue["longitude"] = new JValue(longitude);

      #endregion

      venues.Add(venue);
    }
    
    //NOTE: Currently, there are ONLY 30 MLB venues, so once they have all been loaded,
    //      there's no reason to continue to pull the venue from the events ...
    if(venues.Count() == 30)
    {
      break;
    }
  }
  
#region COMMENTED OUT: ORIGINAL R&D CODE
//
//  JObject content = JObject.Parse(source_response.Content);
//  JArray  events  = (JArray)content["events"];
//  
//  events.Dump("events", 0);
//  
//  JArray venues = new JArray();
//  
//  foreach(JObject venue in events.Children<JObject>())
//  {
//    venues.Add(venue["venue"]);
//  }
//
#endregion
  
  venues.Dump("venues", 0);
  venues.Select(v => (int)v["id"])
        .OrderBy(v => v)
        .Dump("venues.ids", 0);

#endregion

#region send data to destination ...

  var ryans_seatgeek_events    = "https://25b9-2601-40d-8100-8b0-a92f-f00a-322c-ec1c.ngrok.io/upload/api/endpoint/36824EA7-2ACE-46A6-82EC-B529511BBD09";
  var ryans_seatgeek_venues    = "https://25b9-2601-40d-8100-8b0-a92f-f00a-322c-ec1c.ngrok.io/upload/api/endpoint/F39C7BD6-EE14-48B0-BF11-2C60EC30EBDF";  
  //var ryans_mlbstats_venues    = "https://25b9-2601-40d-8100-8b0-a92f-f00a-322c-ec1c.ngrok.io/upload/api/endpoint/23A29E07-2987-4C0E-A371-2FE4DA771128";
  //var ryans_mlbstats_teams     = "https://25b9-2601-40d-8100-8b0-a92f-f00a-322c-ec1c.ngrok.io/uploadupload/api/endpoint/007A51AD-BB70-4AAE-B164-1F49ADD4AE79";
  //var ryans_mlbstats_rosters = "https://25b9-2601-40d-8100-8b0-a92f-f00a-322c-ec1c.ngrok.io/uploadupload/api/endpoint/";
  //var ryans_mlbstats_coaches = "https://25b9-2601-40d-8100-8b0-a92f-f00a-322c-ec1c.ngrok.io/uploadupload/api/endpoint/D3275E5B-6380-47C7-B0F8-242FC7DC75D2";
  
  var randels_seatgeek_events  = "http://127.0.0.1.nip.io:8888/upload/api/endpoint/DF02DA48-40A1-4DD1-86C4-55B8057B874A";
  var randels_seatgeek_venues  = "http://127.0.0.1.nip.io:8888/upload/api/endpoint/3931844F-227C-4FB8-B6C5-349771F003FA";
  var randels_mlbstats_venues  = "http://127.0.0.1.nip.io:8888/upload/api/endpoint/3C16D614-1A95-4B67-A127-1954DBF08FF3";
  var randels_mlbstats_teams   = "http://127.0.0.1.nip.io:8888/upload/api/endpoint/7A4959A8-5B89-46BA-9B3E-6B0A8C321746";
  var randels_mlbstats_rosters = "http://127.0.0.1.nip.io:8888/upload/api/endpoint/";
  
  //var michaels_seatgeek_events = "https://e7ec-174-20-157-135.ngrok.io/upload/api/endpoint/73ACA67F-B08D-462C-9FC0-D57D22BC1E5F";
  //var michaels_seatgeek_venues = "https://e7ec-174-20-157-135.ngrok.io/upload/api/endpoint/";

  var destination_client = new RestClient(ryans_seatgeek_venues);
  destination_client.Timeout = -1;

  var destination_request = new RestRequest(Method.POST);
  destination_request.AddHeader("Authorization", ryans_bearer_token);
  destination_request.AddHeader("Content-Type", "application/json");
  //destination_request.AddHeader("Cookie", "BrowserId=8TszxHy_Ee2idU0jA3cNiQ; CookieConsentPolicy=0:1; LSKey-c$CookieConsentPolicy=0:1");

//  IMPORTANT: build the request body and execute the POST
//  
//  var body = BuildQueryBody;
//  var body = events.ToString();
//  var body = venues.ToString();
//         
//  destination_request.AddParameter("application/json", body,  ParameterType.RequestBody);
//  
//  IRestResponse destination_response = destination_client.Execute(destination_request);
//  destination_response.Dump("destination response", 0);
//
//  JsonConvert.DeserializeObject(destination_response.Content)
////             .ToString()
//             .Dump("destination content", 0);

#endregion
}


private struct Meta
{
  public Meta(JObject meta)
  {
      total = (JValue)meta["total"];
      took  = (JValue)meta["took"];
      
      page     = (JValue)meta["page"];
      per_page = (JValue)meta["per_page"];
      
      geolocation = (JValue)meta["geolocation"];
  }
  
  public JValue total { get; set;}
  public JValue took  { get; set;}
  
  public JValue page      { get; set;}
  public JValue per_page  { get; set;}
  
  public JValue geolocation { get; set;}
}

private class LatitudeLongitude
{
  [JsonProperty("lat")]
  public decimal Latidude { get; set; }

  [JsonProperty("lon")]
  public decimal Longitude { get; set; }
}

#region Property(s)

private string basic_authentication
  => $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{client_id}:{app_secret}"))}";

private string add_client_id
  => $"client_id={client_id}";

private string add_client_secret
  => $"client_secret={app_secret}";

private string Recommendations(int preformer_id, int postal_code)
  => $"recommendations?performers.id={preformer_id}&postal_code={postal_code}";

private string nascar_url
  => $"{base_url}/{url_version}/{nascar}";

private string venues_url
  => $"{base_url}/{url_version}/{venues}";
//  => $"{base_url}/{url_version}/{venues}?client_id={client_id}";
//  => $"{base_url}/{url_version}/{events}?client_id={client_id}&client_secret={app_secret}";

private string events_url
  => $"{base_url}/{url_version}/{events}";
//  => $"{base_url}/{url_version}/{events}?client_id={client_id}";
//  => $"{base_url}/{url_version}/{events}?client_id={client_id}&client_secret={app_secret}";

private string base_url
  => "https://api.seatgeek.com";

private string url_version
  => "2";

private string venues
  => "venues";

private string events
  => "events";

private string nascar
  => "nascar";

private string client_id
  => "MzI3Mjc4ODZ8MTY4MDE5MTc2My4wMjc3MjE";
  
private string app_name
  => "LINQPad";
  
private string app_secret
  => "ad5480ceea057086cbcb856289835b1d8388675a5549ecf9416c0ddcac0ccf23";

private string michaels_bearer_token
  => $"Bearer {ryans_token}";

private string randels_bearer_token
  => $"Bearer {randels_token}";

private string ryans_bearer_token
  => $"Bearer {ryans_token}";

private string bearer_token
  => $"Bearer {token}";

private string token
  => throw new NotImplementedException();

private string michaels_token
  => "eyJhbGciOiJSUzI1NiIsImtpZCI6Ijk2NTBDMThGRUUxMDhBNjE3QTYzQTI3MzhGQkMwRjU3IiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2ODAyMDczOTUsImV4cCI6MTY4Mjg0MjU5NCwiaXNzIjoiaHR0cDovLzEyNy4wLjAuMS5uaXAuaW86OTAwMS8iLCJhdWQiOlsiU2VydmVyQXBpRm9yVUkiLCJQdWJsaWNBcGkiXSwiY2xpZW50X2lkIjoiUHVibGljQXBpQ2xpZW50Iiwicm9sZSI6IkFQSSIsIkNsaWVudElkIjoiaGFja2F0aG9uIiwiT3JnYW5pemF0aW9uSWQiOiJhNDgyN2Y0OS0xNDI4LTQ4ZDMtYWUxNy04NGQ2MjcyNWEzMzYiLCJPcmdhbml6YXRpb25OYW1lIjoiaGFja2F0aG9uIiwiT3JnYW5pemF0aW9uQ2xpZW50UmVmZXJlbmNlIjoiaGFja2F0aG9uIiwiSWQiOiJmZjFiYzVjMy03Yjk4LTRlMTktOTM0My01NTE0ZTQ5NWRiNWUiLCJhcGkiOiIyOTVmYTc5Mi1jYzljLTRlM2ItYmIzYy0xNWVhYzJhMDZjYTEiLCJqdGkiOiIwNTc1MEM2ODg2MDYyNTJDNDZCNUQ3MEI3QjVEOTYzRSIsImlhdCI6MTY4MDIwNzM5NSwic2NvcGUiOlsiUHVibGljQXBpIiwiU2VydmVyQXBpRm9yVUkiXX0.1dQIJp87lhsfXIe96h-_dnf3dVPogItnwDXq85KfdzPLNoe1kyQ3WRl8WdliKXzcrI61im1sHG7NDhGf5MuVhjNInm8FNViHhA-7XtGpjbD89Wg6zuNi9rCh5-Jym2vkGoeshzAieKF99icHQkdCxvFw5C-xmeBK0jX4CBA7GUVpR3qN81XxVVm61YiqDvt41zmyopYYVJpJPuuCwL6_sqWvEmkch42n1uAjHQ1V99bx8iOwjD_KIvnaRaaBxG0ankOpyKlVJ3eTpZmYkQu8DHwWORbHxX_IGMZUsX1NSkXPhKWOqr6C7Ffvl1MjpFPdDBydBUyEcPmp91NsfNDRow";

private string randels_token
  => "eyJhbGciOiJSUzI1NiIsImtpZCI6IjQ5MzQ2REVGRUM4OERFQjc3RDJFMUMwRURDMzBBNUI1IiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2ODAyMTAxMTMsImV4cCI6MTY4NTQzNzMxMiwiaXNzIjoiaHR0cDovLzEyNy4wLjAuMS5uaXAuaW86OTAwMS8iLCJhdWQiOlsiU2VydmVyQXBpRm9yVUkiLCJQdWJsaWNBcGkiXSwiY2xpZW50X2lkIjoiUHVibGljQXBpQ2xpZW50Iiwicm9sZSI6IkFQSSIsIkNsaWVudElkIjoid2F2ZXRlc3QiLCJPcmdhbml6YXRpb25JZCI6IjA5ZWFlNmIxLTIzOWQtNDZmZi1iOWRlLTIxMTkzNDUzMWQ3OSIsIk9yZ2FuaXphdGlvbk5hbWUiOiJ3YXZldGVzdCIsIk9yZ2FuaXphdGlvbkNsaWVudFJlZmVyZW5jZSI6IndhdmV0ZXN0IiwiSWQiOiI4YWEyZTQwYS0xYTc0LTRjZjEtODk4ZS1mYWQzYzcxN2E1ZGIiLCJhcGkiOiIyOTVmYTc5Mi1jYzljLTRlM2ItYmIzYy0xNWVhYzJhMDZjYTEiLCJqdGkiOiIxNUYzNDlCRkJBMzlFMUM3RUNBMDRDRDk3M0QzMEExMCIsImlhdCI6MTY4MDIxMDExMywic2NvcGUiOlsiUHVibGljQXBpIiwiU2VydmVyQXBpRm9yVUkiXX0.M5cST2VhE64tb75Up2YgVqnL810I_GtjAXjVZ66Gn2KGnMXaY_W-97_uHdbmMAFbvV8_pmyNKHrJZU_viJQR7qa92npBeAmf02_e0Qu27MD_Kj9efzuRXMDbj_q1Zm-_pou0PIh7u5Hw_T7GyfQ2NYbFjeM5SFDotY9Cun739IWjP509o8g0mF6vPba8HyXCwaDh7jAovrQK3Zbpxvi40hODcSLUNNnKKp-SmJkUD7W_HPST43B9gCbzQcTdf6DF0mXJICtuikXiWJ5TebsNlJfqivUR5qKofX09Mosnlxf5GiutUFgfvbDyBXO_dE2H0ehwXN6hD2uOIh1Ca6SlXQ";

private string ryans_token
  => "eyJhbGciOiJSUzI1NiIsImtpZCI6IkZCMTY0RDM4QzgwQzYxMjE0OUI4RUNBRUZGNjdENjdEIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2ODAyNzkxOTksImV4cCI6MTY4ODA5ODM5OCwiaXNzIjoiaHR0cDovLzEyNy4wLjAuMS5uaXAuaW86OTAwMS8iLCJhdWQiOlsiU2VydmVyQXBpRm9yVUkiLCJQdWJsaWNBcGkiXSwiY2xpZW50X2lkIjoiUHVibGljQXBpQ2xpZW50Iiwicm9sZSI6IkFQSSIsIkNsaWVudElkIjoid2F2ZXRlc3QiLCJPcmdhbml6YXRpb25JZCI6IjEyMDI0Zjg5LWFmOGUtNDJkNy05MGFkLWQwOTU4NThjZmVjMiIsIk9yZ2FuaXphdGlvbk5hbWUiOiJ3YXZldGVzdCIsIk9yZ2FuaXphdGlvbkNsaWVudFJlZmVyZW5jZSI6IndhdmV0ZXN0IiwiSWQiOiIxNjNjNWRkOS00NmNiLTRiOGYtYTgwZS01YmZiYjc3ZjI3NzEiLCJhcGkiOiIyOTVmYTc5Mi1jYzljLTRlM2ItYmIzYy0xNWVhYzJhMDZjYTEiLCJqdGkiOiIxRDIwQjZFM0M0NjNFRTAyMERFMkJDMjZBMTM0RUQzNCIsImlhdCI6MTY4MDI3OTE5OSwic2NvcGUiOlsiUHVibGljQXBpIiwiU2VydmVyQXBpRm9yVUkiXX0.aRG53XWLvvfYfV4IU3YUDAZiCeiN6CDmGbY1u8BDTmPWxL9n0MpHpXIrRawYyiSDlKHbz6R8csfKKog8EvITRvYoRMX7txwKbNt28zZuwk2-RxUzlDJ_1Np_3ov5YOsy7urfHCxI4U-6wOx3Rw6MSiM9D7orCKb9y5oVFWEyD_O9rQcJtZP6PwgYz6g3M9sWMw-LDKDJfWIT3eRhF8O9i5_H83XicvuPWp0YXgka6ZmZyuzlOx5xNfgdhyatH6x9ptIlBq4XV2S8wYAcJvScyn5wesT-gAHbosHA0hzJqJc6UDSyX720isH3T_SMMIqxX0BLmNz3-JTW1j4030j7WA";

private static string BuildQueryBody
  => @"{"
   + @"  ""operation"" : ""query"""
   + @" ,""query"" : ""SELECT Id FROM Account WHERE VeevaID_vod__c != NULL ORDER BY Id ASC"""
   + @" ,""contentType"" : ""CSV"""
   + @" ,""columnDelimiter"" : ""PIPE"""
   + @" ,""lineEnding"" : ""CRLF"""
   + @"}";

#endregion