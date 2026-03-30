<Query Kind="Statements">
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

// ─────────────────────────────────────────────────────────────
// #load paths in .Shared files use "./" — resolves relative to this file.
// #load paths in scenario scripts use "../_Shared/" — resolves relative to the script.
// ─────────────────────────────────────────────────────────────
// Represents the three-layer structure of the flag JSON file.
//
// global  — system-wide defaults (admin-managed, never mutated by app)
// tenants — per-tenant overrides (admin-managed, never mutated by app)
// runtime — circuit breaker state (app-managed, always transient)
// ─────────────────────────────────────────────────────────────
class FeatureFlagDocument
{
  [JsonPropertyName("global")]
  public Dictionary<string, bool> Global { get; set; } = new();

  [JsonPropertyName("tenants")]
  public Dictionary<string, Dictionary<string, bool>> Tenants { get; set; } = new();

  [JsonPropertyName("runtime")]
  public Dictionary<string, RuntimeEntry> Runtime { get; set; } = new();
}

// ─────────────────────────────────────────────────────────────
// A single runtime override entry written by the circuit breaker.
// Cleared entirely on recovery — never leaks into tenant/global.
// ─────────────────────────────────────────────────────────────
class RuntimeEntry
{
  [JsonPropertyName("value")]
  public bool Value { get; set; }

  [JsonPropertyName("circuitState")]
  public string CircuitState { get; set; }

  [JsonPropertyName("openedAt")]
  public string OpenedAt { get; set; }
}