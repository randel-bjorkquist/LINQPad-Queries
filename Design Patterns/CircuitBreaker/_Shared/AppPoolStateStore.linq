<Query Kind="Statements">
  <Namespace>System.Text.Json</Namespace>
</Query>

#load "./Models/AppPoolState.linq"

// ─────────────────────────────────────────────────────────────
// #load paths in _Shared files use "./" — resolves relative to this file.
// #load paths in scenario scripts use "../_Shared/" — resolves relative to the script.
// ─────────────────────────────────────────────────────────────
// Owns reading and writing the app pool circuit state file.
// Completely independent of FeatureFlagStore — this is the
// standalone circuit breaker's persistence layer.
//
// Consumers read from this to know current circuit state.
// The poller writes to this on every state change.
// ─────────────────────────────────────────────────────────────
class AppPoolStateStore
{
  private readonly string _path;
  private readonly object _lock = new();

  private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true
                                                                      ,WriteIndented               = true };

  private AppPoolStateDocument _doc = new();

  public AppPoolStateStore(string path) => _path = path;

  // ── Load (or reload) from disk ───────────────────────────
  public void Load()
  {
    lock(_lock)
    {
      if(!File.Exists(_path))
      {
        _doc = new AppPoolStateDocument();
        return;
      }

      var raw = File.ReadAllText(_path);
      _doc = JsonSerializer.Deserialize<AppPoolStateDocument>(raw, _jsonOptions) ?? new AppPoolStateDocument();
    }
  }

  // ── Update state for a single app pool ───────────────────
  public void UpdateState(string appPool, string circuitState, string appPoolState, int failureCount, string message = null)
  {
    lock(_lock)
    {
      var now = DateTime.UtcNow.ToString("o");

      // Only update lastStateChange when circuit state actually changes
      var existing     = _doc.AppPools.TryGetValue(appPool, out var e) ? e : null;
      var stateChanged = existing?.CircuitState != circuitState;

      _doc.AppPools[appPool] = new AppPoolCircuitState { AppPool         = appPool
                                                        ,CircuitState    = circuitState
                                                        ,AppPoolState    = appPoolState
                                                        ,FailureCount    = failureCount
                                                        ,LastStateChange = stateChanged 
                                                                            ? now 
                                                                            : existing?.LastStateChange ?? now
                                                        ,LastChecked     = now
                                                        ,Message         = message };

      _doc.LastUpdated = now;
      Persist();
    }
  }

  // ── Read current state for a single app pool ─────────────
  public AppPoolCircuitState GetState(string appPool)
  {
    lock(_lock)
    {
      return _doc.AppPools.TryGetValue(appPool, out var state)
               ? state
               : null;
    }
  }

  // ── Read all app pool states ──────────────────────────────
  public IEnumerable<AppPoolCircuitState> GetAll()
  {
    lock(_lock)
    {
      return _doc.AppPools.Values.ToList();
    }
  }

  // ── Write current in-memory state back to disk ───────────
  private void Persist() 
    => File.WriteAllText(_path, JsonSerializer.Serialize(_doc, _jsonOptions));
}