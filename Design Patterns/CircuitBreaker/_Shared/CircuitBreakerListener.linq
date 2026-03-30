<Query Kind="Statements" />

#load "./Models/FeatureFlagDocument.linq"
#load "./Models/FlagResult.linq"
#load "./FeatureFlagStore.linq"
#load "./CircuitBreaker.linq"

// ─────────────────────────────────────────────────────────────
// #load paths in .Shared files use "./" — resolves relative to this file.
// #load paths in scenario scripts use "../.Shared/" — resolves relative to the script.
// ─────────────────────────────────────────────────────────────
// Optional bridge between a CircuitBreaker and a FeatureFlagStore.
//
// Attach this when a dependency has a corresponding feature flag.
// Skip it entirely when the circuit breaker is standalone
// (database connections, background jobs, etc.)
//
// Wiring example:
//   var listener = new CircuitBreakerListener(store, "TextControl.Enabled");
//   listener.Attach(circuitBreaker);
// ─────────────────────────────────────────────────────────────
class CircuitBreakerListener<T>
{
  private readonly FeatureFlagStore _store;
  private readonly string _flag;

  public CircuitBreakerListener(FeatureFlagStore store, string flag)
  {
    _store = store;
    _flag = flag;
  }

  public void Attach(CircuitBreaker<T> breaker)
  {
    breaker.OnOpen += OnOpen;
    breaker.OnClose += OnClose;
    breaker.OnHalfOpen += OnHalfOpen;
  }

  // Circuit opened — flip the flag off in the runtime block
  private void OnOpen(Exception ex, TimeSpan breakDuration)
  {
    $"[FlagListener] Setting runtime override: {_flag} = false"
      .Dump("🚩 Flag Store");
    _store.SetRuntime(_flag, false, circuitState: "Open");
  }

  // Circuit closed — remove runtime override, flag reverts naturally
  private void OnClose()
  {
    $"[FlagListener] Clearing runtime override: {_flag}"
      .Dump("🚩 Flag Store");
    _store.ClearRuntime(_flag);
  }

  // Half-open — update circuit state metadata, value stays false
  private void OnHalfOpen()
  {
    $"[FlagListener] Updating runtime state: {_flag} = HalfOpen"
      .Dump("🚩 Flag Store");
    _store.SetRuntime(_flag, false, circuitState: "HalfOpen");
  }
}