<Query Kind="Statements" />

#load "Mapping Feature\Interfaces"
#load "Mapping Feature\Types"

public sealed class UserToUserDtoMapping : IMapFrom<User, UserDTO>
{
  public static UserDTO Map(User source)
  {
    if (source == null) 
      throw new ArgumentNullException(nameof(source));

    return new UserDTO( source.ID
                       ,$"{source.FirstName} {source.LastName}".Trim()
                       ,source.Email );
  }
}

public sealed class UserDtoToUserMapping : IMapFrom<UserDTO, User>
{
  public static User Map(UserDTO source)
  {
    if (source == null) 
      throw new ArgumentNullException(nameof(source));

    var parts = (source.Name ?? string.Empty) .Split(' ', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

    return new User { ID        = source.ID
                     ,FirstName = parts.Length > 0 ? parts[0] : string.Empty
                     ,LastName  = parts.Length > 1 ? parts[1] : string.Empty
                     ,Email     = source.Email };
  }
}