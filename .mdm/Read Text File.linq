<Query Kind="Program" />

void Main()
{
  var file_path = @"C:\repos\CluedIn\SalesforceIDs.csv";
  //var data = File.ReadLines(file_path);
  //data.Dump();
  
  //var file_stream = new FileStream(file_path, FileMode.Open);
  //while(file_stream.BeginRead())
  //{
  //  
  //}
  
  var ids = new List<string>();
  
  using(var file = File.OpenText(file_path))
  {
    while(!file.EndOfStream) 
    {
      var line = file.ReadLine();
      
      if(line.Length > 0)
      {
        ids.Add(line.Trim().Replace("\"", ""));
      }
    }
  }

  ids.Dump();
  
}

// You can define other methods, fields, classes and namespaces here