<Query Kind="Program" />

void Main()
{
  ConfigureMappings();
  
  var user = new User { ID        = Guid.NewGuid()
                       ,FirstName = "Fred"
                       ,LastName  = "Flintstone"
                       ,Email     = "fred.flintstone@bedrock.bc" };

  // Desired syntax: destination first, source as parameter
  UserDTO dto = Mapper.Map<UserDTO>(user);
  dto.Dump("User → UserDTO");

  User back = Mapper.Map<User>(dto);
  back.Dump("UserDTO → User");

  var users = new List<User> { user, back };
  var dtos  = users.Select(u => Mapper.Map<UserDTO>(u)).ToList();
  dtos.Dump("Collection mapping");
}

void ConfigureMappings()
{
  Mapper.AddMapping<User, UserDTO>(u => new UserDTO( u.ID, $"{u.FirstName} {u.LastName}".Trim(), u.Email));

  Mapper.AddMapping<UserDTO, User>(d => {
      var parts = (d.Name ?? "").Split(' ', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
      
      return new User { ID        = d.ID
                       ,FirstName = parts.Length > 0 ? parts[0] : ""
                       ,LastName  = parts.Length > 1 ? parts[1] : ""
                       ,Email     = d.Email };
  });

  // Easily add more from other modules/assemblies
  // OtherModule.RegisterMappings();
}

// Static mapper class - this is the standard pattern
public static class Mapper
{
  private static readonly Dictionary<(Type Source, Type Destination), Delegate> Maps = new();

  // Public for controlled access
  internal static IReadOnlyDictionary<(Type Source, Type Destination), Delegate> Registrations => Maps;

  // Explicit registration — called from startup
  public static void AddMapping<TSource, TDestination>(Func<TSource, TDestination> mapper)
  {
    Maps[(typeof(TSource), typeof(TDestination))] = mapper;
  }

  // Optional: bulk registration via assembly scanning (advanced)
  public static void RegisterFromAssemblyContaining<T>()
  {
    // Scan for types implementing IMapFrom<,> or attributes — common in real apps
    // Omitted for brevity, but this is how AutoMapper does it
  }

  public static TDestination Map<TDestination>(object source)
  {
    if (source == null)
      throw new ArgumentNullException(nameof(source));

    var sourceType = source.GetType();
    var key = (sourceType, typeof(TDestination));

    if (Maps.TryGetValue(key, out var del))
      return (TDestination)del.DynamicInvoke(source)!;

    throw new InvalidOperationException($"No mapping registered from {sourceType.Name} to {typeof(TDestination).Name}");
  }
}

// Your types
public sealed record UserDTO(Guid ID, string Name, string Email);

public sealed class User
{
    public Guid ID          { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName  { get; init; } = string.Empty;
    public string Email     { get; init; } = string.Empty;
}

