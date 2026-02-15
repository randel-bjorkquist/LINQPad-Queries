<Query Kind="Statements">
  <Namespace>System.Collections.Concurrent</Namespace>
  <Namespace>System.Numerics</Namespace>
</Query>

//-------------------------------------------------------------------------------------------------
#load "TypedEnums\TypedEnum"

// Interface ITypedFlag ---------------------------------------------------------------------------
/// <summary>
/// Interface for type-safe flag enumerations that support bitwise operations.
/// Extends ITypedEnum to add flag-specific operations and bitwise operator contracts.
/// </summary>
/// <typeparam name="TSelf">The implementing type (CRTP pattern)</typeparam>
/// <typeparam name="Tid">The ID type (must support bitwise operations)</typeparam>
/// <remarks>
/// <para>
/// ITypedFlag defines the contract for flag enumerations that can be combined using
/// bitwise operations while maintaining type safety and rich metadata.
/// </para>
/// <para>
/// Implementations should follow these conventions:
/// <list type="bullet">
///   <item>Use power-of-2 IDs (1, 2, 4, 8, 16, ...) for single flags</item>
///   <item>Define a 'None' flag with ID = 0</item>
///   <item>Define an 'All' flag with ID = OR of all other flags</item>
///   <item>Cache combined instances for reference equality</item>
/// </list>
/// </para>
/// </remarks>
public interface ITypedFlag<TSelf, Tid> : ITypedEnum<TSelf, Tid>
  where TSelf : ITypedFlag<TSelf, Tid>
  where Tid   : notnull, IEquatable<Tid>, IComparable<Tid>, IBitwiseOperators<Tid, Tid, Tid>
{
  #region Flag Checking Methods

  /// <summary>
  /// Determines whether the specified flag is set in this instance.
  /// </summary>
  /// <param name="flag">The flag to check for</param>
  /// <returns>True if the flag is set; otherwise, false</returns>
  /// <exception cref="ArgumentNullException">Thrown when <paramref name="flag"/> is null</exception>
  /// <example>
  /// <code>
  /// var readWrite = Permissions.Read | Permissions.Write;
  /// bool canRead = readWrite.HasFlag(Permissions.Read);  // true
  /// bool canExecute = readWrite.HasFlag(Permissions.Execute);  // false
  /// </code>
  /// </example>
  bool HasFlag(TSelf flag);

  /// <summary>
  /// Determines whether any of the specified flags are set in this instance.
  /// </summary>
  /// <param name="flags">The flags to check for</param>
  /// <returns>True if at least one of the specified flags is set; otherwise, false</returns>
  /// <exception cref="ArgumentException">Thrown when <paramref name="flags"/> is null or empty</exception>
  /// <example>
  /// <code>
  /// var readOnly = Permissions.Read;
  /// bool hasAny = readOnly.HasAnyFlag(Permissions.Read, Permissions.Write);  // true
  /// </code>
  /// </example>
  bool HasAnyFlag(params TSelf[] flags);

  /// <summary>
  /// Determines whether all of the specified flags are set in this instance.
  /// </summary>
  /// <param name="flags">The flags to check for</param>
  /// <returns>True if all of the specified flags are set; otherwise, false</returns>
  /// <exception cref="ArgumentException">Thrown when <paramref name="flags"/> is null or empty</exception>
  /// <example>
  /// <code>
  /// var readWrite = Permissions.Read | Permissions.Write;
  /// bool hasAll = readWrite.HasAllFlags(Permissions.Read, Permissions.Write);  // true
  /// bool hasAll2 = readWrite.HasAllFlags(Permissions.Read, Permissions.Execute);  // false
  /// </code>
  /// </example>
  bool HasAllFlags(params TSelf[] flags);

  #endregion

  #region Decomposition

  /// <summary>
  /// Decomposes this flag combination into its constituent individual flags.
  /// </summary>
  /// <returns>A read-only list of individual flags that are set in this combination</returns>
  /// <remarks>
  /// This method returns only the single flags (power-of-2 IDs) that are set.
  /// Combined flags like 'All' are decomposed into their individual components.
  /// </remarks>
  /// <example>
  /// <code>
  /// var combined = Permissions.Read | Permissions.Write | Permissions.Execute;
  /// var flags = combined.ToFlags();
  /// // Result: [Permissions.Read, Permissions.Write, Permissions.Execute]
  /// </code>
  /// </example>
  IReadOnlyList<TSelf> ToFlags();

  #endregion

  #region Helper Methods

  /// <summary>
  /// Determines whether this instance represents a single flag (power-of-2 ID) or a combination.
  /// </summary>
  /// <returns>True if this is a single flag; false if it's a combination of multiple flags</returns>
  /// <remarks>
  /// A flag is considered "single" if its ID is a power of 2 (1, 2, 4, 8, ...) or zero (None).
  /// </remarks>
  /// <example>
  /// <code>
  /// Permissions.Read.IsSingleFlag();  // true (ID = 1)
  /// (Permissions.Read | Permissions.Write).IsSingleFlag();  // false (ID = 3)
  /// Permissions.None.IsSingleFlag();  // true (ID = 0, special case)
  /// </code>
  /// </example>
  bool IsSingleFlag();

  #endregion

  #region COMMENTED OUT: Static Abstract Operators
  /*

  /// <summary>
  /// Combines two flags using bitwise OR operation.
  /// </summary>
  /// <param name="left">The left operand</param>
  /// <param name="right">The right operand</param>
  /// <returns>A new flag instance with bits from both operands set</returns>
  /// <exception cref="ArgumentNullException">Thrown when either operand is null</exception>
  /// <example>
  /// <code>
  /// var readWrite = Permissions.Read | Permissions.Write;
  /// // Result: Flag with ID = 3 (Read and Write both set)
  /// </code>
  /// </example>
  static abstract TSelf operator |(TSelf left, TSelf right);

  /// <summary>
  /// Finds the intersection of two flags using bitwise AND operation.
  /// </summary>
  /// <param name="left">The left operand</param>
  /// <param name="right">The right operand</param>
  /// <returns>A new flag instance with only bits set in both operands</returns>
  /// <exception cref="ArgumentNullException">Thrown when either operand is null</exception>
  /// <example>
  /// <code>
  /// var common = (Permissions.Read | Permissions.Write) &amp; Permissions.Read;
  /// // Result: Permissions.Read (only Read is common to both)
  /// </code>
  /// </example>
  static abstract TSelf operator &(TSelf left, TSelf right);

  /// <summary>
  /// Finds the symmetric difference of two flags using bitwise XOR operation.
  /// </summary>
  /// <param name="left">The left operand</param>
  /// <param name="right">The right operand</param>
  /// <returns>A new flag instance with bits set in either operand but not both</returns>
  /// <exception cref="ArgumentNullException">Thrown when either operand is null</exception>
  /// <example>
  /// <code>
  /// var diff = (Permissions.Read | Permissions.Write) ^ Permissions.Read;
  /// // Result: Permissions.Write (Read is in both, so it's excluded)
  /// </code>
  /// </example>
  static abstract TSelf operator ^(TSelf left, TSelf right);

  /// <summary>
  /// Inverts all bits of a flag using bitwise NOT operation.
  /// </summary>
  /// <param name="flag">The flag to invert</param>
  /// <returns>A new flag instance with all bits inverted</returns>
  /// <exception cref="ArgumentNullException">Thrown when <paramref name="flag"/> is null</exception>
  /// <remarks>
  /// <para>
  /// <strong>Best Practice:</strong> Use with an 'All' flag for logical NOT:
  /// </para>
  /// <code>
  /// var notRead = Permissions.All &amp; ~Permissions.Read;
  /// </code>
  /// <para>
  /// Without 'All', this returns raw bitwise inversion of all bits in the underlying type,
  /// which may include bits beyond your defined flags.
  /// </para>
  /// </remarks>
  /// <example>
  /// <code>
  /// // Logical NOT - everything except Read
  /// var notRead = Permissions.All &amp; ~Permissions.Read;
  /// 
  /// // Raw bitwise NOT - includes all undefined bits
  /// var rawNot = ~Permissions.Read;  // Usually not what you want
  /// </code>
  /// </example>
  static abstract TSelf operator ~(TSelf flag);
  
  */
  #endregion
}

