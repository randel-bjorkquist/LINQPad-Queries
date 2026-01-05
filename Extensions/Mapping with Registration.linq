<Query Kind="Program" />

void Main()
{
  //-------------------------------------------------------------------------------------------------------
  Mapping.Register<User, UserDTO>(u => new UserDTO(u.ID, $"{u.FirstName} {u.LastName}", u.Email));

  Mapping.Register<UserDTO, User>(d => {
    var parts = (d.Name ?? string.Empty).Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
    
    return new User { ID        = d.ID
                     ,FirstName = parts.ElementAtOrDefault(0) ?? string.Empty
                     ,LastName  = parts.ElementAtOrDefault(1) ?? string.Empty
                     ,Email     = d.Email ?? string.Empty };
  });

  //-------------------------------------------------------------------------------------------------------
  var user_fred_flintstone = new User { ID        = Guid.NewGuid()
                                       ,FirstName = "Fred"
                                       ,LastName  = "Flintstone"
                                       ,Email     = "fred.flintstone@bedrock.bc" }
                                  .Dump("user_fred_flintstone", 0);
  
  var user_barney_rubble = new User { ID        = Guid.NewGuid()
                                     ,FirstName = "Barney"
                                     ,LastName  = "Rubble"
                                     ,Email     = "barney.rubble@bedrock.bc" }
                                .Dump("user_barney_rubble", 0);
  
  var users = new List<User> { user_fred_flintstone
                              ,user_barney_rubble }
                    .Dump("users", 0);
  
  //-------------------------------------------------------------------------------------------------------
  UserDTO dto_fred_flintstone = user_fred_flintstone.MapTo<UserDTO>();
  dto_fred_flintstone.Dump("UserDTO dto_fred_flintstone = user_fred_flintstone.MapTo<UserDTO>();", 0);
  
  User fred_flintstone = dto_fred_flintstone.MapTo<User>();
  fred_flintstone.Dump("User fred_flintstone = dto_fred_flintstone.MapTo<User>();", 0);    
  
  var dtos  = users.MapToList<UserDTO>();
  dtos.Dump("var dtos  = users.MapToList<UserDTO>();", 0);
}

public static class Mapping
{
  private static readonly Dictionary<(Type Source, Type Destination), Delegate> _maps = new();

  public static void Register<TSource, TDestination>(Func<TSource, TDestination> map)
    => _maps[(typeof(TSource), typeof(TDestination))] = map;

  public static TDestination Map<TSource, TDestination>(TSource source)
  {
    if (source is null) 
      throw new ArgumentNullException(nameof(source));

    var key = (typeof(TSource), typeof(TDestination));
    
    if (_maps.TryGetValue(key, out var _delegate) && _delegate is Func<TSource, TDestination> func)
    {
      return func(source);
    }

    throw new InvalidOperationException($"No mapping registered: {typeof(TSource).Name} -> {typeof(TDestination).Name}");
  }
}

public static class MappingExtensions
{
  public static TDest MapTo<TDest, TSource>(this TSource source)
    => Mapping.Map<TSource, TDest>(source);
  
  // This is the clean one you want
  public static TDest MapTo<TDest>(this object source)
  {
    if (source is null) 
      throw new ArgumentNullException(nameof(source));

    // runtime dispatch to registered mapping based on actual source type
    var srcType = source.GetType();
    var key     = (srcType, typeof(TDest));

    // try exact type match first
    var field = typeof(Mapping).GetField("_maps", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!;
    var maps  = (Dictionary<(Type, Type), Delegate>)field.GetValue(null)!;

    if (maps.TryGetValue(key, out var del))
    {
      return (TDest)del.DynamicInvoke(source)!;
    }

    throw new InvalidOperationException($"No map registered: {srcType.Name} -> {typeof(TDest).Name}");
  }
  
  public static IEnumerable<TDest> MapToMany<TSource, TDest>(this IEnumerable<TSource> sources)
    => sources?.Select(s => s.MapTo<TDest, TSource>()) ?? Enumerable.Empty<TDest>();
  
  public static List<TDest> MapToList<TDest, TSource>(this IEnumerable<TSource> sources)
    => sources?.Select(s => s.MapTo<TDest, TSource>()).ToList() ?? new List<TDest>();
  
  // clean collection call: users.MapToList<UserDTO>()
  public static List<TDest> MapToList<TDest>(this IEnumerable<object> sources)
    => sources?.Select(s => s.MapTo<TDest>()).ToList() ?? new List<TDest>();
}

public sealed record UserDTO(Guid ID, string Name, string Email);

public sealed class User
{
  public Guid ID          { get; init; }
  public string FirstName { get; init; } = string.Empty;
  public string LastName  { get; init; } = string.Empty;
  public string Email     { get; init; } = string.Empty;
}
