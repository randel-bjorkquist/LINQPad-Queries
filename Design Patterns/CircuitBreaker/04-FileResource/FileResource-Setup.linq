<Query Kind="Program">
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "../_Shared/Models/FileResourceState.linq"
#load "../_Shared/FileResourceStateStore.linq"

async Task Main()
{
	// ── Write the seed resource file ──────────────────────────
	var resourcePath = ResourceFilePath();
	var statePath    = StateFilePath();

	var resource = new GenericResource { ResourceName = "GenericResource"
                                      ,Version      = "1.0.0"
                                      ,LoadedAt     = DateTime.UtcNow.ToString("o")
                                      ,Settings     = new Dictionary<string, object> { // These settings are intentionally generic.
                                                                                	   	 // In production this could be:
                                                                                	   	 //  - Pricing rules:   { "BaseRate": 1.25, "TaxRate": 0.08 }
                                                                                	   	 //  - Tenant config:   { "MaxUsers": 100, "AllowExport": true }
                                                                                	   	 //  - Report settings: { "PageSize": "A4", "Orientation": "Portrait" }
                                                                                	   	 ["MaxRetries"]      = 3
                                                                                      ,["TimeoutSeconds"]  = 30
                                                                                      ,["EnableCaching"]   = true}
                                                                                      , Items = new List<ResourceItem> { new() { Key = "Item1", Value = "Value1", Enabled = true  }
                                                                                                                        ,new() { Key = "Item2", Value = "Value2", Enabled = true  }
                                                                                                                        ,new() { Key = "Item3", Value = "Value3", Enabled = false }}};

	var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
	File.WriteAllText(resourcePath, JsonSerializer.Serialize(resource, jsonOptions));
  
	$"Resource file written to: {resourcePath}".Dump("Setup");

	// ── Initialize state file ─────────────────────────────────
	var store = new FileResourceStateStore(statePath);
	store.Load();

	store.UpdateState( resourceName:   "GenericResource"
                    ,resourcePath:   resourcePath
                    ,circuitState:   "Closed"
                    ,failureMode:    "None"
                    ,	failureCount:  0
                    ,cacheAvailable: false
                    ,message:        "Initialized" );

	store.GetAll()
       .Dump("Initial Circuit State");

	File.ReadAllText(resourcePath)
      .Dump("Resource File on disk");
      
	File.ReadAllText(statePath)
      .Dump("State File on disk — Initial");
}

string ResourceFilePath()
{
	var queriesRoot        = Path.GetDirectoryName(Util.CurrentQueryPath);
	var circuitBreakerRoot = Path.GetFullPath(Path.Combine(queriesRoot, ".."));
	var dataPath           = Path.Combine(circuitBreakerRoot, "_Data", "FileResource");
  
	Directory.CreateDirectory(dataPath);
  
	return Path.Combine(dataPath, "resource.json");
}

string StateFilePath()
{
	var queriesRoot        = Path.GetDirectoryName(Util.CurrentQueryPath);
	var circuitBreakerRoot = Path.GetFullPath(Path.Combine(queriesRoot, ".."));
	var dataPath           = Path.Combine(circuitBreakerRoot, "_Data", "FileResource");
  
	Directory.CreateDirectory(dataPath);
  
	return Path.Combine(dataPath, "fileresource-state.json");
}
