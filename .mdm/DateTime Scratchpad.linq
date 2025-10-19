<Query Kind="Program" />

void Main()
{
	//NOTE: The property "Kind" of the DatTime type, is of 'DateTimeKind' type, 
	//			which has the values: Local, Utc, Unspecified	

	Console.WriteLine("----------------------------------------------------------");
	
	//NOTE: Because now.Kind is set to 'Utc', there's nothing for the method 'ToUniversalTime' to do, 
	//			so I'm assuming it just returns the current value
	var utc_now = DateTime.UtcNow;
	utc_now.Dump("utc_now");
	
	var utc_now_temp = utc_now.ToUniversalTime();
	utc_now_temp.Dump("utc_now_temp");

	//Because now.Kind is set to 'Local', the method 'ToUniversalTime' will convert it to UTC
	var now = DateTime.Now;
	now.Dump("now");
	
	var now_temp = now.ToUniversalTime();
	now_temp.Dump("now_temp");
	
	Console.WriteLine("----------------------------------------------------------");
	
	new DateTime(1753, 1, 1, 0, 0, 0, DateTimeKind.Utc).Dump("new DateTime(1753, 1, 1, 0, 0, 0)");

	new DateTime(1753, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToString("o")
																	 									 .Dump("new DateTime(1753, 1, 1, 0, 0, 0).ToString(\"o\")");
	
	Console.WriteLine("----------------------------------------------------------");
	
	DateTime.UtcNow.Dump("DateTime.UtcNow");
	DateTime.UtcNow.ToString("o").Dump("DateTime.UtcNow.ToString(\"o\")");
	DateTime.Now.ToUniversalTime().Dump("DateTime.Now.ToUniversalTime()");

	Console.WriteLine("----------------------------------------------------------");

	DateTime.MinValue.Dump("DateTime.MinValue");
	DateTime.MinValue.ToUniversalTime().Dump("DateTime.MinValue.ToUniversalTime()");
	
	Console.WriteLine("----------------------------------------------------------");	
	
//	new DateTime(1753, 1, 1, 0, 0, 0).ToUniversalTime()
//				.ToString("o")
//				.Dump("new DateTime(1753, 1, 1, 0, 0, 0).ToUniversalTime()");
//
//	new DateTime(1753, 1, 1, 0, 0, 0, DateTimeKind.Utc)
//				.ToUniversalTime()
//				.ToString("o")
//				.Dump("new DateTime(1753, 1, 1, 0, 0, 0).ToUniversalTime()");

}
