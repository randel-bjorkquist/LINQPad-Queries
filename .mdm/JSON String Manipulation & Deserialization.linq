<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
</Query>

void Main()
{  
  #region string manipulation
  
  var location = "{\"lat\":42.0679,\"lon\":-84.2449}".Dump("location");

  Console.WriteLine();

  location = location.Replace("{", "");
  location = location.Replace("}", "");
  
  //location.Dump("location - after strippng braces '{' & '}'");
  //Console.WriteLine();

  //var coordinates = location.Split(',').Dump("location.Split(',')", 0);
  var coordinates = location.Split(',');

  $"coordinates[0].Split(\":\")[1]: {coordinates[0].Split(":")[1]}".Dump();
  $"coordinates[1].Split(\":\")[1]: {coordinates[1].Split(":")[1]}".Dump();

  //var x = coordinates.Select(ll => ll.Split(':'));
  //x.Dump();

  Console.WriteLine();

  var lat = coordinates[0];
  var lon = coordinates[1];

  $"lat.Split(\":\")[1]: {lat.Split(":")[1]}".Dump();
  $"lon.Split(\":\")[1]: {lon.Split(":")[1]}".Dump();
  
  Console.WriteLine();
  
  #endregion

  var latitude_longitude = "{\"lat\":42.0679,\"lon\":-84.2449}";
  var coordinances = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(latitude_longitude)
                                .Dump("JsonConvert.DeserializeObject<Dictionary<string, decimal>>(latitude_longitude)", 0);  
  $"coordinances[\"lat\"].ToString(): {coordinances["lat"].ToString()}".Dump();
  $"coordinances[\"lon\"].ToString(): {coordinances["lon"].ToString()}".Dump();

  Console.WriteLine();

  var lat_lon = JsonConvert.DeserializeObject<LatitudeLongitude>(latitude_longitude)
                           .Dump("JsonConvert.DeserializeObject<LatitudeLongitude>(latitude_longitude)", 0);
  $"lat_lon.Latidude: {lat_lon.Latidude}".Dump();
  $"lat_lon.Longitude: {lat_lon.Longitude}".Dump();
}

private class LatitudeLongitude
{
  [JsonProperty("lat")]
  public decimal Latidude {get; set;}

  [JsonProperty("lon")]
  public decimal Longitude { get; set; }
}
