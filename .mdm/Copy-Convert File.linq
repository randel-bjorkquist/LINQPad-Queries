<Query Kind="Program" />

void Main()
{
  var file_path = @"C:\temp\misc\springworkstx_uat_auth.pfx";
  var save_path = @"C:\temp\misc\springworkstx_uat_.base64.auth.tex";
  
  using(FileStream fs = File.OpenRead(file_path))
  {
    var ms = new MemoryStream();
    fs.CopyTo(ms);
    
    var bytes  = ms.ToArray();
    var base64 = Convert.ToBase64String(bytes);
    
    File.WriteAllText(save_path, base64);
  }
}