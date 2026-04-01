<Query Kind="Program">
  <Namespace>Microsoft.Data.SqlClient</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#r "nuget: Microsoft.Data.SqlClient, 5.2.2"

#load "../_Shared/Models/DatabaseState.linq"
#load "../_Shared/DatabaseStateStore.linq"

async Task Main()
{
	// ── Connection definitions ────────────────────────────────
	var connections = new[]
	{
		new { LogicalName = "PHA-Auth-DB",  Database = "PHA-Auth_Dev" },
		new { LogicalName = "PHA-Web-DB",   Database = "PHA-Web_Dev"  }
	};

	// ── Verify connectivity ───────────────────────────────────
	"Verifying database connectivity...".Dump("Setup");

	foreach (var conn in connections)
	{
		try
		{
			var connStr = BuildConnectionString(conn.Database);
			using var sql = new SqlConnection(connStr);
			await sql.OpenAsync();

			new
			{
				LogicalName = conn.LogicalName,
				Database    = conn.Database,
				Server      = sql.DataSource,
				State       = sql.State.ToString(),
				Version     = sql.ServerVersion
			}.Dump($"✅ {conn.LogicalName} — connected");

			await sql.CloseAsync();
		}
		catch (Exception ex)
		{
			new
			{
				LogicalName = conn.LogicalName,
				Database    = conn.Database,
				Error       = ex.Message
			}.Dump($"❌ {conn.LogicalName} — failed");
		}
	}

	// ── Initialize state file ─────────────────────────────────
	var store = new DatabaseStateStore(StateFilePath());
	store.Load();

	foreach (var conn in connections)
	{
		store.UpdateState(
			logicalName:  conn.LogicalName,
			server:       "localhost",
			database:     conn.Database,
			circuitState: "Closed",
			failureCount: 0,
			message:      "Initialized"
		);
	}

	store.GetAll().Dump("Initial Circuit State");
	File.ReadAllText(StateFilePath()).Dump("JSON on disk — Initial");
}

// ── Windows Authentication connection string ──────────────────
string BuildConnectionString(string database) =>
	new SqlConnectionStringBuilder
	{
		DataSource         = "localhost",
		InitialCatalog     = database,
		IntegratedSecurity = true,
		TrustServerCertificate = true,
		ConnectTimeout     = 5
	}.ConnectionString;

string StateFilePath()
{
	var queriesRoot        = Path.GetDirectoryName(Util.CurrentQueryPath);
	var circuitBreakerRoot = Path.GetFullPath(Path.Combine(queriesRoot, ".."));
	var dataPath           = Path.Combine(circuitBreakerRoot, "_Data", "Database");
	Directory.CreateDirectory(dataPath);
	return Path.Combine(dataPath, "database-state.json");
}