// Base TypedFlag class ---------------------------------------------------------------------------
/// <summary>
/// Base class for type-safe flag enumerations that support bitwise operations.
/// Extends TypedEnum to provide [Flags]-like behavior with strong typing.
/// </summary>
/// <typeparam name="TSelf">The derived type (CRTP pattern)</typeparam>
/// <typeparam name="TId">The ID type (must support bitwise operations)</typeparam>
/// <remarks>
/// <para>
/// TypedFlags work like [Flags] enums but with type safety and rich metadata.
/// Each flag should have a power-of-2 ID (1, 2, 4, 8, 16, etc.).
/// </para>
/// <para>
/// <strong>Best Practices:</strong>
/// <list type="bullet">
///   <item>Define a 'None' flag with ID = 0 for "no flags set"</item>
///   <item>Define an 'All' flag with ID = sum of all other flags (e.g., 1|2|4 = 7)</item>
///   <item>Use power-of-2 IDs only (validated at runtime)</item>
///   <item>Use 'All &amp; ~Flag' pattern for logical NOT operations</item>
/// </list>
/// </para>
/// <example>
/// <code>
/// public class Permissions : TypedFlagInt&lt;Permissions&gt;
/// {
///   public static readonly Permissions None    = new(0, "None", nameof(None));
///   public static readonly Permissions Read    = new(1, "Read", nameof(Read));
///   public static readonly Permissions Write   = new(2, "Write", nameof(Write));
///   public static readonly Permissions Execute = new(4, "Execute", nameof(Execute));
///   public static readonly Permissions All     = new(7, "All", nameof(All));
///   
///   private Permissions(int id, string description, string code)
///     : base(id, description, code) { }
/// }
/// 
/// // Usage:
/// var readWrite = Permissions.Read | Permissions.Write;
/// if (readWrite.HasFlag(Permissions.Read)) { /* ... */ }
/// var flags = readWrite.ToFlags(); // [Read, Write]
/// </code>
/// </example>
/// </remarks>
public abstract class TypedFlag<TSelf, Tid> : TypedEnum<TSelf, Tid>, ITypedFlag<TSelf, Tid>
  where TSelf : TypedFlag<TSelf, Tid>
  where Tid   : notnull, IEquatable<Tid>, IComparable<Tid>, IBitwiseOperators<Tid, Tid, Tid>
{
  /// <summary>
  /// Cache for dynamically created combined flag instances.
  /// Ensures reference equality for same combinations: (A | B) == (A | B)
  /// </summary>
  private static readonly ConcurrentDictionary<Tid, TSelf> _combinedCache = new();

  /// <summary>
  /// Initializes a new instance of a typed flag.
  /// </summary>
  /// <param name="id">The unique identifier</param>
  /// <param name="description">Human-readable description</param>
  /// <param name="code">Code/field name (use nameof)</param>
  protected TypedFlag(Tid id, string description, string code)
    : base(id, description, code) { }

  /// <summary>
  /// Gets or creates a combined flag instance from cache.
  /// Ensures that (A | B) returns the same reference every time.
  /// </summary>
  /// <param name="combinedId">The combined ID value</param>
  /// <returns>Cached or new instance</returns>
  protected static TSelf GetOrCreateCombined(Tid combinedId)
  {
    return _combinedCache.GetOrAdd(combinedId, id =>
    {
      // Create instance dynamically - use reflection to call protected constructor
      var type = typeof(TSelf);
      var ctor = type.GetConstructor( BindingFlags.NonPublic | BindingFlags.Instance
                                     ,null
                                     ,new[] { typeof(Tid), typeof(string), typeof(string) }
                                     ,null );

      if(ctor == null)
      {
        throw new InvalidOperationException( $"TypedFlag {type.Name} must have a protected constructor: "
                                           + $"{type.Name}(TId id, string description, string code)" );
      }

      // Generate description and code for combined flag
      var individualFlags = GetIndividualFlagsFromId(id);
      
      var description = individualFlags.Any()
                      ? string.Join(", ", individualFlags.Select(f => f.Description))
                      : "Combined";
                      
      var code = individualFlags.Any()
               ? string.Join(" | ", individualFlags.Select(f => f.Code))
               : "Combined";
      
      return (TSelf)ctor.Invoke(new object[] { id, description, code });
    });
  }
  
  public new static TSelf GetByID(Tid id)
  {
    try
    {
      return TypedEnum<TSelf, Tid>.GetByID(id); 
    }
    catch(ArgumentException)
    {
      return GetOrCreateCombined(id);
    }
  }
  
  /// <summary>
  /// Decomposes an ID into individual flag instances.
  /// Helper for GetOrCreateCombined to generate descriptions.
  /// </summary>
  private static List<TSelf> GetIndividualFlagsFromId(Tid combinedId)
  {
    var result = new List<TSelf>();

    foreach(var flag in TypedEnum<TSelf, Tid>.GetAll())
    {
      var typedFlag = flag as TypedFlag<TSelf, Tid>;
      
      if(typedFlag != null        && 
         typedFlag.IsSingleFlag() && 
        !typedFlag.ID.Equals(default(Tid))) // Excludes "None" (ID == 0)
      {
        // Check if this flag's bit is set in combinedId
        var andResult = combinedId & typedFlag.ID;
        
        if(!andResult.Equals(default(Tid)))
        {
          result.Add(flag);
        }
      }
    }

    return result;
  }

  /// <summary>
  /// Checks if this instance represents a single flag (power-of-2 ID).
  /// </summary>
  /// <returns>True if single flag, false if combination</returns>
  public bool IsSingleFlag()
  {
    if(ID is int intId)
      return intId == 0 || (intId & (intId - 1)) == 0;
    
    if(ID is short shortId)
      return shortId == 0 || (shortId & (shortId - 1)) == 0;
    
    if(ID is long longId)
      return longId == 0 || (longId & (longId - 1)) == 0;
    
    if(ID is Int128 int128Id)
      return int128Id == 0 || (int128Id & (int128Id - 1)) == 0;

    return false; // Unknown type, assume not single
  }

  #region Bitwise Operators

  /// <summary>
  /// Bitwise OR operator - combines two flags.
  /// </summary>
  /// <param name="left">First flag</param>
  /// <param name="right">Second flag</param>
  /// <returns>Combined flag with both bits set</returns>
  /// <example>
  /// <code>
  /// var readWrite = Permissions.Read | Permissions.Write;
  /// </code>
  /// </example>
  public static TSelf operator |(TypedFlag<TSelf, Tid> left, TypedFlag<TSelf, Tid> right)
  {
    if(left  == null) 
      throw new ArgumentNullException(nameof(left));
      
    if(right == null) 
      throw new ArgumentNullException(nameof(right));

    var combinedId = left.ID | right.ID;
    return GetOrCreateCombined(combinedId);
  }

  /// <summary>
  /// Bitwise AND operator - finds common flags.
  /// </summary>
  /// <param name="left">First flag</param>
  /// <param name="right">Second flag</param>
  /// <returns>Flag with only bits set in both operands</returns>
  /// <example>
  /// <code>
  /// var common = (Permissions.Read | Permissions.Write) &amp; Permissions.Read;
  /// // Result: Permissions.Read
  /// </code>
  /// </example>
  public static TSelf operator &(TypedFlag<TSelf, Tid> left, TypedFlag<TSelf, Tid> right)
  {
    if(left  == null) 
      throw new ArgumentNullException(nameof(left));
      
    if(right == null) 
      throw new ArgumentNullException(nameof(right));

    var combinedId = left.ID & right.ID;
    return GetOrCreateCombined(combinedId);
  }

  /// <summary>
  /// Bitwise XOR operator - symmetric difference.
  /// </summary>
  /// <param name="left">First flag</param>
  /// <param name="right">Second flag</param>
  /// <returns>Flag with bits set in either operand but not both</returns>
  /// <example>
  /// <code>
  /// var diff = (Permissions.Read | Permissions.Write) ^ Permissions.Read;
  /// // Result: Permissions.Write
  /// </code>
  /// </example>
  public static TSelf operator ^(TypedFlag<TSelf, Tid> left, TypedFlag<TSelf, Tid> right)
  {
    if(left  == null) 
      throw new ArgumentNullException(nameof(left));
      
    if(right == null) 
      throw new ArgumentNullException(nameof(right));

    var combinedId = left.ID ^ right.ID;
    return GetOrCreateCombined(combinedId);
  }

  /// <summary>
  /// Bitwise NOT operator - inverts all bits.
  /// </summary>
  /// <param name="flag">Flag to invert</param>
  /// <returns>Flag with all bits inverted</returns>
  /// <remarks>
  /// <para>
  /// <strong>Best Practice:</strong> Use with an 'All' flag for logical NOT:
  /// </para>
  /// <code>
  /// var notRead = Permissions.All &amp; ~Permissions.Read;
  /// </code>
  /// <para>
  /// Without 'All', this returns raw bitwise inversion of all bits in the underlying type,
  /// which may include bits beyond your defined flags.
  /// </para>
  /// </remarks>
  public static TSelf operator ~(TypedFlag<TSelf, Tid> flag)
  {
    if(flag == null) 
      throw new ArgumentNullException(nameof(flag));

    var invertedId = ~flag.ID;
    return GetOrCreateCombined(invertedId);
  }

  #endregion

  #region Flag Checking Methods

  /// <summary>
  /// Checks if this instance has the specified flag set.
  /// </summary>
  /// <param name="flag">The flag to check for</param>
  /// <returns>True if the flag is set, false otherwise</returns>
  /// <example>
  /// <code>
  /// var readWrite = Permissions.Read | Permissions.Write;
  /// bool canRead = readWrite.HasFlag(Permissions.Read);  // true
  /// bool canExecute = readWrite.HasFlag(Permissions.Execute);  // false
  /// </code>
  /// </example>
  public bool HasFlag(TSelf flag)
  {
    if(flag == null) 
      throw new ArgumentNullException(nameof(flag));

    var andResult = ID & flag.ID;
    return andResult.Equals(flag.ID);
  }

  /// <summary>
  /// Checks if this instance has any of the specified flags set.
  /// </summary>
  /// <param name="flags">Flags to check for</param>
  /// <returns>True if at least one flag is set, false otherwise</returns>
  /// <example>
  /// <code>
  /// var readOnly = Permissions.Read;
  /// bool hasAny = readOnly.HasAnyFlag(Permissions.Read, Permissions.Write);  // true
  /// </code>
  /// </example>
  public bool HasAnyFlag(params TSelf[] flags)
  {
    if(flags == null || flags.Length == 0)
      throw new ArgumentException("At least one flag must be provided", nameof(flags));

    return flags.Any(HasFlag);
  }

  /// <summary>
  /// Checks if this instance has all of the specified flags set.
  /// </summary>
  /// <param name="flags">Flags to check for</param>
  /// <returns>True if all flags are set, false otherwise</returns>
  /// <example>
  /// <code>
  /// var readWrite = Permissions.Read | Permissions.Write;
  /// bool hasAll = readWrite.HasAllFlags(Permissions.Read, Permissions.Write);  // true
  /// bool hasAll2 = readWrite.HasAllFlags(Permissions.Read, Permissions.Execute);  // false
  /// </code>
  /// </example>
  public bool HasAllFlags(params TSelf[] flags)
  {
    if(flags == null || flags.Length == 0)
      throw new ArgumentException("At least one flag must be provided", nameof(flags));

    return flags.All(HasFlag);
  }

  #endregion

  #region Decomposition

  /// <summary>
  /// Decomposes this flag combination into individual constituent flags.
  /// </summary>
  /// <returns>List of individual flags that are set in this combination</returns>
  /// <example>
  /// <code>
  /// var combined = Permissions.Read | Permissions.Write | Permissions.Execute;
  /// var flags = combined.ToFlags();
  /// // Result: [Permissions.Read, Permissions.Write, Permissions.Execute]
  /// </code>
  /// </example>
  public IReadOnlyList<TSelf> ToFlags()
  {
    var result = new List<TSelf>();

    foreach(var flag in TypedEnum<TSelf, Tid>.GetAll())
    {
      var typedFlag = flag as TypedFlag<TSelf, Tid>;
      
      if(typedFlag != null                 && 
         typedFlag.IsSingleFlag()          && 
        !typedFlag.ID.Equals(default(Tid)) && // Excluded "None" (ID == 0)
         HasFlag(flag))
      {
        result.Add(flag);
      }
    }

    return result.AsReadOnly();
  }

  #endregion

  #region ToString Override

  /// <summary>
  /// Returns a string representation of this flag.
  /// For combined flags, returns comma-separated list of constituent flags.
  /// </summary>
  /// <returns>String representation</returns>
  /// <example>
  /// Single flag: "Read (1)"
  /// Combined: "Read, Write, Execute"
  /// </example>
  public override string ToString()
  {
    if(Code.Contains('|') || Code.Equals("Combined", StringComparison.OrdinalIgnoreCase))
    {
      var flags = ToFlags();
      
      return flags.Any() 
               ? string.Join(", ", flags.Select(f => f.Code)) 
               : base.ToString(); // Fallback for weird edge cases
    }
    
    // Static field (include: "All", "None", etc.) - show as-is
    return base.ToString();
  }

  #endregion

  #region AsJsonObject Override

  public override object AsJsonObject()
  {
    if(IsSingleFlag())
    {
      return base.AsJsonObject();
    }
    
    var flags = ToFlags();
    
    return flags.Any() 
             ? new { id          = ID
                    ,description = Description
                    ,code        = Code
                    ,flags       = flags }
             : base.AsJsonObject(); // Fallback for weird edge cases
  }
  
  #endregion
}

