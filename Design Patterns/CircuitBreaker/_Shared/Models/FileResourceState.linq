<Query Kind="Statements">
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

// ─────────────────────────────────────────────────────────────
// #load paths in _Shared files use "./" — resolves relative to this file.
// #load paths in scenario scripts use "../_Shared/" — resolves relative to the script.
// ─────────────────────────────────────────────────────────────
// Represents the persisted state of a single file resource's
// circuit breaker. Written to disk on every state change.
//
// Use cases this pattern applies to:
//   - Pricing / rate files loaded from a network share
//   - Report templates loaded on demand
//   - Tenant configuration files loaded on a schedule
//   - Any file-backed resource where staleness is preferable
//     to a hard failure
// ─────────────────────────────────────────────────────────────
class FileResourceCircuitState
{
	[JsonPropertyName("resourceName")]
	public string ResourceName                { get; set; }

	[JsonPropertyName("resourcePath")]
	public string ResourcePath                { get; set; }

	[JsonPropertyName("circuitState")]
	public string CircuitState                { get; set; }    // Closed | Open | HalfOpen

	[JsonPropertyName("failureMode")]
	public string FailureMode                 { get; set; }     // None | NotFound | Locked | Corrupt

	[JsonPropertyName("failureCount")]
	public int FailureCount                   { get; set; }

	[JsonPropertyName("lastSuccessfulLoad")]
	public string LastSuccessfulLoad          { get; set; }

	[JsonPropertyName("lastStateChange")]
	public string LastStateChange             { get; set; }

	[JsonPropertyName("lastChecked")]
	public string LastChecked                 { get; set; }

	[JsonPropertyName("lastError")]
	public string LastError                   { get; set; }

	[JsonPropertyName("message")]
	public string Message                     { get; set; }

	[JsonPropertyName("cacheAvailable")]
	public bool CacheAvailable                { get; set; }    // true when stale cache can be served
}

// ─────────────────────────────────────────────────────────────
// Root document — one entry per monitored file resource.
// ─────────────────────────────────────────────────────────────
class FileResourceStateDocument
{
	[JsonPropertyName("resources")]
	public Dictionary<string, FileResourceCircuitState> Resources { get; set; } = new();

	[JsonPropertyName("lastUpdated")]
	public string LastUpdated { get; set; }
}

// ─────────────────────────────────────────────────────────────
// The resource file's own content model.
// Replace with your domain-specific structure in real usage.
//
// Examples of what this could represent in production:
//   - Pricing rules:   { "rates": [...], "effectiveDate": "..." }
//   - Tenant config:   { "tenants": { "TenantA": { ... } } }
//   - Report template: { "sections": [...], "version": "..." }
// ─────────────────────────────────────────────────────────────
class GenericResource
{
	[JsonPropertyName("resourceName")]
	public string ResourceName                  { get; set; }

	[JsonPropertyName("version")]
	public string Version                       { get; set; }

	[JsonPropertyName("loadedAt")]
	public string LoadedAt                      { get; set; }

	[JsonPropertyName("settings")]
	public Dictionary<string, object> Settings  { get; set; } = new();

	[JsonPropertyName("items")]
	public List<ResourceItem> Items             { get; set; } = new();
}

class ResourceItem
{
	[JsonPropertyName("key")]
	public string Key               { get; set; }

	[JsonPropertyName("value")]
	public string Value             { get; set; }

	[JsonPropertyName("enabled")]
	public bool Enabled             { get; set; }
}
