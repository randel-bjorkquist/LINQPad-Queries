<Query Kind="Statements">
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

// ─────────────────────────────────────────────────────────────
// #load paths in _Shared files use "./" — resolves relative to this file.
// #load paths in scenario scripts use "../_Shared/" — resolves relative to the script.
// ─────────────────────────────────────────────────────────────
// Represents the persisted state of a single app pool's
// circuit breaker. Written to disk on every state change so
// external tooling (Grafana, Alloy, future integrations) can
// read current state without polling the circuit breaker itself.
// ─────────────────────────────────────────────────────────────
class AppPoolCircuitState
{
  [JsonPropertyName("appPool")]
  public string AppPool                 { get; set; }

  [JsonPropertyName("circuitState")]
  public string CircuitState            { get; set; }   // Closed | Open | HalfOpen

  [JsonPropertyName("appPoolState")]
  public string AppPoolState            { get; set; }   // Started | Stopped | Unknown

  [JsonPropertyName("failureCount")]
  public int FailureCount               { get; set; }

  [JsonPropertyName("lastStateChange")]
  public string LastStateChange         { get; set; }

  [JsonPropertyName("lastChecked")]
  public string LastChecked             { get; set; }

  [JsonPropertyName("message")]
  public string Message                 { get; set; }
}

// ─────────────────────────────────────────────────────────────
// Root document — one entry per monitored app pool.
// ─────────────────────────────────────────────────────────────
class AppPoolStateDocument
{
  [JsonPropertyName("appPools")]
  public Dictionary<string, AppPoolCircuitState> AppPools { get; set; } = new();

  [JsonPropertyName("lastUpdated")]
  public string LastUpdated { get; set; }
}