// Middle-tier classes ----------------------------------------------------------------------------
// short/Int16 --------------------------------------------------------------------------
/// <summary>
/// Alias for TypedFlagInt16. Type-safe flag enumeration with 16-bit integer IDs.
/// </summary>
/// <typeparam name="TSelf">The derived type (CRTP pattern)</typeparam>
public abstract class TypedFlagShort<TSelf> : TypedFlagInt16<TSelf>
  where TSelf : TypedFlagShort<TSelf>
{
  /// <summary>
  /// Initializes a new short-based typed flag.
  /// </summary>
  /// <param name="id">Unique identifier (should be power-of-2)</param>
  /// <param name="description">Human-readable description</param>
  /// <param name="code">Code/field name (use nameof)</param>
  protected TypedFlagShort(short id, string description, string code)
    : base(id, description, code) { }
}

/// <summary>
/// Type-safe flag enumeration with 16-bit integer IDs.
/// Supports flags from 1 to 32,768 (2^15).
/// </summary>
/// <typeparam name="TSelf">The derived type (CRTP pattern)</typeparam>
/// <example>
/// <code>
/// public class CompactPermissions : TypedFlagInt16&lt;CompactPermissions&gt;
/// {
///   public static readonly CompactPermissions None = new(0, "None", nameof(None));
///   public static readonly CompactPermissions Read = new(1, "Read", nameof(Read));
///   public static readonly CompactPermissions Write = new(2, "Write", nameof(Write));
///   
///   private CompactPermissions(short id, string description, string code)
///     : base(id, description, code) { }
/// }
/// </code>
/// </example>
public abstract class TypedFlagInt16<TSelf> : TypedFlag<TSelf, Int16>
  where TSelf : TypedFlagInt16<TSelf>
{
  /// <summary>
  /// Initializes a new Int16-based typed flag.
  /// </summary>
  /// <param name="id">Unique identifier (should be power-of-2)</param>
  /// <param name="description">Human-readable description</param>
  /// <param name="code">Code/field name (use nameof)</param>
  protected TypedFlagInt16(Int16 id, string description, string code)
    : base(id, description, code) { }
  
  #region 'explicit/implicit' operators ...
  
  // short/Int16 -> TypedFlag (explicit - forces developer to think)
  [Obsolete("Direct casting from int is discouraged. Use GetById(int) for clarity.", false)]
  public static explicit operator TypedFlagInt16<TSelf>(Int16 id)
    => GetByID(id);

  // TypedFlag -> short/Int16 (implicit - commonly needed)
  [Obsolete("Prefer TypedFlag.Field.ID for clarity.", false)]
  public static implicit operator Int16(TypedFlagInt16<TSelf> flag)
    => flag is null ? throw new ArgumentNullException(nameof(flag)) : flag.ID;

  // TypedFlag -> string (implicit - for Description)
  [Obsolete("Prefer TypedFlag.Field.Description for clarity.", false)]
  public static implicit operator string(TypedFlagInt16<TSelf> flag)
    => flag is null ? throw new ArgumentNullException(nameof(flag)) : flag.Description;    
    
  #endregion
}

