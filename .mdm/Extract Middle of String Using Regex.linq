<Query Kind="Statements" />

string input = "--begin-- middle_text_goes_here and more text_here --end--";
string beginMarker = "--begin--";
string endMarker = "--end--";

string pattern = $"{Regex.Escape(beginMarker)}(.*?){Regex.Escape(endMarker)}";
Match match = Regex.Match(input, pattern);

if (match.Success)
{
  string middleText = match.Groups[1].Value;
  Console.WriteLine(middleText);
}
else
{
  Console.WriteLine("No valid middle text found.");
}