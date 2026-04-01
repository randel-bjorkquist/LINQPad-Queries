<Query Kind="Statements">
  <Namespace>System.Text.Json</Namespace>
</Query>

#load "./Models/FileResourceState.linq"

// ─────────────────────────────────────────────────────────────
// #load paths in _Shared files use "./" — resolves relative to this file.
// #load paths in scenario scripts use "../_Shared/" — resolves relative to the script.
// ─────────────────────────────────────────────────────────────
// Owns reading and writing the file resource circuit state file.
// Also maintains the in-memory cache of the last successfully
// loaded resource — served to consumers while the circuit is
// open rather than failing hard.
//
// This is the key difference from the database scenario:
// a file resource almost always has a last known good value
// that can be safely served stale for a period of time.
// ─────────────────────────────────────────────────────────────
class FileResourceStateStore
{
	private readonly string _path;
	private readonly object _lock = new();

	private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true
                                                                      ,WriteIndented               = true };

	private FileResourceStateDocument _doc = new();

	// ── In-memory cache of last known good resource ───────────
	// Keyed by resource name. Populated on every successful load.
	// Served to consumers when the circuit is open.
	private readonly Dictionary<string, GenericResource> _cache = new();

	public FileResourceStateStore(string path) => _path = path;

	// ── Load (or reload) from disk ───────────────────────────
	public void Load()
	{
		lock(_lock)
		{
			if(!File.Exists(_path))
			{
				_doc = new FileResourceStateDocument();
				return;
			}

			var raw = File.ReadAllText(_path);
      
      // Guard against empty file from a previous partial run
      if(string.IsNullOrWhiteSpace(raw))
      {
        _doc = new FileResourceStateDocument();
        return;
      }
      
			_doc = JsonSerializer.Deserialize<FileResourceStateDocument>(raw, _jsonOptions) 
            ?? new FileResourceStateDocument();
		}
	}

	// ── Update cache with a successfully loaded resource ─────
	public void UpdateCache(string resourceName, GenericResource resource)
	{
		lock(_lock)
		{
			_cache[resourceName] = resource;
		}
	}

	// ── Get cached resource (last known good) ────────────────
	// Returns null if no successful load has occurred yet.
	public GenericResource GetCached(string resourceName)
	{
		lock(_lock)
		{
			return _cache.TryGetValue(resourceName, out var cached) ? cached : null;
		}
	}

	// ── Update circuit state for a single resource ───────────
	public void UpdateState( string resourceName
                          ,string resourcePath
                          ,string circuitState
                          ,string failureMode              = "None"
                          ,int    failureCount             = 0
                          ,bool   cacheAvailable           = false
                          ,string lastError                = null
                          ,string message                  = null
                          ,bool   updateLastSuccessfulLoad = false)
	{
		lock(_lock)
		{
			var now          = DateTime.UtcNow.ToString("o");
			var existing     = _doc.Resources.TryGetValue(resourceName, out var e) ? e : null;
			var stateChanged = existing?.CircuitState != circuitState;

			_doc.Resources[resourceName] = new FileResourceCircuitState { ResourceName         = resourceName
                                                                   ,ResourcePath         = resourcePath
                                                                   ,CircuitState         = circuitState
                                                                   ,FailureMode          = failureMode
                                                                   ,FailureCount         = failureCount
                                                                   ,CacheAvailable       = cacheAvailable
                                                                   ,LastSuccessfulLoad   = updateLastSuccessfulLoad 
                                                                                            ? now 
                                                                                            : existing?.LastSuccessfulLoad
                                                                   ,LastStateChange      = stateChanged 
                                                                                            ? now 
                                                                                            : existing?.LastStateChange ?? now
                                                                   ,LastChecked          = now
                                                                   ,LastError            = lastError
                                                                   ,Message              = message };

			_doc.LastUpdated = now;
			Persist();
		}
	}

	// ── Read current state for a single resource ─────────────
	public FileResourceCircuitState GetState(string resourceName)
	{
		lock(_lock)
		{
			return _doc.Resources.TryGetValue(resourceName, out var state)
				      ? state
				      : null;
		}
	}

	// ── Read all resource states ──────────────────────────────
	public List<FileResourceCircuitState> GetAll()
	{
		lock(_lock)
		{
			return _doc.Resources.Values.ToList();
		}
	}

	// ── Write current in-memory state back to disk ───────────
	private void Persist() 
    => File.WriteAllText(_path, JsonSerializer.Serialize(_doc, _jsonOptions));
}
