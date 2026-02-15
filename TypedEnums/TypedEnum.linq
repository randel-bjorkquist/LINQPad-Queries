<Query Kind="Statements">
  <Namespace>System.Collections.Immutable</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>System.Text.Json</Namespace>
</Query>

// interface ----------------------------------------------------------------------------
public interface ITypedEnum<TSelf, Tid>
  where TSelf : ITypedEnum<TSelf, Tid>
  where Tid   : notnull, IEquatable<Tid>, IComparable<Tid>
{
  Tid ID              { get; }
  string Description  { get; }
  string Code         { get; }
  
  // static abstract methods (these can be abstract in interface)
  static abstract IReadOnlyList<TSelf> GetAll();
  static abstract TSelf GetByID(Tid id);
  
  // Instance methods (these can be abstract in interface)
  string ToString(string? format, IFormatProvider? provider = null);  
  object AsJsonObject();
  string AsJsonString(JsonSerializerOptions? options = null);
  
  // Equality & comparison (instance methods)
  bool Equals(TSelf? other);
}

// base ---------------------------------------------------------------------------------
// Base class – clean, no reflection, no static fields except constants
public abstract class TypedEnum<TSelf, Tid> : IFormattable, ITypedEnum<TSelf, Tid>
  where TSelf : TypedEnum<TSelf, Tid>
  where Tid   : notnull, IEquatable<Tid>, IComparable<Tid>
{
  public Tid ID             { get; }
  public string Description { get; }
  public string Code        { get; }
  
  protected TypedEnum(Tid id, string description, string code)
  {
    ID          = id          ?? throw new ArgumentNullException(nameof(id));
    Description = description ?? throw new ArgumentNullException(nameof(description));
    Code        = code        ?? throw new ArgumentNullException(nameof(code));
  }
  
  private static readonly Lazy<IReadOnlyDictionary<Tid, TSelf>> _instances = new(() => {
    var type   = typeof(TSelf);
    var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                     .Where(f => f.FieldType == type)
                     .Select(f => (TSelf)f.GetValue(null)!)
                     .ToList();
    
    var duplicates = fields.GroupBy(f => f.ID)
                           .Where(g => g.Count() > 1)
                           .SelectMany(g => g)
                           .ToList();
    
    if (duplicates.Any())
    {
      var separator = $"{Environment.NewLine}  - ";
      throw new InvalidOperationException($"Duplication IDs detected in {type.Name}: {separator}{string.Join(separator, duplicates)}");
    }
    
    return fields.ToDictionary(f => f.ID);
  });

  private static IReadOnlyDictionary<Tid, TSelf> Fields => _instances.Value;

  public static IReadOnlyList<TSelf> GetAll()
    => Fields.Values
             .OrderBy(v => v.ID)
             .ToList()
             .AsReadOnly();
  
  public static TSelf GetByID(Tid id)
    => Fields.TryGetValue(id, out TSelf value) 
        ? value 
        : throw new ArgumentOutOfRangeException(nameof(id), $"Unknown {typeof(TSelf).Name} id: {id}");
  
  /// <summary>
  /// Returns a formatted string representation of the current smart enum instance.
  /// </summary>
  /// <param name="format">
  /// The format specifier to use:
  /// <list type="bullet">
  ///   <item><c>"D"</c> or <c>null</c> → Description</item>
  ///   <item><c>"C"</c> → Code (the field name)</item>
  ///   <item><c>"I"</c> → ID as string</item>
  ///   <item><c>"F"</c> → Full verbose format: "ID, Description, Code"</item>
  ///   <item><c>"G"</c> → General short format: "ID, Code"</item>
  ///   <item><c>"f"</c> → alternate Full verbose format: "Code (ID): Description"</item>
  ///   <item><c>"g"</c> → alternate General short format: "Code (ID)"</item>
  ///   <item>Any other value → falls back to Description</item>
  /// </list>
  /// </param>
  /// <param name="formatProvider">
  /// An optional format provider for culture-specific formatting (currently ignored; reserved for future use).
  /// </param>
  /// <returns>A formatted string representation of the smart enum instance.</returns>
  /// <remarks>
  /// This method implements <see cref="IFormattable.ToString(string?, IFormatProvider?)"/> and supports
  /// flexible output in string interpolation, <see cref="string.Format(string, object[])"/>, and other formatting contexts.
  /// </remarks>
  /// <example>
  /// <code>
  /// var evt = EventType.InspectionSaved;
  /// Console.WriteLine(evt.ToString("F"));  // Output: "InspectionSaved (1): Inspection Saved"
  /// Console.WriteLine($"{evt:C}");         // Output: "InspectionSaved"
  /// </code>
  /// </example>
  public string ToString(string? format, IFormatProvider? provider = null)
  {
    return format switch
    {
      "D" => Description,                                           // Default: Description
      "C" => Code,                                                  // Code (field name)
      "I" => ID?.ToString() ?? string.Empty,                        // ID as string
      
      "F" => $"ID: {ID}, Description: {Description}, Code: {Code}", // Full verbose format
      "G" => $"ID: {ID}, Code: {Code}",                             // General / short + id
      
      "f" => $"{Code} ({ID}): {Description}",                       // alternative Full verbose format
      "g" => $"{Code} ({ID})",                                      // alternative General / short + id
        _ => Description                                            // fallbase
    };
  }
    
  /// <summary>
  /// This overrides 'object.ToString()' using the 'Full verbose format' as default
  /// </summary>
  public override string ToString()
    => ToString(format: "g", provider: null);

  /// <summary>
  /// Returns a structured JSON-ready object. Safe for APIs, logs, and serialaztion.
  /// </summary>
  public virtual object AsJsonObject()
    => new {id = ID, description = Description, code = Code};
  
  /// <summary>
  /// Returns a JSON string representation. This is a convenience wrapper over AsJsonObject().
  /// </summary>
  public string AsJsonString(JsonSerializerOptions? options = null)
    => JsonSerializer.Serialize(AsJsonObject(), options);
  
  #region enum-like equality: compare based on ID only (ignore Description for uniqueness)
  
  public override bool Equals(object? obj)
    => obj is TSelf other && Equals(other);
  
  public virtual bool Equals(TSelf? other)
    => other is not null && EqualityComparer<Tid>.Default.Equals(ID, other.ID);
  
  public override int GetHashCode()
    => EqualityComparer<Tid>.Default.GetHashCode(ID);
  
  public static bool operator == (TypedEnum<TSelf, Tid> left, TypedEnum<TSelf, Tid> right)
    => ReferenceEquals(left, right) || left is not null && right is not null && left.Equals((TSelf)right);
  
  public static bool operator != (TypedEnum<TSelf, Tid> left, TypedEnum<TSelf, Tid> right)
    => !(left == right);
  
  public static bool operator < (TypedEnum<TSelf, Tid> left, TypedEnum<TSelf, Tid> right)
    => left.ID.CompareTo(right.ID) < 0;
    
  public static bool operator > (TypedEnum<TSelf, Tid> left, TypedEnum<TSelf, Tid> right)
    => left.ID.CompareTo(right.ID) > 0;
    
  public static bool operator <= (TypedEnum<TSelf, Tid> left, TypedEnum<TSelf, Tid> right)
    => left.ID.CompareTo(right.ID) <= 0;
  
  public static bool operator >= (TypedEnum<TSelf, Tid> left, TypedEnum<TSelf, Tid> right)
    => left.ID.CompareTo(right.ID) >= 0;

  #endregion
}

