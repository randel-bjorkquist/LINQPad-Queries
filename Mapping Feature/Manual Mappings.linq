<Query Kind="Statements" />

#load "Mapping Feature\Mapping Registry"
#load "Mapping Feature\Types"

// <Query Kind="Statements" />

// Manual registration of known mappings (called at startup)
public static class ManualMappings
{
  public static void RegisterAll()
  {
    MappingRegistry.Register<User, UserDTO>(user => new UserDTO(user.ID, $"{user.FirstName} {user.LastName}".Trim(), user.Email));

    MappingRegistry.Register<UserDTO, User>(dto => {
      var parts = (dto.Name ?? string.Empty).Split(' ', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

      return new User { ID        = dto.ID
                       ,FirstName = parts.Length > 0 ? parts[0] : string.Empty
                       ,LastName  = parts.Length > 1 ? parts[1] : string.Empty
                       ,Email     = dto.Email };
    });

    // Add more manual mappings here as needed
  }
}