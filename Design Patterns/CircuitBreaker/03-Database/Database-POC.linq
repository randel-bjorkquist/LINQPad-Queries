<Query Kind="Program">
  <Namespace>Microsoft.Data.SqlClient</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#r "nuget: Microsoft.Data.SqlClient, 5.2.2"
#r "nuget: Polly, 8.4.2"

#load "../_Shared/Models/DatabaseState.linq"
#load "../_Shared/DatabaseStateStore.linq"
#load "../_Shared/CircuitBreaker.linq"

async Task Main()
{
	var store = new DatabaseStateStore(StateFilePath());
	store.Load();

	// ── Connection definitions ────────────────────────────────
	var connections = new[]
	{
		new { LogicalName = "PHA-Auth-DB", Database = "PHA-Auth_Dev"       },
		new { LogicalName = "PHA-Web-DB",  Database = "PHA-Web_Dev"        }
	};

	// ── Toggle this to simulate databases going down ──────────
	// true  = use real database name  (healthy)
	// false = use fake database name  (down)
	var dbAvailable = new Dictionary<string, bool>
	{
		["PHA-Auth-DB"] = true,
		["PHA-Web-DB"]  = true
	};

	// ── One circuit breaker per connection ────────────────────
	// Standalone — no flag listener attached.
	// Each breaker is fully independent.
	var breakers = connections.ToDictionary(
		c => c.LogicalName,
		c => new CircuitBreaker<string>(
			name:                     c.LogicalName,
			exceptionsBeforeBreaking: 3,
			durationOfBreak:          TimeSpan.FromSeconds(15)
		)
	);

	// ── Wire state change events to state store ───────────────
	foreach (var conn in connections)
	{
		var logical  = conn.LogicalName;
		var database = conn.Database;
		var breaker  = breakers[logical];

		breaker.OnOpen += (ex, duration) =>
		{
			store.UpdateState(
				logicalName:  logical,
				server:       "localhost",
				database:     database,
				circuitState: "Open",
				failureCount: 3,
				lastError:    ex.Message,
				message:      $"Circuit opened — breaking for {duration.TotalSeconds}s"
			);
			ShowJson($"JSON — {logical} circuit opened");
		};

		breaker.OnHalfOpen += () =>
		{
			store.UpdateState(
				logicalName:  logical,
				server:       "localhost",
				database:     database,
				circuitState: "HalfOpen",
				failureCount: 0,
				message:      "Probing database connectivity"
			);
			ShowJson($"JSON — {logical} circuit half-open");
		};

		breaker.OnClose += () =>
		{
			store.UpdateState(
				logicalName:  logical,
				server:       "localhost",
				database:     database,
				circuitState: "Closed",
				failureCount: 0,
				message:      "Database recovered"
			);
			ShowJson($"JSON — {logical} circuit closed");
		};
	}

	// ── Helper: attempt a real database connection ────────────
	async Task<string> ProbeDatabase(string logicalName, string database)
	{
		// Simulate failure by using a fake database name
		var targetDb = dbAvailable[logicalName]
			? database
			: $"{database}_UNAVAILABLE";

		var connStr = BuildConnectionString(targetDb);

		using var sql = new SqlConnection(connStr);
		await sql.OpenAsync();

		// Run a lightweight query to confirm connectivity
		using var cmd    = sql.CreateCommand();
		cmd.CommandText  = "SELECT DB_NAME(), @@SERVERNAME, GETUTCDATE()";
		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return $"DB: {reader[0]}  Server: {reader[1]}  UTC: {reader[2]}";

		return "Connected — no data returned";
	}

	// ── Phase 1 — Both databases healthy ─────────────────────
	"Phase 1 — Both databases healthy".Dump("─────────────────────────");
	ShowState("State — initial");
	ShowJson("JSON — initial");

	foreach (var conn in connections)
	{
		var result = await breakers[conn.LogicalName]
			.ExecuteAsync(() => ProbeDatabase(conn.LogicalName, conn.Database));
		result.ToString().Dump($"{conn.LogicalName} — probe");
	}

	ShowState("State — after healthy probes");

	// ── Phase 2 — Take PHA-Auth-DB down ──────────────────────
	"Phase 2 — PHA-Auth-DB goes down (PHA-Web-DB stays up)".Dump("─────────────────────────");
	dbAvailable["PHA-Auth-DB"] = false;

	// Four attempts — third opens the circuit, fourth is short-circuited
	for (int i = 0; i < 4; i++)
	{
		foreach (var conn in connections)
		{
			var result = await breakers[conn.LogicalName]
				.ExecuteAsync(() => ProbeDatabase(conn.LogicalName, conn.Database));
			result.ToString().Dump($"Attempt {i + 1} — {conn.LogicalName}");
		}
		await Task.Delay(500);
	}

	ShowState("State — after PHA-Auth-DB circuit opens");

	// ── Phase 3 — Take PHA-Web-DB down too ───────────────────
	"Phase 3 — PHA-Web-DB also goes down".Dump("─────────────────────────");
	dbAvailable["PHA-Web-DB"] = false;

	for (int i = 0; i < 4; i++)
	{
		foreach (var conn in connections)
		{
			var result = await breakers[conn.LogicalName]
				.ExecuteAsync(() => ProbeDatabase(conn.LogicalName, conn.Database));
			result.ToString().Dump($"Attempt {i + 1} — {conn.LogicalName}");
		}
		await Task.Delay(500);
	}

	ShowState("State — both circuits open");

	// ── Phase 4 — Wait for half-open, PHA-Auth-DB still down ─
	"Phase 4 — Waiting for half-open probe...".Dump("─────────────────────────");
	await Task.Delay(TimeSpan.FromSeconds(16));

	foreach (var conn in connections)
	{
		var result = await breakers[conn.LogicalName]
			.ExecuteAsync(() => ProbeDatabase(conn.LogicalName, conn.Database));
		result.ToString().Dump($"Half-open probe — {conn.LogicalName}");
	}

	ShowState("State — after failed half-open probes");

	// ── Phase 5 — Both databases recover ─────────────────────
	"Phase 5 — Both databases recover".Dump("─────────────────────────");
	dbAvailable["PHA-Auth-DB"] = true;
	dbAvailable["PHA-Web-DB"]  = true;

	await Task.Delay(TimeSpan.FromSeconds(16));

	foreach (var conn in connections)
	{
		var result = await breakers[conn.LogicalName]
			.ExecuteAsync(() => ProbeDatabase(conn.LogicalName, conn.Database));
		result.ToString().Dump($"Recovery probe — {conn.LogicalName}");
	}

	ShowState("State — both circuits closed");
	ShowJson("JSON — final");
}

// ── Windows Authentication connection string ──────────────────
string BuildConnectionString(string database) =>
	new SqlConnectionStringBuilder
	{
		DataSource             = "localhost",
		InitialCatalog         = database,
		IntegratedSecurity     = true,
		TrustServerCertificate = true,
		ConnectTimeout         = 5
	}.ConnectionString;

// ── Display helpers ───────────────────────────────────────────
void ShowState(string label)
{
	var s = new DatabaseStateStore(StateFilePath());
	s.Load();
	s.GetAll().Dump(label);
}

void ShowJson(string label) =>
	File.ReadAllText(StateFilePath()).Dump(label);

string StateFilePath()
{
	var queriesRoot        = Path.GetDirectoryName(Util.CurrentQueryPath);
	var circuitBreakerRoot = Path.GetFullPath(Path.Combine(queriesRoot, ".."));
	var dataPath           = Path.Combine(circuitBreakerRoot, "_Data", "Database");
	Directory.CreateDirectory(dataPath);
	return Path.Combine(dataPath, "database-state.json");
}