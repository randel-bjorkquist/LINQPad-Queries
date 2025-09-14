<Query Kind="Program" />

void Main()
{
  var logger = ConsoleLogger.Instance;
  logger.Log("blah blah blah");

}


public class Logger
{
  private static readonly Logger _instance = new Logger();

  private Logger() { }

  public static Logger Instance => _instance;
  public void Log(string message) => Console.WriteLine(message);
}

public class ConsoleLogger
{
  private static readonly ConsoleLogger _instance = new ConsoleLogger();

  private ConsoleLogger() { }

  public static ConsoleLogger Instance => _instance;
  public void Log(string message) => Console.WriteLine(message);
}

//public static class Logger
//{
//  public static void Log(string message) => Console.WriteLine(message);
//}