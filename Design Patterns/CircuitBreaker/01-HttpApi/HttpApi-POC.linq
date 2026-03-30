<Query Kind="Program">
  <NuGetReference Version="8.4.2">Polly</NuGetReference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#r "nuget: Polly, 8.4.2"

#load "../_Shared/Models/FeatureFlagDocument.linq"
#load "../_Shared/Models/FlagResult.linq"
#load "../_Shared/FeatureFlagStore.linq"
#load "../_Shared/CircuitBreaker.linq"
#load "../_Shared/CircuitBreakerListener.linq"

async Task Main()
{
  // ── Setup ─────────────────────────────────────────────────
  var store = new FeatureFlagStore(FlagFilePath());
  store.Load();

  var flags = new[] { "TextControl.Enabled", "PdfExport.Enabled", "BetaReporting.Enabled" };
  var tenants = new[] { "TenantA", "TenantB" };

  // ── Wire circuit breaker ──────────────────────────────────
  var breaker = new CircuitBreaker<string>(
    name: "TextControlServer",
    exceptionsBeforeBreaking: 3,
    durationOfBreak: TimeSpan.FromSeconds(10)   // short for POC
  );

  // Attach flag listener — TextControl.Enabled mirrors circuit state
  var listener = new CircuitBreakerListener<string>(store, "TextControl.Enabled");
  listener.Attach(breaker);

  // ── Simulated HTTP service ────────────────────────────────
  // Toggle this to simulate the dependency going down
  var serviceAvailable = true;

  async Task<string> CallTextControlServer()
  {
    await Task.Delay(50);   // simulate network latency

    if (!serviceAvailable)
      throw new HttpRequestException("TextControlServer unreachable — connection refused.");

    return "Rendered content from TextControlServer";
  }

  // ── Helper: show current flag state for all tenants ───────
  void ShowFlags(string label)
  {
    tenants
      .SelectMany(t => flags.Select(f => store.Evaluate(f, t)))
      .Dump(label);
  }
  
  void ShowJson(string label) 
    => File.ReadAllText(FlagFilePath()).Dump(label);
    
  // ── Phase 1 — Service healthy ─────────────────────────────
  "Phase 1 — Service healthy".Dump("─────────────────────────");
  ShowFlags("Flags — before any calls");
  ShowJson("JSON — before any calls");
  
  for (int i = 0; i < 3; i++)
  {
    var result = await breaker.ExecuteAsync(CallTextControlServer);
    result.ToString().Dump($"Call {i + 1}");
  }

  ShowFlags("Flags — after successful calls");
  ShowJson("JSON — after successful calls");

  // ── Phase 2 — Service goes down ───────────────────────────
  "Phase 2 — Service goes down".Dump("─────────────────────────");
  serviceAvailable = false;

  // Three failures open the circuit
  for (int i = 0; i < 4; i++)
  {
    var result = await breaker.ExecuteAsync(CallTextControlServer);
    result.ToString().Dump($"Call {i + 1} (service down)");
    await Task.Delay(100);
  }

  ShowFlags("Flags — after circuit opens");
  ShowJson("JSON — after circuit opens");

  // ── Phase 3 — Wait for half-open probe ───────────────────
  "Phase 3 — Waiting for half-open...".Dump("─────────────────────────");
  await Task.Delay(TimeSpan.FromSeconds(11));   // outlast the break duration

  // This probe will fail — service still down
  var probeResult = await breaker.ExecuteAsync(CallTextControlServer);
  probeResult.ToString().Dump("Half-open probe (service still down)");

  ShowFlags("Flags — after failed probe");
  ShowJson("JSON — after failed probe");

  // ── Phase 4 — Service recovers ────────────────────────────
  "Phase 4 — Service recovers".Dump("─────────────────────────");
  serviceAvailable = true;

  await Task.Delay(TimeSpan.FromSeconds(11));   // wait for next half-open window

  var recoveryResult = await breaker.ExecuteAsync(CallTextControlServer);
  recoveryResult.ToString().Dump("Half-open probe (service recovered)");

  // ── Final JSON state on disk ──────────────────────────────
  ShowFlags("Flags — after circuit closes");
  ShowJson("JSON — after circuit closes");
}

// ─────────────────────────────────────────────────────────────
string FlagFilePath()
{
  var queriesRoot = Path.GetDirectoryName(Util.CurrentQueryPath);
  var circuitBreakerRoot = Path.GetFullPath(Path.Combine(queriesRoot, ".."));
  var dataPath = Path.Combine(circuitBreakerRoot, "Data", "HttpApi");
  Directory.CreateDirectory(dataPath);
  return Path.Combine(dataPath, "featureflags.json");
}
