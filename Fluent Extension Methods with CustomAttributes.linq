<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
  string.Empty.Dump("5.Times(\"Hello World\");");
  5.Times("Hello World");

  string.Empty.Dump("Thread.Sleep(1.Second());");
  Thread.Sleep(1.Second());
  
  string.Empty.Dump("Thread.Sleep(2.Second());");
  Thread.Sleep(2.Seconds());

  "Hello World".ToSlug()
               .Dump("\"Hello World\".ToSlug();");

  //User user = null;
  //user.ThrowIfNull(nameof(user));

  var user = new User { ID        = 1
                       ,FirstName = "Fred"
                       ,LastName  = "Flintstone" };
  
  user.Tap(u => u.IsActive = true)
      .Tap(u => u.LastLogin = DateTime.UtcNow);

  //NOTE: This one I'm not so keen on; not sure I'm implementing the way
  //      the author's envisoning it.
  user.Status.ToDescription()
             .Dump("user.Status.ToDescription();");
}

public static class IntegerExtensions
{
  public static void Times(this int count, string message)
  {
    for(int i = 0; i < count; i++) 
      Console.WriteLine(message);
  }
  
  public static TimeSpan Second(this int value)
    => Seconds(value);
  
  public static TimeSpan Seconds(this int value)
    => TimeSpan.FromSeconds(value);
    
  public static string ToSlug(this string value)
    => value.ToLower()
            .Replace(' ', '-');
            
  
}

public static class ObjectExtensions
{
  public static void ThrowIfNull<T>(this T obj, string paramName) where T : class
  {
    if(obj is null)
      throw new ArgumentNullException(paramName);
  }
  
  public static T Tap<T>(this T obj, Action<T> action)
  {
    action(obj);
    return obj;
  }
}

public static class EnumExtensions
{
  public static string ToDescription(this Enum value)
    => value.GetType()
            .GetField(value.ToString())
           ?.GetCustomAttribute<CustomEnumAttribute>()
           ?.Description ?? value.ToString();
}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class CustomEnumAttribute : Attribute
{
  public string Description { get; }

  public CustomEnumAttribute(string description)
  {
    Description = description;
  }
}

[Flags]
public enum StatusType
{
  [CustomEnum("User has no value set")]
  NotSet   = 0,
   
  [CustomEnum("User is Active")]
  Active   = 1,
  
  [CustomEnum("User is Inactive.")]
  Inactive = 2,
  
  [CustomEnum("User has been Deleted")]
  Deleted  = 4
}

public class User
{
  public int ID             { get; set; }
  public string FirstName   { get; set; } = string.Empty;
  public string LastName    { get; set; } = string.Empty;
                            
  public DateTime LastLogin { get; set; }  
  public StatusType Status  { get; set; }
  
  public bool IsActive
  {
    get => Status == StatusType.Active;          
    set
    {
      Status = value 
             ? StatusType.Active
             : StatusType.Inactive;
    }
  }
}
