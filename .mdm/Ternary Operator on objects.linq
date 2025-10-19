<Query Kind="Program" />

void Main()
{
	var id = new Guid("ca0a85b3-a61c-4164-bd3b-03447b6cafd3");	// VALID GUID
//	var id = new Guid("0a957233-3026-4acc-a03c-7c0f4f9005b9");	// VALID GUID
//	var id = new Guid("c3d8f729-93ac-47a1-b59b-b846c433b9b9");	// VALID GUID
//	var id = new Guid("335a5fb6-75bd-451e-b30d-cf14da683086");	// VALID GUID
//	var id = new Guid("fa5a0d09-4884-408f-bea5-29576bbb3278");	// INVALID GUID
	
	var command = DataStore.Records
												 .Select(r => new Command{ MessageID 	 = r.ID 
												 													,MessageText = r.Message })
												 .FirstOrDefault(r => r.MessageID == id);

// EXAMPLE: no reason to have 'null' in the false location; simply create the new object
//	var message = command?.MessageID.HasValue ?? false
//							? DataStore.GetByID(command.MessageID)
//							: null
//							?? new GDPRMessage{ ID = new Guid("fa5a0d09-4884-408f-bea5-29576bbb3278")
//																 ,Message = "blah blah blah" };
	
	var message = command?.MessageID.HasValue ?? false
							? DataStore.GetByID(command.MessageID)
							: new GDPRMessage{ ID = new Guid("fa5a0d09-4884-408f-bea5-29576bbb3278")
																,Message = "blah blah blah" };
	message.Dump();
}

public class Command
{
	public Guid? MessageID 		{get; set;}
	public string MessageText {get; set;}
}

public class GDPRMessage
{
	public Guid ID 						{get; set;}
	public string Message 		{get; set;}
}

public static class DataStore
{
	public static IList<GDPRMessage> Records 
		= new List<GDPRMessage> { new GDPRMessage{ ID 				= new Guid("ca0a85b3-a61c-4164-bd3b-03447b6cafd3")
														 								  ,Message   = "Fred Flintstone" }
														 ,new GDPRMessage{ ID         = new Guid("0a957233-3026-4acc-a03c-7c0f4f9005b9")
															 							  ,Message   = "Barney Rubble" }
														 ,new GDPRMessage{ ID         = new Guid("c3d8f729-93ac-47a1-b59b-b846c433b9b9")
														 							    ,Message   = "ScoobyDo" }
														 ,new GDPRMessage{ ID         = new Guid("335a5fb6-75bd-451e-b30d-cf14da683086")
														 							    ,Message   = "Marvin the Martian" }
														 							 };
	
	public static GDPRMessage GetByID(Guid? id)
	{
		if(id is null)
		{
			return null;
		}
		
		return Records.SingleOrDefault(r => r.ID == id);
	}
}