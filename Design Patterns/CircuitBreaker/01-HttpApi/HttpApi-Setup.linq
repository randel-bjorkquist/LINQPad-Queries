<Query Kind="Program">
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "../_Shared/Models/FeatureFlagDocument.linq"
#load "../_Shared/Models/FlagResult.linq"
#load "../_Shared/FeatureFlagStore.linq"

async Task Main()
{
  // ── Write the seed flag file ──────────────────────────────
  var path = FlagFilePath();

  var doc = new FeatureFlagDocument
  {
    Global = new Dictionary<string, bool>
    {
      ["TextControl.Enabled"] = true,
      ["PdfExport.Enabled"] = true,
      ["BetaReporting.Enabled"] = false,
    },
    Tenants = new Dictionary<string, Dictionary<string, bool>>
    {
      ["TenantA"] = new Dictionary<string, bool>
      {
        ["BetaReporting.Enabled"] = true    // TenantA opted into beta
      },
      ["TenantB"] = new Dictionary<string, bool>
      {
        ["PdfExport.Enabled"] = false       // TenantB has PDF disabled
      }
    },
    Runtime = new Dictionary<string, RuntimeEntry>()    // always starts empty
  };

  var options = new JsonSerializerOptions { WriteIndented = true };
  File.WriteAllText(path, JsonSerializer.Serialize(doc, options));
  $"Flag file written to: {path}".Dump("Setup");

  // ── Load and show baseline evaluation ────────────────────
  var store = new FeatureFlagStore(path);
  store.Load();

  var flags = new[] { "TextControl.Enabled", "PdfExport.Enabled", "BetaReporting.Enabled" };

  flags.Select(f => store.Evaluate(f, "TenantA")).Dump("TenantA — Baseline");
  flags.Select(f => store.Evaluate(f, "TenantB")).Dump("TenantB — Baseline");

  File.ReadAllText(path).Dump("JSON on disk — Baseline");
}

// Shared flag file path used by all HttpApi scripts
//string FlagFilePath() =>
//  Path.Combine(Path.GetTempPath(), "CircuitBreaker", "HttpApi", "featureflags.json")
//      .Tap(p => Directory.CreateDirectory(Path.GetDirectoryName(p)));

string FlagFilePath()
{
  // Resolves to the CircuitBreaker folder on disk, next to your scenario folders
  var queriesRoot        = Path.GetDirectoryName(Util.CurrentQueryPath);        // 01-HttpApi/
  var circuitBreakerRoot = Path.GetFullPath(Path.Combine(queriesRoot, ".."));   // CircuitBreaker/
  var dataPath           = Path.Combine(circuitBreakerRoot, "Data", "HttpApi");

  Directory.CreateDirectory(dataPath);

  return Path.Combine(dataPath, "featureflags.json");
}

static class Extensions
{
  public static T Tap<T>(this T value, Action<T> action) { action(value); return value; }
}      