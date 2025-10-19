<Query Kind="Program" />

void Main()
{
  var url = "http://" +
            "www.emoryhealthcare.org/locations/hospitals/emory-university-hospital-midtown/index.html"
//            "this is not a valid web address you dumby"
//            .Replace("/", "//")
            .Dump("url");
  
  var uri = new Uri(url, UriKind.RelativeOrAbsolute);
//  var uri = new Uri(url, UriKind.Absolute);
//  var uri = new Uri(url, true);
//  var uri = new Uri(url, false);
//  var uri = new Uri(url, System.UriCreationOptions option);
//  var uri = new Uri(url);
  
  uri.Dump("uri");
}