// short/Int16 --------------------------------------------------------------------------
public abstract class TypedEnumShort<Tself> : TypedEnumInt16<Tself>
  where Tself : TypedEnumShort<Tself>
{
  protected TypedEnumShort(short id, string description, string code)
    : base(id, description, code) { }
}

public abstract class TypedEnumInt16<TSelf> : TypedEnum<TSelf, Int16>
  where TSelf : TypedEnumInt16<TSelf>
{
  protected TypedEnumInt16(Int16 id, string description, string code)
    : base(id, description, code) { }
  
  #region 'explicit/implicit' operators ...
  
  // short/Int16 -> TypedEnum (explicit cast only - forces developer to think about it)
  [Obsolete("Direct casting from int is discouraged. Use GetById(short) for clarity and future-proofing.", false)]
  public static explicit operator TypedEnumInt16<TSelf>(Int16 id)
    => GetByID(id);
    
  // TypedEnum -> short/Int16 (implicit or explicit)
  [Obsolete("Prefer TypedEnum.Field.ID for clarity and future-proofing.", false)]
  public static implicit operator Int16(TypedEnumInt16<TSelf> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.ID;
  
  [Obsolete("Prefer TypedEnum.Field.Description for clarity and future-proofing.", false)]
  public static implicit operator string(TypedEnumInt16<TSelf> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.Description;
  
  #endregion
}

// int/Int32 ----------------------------------------------------------------------------
public abstract class TypedEnumInt<TSelf> : TypedEnumInt32<TSelf>
  where TSelf : TypedEnumInt<TSelf>
{
  protected TypedEnumInt(int id, string description, string code)
    : base(id, description, code) { }
}

public abstract class TypedEnumInt32<TSelf> : TypedEnum<TSelf, Int32>
  where TSelf : TypedEnumInt32<TSelf>
{
  protected TypedEnumInt32(Int32 id, string description, string code)
    : base(id, description, code) { }
    
  #region 'explicit/implicit' operators ...
  
  // int/Int32 -> TypedEnum (explicit cast only - forces developer to think about it)
  [Obsolete("Direct casting from int is discouraged. Use GetById(int) for clarity and future-proofing.", false)]
  public static explicit operator TypedEnumInt32<TSelf>(int id)
    => GetByID(id);
    
  // TypedEnum -> int/Int32 (implicit or explicit)
  [Obsolete("Prefer TypedEnum.Field.ID for clarity and future-proofing.", false)]
  public static implicit operator int(TypedEnumInt32<TSelf> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.ID;
  
  [Obsolete("Prefer TypedEnum.Field.Description for clarity and future-proofing.", false)]
  public static implicit operator string(TypedEnumInt32<TSelf> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.Description;
  
  #endregion
}

// long/Int64 ---------------------------------------------------------------------------
public abstract class TypedEnumLong<TSelf> : TypedEnumInt64<TSelf>
  where TSelf : TypedEnumLong<TSelf>
{
  protected TypedEnumLong(long id, string description, string code)
    : base(id, description, code) { }
}

public abstract class TypedEnumInt64<TSelf> : TypedEnum<TSelf, Int64>
  where TSelf : TypedEnumInt64<TSelf>
{
  protected TypedEnumInt64(Int64 id, string description, string code)
    : base(id, description, code) { }
  
  #region 'explicit/implicit' operators ...
  
  // long/Int64 -> TypedEnum (explicit cast only - forces developer to think about it)
  [Obsolete("Direct casting from int is discouraged. Use GetById(long) for clarity and future-proofing.", false)]
  public static explicit operator TypedEnumInt64<TSelf>(Int64 id)
    => GetByID(id);
    
  // TypedEnum -> long/Int64 (implicit or explicit)
  [Obsolete("Prefer TypedEnum.Field.ID for clarity and future-proofing.", false)]
  public static implicit operator Int64(TypedEnumInt64<TSelf> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.ID;
  
  [Obsolete("Prefer TypedEnum.Field.Description for clarity and future-proofing.", false)]
  public static implicit operator string(TypedEnumInt64<TSelf> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.Description;
  
  #endregion
}

// Int128 -------------------------------------------------------------------------------
public abstract class TypedEnumInt128<TSelf> : TypedEnum<TSelf, Int128>
  where TSelf : TypedEnumInt128<TSelf>
{
  protected TypedEnumInt128(Int128 id, string description, string code)
    : base(id, description, code) { }
  
  #region 'explicit/implicit' operators ...
  
  // Int128 -> TypedEnum (explicit cast only - forces developer to think about it)
  [Obsolete("Direct casting from int is discouraged. Use GetById(Int128) for clarity and future-proofing.", false)]
  public static explicit operator TypedEnumInt128<TSelf>(Int128 id)
    => GetByID(id);
    
  // TypedEnum -> Int128 (implicit or explicit)
  [Obsolete("Prefer TypedEnum.Field.ID for clarity and future-proofing.", false)]
  public static implicit operator Int128(TypedEnumInt128<TSelf> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.ID;
  
  [Obsolete("Prefer TypedEnum.Field.Description for clarity and future-proofing.", false)]
  public static implicit operator string(TypedEnumInt128<TSelf> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.Description;
  
  #endregion
}

// guid ---------------------------------------------------------------------------------
public abstract class TypedEnumGuid<TSelf> : TypedEnum<TSelf, Guid>
  where TSelf : TypedEnumGuid<TSelf>
{
  protected TypedEnumGuid(Guid id, string description, string code)
    : base(id, description, code) { }
  
  #region 'explicit/implicit' operators ...
  
  // Guid -> TypedEnum (explicit cast only - forces developer to think about it)
  [Obsolete("Direct casting from int is discouraged. Use GetById(guid) for clarity and future-proofing.", false)]
  public static explicit operator TypedEnumGuid<TSelf>(Guid id)
    => GetByID(id);
    
  // TypedEnum -> Guid (implicit or explicit)
  [Obsolete("Prefer TypedEnum.Field.ID for clarity and future-proofing.", false)]
  public static implicit operator Guid(TypedEnumGuid<TSelf> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.ID;
  
  [Obsolete("Prefer TypedEnum.Field.Description for clarity and future-proofing.", false)]
  public static implicit operator string(TypedEnumGuid<TSelf> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.Description;
  
  #endregion
}

// string -------------------------------------------------------------------------------
public abstract class TypedEnumString<TSelf> : TypedEnum<TSelf, string>
  where TSelf : TypedEnumString<TSelf>
{
  protected TypedEnumString(string id, string description, string code)
    : base(id, description, code) { }
  
  #region 'explicit/implicit' operators ...
  
  // string -> TypedEnum (explicit cast only - forces developer to think about it)
  [Obsolete("Direct casting from int is discouraged. Use GetById(string) for clarity and future-proofing.", false)]
  public static explicit operator TypedEnumString<TSelf>(string id)
    => GetByID(id);
    
  // TypedEnum -> string (implicit or explicit)
  //[Obsolete("Prefer TypedEnum.Field.ID for clarity and future-proofing.", false)]
  //public static implicit operator string(TypedEnumString<TSelf> type)
  //  => type is null ? throw new ArgumentNullException(nameof(type)) : type.ID;
  
  [Obsolete("Prefer TypedEnum.Field.Description for clarity and future-proofing.", false)]
  public static implicit operator string(TypedEnumString<TSelf> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.Description;
  
  #endregion
}
