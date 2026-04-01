<Query Kind="Program" />

/// B — Manually Stepped State Machine
///
/// You drive the circuit breaker by calling:
///   cb.RecordSuccess() — simulates a call succeeding
///   cb.RecordFailure() — simulates a call failing
///   cb.TryProbe()      — manually triggers a half-open probe
///   cb.Execute(action) — runs an action through the breaker
///
/// No automatic thresholds firing in the background.
/// No timeouts. No Polly.
///
/// The value here is seeing each state transition happen
/// one explicit step at a time — you are the thing that
/// decides when to probe and when to recover.
///
/// This is the mental model you need before using Polly,
/// because Polly automates exactly these steps.
void Main()
{
	var cb = new ManualCircuitBreaker(failureLimit: 3);

	void ShowState(string label) 
    => cb.GetState().Dump(label);

	// ─────────────────────────────────────────────────────────
	// Phase 1 — Normal operation
	// ─────────────────────────────────────────────────────────
	"── Phase 1 — Normal operation ──".Dump();
	ShowState("Initial");

	cb.Execute("Call 1", () => "result A");
	cb.Execute("Call 2", () => "result B");
	cb.Execute("Call 3", () => "result C");
  
	ShowState("After 3 successes");

	// ─────────────────────────────────────────────────────────
	// Phase 2 — Failures accumulate
	// ─────────────────────────────────────────────────────────
	"── Phase 2 — Failures accumulate ──".Dump();

	cb.RecordFailure("Timeout connecting to service");
	ShowState("After failure 1");

	cb.RecordFailure("Timeout connecting to service");
	ShowState("After failure 2");

	cb.RecordFailure("Timeout connecting to service");
	ShowState("After failure 3 — circuit opens");

	// ─────────────────────────────────────────────────────────
	// Phase 3 — Circuit open, calls blocked
	// ─────────────────────────────────────────────────────────
	"── Phase 3 — Circuit open, calls blocked ──".Dump();

	cb.Execute("Call 4", () => "result D");  // blocked
	cb.Execute("Call 5", () => "result E");  // blocked
  
	ShowState("While circuit open");

	// ─────────────────────────────────────────────────────────
	// Phase 4 — Probe fails, circuit stays open
	// ─────────────────────────────────────────────────────────
	"── Phase 4 — Probe attempt (service still down) ──".Dump();

	cb.TryProbe();
	cb.RecordFailure("Still unreachable");
  
	ShowState("After failed probe");

	// ─────────────────────────────────────────────────────────
	// Phase 5 — Probe succeeds, circuit closes
	// ─────────────────────────────────────────────────────────
	"── Phase 5 — Probe attempt (service recovered) ──".Dump();

	cb.TryProbe();
	cb.RecordSuccess();
  
	ShowState("After successful probe — circuit closes");

	// ─────────────────────────────────────────────────────────
	// Phase 6 — Normal operation restored
	// ─────────────────────────────────────────────────────────
	"── Phase 6 — Normal operation restored ──".Dump();

	cb.Execute("Call 6", () => "result F");
	cb.Execute("Call 7", () => "result G");
  
	ShowState("Final state");
}

// ─────────────────────────────────────────────────────────────
// A circuit breaker you drive manually — no automatic timing,
// no background threads. Every transition is explicit.
// ─────────────────────────────────────────────────────────────
class ManualCircuitBreaker
{
	private readonly int _failureLimit;

	private CircuitBreakerState _state        = CircuitBreakerState.Closed;
	private int                 _failureCount = 0;
	private int                 _successCount = 0;
	private string              _lastError    = null;

	public ManualCircuitBreaker(int failureLimit) 
    => _failureLimit = failureLimit;

	// ── Record a successful call ──────────────────────────────
	public void RecordSuccess()
	{
		_successCount++;
    _failureCount = 0;
		_lastError    = null;

		if(_state == CircuitBreakerState.HalfOpen)
		{
			_state = CircuitBreakerState.Closed;
      $"✅ RecordSuccess → STATE: Closed (recovered after probe)".Dump();
			
      return;
		}

		$"✅ RecordSuccess → STATE: {_state}".Dump();
	}

	// ── Record a failed call ──────────────────────────────────
	public void RecordFailure(string reason)
	{
		_failureCount++;
    _successCount = 0;
		_lastError    = reason;

		if(_state == CircuitBreakerState.HalfOpen)
		{
			_state = CircuitBreakerState.Open;
      $"❌ RecordFailure → STATE: Open (probe failed — {reason})".Dump();
			
      return;
		}

		if(_failureCount >= _failureLimit)
		{
			_state = CircuitBreakerState.Open;
      $"❌ RecordFailure → STATE: Open (threshold {_failureLimit} reached — {reason})".Dump();
			
      return;
		}

		$"❌ RecordFailure → STATE: {_state} (failure {_failureCount} of {_failureLimit} — {reason})".Dump();
	}

	// ── Manually transition to half-open for a probe ─────────
	// In production Polly does this automatically after the
	// break duration elapses. Here you trigger it explicitly
	// so you can see exactly when and why it happens.
	public void TryProbe()
	{
		if(_state != CircuitBreakerState.Open)
		{
			$"🔁 TryProbe → ignored, current state is {_state}".Dump();
			return;
		}

		_state = CircuitBreakerState.HalfOpen;
		$"🔁 TryProbe → STATE: HalfOpen (one probe call now allowed)".Dump();
	}

	// ── Execute an action through the circuit breaker ────────
	public void Execute(string label, Func<string> action)
	{
		if(_state == CircuitBreakerState.Open)
		{
			$"⚡ [{label}] BLOCKED — circuit is open".Dump();
			return;
		}

		try
		{
			var result = action();
			$"✅ [{label}] SUCCESS → {result}".Dump();
      
			RecordSuccess();
		}
		catch(Exception ex)
		{
			RecordFailure(ex.Message);
		}
	}

	// ── Current state snapshot ────────────────────────────────
	public object GetState() 
    => new { State        = _state.ToString()
            ,FailureCount = _failureCount
            ,SuccessCount = _successCount
            ,FailureLimit = _failureLimit
            ,LastError    = _lastError };


  /// The three states a circuit breaker can be in. This is the 
  /// complete state space, nothing else exists.
  ///   Closed   → Normal operation — calls pass through
  ///   Open     → Fault detected — calls blocked immediately
  ///   HalfOpen → Recovery probe — one test call allowed
  private enum CircuitBreakerState { Closed, Open, HalfOpen }
}