<Query Kind="Program" />

#region Source - Summary - Additional Resources
/// <source>
///   Microsoft Learn ...
///   https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
/// </source>
/// <summary>
///   Enumerations (or enum types for short) are a thin language wrapper around an integral type. You might want to limit their use to when you 
///   are storing one value from a closed set of values. Classification based on sizes (small, medium, large) is a good example. Using enums for
///   control flow or more robust abstractions can be a code smell. This type of usage leads to fragile code with many control flow statements 
///   checking values of the enum.
/// </summary>
/// <additional resources>
///   Jimmy Bogard. Enumeration classes
///   https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/
///   
///   Steve Smith. Enum Alternatives in C#
///   https://ardalis.com/enum-alternatives-in-c
///   
///   Enumeration.cs.Base Enumeration class in eShopOnContainers
///   https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/Services/Ordering/Ordering.Domain/SeedWork/Enumeration.cs
///   
///   CardType.cs.Sample Enumeration class in eShopOnContainers.
///   https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/Services/Ordering/Ordering.Domain/AggregatesModel/BuyerAggregate/CardType.cs
///   
///   SmartEnum.Ardalis - Classes to help produce strongly typed smarter enums in .NET.
///   https://www.nuget.org/packages/Ardalis.SmartEnum/
/// </additional resources>
#endregion

void Main()
{
  // 1. Retrieve and display all CardType values
  Console.WriteLine("All Card Types:");
  
  var allCardTypes = CardType.GetAll<CardType>();

  foreach (var cardType in allCardTypes)
  {
    Console.WriteLine($"  ID: {cardType.ID}, Name: {cardType.Name}");
  }

  // 2. Demonstrate equality comparison
  Console.WriteLine();
  Console.WriteLine("Equality Check:");
  Console.WriteLine($"  CardType.Amex.Equals(CardType.Amex): {CardType.Amex.Equals(CardType.Amex)}");
  Console.WriteLine($"  CardType.Amex.Equals(CardType.Visa): {CardType.Amex.Equals(CardType.Visa)}");

  // 3. Demonstrate comparison using CompareTo
  Console.WriteLine();
  Console.WriteLine("Comparison Check:");
  
  Console.WriteLine($"  Amex vs Visa: {CardType.Amex.CompareTo(CardType.Visa)}");                         // Should be -1 (Amex.Id < Visa.Id)
  Console.WriteLine($"  Visa vs MasterCard: {CardType.Visa.CompareTo(CardType.MasterCard)}");             // Should be -1 (Visa.Id < MasterCard.Id)
  
  Console.WriteLine($"  MasterCard vs MasterCard: {CardType.MasterCard.CompareTo(CardType.MasterCard)}"); // Should be  0 (MasterCard.Id > MasterCard.Id)
  Console.WriteLine($"  Visa vs Visa: {CardType.Visa.CompareTo(CardType.Visa)}");                         // Should be  0 (same Id)
  
  Console.WriteLine($"  MasterCard vs Visa: {CardType.MasterCard.CompareTo(CardType.Visa)}");             // Should be  1 (MasterCard.Id > Visa.Id)
  Console.WriteLine($"  Visa vs Amex: {CardType.Visa.CompareTo(CardType.Amex)}");                         // Should be  1 (Visa.Id > Amex.Id)

  // 4. Demonstrate ToString
  Console.WriteLine();
  Console.WriteLine("ToString Check:");
  Console.WriteLine($"  CardType.Amex.ToString(): {CardType.Amex}");  
}

public abstract class Enumeration : IComparable
{
  public int ID       { get; private set; }
  public string Name  { get; private set; }

  protected Enumeration(int id, string name) => (ID, Name) = (id, name);

  public override string ToString() => Name;
  
  public static IEnumerable<T> GetAll<T>() where T : Enumeration
    => typeof(T).GetFields(BindingFlags.Public |
                           BindingFlags.Static |
                           BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();

  public override int GetHashCode() => HashCode.Combine(GetType(), ID);
                                                                             
  public override bool Equals(object? obj)
  {
    if(obj is not Enumeration otherValue)
    {
      return false;
    }
    
    var typeMatches   = GetType().Equals(obj.GetType());
    var valueMatches  = ID.Equals(otherValue.ID);
    
    return typeMatches && valueMatches;
  }

  public int CompareTo(object? other)
  {
    if(other is null)
    {
       return 1; //Non-Null is greater than null
    }
    
    if(other is not Enumeration otherValue)
    {
      throw new ArgumentException($"Object must be of type {nameof(Enumeration)}.");
    }
    
    return ID.CompareTo(otherValue.ID);
  }
}

public class CardType : Enumeration
{
  public static readonly CardType Amex = new(1, nameof(Amex));
  public static readonly CardType Visa = new(2, nameof(Visa));
  public static readonly CardType MasterCard = new(3, nameof(MasterCard));

