<Query Kind="Program">
  <Namespace>System.Collections.Immutable</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <RuntimeVersion>8.0</RuntimeVersion>
</Query>

void Main()
{
  //-----------------------------------------------------------------------------------------------
  var event_type = mcsEventType.InspectionSaved;
  event_type.Dump($"var event_type = mcsEventType.InspectionSaved;", 0);
  
  // implicit conversion of int '1' to 'mcsEventType' - specifically to 'mcsEventType.InspectionSaved'
  var dbEventTypeID = 1;
  var db_event_type = (mcsEventType)dbEventTypeID;
  db_event_type.Dump($"var dbEventTypeID = 1;{Environment.NewLine}var db_event_type = (mcsEventType)dbEventTypeID;", 0);
  
  //-----------------------------------------------------------------------------------------------
  (event_type == db_event_type).Dump("event_type == db_event_type");
  (event_type != db_event_type).Dump("event_type != db_event_type");
  
  //-----------------------------------------------------------------------------------------------
  // implicit conversion of 'mcsEventType.InspectionSaved' to int | ID → 1
  int id = mcsEventType.InspectionSaved;
  id.Dump("int id = mcsEventType.InspectionSaved;");

  // 'mcsEventType.InspectionSaved.ID' → 1
  id = mcsEventType.InspectionSaved.ID;
  id.Dump("int id = mcsEventType.InspectionSaved.ID;");
  
  // implicit conversion of 'mcsEventType.InspectionSaved' to string | Description → "Inspection Saved"
  string description = mcsEventType.InspectionSaved;
  description.Dump("string description = mcsEventType.InspectionSaved;");

  // 'mcsEventType.InspectionSaved.Description' → "Inspection Saved"
  description = mcsEventType.InspectionSaved.Description;
  description.Dump("string description = mcsEventType.InspectionSaved.Description;");
  
  // 'mcsEventType.InspectionSaved.Code' → "InspectionSaved"
  string code = mcsEventType.InspectionSaved.Code;
  code.Dump("string code = mcsEventType.InspectionSaved.Code;");


  //-----------------------------------------------------------------------------------------------
  // Safe lookup
  var event_type_by_id = mcsEventType.GetByID(2);
  event_type_by_id.Dump("var event_type_by_id = mcsEventType.GetByID(2);", 0);

  // All values (for dropdowns, etc.)
  var all_event_types = mcsEventType.GetAll();  // IReadOnlyList<mcsEventType>
  all_event_types.Dump("var all_event_types = mcsEventType.GetAll();", 0);
  
  var descriptions = all_event_types.Select(e => e.Description).Distinct().Order().ToList();
  descriptions.Dump("var descriptions = all_event_types.Select(e => e.Description).Distinct().Order().ToList();", 0);
  
  var codes = all_event_types.Select(e => e.Code).Distinct().Order().ToList();
  codes.Dump("var codes = all_event_types.Select(e => e.Code).Distinct().Order().ToList();", 0);
  
  //-----------------------------------------------------------------------------------------------
  // ToString()s ....
  mcsEventType.VacatedTenantSaved.ToString("D").Dump("mcsEventType.VacatedTenantSaved.ToString(\"D\") => D = 'Description'");
  mcsEventType.VacatedTenantSaved.ToString("C").Dump("mcsEventType.VacatedTenantSaved.ToString(\"C\") => C = 'Code (field name)'");
  mcsEventType.VacatedTenantSaved.ToString("I").Dump("mcsEventType.VacatedTenantSaved.ToString(\"I\") => I = 'ID as string'");
  mcsEventType.VacatedTenantSaved.ToString("F").Dump("mcsEventType.VacatedTenantSaved.ToString(\"F\") => F = 'Full/Verbose Format'");
  mcsEventType.VacatedTenantSaved.ToString("G").Dump("mcsEventType.VacatedTenantSaved.ToString(\"G\") => G = 'General/short + id'");
  mcsEventType.VacatedTenantSaved.ToString("f").Dump("mcsEventType.VacatedTenantSaved.ToString(\"f\") => f = 'alternative Full Format'");
  mcsEventType.VacatedTenantSaved.ToString("g").Dump("mcsEventType.VacatedTenantSaved.ToString(\"g\") => g = 'alternative General'");
  
  // AsJsonString()
  mcsEventType.VacatedTenantSaved.AsJsonString().Dump("mcsEventType.VacatedTenantSaved.AsJsonString()");
  
  // AsJsonObject()
  mcsEventType.VacatedTenantSaved.AsJsonObject().Dump("mcsEventType.VacatedTenantSaved.AsJsonObject()");
}

#region abstract mcsSmartEnum(s) && interface(s) ImcsSmartEnum

public interface ImcsSmartEnum<TSelf, Tid>
  where TSelf : ImcsSmartEnum<TSelf, Tid>
  where Tid   : notnull, IEquatable<Tid>, IComparable<Tid>
{
  Tid ID               { get; }
  string Description   { get; }
  string Code          { get; }
  
  static abstract IReadOnlyList<TSelf> GetAll();
  static abstract TSelf GetByID(Tid id);
  
  // Instance methods (these can be abstract in interface)
  string ToString(string? format, IFormatProvider? provider = null);  
  object AsJsonObject();
  string AsJsonString(JsonSerializerOptions options = null);
  
  // Equality & comparison (instance methods)
  bool Equals(TSelf? other);
}

// base ---------------------------------------------------------------------------------
// Base class – clean, no reflection, no static fields except constants
//public abstract class mcsSmartEnumBase<TSelf, Tid> : IFormattable
public abstract class mcsSmartEnum<TSelf, Tid> : IFormattable, ImcsSmartEnum<TSelf, Tid>
  where TSelf : mcsSmartEnum<TSelf, Tid>
  where Tid   : notnull, IEquatable<Tid>, IComparable<Tid>
{
  public Tid ID             { get; }
  public string Description { get; }
  public string Code        { get; private set; } = "Unknown";
  
  protected mcsSmartEnum(Tid id, string description, string code)
  {
    ID          = id          ?? throw new ArgumentNullException(nameof(id));
    Description = description ?? throw new ArgumentNullException(nameof(description));
    Code        = code        ?? throw new ArgumentNullException(nameof(code));
  }
  
  private static readonly JsonSerializerOptions _jsonOptions 
    = new() { PropertyNameCaseInsensitive = true,
              WriteIndented = true };
  
  private static IReadOnlyDictionary<Tid, TSelf> _AllFieldInstances
    => typeof(TSelf).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                    .Where(f => f.FieldType == typeof(TSelf))
                    .Select(f => (TSelf)f.GetValue(null)!)
                    .OrderBy(f => f.ID)
                    .ToDictionary(f => f.ID);
  
  public static IReadOnlyList<TSelf> GetAll()
    => _AllFieldInstances.Values
                         .ToList<TSelf>()
                         .AsReadOnly();
  
  public static TSelf GetByID(Tid id)
    => _AllFieldInstances.TryGetValue(id, out TSelf value) 
        ? value 
        : throw new ArgumentOutOfRangeException(nameof(id), $"Unknown {typeof(TSelf).Name} id: {id}");
  
  /// <summary>
  /// Returns a formatted string representation of the current smart enum instance.
  /// </summary>
  /// <param name="format">
  /// The format specifier to use:
  /// <list type="bullet">
  ///   <item><c>"D"</c> or <c>null</c> → Description (default)</item>
  ///   <item><c>"C"</c> → Code (the field name)</item>
  ///   <item><c>"I"</c> → Id/Key as string</item>
  ///   <item><c>"F"</c> → Full verbose format: "Code (Id): Description"</item>
  ///   <item><c>"G"</c> → General short format: "Code (Id)"</item>
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
  /// var evt = mcsEventType.InspectionSaved;
  /// Console.WriteLine(evt.ToString("F"));  // Output: "InspectionSaved (1): Inspection Saved"
  /// Console.WriteLine($"{evt:C}");         // Output: "InspectionSaved"
  /// </code>
  /// </example>
  public string ToString(string format, IFormatProvider provider = null)
  {
    return format switch
    {
      null or "" or "D" => Description,                                           // Default: Description
                    "C" => Code,                                                  // Code (field name)
                    "I" => ID?.ToString() ?? string.Empty,                        // ID as string
                    
                    "F" => $"{Code} ({ID}): {Description}",                       // Full verbose format
                    "G" => $"{Code} ({ID})",                                      // General / short + id
                    
                    "f" => $"ID: {ID}, Description: {Description}, Code: {Code}", // Full verbose format
                    "g" => $"ID: {ID}, Code: {Code}",                             // General / short + id
                     _  => Description                                            // fallbase
    };
  }
  
  // Optional: override object.ToString() to default to Description (or your preferred format)
  public override string ToString()
    => ToString(format: "F", provider: null);


  /// <summary>
  /// Returns a structured JSON-ready object. Safe for APIs, logs, and serialaztion.
  /// </summary>
  public virtual object AsJsonObject()
    => new {id = ID, description = Description, code = Code};
  
  /// <summary>
  /// Returns a JSON string representation. This is a convenience wrapper over AsJsonObject().
  /// </summary>
  public string AsJsonString(JsonSerializerOptions options = null)
    => JsonSerializer.Serialize(AsJsonObject(), options ?? _jsonOptions);
  
  #region enum-like equality: compare based on ID only (ignore Description for uniqueness)
  
  public override bool Equals(object obj)
    => obj is TSelf other && Equals(other);
  
  public virtual bool Equals(TSelf other)
    => other is not null && EqualityComparer<Tid>.Default.Equals(ID, other.ID);
  
  public override int GetHashCode()
    => EqualityComparer<Tid>.Default.GetHashCode(ID);
  
  public static bool operator == (mcsSmartEnum<TSelf, Tid> left, mcsSmartEnum<TSelf, Tid> right)
    => ReferenceEquals(left, right) || (left is not null && right is not null && left.Equals((TSelf)right));
  
  public static bool operator != (mcsSmartEnum<TSelf, Tid> left, mcsSmartEnum<TSelf, Tid> right)
    => !(left == right);
  
  public static bool operator < (mcsSmartEnum<TSelf, Tid> left, mcsSmartEnum<TSelf, Tid> right)
    => left.ID.CompareTo(right.ID) < 0;
    
  public static bool operator > (mcsSmartEnum<TSelf, Tid> left, mcsSmartEnum<TSelf, Tid> right)
    => left.ID.CompareTo(right.ID) > 0;
    
  public static bool operator <= (mcsSmartEnum<TSelf, Tid> left, mcsSmartEnum<TSelf, Tid> right)
    => left.ID.CompareTo(right.ID) <= 0;
  
  public static bool operator >= (mcsSmartEnum<TSelf, Tid> left, mcsSmartEnum<TSelf, Tid> right)
    => left.ID.CompareTo(right.ID) >= 0;

  #endregion
}

// short/int16 --------------------------------------------------------------------------
public abstract class mcsSmartEnumShort<Tself> : mcsSmartEnumInt16<Tself>
  where Tself : mcsSmartEnumShort<Tself>
{
  protected mcsSmartEnumShort(short id, string description, string code) 
    : base(id, description, code) { }
}