// int/Int32 ----------------------------------------------------------------------------
/// <summary>
/// Alias for TypedFlagInt32. Type-safe flag enumeration with 32-bit integer IDs.
/// This is the most commonly used TypedFlag variant.
/// </summary>
/// <typeparam name="TSelf">The derived type (CRTP pattern)</typeparam>
public abstract class TypedFlagInt<TSelf> : TypedFlagInt32<TSelf>
  where TSelf : TypedFlagInt<TSelf>
{
  /// <summary>
  /// Initializes a new int-based typed flag.
  /// </summary>
  /// <param name="id">Unique identifier (should be power-of-2)</param>
  /// <param name="description">Human-readable description</param>
  /// <param name="code">Code/field name (use nameof)</param>
  protected TypedFlagInt(int id, string description, string code)
    : base(id, description, code) { }
}

/// <summary>
/// Type-safe flag enumeration with 32-bit integer IDs.
/// Supports flags from 1 to 2,147,483,648 (2^31).
/// This is the recommended default for most use cases.
/// </summary>
/// <typeparam name="TSelf">The derived type (CRTP pattern)</typeparam>
/// <example>
/// <code>
/// public class Permissions : TypedFlagInt32&lt;Permissions&gt;
/// {
///   public static readonly Permissions None    = new(0, "None", nameof(None));
///   public static readonly Permissions Read    = new(1, "Read", nameof(Read));
///   public static readonly Permissions Write   = new(2, "Write", nameof(Write));
///   public static readonly Permissions Execute = new(4, "Execute", nameof(Execute));
///   public static readonly Permissions All     = new(7, "All", nameof(All));
///   
///   private Permissions(int id, string description, string code)
///     : base(id, description, code) { }
/// }
/// 
/// // Usage:
/// var readWrite = Permissions.Read | Permissions.Write;
/// if (readWrite.HasFlag(Permissions.Read))
/// {
///   Console.WriteLine("Can read!");
/// }
/// </code>
/// </example>
public abstract class TypedFlagInt32<TSelf> : TypedFlag<TSelf, Int32>
  where TSelf : TypedFlagInt32<TSelf>
{
  /// <summary>
  /// Initializes a new Int32-based typed flag.
  /// </summary>
  /// <param name="id">Unique identifier (should be power-of-2)</param>
  /// <param name="description">Human-readable description</param>
  /// <param name="code">Code/field name (use nameof)</param>
  protected TypedFlagInt32(Int32 id, string description, string code)
    : base(id, description, code) { }

  #region 'explicit/implicit' operators ...

  // int/Int32 -> TypedFlag (explicit - forces developer to think)
  [Obsolete("Direct casting from int is discouraged. Use GetById(int) for clarity.", false)]
  public static explicit operator TypedFlagInt32<TSelf>(Int32 id)
    => GetByID(id);

  // TypedFlag -> int/Int32 (implicit - commonly needed)
  [Obsolete("Prefer TypedFlag.Field.ID for clarity.", false)]
  public static implicit operator Int32(TypedFlagInt32<TSelf> flag)
    => flag is null ? throw new ArgumentNullException(nameof(flag)) : flag.ID;

  // TypedFlag -> string (implicit - for Description)
  [Obsolete("Prefer TypedFlag.Field.Description for clarity.", false)]
  public static implicit operator string(TypedFlagInt32<TSelf> flag)
    => flag is null ? throw new ArgumentNullException(nameof(flag)) : flag.Description;    
    
  #endregion
}

