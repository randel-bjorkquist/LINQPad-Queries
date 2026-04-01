<Query Kind="Program" />

/// C — Polly Bare Minimum
///
/// The exact same circuit breaker pattern as A and B, but now
/// Polly drives it automatically. No manual RecordFailure(),
/// no manual TryProbe() — Polly handles all of that internally.
///
/// What Polly automates that you did manually in B:
///   - Counting failures and opening the circuit
///   - Waiting for the break duration to elapse
///   - Transitioning to HalfOpen automatically
///   - Closing on a successful probe
///   - Reopening on a failed probe
///
/// One bool drives everything:
///   serviceAvailable = true  → calls succeed
///   serviceAvailable = false → calls fail
///
/// No stores, no flags, no listeners — just Polly and a lambda.

// If NuGet packages fail to load — press Alt+Shift+Minus to restore
#r "nuget: Polly, 8.4.2"

using System;
using System.Threading.Tasks;
using Polly;
using Polly.CircuitBreaker;

async Task Main()
{
	// ── Toggle this to simulate the dependency ────────────────
	var serviceAvailable = true;

	// ── The simulated service call ────────────────────────────
	async Task<string> CallService()
	{
		await Task.Delay(50);   // simulate latency

		if (!serviceAvailable)
			throw new InvalidOperationException("Service unreachable.");

		return "result from service";
	}

	// ── The circuit breaker — Polly's bare minimum ────────────
	// Three settings:
	//   exceptionsAllowedBeforeBreaking — how many failures open it
	//   durationOfBreak                 — how long it stays open
	//   onBreak / onReset / onHalfOpen  — callbacks so we can see it
	var breaker = Policy
		.Handle<Exception>()
		.CircuitBreakerAsync(
			exceptionsAllowedBeforeBreaking: 3,
			durationOfBreak:                 TimeSpan.FromSeconds(10),
			onBreak: (ex, duration) =>
				$"⚡ STATE → Open  (breaking for {duration.TotalSeconds}s — {ex.Message})"
					.Dump("Circuit Breaker"),
			onReset: () =>
				"✅ STATE → Closed  (service recovered)"
					.Dump("Circuit Breaker"),
			onHalfOpen: () =>
				"🔁 STATE → HalfOpen  (probing...)"
					.Dump("Circuit Breaker")
		);

	// ── Helper: execute through the breaker ──────────────────
	async Task Attempt(string label)
	{
		try
		{
			var result = await breaker.ExecuteAsync(CallService);
			$"✅ {label} — {result}  [circuit: {breaker.CircuitState}]"
				.Dump("Call");
		}
		catch (BrokenCircuitException)
		{
			$"⚡ {label} — BLOCKED, circuit is open"
				.Dump("Call");
		}
		catch (Exception ex)
		{
			$"❌ {label} — FAILED: {ex.Message}  [circuit: {breaker.CircuitState}]"
				.Dump("Call");
		}
	}

	// ─────────────────────────────────────────────────────────
	// Phase 1 — Service healthy
	// ─────────────────────────────────────────────────────────
	"── Phase 1 — Service healthy ──".Dump();
	await Attempt("Call 1");
	await Attempt("Call 2");
	await Attempt("Call 3");

	// ─────────────────────────────────────────────────────────
	// Phase 2 — Service fails, circuit opens
	// ─────────────────────────────────────────────────────────
	"── Phase 2 — Service fails ──".Dump();
	serviceAvailable = false;

	await Attempt("Call 4");   // fail 1
	await Attempt("Call 5");   // fail 2
	await Attempt("Call 6");   // fail 3 — circuit opens
	await Attempt("Call 7");   // blocked
	await Attempt("Call 8");   // blocked

	// ─────────────────────────────────────────────────────────
	// Phase 3 — Wait for half-open (Polly does this automatically)
	// ─────────────────────────────────────────────────────────
	"── Phase 3 — Waiting 11s for Polly to half-open... ──".Dump();
	await Task.Delay(TimeSpan.FromSeconds(11));

	// Service still down — probe fails, circuit reopens
	await Attempt("Probe 1");

	// ─────────────────────────────────────────────────────────
	// Phase 4 — Service recovers
	// ─────────────────────────────────────────────────────────
	"── Phase 4 — Service recovers ──".Dump();
	serviceAvailable = true;

	await Task.Delay(TimeSpan.FromSeconds(11));

	// Probe succeeds — circuit closes automatically
	await Attempt("Probe 2");

	// ─────────────────────────────────────────────────────────
	// Phase 5 — Normal operation restored
	// ─────────────────────────────────────────────────────────
	"── Phase 5 — Normal operation restored ──".Dump();
	await Attempt("Call 9");
	await Attempt("Call 10");

	$"Final circuit state: {breaker.CircuitState}".Dump("Done");
}