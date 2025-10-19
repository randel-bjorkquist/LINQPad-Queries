<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main(string[] args)
{
  ProcessRepositories(_client).GetAwaiter()
                              .GetResult()
                              .Dump("repository(s)", 0);
                              
  ProcessContents(_client).GetAwaiter()
                          .GetResult()
                          .Dump("content(s)", 0);
}

private static readonly HttpClient _client = new HttpClient();

//Resourced URL: https://github.com/dotnet/samples/tree/main/csharp/getting-started/console-webapiclient
private static async Task<List<Repository>> ProcessRepositories(HttpClient client)
{
  client.DefaultRequestHeaders.Accept.Clear();
  client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
  client.DefaultRequestHeaders.Add("User-Agent"
                                   ,".NET Foundation Repository Reporter");

  var streamTask    = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
  var repositories  = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
  
  return repositories;
}

public class Repository {
  [JsonPropertyName("name")]
  public string Name                { get; set; }

  [JsonPropertyName("description")]
  public string Description         { get; set; }

  [JsonPropertyName("html_url")]
  public Uri GitHubHomeUrl          { get; set; }

  [JsonPropertyName("homepage")]
  public Uri Homepage               { get; set; }

  [JsonPropertyName("watchers")]
  public int Watchers               { get; set; }

  [JsonPropertyName("pushed_at")]
  public string JsonDate            { get; set; }

  public DateTime LastPush =>
      DateTime.ParseExact( JsonDate
                          ,"yyyy-MM-ddTHH:mm:ssZ"
                          ,CultureInfo.InvariantCulture );
}

//Resource URL: https://gist.github.com/kbrammer/5512319
private static async Task<IEnumerable<Content>> ProcessContents(HttpClient client)
{
  client.DefaultRequestHeaders.Accept.Clear();
  client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
  
  var stringTask = client.GetStringAsync("https://api.github.com/repos/kbrammer/kevinbrammer.azurewebsites.net/contents");
  var contents   = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType<IEnumerable<Content>>(await stringTask, Enumerable.Empty<Content>());
  
  return contents;
}

public class Links
{
  public string self      { get; set; }
  public string git       { get; set; }
  public string html      { get; set; }
}

public class Content
{
  public string sha       { get; set; }
  public string size      { get; set; }
  
  public string name      { get; set; }
  public string path      { get; set; }
  public string type      { get; set; }
  
  public string url       { get; set; }
  public string git_url   { get; set; }
  public string html_url  { get; set; }
  
  public Links _links     { get; set; }
}