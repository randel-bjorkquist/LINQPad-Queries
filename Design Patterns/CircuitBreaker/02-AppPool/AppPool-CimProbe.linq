<Query Kind="Program">
  <Namespace>Microsoft.Management.Infrastructure</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#r "nuget: Microsoft.Management.Infrastructure, 3.0.0"

async Task Main()
{
	// ── Probe 1 — confirm root\WebAdministration is accessible ──
	try
	{
		using var session = CimSession.Create("localhost");

		var appPools = session
			.QueryInstances(
				@"root\WebAdministration",
				"WQL",
				"SELECT * FROM ApplicationPool"
			)
			.ToList();

		appPools
			.Select(p => new
			{
				Name  = p.CimInstanceProperties["Name"]?.Value,
				State = p.CimInstanceProperties["State"]?.Value
			})
			.Dump("All App Pools — root\\WebAdministration");
	}
	catch (Exception ex)
	{
		ex.Message.Dump("❌ root\\WebAdministration query failed");
	}

	// ── Probe 2 — confirm PHA-Web and PHA-WebCore are visible ──
	try
	{
		using var session = CimSession.Create("localhost");

		var appPools = new[] { "PHA-Web", "PHA-WebCore" };

		foreach (var name in appPools)
		{
			var pool = session
				.QueryInstances(
					@"root\WebAdministration",
					"WQL",
					$"SELECT * FROM ApplicationPool WHERE Name = '{name}'"
				)
				.FirstOrDefault();

			if (pool == null)
			{
				$"❌ {name} — not found".Dump("App Pool");
				continue;
			}

			// Dump all available properties so we know exactly
			// what fields we can use in the full implementation
			pool.CimInstanceProperties
				.Select(p => new
				{
					Property = p.Name,
					Value    = p.Value,
					Type     = p.CimType.ToString()
				})
				.Dump($"✅ {name} — all properties");
		}
	}
	catch (Exception ex)
	{
		ex.Message.Dump("❌ App pool query failed");
	}
}