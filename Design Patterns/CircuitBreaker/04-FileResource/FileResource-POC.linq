<Query Kind="Program">
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#r "nuget: Polly, 8.4.2"

// If NuGet packages fail to load — press Alt+Shift+Minus to restore

#load "../_Shared/Models/FeatureFlagDocument.linq"
#load "../_Shared/Models/FlagResult.linq"
#load "../_Shared/Models/FileResourceState.linq"
#load "../_Shared/FeatureFlagStore.linq"
#load "../_Shared/FileResourceStateStore.linq"
#load "../_Shared/CircuitBreaker.linq"
#load "../_Shared/CircuitBreakerListener.linq"

async Task Main()
{
  var stateStore = new FileResourceStateStore(StateFilePath());
  stateStore.Load();

  var flagStore = new FeatureFlagStore(FlagFilePath());
  flagStore.Load();

  // ── Failure mode toggle ───────────────────────────────────
  // Change this to simulate different failure scenarios.
  //
  // None     — file loads cleanly every time
  // NotFound — file has been moved or deleted
  // Locked   — file is held open by another process
  // Corrupt  — file contains invalid JSON
  //
  // In production these failure modes map to real situations:
  //   NotFound — network share unavailable, file not deployed
  //   Locked   — another process writing the file during load
  //   Corrupt  — partial write, encoding issue, manual edit error
  var failureMode = "None";

  // ── Circuit breaker ───────────────────────────────────────
  var breaker = new CircuitBreaker<GenericResource>(
    name: "GenericResource",
    exceptionsBeforeBreaking: 3,
    durationOfBreak: TimeSpan.FromSeconds(15)
  );

  // ── Flag listener ─────────────────────────────────────────
  // Attach because this resource backs a feature.
  // When the resource can't be loaded, the feature is disabled.
  // When it recovers, the feature re-enables automatically.
  //
  // In production this flag would gate the UI feature that
  // depends on this resource — users see a degraded message
  // instead of an unhandled error while the circuit is open.
  var listener = new CircuitBreakerListener<GenericResource>(
    flagStore, "GenericResource.Enabled");
  listener.Attach(breaker);

  // ── Wire state change events to state store ───────────────
  breaker.OnOpen += (ex, duration) =>
  {
    var cached = stateStore.GetCached("GenericResource");
    stateStore.UpdateState( resourceName:   "GenericResource"
                           ,resourcePath:   ResourceFilePath()
                           ,circuitState:   "Open"
                           ,failureMode:    failureMode
                           ,failureCount:   3
                           ,cacheAvailable: cached != null
                           ,lastError:      ex.Message
                           ,message:        cached != null
                                              ? $"Circuit open — serving stale cache from {cached.LoadedAt}"
                                              : "Circuit open — no cache available, feature disabled" );
                                              
    ShowJson(stateStore, flagStore, "JSON — circuit opened");
  };

  breaker.OnHalfOpen += () =>
  {
    var cached = stateStore.GetCached("GenericResource");
    stateStore.UpdateState( resourceName:   "GenericResource"
                           ,resourcePath:   ResourceFilePath()
                           ,circuitState:   "HalfOpen"
                           ,failureMode:    failureMode
                           ,failureCount:   0
                           ,cacheAvailable: cached != null
                           ,message:        "Probing resource availability" );
                           
    ShowJson(stateStore, flagStore, "JSON — circuit half-open");
  };

  breaker.OnClose += () =>
  {
    stateStore.UpdateState( resourceName:             "GenericResource"
                           ,resourcePath:             ResourceFilePath()
                           ,circuitState:             "Closed"
                           ,failureMode:              "None"
                           ,failureCount:             0
                           ,cacheAvailable:           true
                           ,message:                  "Resource recovered — cache refreshed"
                           ,updateLastSuccessfulLoad: true );
                           
    ShowJson(stateStore, flagStore, "JSON — circuit closed");
  };

  // ── Resource loader ───────────────────────────────────────
  async Task<GenericResource> LoadResource()
  {
    await Task.CompletedTask;

    switch(failureMode)
    {
      case "NotFound":
        // Simulates: network share offline, file not deployed, file moved or renamed by another process
        throw new FileNotFoundException("Resource file not found — path may be unavailable.", ResourceFilePath());

      case "Locked":
        // Simulates: another process holding an exclusive write lock on the file during a scheduled update
        throw new IOException("Resource file is locked by another process.");

      case "Corrupt":
        // Simulates: partial write left invalid JSON, encoding issue, or manual edit error
        throw new JsonException("Resource file contains invalid JSON — parse failed.");

      default:
        // Healthy — load and deserialize normally
        var raw      = File.ReadAllText(ResourceFilePath());
        var options  = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var resource = JsonSerializer.Deserialize<GenericResource>(raw, options);

        // Update cache with fresh load
        stateStore.UpdateCache("GenericResource", resource);
        stateStore.UpdateState( resourceName:             "GenericResource"
                               ,resourcePath:             ResourceFilePath()
                               ,circuitState:             "Closed"
                               ,failureMode:              "None"
                               ,failureCount:             0
                               ,cacheAvailable:           true
                               ,message:                  "Loaded successfully"
                               ,updateLastSuccessfulLoad: true );

        return resource;
    }
  }

  // ── Helper: attempt load and handle result ────────────────
  async Task AttemptLoad(string label)
  {
    var result = await breaker.ExecuteAsync(LoadResource);

    if(result.Succeeded)
    {
      $"✅ Loaded: {result.Value.ResourceName} v{result.Value.Version} — {result.Value.Items.Count} items"
        .Dump(label);
        
      return;
    }

    if(result.CircuitOpen)
    {
      // Circuit is open — serve from cache if available
      var cached = stateStore.GetCached("GenericResource");
      
      if(cached != null)
      {
        $"⚡ Circuit open — serving stale cache: {cached.ResourceName} v{cached.Version} loaded at {cached.LoadedAt}"
          .Dump(label);
      }
      else
      {
        "⚡ Circuit open — no cache available. Feature disabled."
          .Dump(label);
      }
      
      return;
    }

    $"❌ Failed [{failureMode}] — {result.Exception.Message}"
      .Dump(label);
  }

  // ─────────────────────────────────────────────────────────
  // Phase 1 — Resource healthy
  // ─────────────────────────────────────────────────────────
  "Phase 1 — Resource healthy".Dump("─────────────────────────");
  failureMode = "None";

  ShowState(stateStore, "State — initial");
  ShowFlags(flagStore, "Flags — initial");

  for(int i = 0; i < 3; i++)
    await AttemptLoad($"Load attempt {i + 1}");

  ShowState(stateStore, "State — after healthy loads");

  // ─────────────────────────────────────────────────────────
  // Phase 2 — File not found (cache available)
  // ─────────────────────────────────────────────────────────
  "Phase 2 — File not found (cache available from Phase 1)".Dump("─────────────────────────");
  failureMode = "NotFound";

  for(int i = 0; i < 4; i++)
    await AttemptLoad($"Load attempt {i + 1} (not found)");

  ShowState(stateStore, "State — after NotFound circuit opens");
  ShowFlags(flagStore, "Flags — after NotFound circuit opens");

  // ─────────────────────────────────────────────────────────
  // Phase 3 — Wait for half-open, simulate Locked failure
  // ─────────────────────────────────────────────────────────
  "Phase 3 — Waiting for half-open, then Locked failure".Dump("─────────────────────────");
  await Task.Delay(TimeSpan.FromSeconds(16));

  failureMode = "Locked";
  await AttemptLoad("Half-open probe (Locked)");

  ShowState(stateStore, "State — after Locked half-open probe");

  // ─────────────────────────────────────────────────────────
  // Phase 4 — Wait for half-open, simulate Corrupt failure
  // ─────────────────────────────────────────────────────────
  "Phase 4 — Waiting for half-open, then Corrupt failure".Dump("─────────────────────────");
  await Task.Delay(TimeSpan.FromSeconds(16));

  failureMode = "Corrupt";
  await AttemptLoad("Half-open probe (Corrupt)");

  ShowState(stateStore, "State — after Corrupt half-open probe");

  // ─────────────────────────────────────────────────────────
  // Phase 5 — Resource recovers
  // ─────────────────────────────────────────────────────────
  "Phase 5 — Resource recovers".Dump("─────────────────────────");
  await Task.Delay(TimeSpan.FromSeconds(16));

  failureMode = "None";
  await AttemptLoad("Recovery probe");

  ShowState(stateStore, "State — after recovery");
  ShowFlags(flagStore, "Flags — after recovery");
  ShowJson(stateStore, flagStore, "JSON — final");
}

