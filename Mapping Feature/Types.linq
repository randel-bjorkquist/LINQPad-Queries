<Query Kind="Statements" />

// <Query Kind="Statements" />

public sealed class User
{
  public Guid ID          { get; init; }
  public string FirstName { get; init; } = string.Empty;
  public string LastName  { get; init; } = string.Empty;
  public string Email     { get; init; } = string.Empty;

  public override string ToString() => $"{FirstName} {LastName} ({Email})";
}

public sealed record UserDTO(Guid ID, string Name, string Email)
{
  public override string ToString() => $"{Name} ({Email})";
}