// long/Int64 ---------------------------------------------------------------------------
/// <summary>
/// Alias for TypedFlagInt64. Type-safe flag enumeration with 64-bit integer IDs.
/// </summary>
/// <typeparam name="TSelf">The derived type (CRTP pattern)</typeparam>
public abstract class TypedFlagLong<TSelf> : TypedFlagInt64<TSelf>
  where TSelf : TypedFlagLong<TSelf>
{
  /// <summary>
  /// Initializes a new long-based typed flag.
  /// </summary>
  /// <param name="id">Unique identifier (should be power-of-2)</param>
  /// <param name="description">Human-readable description</param>
  /// <param name="code">Code/field name (use nameof)</param>
  protected TypedFlagLong(long id, string description, string code)
    : base(id, description, code) { }
}

/// <summary>
/// Type-safe flag enumeration with 64-bit integer IDs.
/// Supports flags from 1 to 9,223,372,036,854,775,808 (2^63).
/// Use when you need more than 32 distinct flags.
/// </summary>
/// <typeparam name="TSelf">The derived type (CRTP pattern)</typeparam>
/// <example>
/// <code>
/// public class ExtendedPermissions : TypedFlagInt64&lt;ExtendedPermissions&gt;
/// {
///   public static readonly ExtendedPermissions None = new(0L, "None", nameof(None));
///   public static readonly ExtendedPermissions Read = new(1L, "Read", nameof(Read));
///   // ... up to 64 distinct flags
///   
///   private ExtendedPermissions(long id, string description, string code)
///     : base(id, description, code) { }
/// }
/// </code>
/// </example>
public abstract class TypedFlagInt64<TSelf> : TypedFlag<TSelf, Int64>
  where TSelf : TypedFlagInt64<TSelf>
{
  /// <summary>
  /// Initializes a new Int64-based typed flag.
  /// </summary>
  /// <param name="id">Unique identifier (should be power-of-2)</param>
  /// <param name="description">Human-readable description</param>
  /// <param name="code">Code/field name (use nameof)</param>
  protected TypedFlagInt64(Int64 id, string description, string code)
    : base(id, description, code) { }

  #region 'explicit/implicit' operators ...

  // long/Int64 -> TypedFlag (explicit - forces developer to think)
  [Obsolete("Direct casting from int is discouraged. Use GetById(int) for clarity.", false)]
  public static explicit operator TypedFlagInt64<TSelf>(Int64 id)
    => GetByID(id);

  // TypedFlag -> long/Int64 (implicit - commonly needed)
  [Obsolete("Prefer TypedFlag.Field.ID for clarity.", false)]
  public static implicit operator Int64(TypedFlagInt64<TSelf> flag)
    => flag is null ? throw new ArgumentNullException(nameof(flag)) : flag.ID;

  // TypedFlag -> string (implicit - for Description)
  [Obsolete("Prefer TypedFlag.Field.Description for clarity.", false)]
  public static implicit operator string(TypedFlagInt64<TSelf> flag)
    => flag is null ? throw new ArgumentNullException(nameof(flag)) : flag.Description;

  #endregion
}

