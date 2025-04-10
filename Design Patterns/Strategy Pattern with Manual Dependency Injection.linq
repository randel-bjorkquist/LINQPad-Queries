<Query Kind="Program" />

void Main()
{
  // Manual Dependency Injection: create the list of strategies ...
  //List<INotificationStrategy> strategies = null;  /* NOTE: should cause an ArgumentNullException to be thrown ... */
  var strategies = new List<INotificationStrategy> { new EmailStrategy()
                                                    ,new SmsStrategy() };
  
  // Inject the strategies into the NotificationService ...
  var notificationService = new NotificationService(strategies);

  // Test the Notify method ...
  notificationService.Notify("email", "Hello via Email!");
  notificationService.Notify("sms", "Hello via SMS!");
  notificationService.Notify("fax", "This should fail"); // Test invalid channel

  // Optional: dump the strategies to see what's registered
  strategies.Select(s => s.Name)
            .Dump("Registered Strategies");
}

// Interfaces and Classes
public interface INotificationStrategy
{
  string Name { get; }
  void Send(string message);
}

public class EmailStrategy : INotificationStrategy
{
  public string Name => "email";
  public void Send(string message) => Console.WriteLine($"Email: {message}");
}

public class SmsStrategy : INotificationStrategy
{
  public string Name => "sms";
  public void Send(string message) => Console.WriteLine($"SMS: {message}");
}

public class NotificationService
{
  private readonly IEnumerable<INotificationStrategy> _strategies;
  
  public NotificationService(IEnumerable<INotificationStrategy> strategies)
  {
    _strategies = strategies ?? throw new ArgumentNullException(nameof(strategies));
  }
  
  public void Notify(string channel, string message)
  {
    var strategy = _strategies.SingleOrDefault(s => s.Name == channel);
    
    if(strategy is null)
    {
      Console.WriteLine($"Error: No strategy found for channel '{channel}'.");
      return;
    }
    
    strategy.Send(message);
  }
  
  public bool Unsubscribe(INotificationStrategy strategy)
  {
    throw new NotImplementedException("ERROR: NotificationService.Unsubscribe has not been implemented yet !!!");
  }
  
  public bool Subscribe(INotificationStrategy strategy)
  {
    throw new NotImplementedException("ERROR: NotificationService.Subscribe has not been implemented yet !!!");
  }
}