<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <RemoveNamespace>System.Xml</RemoveNamespace>
  <RemoveNamespace>System.Xml.Linq</RemoveNamespace>
  <RemoveNamespace>System.Xml.XPath</RemoveNamespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var languages = GetSupportedLanguages();
	FormatJson4Printing(languages).Dump("GetSupportedLanguages()");

	var translation = TranslateText("Frog");
	FormatJson4Printing(translation).Dump("TranslateText(\"Frog\")");
	
	var transliterate = TransliterateText("", "ar", "Arab", "latn");
	FormatJson4Printing(transliterate).Dump("TransliterateText(\"\", \"ar\", \"Arab\", \"latn\")");
	
	var language = IdentifyLanguage("hallo");
	FormatJson4Printing(language).Dump("IdentifyLanguage(\"hallo\")");
}

//NOTE: this needs to be a valid Azure service subscription key
public static string subscription_key = "e0aeab08721e469ca0b8835c155bba1a";
public static string base_url 				= "https://api.cognitive.microsofttranslator.com";

public static string GetSupportedLanguages()
{
	var route = "/languages?api-version=3.0";
	
	return GET(route);
}

public static string TranslateText(string string2translate)
{
	var route 		 = "/translate?api-version=3.0";
	var parameters = "&to=de&to=it&to=fr";  //de = German, it = Italian, fr = French
	
	var body = new System.Object[] { new { Text = string2translate } };
		
  return POST(route, parameters, body);
}

public static string TransliterateText(string string2translate, string language, string from, string to)
{
	var route = "/transliterate?api-version=3.0";
	var parameters = $"&language={language}&fromScript={from}&toScript={to}";
	
	var body = new System.Object[] { new { Text = string2translate } };
		
  return POST(route, parameters, body);
}

public static string IdentifyLanguage(string string2identity)
{
	var route = "/detect?api-version=3.0";
	var parameters = "";

	var body = new System.Object[] { new { Text = string2identity } };

	return POST(route, parameters, body);
}

public static string GET(string route)
{
	return MakeWebCall(route, "", null, HttpMethod.Get);
}

public static string POST(string route, string parameters, Object[] body)
{
	return MakeWebCall(route, parameters, body, HttpMethod.Post);
}

private static string MakeWebCall(string route, string parameters, Object[] body, HttpMethod method)
{
	using(var client  = new HttpClient())
	using(var request = new HttpRequestMessage())
	{
		request.Method 		 = method;
		request.RequestUri = new Uri($"{base_url}{route}{parameters}");
		
		request.Headers.Add("Ocp-Apim-Subscription-Key", subscription_key);
		
		request.Content = method == HttpMethod.Post
										? new StringContent( JsonConvert.SerializeObject(body)
																				,Encoding.UTF8
																				,"application/json")
										: null;
		
		var response = client.SendAsync(request)
												 .Result;

		return response.Content
									 .ReadAsStringAsync()
									 .Result;
	}	
}

public static string FormatJson4Printing(string string2format)
{
	return JsonConvert.SerializeObject( JsonConvert.DeserializeObject(string2format)
																		 ,Formatting.Indented );
}