<Query Kind="Program" />

void Main()
{
  DateTime.Now.Date.Dump("DateTime.Now.Date");
  DateTime.Now.Date.ToShortDateString().Dump("DateTime.Now.Date.ToShortDateString()");
  
  DateTime.Now.ToString("MMddyyy").Dump("DateTime.Now.ToString(\"MMddyyy\")");
  DateTime.Now.Date.ToString("MMddyyy").Dump("DateTime.Now.Date.ToString(\"MMddyyy\")");

  $"{DateTime.Now:MMddyyy}".Dump("DateTime.Now:MMddyyy");
}


