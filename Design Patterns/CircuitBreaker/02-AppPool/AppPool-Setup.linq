<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#r "C:\Windows\System32\inetsrv\Microsoft.Web.Administration.dll"
#r "nuget: System.ServiceProcess.ServiceController, 8.0.0"

#load "../_Shared/Models/AppPoolState.linq"
#load "../_Shared/AppPoolStateStore.linq"

using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Web.Administration;

async Task Main()
{
	// ── Verify IIS app pools are visible ─────────────────────
	var appPools = new[] { "PHA-Web", "PHA-WebCore" };

	using var iis = new ServerManager();

	appPools
		.Select(name =>
		{
			var pool = iis.ApplicationPools[name];
			return new
			{
				AppPool = name,
				Found   = pool != null,
				State   = pool?.State.ToString() ?? "Not Found"
			};
		})
		.Dump("IIS App Pools — Detected");

	// ── Initialize state file ────────────────────────────────
	var store = new AppPoolStateStore(StateFilePath());
	store.Load();

	foreach (var name in appPools)
	{
		var pool  = iis.ApplicationPools[name];
		var state = pool?.State.ToString() ?? "Unknown";

		store.UpdateState(
			appPool:      name,
			circuitState: "Closed",
			appPoolState: state,
			failureCount: 0,
			message:      "Initialized"
		);
	}

	store.GetAll().Dump("Initial Circuit State");
	File.ReadAllText(StateFilePath()).Dump("JSON on disk — Initial");
}

string StateFilePath()
{
	var queriesRoot        = Path.GetDirectoryName(Util.CurrentQueryPath);
	var circuitBreakerRoot = Path.GetFullPath(Path.Combine(queriesRoot, ".."));
	var dataPath           = Path.Combine(circuitBreakerRoot, "_Data", "AppPool");
	Directory.CreateDirectory(dataPath);
	return Path.Combine(dataPath, "apppool-state.json");
}