  public CardType(int id, string name) 
    : base(id, name) { }
}

//OPTION 1 ...
public class EmployeeType : Enumeration
{
  public static readonly EmployeeType Manager = new(0, "Manager");
  public static readonly EmployeeType Servant = new(1, "Servant");
  public static readonly EmployeeType AssistantToTheRegionalManager = new(2, "AssistantToTheRegionalManager");

  public EmployeeType(int id, string name) : base(id, name) { }
}

//OPTION 2 ...
//public abstract class EmployeeType : Enumeration
//{
//  public static readonly EmployeeType Manager = new ManagerType();
//  public static readonly EmployeeType Servant = new ServantType();
//  public static readonly EmployeeType AssistantToTheRegionalManager = new AssistantToTheRegionalManagerType();
//
//  protected EmployeeType(int id, string name) : base(id, name) { }
//
//  public abstract decimal BonusSize { get; }
//
//  private class ManagerType : EmployeeType
//  {
//    public ManagerType() : base(0, "Manager") { }
//    public override decimal BonusSize => 1000m;
//  }
//
//  private class ServantType : EmployeeType
//  {
//    public ServantType() : base(1, "Servant") { }
//    public override decimal BonusSize => 0m;
//  }
//
//  private class AssistantToTheRegionalManagerType : EmployeeType
//  {
//    public AssistantToTheRegionalManagerType() : base(2, "AssistantToTheRegionalManager") { }
//    public override decimal BonusSize => 0m;
//  }
//}

//OPTION 3 ...
//Enum Alternatives in C# by Steve Smith
//Resource: https://ardalis.com/enum-alternatives-in-c/
public class Role 
{
  public static Role Author               { get; } = new Role(0, "Author");
  public static Role Editor               { get; } = new Role(1, "Editor");
  public static Role Administrator        { get; } = new Role(2, "Administrator");
  public static Role SalesRepresentative  { get; } = new Role(3, "Sales Representative");
  
  private Role(int id, string name)
  {
    ID = id;
    Name = name;
  }

  public int ID      { get; private set; }
  public string Name { get; private set; }
  
  public static IEnumerable<Role> List() 
    => new[] { Author, Editor, Administrator, SalesRepresentative };
  
  public static Role FromString(string name) 
    => List().Single(r => String.Equals(r.Name, name, StringComparison.OrdinalIgnoreCase));
    
  public static Role FromValue(int id)
    => List().Single(r => r.ID == id);
}

//OPTION 4 ...
//Listing Strongly Typed Enum Options in C# by Steve Smith
//Resource: https://ardalis.com/listing-strongly-typed-enum-options-in-c/
public class RoleType
{
  public static RoleType Author { get; } = new RoleType(0, "Author");
  public static RoleType Editor { get; } = new RoleType(1, "Editor");
  public static RoleType Administrator { get; } = new RoleType(2, "Administrator");
  public static RoleType SalesRepresentative { get; } = new RoleType(3, "Sales Representative");
  
  public static List<RoleType> AllRoleTypes
    => typeof(RoleType).GetProperties(BindingFlags.Public |
                                      BindingFlags.Static |
                                      BindingFlags.DeclaredOnly)
                       .Where(p => p.PropertyType == typeof(RoleType))
                       .Select(pi => (RoleType)pi.GetValue(null, null))
                       .OrderBy(p => p.Name)
                       .ToList();
  
  private RoleType(int id, string name)
  {
    ID = id;
    Name = name;
  }

  public int ID { get; private set; }
  public string Name { get; private set; }

  public static RoleType FromString(string name)
    => AllRoleTypes.Single(r => String.Equals(r.Name, name, StringComparison.OrdinalIgnoreCase));

  public static RoleType FromValue(int id)
    => AllRoleTypes.Single(r => r.ID == id);
}

public class JobTitle
{
  public static List<JobTitle> AllJobTitles { get; } = new List<JobTitle>();
  
  public static JobTitle Author { get; } = new JobTitle(0, "Author");
  public static JobTitle Editor { get; } = new JobTitle(1, "Editor");
  public static JobTitle Administrator { get; } = new JobTitle(2, "Administrator");
  public static JobTitle SalesRepresentative { get; } = new JobTitle(3, "Sales Representative");

  private JobTitle(int id, string name)
  {
    ID = id;
    Name = name;
    
    AllJobTitles.Add(this);
  }

  public int ID { get; private set; }
  public string Name { get; private set; }

  public static JobTitle FromString(string name)
    => AllJobTitles.Single(r => String.Equals(r.Name, name, StringComparison.OrdinalIgnoreCase));

  public static JobTitle FromValue(int id)
    => AllJobTitles.Single(r => r.ID == id);
}
