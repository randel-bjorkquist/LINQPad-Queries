<Query Kind="Statements" />

// ─────────────────────────────────────────────────────────────
// #load paths in .Shared files use "./" — resolves relative to this file.
// #load paths in scenario scripts use "../_Shared/" — resolves relative to the script.
// ─────────────────────────────────────────────────────────────
// The result of evaluating a single flag for a tenant.
// Source tells you which layer answered — useful for diagnostics
// and for wiring Prometheus ObservableGauge metrics later.
// ─────────────────────────────────────────────────────────────
class FlagResult
{
  public string Flag          { get; set; }
  public string TenantId      { get; set; }
  public bool Value           { get; set; }
  public string Source        { get; set; }  // Runtime | Tenant | Global | Default
  public string CircuitState  { get; set; }  // Only populated when Source == Runtime

  public override string ToString() => $"{Flag,-30} = {Value,-5}  via {Source,-8}" 
                                    +   (CircuitState != null ? $"  [{CircuitState}]" : string.Empty);
}