// ── Display helpers ───────────────────────────────────────────
void ShowState(FileResourceStateStore store, string label) =>
  store.GetAll().Dump(label);

void ShowFlags(FeatureFlagStore store, string label)
{
  var flags   = new[] { "GenericResource.Enabled", "TextControl.Enabled" };
  var tenants = new[] { "TenantA", "TenantB" };
  
  tenants.SelectMany(t => flags.Select(f => store.Evaluate(f, t)))
         .Dump(label);
}

void ShowJson(FileResourceStateStore stateStore, FeatureFlagStore flagStore, string label)
{
  new { StateFile = File.ReadAllText(StateFilePath())
       ,FlagFile  = File.ReadAllText(FlagFilePath()) }.Dump(label);
}

// ── Path helpers ──────────────────────────────────────────────
string ResourceFilePath()
{
  var queriesRoot        = Path.GetDirectoryName(Util.CurrentQueryPath);
  var circuitBreakerRoot = Path.GetFullPath(Path.Combine(queriesRoot, ".."));
  
  return Path.Combine(circuitBreakerRoot, "_Data", "FileResource", "resource.json");
}

string StateFilePath()
{
  var queriesRoot        = Path.GetDirectoryName(Util.CurrentQueryPath);
  var circuitBreakerRoot = Path.GetFullPath(Path.Combine(queriesRoot, ".."));
  
  return Path.Combine(circuitBreakerRoot, "_Data", "FileResource", "fileresource-state.json");
}

string FlagFilePath()
{
  var queriesRoot        = Path.GetDirectoryName(Util.CurrentQueryPath);
  var circuitBreakerRoot = Path.GetFullPath(Path.Combine(queriesRoot, ".."));
  
  return Path.Combine(circuitBreakerRoot, "_Data", "HttpApi", "featureflags.json");
}
