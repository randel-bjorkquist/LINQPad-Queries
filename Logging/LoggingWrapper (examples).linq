<Query Kind="Program">
  <NuGetReference>Serilog</NuGetReference>
  <NuGetReference>Serilog.Extensions.Logging</NuGetReference>
  <NuGetReference>Serilog.Sinks.Console</NuGetReference>
  <NuGetReference>Serilog.Sinks.Seq</NuGetReference>
  <Namespace>Serilog</Namespace>
</Query>

#load "Logging\LoggingWrapper"

void Main()
{
  using var logger_factory = CreateLoggerFactory();
  var logger = logger_factory.CreateLogger("DEMO");
  
  // Simple example of using the LoggingWrapper ...
  var result = LoggingWrapper.Run( logger
                                  ,() => Add(3, 5));
  
  result.Dump();
}

public int Add(int left, int right) 
  => left + right;
  
public static ILoggerFactory CreateLoggerFactory()
{
  var serilog_logger = new Serilog.LoggerConfiguration()
                                  .MinimumLevel.Debug()
                                  .WriteTo.Console(outputTemplate: "[{Level:u3}|{Timestamp:HH:mm:ss.fff}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                                  .WriteTo.Seq("http://localhost:5341")
                                  .Enrich.WithProperty("Application", $"LoggingWrapper (examples)")
                                  .Enrich.WithProperty("Environment", $"LINQPad")
                                  .CreateLogger();

  return LoggerFactory.Create(builder => { builder.ClearProviders()
                                                  .AddSerilog(serilog_logger, dispose: true); });
}