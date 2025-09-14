<Query Kind="Program" />

void Main()
{
  StringBuilder htmlBuilder = new StringBuilder();
  
  htmlBuilder.Append("<html>");
  htmlBuilder.Append("<head>");
  htmlBuilder.Append("<title>My HTML Template</title>");
  htmlBuilder.Append("</head>");
  htmlBuilder.Append("<body>");
  htmlBuilder.Append("<h1>This is a simple heading.</h1>");
  htmlBuilder.Append("<p>This is a simple paragraph.</p>");
  htmlBuilder.Append("</body>");
  htmlBuilder.Append("</html>");
  
  string htmlTemplate = htmlBuilder.ToString();
  Console.WriteLine(htmlTemplate);
  
  htmlBuilder.Clear();
  Console.WriteLine();
  
  htmlBuilder.AppendLine("<html>")
             .AppendLine("<head>")
             .AppendLine("<title>My HTML Template</title>")
             .AppendLine("</head>")
             .AppendLine("<body>")
             .AppendLine("<h1>This is a simple heading.</h1>")
             .AppendLine("<p>This is a simple paragraph.</p>")
             .AppendLine("</body")
             .AppendLine("</html>");

  htmlTemplate = htmlBuilder.ToString();
  Console.WriteLine(htmlTemplate);

  htmlBuilder.Clear();
  Console.WriteLine();
  
  var html = A.HtmlTemplateBuilder
              .AddDocType()
              .OpenHtml()
              .OpenBody()
              .AddHeader("This is a simple heding.")
              .AddParagraph("This is a simple paragraph")
              .AddList(new[] {"one", "two", "three"})
              .AddLineBreak()
              .AddRaw("<hr>")
              .CloseBody()
              .CloseHtml()
              .ToString();
              
  Console.WriteLine(html);
  
  html = A.HtmlElementBuilder("html")
          .AddChild(A.HtmlElementBuilder("body")
          .AddChild(A.HtmlElementBuilder("h1").WithText("Welcome"))
          .AddChild(A.HtmlElementBuilder("p").WithText("This is paragraph 1."))
          .AddChild(A.HtmlElementBuilder("p").WithText("This is paragraph 2.")))
          .ToString();

  Console.WriteLine();
  Console.WriteLine(html);

  var list = A.HtmlElementBuilder("ul")
              .AddChild(A.HtmlElementBuilder("li").WithText("One"))
              .AddChild(A.HtmlElementBuilder("li").WithText("Two"))
              .AddChild(A.HtmlElementBuilder("li").WithText("Three"));
  
  var page = A.HtmlTemplateBuilder
              .AddDocType()
              .OpenHtml()
              .OpenBody()
              .AddHeader("Welcome")
              .AddParagraph("This is a test message, in a paragraph")
              .AddRaw(list)
              .CloseBody()
              .CloseHtml()
              .ToString();
  
  Console.WriteLine();
  Console.WriteLine(page);
}


public static class A
{
  public static HtmlElementBuilder HtmlElementBuilder(string tag) => HtmlBuilderFactory.HtmlElementBuilder(tag);
  public static HtmlTemplateBuilder HtmlTemplateBuilder => HtmlBuilderFactory.HtmlTemplateBuilder;
}

public static class An
{
  public static HtmlElementBuilder HtmlElementBuilder(string tag) => HtmlBuilderFactory.HtmlElementBuilder(tag);  
}

public static class HtmlBuilderFactory
{
  public static HtmlElementBuilder HtmlElementBuilder(string tag) => new HtmlElementBuilder(tag);
  public static HtmlTemplateBuilder HtmlTemplateBuilder => new HtmlTemplateBuilder();
}

public class HtmlTemplateBuilder
{
  private readonly StringBuilder _sb = new();

  public HtmlTemplateBuilder OpenHtml()   => AddTag("<html>");
  public HtmlTemplateBuilder CloseHtml()  => AddTag("</html>");

  public HtmlTemplateBuilder OpenBody()   => AddTag("<body>");
  public HtmlTemplateBuilder CloseBody()  => AddTag("</body>");
  
  public HtmlTemplateBuilder AddDocType()
  {
    _sb.AppendLine("<!DOCTYPE html>");
    return this;
  }
  
  public HtmlTemplateBuilder AddHeader(string text, int level = 1)
  {
    level = Math.Clamp(level, 1, 6);
    _sb.AppendLine($"<header>{text}</h{level}>");
    return this;
  }

  public HtmlTemplateBuilder AddRaw(string html)
  {
    _sb.AppendLine(html);
    return this;
  }
  
  public HtmlTemplateBuilder AddTag(string tag)
  {
    _sb.AppendLine(tag);
    return this;
  }

  public HtmlTemplateBuilder AddList(IEnumerable<string> items, bool ordered = false)
  {
    var tag = ordered ? "ol" : "ul";
    _sb.AppendLine($"<{tag}>");
    
    foreach(var item in items)
    {
      _sb.AppendLine($"  <li>{item}</li>");
    }

    _sb.AppendLine($"</{tag}>");

    return this;
  }

  public HtmlTemplateBuilder AddLineBreak()
  {
    _sb.AppendLine("<br>");
    return this;
  }

  public HtmlTemplateBuilder AddParagraph(string text)
  {
    _sb.AppendLine($"<p>{text}</p>");
    return this;
  }

  public override string ToString()
    => _sb.ToString();

  public static implicit operator String(HtmlTemplateBuilder builder) 
    => builder.ToString();
}

public class HtmlElementBuilder
{
  public string TagName { get; }
  
  public List<HtmlElementBuilder> Children { get; } = new();
  public Dictionary<string, string> Attributes { get; } = new();

  public string? InnerText { get; set; }

  public HtmlElementBuilder(string tagName)
  {
    TagName = tagName;
  }

  public HtmlElementBuilder AddChild(HtmlElementBuilder child)
  {
    Children.Add(child);
    return this;
  }

  public HtmlElementBuilder WithText(string text)
  {
    InnerText = text;
    return this;
  }

  public HtmlElementBuilder WithAttribute(string name, string value)
  {
    Attributes[name] = value;
    return this;
  }

  public override string ToString()
  {
    var sb = new StringBuilder();
    sb.Append('<').Append(TagName);

    foreach (var attr in Attributes)
      sb.Append($" {attr.Key}=\"{attr.Value}\"");

    sb.Append('>');

    if (!string.IsNullOrWhiteSpace(InnerText))
      sb.Append(InnerText);

    foreach (var child in Children)
      sb.Append(child.ToString());

    sb.Append($"</{TagName}>");
    return sb.ToString();
  }

  public static implicit operator string(HtmlElementBuilder builder)
      => builder.ToString();
}
