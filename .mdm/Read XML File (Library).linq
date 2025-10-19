<Query Kind="Program" />

//---------------------------------------------------------------------------------------------
// resource url: https://www.encodedna.com/linq/using-linq-contains-method-to-do-sql-like.htm
//
void Main()
{
  var current_path    = Path.GetDirectoryName( Util.CurrentQueryPath );
  var data_files_path = Path.Combine(current_path, "data_files");
  var xml_file        = Path.Combine( data_files_path, "Library.xml" );
  
  var library = XElement.Load(xml_file);
  //library.Dump();

  var search_value = "01";
  
  //Query Expression ---------------------------------------------------------------
  //var search_date  = from xFi in library.Descendants("List")
  //                   where xFi.Element("ReleaseDate").Value.Contains(search_value)
  //                   select xFi;
  
  //Lambda Expression --------------------------------------------------------------
  var search_date  = library.Descendants("List")
                            .Where(e => e.Element("ReleaseDate")
                                         .Value
                                         .Contains(search_value));
  search_date.Dump();
}

// You can define other methods, fields, classes and namespaces here