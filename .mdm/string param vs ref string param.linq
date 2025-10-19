<Query Kind="Program" />

void Main()
{
	string field_name = "original_field";
	
	$"Main -> original field_name value = '{field_name}' \n".Dump();
	
	//DOES NOT CHANGE the value of field_name within the scope of 'Main'
	StringParameterTest(field_name);

	$"Main -> returned field_name value = '{field_name}' \n".Dump();
	
	//DOES CHANGE the value of field_name within the scope of 'Main'
	StringParameterTest(ref field_name);
	
	$"Main -> returned field_name value = '{field_name}' \n".Dump();
}

// You can define other methods, fields, classes and namespaces here
public static void StringParameterTest(string field_name)
{
	$"StringParameterTest -> original field_name value = '{field_name}'".Dump();

	field_name = "new_field";
	
	$"StringParameterTest -> modified field_name value = '{field_name}'".Dump();
	"".Dump();
}

// You can define other methods, fields, classes and namespaces here
public static void StringParameterTest(ref string field_name)
{
	$"StringParameterTest -> original field_name value: '{field_name}'".Dump();

	field_name = "new_field";

	$"StringParameterTest -> modified field_name value: '{field_name}'".Dump();
	"".Dump();
}
