<Query Kind="Program">
  <Namespace>System.Collections.Immutable</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>System.Text.Json</Namespace>
</Query>

//-------------------------------------------------------------------------------------------------
#load ".\TypedEnum"
#load ".\EventType"

//-------------------------------------------------------------------------------------------------
void Main()
{
  //-----------------------------------------------------------------------------------------------
  var event_type = EventType.InspectionSaved;
  event_type.Dump($"var event_type = mcsEventType.InspectionSaved;", 0);
  
  // implicit conversion of int '1' to 'mcsEventType' - specifically to 'mcsEventType.InspectionSaved'
  var dbEventTypeID = 1;
  var db_event_type = (EventType)dbEventTypeID;
  db_event_type.Dump($"var dbEventTypeID = 1;{Environment.NewLine}var db_event_type = (mcsEventType)dbEventTypeID;", 0);
  
  //-----------------------------------------------------------------------------------------------
  (event_type == db_event_type).Dump("event_type == db_event_type");
  (event_type != db_event_type).Dump("event_type != db_event_type");
  
  //-----------------------------------------------------------------------------------------------
  // implicit conversion of 'mcsEventType.InspectionSaved' to int | ID → 1
  int id = EventType.InspectionSaved;
  id.Dump("int id = mcsEventType.InspectionSaved;");

  // 'mcsEventType.InspectionSaved.ID' → 1
  id = EventType.InspectionSaved.ID;
  id.Dump("int id = mcsEventType.InspectionSaved.ID;");
  
  // implicit conversion of 'mcsEventType.InspectionSaved' to string | Description → "Inspection Saved"
  string description = EventType.InspectionSaved;
  description.Dump("string description = mcsEventType.InspectionSaved;");

  // 'mcsEventType.InspectionSaved.Description' → "Inspection Saved"
  description = EventType.InspectionSaved.Description;
  description.Dump("string description = mcsEventType.InspectionSaved.Description;");
  
  // 'mcsEventType.InspectionSaved.Code' → "InspectionSaved"
  string code = EventType.InspectionSaved.Code;
  code.Dump("string code = mcsEventType.InspectionSaved.Code;");

  //-----------------------------------------------------------------------------------------------
  // Safe lookup
  var event_type_by_id = EventType.GetByID(2);
  event_type_by_id.Dump("var event_type_by_id = mcsEventType.GetByID(2);", 0);

  // All values (for dropdowns, etc.)
  var all_event_types = EventType.GetAll();  // IReadOnlyList<mcsEventType>
  all_event_types.Dump("var all_event_types = mcsEventType.GetAll();", 0);
  
  var descriptions = all_event_types.Select(e => e.Description).Distinct().Order().ToList();
  descriptions.Dump("var descriptions = all_event_types.Select(e => e.Description).Distinct().Order().ToList();", 0);
  
  var codes = all_event_types.Select(e => e.Code).Distinct().Order().ToList();
  codes.Dump("var codes = all_event_types.Select(e => e.Code).Distinct().Order().ToList();", 0);
  
  //-----------------------------------------------------------------------------------------------
  // ToString()s ....
  EventType.VacatedTenantSaved.ToString("D").Dump("mcsEventType.VacatedTenantSaved.ToString(\"D\") => D = 'Description'");
  EventType.VacatedTenantSaved.ToString("C").Dump("mcsEventType.VacatedTenantSaved.ToString(\"C\") => C = 'Code (field name)'");
  EventType.VacatedTenantSaved.ToString("I").Dump("mcsEventType.VacatedTenantSaved.ToString(\"I\") => I = 'ID as string'");
  EventType.VacatedTenantSaved.ToString("F").Dump("mcsEventType.VacatedTenantSaved.ToString(\"F\") => F = 'Full/Verbose Format'");
  EventType.VacatedTenantSaved.ToString("G").Dump("mcsEventType.VacatedTenantSaved.ToString(\"G\") => G = 'General/short + id'");
  EventType.VacatedTenantSaved.ToString("f").Dump("mcsEventType.VacatedTenantSaved.ToString(\"f\") => f = 'alternative Full Format'");
  EventType.VacatedTenantSaved.ToString("g").Dump("mcsEventType.VacatedTenantSaved.ToString(\"g\") => g = 'alternative General'");
  
  // AsJsonString()
  EventType.VacatedTenantSaved.AsJsonString().Dump("mcsEventType.VacatedTenantSaved.AsJsonString()");
  
  // AsJsonObject()
  EventType.VacatedTenantSaved.AsJsonObject().Dump("mcsEventType.VacatedTenantSaved.AsJsonObject()");
}
