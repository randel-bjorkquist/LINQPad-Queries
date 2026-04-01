<Query Kind="Program" />

#r "C:\Windows\System32\inetsrv\Microsoft.Web.Administration.dll"
#r "nuget: System.ServiceProcess.ServiceController, 8.0.0"
#r "nuget: Polly, 8.4.2"


#load "../_Shared/Models/AppPoolState.linq"
#load "../_Shared/AppPoolStateStore.linq"
#load "../_Shared/CircuitBreaker.linq"

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Web.Administration;

async Task Main()
{
	var appPools = new[] { "PHA-Web", "PHA-WebCore" };
	var store    = new AppPoolStateStore(StateFilePath());
	store.Load();

	// ── One circuit breaker per app pool ─────────────────────
	// No flag listener — this is standalone resilience tracking.
	// Each breaker is independent — PHA-Web going down does not
	// affect PHA-WebCore's circuit state.
	var breakers = appPools.ToDictionary( name => name
                                       ,name => new CircuitBreaker<string>( name:                     name
                                                                           ,exceptionsBeforeBreaking: 3
                                                                           ,durationOfBreak:          TimeSpan.FromSeconds(15)));

	// ── Wire state change events to the state store ──────────
	foreach(var (name, breaker) in breakers)
	{
		var poolName = name;   // capture for closure

		breaker.OnOpen += (ex, duration) =>
		{
			store.UpdateState( appPool:      poolName
                        ,circuitState: "Open"
                        ,appPoolState: GetAppPoolState(poolName)
                        ,failureCount: 3
                        ,message:      $"Circuit opened — {ex.Message}" );
                        
			ShowJson("JSON — circuit opened");
		};

		breaker.OnHalfOpen += () =>
		{
			store.UpdateState( appPool:      poolName
                        ,circuitState: "HalfOpen"
                        ,appPoolState: GetAppPoolState(poolName)
                        ,failureCount: 0
                        ,message:      "Probing app pool state" );
                        
			ShowJson("JSON — circuit half-open");
		};

		breaker.OnClose += () =>
		{
			store.UpdateState( appPool:      poolName
                        ,circuitState: "Closed"
                        ,appPoolState: GetAppPoolState(poolName)
                        ,failureCount: 0
                        ,message:      "App pool recovered" );
                        
			ShowJson("JSON — circuit closed");
		};
	}

	// ── Polling loop ─────────────────────────────────────────
	// Checks each app pool every 3 seconds. Records a failure
	// against the circuit breaker when the pool is not Started.
	// Records a success when it is Started.
	// Stop the script manually when done (cancel button).
	var cts = new CancellationTokenSource();

	"Poller starting — stop and start app pools in IIS Manager to trigger state changes.".Dump("▶ Poller");
	"Polling every 3 seconds — press cancel in LINQPad toolbar to stop.".Dump("▶ Poller");

	ShowState("Initial state");
	ShowJson("JSON — initial");

	while(!cts.Token.IsCancellationRequested)
	{
		foreach(var (name, breaker) in breakers)
		{
			var result = await breaker.ExecuteAsync(async () =>
			{
				await Task.CompletedTask;

				var state = GetAppPoolState(name);

				if (state != "Started")
					throw new InvalidOperationException($"{name} is {state} — expected Started.");

				return state;
			});

			// Update state store on every poll regardless of outcome
			store.UpdateState( appPool:      name
                        ,circuitState: breaker.State.ToString()
                        ,appPoolState: GetAppPoolState(name)
                        ,failureCount: result.Succeeded ? 0 
                                                        : result.CircuitOpen ? 3 : 0
                        ,message:      result.Succeeded ? "Healthy"
				                                                : result.ToString()
			);
		}

		ShowState($"Poll — {DateTime.Now:HH:mm:ss}");

		await Task.Delay(TimeSpan.FromSeconds(3), cts.Token)
		          .ContinueWith(_ => { });   // swallow cancellation exception
	}
}

// ── Read app pool state directly from IIS ────────────────────
string GetAppPoolState(string appPoolName)
{
	try
	{
		using var iis = new ServerManager();
		var pool      = iis.ApplicationPools[appPoolName];
		return pool?.State.ToString() ?? "Unknown";
	}
	catch (Exception ex)
	{
		return $"Error: {ex.Message}";
	}
}

// ── Display helpers ──────────────────────────────────────────
void ShowState(string label) 
  => new AppPoolStateStore(StateFilePath()).Tap(s => { s.Load(); s.GetAll().Dump(label); });

void ShowJson(string label) 
  => File.ReadAllText(StateFilePath()).Dump(label);

string StateFilePath()
{
	var queriesRoot        = Path.GetDirectoryName(Util.CurrentQueryPath);
	var circuitBreakerRoot = Path.GetFullPath(Path.Combine(queriesRoot, ".."));
	var dataPath           = Path.Combine(circuitBreakerRoot, "_Data", "AppPool");
	Directory.CreateDirectory(dataPath);
	return Path.Combine(dataPath, "apppool-state.json");
}

static class Extensions
{
	public static T Tap<T>(this T value, Action<T> action)
	{
		action(value);
		return value;
	}
}