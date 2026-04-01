<Query Kind="Program" />

/// A — Pure State Machine
///
/// The circuit breaker pattern reduced to its absolute minimum.
/// No Polly, no async, no external dependencies.
///
/// Three states:
///   Closed   — calls pass through normally
///   Open     — calls are blocked immediately, no attempt made
///   HalfOpen — one probe call allowed through to test recovery
///
/// One bool drives everything:
///   serviceAvailable = true  → calls succeed
///   serviceAvailable = false → calls fail
///
/// Flip serviceAvailable in the code and rerun to see the
/// state machine respond.
void Main()
{
  #region setup/configuration ----------------------------------
  
  // -- Toggle this to simulate the dependency -----------------
	var serviceAvailable = true;

  // -- Circuit breaker state ----------------------------------
	var state         = CircuitState.Closed;
	var failureCount  = 0;
	var failureLimit  = 3;
	var successCount  = 0;

  #endregion
  
  #region Helper Method(s) -------------------------------------
  
  // -- Simulated service call ---------------------------------
	bool TryCallService(string label)
	{
		// Circuit is open — block immediately, no attempt
		if(state == CircuitState.Open)
		{
			$"  [{label}] ⚡ BLOCKED — circuit is open, not attempting call".Dump();
			return false;
		}

		// Circuit is closed or half-open — attempt the call
		if(serviceAvailable)
		{
			successCount++;
			failureCount = 0;

			$"  [{label}] ✅ SUCCESS — call completed normally".Dump();

			// Half-open probe succeeded — close the circuit
			if(state == CircuitState.HalfOpen)
			{
				state = CircuitState.Closed;
				$"  [{label}] 🔒 STATE → Closed (recovered)".Dump();
			}

			return true;
		}
		else
		{
			failureCount++;
			successCount = 0;

			$"  [{label}] ❌ FAILED — call failed (failure {failureCount} of {failureLimit})".Dump();

			// Half-open probe failed — reopen the circuit
			if(state == CircuitState.HalfOpen)
			{
				state = CircuitState.Open;
				$"  [{label}] ⚡ STATE → Open (probe failed, reopened)".Dump();
				return false;
			}

			// Failure threshold reached — open the circuit
			if(failureCount >= failureLimit)
			{
				state = CircuitState.Open;
				$"  [{label}] ⚡ STATE → Open (threshold reached)".Dump();
			}

			return false;
		}
	}

	void ShowState(string label) 
    => $"State = {state} | Failures = {failureCount} | Successes = {successCount}".Dump(label);

  #endregion

  #region Run --------------------------------------------------

	"── Phase 1 — Service healthy, circuit closed ──".Dump();
	ShowState("Initial");
  
	TryCallService("Call 1");
	TryCallService("Call 2");
	TryCallService("Call 3");
  
	ShowState("After healthy calls");

	"── Phase 2 — Service fails, circuit opens ──".Dump();
	serviceAvailable = false;
  
	TryCallService("Call 4");
	TryCallService("Call 5");
	TryCallService("Call 6");  // opens circuit
	TryCallService("Call 7");  // blocked — circuit open
	TryCallService("Call 8");  // blocked — circuit open
  
	ShowState("After failures");

	"── Phase 3 — Manual probe (still failing) ──".Dump();
	// Simulate half-open by manually setting state. In production Polly does 
  // this automatically after a timeout.
	state = CircuitState.HalfOpen;
  
	$"  [Manual] 🔁 STATE → HalfOpen (simulating timeout elapsed)".Dump();
  
	TryCallService("Probe 1");  // fails — reopens
  
	ShowState("After failed probe");

	"── Phase 4 — Service recovers ──".Dump();
	serviceAvailable = true;
	state            = CircuitState.HalfOpen;
  
	$"  [Manual] 🔁 STATE → HalfOpen (simulating timeout elapsed)".Dump();
  
	TryCallService("Probe 2");  // succeeds — closes circuit
	TryCallService("Call 9");   // normal call — circuit closed
	TryCallService("Call 10");  // normal call — circuit closed

	ShowState("After recovery");
  
  #endregion
}

/// The three states a circuit breaker can be in. This is the 
/// complete state space, nothing else exists.
///   Closed   → Normal operation — calls pass through
///   Open     → Fault detected — calls blocked immediately
///   HalfOpen → Recovery probe — one test call allowed
enum CircuitState { Closed, Open, HalfOpen }
