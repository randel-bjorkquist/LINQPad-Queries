<Query Kind="Program" />

void Main()
{
  var user_fred_flintstone = new User { ID        = Guid.NewGuid()
                                       ,FirstName = "Fred"
                                       ,LastName  = "Flintstone"
                                       ,Email     = "fred.flintstone@bedrock.bc" };
  
  // Model -> DTO
  UserDTO dto_fred_flintstone = user_fred_flintstone.MapTo<UserDTO, User>();
  dto_fred_flintstone.Dump("UserDTO dto_fred_flintstone = user_fred_flintstone.MapTo<UserDTO, User>();", 0);
  
  // DTO -> Model
  User fred_flintstone = dto_fred_flintstone.MapTo<User, UserDTO>();
  fred_flintstone.Dump("User fred_flintstone = dto_fred_flintstone.MapTo<User, UserDTO>();", 0);
  
  // Collection mapping
  var user_barney_rubble = new User { ID        = Guid.NewGuid()
                                     ,FirstName = "Barney"
                                     ,LastName  = "Rubble"
                                     ,Email     = "barney.rubble@bedrock.bc" };
  
  var users = new List<User> { user_fred_flintstone, user_barney_rubble };
  List<UserDTO> dtos = users.MapToList<User, UserDTO>();
  dtos.Dump("List<UserDTO> dtos = users.MapToList<User, UserDTO>();", 0);
}

public interface IMapFrom<TSource, TDestination> where TDestination : IMapFrom<TSource, TDestination>
{
  static abstract TDestination From(TSource source);
}

public static class MappingExtensions
{
  // Single Item Conversion
  public static TDestination MapTo<TDestination, TSource>(this TSource source)
    where TDestination : IMapFrom<TSource, TDestination> 
    => source is null ? throw new ArgumentNullException(nameof(source)) : TDestination.From(source);
    
    
  // Single item (type inference helper: source inferred from "this", you specify only destination)
  public static TDestination MapTo<TDestination>(this object source)
    => throw new NotSupportedException("Use MapTo<TDestination, TSource>(this TSource source) or Map collections with MapToEnumerable/MapToList.");
    
    
  // Collections
  public static IEnumerable<TDestination> MapToEnumerable<TSource, TDestination>(this IEnumerable<TSource> sources)
    where TDestination : IMapFrom<TSource, TDestination>
  {
    if(sources is null)
      return Enumerable.Empty<TDestination>();
      
    return sources.Select(TDestination.From);
  }
  
  public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> sources)
    where TDestination : IMapFrom<TSource, TDestination>
    => sources.MapToEnumerable<TSource, TDestination>().ToList();
}

public sealed class User : IMapFrom<UserDTO, User>
{
  public Guid ID          { get; init; }
  
  public string FirstName { get; init; } = default;
  public string LastName  { get; init; } = default;
  
  public string Email     { get; init; } = default;
  
  // DTO -> Model
  public static User From(UserDTO dto)
  {
    //naive split just for demo; use whatever rule needed here ...
    var parts = (dto.Name ?? string.Empty).Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

    return new User { ID        = dto.ID
                     ,FirstName = parts.ElementAtOrDefault(0) ?? string.Empty
                     ,LastName  = parts.ElementAtOrDefault(1) ?? string.Empty
                     ,Email     = dto.Email                   ?? string.Empty };
  }
}

public sealed record UserDTO( Guid ID, string Name, string Email) : IMapFrom<User, UserDTO>
{
  public static UserDTO From(User user)
    => new(user.ID, $"{user.FirstName} {user.LastName}", user.Email);
}