// Int128 -------------------------------------------------------------------------------
/// <summary>
/// Type-safe flag enumeration with 128-bit integer IDs.
/// Supports flags from 1 to 2^127.
/// Use when you need more than 64 distinct flags (rare).
/// </summary>
/// <typeparam name="TSelf">The derived type (CRTP pattern)</typeparam>
/// <example>
/// <code>
/// public class MassivePermissions : TypedFlagInt128&lt;MassivePermissions&gt;
/// {
///   public static readonly MassivePermissions None = new((Int128)0, "None", nameof(None));
///   public static readonly MassivePermissions Read = new((Int128)1, "Read", nameof(Read));
///   // ... up to 128 distinct flags
///   
///   private MassivePermissions(Int128 id, string description, string code)
///     : base(id, description, code) { }
/// }
/// </code>
/// </example>
public abstract class TypedFlagInt128<TSelf> : TypedFlag<TSelf, Int128>
  where TSelf : TypedFlagInt128<TSelf>
{
  /// <summary>
  /// Initializes a new Int128-based typed flag.
  /// </summary>
  /// <param name="id">Unique identifier (should be power-of-2)</param>
  /// <param name="description">Human-readable description</param>
  /// <param name="code">Code/field name (use nameof)</param>
  protected TypedFlagInt128(Int128 id, string description, string code)
    : base(id, description, code) { }

  #region 'explicit/implicit' operators ...

  // Int128 -> TypedFlag (explicit - forces developer to think)
  [Obsolete("Direct casting from int is discouraged. Use GetById(int) for clarity.", false)]
  public static explicit operator TypedFlagInt128<TSelf>(Int128 id)
    => GetByID(id);

  // TypedFlag -> Int128 (implicit - commonly needed)
  [Obsolete("Prefer TypedFlag.Field.ID for clarity.", false)]
  public static implicit operator Int128(TypedFlagInt128<TSelf> flag)
    => flag is null ? throw new ArgumentNullException(nameof(flag)) : flag.ID;

  // TypedFlag -> string (implicit - for Description)
  [Obsolete("Prefer TypedFlag.Field.Description for clarity.", false)]
  public static implicit operator string(TypedFlagInt128<TSelf> flag)
    => flag is null ? throw new ArgumentNullException(nameof(flag)) : flag.Description;    
    
  #endregion
}