public abstract class mcsSmartEnumInt16<Tself> : mcsSmartEnum<Tself, Int16>
  where Tself : mcsSmartEnumInt16<Tself>
{
  protected mcsSmartEnumInt16(Int16 id, string description, string code) 
    : base(id, description, code) { }
  
  #region 'explicit/implicit' operators ...
  
  // short -> EventType (explicit cast only - forces developer to think about it)
  [Obsolete("Direct casting from int is discouraged. Use GetById(short) for clarity and future-proofing.", false)]
  public static explicit operator mcsSmartEnumInt16<Tself>(Int16 id)
    => GetByID(id);
    
  // EventType -> short (implicit or explicit)
  [Obsolete("Prefer mcsEventType.Field.ID for clarity and future-proofing.", false)]
  public static implicit operator Int16(mcsSmartEnumInt16<Tself> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.ID;
  
  [Obsolete("Prefer mcsEventType.Field.Description for clarity and future-proofing.", false)]
  public static implicit operator string(mcsSmartEnumInt16<Tself> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.Description;
  
  #endregion
}

// int/int32 ----------------------------------------------------------------------------
public abstract class mcsSmartEnumInt<Tself> : mcsSmartEnumInt32<Tself>
  where Tself : mcsSmartEnumInt<Tself>
{
  protected mcsSmartEnumInt(int id, string description, string code)
    : base(id, description, code) { }
}

public abstract class mcsSmartEnumInt32<Tself> : mcsSmartEnum<Tself, Int32>
  where Tself : mcsSmartEnumInt32<Tself>
{
  protected mcsSmartEnumInt32(Int32 id, string description, string code)
    : base(id, description, code) { }
    
  #region 'explicit/implicit' operators ...
  
  // int -> EventType (explicit cast only - forces developer to think about it)
  [Obsolete("Direct casting from int is discouraged. Use GetById(int) for clarity and future-proofing.", false)]
  public static explicit operator mcsSmartEnumInt32<Tself>(int id)
    => GetByID(id);
    
  // EventType -> int (implicit or explicit)
  [Obsolete("Prefer mcsEventType.Field.ID for clarity and future-proofing.", false)]
  public static implicit operator int(mcsSmartEnumInt32<Tself> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.ID;
  
  [Obsolete("Prefer mcsEventType.Field.Description for clarity and future-proofing.", false)]
  public static implicit operator string(mcsSmartEnumInt32<Tself> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.Description;
  
  #endregion
}

// long/int64 ---------------------------------------------------------------------------
public abstract class mcsSmartEnumLong<Tself> : mcsSmartEnumInt64<Tself>
  where Tself : mcsSmartEnumInt64<Tself>
{
  protected mcsSmartEnumLong(long id, string description, string code)
    : base(id, description, code) { }
}

public abstract class mcsSmartEnumInt64<Tself> : mcsSmartEnum<Tself, Int64>
  where Tself : mcsSmartEnumInt64<Tself>
{
  protected mcsSmartEnumInt64(Int64 id, string description, string code)
    : base(id, description, code) { }
  
  #region 'explicit/implicit' operators ...
  
  // long -> EventType (explicit cast only - forces developer to think about it)
  [Obsolete("Direct casting from int is discouraged. Use GetById(long) for clarity and future-proofing.", false)]
  public static explicit operator mcsSmartEnumInt64<Tself>(Int64 id)
    => GetByID(id);
    
  // EventType -> long (implicit or explicit)
  [Obsolete("Prefer mcsEventType.Field.ID for clarity and future-proofing.", false)]
  public static implicit operator Int64(mcsSmartEnumInt64<Tself> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.ID;
  
  [Obsolete("Prefer mcsEventType.Field.Description for clarity and future-proofing.", false)]
  public static implicit operator string(mcsSmartEnumInt64<Tself> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.Description;
  
  #endregion
}

// int128 -------------------------------------------------------------------------------
public abstract class mcsSmartEnumInt128<Tself> : mcsSmartEnum<Tself, Int128>
  where Tself : mcsSmartEnumInt128<Tself>
{
  protected mcsSmartEnumInt128(Int128 id, string description, string code)
    : base(id, description, code) { }
  
  #region 'explicit/implicit' operators ...
  
  // Int128t -> EventType (explicit cast only - forces developer to think about it)
  [Obsolete("Direct casting from int is discouraged. Use GetById(Int128) for clarity and future-proofing.", false)]
  public static explicit operator mcsSmartEnumInt128<Tself>(Int128 id)
    => GetByID(id);
    
  // EventType -> Int128 (implicit or explicit)
  [Obsolete("Prefer mcsEventType.Field.ID for clarity and future-proofing.", false)]
  public static implicit operator Int128(mcsSmartEnumInt128<Tself> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.ID;
  
  [Obsolete("Prefer mcsEventType.Field.Description for clarity and future-proofing.", false)]
  public static implicit operator string(mcsSmartEnumInt128<Tself> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.Description;
  
  #endregion
}

// guid ---------------------------------------------------------------------------------
public abstract class mcsSmartEnumGuid<Tself> : mcsSmartEnum<Tself, Guid>
  where Tself : mcsSmartEnumGuid<Tself>
{
  protected mcsSmartEnumGuid(Guid id, string description, string code)
    : base(id, description, code) { }
  
  #region 'explicit/implicit' operators ...
  
  // guid -> EventType (explicit cast only - forces developer to think about it)
  [Obsolete("Direct casting from int is discouraged. Use GetById(guid) for clarity and future-proofing.", false)]
  public static explicit operator mcsSmartEnumGuid<Tself>(Guid id)
    => GetByID(id);
    
  // EventType -> guid (implicit or explicit)
  [Obsolete("Prefer mcsEventType.Field.ID for clarity and future-proofing.", false)]
  public static implicit operator Guid(mcsSmartEnumGuid<Tself> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.ID;
  
  [Obsolete("Prefer mcsEventType.Field.Description for clarity and future-proofing.", false)]
  public static implicit operator string(mcsSmartEnumGuid<Tself> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.Description;
  
  #endregion
}

// string -------------------------------------------------------------------------------
public abstract class mcsSmartEnumString<Tself> : mcsSmartEnum<Tself, string>
  where Tself : mcsSmartEnumString<Tself>
{
  protected mcsSmartEnumString(string id, string description, string code)
    : base(id, description, code) { }
  
  #region 'explicit/implicit' operators ...
  
  // string -> EventType (explicit cast only - forces developer to think about it)
  [Obsolete("Direct casting from int is discouraged. Use GetById(string) for clarity and future-proofing.", false)]
  public static explicit operator mcsSmartEnumString<Tself>(string id)
    => GetByID(id);
    
  // EventType -> string (implicit or explicit)
  //[Obsolete("Prefer mcsEventType.Field.ID for clarity and future-proofing.", false)]
  //public static implicit operator string(mcsSmartEnumString<Tself> type)
  //  => type is null ? throw new ArgumentNullException(nameof(type)) : type.ID;
  
  [Obsolete("Prefer mcsEventType.Field.Description for clarity and future-proofing.", false)]
  public static implicit operator string(mcsSmartEnumString<Tself> type)
    => type is null ? throw new ArgumentNullException(nameof(type)) : type.Description;
  
  #endregion
}

#endregion

public sealed class mcsEventType : mcsSmartEnumInt<mcsEventType>
{
  private mcsEventType(int id, string description, string code = null) 
    : base(id, description, code) { }

  #region COMMENTED OUT: specific mcsEventType declarations w/o nameof(field_name) ...
  /****************************************************************************************************************************************************************************************************

  public static readonly mcsEventType InspectionSaved                            = new(  1, "Inspection Saved");
  public static readonly mcsEventType InsertFamily                               = new(  2, "Family Inserted");
  public static readonly mcsEventType VacatedTenantSaved                         = new(  3, "Vacated Tenant Saved");
  public static readonly mcsEventType PmPhaSaved                                 = new(  4, "Agency Saved");
  public static readonly mcsEventType FamilyCertSaved                            = new(  5, "Family Cert Saved");
                                                                                                                                                        
  public static readonly mcsEventType WaitingListApplicationSaved                = new(  6, "Waiting List Application Saved");
  public static readonly mcsEventType RentReasonablenessSaved                    = new(  7, "Rent Reasonableness Saved");
  public static readonly mcsEventType UpdateFamily                               = new(  8, "Family Updated");
  public static readonly mcsEventType PortabilityTenantSaved                     = new(  9, "Section 8 Portability Tenant Saved");
  public static readonly mcsEventType PortabilityAdjustmentSaved                 = new( 10, "Section 8 Portability Adjustment Saved");
                                                                                                                                                         
  public static readonly mcsEventType RapTrapFileGroupSaved                      = new( 11, "RAP/T-RAP File Group Saved");
  public static readonly mcsEventType RapTrapFormSaved                           = new( 12, "RAP/T-RAP Form Saved");
  public static readonly mcsEventType MCSImportedData                            = new( 13, "MCS Imported Data");
  public static readonly mcsEventType PortabilityMonthlyPayablesSaved            = new( 14, "Section 8 Portability Monthly Payables Saved");
  public static readonly mcsEventType CountySaved                                = new( 15, "County Saved");
                                                                                                                                                         
  public static readonly mcsEventType PortabilityDisbusementSaved                = new( 16, "Section 8 Portability Disbursement Saved");
  public static readonly mcsEventType TracsSaved                                 = new( 17, "TRACS Saved");
  public static readonly mcsEventType PhaSaved                                   = new( 18, "PHA Saved");
  public static readonly mcsEventType ChecksSaved                                = new( 19, "Checks Saved");
  public static readonly mcsEventType gpIncomeLimitSaved                         = new( 20, "Income Limit Saved");
                                                                                                                                                         
  public static readonly mcsEventType MonthEndSaved                              = new( 21, "Month End Saved");
  public static readonly mcsEventType LandlordSaved                              = new( 22, "Landlord Saved");
  public static readonly mcsEventType FamilyCertSetupSaved                       = new( 23, "Family Cert Setup Saved");
  public static readonly mcsEventType HAPContractNumberSaved                     = new( 24, "HAP Contract Number Saved");
  public static readonly mcsEventType Section8UnitSaved                          = new( 25, "Section8 Unit Saved");
                                                                                                                                                         
  public static readonly mcsEventType MCSLandlordImport                          = new( 26, "MCS Landlord Imported");
  public static readonly mcsEventType ComparableUnitSaved                        = new( 27, "Comparable Unit Saved");
  public static readonly mcsEventType RequestedUnitSaved                         = new( 28, "Requested Unit Saved");
  public static readonly mcsEventType PortabilityMasterSaved                     = new( 29, "Section 8 Portability Setup Saved");
  public static readonly mcsEventType ResidentInfoSaved                          = new( 30, "Resident Information Saved");
                                                                                                                                                         
  public static readonly mcsEventType ProgramSaved                               = new( 31, "Program Saved");
  public static readonly mcsEventType ForumMessagePosted                         = new( 32, "Forum Message Posted");
  public static readonly mcsEventType GlobalValueSaved                           = new( 33, "Global Value Saved");
  public static readonly mcsEventType FinBankSaved                               = new( 34, "Bank Saved");
  public static readonly mcsEventType DynamicPageSaved                           = new( 35, "Dynamic Page Saved");
                                                                                                                                                         
  public static readonly mcsEventType FinAccountSaved                            = new( 36, "Account Saved");
  public static readonly mcsEventType LandlordAdjustmentSaved                    = new( 37, "Landlord Adjustment Saved");
//  public static readonly mcsEventType                                            = new( 38, "");  //NOT DEFINED in 'enum'
  public static readonly mcsEventType LandlordPayablesPosted                     = new( 39, "Landlord Payables Posted");
  public static readonly mcsEventType PublicUnitSaved                            = new( 40, "Public Unit Saved");
                                                                                                                                                         
  public static readonly mcsEventType ProjectSaved                               = new( 41, "Project Saved");
  public static readonly mcsEventType PublicBuildingSaved                        = new( 42, "Public Building Saved");
  public static readonly mcsEventType Section8BuildingSaved                      = new( 43, "Section 8 Building Saved");
  public static readonly mcsEventType PortabilityTenantRentSaved                 = new( 44, "Section 8 Portability Tenant Rent Saved");
  public static readonly mcsEventType tracsMAT30Saved                            = new( 45, "Tracs MAT30 Saved");
                                                                                                                                                         
  public static readonly mcsEventType tracsMonthlySubmissionFileSaved            = new( 46, "Tracs Monthly Submission File Saved");
  public static readonly mcsEventType FinDocumentSaved                           = new( 47, "Financial Document Saved");
  public static readonly mcsEventType FinControlGroupSaved                       = new( 48, "Financial Control Group Saved");
  public static readonly mcsEventType FinTransactionSaved                        = new( 49, "Financial Transaction Saved");
  public static readonly mcsEventType FinGlAccountSaved                          = new( 50, "Fin Gl Account Saved");
                                                                                                                                                         
  public static readonly mcsEventType FinTransactionTypeSaved                    = new( 51, "Fin Transaction Type Saved");
//  public static readonly mcsEventType phaFileSaved                               = new( 52, "PHA File Saved");                    // COMMENTED OUT in 'enum'
  public static readonly mcsEventType MaFormSaved                                = new( 53, "MaForm Saved");
//  public static readonly mcsEventType StMaUnitSaved                              = new( 54, "General Certification Unit Saved");  // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static readonly mcsEventType StMaIncomeRangeBaseSaved                   = new( 55, "StMa Income Range Base Saved");
                                                                                                                                                         
  public static readonly mcsEventType DataExported                               = new( 56, "Data Exported");
//  public static readonly mcsEventType FamilyCertSubmissionFileSaved              = new( 57, "50058 Submission File");   // COMMENTED OUT in 'enum'
  public static readonly mcsEventType PhaUserSaved                               = new( 58, "PHA User Saved");
  public static readonly mcsEventType pmPhaAccountSaved                          = new( 59, "Agency Account Saved");
  public static readonly mcsEventType GeneralLedgerJournalEntrySaved             = new( 60, "General Ledger Journal Entry Saved");
                                                                                                                                                         
  public static readonly mcsEventType finHoldReasonSaved                         = new( 61, "finHoldReason Saved");
  public static readonly mcsEventType finTransPartSelectionSaved                 = new( 62, "finTransactionPartSelection Saved");
  public static readonly mcsEventType stMaSetupSaved                             = new( 63, "stMaSetup Saved");
  public static readonly mcsEventType finAdminFee                                = new( 64, "finAdminFee Saved");
  public static readonly mcsEventType stMaPaymentStandardTownSaved               = new( 65, "stMaPaymentStandardTown Saved");
                                                                                                                                                         
  public static readonly mcsEventType stMaPaymentStandardBedSaved                = new( 66, "stMaPaymentStandardBed Saved");
  public static readonly mcsEventType hapScheduleAdjustmentUpdate                = new( 67, "HAP Schedule Adjustment Update");
  public static readonly mcsEventType imRoomTypeDefinitionSaved                  = new( 68, "Inspection Manager Room Type Definition Saved");
  public static readonly mcsEventType imQuestionTypeDefinitionSaved              = new( 69, "Inspection Manager Question Type Definition Saved");
  public static readonly mcsEventType imFailureTypeDefinitionSaved               = new( 70, "Inspection Manager Failure Type Definition Saved");
                                                                                                                                                         
  public static readonly mcsEventType imFormTypeSaved                            = new( 71, "Inspection Manager Form Type Saved");
  public static readonly mcsEventType imInspectionSaved                          = new( 72, "Inspection Manager Inspection Saved");
  public static readonly mcsEventType vendorSaved                                = new( 73, "Vendor Saved");
//  public static readonly mcsEventType distributionSaved                          = new( 74, "Distribution Saved");  // NOT FOUND in the 'eventTypeDescription's Select Case statement
//  public static readonly mcsEventType stCtUnitSaved                              = new( 75, "stCtUnitSaved");       // COMMENTED OUT in 'enum'
                                                                                                                                                         
  public static readonly mcsEventType saveSignature                              = new( 76, "Signature Saved");
  public static readonly mcsEventType phMiscChargesSaved                         = new( 77, "PH Misc Charges Saved");
  public static readonly mcsEventType finPaymentTermsSaved                       = new( 78, "Payment Terms Saved");
  public static readonly mcsEventType tracsSetupSaved                            = new( 79, "TRACS Setup Saved");
  public static readonly mcsEventType landlordsMerged                            = new( 80, "Landlords Merged");
                                                                                                                                                         
  public static readonly mcsEventType finPaymentScheduleSaved                    = new( 81, "Payment Schedule Saved");
  public static readonly mcsEventType waitingListLotteryProcess                  = new( 82, "Waiting List Lottery Process");
  public static readonly mcsEventType recurringInvoiceSaved                      = new( 83, "Recurring Invoice Saved");
  public static readonly mcsEventType imCustomQuestionTypeDefinitionSaved        = new( 84, "Inspection Manager Custom Question Type Definition Saved");
  public static readonly mcsEventType glReportGroupSaved                         = new( 85, "General Ledger Report Group Saved");
                                                                                                                                                         
  public static readonly mcsEventType phRepaymentAgreementSaved                  = new( 86, "Tenant Repayment Agreement Saved");
  public static readonly mcsEventType FinOpenItemRelationSaved                   = new( 87, "Financial Open Item Relation Saved");
  public static readonly mcsEventType fmCustomValueSetupSaved                    = new( 88, "Family Custom Value Setup Saved");
  public static readonly mcsEventType smTicketSaved                              = new( 89, "Support Manager Ticket Saved");
  public static readonly mcsEventType smUserSaved                                = new( 90, "Support Manager User Saved");
                                                                                                                                                         
  public static readonly mcsEventType woWorkOrderSaved                           = new( 91, "Work Order Saved");
  public static readonly mcsEventType woAssetSaved                               = new( 92, "Work Order Asset Saved");
  public static readonly mcsEventType woInventorySaved                           = new( 93, "Work Order Inventory Saved");
  public static readonly mcsEventType woTaskSaved                                = new( 94, "Work Order Task Saved");
  public static readonly mcsEventType woAssetMaintenanceSaved                    = new( 95, "Work Order Asset Maintenance Saved");
                                                                                                                                                         
  public static readonly mcsEventType woEmployeeAdjustmentSaved                  = new( 96, "Work Order Employee Adjustment Saved");
  public static readonly mcsEventType woInventoryAdjustmentSaved                 = new( 97, "Work Order Inventory Adjustment Saved");
  public static readonly mcsEventType woSetupAssetTypeSaved                      = new( 98, "Work Order Setup Asset Type Saved");
  public static readonly mcsEventType woSetupNumberSaved                         = new( 99, "Work Order Setup Number Saved");
  public static readonly mcsEventType woSetupUnitOfMeasureSaved                  = new(100, "Work Order Setup Unit Of Measure Saved");
                                                                                                                                                         
  public static readonly mcsEventType woSetupInventoryTypeSaved                  = new(101, "Work Order Setup Inventory Type Saved");
  public static readonly mcsEventType woSetupInventoryLocationSaved              = new(102, "Work Order Setup Inventory Location Saved");
  public static readonly mcsEventType glTemplateDocSaved                         = new(103, "glTemplateDoc Saved");
  public static readonly mcsEventType woInventoryUpdateSaved                     = new(104, "Work Order Inventory Update Saved");
  public static readonly mcsEventType glProjectGroupSaved                        = new(105, "glProjectGroup Saved");
                                                                                                                                                         
  public static readonly mcsEventType woSetupDefaultCommentsSaved                = new(106, "Work Order Setup Default Comments Saved");
  public static readonly mcsEventType rapTrapSetupSaved                          = new(107, "Rap Trap Setup Saved");
  public static readonly mcsEventType familiesMerged                             = new(108, "Families Merged");
  public static readonly mcsEventType woSetupLaborTypeSaved                      = new(109, "Work Order Setup Labor Type Saved");
  public static readonly mcsEventType uaScheduleTypeSaved                        = new(110, "Utility Allowance ScheduleType Saved");
                                                                                                                                                         
  public static readonly mcsEventType uaScheduleSaved                            = new(111, "Utility Allowance Schedule Saved");
  public static readonly mcsEventType uaScheduleBedSizeSaved                     = new(112, "Utility Allowance ScheduleBedSize Saved");
  public static readonly mcsEventType zipGroupSaved                              = new(113, "Zip Group Saved");
  public static readonly mcsEventType zipGroupItemSaved                          = new(114, "Zip Group Item Saved");
  public static readonly mcsEventType poPurchaseOrderSaved                       = new(115, "Purchase Order Saved");
                                                                                                                                                         
  public static readonly mcsEventType poLineItemSaved                            = new(116, "Purchase Order Line Item Saved");
  public static readonly mcsEventType woSetupReportItemSaved                     = new(117, "Work Order Setup Report Item Saved");
  public static readonly mcsEventType paymentStandardTypeSaved                   = new(118, "Payment Standard Type Saved");
  public static readonly mcsEventType phaUserSignatureSaved                      = new(119, "Pha User Signature Saved");
  public static readonly mcsEventType finStatementSetupSaved                     = new(120, "Financial Statement Setup Saved");
                                                                                                                                                         
  public static readonly mcsEventType glPayrollReportTypeSetupSaved              = new(121, "Payroll Report Type Setup Saved");
  public static readonly mcsEventType glPayrollTypeSetupSaved                    = new(122, "Payroll Type Setup Saved");
  public static readonly mcsEventType glPayrollSummarySaved                      = new(123, "Payroll Summary Saved");
  public static readonly mcsEventType glPayrollDistributionSaved                 = new(124, "Payroll Distribution Saved");
  public static readonly mcsEventType glPayrollEmployeeDistributionSaved         = new(125, "Payroll Employee Distribution Saved");
                                                                                                                                                         
  public static readonly mcsEventType vmsTypeSaved                               = new(126, "VMS Type Saved");
  public static readonly mcsEventType glPayrollDistributionFieldNumberSaved      = new(127, "Payroll Distribution Field Number Saved");
  public static readonly mcsEventType meterReadingSaved                          = new(128, "Meter Reading Saved");
  public static readonly mcsEventType vmsEffectiveDateSaved                      = new(129, "VMS Effective Date Saved");
  public static readonly mcsEventType phHoEffectiveAdjustmentsSaved              = new(130, "PH Homeownership Effective Adjustments");
                                                                                                                                                         
  public static readonly mcsEventType phHoAdjustmentsToBalanceSaved              = new(131, "PH Homeownership Adjustments to Balance");
  public static readonly mcsEventType wlStatusSaved                              = new(132, "WL Status Saved");
  public static readonly mcsEventType glJESTemplateSaved                         = new(133, "GL Journal Entry Simple Template Saved");
  public static readonly mcsEventType glJESTemplateDetailSaved                   = new(134, "GL Journal Entry Simple Template Detail Saved");
  public static readonly mcsEventType voidedCheckSaved                           = new(135, "Voided Check Saved");
                                                                                                                                                         
  public static readonly mcsEventType recertPacketSetupSaved                     = new(136, "Recertification Packet Setup Saved");
  public static readonly mcsEventType rfParticipatingProgramSaved                = new(137, "RF Participating Program Saved");
  public static readonly mcsEventType woReportItemSaved                          = new(138, "Work Order Report Item Saved");
  public static readonly mcsEventType projectLookupCodeSaved                     = new(139, "Project Lookup Code Saved");
  public static readonly mcsEventType payeeSaved                                 = new(140, "Payee Saved");
                                                                                                                                                         
  public static readonly mcsEventType payeeTemplateSaved                         = new(141, "Payee Template Saved");
  public static readonly mcsEventType familyCertSubmissionErrorSaved             = new(142, "Family Cert Submission Error Saved");
//  public static readonly mcsEventType orderingProductSaved                       = new(143, "Forms Ordering Product Saved");        // COMMENTED OUT in 'enum'
//  public static readonly mcsEventType orderingOrderSaved                         = new(144, "Forms Ordering Order Saved");          // COMMENTED OUT in 'enum'
//  public static readonly mcsEventType orderingOrderDetailsSaved                  = new(145, "Forms Ordering Order Details Saved");  // COMMENTED OUT in 'enum'
                                                                                                                                                         
  public static readonly mcsEventType phUtilityBillingSaved                      = new(146, "PH Utility Billing Saved");
  public static readonly mcsEventType diFolderSaved                              = new(147, "Document Imaging Folder Saved");
  public static readonly mcsEventType diPHADocumentSaved                         = new(148, "PHA Document Saved");
  public static readonly mcsEventType diUserDocumentSaved                        = new(149, "User Document Saved");
  public static readonly mcsEventType woInventoryExpensingSetupSaved             = new(150, "Work Order Inventory Expensing Setup Saved");
                                                                                                                                                         
  public static readonly mcsEventType woInventoryExpensingSaved                  = new(151, "Work Order Inventory Expensing Saved");
  public static readonly mcsEventType fmAppointmentSaved                         = new(152, "Family Appointment Saved");
  public static readonly mcsEventType phDepositMarginSetupSaved                  = new(153, "Deposit Ticket Margin Setup Saved");
  public static readonly mcsEventType communityServiceDetailSaved                = new(154, "Community Service Detail Saved");
  public static readonly mcsEventType glAccountReconciliationSaved               = new(155, "Account Reconciliation Saved");
                                                                                                                                                         
  public static readonly mcsEventType genericNoteSaved                           = new(156, "Generic Note Saved");
  public static readonly mcsEventType genericNoteReminderSaved                   = new(157, "Generic Note Reminder Saved");
  public static readonly mcsEventType gnNoteRemindAllSaved                       = new(158, "gnNoteRemindAll Saved");
  public static readonly mcsEventType diSignatureDetailSaved                     = new(159, "Document Imaging Signature Detail Saved");
  public static readonly mcsEventType flatRentAreaTypeSaved                      = new(160, "Flat Rent Area Type Saved");
                                                                                                                                                         
  public static readonly mcsEventType flatRentAreaDateSaved                      = new(161, "Flat Rent Area Date Saved");
  public static readonly mcsEventType tracsHistoricalSaved                       = new(162, "TRACS Historical Saved");
  public static readonly mcsEventType annualHQSCertFormSaved                     = new(163, "Annual HQS Form Saved");
  public static readonly mcsEventType staticFileSaved                            = new(164, "Static File Saved");
  public static readonly mcsEventType mspTaskSaved                               = new(165, "Multi-Step Task Saved");
                                                                                                                                                         
  public static readonly mcsEventType mspStepSaved                               = new(166, "Multi-Step Step Saved");
  public static readonly mcsEventType perFormAutoAdjustmentSave                  = new(167, "Per-Form Auto Adjustment Save");
  public static readonly mcsEventType wlApplicantQuestionSaved                   = new(168, "Applicant Question Saved");
  public static readonly mcsEventType wlApplicantFullAppAnswerSaved              = new(169, "Applicant Question Full App Answer Saved");
  public static readonly mcsEventType ocOnlineClassPHAUserSaved                  = new(170, "Online Class PHA User Saved");
                                                                                                                                                         
  public static readonly mcsEventType repaymentAgreementTypeSaved                = new(171, "Repayment Agreement Type Saved");
  public static readonly mcsEventType fairMarketRentAreaTypeSaved                = new(172, "Fair Market Rent Area Saved");
  public static readonly mcsEventType fairMarketRentAmountSaved                  = new(173, "Fair Market Rent Amount Saved");
  public static readonly mcsEventType prRequisitionSaved                         = new(174, "Purchase Requisition Saved");
  public static readonly mcsEventType poShippingAddressSaved                     = new(175, "Purchase Order Shipping Address Saved");
                                                                                                                                                         
  public static readonly mcsEventType prApprovalSetupSaved                       = new(176, "PR Approval Setup Saved");
  public static readonly mcsEventType prApprovalCostPhaUserSaved                 = new(177, "PR Approval Cost Pha User Saved");
  public static readonly mcsEventType prApprovalSaved                            = new(178, "PR Approval Saved");
  public static readonly mcsEventType tracsRepAgreementLinkSaved                 = new(179, "Repayment Agreement Link Saved");
  public static readonly mcsEventType fssBalanceAdjustmentSaved                  = new(180, "FSS Balance Adjustment Saved");
                                                                                                                                                         
  public static readonly mcsEventType vendorContractSaved                        = new(181, "Vendor Contract Saved");
//  public static readonly mcsEventType familyNotificationSetupSaved               = new(182, "Family Notification Setup Saved");   // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static readonly mcsEventType familyCertMasterVoucherExtensionSaved      = new(183, "Family Cert Voucher Extension Saved");
  public static readonly mcsEventType apSelectForPayementSaved                   = new(184, "AP Select For Payment Saved");
  public static readonly mcsEventType familyCertContractRentIncreaseSaved        = new(185, "Family Cert Contract Rent Increase");
                                                                                                                                                         
  public static readonly mcsEventType backgroundCheckRequestSaved                = new(186, "Background Check Request Saved");
  public static readonly mcsEventType fmPublicSafetyIncidentSaved                = new(187, "Public Safety Incident Saved");
  public static readonly mcsEventType update1099BatchSaved                       = new(188, "Update 1099 Batch Saved");
  public static readonly mcsEventType insert1099BatchSaved                       = new(189, "Insert 1099 Batch Saved");
  public static readonly mcsEventType woBillingPosted                            = new(190, "Post Work Order Billing");
                                                                                                                                                         
  public static readonly mcsEventType fssItspGoalSaved                           = new(191, "FSS ITSP Goal Saved");
  public static readonly mcsEventType fssItspNoteSaved                           = new(192, "FSS ITSP Note Saved");
  public static readonly mcsEventType certNotificationSaved                      = new(193, "Notification Saved");
  public static readonly mcsEventType certNotificationProcessSaved               = new(194, "Notification Process Saved");
  public static readonly mcsEventType finUtilityDistributionSaved                = new(195, "Utility Distribution Saved");
                                                                                                                                                         
  public static readonly mcsEventType communityServiceRecurringSaved             = new(196, "Community Service Recurring Saved");
  public static readonly mcsEventType noteTypeSaved                              = new(197, "Note Type Saved");
  public static readonly mcsEventType phaPortalSetupSaved                        = new(198, "Portal Management Setup Saved");
  public static readonly mcsEventType phaPortalFamilyOptionsSaved                = new(199, "Portal Family Options Saved");
//  public static readonly mcsEventType phaFamilyPortalPermissionSetup             = new(200, "Family Portal Permission Setup");    // COMMENTED OUT in 'enum'
                                                                                                                                                         
  public static readonly mcsEventType finTransPartSelectionByProjectSaved        = new(201, "finTransactionPartSelectionByProject Saved");
  public static readonly mcsEventType glExportFileSaved                          = new(202, "GL Export File Saved");
  public static readonly mcsEventType famCertUtilScheduleTypeSaved               = new(203, "FamilyCertUtilityScheduleType Saved");
  public static readonly mcsEventType famCertUtilScheduleDateSaved               = new(204, "FamilyCertUtilityScheduleDate Saved");
  public static readonly mcsEventType stMaUtilityScheduleTypeSaved               = new(205, "StMaUtilityScheduleType Saved");
                                                                                                                                                         
  public static readonly mcsEventType stMaUtilityScheduleDateSaved               = new(206, "StMaUtilityScheduleDate Saved");
  public static readonly mcsEventType phaPortalOppAccSetupSaved                  = new(207, "phaPortalOppAccSetup Saved");
  public static readonly mcsEventType phaPortalOppAccUserSaved                   = new(208, "phaPortalOppAccUser Saved");
  public static readonly mcsEventType phaPortalOppCustomerSaved                  = new(209, "phaPortalOppCustomer Saved");
  public static readonly mcsEventType phaPortalOppBatchSaved                     = new(210, "phaPortalOppBatch Saved");
                                                                                                                                                         
  public static readonly mcsEventType phaPortalOppPaymentSaved                   = new(211, "phaPortalOppPayment Saved");
  public static readonly mcsEventType phaPortalOppPaymentDetailSaved             = new(212, "phaPortalOppPaymentDetail Saved");
  public static readonly mcsEventType sohaDhcdTransmissionFileSaved              = new(213, "SOHA EOHLC Transmission File Saved");
  public static readonly mcsEventType phaPortalMcConversationSaved               = new(214, "PortalConversation Saved");
  public static readonly mcsEventType phaPortalMcAttachmentSaved                 = new(215, "PortalAttachment Saved");
                                                                                                                                                         
  public static readonly mcsEventType phaPortalMcMessageSaved                    = new(216, "PortalMessage Saved");
  public static readonly mcsEventType woSetupEmployeeScheduleSaved               = new(217, "woSetupEmployeeSchedule Saved");
  public static readonly mcsEventType woSetupEmployeeScheduleDeSaved             = new(218, "woSetupEmployeeScheduleDe Saved");
  public static readonly mcsEventType unitTurnoverSaved                          = new(219, "unitTurnover Saved");
  public static readonly mcsEventType certRequestSaved                           = new(220, "certRequest Saved");
                                                                                                                                                         
  public static readonly mcsEventType phaPortalLandlordOptionsSetupSaved         = new(221, "Portal Landlord Options Saved");
  public static readonly mcsEventType stMaHomeRentIncomeScheduleTypeSaved        = new(222, "stMaHomeRentIncomeScheduleType Saved");
  public static readonly mcsEventType stMaHomeRentIncomeScheduleDateSaved        = new(223, "stMaHomeRentIncomeScheduleDate Saved");
  public static readonly mcsEventType phaPortalLandlordSaved                     = new(224, "Portal Landlord Saved");
  public static readonly mcsEventType certFormReviewSaved                        = new(225, "Certification Form Review Saved");
                                                                                                                                                         
  public static readonly mcsEventType invoiceSavedAvidInvoiceImport              = new(226, "Invoice Saved From Avid Invoice Import");
  public static readonly mcsEventType phaPortalApplicantOptionsSaved             = new(227, "Portal Applicant Saved");
  public static readonly mcsEventType glTrialBalanceWildCardSaved                = new(228, "GL Trial Balance Wild Card Saved");
  public static readonly mcsEventType certFinishFormPacketSetupSaved             = new(229, "Cert Finish Form Packet Setup Saved");
  public static readonly mcsEventType familyCertChangeOfOwnershipSaved           = new(230, "Family Cert Change of Ownership");
                                                                                                                                                         
  public static readonly mcsEventType certSignatureRequestSaved                  = new(231, "Cert Signature Request Saved");
  public static readonly mcsEventType invoiceSavedInvoiceImport                  = new(232, "invoice Saved Invoice Import");
  public static readonly mcsEventType woPhaSetupSaved                            = new(233, "work order pha setup saved");
  public static readonly mcsEventType diUploadApi                                = new(234, "API Uploaded Document");
  public static readonly mcsEventType apVendorPhaSetupSaved                      = new(235, "ap vendor pha setup saved");
                                                                                                                                                         
  public static readonly mcsEventType ecEmailAddressSaved                        = new(236, "ec Email Address Saved");
  public static readonly mcsEventType woSetupPrioritySaved                       = new(237, "Work Order Setup Priority Saved");
  public static readonly mcsEventType woSetupRequestedBySaved                    = new(238, "Work Order Setup Requested By Saved");
  public static readonly mcsEventType woSetupConfigurationSaved                  = new(239, "Work Order Setup Configuration Saved");
  public static readonly mcsEventType familyCertMassCreateBatchSaved             = new(240, "Family Cert Mass Create Batch Saved");
                                                                                                                                                         
  public static readonly mcsEventType familyCertPaymentStandardChangeSaved       = new(241, "Family Cert Payment Standard Change Saved");
  public static readonly mcsEventType familyCertEndOfParticipationSaved          = new(242, "Family Cert End of Participation Saved");
  public static readonly mcsEventType glDepreciableAssetTypeSaved                = new(243, "GL Depreciable Asset Type Saved");
  public static readonly mcsEventType familyCertMoveOutReasonSaved               = new(244, "50058 Move Out Reason Saved");
  public static readonly mcsEventType stMaMoveOutReasonSaved                     = new(245, "General Move Out Reason Saved");
                                                                                                                                                         
  public static readonly mcsEventType familyCertPhaSetupSaved                    = new(246, "50058 PHA Setup Saved");
  public static readonly mcsEventType familyCertSetupScheduleTypeSaved           = new(247, "Setup Schedule Type Saved");
  public static readonly mcsEventType familyCertSetupScheduleEffectiveDateSaved  = new(248, "Setup Schedule Effective Date Saved");
  public static readonly mcsEventType mergeTemplateSaved                         = new(249, "Letter Merge Template Saved");
  public static readonly mcsEventType hipSubmissionFileSaved                     = new(250, "HIP Submission File Saved");
                                                                                                                                                         
  public static readonly mcsEventType hipSubmissionFileFormJoinSaved             = new(251, "HIP Submission File Form Join Saved");
  public static readonly mcsEventType hipSubmissionErrorSaved                    = new(252, "HIP Submission Error Saved");
  public static readonly mcsEventType finishFormConfigSaved                      = new(253, "Finish Form Config Saved");
  public static readonly mcsEventType finishFormConfigSignatureSaved             = new(254, "Finish Form Config Signature Saved");
  public static readonly mcsEventType tracsPhaSetupSaved                         = new(255, "TRACS PHA Setup Saved");
                                                                                                                                                         
  public static readonly mcsEventType insInspectionSaved                         = new(256, "Inspection Standard Inspection Saved");
  public static readonly mcsEventType insQuestionSaved                           = new(257, "Inspection Standard Question Setup Saved");
  public static readonly mcsEventType insRoomSaved                               = new(258, "Inspection Standard Room Setup Saved");
  public static readonly mcsEventType insInspectionDeficiencyWorkOrderSaved      = new(259, "Inspection Standard Deficiency Work Order Saved");
  public static readonly mcsEventType insInspectionMitigationSaved               = new(260, "Inspection Standard Mitigation Saved");
                                                                                                                                                         
  public static readonly mcsEventType stMaMassCreateBatchSaved                   = new(261, "General Cert Mass Create Batch Saved");
  public static readonly mcsEventType insFamilyNotificationSetupSaved            = new(262, "NSPIRE Family Notification Template Saved");
  public static readonly mcsEventType glPhaSetupSaved                            = new(263, "General Ledger PHA Setup Saved");
  public static readonly mcsEventType phaPortalOppBatchDetailSaved               = new(264, "Deposit Batch Detail Saved");
  public static readonly mcsEventType depositProcessed                           = new(265, "Deposit Processed");
                                                                                                                                                         
  public static readonly mcsEventType phaPortalOppBatchCompletionToggle          = new(266, "Deposit Batch Completed Toggle");
  public static readonly mcsEventType insInspectionDownloaded                    = new(267, "Inspection Standard Inspection Downloaded");
  public static readonly mcsEventType insTemplateTypeProgramSaved                = new(268, "Inspection Standard Template Type Program Saved");
  public static readonly mcsEventType familyCertProjectContractSaved             = new(269, "Family Cert Project Contract Saved");
  public static readonly mcsEventType insPhaSetupSaved                           = new(270, "NSPIRE PHA Setup Saved");
                                                                                                                                                         
  public static readonly mcsEventType phArSetupReceiptCashAccountSaved           = new(271, "T AR Setup Receipt Cash Account Saved");
  public static readonly mcsEventType phArSetupReceiptCashAccountProjectSaved    = new(272, "T AR Setup Receipt Cash Account Project Saved");
  public static readonly mcsEventType vendorContractorHoursSaved                 = new(273, "Vendor Contractor Hours Saved");
  public static readonly mcsEventType finAssetDepTemplateSaved                   = new(274, "Asset Depreciation Template Saved");
  public static readonly mcsEventType finAssetDepTemplateDetailSaved             = new(275, "Asset Depreciation Template Detail Saved");
                                                                                                                                                         
  public static readonly mcsEventType finAssetDepDistributionDetailSaved         = new(276, "Asset Depreciation Distribution Detail Saved");
  public static readonly mcsEventType phDirectDebitGroupSaved                    = new(277, "PH Direct Debit Group Saved");
  public static readonly mcsEventType phaPortalMcConversationSeen                = new(278, "Portal Conversation Seen");
  public static readonly mcsEventType wlStatusUpdateBatchSaved                   = new(279, "Waiting List Status Update Batch Saved");
  public static readonly mcsEventType wlStatusUpdateCriteriaSaved                = new(280, "Waiting List Status Update Criteria Saved");
                                                                                                                                                         
  public static readonly mcsEventType wlStatusUpdateRequestSaved                 = new(281, "Waiting List Status Update Request Saved");
  public static readonly mcsEventType stMaVoucherExtensionSaved                  = new(282, "General Cert Voucher Extension Saved");
  public static readonly mcsEventType stMaPhaSetupSaved                          = new(283, "General Cert PHA Setup Saved");
  public static readonly mcsEventType aiUnitDesignationSaved                     = new(284, "AI Unit Designation Saved");
  public static readonly mcsEventType aiSetAsideGroupSaved                       = new(285, "AI Set Aside Group Saved");
                                                                                                                                                         
  public static readonly mcsEventType aiApplicableFractionGroupSaved             = new(286, "AI Applicable Fraction Group Saved");
//  public static readonly mcsEventType phaWebsiteSetupSaved                       = new(287, "PHA Website Setup Saved");   // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static readonly mcsEventType adminPhaSetupSaved                         = new(288, "Admin Pha Setup Group Saved");
  public static readonly mcsEventType meterReadingUnitTypeSaved                  = new(289, "Meter Reading Unit Type Saved");
  public static readonly mcsEventType meterReadingUtilityEffectiveSaved          = new(290, "Meter Reading Utility Effective Saved");
                                                                                                                                                         
  public static readonly mcsEventType meterReadingUtilityTypeSaved               = new(291, "Meter Reading Utility Type Saved");
  public static readonly mcsEventType saPHASetupSaved                            = new(292, "Super Admin PHA Setup Saved");
  public static readonly mcsEventType wlStatusReasonSaved                        = new(293, "Waiting List Status Reason Saved");
  public static readonly mcsEventType fmContactSaved                             = new(294, "Family Contact Saved");
  public static readonly mcsEventType fmVehicleSaved                             = new(295, "Family Vehicle Saved");
                                                                                                                                                         
  public static readonly mcsEventType fmPetSaved                                 = new(296, "Family Pet Saved");
  public static readonly mcsEventType lockboxByProgramSaved                      = new(297, "Lockbox By Program Saved");
//  public static readonly mcsEventType glAccountLabelSaved                        = new(298, "Financial Account Label Saved");   // COMMENTED OUT in 'enum'
  public static readonly mcsEventType smInfoMessageSaved                         = new(299, "Support Info Message Saved");
  public static readonly mcsEventType certALApplicationListSaved                 = new(300, "Application List Saved");
                                                                                                                                                         
  public static readonly mcsEventType certALCredentialSaved                      = new(301, "Application List Credential Saved");
  public static readonly mcsEventType certALApplicationSaved                     = new(302, "Application List Application Saved");
  public static readonly mcsEventType glStatementSaved                           = new(303, "General Ledger Statement Saved");
  public static readonly mcsEventType glStatementGroupNodeSaved                  = new(304, "General Ledger Statement Group Node Saved");
  public static readonly mcsEventType glStatementColumnNodeSaved                 = new(305, "General Ledger Statement Column Node Saved");
                                                                                                                                                         
  public static readonly mcsEventType certALStatusSaved                          = new(306, "Application List Status Saved");
  public static readonly mcsEventType certALApplicationListStatusSaved           = new(307, "Application List Status Connection Saved");
  public static readonly mcsEventType glStatementPacketSaved                     = new(308, "General Ledger Statement Packet Saved");
  public static readonly mcsEventType poInvoiceLineItemRelationSaved             = new(309, "Purchase Order Line Item Relation Saved");
  public static readonly mcsEventType poLineItemReleaseSaved                     = new(310, "Purchase Order Line Item Release Saved");
                                                                                                                                                         
  public static readonly mcsEventType phPhaSetupSaved                            = new(311, "Tenant Accounting Pha Setup Saved");
  public static readonly mcsEventType hapMiscChargesSaved                        = new(312, "HAP Misc. Charges Saved");
  public static readonly mcsEventType programFinMiscChargesSaved                 = new(313, "Program Misc Charges Connection Saved");
  public static readonly mcsEventType phaUserGroupSaved                          = new(314, "PHA User Group Saved");
  public static readonly mcsEventType phaUserGroupLinkSaved                      = new(315, "PHA User Group Link Saved");
                                                                                                                                                         
  public static readonly mcsEventType ddDataDownloadSaved                        = new(316, "Data Download Saved");
  public static readonly mcsEventType tracsSpecialClaimSaved                     = new(317, "TRACS Special Claim Saved");
  public static readonly mcsEventType tracsSpecialClaimUnpaidRentDamagesSaved    = new(318, "TRACS Special Claim Unpaid Rent/Damages Saved");
  public static readonly mcsEventType tracsSpecialClaimVacancyDuringRentUpSaved  = new(319, "TRACS Special Claim Vacancy During Rent-Up Saved");
  public static readonly mcsEventType tracsSpecialClaimRegularVacancySaved       = new(320, "TRACS Special Claim Regular Vacancy Saved");
                                                                                                                                                         
  public static readonly mcsEventType tracsSpecialClaimDebtServiceSaved          = new(321, "TRACS Special Claim Debt Service Saved");
  public static readonly mcsEventType finDepartmentSaved                         = new(322, "Department Saved");
  public static readonly mcsEventType expirableFileSaved                         = new(323, "Expirable File Saved");
  public static readonly mcsEventType smTicketTagSaved                           = new(324, "Support Manager Ticket Tag Saved");
  
  ****************************************************************************************************************************************************************************************************/
  #endregion

  #region specific mcsEventType declarations w/nameof(field_name) ...
  /****************************************************************************************************************************************************************************************************/
  
  public static readonly mcsEventType InspectionSaved                            = new(  1, "Inspection Saved"                                          ,nameof(InspectionSaved));
  public static readonly mcsEventType InsertFamily                               = new(  2, "Family Inserted"                                           ,nameof(InsertFamily));
  public static readonly mcsEventType VacatedTenantSaved                         = new(  3, "Vacated Tenant Saved"                                      ,nameof(VacatedTenantSaved));
  public static readonly mcsEventType PmPhaSaved                                 = new(  4, "Agency Saved"                                              ,nameof(PmPhaSaved));
  public static readonly mcsEventType FamilyCertSaved                            = new(  5, "Family Cert Saved"                                         ,nameof(FamilyCertSaved));
                                                                                                                                                        
  public static readonly mcsEventType WaitingListApplicationSaved                = new(  6, "Waiting List Application Saved"                            ,nameof(WaitingListApplicationSaved));
  public static readonly mcsEventType RentReasonablenessSaved                    = new(  7, "Rent Reasonableness Saved"                                 ,nameof(RentReasonablenessSaved));
  public static readonly mcsEventType UpdateFamily                               = new(  8, "Family Updated"                                            ,nameof(UpdateFamily));
  public static readonly mcsEventType PortabilityTenantSaved                     = new(  9, "Section 8 Portability Tenant Saved"                        ,nameof(PortabilityTenantSaved));
  public static readonly mcsEventType PortabilityAdjustmentSaved                 = new( 10, "Section 8 Portability Adjustment Saved"                    ,nameof(PortabilityAdjustmentSaved));
                                                                                                                                                         
  public static readonly mcsEventType RapTrapFileGroupSaved                      = new( 11, "RAP/T-RAP File Group Saved"                                ,nameof(RapTrapFileGroupSaved));
  public static readonly mcsEventType RapTrapFormSaved                           = new( 12, "RAP/T-RAP Form Saved"                                      ,nameof(RapTrapFormSaved));
  public static readonly mcsEventType MCSImportedData                            = new( 13, "MCS Imported Data"                                         ,nameof(MCSImportedData));
  public static readonly mcsEventType PortabilityMonthlyPayablesSaved            = new( 14, "Section 8 Portability Monthly Payables Saved"              ,nameof(PortabilityMonthlyPayablesSaved));
  public static readonly mcsEventType CountySaved                                = new( 15, "County Saved"                                              ,nameof(CountySaved));
                                                                                                                                                         
  public static readonly mcsEventType PortabilityDisbusementSaved                = new( 16, "Section 8 Portability Disbursement Saved"                  ,nameof(PortabilityDisbusementSaved));
  public static readonly mcsEventType TracsSaved                                 = new( 17, "TRACS Saved"                                               ,nameof(TracsSaved));
  public static readonly mcsEventType PhaSaved                                   = new( 18, "PHA Saved"                                                 ,nameof(PhaSaved));
  public static readonly mcsEventType ChecksSaved                                = new( 19, "Checks Saved"                                              ,nameof(ChecksSaved));
  public static readonly mcsEventType gpIncomeLimitSaved                         = new( 20, "Income Limit Saved"                                        ,nameof(gpIncomeLimitSaved));
                                                                                                                                                         
  public static readonly mcsEventType MonthEndSaved                              = new( 21, "Month End Saved"                                           ,nameof(MonthEndSaved));
  public static readonly mcsEventType LandlordSaved                              = new( 22, "Landlord Saved"                                            ,nameof(LandlordSaved));
  public static readonly mcsEventType FamilyCertSetupSaved                       = new( 23, "Family Cert Setup Saved"                                   ,nameof(FamilyCertSetupSaved));
  public static readonly mcsEventType HAPContractNumberSaved                     = new( 24, "HAP Contract Number Saved"                                 ,nameof(HAPContractNumberSaved));
  public static readonly mcsEventType Section8UnitSaved                          = new( 25, "Section8 Unit Saved"                                       ,nameof(Section8UnitSaved));
                                                                                                                                                         
  public static readonly mcsEventType MCSLandlordImport                          = new( 26, "MCS Landlord Imported"                                     ,nameof(MCSLandlordImport));
  public static readonly mcsEventType ComparableUnitSaved                        = new( 27, "Comparable Unit Saved"                                     ,nameof(ComparableUnitSaved));
  public static readonly mcsEventType RequestedUnitSaved                         = new( 28, "Requested Unit Saved"                                      ,nameof(RequestedUnitSaved));
  public static readonly mcsEventType PortabilityMasterSaved                     = new( 29, "Section 8 Portability Setup Saved"                         ,nameof(PortabilityMasterSaved));
  public static readonly mcsEventType ResidentInfoSaved                          = new( 30, "Resident Information Saved"                                ,nameof(ResidentInfoSaved));
                                                                                                                                                         
  public static readonly mcsEventType ProgramSaved                               = new( 31, "Program Saved"                                             ,nameof(ProgramSaved));
  public static readonly mcsEventType ForumMessagePosted                         = new( 32, "Forum Message Posted"                                      ,nameof(ForumMessagePosted));
  public static readonly mcsEventType GlobalValueSaved                           = new( 33, "Global Value Saved"                                        ,nameof(GlobalValueSaved));
  public static readonly mcsEventType FinBankSaved                               = new( 34, "Bank Saved"                                                ,nameof(FinBankSaved));
  public static readonly mcsEventType DynamicPageSaved                           = new( 35, "Dynamic Page Saved"                                        ,nameof(DynamicPageSaved));
                                                                                                                                                         
  public static readonly mcsEventType FinAccountSaved                            = new( 36, "Account Saved"                                             ,nameof(FinAccountSaved));
  public static readonly mcsEventType LandlordAdjustmentSaved                    = new( 37, "Landlord Adjustment Saved"                                 ,nameof(LandlordAdjustmentSaved));
//  public static readonly mcsEventType                                            = new( 38, ""                                                          ,nameof());  //NOT DEFINED in 'enum'
  public static readonly mcsEventType LandlordPayablesPosted                     = new( 39, "Landlord Payables Posted"                                  ,nameof(LandlordPayablesPosted));
  public static readonly mcsEventType PublicUnitSaved                            = new( 40, "Public Unit Saved"                                         ,nameof(PublicUnitSaved));
                                                                                                                                                         
  public static readonly mcsEventType ProjectSaved                               = new( 41, "Project Saved"                                             ,nameof(ProjectSaved));
  public static readonly mcsEventType PublicBuildingSaved                        = new( 42, "Public Building Saved"                                     ,nameof(PublicBuildingSaved));
  public static readonly mcsEventType Section8BuildingSaved                      = new( 43, "Section 8 Building Saved"                                  ,nameof(Section8BuildingSaved));
  public static readonly mcsEventType PortabilityTenantRentSaved                 = new( 44, "Section 8 Portability Tenant Rent Saved"                   ,nameof(PortabilityTenantRentSaved));
  public static readonly mcsEventType tracsMAT30Saved                            = new( 45, "Tracs MAT30 Saved"                                         ,nameof(tracsMAT30Saved));
                                                                                                                                                         
  public static readonly mcsEventType tracsMonthlySubmissionFileSaved            = new( 46, "Tracs Monthly Submission File Saved"                       ,nameof(tracsMonthlySubmissionFileSaved));
  public static readonly mcsEventType FinDocumentSaved                           = new( 47, "Financial Document Saved"                                  ,nameof(FinDocumentSaved));
  public static readonly mcsEventType FinControlGroupSaved                       = new( 48, "Financial Control Group Saved"                             ,nameof(FinControlGroupSaved));
  public static readonly mcsEventType FinTransactionSaved                        = new( 49, "Financial Transaction Saved"                               ,nameof(FinTransactionSaved));
  public static readonly mcsEventType FinGlAccountSaved                          = new( 50, "Fin Gl Account Saved"                                      ,nameof(FinGlAccountSaved));
                                                                                                                                                         
  public static readonly mcsEventType FinTransactionTypeSaved                    = new( 51, "Fin Transaction Type Saved"                                ,nameof(FinTransactionTypeSaved));
//  public static readonly mcsEventType phaFileSaved                               = new( 52, "PHA File Saved"                                            ,nameof(phaFileSaved)); // COMMENTED OUT in 'enum'
  public static readonly mcsEventType MaFormSaved                                = new( 53, "MaForm Saved"                                              ,nameof(MaFormSaved));
//  public static readonly mcsEventType StMaUnitSaved                              = new( 54, "General Certification Unit Saved"                          ,nameof(StMaUnitSaved));  // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static readonly mcsEventType StMaIncomeRangeBaseSaved                   = new( 55, "StMa Income Range Base Saved"                              ,nameof(StMaIncomeRangeBaseSaved));
                                                                                                                                                         
  public static readonly mcsEventType DataExported                               = new( 56, "Data Exported"                                             ,nameof(DataExported));
//  public static readonly mcsEventType FamilyCertSubmissionFileSaved              = new( 57, "50058 Submission File"                                     ,nameof(FamilyCertSubmissionFileSaved));  // COMMENTED OUT in 'enum'
  public static readonly mcsEventType PhaUserSaved                               = new( 58, "PHA User Saved"                                            ,nameof(PhaUserSaved));
  public static readonly mcsEventType pmPhaAccountSaved                          = new( 59, "Agency Account Saved"                                      ,nameof(pmPhaAccountSaved));
  public static readonly mcsEventType GeneralLedgerJournalEntrySaved             = new( 60, "General Ledger Journal Entry Saved"                        ,nameof(GeneralLedgerJournalEntrySaved));
                                                                                                                                                         
  public static readonly mcsEventType finHoldReasonSaved                         = new( 61, "finHoldReason Saved"                                       ,nameof(finHoldReasonSaved));
  public static readonly mcsEventType finTransPartSelectionSaved                 = new( 62, "finTransactionPartSelection Saved"                         ,nameof(finTransPartSelectionSaved));
  public static readonly mcsEventType stMaSetupSaved                             = new( 63, "stMaSetup Saved"                                           ,nameof(stMaSetupSaved));
  public static readonly mcsEventType finAdminFee                                = new( 64, "finAdminFee Saved"                                         ,nameof(finAdminFee));
  public static readonly mcsEventType stMaPaymentStandardTownSaved               = new( 65, "stMaPaymentStandardTown Saved"                             ,nameof(stMaPaymentStandardTownSaved));
                                                                                                                                                         
  public static readonly mcsEventType stMaPaymentStandardBedSaved                = new( 66, "stMaPaymentStandardBed Saved"                              ,nameof(stMaPaymentStandardBedSaved));
  public static readonly mcsEventType hapScheduleAdjustmentUpdate                = new( 67, "HAP Schedule Adjustment Update"                            ,nameof(hapScheduleAdjustmentUpdate));
  public static readonly mcsEventType imRoomTypeDefinitionSaved                  = new( 68, "Inspection Manager Room Type Definition Saved"             ,nameof(imRoomTypeDefinitionSaved));
  public static readonly mcsEventType imQuestionTypeDefinitionSaved              = new( 69, "Inspection Manager Question Type Definition Saved"         ,nameof(imQuestionTypeDefinitionSaved));
  public static readonly mcsEventType imFailureTypeDefinitionSaved               = new( 70, "Inspection Manager Failure Type Definition Saved"          ,nameof(imFailureTypeDefinitionSaved));
                                                                                                                                                         
  public static readonly mcsEventType imFormTypeSaved                            = new( 71, "Inspection Manager Form Type Saved"                        ,nameof(imFormTypeSaved));
  public static readonly mcsEventType imInspectionSaved                          = new( 72, "Inspection Manager Inspection Saved"                       ,nameof(imInspectionSaved));
  public static readonly mcsEventType vendorSaved                                = new( 73, "Vendor Saved"                                              ,nameof(vendorSaved));
//  public static readonly mcsEventType distributionSaved                          = new( 74, "Distribution Saved"                                        ,nameof(distributionSaved));  // NOT FOUND in the 'eventTypeDescription's Select Case statement
//  public static readonly mcsEventType stCtUnitSaved                              = new( 75, "stCtUnitSaved"                                             ,nameof(stCtUnitSaved));  // COMMENTED OUT in 'enum'
                                                                                                                                                         
  public static readonly mcsEventType saveSignature                              = new( 76, "Signature Saved"                                           ,nameof(saveSignature));
  public static readonly mcsEventType phMiscChargesSaved                         = new( 77, "PH Misc Charges Saved"                                     ,nameof(phMiscChargesSaved));
  public static readonly mcsEventType finPaymentTermsSaved                       = new( 78, "Payment Terms Saved"                                       ,nameof(finPaymentTermsSaved));
  public static readonly mcsEventType tracsSetupSaved                            = new( 79, "TRACS Setup Saved"                                         ,nameof(tracsSetupSaved));
  public static readonly mcsEventType landlordsMerged                            = new( 80, "Landlords Merged"                                          ,nameof(landlordsMerged));
                                                                                                                                                         
  public static readonly mcsEventType finPaymentScheduleSaved                    = new( 81, "Payment Schedule Saved"                                    ,nameof(finPaymentScheduleSaved));
  public static readonly mcsEventType waitingListLotteryProcess                  = new( 82, "Waiting List Lottery Process"                              ,nameof(waitingListLotteryProcess));
  public static readonly mcsEventType recurringInvoiceSaved                      = new( 83, "Recurring Invoice Saved"                                   ,nameof(recurringInvoiceSaved));
  public static readonly mcsEventType imCustomQuestionTypeDefinitionSaved        = new( 84, "Inspection Manager Custom Question Type Definition Saved"  ,nameof(imCustomQuestionTypeDefinitionSaved));
  public static readonly mcsEventType glReportGroupSaved                         = new( 85, "General Ledger Report Group Saved"                         ,nameof(glReportGroupSaved));
                                                                                                                                                         
  public static readonly mcsEventType phRepaymentAgreementSaved                  = new( 86, "Tenant Repayment Agreement Saved"                          ,nameof(phRepaymentAgreementSaved));
  public static readonly mcsEventType FinOpenItemRelationSaved                   = new( 87, "Financial Open Item Relation Saved"                        ,nameof(FinOpenItemRelationSaved));
  public static readonly mcsEventType fmCustomValueSetupSaved                    = new( 88, "Family Custom Value Setup Saved"                           ,nameof(fmCustomValueSetupSaved));
  public static readonly mcsEventType smTicketSaved                              = new( 89, "Support Manager Ticket Saved"                              ,nameof(smTicketSaved));
  public static readonly mcsEventType smUserSaved                                = new( 90, "Support Manager User Saved"                                ,nameof(smUserSaved));
                                                                                                                                                         
  public static readonly mcsEventType woWorkOrderSaved                           = new( 91, "Work Order Saved"                                          ,nameof(woWorkOrderSaved));
  public static readonly mcsEventType woAssetSaved                               = new( 92, "Work Order Asset Saved"                                    ,nameof(woAssetSaved));
  public static readonly mcsEventType woInventorySaved                           = new( 93, "Work Order Inventory Saved"                                ,nameof(woInventorySaved));
  public static readonly mcsEventType woTaskSaved                                = new( 94, "Work Order Task Saved"                                     ,nameof(woTaskSaved));
  public static readonly mcsEventType woAssetMaintenanceSaved                    = new( 95, "Work Order Asset Maintenance Saved"                        ,nameof(woAssetMaintenanceSaved));
                                                                                                                                                         
  public static readonly mcsEventType woEmployeeAdjustmentSaved                  = new( 96, "Work Order Employee Adjustment Saved"                      ,nameof(woEmployeeAdjustmentSaved));
  public static readonly mcsEventType woInventoryAdjustmentSaved                 = new( 97, "Work Order Inventory Adjustment Saved"                     ,nameof(woInventoryAdjustmentSaved));
  public static readonly mcsEventType woSetupAssetTypeSaved                      = new( 98, "Work Order Setup Asset Type Saved"                         ,nameof(woSetupAssetTypeSaved));
  public static readonly mcsEventType woSetupNumberSaved                         = new( 99, "Work Order Setup Number Saved"                             ,nameof(woSetupNumberSaved));
  public static readonly mcsEventType woSetupUnitOfMeasureSaved                  = new(100, "Work Order Setup Unit Of Measure Saved"                    ,nameof(woSetupUnitOfMeasureSaved));
                                                                                                                                                         
  public static readonly mcsEventType woSetupInventoryTypeSaved                  = new(101, "Work Order Setup Inventory Type Saved"                     ,nameof(woSetupInventoryTypeSaved));
  public static readonly mcsEventType woSetupInventoryLocationSaved              = new(102, "Work Order Setup Inventory Location Saved"                 ,nameof(woSetupInventoryLocationSaved));
  public static readonly mcsEventType glTemplateDocSaved                         = new(103, "glTemplateDoc Saved"                                       ,nameof(glTemplateDocSaved));
  public static readonly mcsEventType woInventoryUpdateSaved                     = new(104, "Work Order Inventory Update Saved"                         ,nameof(woInventoryUpdateSaved));
  public static readonly mcsEventType glProjectGroupSaved                        = new(105, "glProjectGroup Saved"                                      ,nameof(glProjectGroupSaved));
                                                                                                                                                         
  public static readonly mcsEventType woSetupDefaultCommentsSaved                = new(106, "Work Order Setup Default Comments Saved"                   ,nameof(woSetupDefaultCommentsSaved));
  public static readonly mcsEventType rapTrapSetupSaved                          = new(107, "Rap Trap Setup Saved"                                      ,nameof(rapTrapSetupSaved));
  public static readonly mcsEventType familiesMerged                             = new(108, "Families Merged"                                           ,nameof(familiesMerged));
  public static readonly mcsEventType woSetupLaborTypeSaved                      = new(109, "Work Order Setup Labor Type Saved"                         ,nameof(woSetupLaborTypeSaved));
  public static readonly mcsEventType uaScheduleTypeSaved                        = new(110, "Utility Allowance ScheduleType Saved"                      ,nameof(uaScheduleTypeSaved));
                                                                                                                                                         
  public static readonly mcsEventType uaScheduleSaved                            = new(111, "Utility Allowance Schedule Saved"                          ,nameof(uaScheduleSaved));
  public static readonly mcsEventType uaScheduleBedSizeSaved                     = new(112, "Utility Allowance ScheduleBedSize Saved"                   ,nameof(uaScheduleBedSizeSaved));
  public static readonly mcsEventType zipGroupSaved                              = new(113, "Zip Group Saved"                                           ,nameof(zipGroupSaved));
  public static readonly mcsEventType zipGroupItemSaved                          = new(114, "Zip Group Item Saved"                                      ,nameof(zipGroupItemSaved));
  public static readonly mcsEventType poPurchaseOrderSaved                       = new(115, "Purchase Order Saved"                                      ,nameof(poPurchaseOrderSaved));
                                                                                                                                                         
  public static readonly mcsEventType poLineItemSaved                            = new(116, "Purchase Order Line Item Saved"                            ,nameof(poLineItemSaved));
  public static readonly mcsEventType woSetupReportItemSaved                     = new(117, "Work Order Setup Report Item Saved"                        ,nameof(woSetupReportItemSaved));
  public static readonly mcsEventType paymentStandardTypeSaved                   = new(118, "Payment Standard Type Saved"                               ,nameof(paymentStandardTypeSaved));
  public static readonly mcsEventType phaUserSignatureSaved                      = new(119, "Pha User Signature Saved"                                  ,nameof(phaUserSignatureSaved));
  public static readonly mcsEventType finStatementSetupSaved                     = new(120, "Financial Statement Setup Saved"                           ,nameof(finStatementSetupSaved));
                                                                                                                                                         
  public static readonly mcsEventType glPayrollReportTypeSetupSaved              = new(121, "Payroll Report Type Setup Saved"                           ,nameof(glPayrollReportTypeSetupSaved));
  public static readonly mcsEventType glPayrollTypeSetupSaved                    = new(122, "Payroll Type Setup Saved"                                  ,nameof(glPayrollTypeSetupSaved));
  public static readonly mcsEventType glPayrollSummarySaved                      = new(123, "Payroll Summary Saved"                                     ,nameof(glPayrollSummarySaved));
  public static readonly mcsEventType glPayrollDistributionSaved                 = new(124, "Payroll Distribution Saved"                                ,nameof(glPayrollDistributionSaved));
  public static readonly mcsEventType glPayrollEmployeeDistributionSaved         = new(125, "Payroll Employee Distribution Saved"                       ,nameof(glPayrollEmployeeDistributionSaved));
                                                                                                                                                         
  public static readonly mcsEventType vmsTypeSaved                               = new(126, "VMS Type Saved"                                            ,nameof(vmsTypeSaved));
  public static readonly mcsEventType glPayrollDistributionFieldNumberSaved      = new(127, "Payroll Distribution Field Number Saved"                   ,nameof(glPayrollDistributionFieldNumberSaved));
  public static readonly mcsEventType meterReadingSaved                          = new(128, "Meter Reading Saved"                                       ,nameof(meterReadingSaved));
  public static readonly mcsEventType vmsEffectiveDateSaved                      = new(129, "VMS Effective Date Saved"                                  ,nameof(vmsEffectiveDateSaved));
  public static readonly mcsEventType phHoEffectiveAdjustmentsSaved              = new(130, "PH Homeownership Effective Adjustments"                    ,nameof(phHoEffectiveAdjustmentsSaved));
                                                                                                                                                         
  public static readonly mcsEventType phHoAdjustmentsToBalanceSaved              = new(131, "PH Homeownership Adjustments to Balance"                   ,nameof(phHoAdjustmentsToBalanceSaved));
  public static readonly mcsEventType wlStatusSaved                              = new(132, "WL Status Saved"                                           ,nameof(wlStatusSaved));
  public static readonly mcsEventType glJESTemplateSaved                         = new(133, "GL Journal Entry Simple Template Saved"                    ,nameof(glJESTemplateSaved));
  public static readonly mcsEventType glJESTemplateDetailSaved                   = new(134, "GL Journal Entry Simple Template Detail Saved"             ,nameof(glJESTemplateDetailSaved));
  public static readonly mcsEventType voidedCheckSaved                           = new(135, "Voided Check Saved"                                        ,nameof(voidedCheckSaved));
                                                                                                                                                         
  public static readonly mcsEventType recertPacketSetupSaved                     = new(136, "Recertification Packet Setup Saved"                        ,nameof(recertPacketSetupSaved));
  public static readonly mcsEventType rfParticipatingProgramSaved                = new(137, "RF Participating Program Saved"                            ,nameof(rfParticipatingProgramSaved));
  public static readonly mcsEventType woReportItemSaved                          = new(138, "Work Order Report Item Saved"                              ,nameof(woReportItemSaved));
  public static readonly mcsEventType projectLookupCodeSaved                     = new(139, "Project Lookup Code Saved"                                 ,nameof(projectLookupCodeSaved));
  public static readonly mcsEventType payeeSaved                                 = new(140, "Payee Saved"                                               ,nameof(payeeSaved));
                                                                                                                                                         
  public static readonly mcsEventType payeeTemplateSaved                         = new(141, "Payee Template Saved"                                      ,nameof(payeeTemplateSaved));
  public static readonly mcsEventType familyCertSubmissionErrorSaved             = new(142, "Family Cert Submission Error Saved"                        ,nameof(familyCertSubmissionErrorSaved));
//  public static readonly mcsEventType orderingProductSaved                       = new(143, "Forms Ordering Product Saved"                            ,nameof(orderingProductSaved));  // COMMENTED OUT in 'enum'
//  public static readonly mcsEventType orderingOrderSaved                         = new(144, "Forms Ordering Order Saved"                              ,nameof(orderingOrderSaved));  // COMMENTED OUT in 'enum'
//  public static readonly mcsEventType orderingOrderDetailsSaved                  = new(145, "Forms Ordering Order Details Saved"                      ,nameof(orderingOrderDetailsSaved));  // COMMENTED OUT in 'enum'
                                                                                                                                                         
  public static readonly mcsEventType phUtilityBillingSaved                      = new(146, "PH Utility Billing Saved"                                  ,nameof(phUtilityBillingSaved));
  public static readonly mcsEventType diFolderSaved                              = new(147, "Document Imaging Folder Saved"                             ,nameof(diFolderSaved));
  public static readonly mcsEventType diPHADocumentSaved                         = new(148, "PHA Document Saved"                                        ,nameof(diPHADocumentSaved));
  public static readonly mcsEventType diUserDocumentSaved                        = new(149, "User Document Saved"                                       ,nameof(diUserDocumentSaved));
  public static readonly mcsEventType woInventoryExpensingSetupSaved             = new(150, "Work Order Inventory Expensing Setup Saved"                ,nameof(woInventoryExpensingSetupSaved));
                                                                                                                                                         
  public static readonly mcsEventType woInventoryExpensingSaved                  = new(151, "Work Order Inventory Expensing Saved"                      ,nameof(woInventoryExpensingSaved));
  public static readonly mcsEventType fmAppointmentSaved                         = new(152, "Family Appointment Saved"                                  ,nameof(fmAppointmentSaved));
  public static readonly mcsEventType phDepositMarginSetupSaved                  = new(153, "Deposit Ticket Margin Setup Saved"                         ,nameof(phDepositMarginSetupSaved));
  public static readonly mcsEventType communityServiceDetailSaved                = new(154, "Community Service Detail Saved"                            ,nameof(communityServiceDetailSaved));
  public static readonly mcsEventType glAccountReconciliationSaved               = new(155, "Account Reconciliation Saved"                              ,nameof(glAccountReconciliationSaved));
                                                                                                                                                         
  public static readonly mcsEventType genericNoteSaved                           = new(156, "Generic Note Saved"                                        ,nameof(genericNoteSaved));
  public static readonly mcsEventType genericNoteReminderSaved                   = new(157, "Generic Note Reminder Saved"                               ,nameof(genericNoteReminderSaved));
  public static readonly mcsEventType gnNoteRemindAllSaved                       = new(158, "gnNoteRemindAll Saved"                                     ,nameof(gnNoteRemindAllSaved));
  public static readonly mcsEventType diSignatureDetailSaved                     = new(159, "Document Imaging Signature Detail Saved"                   ,nameof(diSignatureDetailSaved));
  public static readonly mcsEventType flatRentAreaTypeSaved                      = new(160, "Flat Rent Area Type Saved"                                 ,nameof(flatRentAreaTypeSaved));
                                                                                                                                                         
  public static readonly mcsEventType flatRentAreaDateSaved                      = new(161, "Flat Rent Area Date Saved"                                 ,nameof(flatRentAreaDateSaved));
  public static readonly mcsEventType tracsHistoricalSaved                       = new(162, "TRACS Historical Saved"                                    ,nameof(tracsHistoricalSaved));
  public static readonly mcsEventType annualHQSCertFormSaved                     = new(163, "Annual HQS Form Saved"                                     ,nameof(annualHQSCertFormSaved));
  public static readonly mcsEventType staticFileSaved                            = new(164, "Static File Saved"                                         ,nameof(staticFileSaved));
  public static readonly mcsEventType mspTaskSaved                               = new(165, "Multi-Step Task Saved"                                     ,nameof(mspTaskSaved));
                                                                                                                                                         
  public static readonly mcsEventType mspStepSaved                               = new(166, "Multi-Step Step Saved"                                     ,nameof(mspStepSaved));
  public static readonly mcsEventType perFormAutoAdjustmentSave                  = new(167, "Per-Form Auto Adjustment Save"                             ,nameof(perFormAutoAdjustmentSave));
  public static readonly mcsEventType wlApplicantQuestionSaved                   = new(168, "Applicant Question Saved"                                  ,nameof(wlApplicantQuestionSaved));
  public static readonly mcsEventType wlApplicantFullAppAnswerSaved              = new(169, "Applicant Question Full App Answer Saved"                  ,nameof(wlApplicantFullAppAnswerSaved));
  public static readonly mcsEventType ocOnlineClassPHAUserSaved                  = new(170, "Online Class PHA User Saved"                               ,nameof(ocOnlineClassPHAUserSaved));
                                                                                                                                                         
  public static readonly mcsEventType repaymentAgreementTypeSaved                = new(171, "Repayment Agreement Type Saved"                            ,nameof(repaymentAgreementTypeSaved));
  public static readonly mcsEventType fairMarketRentAreaTypeSaved                = new(172, "Fair Market Rent Area Saved"                               ,nameof(fairMarketRentAreaTypeSaved));
  public static readonly mcsEventType fairMarketRentAmountSaved                  = new(173, "Fair Market Rent Amount Saved"                             ,nameof(fairMarketRentAmountSaved));
  public static readonly mcsEventType prRequisitionSaved                         = new(174, "Purchase Requisition Saved"                                ,nameof(prRequisitionSaved));
  public static readonly mcsEventType poShippingAddressSaved                     = new(175, "Purchase Order Shipping Address Saved"                     ,nameof(poShippingAddressSaved));
                                                                                                                                                         
  public static readonly mcsEventType prApprovalSetupSaved                       = new(176, "PR Approval Setup Saved"                                   ,nameof(prApprovalSetupSaved));
  public static readonly mcsEventType prApprovalCostPhaUserSaved                 = new(177, "PR Approval Cost Pha User Saved"                           ,nameof(prApprovalCostPhaUserSaved));
  public static readonly mcsEventType prApprovalSaved                            = new(178, "PR Approval Saved"                                         ,nameof(prApprovalSaved));
  public static readonly mcsEventType tracsRepAgreementLinkSaved                 = new(179, "Repayment Agreement Link Saved"                            ,nameof(tracsRepAgreementLinkSaved));
  public static readonly mcsEventType fssBalanceAdjustmentSaved                  = new(180, "FSS Balance Adjustment Saved"                              ,nameof(fssBalanceAdjustmentSaved));
                                                                                                                                                         
  public static readonly mcsEventType vendorContractSaved                        = new(181, "Vendor Contract Saved"                                     ,nameof(vendorContractSaved));
//  public static readonly mcsEventType familyNotificationSetupSaved               = new(182, "Family Notification Setup Saved"                           ,nameof(familyNotificationSetupSaved)); // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static readonly mcsEventType familyCertMasterVoucherExtensionSaved      = new(183, "Family Cert Voucher Extension Saved"                       ,nameof(familyCertMasterVoucherExtensionSaved));
  public static readonly mcsEventType apSelectForPayementSaved                   = new(184, "AP Select For Payment Saved"                               ,nameof(apSelectForPayementSaved));
  public static readonly mcsEventType familyCertContractRentIncreaseSaved        = new(185, "Family Cert Contract Rent Increase"                        ,nameof(familyCertContractRentIncreaseSaved));
                                                                                                                                                         
  public static readonly mcsEventType backgroundCheckRequestSaved                = new(186, "Background Check Request Saved"                            ,nameof(backgroundCheckRequestSaved));
  public static readonly mcsEventType fmPublicSafetyIncidentSaved                = new(187, "Public Safety Incident Saved"                              ,nameof(fmPublicSafetyIncidentSaved));
  public static readonly mcsEventType update1099BatchSaved                       = new(188, "Update 1099 Batch Saved"                                   ,nameof(update1099BatchSaved));
  public static readonly mcsEventType insert1099BatchSaved                       = new(189, "Insert 1099 Batch Saved"                                   ,nameof(insert1099BatchSaved));
  public static readonly mcsEventType woBillingPosted                            = new(190, "Post Work Order Billing"                                   ,nameof(woBillingPosted));
                                                                                                                                                         
  public static readonly mcsEventType fssItspGoalSaved                           = new(191, "FSS ITSP Goal Saved"                                       ,nameof(fssItspGoalSaved));
  public static readonly mcsEventType fssItspNoteSaved                           = new(192, "FSS ITSP Note Saved"                                       ,nameof(fssItspNoteSaved));
  public static readonly mcsEventType certNotificationSaved                      = new(193, "Notification Saved"                                        ,nameof(certNotificationSaved));
  public static readonly mcsEventType certNotificationProcessSaved               = new(194, "Notification Process Saved"                                ,nameof(certNotificationProcessSaved));
  public static readonly mcsEventType finUtilityDistributionSaved                = new(195, "Utility Distribution Saved"                                ,nameof(finUtilityDistributionSaved));
                                                                                                                                                         
  public static readonly mcsEventType communityServiceRecurringSaved             = new(196, "Community Service Recurring Saved"                         ,nameof(communityServiceRecurringSaved));
  public static readonly mcsEventType noteTypeSaved                              = new(197, "Note Type Saved"                                           ,nameof(noteTypeSaved));
  public static readonly mcsEventType phaPortalSetupSaved                        = new(198, "Portal Management Setup Saved"                             ,nameof(phaPortalSetupSaved));
  public static readonly mcsEventType phaPortalFamilyOptionsSaved                = new(199, "Portal Family Options Saved"                               ,nameof(phaPortalFamilyOptionsSaved));
//  public static readonly mcsEventType phaFamilyPortalPermissionSetup             = new(200, "Family Portal Permission Setup"                            ,nameof(phaFamilyPortalPermissionSetup));  // COMMENTED OUT in 'enum'
                                                                                                                                                         
  public static readonly mcsEventType finTransPartSelectionByProjectSaved        = new(201, "finTransactionPartSelectionByProject Saved"                ,nameof(finTransPartSelectionByProjectSaved));
  public static readonly mcsEventType glExportFileSaved                          = new(202, "GL Export File Saved"                                      ,nameof(glExportFileSaved));
  public static readonly mcsEventType famCertUtilScheduleTypeSaved               = new(203, "FamilyCertUtilityScheduleType Saved"                       ,nameof(famCertUtilScheduleTypeSaved));
  public static readonly mcsEventType famCertUtilScheduleDateSaved               = new(204, "FamilyCertUtilityScheduleDate Saved"                       ,nameof(famCertUtilScheduleDateSaved));
  public static readonly mcsEventType stMaUtilityScheduleTypeSaved               = new(205, "StMaUtilityScheduleType Saved"                             ,nameof(stMaUtilityScheduleTypeSaved));
                                                                                                                                                         
  public static readonly mcsEventType stMaUtilityScheduleDateSaved               = new(206, "StMaUtilityScheduleDate Saved"                             ,nameof(stMaUtilityScheduleDateSaved));
  public static readonly mcsEventType phaPortalOppAccSetupSaved                  = new(207, "phaPortalOppAccSetup Saved"                                ,nameof(phaPortalOppAccSetupSaved));
  public static readonly mcsEventType phaPortalOppAccUserSaved                   = new(208, "phaPortalOppAccUser Saved"                                 ,nameof(phaPortalOppAccUserSaved));
  public static readonly mcsEventType phaPortalOppCustomerSaved                  = new(209, "phaPortalOppCustomer Saved"                                ,nameof(phaPortalOppCustomerSaved));
  public static readonly mcsEventType phaPortalOppBatchSaved                     = new(210, "phaPortalOppBatch Saved"                                   ,nameof(phaPortalOppBatchSaved));
                                                                                                                                                         
  public static readonly mcsEventType phaPortalOppPaymentSaved                   = new(211, "phaPortalOppPayment Saved"                                 ,nameof(phaPortalOppPaymentSaved));
  public static readonly mcsEventType phaPortalOppPaymentDetailSaved             = new(212, "phaPortalOppPaymentDetail Saved"                           ,nameof(phaPortalOppPaymentDetailSaved));
  public static readonly mcsEventType sohaDhcdTransmissionFileSaved              = new(213, "SOHA EOHLC Transmission File Saved"                        ,nameof(sohaDhcdTransmissionFileSaved));
  public static readonly mcsEventType phaPortalMcConversationSaved               = new(214, "PortalConversation Saved"                                  ,nameof(phaPortalMcConversationSaved));
  public static readonly mcsEventType phaPortalMcAttachmentSaved                 = new(215, "PortalAttachment Saved"                                    ,nameof(phaPortalMcAttachmentSaved));
                                                                                                                                                         
  public static readonly mcsEventType phaPortalMcMessageSaved                    = new(216, "PortalMessage Saved"                                       ,nameof(phaPortalMcMessageSaved));
  public static readonly mcsEventType woSetupEmployeeScheduleSaved               = new(217, "woSetupEmployeeSchedule Saved"                             ,nameof(woSetupEmployeeScheduleSaved));
  public static readonly mcsEventType woSetupEmployeeScheduleDeSaved             = new(218, "woSetupEmployeeScheduleDe Saved"                           ,nameof(woSetupEmployeeScheduleDeSaved));
  public static readonly mcsEventType unitTurnoverSaved                          = new(219, "unitTurnover Saved"                                        ,nameof(unitTurnoverSaved));
  public static readonly mcsEventType certRequestSaved                           = new(220, "certRequest Saved"                                         ,nameof(certRequestSaved));
                                                                                                                                                         
  public static readonly mcsEventType phaPortalLandlordOptionsSetupSaved         = new(221, "Portal Landlord Options Saved"                             ,nameof(phaPortalLandlordOptionsSetupSaved));
  public static readonly mcsEventType stMaHomeRentIncomeScheduleTypeSaved        = new(222, "stMaHomeRentIncomeScheduleType Saved"                      ,nameof(stMaHomeRentIncomeScheduleTypeSaved));
  public static readonly mcsEventType stMaHomeRentIncomeScheduleDateSaved        = new(223, "stMaHomeRentIncomeScheduleDate Saved"                      ,nameof(stMaHomeRentIncomeScheduleDateSaved));
  public static readonly mcsEventType phaPortalLandlordSaved                     = new(224, "Portal Landlord Saved"                                     ,nameof(phaPortalLandlordSaved));
  public static readonly mcsEventType certFormReviewSaved                        = new(225, "Certification Form Review Saved"                           ,nameof(certFormReviewSaved));
                                                                                                                                                         
  public static readonly mcsEventType invoiceSavedAvidInvoiceImport              = new(226, "Invoice Saved From Avid Invoice Import"                    ,nameof(invoiceSavedAvidInvoiceImport));
  public static readonly mcsEventType phaPortalApplicantOptionsSaved             = new(227, "Portal Applicant Saved"                                    ,nameof(phaPortalApplicantOptionsSaved));
  public static readonly mcsEventType glTrialBalanceWildCardSaved                = new(228, "GL Trial Balance Wild Card Saved"                          ,nameof(glTrialBalanceWildCardSaved));
  public static readonly mcsEventType certFinishFormPacketSetupSaved             = new(229, "Cert Finish Form Packet Setup Saved"                       ,nameof(certFinishFormPacketSetupSaved));
  public static readonly mcsEventType familyCertChangeOfOwnershipSaved           = new(230, "Family Cert Change of Ownership"                           ,nameof(familyCertChangeOfOwnershipSaved));
                                                                                                                                                         
  public static readonly mcsEventType certSignatureRequestSaved                  = new(231, "Cert Signature Request Saved"                              ,nameof(certSignatureRequestSaved));
  public static readonly mcsEventType invoiceSavedInvoiceImport                  = new(232, "invoice Saved Invoice Import"                              ,nameof(invoiceSavedInvoiceImport));
  public static readonly mcsEventType woPhaSetupSaved                            = new(233, "work order pha setup saved"                                ,nameof(woPhaSetupSaved));
  public static readonly mcsEventType diUploadApi                                = new(234, "API Uploaded Document"                                     ,nameof(diUploadApi));
  public static readonly mcsEventType apVendorPhaSetupSaved                      = new(235, "ap vendor pha setup saved"                                 ,nameof(apVendorPhaSetupSaved));
                                                                                                                                                         
  public static readonly mcsEventType ecEmailAddressSaved                        = new(236, "ec Email Address Saved"                                    ,nameof(ecEmailAddressSaved));
  public static readonly mcsEventType woSetupPrioritySaved                       = new(237, "Work Order Setup Priority Saved"                           ,nameof(woSetupPrioritySaved));
  public static readonly mcsEventType woSetupRequestedBySaved                    = new(238, "Work Order Setup Requested By Saved"                       ,nameof(woSetupRequestedBySaved));
  public static readonly mcsEventType woSetupConfigurationSaved                  = new(239, "Work Order Setup Configuration Saved"                      ,nameof(woSetupConfigurationSaved));
  public static readonly mcsEventType familyCertMassCreateBatchSaved             = new(240, "Family Cert Mass Create Batch Saved"                       ,nameof(familyCertMassCreateBatchSaved));
                                                                                                                                                         
  public static readonly mcsEventType familyCertPaymentStandardChangeSaved       = new(241, "Family Cert Payment Standard Change Saved"                 ,nameof(familyCertPaymentStandardChangeSaved));
  public static readonly mcsEventType familyCertEndOfParticipationSaved          = new(242, "Family Cert End of Participation Saved"                    ,nameof(familyCertEndOfParticipationSaved));
  public static readonly mcsEventType glDepreciableAssetTypeSaved                = new(243, "GL Depreciable Asset Type Saved"                           ,nameof(glDepreciableAssetTypeSaved));
  public static readonly mcsEventType familyCertMoveOutReasonSaved               = new(244, "50058 Move Out Reason Saved"                               ,nameof(familyCertMoveOutReasonSaved));
  public static readonly mcsEventType stMaMoveOutReasonSaved                     = new(245, "General Move Out Reason Saved"                             ,nameof(stMaMoveOutReasonSaved));
                                                                                                                                                         
  public static readonly mcsEventType familyCertPhaSetupSaved                    = new(246, "50058 PHA Setup Saved"                                     ,nameof(familyCertPhaSetupSaved));
  public static readonly mcsEventType familyCertSetupScheduleTypeSaved           = new(247, "Setup Schedule Type Saved"                                 ,nameof(familyCertSetupScheduleTypeSaved));
  public static readonly mcsEventType familyCertSetupScheduleEffectiveDateSaved  = new(248, "Setup Schedule Effective Date Saved"                       ,nameof(familyCertSetupScheduleEffectiveDateSaved));
  public static readonly mcsEventType mergeTemplateSaved                         = new(249, "Letter Merge Template Saved"                               ,nameof(mergeTemplateSaved));
  public static readonly mcsEventType hipSubmissionFileSaved                     = new(250, "HIP Submission File Saved"                                 ,nameof(hipSubmissionFileSaved));
                                                                                                                                                         
  public static readonly mcsEventType hipSubmissionFileFormJoinSaved             = new(251, "HIP Submission File Form Join Saved"                       ,nameof(hipSubmissionFileFormJoinSaved));
  public static readonly mcsEventType hipSubmissionErrorSaved                    = new(252, "HIP Submission Error Saved"                                ,nameof(hipSubmissionErrorSaved));
  public static readonly mcsEventType finishFormConfigSaved                      = new(253, "Finish Form Config Saved"                                  ,nameof(finishFormConfigSaved));
  public static readonly mcsEventType finishFormConfigSignatureSaved             = new(254, "Finish Form Config Signature Saved"                        ,nameof(finishFormConfigSignatureSaved));
  public static readonly mcsEventType tracsPhaSetupSaved                         = new(255, "TRACS PHA Setup Saved"                                     ,nameof(tracsPhaSetupSaved));
                                                                                                                                                         
  public static readonly mcsEventType insInspectionSaved                         = new(256, "Inspection Standard Inspection Saved"                      ,nameof(insInspectionSaved));
  public static readonly mcsEventType insQuestionSaved                           = new(257, "Inspection Standard Question Setup Saved"                  ,nameof(insQuestionSaved));
  public static readonly mcsEventType insRoomSaved                               = new(258, "Inspection Standard Room Setup Saved"                      ,nameof(insRoomSaved));
  public static readonly mcsEventType insInspectionDeficiencyWorkOrderSaved      = new(259, "Inspection Standard Deficiency Work Order Saved"           ,nameof(insInspectionDeficiencyWorkOrderSaved));
  public static readonly mcsEventType insInspectionMitigationSaved               = new(260, "Inspection Standard Mitigation Saved"                      ,nameof(insInspectionMitigationSaved));
                                                                                                                                                         
  public static readonly mcsEventType stMaMassCreateBatchSaved                   = new(261, "General Cert Mass Create Batch Saved"                      ,nameof(stMaMassCreateBatchSaved));
  public static readonly mcsEventType insFamilyNotificationSetupSaved            = new(262, "NSPIRE Family Notification Template Saved"                 ,nameof(insFamilyNotificationSetupSaved));
  public static readonly mcsEventType glPhaSetupSaved                            = new(263, "General Ledger PHA Setup Saved"                            ,nameof(glPhaSetupSaved));
  public static readonly mcsEventType phaPortalOppBatchDetailSaved               = new(264, "Deposit Batch Detail Saved"                                ,nameof(phaPortalOppBatchDetailSaved));
  public static readonly mcsEventType depositProcessed                           = new(265, "Deposit Processed"                                         ,nameof(depositProcessed));
                                                                                                                                                         
  public static readonly mcsEventType phaPortalOppBatchCompletionToggle          = new(266, "Deposit Batch Completed Toggle"                            ,nameof(phaPortalOppBatchCompletionToggle));
  public static readonly mcsEventType insInspectionDownloaded                    = new(267, "Inspection Standard Inspection Downloaded"                 ,nameof(insInspectionDownloaded));
  public static readonly mcsEventType insTemplateTypeProgramSaved                = new(268, "Inspection Standard Template Type Program Saved"           ,nameof(insTemplateTypeProgramSaved));
  public static readonly mcsEventType familyCertProjectContractSaved             = new(269, "Family Cert Project Contract Saved"                        ,nameof(familyCertProjectContractSaved));
  public static readonly mcsEventType insPhaSetupSaved                           = new(270, "NSPIRE PHA Setup Saved"                                    ,nameof(insPhaSetupSaved));
                                                                                                                                                         
  public static readonly mcsEventType phArSetupReceiptCashAccountSaved           = new(271, "T AR Setup Receipt Cash Account Saved"                     ,nameof(phArSetupReceiptCashAccountSaved));
  public static readonly mcsEventType phArSetupReceiptCashAccountProjectSaved    = new(272, "T AR Setup Receipt Cash Account Project Saved"             ,nameof(phArSetupReceiptCashAccountProjectSaved));
  public static readonly mcsEventType vendorContractorHoursSaved                 = new(273, "Vendor Contractor Hours Saved"                             ,nameof(vendorContractorHoursSaved));
  public static readonly mcsEventType finAssetDepTemplateSaved                   = new(274, "Asset Depreciation Template Saved"                         ,nameof(finAssetDepTemplateSaved));
  public static readonly mcsEventType finAssetDepTemplateDetailSaved             = new(275, "Asset Depreciation Template Detail Saved"                  ,nameof(finAssetDepTemplateDetailSaved));
                                                                                                                                                         
  public static readonly mcsEventType finAssetDepDistributionDetailSaved         = new(276, "Asset Depreciation Distribution Detail Saved"              ,nameof(finAssetDepDistributionDetailSaved));
  public static readonly mcsEventType phDirectDebitGroupSaved                    = new(277, "PH Direct Debit Group Saved"                               ,nameof(phDirectDebitGroupSaved));
  public static readonly mcsEventType phaPortalMcConversationSeen                = new(278, "Portal Conversation Seen"                                  ,nameof(phaPortalMcConversationSeen));
  public static readonly mcsEventType wlStatusUpdateBatchSaved                   = new(279, "Waiting List Status Update Batch Saved"                    ,nameof(wlStatusUpdateBatchSaved));
  public static readonly mcsEventType wlStatusUpdateCriteriaSaved                = new(280, "Waiting List Status Update Criteria Saved"                 ,nameof(wlStatusUpdateCriteriaSaved));
                                                                                                                                                         
  public static readonly mcsEventType wlStatusUpdateRequestSaved                 = new(281, "Waiting List Status Update Request Saved"                  ,nameof(wlStatusUpdateRequestSaved));
  public static readonly mcsEventType stMaVoucherExtensionSaved                  = new(282, "General Cert Voucher Extension Saved"                      ,nameof(stMaVoucherExtensionSaved));
  public static readonly mcsEventType stMaPhaSetupSaved                          = new(283, "General Cert PHA Setup Saved"                              ,nameof(stMaPhaSetupSaved));
  public static readonly mcsEventType aiUnitDesignationSaved                     = new(284, "AI Unit Designation Saved"                                 ,nameof(aiUnitDesignationSaved));
  public static readonly mcsEventType aiSetAsideGroupSaved                       = new(285, "AI Set Aside Group Saved"                                  ,nameof(aiSetAsideGroupSaved));
                                                                                                                                                         
  public static readonly mcsEventType aiApplicableFractionGroupSaved             = new(286, "AI Applicable Fraction Group Saved"                        ,nameof(aiApplicableFractionGroupSaved));
//  public static readonly mcsEventType phaWebsiteSetupSaved                       = new(287, "PHA Website Setup Saved"                                   ,nameof(phaWebsiteSetupSaved)); // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static readonly mcsEventType adminPhaSetupSaved                         = new(288, "Admin Pha Setup Group Saved"                               ,nameof(adminPhaSetupSaved));
  public static readonly mcsEventType meterReadingUnitTypeSaved                  = new(289, "Meter Reading Unit Type Saved"                             ,nameof(meterReadingUnitTypeSaved));
  public static readonly mcsEventType meterReadingUtilityEffectiveSaved          = new(290, "Meter Reading Utility Effective Saved"                     ,nameof(meterReadingUtilityEffectiveSaved));
                                                                                                                                                         
  public static readonly mcsEventType meterReadingUtilityTypeSaved               = new(291, "Meter Reading Utility Type Saved"                          ,nameof(meterReadingUtilityTypeSaved));
  public static readonly mcsEventType saPHASetupSaved                            = new(292, "Super Admin PHA Setup Saved"                               ,nameof(saPHASetupSaved));
  public static readonly mcsEventType wlStatusReasonSaved                        = new(293, "Waiting List Status Reason Saved"                          ,nameof(wlStatusReasonSaved));
  public static readonly mcsEventType fmContactSaved                             = new(294, "Family Contact Saved"                                      ,nameof(fmContactSaved));
  public static readonly mcsEventType fmVehicleSaved                             = new(295, "Family Vehicle Saved"                                      ,nameof(fmVehicleSaved));
                                                                                                                                                         
  public static readonly mcsEventType fmPetSaved                                 = new(296, "Family Pet Saved"                                          ,nameof(fmPetSaved));
  public static readonly mcsEventType lockboxByProgramSaved                      = new(297, "Lockbox By Program Saved"                                  ,nameof(lockboxByProgramSaved));
//  public static readonly mcsEventType glAccountLabelSaved                        = new(298, "Financial Account Label Saved"                             ,nameof(glAccountLabelSaved)); // COMMENTED OUT in 'enum'
  public static readonly mcsEventType smInfoMessageSaved                         = new(299, "Support Info Message Saved"                                ,nameof(smInfoMessageSaved));
  public static readonly mcsEventType certALApplicationListSaved                 = new(300, "Application List Saved"                                    ,nameof(certALApplicationListSaved));
                                                                                                                                                         
  public static readonly mcsEventType certALCredentialSaved                      = new(301, "Application List Credential Saved"                         ,nameof(certALCredentialSaved));
  public static readonly mcsEventType certALApplicationSaved                     = new(302, "Application List Application Saved"                        ,nameof(certALApplicationSaved));
  public static readonly mcsEventType glStatementSaved                           = new(303, "General Ledger Statement Saved"                            ,nameof(glStatementSaved));
  public static readonly mcsEventType glStatementGroupNodeSaved                  = new(304, "General Ledger Statement Group Node Saved"                 ,nameof(glStatementGroupNodeSaved));
  public static readonly mcsEventType glStatementColumnNodeSaved                 = new(305, "General Ledger Statement Column Node Saved"                ,nameof(glStatementColumnNodeSaved));
                                                                                                                                                         
  public static readonly mcsEventType certALStatusSaved                          = new(306, "Application List Status Saved"                             ,nameof(certALStatusSaved));
  public static readonly mcsEventType certALApplicationListStatusSaved           = new(307, "Application List Status Connection Saved"                  ,nameof(certALApplicationListStatusSaved));
  public static readonly mcsEventType glStatementPacketSaved                     = new(308, "General Ledger Statement Packet Saved"                     ,nameof(glStatementPacketSaved));
  public static readonly mcsEventType poInvoiceLineItemRelationSaved             = new(309, "Purchase Order Line Item Relation Saved"                   ,nameof(poInvoiceLineItemRelationSaved));
  public static readonly mcsEventType poLineItemReleaseSaved                     = new(310, "Purchase Order Line Item Release Saved"                    ,nameof(poLineItemReleaseSaved));
                                                                                                                                                         
  public static readonly mcsEventType phPhaSetupSaved                            = new(311, "Tenant Accounting Pha Setup Saved"                         ,nameof(phPhaSetupSaved));
  public static readonly mcsEventType hapMiscChargesSaved                        = new(312, "HAP Misc. Charges Saved"                                   ,nameof(hapMiscChargesSaved));
  public static readonly mcsEventType programFinMiscChargesSaved                 = new(313, "Program Misc Charges Connection Saved"                     ,nameof(programFinMiscChargesSaved));
  public static readonly mcsEventType phaUserGroupSaved                          = new(314, "PHA User Group Saved"                                      ,nameof(phaUserGroupSaved));
  public static readonly mcsEventType phaUserGroupLinkSaved                      = new(315, "PHA User Group Link Saved"                                 ,nameof(phaUserGroupLinkSaved));
                                                                                                                                                         
  public static readonly mcsEventType ddDataDownloadSaved                        = new(316, "Data Download Saved"                                       ,nameof(ddDataDownloadSaved));
  public static readonly mcsEventType tracsSpecialClaimSaved                     = new(317, "TRACS Special Claim Saved"                                 ,nameof(tracsSpecialClaimSaved));
  public static readonly mcsEventType tracsSpecialClaimUnpaidRentDamagesSaved    = new(318, "TRACS Special Claim Unpaid Rent/Damages Saved"             ,nameof(tracsSpecialClaimUnpaidRentDamagesSaved));
  public static readonly mcsEventType tracsSpecialClaimVacancyDuringRentUpSaved  = new(319, "TRACS Special Claim Vacancy During Rent-Up Saved"          ,nameof(tracsSpecialClaimVacancyDuringRentUpSaved));
  public static readonly mcsEventType tracsSpecialClaimRegularVacancySaved       = new(320, "TRACS Special Claim Regular Vacancy Saved"                 ,nameof(tracsSpecialClaimRegularVacancySaved));
                                                                                                                                                         
  public static readonly mcsEventType tracsSpecialClaimDebtServiceSaved          = new(321, "TRACS Special Claim Debt Service Saved"                    ,nameof(tracsSpecialClaimDebtServiceSaved));
  public static readonly mcsEventType finDepartmentSaved                         = new(322, "Department Saved"                                          ,nameof(finDepartmentSaved));
  public static readonly mcsEventType expirableFileSaved                         = new(323, "Expirable File Saved"                                      ,nameof(expirableFileSaved));
  public static readonly mcsEventType smTicketTagSaved                           = new(324, "Support Manager Ticket Tag Saved"                          ,nameof(smTicketTagSaved));
  
  /****************************************************************************************************************************************************************************************************/
  #endregion
}


#region (Option 1: sealed class ...)
/*************************************************************************************************/

public sealed class EventType
{
  public int ID             { get; }
  public string Description { get; }
  
  private EventType(int id, string description)
  {
    ID          = id;
    Description = description;
  }

  #region Specific EventType Declarations ...
  
  public static readonly EventType InspectionSaved                             = new(  1, "Inspection Saved");  
  public static readonly EventType InsertFamily                                = new(  2, "Family Inserted");
  public static readonly EventType VacatedTenantSaved                          = new(  3, "Vacated Tenant Saved");
  public static readonly EventType PmPhaSaved                                  = new(  4, "Agency Saved");
  public static readonly EventType FamilyCertSaved                             = new(  5, "Family Cert Saved");
  
  public static readonly EventType WaitingListApplicationSaved                 = new(  6, "Waiting List Application Saved");
  public static readonly EventType RentReasonablenessSaved                     = new(  7, "Rent Reasonableness Saved");
  public static readonly EventType UpdateFamily                                = new(  8, "Family Updated");
  public static readonly EventType PortabilityTenantSaved                      = new(  9, "Section 8 Portability Tenant Saved");
  public static readonly EventType PortabilityAdjustmentSaved                  = new( 10, "Section 8 Portability Adjustment Saved");
  
  public static readonly EventType RapTrapFileGroupSaved                       = new( 11, "RAP/T-RAP File Group Saved");
  public static readonly EventType RapTrapFormSaved                            = new( 12, "RAP/T-RAP Form Saved");
  public static readonly EventType MCSImportedData                             = new( 13, "MCS Imported Data");
  public static readonly EventType PortabilityMonthlyPayablesSaved             = new( 14, "Section 8 Portability Monthly Payables Saved");
  public static readonly EventType CountySaved                                 = new( 15, "County Saved");
  
  public static readonly EventType PortabilityDisbusementSaved                 = new( 16, "Section 8 Portability Disbursement Saved");
  public static readonly EventType TracsSaved                                  = new( 17, "TRACS Saved");
  public static readonly EventType PhaSaved                                    = new( 18, "PHA Saved");
  public static readonly EventType ChecksSaved                                 = new( 19, "Checks Saved");
  public static readonly EventType gpIncomeLimitSaved                          = new( 20, "Income Limit Saved");
  
  public static readonly EventType MonthEndSaved                               = new( 21, "Month End Saved");
  public static readonly EventType LandlordSaved                               = new( 22, "Landlord Saved");
  public static readonly EventType FamilyCertSetupSaved                        = new( 23, "Family Cert Setup Saved");
  public static readonly EventType HAPContractNumberSaved                      = new( 24, "HAP Contract Number Saved");
  public static readonly EventType Section8UnitSaved                           = new( 25, "Section8 Unit Saved");
  
  public static readonly EventType MCSLandlordImport                           = new( 26, "MCS Landlord Imported");
  public static readonly EventType ComparableUnitSaved                         = new( 27, "Comparable Unit Saved");
  public static readonly EventType RequestedUnitSaved                          = new( 28, "Requested Unit Saved");
  public static readonly EventType PortabilityMasterSaved                      = new( 29, "Section 8 Portability Setup Saved");
  public static readonly EventType ResidentInfoSaved                           = new( 30, "Resident Information Saved");
  
  public static readonly EventType ProgramSaved                                = new( 31, "Program Saved");
  public static readonly EventType ForumMessagePosted                          = new( 32, "Forum Message Posted");
  public static readonly EventType GlobalValueSaved                            = new( 33, "Global Value Saved");
  public static readonly EventType FinBankSaved                                = new( 34, "Bank Saved");
  public static readonly EventType DynamicPageSaved                            = new( 35, "Dynamic Page Saved");
  
  public static readonly EventType FinAccountSaved                             = new( 36, "Account Saved");
  public static readonly EventType LandlordAdjustmentSaved                     = new( 37, "Landlord Adjustment Saved");
//  public static readonly EventType                                             = new( 38, "");                                //NOT DEFINED in 'enum'
  public static readonly EventType LandlordPayablesPosted                      = new( 39, "Landlord Payables Posted");
  public static readonly EventType PublicUnitSaved                             = new( 40, "Public Unit Saved");
  
  public static readonly EventType ProjectSaved                                = new( 41, "Project Saved");
  public static readonly EventType PublicBuildingSaved                         = new( 42, "Public Building Saved");
  public static readonly EventType Section8BuildingSaved                       = new( 43, "Section 8 Building Saved");
  public static readonly EventType PortabilityTenantRentSaved                  = new( 44, "Section 8 Portability Tenant Rent Saved");
  public static readonly EventType tracsMAT30Saved                             = new( 45, "Tracs MAT30 Saved");
  
  public static readonly EventType tracsMonthlySubmissionFileSaved             = new( 46, "Tracs Monthly Submission File Saved");
  public static readonly EventType FinDocumentSaved                            = new( 47, "Financial Document Saved");
  public static readonly EventType FinControlGroupSaved                        = new( 48, "Financial Control Group Saved");
  public static readonly EventType FinTransactionSaved                         = new( 49, "Financial Transaction Saved");
  public static readonly EventType FinGlAccountSaved                           = new( 50, "Fin Gl Account Saved");
  
  public static readonly EventType FinTransactionTypeSaved                     = new( 51, "Fin Transaction Type Saved");
//  public static readonly EventType phaFileSaved                                = new( 52, "PHA File Saved");                  // COMMENTED OUT in 'enum'
  public static readonly EventType MaFormSaved                                 = new( 53, "MaForm Saved");
//  public static readonly EventType StMaUnitSaved                               = new( 54, "General Certification Unit Saved");  // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static readonly EventType StMaIncomeRangeBaseSaved                    = new( 55, "StMa Income Range Base Saved");
  
  public static readonly EventType DataExported                                = new( 56, "Data Exported");
//  public static readonly EventType FamilyCertSubmissionFileSaved               = new( 57, "50058 Submission File");           // COMMENTED OUT in 'enum'
  public static readonly EventType PhaUserSaved                                = new( 58, "PHA User Saved");
  public static readonly EventType pmPhaAccountSaved                           = new( 59, "Agency Account Saved");
  public static readonly EventType GeneralLedgerJournalEntrySaved              = new( 60, "General Ledger Journal Entry Saved");
  
  public static readonly EventType finHoldReasonSaved                          = new( 61, "finHoldReason Saved");
  public static readonly EventType finTransPartSelectionSaved                  = new( 62, "finTransactionPartSelection Saved");
  public static readonly EventType stMaSetupSaved                              = new( 63, "stMaSetup Saved");
  public static readonly EventType finAdminFee                                 = new( 64, "finAdminFee Saved");
  public static readonly EventType stMaPaymentStandardTownSaved                = new( 65, "stMaPaymentStandardTown Saved");
  
  public static readonly EventType stMaPaymentStandardBedSaved                 = new( 66, "stMaPaymentStandardBed Saved");
  public static readonly EventType hapScheduleAdjustmentUpdate                 = new( 67, "HAP Schedule Adjustment Update");
  public static readonly EventType imRoomTypeDefinitionSaved                   = new( 68, "Inspection Manager Room Type Definition Saved");
  public static readonly EventType imQuestionTypeDefinitionSaved               = new( 69, "Inspection Manager Question Type Definition Saved");
  public static readonly EventType imFailureTypeDefinitionSaved                = new( 70, "Inspection Manager Failure Type Definition Saved");
  
  public static readonly EventType imFormTypeSaved                             = new( 71, "Inspection Manager Form Type Saved");
  public static readonly EventType imInspectionSaved                           = new( 72, "Inspection Manager Inspection Saved");
  public static readonly EventType vendorSaved                                 = new( 73, "Vendor Saved");
//  public static readonly EventType distributionSaved                           = new( 74, "Distribution Saved");  // NOT FOUND in the 'eventTypeDescription's Select Case statement
//  public static readonly EventType stCtUnitSaved                               = new( 75, "stCtUnitSaved");       // COMMENTED OUT in 'enum'
  
  public static readonly EventType saveSignature                               = new( 76, "Signature Saved");
  public static readonly EventType phMiscChargesSaved                          = new( 77, "PH Misc Charges Saved");
  public static readonly EventType finPaymentTermsSaved                        = new( 78, "Payment Terms Saved");
  public static readonly EventType tracsSetupSaved                             = new( 79, "TRACS Setup Saved");
  public static readonly EventType landlordsMerged                             = new( 80, "Landlords Merged");
  
  public static readonly EventType finPaymentScheduleSaved                     = new( 81, "Payment Schedule Saved");
  public static readonly EventType waitingListLotteryProcess                   = new( 82, "Waiting List Lottery Process");
  public static readonly EventType recurringInvoiceSaved                       = new( 83, "Recurring Invoice Saved");
  public static readonly EventType imCustomQuestionTypeDefinitionSaved         = new( 84, "Inspection Manager Custom Question Type Definition Saved");
  public static readonly EventType glReportGroupSaved                          = new( 85, "General Ledger Report Group Saved");
  
  public static readonly EventType phRepaymentAgreementSaved                   = new( 86, "Tenant Repayment Agreement Saved");
  public static readonly EventType FinOpenItemRelationSaved                    = new( 87, "Financial Open Item Relation Saved");
  public static readonly EventType fmCustomValueSetupSaved                     = new( 88, "Family Custom Value Setup Saved");
  public static readonly EventType smTicketSaved                               = new( 89, "Support Manager Ticket Saved");
  public static readonly EventType smUserSaved                                 = new( 90, "Support Manager User Saved");
  
  public static readonly EventType woWorkOrderSaved                            = new( 91, "Work Order Saved");
  public static readonly EventType woAssetSaved                                = new( 92, "Work Order Asset Saved");
  public static readonly EventType woInventorySaved                            = new( 93, "Work Order Inventory Saved");
  public static readonly EventType woTaskSaved                                 = new( 94, "Work Order Task Saved");
  public static readonly EventType woAssetMaintenanceSaved                     = new( 95, "Work Order Asset Maintenance Saved");
  
  public static readonly EventType woEmployeeAdjustmentSaved                   = new( 96, "Work Order Employee Adjustment Saved");
  public static readonly EventType woInventoryAdjustmentSaved                  = new( 97, "Work Order Inventory Adjustment Saved");
  public static readonly EventType woSetupAssetTypeSaved                       = new( 98, "Work Order Setup Asset Type Saved");
  public static readonly EventType woSetupNumberSaved                          = new( 99, "Work Order Setup Number Saved");
  public static readonly EventType woSetupUnitOfMeasureSaved                   = new(100, "Work Order Setup Unit Of Measure Saved");
  
  public static readonly EventType woSetupInventoryTypeSaved                   = new(101, "Work Order Setup Inventory Type Saved");
  public static readonly EventType woSetupInventoryLocationSaved               = new(102, "Work Order Setup Inventory Location Saved");
  public static readonly EventType glTemplateDocSaved                          = new(103, "glTemplateDoc Saved");
  public static readonly EventType woInventoryUpdateSaved                      = new(104, "Work Order Inventory Update Saved");
  public static readonly EventType glProjectGroupSaved                         = new(105, "glProjectGroup Saved");
  
  public static readonly EventType woSetupDefaultCommentsSaved                 = new(106, "Work Order Setup Default Comments Saved");
  public static readonly EventType rapTrapSetupSaved                           = new(107, "Rap Trap Setup Saved");
  public static readonly EventType familiesMerged                              = new(108, "Families Merged");
  public static readonly EventType woSetupLaborTypeSaved                       = new(109, "Work Order Setup Labor Type Saved");
  public static readonly EventType uaScheduleTypeSaved                         = new(110, "Utility Allowance ScheduleType Saved");
  
  public static readonly EventType uaScheduleSaved                             = new(111, "Utility Allowance Schedule Saved");
  public static readonly EventType uaScheduleBedSizeSaved                      = new(112, "Utility Allowance ScheduleBedSize Saved");
  public static readonly EventType zipGroupSaved                               = new(113, "Zip Group Saved");
  public static readonly EventType zipGroupItemSaved                           = new(114, "Zip Group Item Saved");
  public static readonly EventType poPurchaseOrderSaved                        = new(115, "Purchase Order Saved");
  
  public static readonly EventType poLineItemSaved                             = new(116, "Purchase Order Line Item Saved");
  public static readonly EventType woSetupReportItemSaved                      = new(117, "Work Order Setup Report Item Saved");
  public static readonly EventType paymentStandardTypeSaved                    = new(118, "Payment Standard Type Saved");
  public static readonly EventType phaUserSignatureSaved                       = new(119, "Pha User Signature Saved");
  public static readonly EventType finStatementSetupSaved                      = new(120, "Financial Statement Setup Saved");
  
  public static readonly EventType glPayrollReportTypeSetupSaved               = new(121, "Payroll Report Type Setup Saved");
  public static readonly EventType glPayrollTypeSetupSaved                     = new(122, "Payroll Type Setup Saved");
  public static readonly EventType glPayrollSummarySaved                       = new(123, "Payroll Summary Saved");
  public static readonly EventType glPayrollDistributionSaved                  = new(124, "Payroll Distribution Saved");
  public static readonly EventType glPayrollEmployeeDistributionSaved          = new(125, "Payroll Employee Distribution Saved");
  
  public static readonly EventType vmsTypeSaved                                = new(126, "VMS Type Saved");
  public static readonly EventType glPayrollDistributionFieldNumberSaved       = new(127, "Payroll Distribution Field Number Saved");
  public static readonly EventType meterReadingSaved                           = new(128, "Meter Reading Saved");
  public static readonly EventType vmsEffectiveDateSaved                       = new(129, "VMS Effective Date Saved");
  public static readonly EventType phHoEffectiveAdjustmentsSaved               = new(130, "PH Homeownership Effective Adjustments");
  
  public static readonly EventType phHoAdjustmentsToBalanceSaved               = new(131, "PH Homeownership Adjustments to Balance");
  public static readonly EventType wlStatusSaved                               = new(132, "WL Status Saved");
  public static readonly EventType glJESTemplateSaved                          = new(133, "GL Journal Entry Simple Template Saved");
  public static readonly EventType glJESTemplateDetailSaved                    = new(134, "GL Journal Entry Simple Template Detail Saved");
  public static readonly EventType voidedCheckSaved                            = new(135, "Voided Check Saved");
  
  public static readonly EventType recertPacketSetupSaved                      = new(136, "Recertification Packet Setup Saved");
  public static readonly EventType rfParticipatingProgramSaved                 = new(137, "RF Participating Program Saved");
  public static readonly EventType woReportItemSaved                           = new(138, "Work Order Report Item Saved");
  public static readonly EventType projectLookupCodeSaved                      = new(139, "Project Lookup Code Saved");
  public static readonly EventType payeeSaved                                  = new(140, "Payee Saved");
  
  public static readonly EventType payeeTemplateSaved                          = new(141, "Payee Template Saved");
  public static readonly EventType familyCertSubmissionErrorSaved              = new(142, "Family Cert Submission Error Saved");
//  public static readonly EventType orderingProductSaved                        = new(143, "Forms Ordering Product Saved");        // COMMENTED OUT in 'enum'
//  public static readonly EventType orderingOrderSaved                          = new(144, "Forms Ordering Order Saved");          // COMMENTED OUT in 'enum'
//  public static readonly EventType orderingOrderDetailsSaved                   = new(145, "Forms Ordering Order Details Saved");  // COMMENTED OUT in 'enum'
  
  public static readonly EventType phUtilityBillingSaved                       = new(146, "PH Utility Billing Saved");
  public static readonly EventType diFolderSaved                               = new(147, "Document Imaging Folder Saved");
  public static readonly EventType diPHADocumentSaved                          = new(148, "PHA Document Saved");
  public static readonly EventType diUserDocumentSaved                         = new(149, "User Document Saved");
  public static readonly EventType woInventoryExpensingSetupSaved              = new(150, "Work Order Inventory Expensing Setup Saved");
  
  public static readonly EventType woInventoryExpensingSaved                   = new(151, "Work Order Inventory Expensing Saved");
  public static readonly EventType fmAppointmentSaved                          = new(152, "Family Appointment Saved");
  public static readonly EventType phDepositMarginSetupSaved                   = new(153, "Deposit Ticket Margin Setup Saved");
  public static readonly EventType communityServiceDetailSaved                 = new(154, "Community Service Detail Saved");
  public static readonly EventType glAccountReconciliationSaved                = new(155, "Account Reconciliation Saved");
  
  public static readonly EventType genericNoteSaved                            = new(156, "Generic Note Saved");
  public static readonly EventType genericNoteReminderSaved                    = new(157, "Generic Note Reminder Saved");
  public static readonly EventType gnNoteRemindAllSaved                        = new(158, "gnNoteRemindAll Saved");
  public static readonly EventType diSignatureDetailSaved                      = new(159, "Document Imaging Signature Detail Saved");
  public static readonly EventType flatRentAreaTypeSaved                       = new(160, "Flat Rent Area Type Saved");
  
  public static readonly EventType flatRentAreaDateSaved                       = new(161, "Flat Rent Area Date Saved");
  public static readonly EventType tracsHistoricalSaved                        = new(162, "TRACS Historical Saved");
  public static readonly EventType annualHQSCertFormSaved                      = new(163, "Annual HQS Form Saved");
  public static readonly EventType staticFileSaved                             = new(164, "Static File Saved");
  public static readonly EventType mspTaskSaved                                = new(165, "Multi-Step Task Saved");
  
  public static readonly EventType mspStepSaved                                = new(166, "Multi-Step Step Saved");
  public static readonly EventType perFormAutoAdjustmentSave                   = new(167, "Per-Form Auto Adjustment Save");
  public static readonly EventType wlApplicantQuestionSaved                    = new(168, "Applicant Question Saved");
  public static readonly EventType wlApplicantFullAppAnswerSaved               = new(169, "Applicant Question Full App Answer Saved");
  public static readonly EventType ocOnlineClassPHAUserSaved                   = new(170, "Online Class PHA User Saved");
  
  public static readonly EventType repaymentAgreementTypeSaved                 = new(171, "Repayment Agreement Type Saved");
  public static readonly EventType fairMarketRentAreaTypeSaved                 = new(172, "Fair Market Rent Area Saved");
  public static readonly EventType fairMarketRentAmountSaved                   = new(173, "Fair Market Rent Amount Saved");
  public static readonly EventType prRequisitionSaved                          = new(174, "Purchase Requisition Saved");
  public static readonly EventType poShippingAddressSaved                      = new(175, "Purchase Order Shipping Address Saved");
  
  public static readonly EventType prApprovalSetupSaved                        = new(176, "PR Approval Setup Saved");
  public static readonly EventType prApprovalCostPhaUserSaved                  = new(177, "PR Approval Cost Pha User Saved");
  public static readonly EventType prApprovalSaved                             = new(178, "PR Approval Saved");
  public static readonly EventType tracsRepAgreementLinkSaved                  = new(179, "Repayment Agreement Link Saved");
  public static readonly EventType fssBalanceAdjustmentSaved                   = new(180, "FSS Balance Adjustment Saved");
  
  public static readonly EventType vendorContractSaved                         = new(181, "Vendor Contract Saved");
//  public static readonly EventType familyNotificationSetupSaved                = new(182, "Family Notification Setup Saved"); // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static readonly EventType familyCertMasterVoucherExtensionSaved       = new(183, "Family Cert Voucher Extension Saved");
  public static readonly EventType apSelectForPayementSaved                    = new(184, "AP Select For Payment Saved");
  public static readonly EventType familyCertContractRentIncreaseSaved         = new(185, "Family Cert Contract Rent Increase");
  
  public static readonly EventType backgroundCheckRequestSaved                 = new(186, "Background Check Request Saved");
  public static readonly EventType fmPublicSafetyIncidentSaved                 = new(187, "Public Safety Incident Saved");
  public static readonly EventType update1099BatchSaved                        = new(188, "Update 1099 Batch Saved");
  public static readonly EventType insert1099BatchSaved                        = new(189, "Insert 1099 Batch Saved");
  public static readonly EventType woBillingPosted                             = new(190, "Post Work Order Billing");
  
  public static readonly EventType fssItspGoalSaved                            = new(191, "FSS ITSP Goal Saved");
  public static readonly EventType fssItspNoteSaved                            = new(192, "FSS ITSP Note Saved");
  public static readonly EventType certNotificationSaved                       = new(193, "Notification Saved");
  public static readonly EventType certNotificationProcessSaved                = new(194, "Notification Process Saved");
  public static readonly EventType finUtilityDistributionSaved                 = new(195, "Utility Distribution Saved");
  
  public static readonly EventType communityServiceRecurringSaved              = new(196, "Community Service Recurring Saved");
  public static readonly EventType noteTypeSaved                               = new(197, "Note Type Saved");
  public static readonly EventType phaPortalSetupSaved                         = new(198, "Portal Management Setup Saved");
  public static readonly EventType phaPortalFamilyOptionsSaved                 = new(199, "Portal Family Options Saved");
//  public static readonly EventType phaFamilyPortalPermissionSetup              = new(200, "Family Portal Permission Setup");  // COMMENTED OUT in 'enum'
  
  public static readonly EventType finTransPartSelectionByProjectSaved         = new(201, "finTransactionPartSelectionByProject Saved");
  public static readonly EventType glExportFileSaved                           = new(202, "GL Export File Saved");
  public static readonly EventType famCertUtilScheduleTypeSaved                = new(203, "FamilyCertUtilityScheduleType Saved");
  public static readonly EventType famCertUtilScheduleDateSaved                = new(204, "FamilyCertUtilityScheduleDate Saved");
  public static readonly EventType stMaUtilityScheduleTypeSaved                = new(205, "StMaUtilityScheduleType Saved");
  
  public static readonly EventType stMaUtilityScheduleDateSaved                = new(206, "StMaUtilityScheduleDate Saved");
  public static readonly EventType phaPortalOppAccSetupSaved                   = new(207, "phaPortalOppAccSetup Saved");
  public static readonly EventType phaPortalOppAccUserSaved                    = new(208, "phaPortalOppAccUser Saved");
  public static readonly EventType phaPortalOppCustomerSaved                   = new(209, "phaPortalOppCustomer Saved");
  public static readonly EventType phaPortalOppBatchSaved                      = new(210, "phaPortalOppBatch Saved");
  
  public static readonly EventType phaPortalOppPaymentSaved                    = new(211, "phaPortalOppPayment Saved");
  public static readonly EventType phaPortalOppPaymentDetailSaved              = new(212, "phaPortalOppPaymentDetail Saved");
  public static readonly EventType sohaDhcdTransmissionFileSaved               = new(213, "SOHA EOHLC Transmission File Saved");
  public static readonly EventType phaPortalMcConversationSaved                = new(214, "PortalConversation Saved");
  public static readonly EventType phaPortalMcAttachmentSaved                  = new(215, "PortalAttachment Saved");
  
  public static readonly EventType phaPortalMcMessageSaved                     = new(216, "PortalMessage Saved");
  public static readonly EventType woSetupEmployeeScheduleSaved                = new(217, "woSetupEmployeeSchedule Saved");
  public static readonly EventType woSetupEmployeeScheduleDeSaved              = new(218, "woSetupEmployeeScheduleDe Saved");
  public static readonly EventType unitTurnoverSaved                           = new(219, "unitTurnover Saved");
  public static readonly EventType certRequestSaved                            = new(220, "certRequest Saved");
  
  public static readonly EventType phaPortalLandlordOptionsSetupSaved          = new(221, "Portal Landlord Options Saved");
  public static readonly EventType stMaHomeRentIncomeScheduleTypeSaved         = new(222, "stMaHomeRentIncomeScheduleType Saved");
  public static readonly EventType stMaHomeRentIncomeScheduleDateSaved         = new(223, "stMaHomeRentIncomeScheduleDate Saved");
  public static readonly EventType phaPortalLandlordSaved                      = new(224, "Portal Landlord Saved");
  public static readonly EventType certFormReviewSaved                         = new(225, "Certification Form Review Saved");
  
  public static readonly EventType invoiceSavedAvidInvoiceImport               = new(226, "Invoice Saved From Avid Invoice Import");
  public static readonly EventType phaPortalApplicantOptionsSaved              = new(227, "Portal Applicant Saved");
  public static readonly EventType glTrialBalanceWildCardSaved                 = new(228, "GL Trial Balance Wild Card Saved");
  public static readonly EventType certFinishFormPacketSetupSaved              = new(229, "Cert Finish Form Packet Setup Saved");
  public static readonly EventType familyCertChangeOfOwnershipSaved            = new(230, "Family Cert Change of Ownership");
  
  public static readonly EventType certSignatureRequestSaved                   = new(231, "Cert Signature Request Saved");
  public static readonly EventType invoiceSavedInvoiceImport                   = new(232, "invoice Saved Invoice Import");
  public static readonly EventType woPhaSetupSaved                             = new(233, "work order pha setup saved");
  public static readonly EventType diUploadApi                                 = new(234, "API Uploaded Document");
  public static readonly EventType apVendorPhaSetupSaved                       = new(235, "ap vendor pha setup saved");
  
  public static readonly EventType ecEmailAddressSaved                         = new(236, "ec Email Address Saved");
  public static readonly EventType woSetupPrioritySaved                        = new(237, "Work Order Setup Priority Saved");
  public static readonly EventType woSetupRequestedBySaved                     = new(238, "Work Order Setup Requested By Saved");
  public static readonly EventType woSetupConfigurationSaved                   = new(239, "Work Order Setup Configuration Saved");
  public static readonly EventType familyCertMassCreateBatchSaved              = new(240, "Family Cert Mass Create Batch Saved");
  
  public static readonly EventType familyCertPaymentStandardChangeSaved        = new(241, "Family Cert Payment Standard Change Saved");
  public static readonly EventType familyCertEndOfParticipationSaved           = new(242, "Family Cert End of Participation Saved");
  public static readonly EventType glDepreciableAssetTypeSaved                 = new(243, "GL Depreciable Asset Type Saved");
  public static readonly EventType familyCertMoveOutReasonSaved                = new(244, "50058 Move Out Reason Saved");
  public static readonly EventType stMaMoveOutReasonSaved                      = new(245, "General Move Out Reason Saved");
  
  public static readonly EventType familyCertPhaSetupSaved                     = new(246, "50058 PHA Setup Saved");
  public static readonly EventType familyCertSetupScheduleTypeSaved            = new(247, "Setup Schedule Type Saved");
  public static readonly EventType familyCertSetupScheduleEffectiveDateSaved   = new(248, "Setup Schedule Effective Date Saved");
  public static readonly EventType mergeTemplateSaved                          = new(249, "Letter Merge Template Saved");
  public static readonly EventType hipSubmissionFileSaved                      = new(250, "HIP Submission File Saved");
  
  public static readonly EventType hipSubmissionFileFormJoinSaved              = new(251, "HIP Submission File Form Join Saved");
  public static readonly EventType hipSubmissionErrorSaved                     = new(252, "HIP Submission Error Saved");
  public static readonly EventType finishFormConfigSaved                       = new(253, "Finish Form Config Saved");
  public static readonly EventType finishFormConfigSignatureSaved              = new(254, "Finish Form Config Signature Saved");
  public static readonly EventType tracsPhaSetupSaved                          = new(255, "TRACS PHA Setup Saved");
  
  public static readonly EventType insInspectionSaved                          = new(256, "Inspection Standard Inspection Saved");
  public static readonly EventType insQuestionSaved                            = new(257, "Inspection Standard Question Setup Saved");
  public static readonly EventType insRoomSaved                                = new(258, "Inspection Standard Room Setup Saved");
  public static readonly EventType insInspectionDeficiencyWorkOrderSaved       = new(259, "Inspection Standard Deficiency Work Order Saved");
  public static readonly EventType insInspectionMitigationSaved                = new(260, "Inspection Standard Mitigation Saved");
  
  public static readonly EventType stMaMassCreateBatchSaved                    = new(261, "General Cert Mass Create Batch Saved");
  public static readonly EventType insFamilyNotificationSetupSaved             = new(262, "NSPIRE Family Notification Template Saved");
  public static readonly EventType glPhaSetupSaved                             = new(263, "General Ledger PHA Setup Saved");
  public static readonly EventType phaPortalOppBatchDetailSaved                = new(264, "Deposit Batch Detail Saved");
  public static readonly EventType depositProcessed                            = new(265, "Deposit Processed");
  
  public static readonly EventType phaPortalOppBatchCompletionToggle           = new(266, "Deposit Batch Completed Toggle");
  public static readonly EventType insInspectionDownloaded                     = new(267, "Inspection Standard Inspection Downloaded");
  public static readonly EventType insTemplateTypeProgramSaved                 = new(268, "Inspection Standard Template Type Program Saved");
  public static readonly EventType familyCertProjectContractSaved              = new(269, "Family Cert Project Contract Saved");
  public static readonly EventType insPhaSetupSaved                            = new(270, "NSPIRE PHA Setup Saved");
  
  public static readonly EventType phArSetupReceiptCashAccountSaved            = new(271, "T AR Setup Receipt Cash Account Saved");
  public static readonly EventType phArSetupReceiptCashAccountProjectSaved     = new(272, "T AR Setup Receipt Cash Account Project Saved");
  public static readonly EventType vendorContractorHoursSaved                  = new(273, "Vendor Contractor Hours Saved");
  public static readonly EventType finAssetDepTemplateSaved                    = new(274, "Asset Depreciation Template Saved");
  public static readonly EventType finAssetDepTemplateDetailSaved              = new(275, "Asset Depreciation Template Detail Saved");
  
  public static readonly EventType finAssetDepDistributionDetailSaved          = new(276, "Asset Depreciation Distribution Detail Saved");
  public static readonly EventType phDirectDebitGroupSaved                     = new(277, "PH Direct Debit Group Saved");
  public static readonly EventType phaPortalMcConversationSeen                 = new(278, "Portal Conversation Seen");
  public static readonly EventType wlStatusUpdateBatchSaved                    = new(279, "Waiting List Status Update Batch Saved");
  public static readonly EventType wlStatusUpdateCriteriaSaved                 = new(280, "Waiting List Status Update Criteria Saved");
  
  public static readonly EventType wlStatusUpdateRequestSaved                  = new(281, "Waiting List Status Update Request Saved");
  public static readonly EventType stMaVoucherExtensionSaved                   = new(282, "General Cert Voucher Extension Saved");
  public static readonly EventType stMaPhaSetupSaved                           = new(283, "General Cert PHA Setup Saved");
  public static readonly EventType aiUnitDesignationSaved                      = new(284, "AI Unit Designation Saved");
  public static readonly EventType aiSetAsideGroupSaved                        = new(285, "AI Set Aside Group Saved");
  
  public static readonly EventType aiApplicableFractionGroupSaved              = new(286, "AI Applicable Fraction Group Saved");
//  public static readonly EventType phaWebsiteSetupSaved                        = new(287, "PHA Website Setup Saved"); // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static readonly EventType adminPhaSetupSaved                          = new(288, "Admin Pha Setup Group Saved");
  public static readonly EventType meterReadingUnitTypeSaved                   = new(289, "Meter Reading Unit Type Saved");
  public static readonly EventType meterReadingUtilityEffectiveSaved           = new(290, "Meter Reading Utility Effective Saved");
  
  public static readonly EventType meterReadingUtilityTypeSaved                = new(291, "Meter Reading Utility Type Saved");
  public static readonly EventType saPHASetupSaved                             = new(292, "Super Admin PHA Setup Saved");
  public static readonly EventType wlStatusReasonSaved                         = new(293, "Waiting List Status Reason Saved");
  public static readonly EventType fmContactSaved                              = new(294, "Family Contact Saved");
  public static readonly EventType fmVehicleSaved                              = new(295, "Family Vehicle Saved");
  
  public static readonly EventType fmPetSaved                                  = new(296, "Family Pet Saved");
  public static readonly EventType lockboxByProgramSaved                       = new(297, "Lockbox By Program Saved");
//  public static readonly EventType glAccountLabelSaved                         = new(298, "Financial Account Label Saved"); // COMMENTED OUT in 'enum'
  public static readonly EventType smInfoMessageSaved                          = new(299, "Support Info Message Saved");
  public static readonly EventType certALApplicationListSaved                  = new(300, "Application List Saved");
  
  public static readonly EventType certALCredentialSaved                       = new(301, "Application List Credential Saved");
  public static readonly EventType certALApplicationSaved                      = new(302, "Application List Application Saved");
  public static readonly EventType glStatementSaved                            = new(303, "General Ledger Statement Saved");
  public static readonly EventType glStatementGroupNodeSaved                   = new(304, "General Ledger Statement Group Node Saved");
  public static readonly EventType glStatementColumnNodeSaved                  = new(305, "General Ledger Statement Column Node Saved");
  
  public static readonly EventType certALStatusSaved                           = new(306, "Application List Status Saved");
  public static readonly EventType certALApplicationListStatusSaved            = new(307, "Application List Status Connection Saved");
  public static readonly EventType glStatementPacketSaved                      = new(308, "General Ledger Statement Packet Saved");
  public static readonly EventType poInvoiceLineItemRelationSaved              = new(309, "Purchase Order Line Item Relation Saved");
  public static readonly EventType poLineItemReleaseSaved                      = new(310, "Purchase Order Line Item Release Saved");
  
  public static readonly EventType phPhaSetupSaved                             = new(311, "Tenant Accounting Pha Setup Saved");
  public static readonly EventType hapMiscChargesSaved                         = new(312, "HAP Misc. Charges Saved");
  public static readonly EventType programFinMiscChargesSaved                  = new(313, "Program Misc Charges Connection Saved");
  public static readonly EventType phaUserGroupSaved                           = new(314, "PHA User Group Saved");
  public static readonly EventType phaUserGroupLinkSaved                       = new(315, "PHA User Group Link Saved");
  
  public static readonly EventType ddDataDownloadSaved                         = new(316, "Data Download Saved");
  public static readonly EventType tracsSpecialClaimSaved                      = new(317, "TRACS Special Claim Saved");
  public static readonly EventType tracsSpecialClaimUnpaidRentDamagesSaved     = new(318, "TRACS Special Claim Unpaid Rent/Damages Saved");
  public static readonly EventType tracsSpecialClaimVacancyDuringRentUpSaved   = new(319, "TRACS Special Claim Vacancy During Rent-Up Saved");
  public static readonly EventType tracsSpecialClaimRegularVacancySaved        = new(320, "TRACS Special Claim Regular Vacancy Saved");
  
  public static readonly EventType tracsSpecialClaimDebtServiceSaved           = new(321, "TRACS Special Claim Debt Service Saved");
  public static readonly EventType finDepartmentSaved                          = new(322, "Department Saved");
  public static readonly EventType expirableFileSaved                          = new(323, "Expirable File Saved");
  public static readonly EventType smTicketTagSaved                            = new(324, "Support Manager Ticket Tag Saved");
  
  #endregion
  
  #region Helper Method(s) ...
  
  // IMPORTANT: make sure ALL EventTypes are defined above, within the "Specific EventType Declarations" region.  IF NOT,
  //            then some EvenTypes "may" be left out of their associated dictionary ...
  
  //public static readonly IReadOnlyDictionary<int, EventType> Fields = 
  //  typeof(EventType).GetFields(BindingFlags.Public | BindingFlags.Static)
  //                   .Where(f  => f.FieldType == typeof(EventType))
  //                   .Select(f => (EventType)f.GetValue(null)!)
  //                   .ToImmutableDictionary(e => e.ID);         //NOTE: requires "using System.Collections.Immutable"
  
  private static readonly IReadOnlyDictionary<int, EventType> _fields = 
    typeof(EventType).GetFields(BindingFlags.Public | BindingFlags.Static)
                     .Where(f  => f.FieldType == typeof(EventType))
                     .Select(f => (EventType)f.GetValue(null)!)
                     .ToDictionary(e => e.ID);
   
  public static EventType GetFieldByID(int id)
    => _fields.TryGetValue(id, out var value) ? value : throw new ArgumentOutOfRangeException($"Unknown event type id: {id}");

#region Field Descriptions
//
//  private static readonly IReadOnlyDictionary<string, EventType> _field_descriptions =
//    typeof(EventType).GetFields(BindingFlags.Public | BindingFlags.Static)
//                     .Where(f  => f.FieldType == typeof(EventType))
//                     .Select(f => (EventType)f.GetValue(null)!)
//                     .ToDictionary(e => e.Description);
//
//  public static object GetAllFieldDescriptions()
//    => _field_descriptions.Values.ToList().AsReadOnly();
//
#endregion
   
  //public static IReadOnlyList<EventType> GetAllFields()
  //  => _fields.Values.ToList().AsReadOnly();
   
  public static object GetAllFields(bool asDictionary = false) 
    => asDictionary ? _fields.Values.ToList().AsReadOnly()
                    : _fields.Values.ToDictionary(v => v.ID);
 
  //NOTE: WE DO NOT WANT TO DO THIS ... since the 'index' and 'ID' are not the same
  //public EventType this[int id] 
  //  => GetFieldByID(id);
  
  //NOTE: I'm not sure if I want to do these or not.  I'm trying to figure a way to
  //      build as much 'enum' support for this 'smart-enum' ...
  // int -> EventType (explicit cast only - forces developer to think about it)
  [Obsolete("Prefer EventType.GetById(int) for clarity and future-proofing.", false)]
  public static explicit operator EventType(int id)
    => GetFieldByID(id);
    
  // EventType -> int (implicit or explicit)
  public static implicit operator int(EventType type)
    => type.ID;

  [Obsolete("Prefer EventType.event_type.Description for clarity and future-proofing.", false)]
  public static implicit operator string(EventType type)
    => type.Description;

  //public override string ToString()
  //  => $"ID: {ID}, Description: {Description}";

  public string ToString(bool asJsonObject = false)
    => !asJsonObject ? $"ID: {ID}, Description: {Description}"
                     : $"{{ID: {ID}, Description: {Description}}}";

  // Property(s) ---------------------------------------------------------------------------------------------------------------
  private static readonly Lazy<IReadOnlyDictionary<int, EventType>> _properties = new(() => 
    typeof(EventType).GetProperties(BindingFlags.Public | BindingFlags.Static)
                     .Where(p  => p.PropertyType == typeof(EventType))
                     .Select(p => (EventType)p.GetValue(null)!)
                     .ToDictionary(e => e.ID));
  
  public static IReadOnlyDictionary<int, EventType> Properties = _properties.Value;

  public static EventType GetPropertyByID(int id)
    => Properties.TryGetValue(id, out var value) ? value : throw new ArgumentOutOfRangeException($"Unknown event type id: {id}");

  #endregion

  #region enum-like equality: compare based on ID only (ignore Description for uniqueness)

  public override bool Equals(object obj)
    => obj is EventType type && Equals(type);

  public bool Equals(EventType other)
    => other is not null && ID == other.ID;

  public override int GetHashCode()
    => ID.GetHashCode();

  public static bool operator == (EventType left, EventType right)
    => left.Equals(right);
    //=> left?.ID == right?.ID;
  
  public static bool operator != (EventType left, EventType right)
    => !left.Equals(right);
    //=> left?.ID != right?.ID;
  
  #endregion

}

/*************************************************************************************************/
#endregion

#region (Option 2: sealed class ...)
/**************************************************************************************************

public sealed record EventType(int ID, string Description)
{
  #region Specific EventType Declarations ...
  
  public static EventType InspectionSaved                             { get; } = new(  1, "Inspection Saved");
  public static EventType InsertFamily                                { get; } = new(  2, "Family Inserted");
  public static EventType VacatedTenantSaved                          { get; } = new(  3, "Vacated Tenant Saved");
  public static EventType PmPhaSaved                                  { get; } = new(  4, "Agency Saved");
  public static EventType FamilyCertSaved                             { get; } = new(  5, "Family Cert Saved");
  
  public static EventType WaitingListApplicationSaved                 { get; } = new(  6, "Waiting List Application Saved");
  public static EventType RentReasonablenessSaved                     { get; } = new(  7, "Rent Reasonableness Saved");
  public static EventType UpdateFamily                                { get; } = new(  8, "Family Updated");
  public static EventType PortabilityTenantSaved                      { get; } = new(  9, "Section 8 Portability Tenant Saved");
  public static EventType PortabilityAdjustmentSaved                  { get; } = new( 10, "Section 8 Portability Adjustment Saved");
  
  public static EventType RapTrapFileGroupSaved                       { get; } = new( 11, "RAP/T-RAP File Group Saved");
  public static EventType RapTrapFormSaved                            { get; } = new( 12, "RAP/T-RAP Form Saved");
  public static EventType MCSImportedData                             { get; } = new( 13, "MCS Imported Data");
  public static EventType PortabilityMonthlyPayablesSaved             { get; } = new( 14, "Section 8 Portability Monthly Payables Saved");
  public static EventType CountySaved                                 { get; } = new( 15, "County Saved");
  
  public static EventType PortabilityDisbusementSaved                 { get; } = new( 16, "Section 8 Portability Disbursement Saved");
  public static EventType TracsSaved                                  { get; } = new( 17, "TRACS Saved");
  public static EventType PhaSaved                                    { get; } = new( 18, "PHA Saved");
  public static EventType ChecksSaved                                 { get; } = new( 19, "Checks Saved");
  public static EventType gpIncomeLimitSaved                          { get; } = new( 20, "Income Limit Saved");
  
  public static EventType MonthEndSaved                               { get; } = new( 21, "Month End Saved");
  public static EventType LandlordSaved                               { get; } = new( 22, "Landlord Saved");
  public static EventType FamilyCertSetupSaved                        { get; } = new( 23, "Family Cert Setup Saved");
  public static EventType HAPContractNumberSaved                      { get; } = new( 24, "HAP Contract Number Saved");
  public static EventType Section8UnitSaved                           { get; } = new( 25, "Section8 Unit Saved");
  
  public static EventType MCSLandlordImport                           { get; } = new( 26, "MCS Landlord Imported");
  public static EventType ComparableUnitSaved                         { get; } = new( 27, "Comparable Unit Saved");
  public static EventType RequestedUnitSaved                          { get; } = new( 28, "Requested Unit Saved");
  public static EventType PortabilityMasterSaved                      { get; } = new( 29, "Section 8 Portability Setup Saved");
  public static EventType ResidentInfoSaved                           { get; } = new( 30, "Resident Information Saved");
  
  public static EventType ProgramSaved                                { get; } = new( 31, "Program Saved");
  public static EventType ForumMessagePosted                          { get; } = new( 32, "Forum Message Posted");
  public static EventType GlobalValueSaved                            { get; } = new( 33, "Global Value Saved");
  public static EventType FinBankSaved                                { get; } = new( 34, "Bank Saved");
  public static EventType DynamicPageSaved                            { get; } = new( 35, "Dynamic Page Saved");
  
  public static EventType FinAccountSaved                             { get; } = new( 36, "Account Saved");
  public static EventType LandlordAdjustmentSaved                     { get; } = new( 37, "Landlord Adjustment Saved");
//  public static EventType                                             { get; } = new( 38, "");                                //NOT DEFINED in 'enum'
  public static EventType LandlordPayablesPosted                      { get; } = new( 39, "Landlord Payables Posted");
  public static EventType PublicUnitSaved                             { get; } = new( 40, "Public Unit Saved");
  
  public static EventType ProjectSaved                                { get; } = new( 41, "Project Saved");
  public static EventType PublicBuildingSaved                         { get; } = new( 42, "Public Building Saved");
  public static EventType Section8BuildingSaved                       { get; } = new( 43, "Section 8 Building Saved");
  public static EventType PortabilityTenantRentSaved                  { get; } = new( 44, "Section 8 Portability Tenant Rent Saved");
  public static EventType tracsMAT30Saved                             { get; } = new( 45, "Tracs MAT30 Saved");
  
  public static EventType tracsMonthlySubmissionFileSaved             { get; } = new( 46, "Tracs Monthly Submission File Saved");
  public static EventType FinDocumentSaved                            { get; } = new( 47, "Financial Document Saved");
  public static EventType FinControlGroupSaved                        { get; } = new( 48, "Financial Control Group Saved");
  public static EventType FinTransactionSaved                         { get; } = new( 49, "Financial Transaction Saved");
  public static EventType FinGlAccountSaved                           { get; } = new( 50, "Fin Gl Account Saved");
  
  public static EventType FinTransactionTypeSaved                     { get; } = new( 51, "Fin Transaction Type Saved");
//  public static EventType phaFileSaved                                { get; } = new( 52, "PHA File Saved");                  // COMMENTED OUT in 'enum'
  public static EventType MaFormSaved                                 { get; } = new( 53, "MaForm Saved");
  public static EventType StMaUnitSaved                               { get; } = new( 54, "General Certification Unit Saved");  // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static EventType StMaIncomeRangeBaseSaved                    { get; } = new( 55, "StMa Income Range Base Saved");
  
  public static EventType DataExported                                { get; } = new( 56, "Data Exported");
//  public static EventType FamilyCertSubmissionFileSaved               { get; } = new( 57, "50058 Submission File");           // COMMENTED OUT in 'enum'
  public static EventType PhaUserSaved                                { get; } = new( 58, "PHA User Saved");
  public static EventType pmPhaAccountSaved                           { get; } = new( 59, "Agency Account Saved");
  public static EventType GeneralLedgerJournalEntrySaved              { get; } = new( 60, "General Ledger Journal Entry Saved");
  
  public static EventType finHoldReasonSaved                          { get; } = new( 61, "finHoldReason Saved");
  public static EventType finTransPartSelectionSaved                  { get; } = new( 62, "finTransactionPartSelection Saved");
  public static EventType stMaSetupSaved                              { get; } = new( 63, "stMaSetup Saved");
  public static EventType finAdminFee                                 { get; } = new( 64, "finAdminFee Saved");
  public static EventType stMaPaymentStandardTownSaved                { get; } = new( 65, "stMaPaymentStandardTown Saved");
  
  public static EventType stMaPaymentStandardBedSaved                 { get; } = new( 66, "stMaPaymentStandardBed Saved");
  public static EventType hapScheduleAdjustmentUpdate                 { get; } = new( 67, "HAP Schedule Adjustment Update");
  public static EventType imRoomTypeDefinitionSaved                   { get; } = new( 68, "Inspection Manager Room Type Definition Saved");
  public static EventType imQuestionTypeDefinitionSaved               { get; } = new( 69, "Inspection Manager Question Type Definition Saved");
  public static EventType imFailureTypeDefinitionSaved                { get; } = new( 70, "Inspection Manager Failure Type Definition Saved");
  
  public static EventType imFormTypeSaved                             { get; } = new( 71, "Inspection Manager Form Type Saved");
  public static EventType imInspectionSaved                           { get; } = new( 72, "Inspection Manager Inspection Saved");
  public static EventType vendorSaved                                 { get; } = new( 73, "Vendor Saved");
//  public static EventType distributionSaved                           { get; } = new( 74, "Distribution Saved");  // NOT FOUND in the 'eventTypeDescription's Select Case statement
//  public static EventType stCtUnitSaved                               { get; } = new( 75, "stCtUnitSaved");       // COMMENTED OUT in 'enum'
  
  public static EventType saveSignature                               { get; } = new( 76, "Signature Saved");
  public static EventType phMiscChargesSaved                          { get; } = new( 77, "PH Misc Charges Saved");
  public static EventType finPaymentTermsSaved                        { get; } = new( 78, "Payment Terms Saved");
  public static EventType tracsSetupSaved                             { get; } = new( 79, "TRACS Setup Saved");
  public static EventType landlordsMerged                             { get; } = new( 80, "Landlords Merged");
  
  public static EventType finPaymentScheduleSaved                     { get; } = new( 81, "Payment Schedule Saved");
  public static EventType waitingListLotteryProcess                   { get; } = new( 82, "Waiting List Lottery Process");
  public static EventType recurringInvoiceSaved                       { get; } = new( 83, "Recurring Invoice Saved");
  public static EventType imCustomQuestionTypeDefinitionSaved         { get; } = new( 84, "Inspection Manager Custom Question Type Definition Saved");
  public static EventType glReportGroupSaved                          { get; } = new( 85, "General Ledger Report Group Saved");
  
  public static EventType phRepaymentAgreementSaved                   { get; } = new( 86, "Tenant Repayment Agreement Saved");
  public static EventType FinOpenItemRelationSaved                    { get; } = new( 87, "Financial Open Item Relation Saved");
  public static EventType fmCustomValueSetupSaved                     { get; } = new( 88, "Family Custom Value Setup Saved");
  public static EventType smTicketSaved                               { get; } = new( 89, "Support Manager Ticket Saved");
  public static EventType smUserSaved                                 { get; } = new( 90, "Support Manager User Saved");
  
  public static EventType woWorkOrderSaved                            { get; } = new( 91, "Work Order Saved");
  public static EventType woAssetSaved                                { get; } = new( 92, "Work Order Asset Saved");
  public static EventType woInventorySaved                            { get; } = new( 93, "Work Order Inventory Saved");
  public static EventType woTaskSaved                                 { get; } = new( 94, "Work Order Task Saved");
  public static EventType woAssetMaintenanceSaved                     { get; } = new( 95, "Work Order Asset Maintenance Saved");
  
  public static EventType woEmployeeAdjustmentSaved                   { get; } = new( 96, "Work Order Employee Adjustment Saved");
  public static EventType woInventoryAdjustmentSaved                  { get; } = new( 97, "Work Order Inventory Adjustment Saved");
  public static EventType woSetupAssetTypeSaved                       { get; } = new( 98, "Work Order Setup Asset Type Saved");
  public static EventType woSetupNumberSaved                          { get; } = new( 99, "Work Order Setup Number Saved");
  public static EventType woSetupUnitOfMeasureSaved                   { get; } = new(100, "Work Order Setup Unit Of Measure Saved");
  
  public static EventType woSetupInventoryTypeSaved                   { get; } = new(101, "Work Order Setup Inventory Type Saved");
  public static EventType woSetupInventoryLocationSaved               { get; } = new(102, "Work Order Setup Inventory Location Saved");
  public static EventType glTemplateDocSaved                          { get; } = new(103, "glTemplateDoc Saved");
  public static EventType woInventoryUpdateSaved                      { get; } = new(104, "Work Order Inventory Update Saved");
  public static EventType glProjectGroupSaved                         { get; } = new(105, "glProjectGroup Saved");
  
  public static EventType woSetupDefaultCommentsSaved                 { get; } = new(106, "Work Order Setup Default Comments Saved");
  public static EventType rapTrapSetupSaved                           { get; } = new(107, "Rap Trap Setup Saved");
  public static EventType familiesMerged                              { get; } = new(108, "Families Merged");
  public static EventType woSetupLaborTypeSaved                       { get; } = new(109, "Work Order Setup Labor Type Saved");
  public static EventType uaScheduleTypeSaved                         { get; } = new(110, "Utility Allowance ScheduleType Saved");
  
  public static EventType uaScheduleSaved                             { get; } = new(111, "Utility Allowance Schedule Saved");
  public static EventType uaScheduleBedSizeSaved                      { get; } = new(112, "Utility Allowance ScheduleBedSize Saved");
  public static EventType zipGroupSaved                               { get; } = new(113, "Zip Group Saved");
  public static EventType zipGroupItemSaved                           { get; } = new(114, "Zip Group Item Saved");
  public static EventType poPurchaseOrderSaved                        { get; } = new(115, "Purchase Order Saved");
  
  public static EventType poLineItemSaved                             { get; } = new(116, "Purchase Order Line Item Saved");
  public static EventType woSetupReportItemSaved                      { get; } = new(117, "Work Order Setup Report Item Saved");
  public static EventType paymentStandardTypeSaved                    { get; } = new(118, "Payment Standard Type Saved");
  public static EventType phaUserSignatureSaved                       { get; } = new(119, "Pha User Signature Saved");
  public static EventType finStatementSetupSaved                      { get; } = new(120, "Financial Statement Setup Saved");
  
  public static EventType glPayrollReportTypeSetupSaved               { get; } = new(121, "Payroll Report Type Setup Saved");
  public static EventType glPayrollTypeSetupSaved                     { get; } = new(122, "Payroll Type Setup Saved");
  public static EventType glPayrollSummarySaved                       { get; } = new(123, "Payroll Summary Saved");
  public static EventType glPayrollDistributionSaved                  { get; } = new(124, "Payroll Distribution Saved");
  public static EventType glPayrollEmployeeDistributionSaved          { get; } = new(125, "Payroll Employee Distribution Saved");
  
  public static EventType vmsTypeSaved                                { get; } = new(126, "VMS Type Saved");
  public static EventType glPayrollDistributionFieldNumberSaved       { get; } = new(127, "Payroll Distribution Field Number Saved");
  public static EventType meterReadingSaved                           { get; } = new(128, "Meter Reading Saved");
  public static EventType vmsEffectiveDateSaved                       { get; } = new(129, "VMS Effective Date Saved");
  public static EventType phHoEffectiveAdjustmentsSaved               { get; } = new(130, "PH Homeownership Effective Adjustments");
  
  public static EventType phHoAdjustmentsToBalanceSaved               { get; } = new(131, "PH Homeownership Adjustments to Balance");
  public static EventType wlStatusSaved                               { get; } = new(132, "WL Status Saved");
  public static EventType glJESTemplateSaved                          { get; } = new(133, "GL Journal Entry Simple Template Saved");
  public static EventType glJESTemplateDetailSaved                    { get; } = new(134, "GL Journal Entry Simple Template Detail Saved");
  public static EventType voidedCheckSaved                            { get; } = new(135, "Voided Check Saved");
  
  public static EventType recertPacketSetupSaved                      { get; } = new(136, "Recertification Packet Setup Saved");
  public static EventType rfParticipatingProgramSaved                 { get; } = new(137, "RF Participating Program Saved");
  public static EventType woReportItemSaved                           { get; } = new(138, "Work Order Report Item Saved");
  public static EventType projectLookupCodeSaved                      { get; } = new(139, "Project Lookup Code Saved");
  public static EventType payeeSaved                                  { get; } = new(140, "Payee Saved");
  
  public static EventType payeeTemplateSaved                          { get; } = new(141, "Payee Template Saved");
  public static EventType familyCertSubmissionErrorSaved              { get; } = new(142, "Family Cert Submission Error Saved");
//  public static EventType orderingProductSaved                        { get; } = new(143, "Forms Ordering Product Saved");        // COMMENTED OUT in 'enum'
//  public static EventType orderingOrderSaved                          { get; } = new(144, "Forms Ordering Order Saved");          // COMMENTED OUT in 'enum'
//  public static EventType orderingOrderDetailsSaved                   { get; } = new(145, "Forms Ordering Order Details Saved");  // COMMENTED OUT in 'enum'
  
  public static EventType phUtilityBillingSaved                       { get; } = new(146, "PH Utility Billing Saved");
  public static EventType diFolderSaved                               { get; } = new(147, "Document Imaging Folder Saved");
  public static EventType diPHADocumentSaved                          { get; } = new(148, "PHA Document Saved");
  public static EventType diUserDocumentSaved                         { get; } = new(149, "User Document Saved");
  public static EventType woInventoryExpensingSetupSaved              { get; } = new(150, "Work Order Inventory Expensing Setup Saved");
  
  public static EventType woInventoryExpensingSaved                   { get; } = new(151, "Work Order Inventory Expensing Saved");
  public static EventType fmAppointmentSaved                          { get; } = new(152, "Family Appointment Saved");
  public static EventType phDepositMarginSetupSaved                   { get; } = new(153, "Deposit Ticket Margin Setup Saved");
  public static EventType communityServiceDetailSaved                 { get; } = new(154, "Community Service Detail Saved");
  public static EventType glAccountReconciliationSaved                { get; } = new(155, "Account Reconciliation Saved");
  
  public static EventType genericNoteSaved                            { get; } = new(156, "Generic Note Saved");
  public static EventType genericNoteReminderSaved                    { get; } = new(157, "Generic Note Reminder Saved");
  public static EventType gnNoteRemindAllSaved                        { get; } = new(158, "gnNoteRemindAll Saved");
  public static EventType diSignatureDetailSaved                      { get; } = new(159, "Document Imaging Signature Detail Saved");
  public static EventType flatRentAreaTypeSaved                       { get; } = new(160, "Flat Rent Area Type Saved");
  
  public static EventType flatRentAreaDateSaved                       { get; } = new(161, "Flat Rent Area Date Saved");
  public static EventType tracsHistoricalSaved                        { get; } = new(162, "TRACS Historical Saved");
  public static EventType annualHQSCertFormSaved                      { get; } = new(163, "Annual HQS Form Saved");
  public static EventType staticFileSaved                             { get; } = new(164, "Static File Saved");
  public static EventType mspTaskSaved                                { get; } = new(165, "Multi-Step Task Saved");
  
  public static EventType mspStepSaved                                { get; } = new(166, "Multi-Step Step Saved");
  public static EventType perFormAutoAdjustmentSave                   { get; } = new(167, "Per-Form Auto Adjustment Save");
  public static EventType wlApplicantQuestionSaved                    { get; } = new(168, "Applicant Question Saved");
  public static EventType wlApplicantFullAppAnswerSaved               { get; } = new(169, "Applicant Question Full App Answer Saved");
  public static EventType ocOnlineClassPHAUserSaved                   { get; } = new(170, "Online Class PHA User Saved");
  
  public static EventType repaymentAgreementTypeSaved                 { get; } = new(171, "Repayment Agreement Type Saved");
  public static EventType fairMarketRentAreaTypeSaved                 { get; } = new(172, "Fair Market Rent Area Saved");
  public static EventType fairMarketRentAmountSaved                   { get; } = new(173, "Fair Market Rent Amount Saved");
  public static EventType prRequisitionSaved                          { get; } = new(174, "Purchase Requisition Saved");
  public static EventType poShippingAddressSaved                      { get; } = new(175, "Purchase Order Shipping Address Saved");
  
  public static EventType prApprovalSetupSaved                        { get; } = new(176, "PR Approval Setup Saved");
  public static EventType prApprovalCostPhaUserSaved                  { get; } = new(177, "PR Approval Cost Pha User Saved");
  public static EventType prApprovalSaved                             { get; } = new(178, "PR Approval Saved");
  public static EventType tracsRepAgreementLinkSaved                  { get; } = new(179, "Repayment Agreement Link Saved");
  public static EventType fssBalanceAdjustmentSaved                   { get; } = new(180, "FSS Balance Adjustment Saved");
  
  public static EventType vendorContractSaved                         { get; } = new(181, "Vendor Contract Saved");
//  public static EventType familyNotificationSetupSaved                { get; } = new(182, "Family Notification Setup Saved"); // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static EventType familyCertMasterVoucherExtensionSaved       { get; } = new(183, "Family Cert Voucher Extension Saved");
  public static EventType apSelectForPayementSaved                    { get; } = new(184, "AP Select For Payment Saved");
  public static EventType familyCertContractRentIncreaseSaved         { get; } = new(185, "Family Cert Contract Rent Increase");
  
  public static EventType backgroundCheckRequestSaved                 { get; } = new(186, "Background Check Request Saved");
  public static EventType fmPublicSafetyIncidentSaved                 { get; } = new(187, "Public Safety Incident Saved");
  public static EventType update1099BatchSaved                        { get; } = new(188, "Update 1099 Batch Saved");
  public static EventType insert1099BatchSaved                        { get; } = new(189, "Insert 1099 Batch Saved");
  public static EventType woBillingPosted                             { get; } = new(190, "Post Work Order Billing");
  
  public static EventType fssItspGoalSaved                            { get; } = new(191, "FSS ITSP Goal Saved");
  public static EventType fssItspNoteSaved                            { get; } = new(192, "FSS ITSP Note Saved");
  public static EventType certNotificationSaved                       { get; } = new(193, "Notification Saved");
  public static EventType certNotificationProcessSaved                { get; } = new(194, "Notification Process Saved");
  public static EventType finUtilityDistributionSaved                 { get; } = new(195, "Utility Distribution Saved");
  
  public static EventType communityServiceRecurringSaved              { get; } = new(196, "Community Service Recurring Saved");
  public static EventType noteTypeSaved                               { get; } = new(197, "Note Type Saved");
  public static EventType phaPortalSetupSaved                         { get; } = new(198, "Portal Management Setup Saved");
  public static EventType phaPortalFamilyOptionsSaved                 { get; } = new(199, "Portal Family Options Saved");
//  public static EventType phaFamilyPortalPermissionSetup              { get; } = new(200, "Family Portal Permission Setup");  // COMMENTED OUT in 'enum'
  
  public static EventType finTransPartSelectionByProjectSaved         { get; } = new(201, "finTransactionPartSelectionByProject Saved");
  public static EventType glExportFileSaved                           { get; } = new(202, "GL Export File Saved");
  public static EventType famCertUtilScheduleTypeSaved                { get; } = new(203, "FamilyCertUtilityScheduleType Saved");
  public static EventType famCertUtilScheduleDateSaved                { get; } = new(204, "FamilyCertUtilityScheduleDate Saved");
  public static EventType stMaUtilityScheduleTypeSaved                { get; } = new(205, "StMaUtilityScheduleType Saved");
  
  public static EventType stMaUtilityScheduleDateSaved                { get; } = new(206, "StMaUtilityScheduleDate Saved");
  public static EventType phaPortalOppAccSetupSaved                   { get; } = new(207, "phaPortalOppAccSetup Saved");
  public static EventType phaPortalOppAccUserSaved                    { get; } = new(208, "phaPortalOppAccUser Saved");
  public static EventType phaPortalOppCustomerSaved                   { get; } = new(209, "phaPortalOppCustomer Saved");
  public static EventType phaPortalOppBatchSaved                      { get; } = new(210, "phaPortalOppBatch Saved");
  
  public static EventType phaPortalOppPaymentSaved                    { get; } = new(211, "phaPortalOppPayment Saved");
  public static EventType phaPortalOppPaymentDetailSaved              { get; } = new(212, "phaPortalOppPaymentDetail Saved");
  public static EventType sohaDhcdTransmissionFileSaved               { get; } = new(213, "SOHA EOHLC Transmission File Saved");
  public static EventType phaPortalMcConversationSaved                { get; } = new(214, "PortalConversation Saved");
  public static EventType phaPortalMcAttachmentSaved                  { get; } = new(215, "PortalAttachment Saved");
  
  public static EventType phaPortalMcMessageSaved                     { get; } = new(216, "PortalMessage Saved");
  public static EventType woSetupEmployeeScheduleSaved                { get; } = new(217, "woSetupEmployeeSchedule Saved");
  public static EventType woSetupEmployeeScheduleDeSaved              { get; } = new(218, "woSetupEmployeeScheduleDe Saved");
  public static EventType unitTurnoverSaved                           { get; } = new(219, "unitTurnover Saved");
  public static EventType certRequestSaved                            { get; } = new(220, "certRequest Saved");
  
  public static EventType phaPortalLandlordOptionsSetupSaved          { get; } = new(221, "Portal Landlord Options Saved");
  public static EventType stMaHomeRentIncomeScheduleTypeSaved         { get; } = new(222, "stMaHomeRentIncomeScheduleType Saved");
  public static EventType stMaHomeRentIncomeScheduleDateSaved         { get; } = new(223, "stMaHomeRentIncomeScheduleDate Saved");
  public static EventType phaPortalLandlordSaved                      { get; } = new(224, "Portal Landlord Saved");
  public static EventType certFormReviewSaved                         { get; } = new(225, "Certification Form Review Saved");
  
  public static EventType invoiceSavedAvidInvoiceImport               { get; } = new(226, "Invoice Saved From Avid Invoice Import");
  public static EventType phaPortalApplicantOptionsSaved              { get; } = new(227, "Portal Applicant Saved");
  public static EventType glTrialBalanceWildCardSaved                 { get; } = new(228, "GL Trial Balance Wild Card Saved");
  public static EventType certFinishFormPacketSetupSaved              { get; } = new(229, "Cert Finish Form Packet Setup Saved");
  public static EventType familyCertChangeOfOwnershipSaved            { get; } = new(230, "Family Cert Change of Ownership");
  
  public static EventType certSignatureRequestSaved                   { get; } = new(231, "Cert Signature Request Saved");
  public static EventType invoiceSavedInvoiceImport                   { get; } = new(232, "invoice Saved Invoice Import");
  public static EventType woPhaSetupSaved                             { get; } = new(233, "work order pha setup saved");
  public static EventType diUploadApi                                 { get; } = new(234, "API Uploaded Document");
  public static EventType apVendorPhaSetupSaved                       { get; } = new(235, "ap vendor pha setup saved");
  
  public static EventType ecEmailAddressSaved                         { get; } = new(236, "ec Email Address Saved");
  public static EventType woSetupPrioritySaved                        { get; } = new(237, "Work Order Setup Priority Saved");
  public static EventType woSetupRequestedBySaved                     { get; } = new(238, "Work Order Setup Requested By Saved");
  public static EventType woSetupConfigurationSaved                   { get; } = new(239, "Work Order Setup Configuration Saved");
  public static EventType familyCertMassCreateBatchSaved              { get; } = new(240, "Family Cert Mass Create Batch Saved");
  
  public static EventType familyCertPaymentStandardChangeSaved        { get; } = new(241, "Family Cert Payment Standard Change Saved");
  public static EventType familyCertEndOfParticipationSaved           { get; } = new(242, "Family Cert End of Participation Saved");
  public static EventType glDepreciableAssetTypeSaved                 { get; } = new(243, "GL Depreciable Asset Type Saved");
  public static EventType familyCertMoveOutReasonSaved                { get; } = new(244, "50058 Move Out Reason Saved");
  public static EventType stMaMoveOutReasonSaved                      { get; } = new(245, "General Move Out Reason Saved");
  
  public static EventType familyCertPhaSetupSaved                     { get; } = new(246, "50058 PHA Setup Saved");
  public static EventType familyCertSetupScheduleTypeSaved            { get; } = new(247, "Setup Schedule Type Saved");
  public static EventType familyCertSetupScheduleEffectiveDateSaved   { get; } = new(248, "Setup Schedule Effective Date Saved");
  public static EventType mergeTemplateSaved                          { get; } = new(249, "Letter Merge Template Saved");
  public static EventType hipSubmissionFileSaved                      { get; } = new(250, "HIP Submission File Saved");
  
  public static EventType hipSubmissionFileFormJoinSaved              { get; } = new(251, "HIP Submission File Form Join Saved");
  public static EventType hipSubmissionErrorSaved                     { get; } = new(252, "HIP Submission Error Saved");
  public static EventType finishFormConfigSaved                       { get; } = new(253, "Finish Form Config Saved");
  public static EventType finishFormConfigSignatureSaved              { get; } = new(254, "Finish Form Config Signature Saved");
  public static EventType tracsPhaSetupSaved                          { get; } = new(255, "TRACS PHA Setup Saved");
  
  public static EventType insInspectionSaved                          { get; } = new(256, "Inspection Standard Inspection Saved");
  public static EventType insQuestionSaved                            { get; } = new(257, "Inspection Standard Question Setup Saved");
  public static EventType insRoomSaved                                { get; } = new(258, "Inspection Standard Room Setup Saved");
  public static EventType insInspectionDeficiencyWorkOrderSaved       { get; } = new(259, "Inspection Standard Deficiency Work Order Saved");
  public static EventType insInspectionMitigationSaved                { get; } = new(260, "Inspection Standard Mitigation Saved");
  
  public static EventType stMaMassCreateBatchSaved                    { get; } = new(261, "General Cert Mass Create Batch Saved");
  public static EventType insFamilyNotificationSetupSaved             { get; } = new(262, "NSPIRE Family Notification Template Saved");
  public static EventType glPhaSetupSaved                             { get; } = new(263, "General Ledger PHA Setup Saved");
  public static EventType phaPortalOppBatchDetailSaved                { get; } = new(264, "Deposit Batch Detail Saved");
  public static EventType depositProcessed                            { get; } = new(265, "Deposit Processed");
  
  public static EventType phaPortalOppBatchCompletionToggle           { get; } = new(266, "Deposit Batch Completed Toggle");
  public static EventType insInspectionDownloaded                     { get; } = new(267, "Inspection Standard Inspection Downloaded");
  public static EventType insTemplateTypeProgramSaved                 { get; } = new(268, "Inspection Standard Template Type Program Saved");
  public static EventType familyCertProjectContractSaved              { get; } = new(269, "Family Cert Project Contract Saved");
  public static EventType insPhaSetupSaved                            { get; } = new(270, "NSPIRE PHA Setup Saved");
  
  public static EventType phArSetupReceiptCashAccountSaved            { get; } = new(271, "T AR Setup Receipt Cash Account Saved");
  public static EventType phArSetupReceiptCashAccountProjectSaved     { get; } = new(272, "T AR Setup Receipt Cash Account Project Saved");
  public static EventType vendorContractorHoursSaved                  { get; } = new(273, "Vendor Contractor Hours Saved");
  public static EventType finAssetDepTemplateSaved                    { get; } = new(274, "Asset Depreciation Template Saved");
  public static EventType finAssetDepTemplateDetailSaved              { get; } = new(275, "Asset Depreciation Template Detail Saved");
  
  public static EventType finAssetDepDistributionDetailSaved          { get; } = new(276, "Asset Depreciation Distribution Detail Saved");
  public static EventType phDirectDebitGroupSaved                     { get; } = new(277, "PH Direct Debit Group Saved");
  public static EventType phaPortalMcConversationSeen                 { get; } = new(278, "Portal Conversation Seen");
  public static EventType wlStatusUpdateBatchSaved                    { get; } = new(279, "Waiting List Status Update Batch Saved");
  public static EventType wlStatusUpdateCriteriaSaved                 { get; } = new(280, "Waiting List Status Update Criteria Saved");
  
  public static EventType wlStatusUpdateRequestSaved                  { get; } = new(281, "Waiting List Status Update Request Saved");
  public static EventType stMaVoucherExtensionSaved                   { get; } = new(282, "General Cert Voucher Extension Saved");
  public static EventType stMaPhaSetupSaved                           { get; } = new(283, "General Cert PHA Setup Saved");
  public static EventType aiUnitDesignationSaved                      { get; } = new(284, "AI Unit Designation Saved");
  public static EventType aiSetAsideGroupSaved                        { get; } = new(285, "AI Set Aside Group Saved");
  
  public static EventType aiApplicableFractionGroupSaved              { get; } = new(286, "AI Applicable Fraction Group Saved");
//  public static EventType phaWebsiteSetupSaved                        { get; } = new(287, "PHA Website Setup Saved"); // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static EventType adminPhaSetupSaved                          { get; } = new(288, "Admin Pha Setup Group Saved");
  public static EventType meterReadingUnitTypeSaved                   { get; } = new(289, "Meter Reading Unit Type Saved");
  public static EventType meterReadingUtilityEffectiveSaved           { get; } = new(290, "Meter Reading Utility Effective Saved");
  
  public static EventType meterReadingUtilityTypeSaved                { get; } = new(291, "Meter Reading Utility Type Saved");
  public static EventType saPHASetupSaved                             { get; } = new(292, "Super Admin PHA Setup Saved");
  public static EventType wlStatusReasonSaved                         { get; } = new(293, "Waiting List Status Reason Saved");
  public static EventType fmContactSaved                              { get; } = new(294, "Family Contact Saved");
  public static EventType fmVehicleSaved                              { get; } = new(295, "Family Vehicle Saved");
  
  public static EventType fmPetSaved                                  { get; } = new(296, "Family Pet Saved");
  public static EventType lockboxByProgramSaved                       { get; } = new(297, "Lockbox By Program Saved");
//  public static EventType glAccountLabelSaved                         { get; } = new(298, "Financial Account Label Saved"); // COMMENTED OUT in 'enum'
  public static EventType smInfoMessageSaved                          { get; } = new(299, "Support Info Message Saved");
  public static EventType certALApplicationListSaved                  { get; } = new(300, "Application List Saved");
  
  public static EventType certALCredentialSaved                       { get; } = new(301, "Application List Credential Saved");
  public static EventType certALApplicationSaved                      { get; } = new(302, "Application List Application Saved");
  public static EventType glStatementSaved                            { get; } = new(303, "General Ledger Statement Saved");
  public static EventType glStatementGroupNodeSaved                   { get; } = new(304, "General Ledger Statement Group Node Saved");
  public static EventType glStatementColumnNodeSaved                  { get; } = new(305, "General Ledger Statement Column Node Saved");
  
  public static EventType certALStatusSaved                           { get; } = new(306, "Application List Status Saved");
  public static EventType certALApplicationListStatusSaved            { get; } = new(307, "Application List Status Connection Saved");
  public static EventType glStatementPacketSaved                      { get; } = new(308, "General Ledger Statement Packet Saved");
  public static EventType poInvoiceLineItemRelationSaved              { get; } = new(309, "Purchase Order Line Item Relation Saved");
  public static EventType poLineItemReleaseSaved                      { get; } = new(310, "Purchase Order Line Item Release Saved");
  
  public static EventType phPhaSetupSaved                             { get; } = new(311, "Tenant Accounting Pha Setup Saved");
  public static EventType hapMiscChargesSaved                         { get; } = new(312, "HAP Misc. Charges Saved");
  public static EventType programFinMiscChargesSaved                  { get; } = new(313, "Program Misc Charges Connection Saved");
  public static EventType phaUserGroupSaved                           { get; } = new(314, "PHA User Group Saved");
  public static EventType phaUserGroupLinkSaved                       { get; } = new(315, "PHA User Group Link Saved");
  
  public static EventType ddDataDownloadSaved                         { get; } = new(316, "Data Download Saved");
  public static EventType tracsSpecialClaimSaved                      { get; } = new(317, "TRACS Special Claim Saved");
  public static EventType tracsSpecialClaimUnpaidRentDamagesSaved     { get; } = new(318, "TRACS Special Claim Unpaid Rent/Damages Saved");
  public static EventType tracsSpecialClaimVacancyDuringRentUpSaved   { get; } = new(319, "TRACS Special Claim Vacancy During Rent-Up Saved");
  public static EventType tracsSpecialClaimRegularVacancySaved        { get; } = new(320, "TRACS Special Claim Regular Vacancy Saved");
  
  public static EventType tracsSpecialClaimDebtServiceSaved           { get; } = new(321, "TRACS Special Claim Debt Service Saved");
  public static EventType finDepartmentSaved                          { get; } = new(322, "Department Saved");
  public static EventType expirableFileSaved                          { get; } = new(323, "Expirable File Saved");
  public static EventType smTicketTagSaved                            { get; } = new(324, "Support Manager Ticket Tag Saved");
  
  #endregion
  
  #region Helper Method(s) ...
  
  // IMPORTANT: make sure ALL EventTypes are defined above, within the "Specific EventType Declarations" region.  IF NOT,
  //            then some EvenTypes "may" be left out of their associated dictionary ...
  
  public static readonly Lazy<IReadOnlyDictionary<int, EventType>> _fields = new (() => 
    typeof(EventType).GetFields(BindingFlags.Public | BindingFlags.Static)
                     .Where(f  => f.FieldType == typeof(EventType))
                     .Select(f => (EventType)f.GetValue(null)!)
                     .ToDictionary(e => e.ID));
  
  private static readonly Lazy<IReadOnlyDictionary<int, EventType>> _properties = new(() => 
    typeof(EventType).GetProperties(BindingFlags.Public | BindingFlags.Static)
                     .Where(p  => p.PropertyType == typeof(EventType))
                     .Select(p => (EventType)p.GetValue(null)!)
                     .ToDictionary(e => e.ID));
  
  public static IReadOnlyDictionary<int, EventType> Fields     = _fields.Value;
  public static IReadOnlyDictionary<int, EventType> Properties = _properties.Value;
  
  public static EventType GetFieldByID(int id) 
    => Fields.TryGetValue(id, out var value) ? value : throw new ArgumentOutOfRangeException($"Unknown event type id: {id}");
  
  public static EventType GetPropertyByID(int id) 
    => Properties.TryGetValue(id, out var value) ? value : throw new ArgumentOutOfRangeException($"Unknown event type id: {id}");

  #endregion

  #region enum-like equality: compare based on ID only (ignore Description for uniqueness)

  //public override bool Equals(object obj)
  //  => obj is EventType type && Equals(type);

  public bool Equals(EventType other)
    => other is not null && ID == other.ID;

  public override int GetHashCode()
    => ID.GetHashCode();

  //public static bool operator == (EventType left, EventType right)
  //  => left.Equals(right);
  //  //=> left?.ID == right?.ID;
  //
  //public static bool operator != (EventType left, EventType right)
  //  => !left.Equals(right);
  //  //=> left?.ID != right?.ID;
  
  #endregion
}

**************************************************************************************************/
#endregion
