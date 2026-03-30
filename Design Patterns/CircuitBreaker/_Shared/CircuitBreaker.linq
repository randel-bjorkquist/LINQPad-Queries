<Query Kind="Statements">
  <NuGetReference>Polly</NuGetReference>
  <Namespace>Polly</Namespace>
  <Namespace>Polly.CircuitBreaker</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

// ─────────────────────────────────────────────────────────────
// #load paths in .Shared files use "./" — resolves relative to this file.
// #load paths in scenario scripts use "../_Shared/" — resolves relative to the script.
// ─────────────────────────────────────────────────────────────
// Reusable circuit breaker wrapper.
//
// Completely decoupled from feature flags — knows nothing about
// the flag system. Exposes three events that anything can listen
// to: OnOpen, OnClose, OnHalfOpen.
//
// State machine:
//
//   Closed ──(failures exceed threshold)──► Open
//     ▲                                      │
//     │                                      │ (break duration elapses)
//     │                                      ▼
//   (probe succeeds)◄────────────────── HalfOpen
//                                            │
//                                            │ (probe fails)
//                                            ▼
//                                           Open
// ─────────────────────────────────────────────────────────────
class CircuitBreaker<T>
{
  // ── Events ───────────────────────────────────────────────
  // Any number of listeners can subscribe — flag writer is
  // just one possible listener alongside logging, metrics, etc.
  public event Action<Exception, TimeSpan> OnOpen;
  public event Action OnClose;
  public event Action OnHalfOpen;

  // ── Configuration ────────────────────────────────────────
  private readonly string _name;
  private readonly int _exceptionsBeforeBreaking;
  private readonly TimeSpan _durationOfBreak;

  // ── Polly policy ─────────────────────────────────────────
  private readonly AsyncCircuitBreakerPolicy _policy;

  // ── Current state (readable without executing) ───────────
  public CircuitState State => _policy.CircuitState;
  public bool IsOpen => State == CircuitState.Open;

  public CircuitBreaker(string name, int exceptionsBeforeBreaking = 3, TimeSpan? durationOfBreak = null)
  {
    _name = name;
    _exceptionsBeforeBreaking = exceptionsBeforeBreaking;
    _durationOfBreak = durationOfBreak ?? TimeSpan.FromSeconds(30);

    _policy = Policy
      .Handle<Exception>()
      .CircuitBreakerAsync(
        exceptionsAllowedBeforeBreaking: _exceptionsBeforeBreaking,
        durationOfBreak: _durationOfBreak,
        onBreak: (ex, breakDuration) =>
        {
          $"[{_name}] Circuit OPEN — breaking for {breakDuration.TotalSeconds}s. Reason: {ex.Message}"
            .Dump("⚡ Circuit Breaker");
          OnOpen?.Invoke(ex, breakDuration);
        },
        onReset: () =>
        {
          $"[{_name}] Circuit CLOSED — service recovered."
            .Dump("✅ Circuit Breaker");
          OnClose?.Invoke();
        },
        onHalfOpen: () =>
        {
          $"[{_name}] Circuit HALF-OPEN — probing service..."
            .Dump("🔁 Circuit Breaker");
          OnHalfOpen?.Invoke();
        }
      );
  }

  // ── Execute with circuit breaker protection ───────────────
  // Returns (success, result, exception) so callers can handle
  // both BrokenCircuitException and execution exceptions cleanly.
  public async Task<CircuitBreakerResult<T>> ExecuteAsync(Func<Task<T>> action)
  {
    try
    {
      var result = await _policy.ExecuteAsync(action);
      return CircuitBreakerResult<T>.Success(result);
    }
    catch (BrokenCircuitException ex)
    {
      return CircuitBreakerResult<T>.Tripped(ex);
    }
    catch (Exception ex)
    {
      return CircuitBreakerResult<T>.Failed(ex);
    }
  }
}

// ─────────────────────────────────────────────────────────────
// Result wrapper — lets callers pattern-match on outcome without
// catching exceptions themselves.
// ─────────────────────────────────────────────────────────────
class CircuitBreakerResult<T>
{
  public bool Succeeded       { get; private set; }
  public T Value              { get; private set; }
  public Exception Exception  { get; private set; }
  public bool CircuitOpen     { get; private set; }

  public static CircuitBreakerResult<T> Success(T value) 
    => new() { Succeeded = true, Value = value };

  public static CircuitBreakerResult<T> Tripped(BrokenCircuitException ex) 
    => new() { Succeeded = false, CircuitOpen = true, Exception = ex };

  public static CircuitBreakerResult<T> Failed(Exception ex) 
    => new() { Succeeded = false, CircuitOpen = false, Exception = ex };

  public override string ToString() 
    => Succeeded ? $"✅ Success: {Value}" 
                 : CircuitOpen ? $"⚡ Circuit open — {Exception.Message}" 
                               : $"❌ Failed — {Exception.Message}";
}