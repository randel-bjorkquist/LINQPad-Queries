<Query Kind="Statements">
  <Namespace>System.Text.Json</Namespace>
</Query>

#load "./Models/FeatureFlagDocument.linq"
#load "./Models/FlagResult.linq"

// ─────────────────────────────────────────────────────────────
// #load paths in .Shared files use "./" — resolves relative to this file.
// #load paths in scenario scripts use "../.Shared/" — resolves relative to the script.
// ─────────────────────────────────────────────────────────────
// Owns loading, evaluating, and persisting the three-layer flag
// file. Thread-safe via a single lock on all file operations.
//
// Consumers call only:   Evaluate(flag, tenantId)
// Circuit breaker calls: SetRuntime(flag, value, circuitState)
//                        ClearRuntime(flag)
//
// The global and tenant blocks are never touched by the app.
// ─────────────────────────────────────────────────────────────
class FeatureFlagStore
{
  private readonly string _path;
  private readonly object _lock = new();

  private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true
                                                                      ,WriteIndented               = true };

  private FeatureFlagDocument _doc = new();

  public FeatureFlagStore(string path) => _path = path;

  // ── Load (or reload) from disk ───────────────────────────
  public void Load()
  {
    lock (_lock)
    {
      var raw = File.ReadAllText(_path);
      _doc = JsonSerializer.Deserialize<FeatureFlagDocument>(raw, _jsonOptions) ?? new FeatureFlagDocument();
    }
  }

  // ── Evaluate a single flag for a tenant ──────────────────
  // Priority: Runtime → Tenant override → Global default → false
  public FlagResult Evaluate(string flag, string tenantId)
  {
    lock(_lock)
    {
      // Layer 1 — Runtime (circuit breaker overrides)
      if(_doc.Runtime.TryGetValue(flag, out var entry))
        return new FlagResult { Flag         = flag
                               ,TenantId     = tenantId
                               ,Value        = entry.Value
                               ,Source       = "Runtime"
                               ,CircuitState = entry.CircuitState };

      // Layer 2 — Tenant override
      if(_doc.Tenants.TryGetValue(tenantId, out var tenantFlags) && 
          tenantFlags.TryGetValue(flag, out var tenantValue))
        return new FlagResult { Flag      = flag
                               ,TenantId  = tenantId
                               ,Value     = tenantValue
                               ,Source    = "Tenant" };

      // Layer 3 — Global default
      if(_doc.Global.TryGetValue(flag, out var globalValue))
        return new FlagResult { Flag      = flag
                               ,TenantId  = tenantId
                               ,Value     = globalValue
                               ,Source    = "Global" };

      // Safe default — flag not defined anywhere
      return new FlagResult { Flag      = flag
                             ,TenantId  = tenantId
                             ,Value     = false
                             ,Source  = "Default" };
    }
  }

  // ── Called by circuit breaker on OPEN ────────────────────
  public void SetRuntime(string flag, bool value, string circuitState = null)
  {
    lock(_lock)
    {
      _doc.Runtime[flag] = new RuntimeEntry{ Value        = value
                                            ,CircuitState = circuitState
                                            ,OpenedAt     = DateTime.UtcNow.ToString("o") };
      Persist();
    }
  }

  // ── Called by circuit breaker on CLOSE ───────────────────
  // Removes entry entirely — tenant/global values restore naturally.
  public void ClearRuntime(string flag)
  {
    lock(_lock)
    {
      if(_doc.Runtime.Remove(flag))
        Persist();
    }
  }

  // ── Write current in-memory state back to disk ───────────
  private void Persist() 
    => File.WriteAllText(_path, JsonSerializer.Serialize(_doc, _jsonOptions));
}