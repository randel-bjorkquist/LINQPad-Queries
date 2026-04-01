<Query Kind="Statements">
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

// ─────────────────────────────────────────────────────────────
// #load paths in _Shared files use "./" — resolves relative to this file.
// #load paths in scenario scripts use "../_Shared/" — resolves relative to the script.
// ─────────────────────────────────────────────────────────────
// Represents the persisted state of a single database
// connection's circuit breaker. Written to disk on every state
// change so external tooling can read current state without
// polling the circuit breaker itself.
// ─────────────────────────────────────────────────────────────
class DatabaseCircuitState
{
	[JsonPropertyName("logicalName")]
	public string LogicalName { get; set; }

	[JsonPropertyName("server")]
	public string Server { get; set; }

	[JsonPropertyName("database")]
	public string Database { get; set; }

	[JsonPropertyName("circuitState")]
	public string CircuitState { get; set; }    // Closed | Open | HalfOpen

	[JsonPropertyName("failureCount")]
	public int FailureCount { get; set; }

	[JsonPropertyName("lastStateChange")]
	public string LastStateChange { get; set; }

	[JsonPropertyName("lastChecked")]
	public string LastChecked { get; set; }

	[JsonPropertyName("lastError")]
	public string LastError { get; set; }

	[JsonPropertyName("message")]
	public string Message { get; set; }
}

// ─────────────────────────────────────────────────────────────
// Root document — one entry per monitored database connection.
// ─────────────────────────────────────────────────────────────
class DatabaseStateDocument
{
	[JsonPropertyName("connections")]
	public Dictionary<string, DatabaseCircuitState> Connections { get; set; } = new();

	[JsonPropertyName("lastUpdated")]
	public string LastUpdated { get; set; }
}