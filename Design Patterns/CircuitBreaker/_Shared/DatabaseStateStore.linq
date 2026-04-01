<Query Kind="Statements">
  <Namespace>System.Text.Json</Namespace>
</Query>

#load "./Models/DatabaseState.linq"

// ─────────────────────────────────────────────────────────────
// #load paths in _Shared files use "./" — resolves relative to this file.
// #load paths in scenario scripts use "../_Shared/" — resolves relative to the script.
// ─────────────────────────────────────────────────────────────
// Owns reading and writing the database circuit state file.
// Completely independent of FeatureFlagStore and
// AppPoolStateStore — each scenario has its own persistence
// layer. Standalone circuit breaker — no feature flag involved.
// ─────────────────────────────────────────────────────────────
class DatabaseStateStore
{
	private readonly string _path;
	private readonly object _lock = new();

	private static readonly JsonSerializerOptions _jsonOptions = new()
	{
		PropertyNameCaseInsensitive = true,
		WriteIndented               = true,
	};

	private DatabaseStateDocument _doc = new();

	public DatabaseStateStore(string path) => _path = path;

	// ── Load (or reload) from disk ───────────────────────────
	public void Load()
	{
		lock(_lock)
		{
			if(!File.Exists(_path))
			{
				_doc = new DatabaseStateDocument();
				return;
			}

			var raw = File.ReadAllText(_path);
            
      // Guard against empty file from a previous partial run
      if(string.IsNullOrWhiteSpace(raw))
      {
        _doc = new DatabaseStateDocument();
        return;
      }
      
			_doc = JsonSerializer.Deserialize<DatabaseStateDocument>(raw, _jsonOptions)
			       ?? new DatabaseStateDocument();
		}
	}

	// ── Update state for a single connection ─────────────────
	public void UpdateState(
		string logicalName,
		string server,
		string database,
		string circuitState,
		int    failureCount,
		string lastError  = null,
		string message    = null)
	{
		lock (_lock)
		{
			var now          = DateTime.UtcNow.ToString("o");
			var existing     = _doc.Connections.TryGetValue(logicalName, out var e) ? e : null;
			var stateChanged = existing?.CircuitState != circuitState;

			_doc.Connections[logicalName] = new DatabaseCircuitState
			{
				LogicalName     = logicalName,
				Server          = server,
				Database        = database,
				CircuitState    = circuitState,
				FailureCount    = failureCount,
				LastStateChange = stateChanged ? now : existing?.LastStateChange ?? now,
				LastChecked     = now,
				LastError       = lastError,
				Message         = message
			};

			_doc.LastUpdated = now;
			Persist();
		}
	}

	// ── Read current state for a single connection ───────────
	public DatabaseCircuitState GetState(string logicalName)
	{
		lock (_lock)
		{
			return _doc.Connections.TryGetValue(logicalName, out var state)
				? state
				: null;
		}
	}

	// ── Read all connection states ────────────────────────────
	public List<DatabaseCircuitState> GetAll()
	{
		lock (_lock)
		{
			return _doc.Connections.Values.ToList();
		}
	}

	// ── Write current in-memory state back to disk ───────────
	private void Persist() =>
		File.WriteAllText(_path, JsonSerializer.Serialize(_doc, _jsonOptions));
}