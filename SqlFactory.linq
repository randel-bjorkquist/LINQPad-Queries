<Query Kind="Program">
  <NuGetReference>Microsoft.Data.SqlClient</NuGetReference>
  <Namespace>Microsoft.Data.SqlClient</Namespace>
  <Namespace>System.Configuration</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

//NOTE: loading code from other files within 'My Queries"
#load "Extensions\EnumerableExtensions\ToCSV Examples"
#load "Extensions\EnumerableExtensions\HasItems + IsNullOrEmpty + Random + Chunk"

void Main()
{
  var phaId     = 1190;
  var objectIds = new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
  var objectId  = objectIds.Random(1).First();
  
  IEnumerable<ExpirableFileEntity> results = [];
  
  try
  {
    GetByIDAsync(objectId).GetAwaiter()
                          .GetResult()
                          .Dump("GetByIDAsync(objectId)", 0);
                          
    GetByIDAsync(objectId, phaId).GetAwaiter()
                                 .GetResult()
                                 .Dump("GetByIDAsync(objectId, phaId)", 0);

    GetByIDsAsync(objectIds).GetAwaiter()
                            .GetResult()
                            .OrderBy(ob => ob.PhaId)
                            .ThenBy(ob => ob.Id)
                            .Dump("GetByIDsAsync(objectIds).OrderBy(ob => ob.PhaId).ThenBy(ob => ob.Id)", 0);

    GetByIDsAsync(objectIds, phaId).GetAwaiter()
                                   .GetResult()
                                   .OrderBy(ob => ob.Id)
                                   .Dump("GetByIDsAsync(objectIds, phaId).OrderBy(ob => ob.Id)", 0);

    //NOTE: forces check for .IsNullOrEmpty() against the IEnumerable<int> 'objectIds' parameter ...
    GetByIDsAsync(null, phaId).GetAwaiter()
                                   .GetResult()
                                   .OrderBy(ob => ob.Id)
                                   .Dump("GetByIDsAsync(null, phaId).OrderBy(ob => ob.Id)", 0);
  }
  catch(StoredProcedureException ex)
  {
    Console.WriteLine($"STORED PROCEDURE EXCEPTION: {ex.Message}");
  }
  catch(SqlException ex)
  {
    Console.WriteLine($"SQL EXCEPTION: {ex.Message}");
  }
  catch(Exception ex)
  {
    Console.WriteLine($"GENERAL EXCEPTION: {ex.Message}");
  }
}

#region GetByID/GetByIDs

public async Task<ExpirableFileEntity> GetByIDAsync(int objectId, CancellationToken token = default)
{
  var results = await GetByIDsAsync( objectIds: [objectId]
                                    ,phaId: null
                                    ,token: token );
  return results.FirstOrDefault();
}

public async Task<ExpirableFileEntity> GetByIDAsync(int objectId, int phaId, CancellationToken token = default)
{
  var results = await GetByIDsAsync( objectIds: [objectId]
                                    ,phaId: phaId
                                    ,token: token );
  return results.FirstOrDefault();
}

public async Task<IEnumerable<ExpirableFileEntity>> GetByIDsAsync(IEnumerable<int> objectIds, int? phaId = null, CancellationToken token = default)
{
  if(objectIds.IsNullOrEmpty())
    return Enumerable.Empty<ExpirableFileEntity>();
  
  var results = new List<ExpirableFileEntity>();
  
  //NOTE: SQL Server Accounts, authenticating with SQL Server Authentication
  //        'PHA-WebServer' OR 'sa' (only my local SQL Server instance)
  //NOTE: true = 'sa' | false = 'PHA-WebServer'
  //NOTE: Connecting to "localhost", 'trust_certificate = true' allows, non-trusted, self-assigned cert, and thus 'encrypt = true' works;
  //      when setting 'trust_certificate = false', 'encrypt = true' forces encryption via a trusted, non-self-assigned certs. To make
  //      this work, simply modify 'encrypt = false', which is not affected if 'trust_certificate = false'.
  //using var connection = true
  //                     ? SqlFactory.BuildSqlConnection("localhost", "PHA-Web_dev", "sa", "SQLP@ssw0rd", encrypt: false)
  //                     : SqlFactory.BuildSqlConnection("localhost", "PHA-Web_dev", "PHA-WebServer", "vfk72oik", trust_certificate: true);
  //
  //NOTE: Windows Accounts, authenticating with Windows Authentication
  //using var connection = SqlFactory.BuildSqlConnection("localhost", "PHA-Web_dev", encrypt: true, trust_certificate: false);  //FAILURE
  using var connection = SqlFactory.BuildSqlConnection("localhost", "PHA-Web_dev", encrypt: true, trust_certificate: true);   //SUCCESS
  //using var connection = SqlFactory.BuildSqlConnection("localhost", "PHA-Web_dev", encrypt: false, trust_certificate: false); //SUCCESS
  //using var connection = SqlFactory.BuildSqlConnection("localhost", "PHA-Web_dev", encrypt: false, trust_certificate: true);  //SUCCESS
  
  //Debug.WriteLineIf( connection.ConnectionString.Contains("User ID=sa")
  //                  ,"SA-Connection" );
                       
  using var command = BuildGetByIDsCommand(connection);
  
  AddGetByIDsParameters(command, objectIds, phaId);
  
  await connection.OpenAsync(token);
  
  using var reader = await command.ExecuteReaderAsync(token);
  
  while(await reader.ReadAsync(token))
  {
    var entity = HydrateFromReader(reader);
    
    if(entity is not null)
      results.Add(entity);
  }
  
  if(!reader.IsClosed)
    await reader.CloseAsync();
  
  var return_value = SqlHelpers.GetReturnValue(command, -1);
  
  if(return_value != 0)
    throw new StoredProcedureException("sp_ExpirableFileGetByIDs", return_value);
  
  return results ?? Enumerable.Empty<ExpirableFileEntity>();
}

protected SqlCommand BuildGetByIDsCommand(SqlConnection connection)
  => SqlFactory.BuildSqlCommand("sp_ExpirableFileGetByIDs", connection);

protected void AddGetByIDsParameters(SqlCommand command, IEnumerable<int> objectIds, int? phaId)
{
  var delimiter = ",";
  var ids = objectIds.Distinct()
                     .OrderBy(id => id)
                     .Join(delimiter);

  // Input Parameter(s)
  SqlHelpers.AddInputParameter(command, "@IDs", ids);
  SqlHelpers.AddInputParameter(command, "@delimiter", delimiter);
  
  if(phaId is not null)
    SqlHelpers.AddInputParameter(command, "@PhaID", phaId);
  
  // Return Parameter
  SqlHelpers.AddReturnParameter(command, "@RETURN_VALUE", SqlDbType.Int);
}

protected ExpirableFileEntity HydrateFromReader(SqlDataReader reader)
  => reader.ToExpirableFileEntity();

#endregion

#region interface(s)

public interface IValidatable
{
  void Validate(bool isCreate = false);
}

public interface IdbEntity : IValidatable
{
  /// <summary> The unique identifier for the entity. </summary>
  int Id { get; set; }
}

public interface IdbDeletableEntity : IdbEntity
{
  /// <summary> Indicates if the entity is marked as deleted (soft delete). </summary>
  bool Deleted { get; set; }
}

public interface IdbPhaEntity : IdbEntity
{
  /// <summary> The Phase identifier associated with the entity. </summary>
  int PhaId { get; set; }
}

public interface IdbDeletablePhaEntity : IdbDeletableEntity, IdbPhaEntity
{
  // No additional members; inherits all from bases
}

public interface IExpirableFileEntity : IdbDeletablePhaEntity
{
  public DateTime ExpiresAt { get; set; }
  public int FileSource     { get; set; }
  public bool Purged        { get; set; }
  public int StorageId      { get; set; }
}

#endregion

#region entity(s)

public abstract class dbEntity : IdbEntity
{
  public virtual int Id { get; set; }
  
  public virtual void Validate(bool isCreate = false)
  {
    if(!isCreate && Id <= 0)
      Console.WriteLine($"ERROR: {nameof(Id)} must be greater than 0 (zero) when updating or deleting.");
  }
}

public abstract class dbDeletableEntity : dbEntity, IdbDeletableEntity
{
  public virtual bool Deleted { get; set; }

  public override void Validate(bool isCreate = false)
  {
    Validate(isCreate, false);
  }

  public virtual void Validate(bool isCreate = false, bool allowDeletedOnCreate = false)
  {
    base.Validate(isCreate);

    // Core soft-delete check: Prevent accidental deleted inserts
    if (isCreate && Deleted && !allowDeletedOnCreate)
      Console.WriteLine($"ERROR: {nameof(Deleted)}: new entities cannot have Deleted = true unless explicitly allowed (e.g., for imports).");
  }
}

public abstract class dbPhaEntity : dbEntity, IdbPhaEntity
{
  public virtual int PhaId { get; set; }

  public override void Validate(bool isCreate = false)
  {
    base.Validate(isCreate);

    if (PhaId <= 0)
      Console.WriteLine($"ERROR: {nameof(PhaId)} must be greater than 0 (zero).");
  }
}

public abstract class dbDeletablePhaEntity : dbDeletableEntity, IdbPhaEntity
{
  public int PhaId { get; set; } // FROM: IdbPhaEntity

  public override void Validate(bool isCreate = false, bool allowDeletedOnCreate = false)
  {
    base.Validate(isCreate, allowDeletedOnCreate);  // Inherits Deleted checks

    // Pha-specific: Always require positive PhaId (even on create)
    if (PhaId <= 0)
      Console.WriteLine($"ERROR: {nameof(PhaId)} must be greater than 0 (zero).");
  }
}

public class ExpirableFileEntity : dbDeletablePhaEntity, IExpirableFileEntity
{
  public DateTime ExpiresAt { get; set; }
  public int FileSource     { get; set; }
  public bool Purged        { get; set; }
  public int StorageId      { get; set; }
}

#endregion


public static class SqlDataReaderExtensions
{
    public static ExpirableFileEntity ToExpirableFileEntity(this SqlDataReader reader)
      => new ExpirableFileEntity { Id         = SqlHelpers.GetInteger(reader, "ID"),
                                   PhaId      = SqlHelpers.GetInteger(reader, "phaId"),
                                   Deleted    = SqlHelpers.GetBoolean(reader, "deleted"),
                                   
                                   ExpiresAt  = SqlHelpers.GetDateTime(reader, "expiresAt"),
                                   FileSource = SqlHelpers.GetInteger(reader, "fileSource"),
                                   Purged     = SqlHelpers.GetBoolean(reader, "purged"),
                                   StorageId  = SqlHelpers.GetInteger(reader, "storageId") };
}


public class GPEvent
{
  public int Id                         { get; set; } = -1;
  public int PhaId                      { get; set; }
  public int UserId                     { get; set; }
  public string UserName                { get; set; }

  public SqlConnection SqlConnection    { get; set; }
  public SqlTransaction SqlTransaction  { get; set; }

  public bool HistoricalCopy            { get; set; }
  public DateTime HistoricalCopied      { get; set; }
                                         
  public int EventTypeId                { get; set; }
  public string EventLocation           { get; set; }
  public int OriginalGpEventId          { get; set; }
  public DateTime DateTime              { get; set; }
  public string Note                    { get; set; }
}

#region interface ISqlFactory

//NOTE: ONLY implement when after converting the static SqlFactory class to
//      non-static, when implementing DI ...
//public interface ISqlFactory
//{
//  SqlConnection BuildSqlConnection(string? settingName = null);
//
//  SqlCommand BuildSqlCommand(string procedureName, GPEvent gpEvent);
//  SqlCommand BuildSqlCommand(string procedureName, SqlConnection connection);
//  SqlCommand BuildSqlCommand(string procedureName, SqlConnection connection, SqlTransaction transaction);
//}

#endregion

//public class SqlFactory : ISqlFactory
public static class SqlFactory
{
  #region COMMENTED OUT: R&D CODE (reflection method for gpEvent)

  //NOTE: Uses reflection to find the 'SqlConnection' and 'SqlTransaction' properties; removing the
  //      need to reference 'mcsClasses' and thus removing any chance of creating a circular reference
  //private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _propertyCache = new ConcurrentDictionary<Type, PropertyInfo[]>();

  //NOTE: Uses reflection to find the 'SqlConnection' and 'SqlTransaction' properties; removing the
  //      need to reference 'mcsClasses' and thus removing any chance of creating a circular reference
  //public SqlCommand BuildSqlCommand(string procedureName, object gpEvent = null)
  //{
  //  if(gpEvent == null)
  //    throw new ArgumentNullException(nameof(gpEvent));
  //
  //  var type = gpEvent.GetType();
  //
  //  //Get Or Cache the 'SqlConnection' and 'SqlTransaction' metadata ...
  //  var properties = _propertyCache.GetOrAdd( type, t => {
  //    return new[] { t.GetProperty("sqlConnection",  BindingFlags.Public | BindingFlags.Instance),
  //                    t.GetProperty("sqlTransaction", BindingFlags.Public | BindingFlags.Instance) };
  //
  //    //var connProp = t.GetProperty("sqlConnection", BindingFlags.Public | BindingFlags.Instance);
  //    //var tranProp = t.GetProperty("sqlTransaction", BindingFlags.Public | BindingFlags.Instance);
  //    //
  //    //return new[] { connProp, tranProp };
  //  });
  //    
  //  var connectionProperty  = properties[0];
  //  var transactionProperty = properties[1];
  //
  //  if(connectionProperty == null || transactionProperty == null)
  //    throw new ArgumentException("gpEvent must provide sqlConnection and sqlTransaction properties.", nameof(gpEvent));
  //
  //  // Retrieve the actual property values for this specific instance
  //  var connection = connectionProperty.GetValue(gpEvent) as SqlConnection 
  //                  ?? throw new InvalidOperationException("sqlConnection property was null or not a SqlConnection.");
  //
  //  var transaction = transactionProperty.GetValue(gpEvent) as SqlTransaction;
  //    
  //  return BuildSqlCommand(procedureName, connection, transaction);
  //
  //  #region COMMENTED OUT: non-cache option
  //  //
  //  //if (gpEvent != null)
  //  //{
  //  //  var type = gpEvent.GetType();
  //  //
  //  //  // Retrieve the metadata for the following properties (no values yet)
  //  //  var connectionProperty  = type.GetProperty("sqlConnection");
  //  //  var transactionProperty = type.GetProperty("sqlTransaction");
  //  //
  //  //  if (connectionProperty != null && transactionProperty != null) 
  //  //  {
  //  //      // Now fetch the actual property values for the gpEvent instance ...
  //  //      var connection  = (SqlConnection)connectionProperty.GetValue(gpEvent);
  //  //      var transaction = (SqlTransaction)transactionProperty.GetValue(gpEvent);
  //  //
  //  //      return BuildSqlCommand(procedureName, connection, transaction);
  //  //  }
  //  //}
  //  //
  //  //throw new ArgumentException("gpEvent must provide connection and transaction.", nameof(gpEvent));
  //  //
  //  #endregion
  //}

  #endregion

  public static SqlConnection BuildSqlConnection(string server, string database, string username, string password, bool encrypt = true, bool trust_certificate = false)
    => BuildSqlConnection(server, database, username, password, encrypt, trust_certificate, integrated_security: false);
    
  public static SqlConnection BuildSqlConnection(string server, string database, bool encrypt = true, bool trust_certificate = false)
    => BuildSqlConnection(server, database, username: null, password: null, encrypt, trust_certificate, integrated_security: true);
  
  private static SqlConnection BuildSqlConnection( string server
                                                  ,string database
                                                  ,string username
                                                  ,string password
                                                  ,bool encrypt             = true     // Explicitly disable encryption (false) by overriding the default value (true)
                                                  ,bool trust_certificate   = false    // Optional: Disable (true) trust server certificate default value (false) if needed (for non-SSL connections)
                                                  ,bool integrated_security = false )
  {
    switch(integrated_security)
    {
      case true when username is not null || 
                     password is not null:
        throw new ArgumentException("Cannot specify username and/or password when using IntegratedSecurity (Windows Authentication).");
      
      case false when string.IsNullOrEmpty(username) ||
                      string.IsNullOrEmpty(password):
        throw new ArgumentException("Username and password are required for SQL Server Authentication.");        
    }
    
    var builder = new SqlConnectionStringBuilder { DataSource             = server
                                                  ,InitialCatalog         = database
                                                  ,IntegratedSecurity     = integrated_security
                                                  ,Encrypt                = encrypt
                                                  ,TrustServerCertificate = trust_certificate };
    
    if(!integrated_security)
    {
      builder.UserID    = username;
      builder.Password  = password;
    }
    
    var connection = new SqlConnection(builder.ConnectionString);

    #region COMMENTED OUT: Optional logging for debugging (random length masked password)
    //
    //var org_length   = builder.Password.Length;
    //var min_length   = (int)Math.Ceiling(org_length / 2.0); //e.g., 8 -> 4
    //var max_length   = org_length * 2;                      //e.g., 8 -> 16
    //var random_lenth = Random.Shared.Next(min_length, max_length + 1);
    //
    //string masked_connection_string = string.IsNullOrWhiteSpace(builder.Password)
    //                                ? builder.ConnectionString
    //                                : builder.ConnectionString
    //                                         .Replace( builder.Password
    //                                                  ,new string('*', random_lenth));
    //
    //Debug.WriteLine($"Connection built: {masked_connection_string}");
    //
    #endregion
    
    return connection;
  }
  
  public static SqlConnection BuildSqlConnection(string settingName = null)
    => settingName == null
                    ? new SqlConnection(ConfigurationManager.AppSettings["connectionString"])
                    : new SqlConnection(ConfigurationManager.AppSettings[settingName]);
  
  public static SqlCommand BuildSqlCommand(string procedureName, GPEvent gpEvent)
    => BuildSqlCommand(procedureName, gpEvent.SqlConnection, gpEvent.SqlTransaction);
  
  public static SqlCommand BuildSqlCommand(string procedureName, SqlConnection connection)
    => BuildSqlCommand(procedureName, connection, null);
  
  public static SqlCommand BuildSqlCommand(string procedureName, SqlConnection connection, SqlTransaction transaction)
  {
    SqlCommand command = transaction == null
                       ? new SqlCommand(procedureName, connection) 
                       : new SqlCommand(procedureName, connection, transaction);

    command.CommandType    = CommandType.StoredProcedure;                        
    command.CommandTimeout = int.Parse(ConfigurationManager.AppSettings["commandTimeOut"] ?? "30");
      
    return command;
  }
}

internal static class SqlHelpers
{
  #region Add Parameter Methods

  public static SqlParameter AddInputParameter<T>(SqlCommand command, string parameterName, T value, SqlDbType? sqlDbType = null)
  {
    var dbType = sqlDbType ?? GetSqlDbTypeForType(typeof(T));
    return AddParameter(command, parameterName, value, dbType, ParameterDirection.Input);
  }

  public static SqlParameter AddInputParameter(SqlCommand command, string parameterName, object value, SqlDbType sqlDbType)
  {
    return AddParameter(command, parameterName, value, sqlDbType, ParameterDirection.Input);
  }

  public static SqlParameter AddInputOutputParameter<T>(SqlCommand command, string parameterName, T value = default, SqlDbType? sqlDbType = null)
  {
    var dbType = sqlDbType ?? GetSqlDbTypeForType(typeof(T));
    return AddParameter(command, parameterName, value, dbType, ParameterDirection.InputOutput);
  }

  public static SqlParameter AddInputOutputParameter(SqlCommand command, string parameterName, object value, SqlDbType sqlDbType)
  {
    return AddParameter(command, parameterName, value, sqlDbType, ParameterDirection.InputOutput);
  }

  public static SqlParameter AddOutputParameter<T>(SqlCommand command, string parameterName)
  {
    var dbType = GetSqlDbTypeForType(typeof(T));
    return AddOutputParameter(command, parameterName, dbType);
  }

  public static SqlParameter AddOutputParameter(SqlCommand command, string parameterName, SqlDbType sqlDbType)
  {
    return AddParameter(command, parameterName, sqlDbType, ParameterDirection.Output);
  }

  public static SqlParameter AddReturnParameter(SqlCommand command, string parameterName, SqlDbType sqlDbType)
  {
    return AddParameter(command, parameterName, sqlDbType, ParameterDirection.ReturnValue);
  }

  #endregion

  #region Return Value

  public static int GetReturnValue(SqlCommand command, int defaultValue = 0)
    => GetInteger(command, "@RETURN_VALUE", defaultValue);

  #endregion

  #region Boolean (SqlCommand/SqlDataReader) Function(s)

  public static bool GetBoolean(SqlCommand command, string parameterName, bool defaultValue = false) 
    => GetValue(command, parameterName, defaultValue);

  public static bool? GetNullableBoolean(SqlCommand command, string parameterName, bool? defaultValue = null)
    => GetValue(command, parameterName, defaultValue);

  public static bool GetBoolean(SqlDataReader reader, string columnName, bool defaultValue = false)
    => GetValue(reader, columnName, defaultValue);

  public static bool? GetNullableBoolean(SqlDataReader reader, string columnName, bool? defaultValue = null)
    => GetValue(reader, columnName, defaultValue);

  #endregion

  #region Numeric (SqlCommand/SqlDataReader) Function(s)

  public static byte GetByte(SqlCommand command, string parameterName, byte defaultValue = 0)
    => GetValue(command, parameterName, defaultValue);

  public static byte? GetNullableByte(SqlCommand command, string parameterName, byte? defaultValue = null)
    => GetValue(command, parameterName, defaultValue);

  public static byte GetByte(SqlDataReader reader, string columnName, byte defaultValue = 0)
    => GetValue(reader, columnName, defaultValue);

  public static byte? GetNullableByte(SqlDataReader reader, string columnName, byte? defaultValue = null)
    => GetValue(reader, columnName, defaultValue);

  public static short GetShort(SqlCommand command, string parameterName, short defaultValue = 0)
    => GetValue(command, parameterName, defaultValue);

  public static short? GetNullableShort(SqlCommand command, string parameterName, short? defaultValue = null)
    => GetValue(command, parameterName, defaultValue);

  public static short GetShort(SqlDataReader reader, string columnName, short defaultValue = 0)
    => GetValue(reader, columnName, defaultValue);

  public static short? GetNullableShort(SqlDataReader reader, string columnName, short? defaultValue = null)
    => GetValue(reader, columnName, defaultValue);

  public static int GetInteger(SqlCommand command, string parameterName, int defaultValue = 0)
    => GetValue(command, parameterName, defaultValue);

  public static int? GetNullableInteger(SqlCommand command, string parameterName, int? defaultValue = null)
    => GetValue(command, parameterName, defaultValue);

  public static int GetInteger(SqlDataReader reader, string columnName, int defaultValue = 0)
    => GetValue(reader, columnName, defaultValue);

  public static int? GetNullableInteger(SqlDataReader reader, string columnName, int? defaultValue = null)
    => GetValue(reader, columnName, defaultValue);

  public static long GetLong(SqlCommand command, string parameterName, long defaultValue = 0)
    => GetValue(command, parameterName, defaultValue);

  public static long? GetNullableLong(SqlCommand command, string parameterName, long? defaultValue = null)
    => GetValue(command, parameterName, defaultValue);

  public static long GetLong(SqlDataReader reader, string columnName, long defaultValue = 0)
    => GetValue(reader, columnName, defaultValue);

  public static long? GetNullableLong(SqlDataReader reader, string columnName, long? defaultValue = null)
    => GetValue(reader, columnName, defaultValue);

  public static decimal GetDecimal(SqlCommand command, string parameterName, decimal defaultValue = 0)
    => GetValue(command, parameterName, defaultValue);

  public static decimal? GetNullableDecimal(SqlCommand command, string parameterName, decimal? defaultValue = null)
    => GetValue(command, parameterName, defaultValue);

  public static decimal GetDecimal(SqlDataReader reader, string columnName, decimal defaultValue = 0)
    => GetValue(reader, columnName, defaultValue);
  public static decimal? GetNullableDecimal(SqlDataReader reader, string columnName, decimal? defaultValue = null)
    => GetValue(reader, columnName, defaultValue);

  public static float GetSingle(SqlCommand command, string parameterName, float defaultValue = 0)
    => GetValue(command, parameterName, defaultValue);

  public static float? GetNullableSingle(SqlCommand command, string parameterName, float? defaultValue = null)
    => GetValue(command, parameterName, defaultValue);

  public static float GetSingle(SqlDataReader reader, string columnName, float defaultValue = 0)
    => GetValue(reader, columnName, defaultValue);

  public static float? GetNullableSingle(SqlDataReader reader, string columnName, float? defaultValue = null)
    => GetValue(reader, columnName, defaultValue);

  public static double GetDouble(SqlCommand command, string parameterName, double defaultValue = 0)
    => GetValue(command, parameterName, defaultValue);

  public static double? GetNullableDouble(SqlCommand command, string parameterName, double? defaultValue = null)
    => GetValue(command, parameterName, defaultValue);

  public static double GetDouble(SqlDataReader reader, string columnName, double defaultValue = 0)
    => GetValue(reader, columnName, defaultValue);

  public static double? GetNullableDouble(SqlDataReader reader, string columnName, double? defaultValue = null)
    => GetValue(reader, columnName, defaultValue);

  #endregion

  #region DateTime (SqlCommand/SqlDataReader) Function(s)

  public static DateTime GetDateTime(SqlCommand command, string parameterName, DateTime defaultValue = default)
    => GetValue(command, parameterName, defaultValue);

  public static DateTime? GetNullableDateTime(SqlCommand command, string parameterName, DateTime? defaultValue = null)
    => GetValue(command, parameterName, defaultValue);

  public static DateTime GetDateTime(SqlDataReader reader, string columnName, DateTime defaultValue = default)
    => GetValue(reader, columnName, defaultValue);

  public static DateTime? GetNullableDateTime(SqlDataReader reader, string columnName, DateTime? defaultValue = null)
    => GetValue(reader, columnName, defaultValue);

  #endregion

  #region Text (SqlCommand/SqlDataReader) Function(s)

  public static string GetString(SqlCommand command, string parameterName, string defaultValue = null)
    => GetValue(command, parameterName, defaultValue);

  public static string GetString(SqlDataReader reader, string columnName, string defaultValue = null)
    => GetValue(reader, columnName, defaultValue);

  public static Guid GetGuid(SqlCommand command, string parameterName, Guid defaultValue = default)
    => GetValue(command, parameterName, defaultValue);

  public static Guid? GetNullableGuid(SqlCommand command, string parameterName, Guid? defaultValue = null)
    => GetValue(command, parameterName, defaultValue);

  public static Guid GetGuid(SqlDataReader reader, string columnName, Guid defaultValue = default)
    => GetValue(reader, columnName, defaultValue);

  public static Guid? GetNullableGuid(SqlDataReader reader, string columnName, Guid? defaultValue = null)
    => GetValue(reader, columnName, defaultValue);

  #endregion

  #region Binary (SqlCommand/SqlDataReader) Function(s)

  public static byte[] GetBinary(SqlCommand command, string parameterName, byte[] defaultValue = null)
    => GetValue(command, parameterName, defaultValue);

  public static byte[] GetBinary(SqlDataReader reader, string columnName, byte[] defaultValue = null)
    => GetValue(reader, columnName, defaultValue);

  #endregion

  #region Private Method(s)

  private static void ValidateParameters(SqlCommand command, string parameterName)
  {
    if (command == null)
      throw new ArgumentNullException(nameof(command), "SqlCommand cannot be null.");
                
    if (string.IsNullOrEmpty(parameterName)) 
      throw new ArgumentException("Parameter Name cannot be null or empty.", nameof(parameterName));
  }

  private static void ValidateParameters(SqlDataReader reader, string columnName)
  {
    if (reader == null)
      throw new ArgumentNullException(nameof(reader), "SqlDataReader cannot be null.");

    if (string.IsNullOrEmpty(columnName))
      throw new ArgumentException("Column Name cannot be null or empty.", nameof(columnName));
  }

  private static SqlDbType GetSqlDbTypeForType(Type type)
  {        
    #region COMMENTED OUT: original code (multiple if-statements new pattern-matching return switch-statement)
    //
    //if (type == null)
    //  throw new ArgumentNullException(nameof(type));
    //
    //if (type.IsEnum) 
    //  return SqlDbType.Int;
    //
    //if (type == typeof(bool) || type == typeof(bool?))
    //  return SqlDbType.Bit;
    //
    //if (type == typeof(byte) || type == typeof(byte?))
    //  return SqlDbType.TinyInt;
    //
    //if (type == typeof(short) || type == typeof(short?))
    //  return SqlDbType.SmallInt;
    //
    //if (type == typeof(int) || type == typeof(int?))
    //  return SqlDbType.Int;
    //
    //if (type == typeof(long) || type == typeof(long?))
    //  return SqlDbType.BigInt;
    //
    //if (type == typeof(decimal) || type == typeof(decimal?))
    //  return SqlDbType.Decimal;
    //
    //if (type == typeof(Single) || type == typeof(Single?))
    //  return SqlDbType.Real;
    //
    //if (type == typeof(double) || type == typeof(double?))
    //  return SqlDbType.Float;
    //
    //if (type == typeof(DateTime) || type == typeof(DateTime?))
    //  return SqlDbType.DateTime;
    //
    //if (type == typeof(string))
    //  return SqlDbType.NVarChar;
    //
    //if (type == typeof(Guid) || type == typeof(Guid?))
    //  return SqlDbType.UniqueIdentifier;
    //
    //if (type == typeof(byte[]))
    //  return SqlDbType.VarBinary;
    //
    //throw new ArgumentException($"Unsupported type: {type.Name}. Specify SqlDbType explicitly.", nameof(type));
    //
    #endregion        
     
    #region REFACTORD CODE: new pattern-matching return switch-statement
    
    if(type == null)
      throw new ArgumentNullException(nameof(type));
    
    return type switch {
      Type t when t.IsEnum => SqlDbType.Int,
    
      Type t when t == typeof(bool)     || t == typeof(bool?)     => SqlDbType.Bit,
      Type t when t == typeof(byte)     || t == typeof(byte?)     => SqlDbType.TinyInt,
                                                                  
      Type t when t == typeof(short)    || t == typeof(short?)    => SqlDbType.SmallInt,
      Type t when t == typeof(int)      || t == typeof(int?)      => SqlDbType.Int,
      Type t when t == typeof(long)     || t == typeof(long?)     => SqlDbType.BigInt,
                                                                  
      Type t when t == typeof(decimal)  || t == typeof(decimal?)  => SqlDbType.Decimal,
      Type t when t == typeof(Single)   || t == typeof(Single?)   => SqlDbType.Real,
      Type t when t == typeof(double)   || t == typeof(double?)   => SqlDbType.Float,
                                                                  
      Type t when t == typeof(DateTime) || t == typeof(DateTime?) => SqlDbType.DateTime,
      Type t when t == typeof(Guid)     || t == typeof(Guid?)     => SqlDbType.UniqueIdentifier,
    
      Type t when t == typeof(string) => SqlDbType.NVarChar,
      Type t when t == typeof(byte[]) => SqlDbType.VarBinary,
    
      _ => throw new ArgumentException($"Unsupported type: {type.Name}. Specify SqlDbType explicitly.", nameof(type))
    };
    
    #endregion
  }

  private static SqlParameter GetParameter(SqlCommand command, string parameterName)
  {
    ValidateParameters(command, parameterName);

    if (parameterName == "@RETURN_VALUE" && !command.Parameters.Contains(parameterName))
      return null;

    if (!command.Parameters.Contains(parameterName))
      throw new ArgumentOutOfRangeException($"Failed to find parameter {parameterName}.");

    return command.Parameters[parameterName];
  }

  private static T GetValue<T>(SqlCommand command, string parameterName, T defaultValue)
  {
    var parameter = GetParameter(command, parameterName);
      
    if (parameter == null && parameterName == "@RETURN_VALUE") 
      return defaultValue;

    if (parameter.Value == DBNull.Value) 
      return defaultValue;

    try
    {
      return (T)Convert.ChangeType(command.Parameters[parameterName].Value, typeof(T));
    }
    catch (InvalidCastException ex)
    {
      throw new InvalidCastException($"Failed to cast parameter {parameterName} to type {typeof(T).Name}.", ex);
    }
  }

  private static T? GetValue<T>(SqlCommand command, string parameterName, T? defaultValue) where T : struct
  {
    var parameter = GetParameter(command, parameterName);
      
    if (parameter == null && parameterName == "@RETURN_VALUE") 
      return defaultValue;

    if (parameter.Value == DBNull.Value)
      return defaultValue ?? default;

    try
    {
      return (T)Convert.ChangeType(command.Parameters[parameterName].Value, typeof(T));
    }
    catch (InvalidCastException ex)
    {
      throw new InvalidCastException($"Failed to cast parameter {parameterName} to type {typeof(T).Name}.", ex);
    }
  }

  private static T GetValue<T>(SqlDataReader reader, string columnName, T defaultValue)
  {
    ValidateParameters(reader, columnName);

    var ordinal = reader.GetOrdinal(columnName);

    if (reader.IsDBNull(ordinal)) 
        return defaultValue;

    try
    {
      return (T)Convert.ChangeType(reader.GetValue(ordinal), typeof(T));
    }
    catch (InvalidCastException ex)
    {
      throw new InvalidCastException($"Failed to cast column {columnName} to type {typeof(T).Name}.", ex);
    }
  }

  private static T? GetValue<T>(SqlDataReader reader, string columnName, T? defaultValue) where T : struct
  {
    ValidateParameters(reader, columnName);

    var ordinal = reader.GetOrdinal(columnName);

    if (reader.IsDBNull(ordinal))
        return defaultValue ?? default;

    try
    {
      return (T)Convert.ChangeType(reader.GetValue(ordinal), typeof(T));
    }
    catch (InvalidCastException ex)
    {
      throw new InvalidCastException($"Failed to cast column {columnName} to type {typeof(T).Name}.", ex);
    }
  }

  private static SqlParameter AddParameter<T>(SqlCommand command, string parameterName, T value, SqlDbType sqlDbType, ParameterDirection direction)
  {
    ValidateParameters(command, parameterName);

    var parameter = command.Parameters.Add(parameterName, sqlDbType);
    parameter.Direction = direction;
    parameter.Value = value == null ? DBNull.Value : (object)value;

    return parameter;
  }

  private static SqlParameter AddParameter(SqlCommand command, string parameterName, SqlDbType sqlDbType, ParameterDirection direction)
  {
    ValidateParameters(command, parameterName);

    if (direction == ParameterDirection.Input)
      throw new ArgumentException("Direction cannot be Input for a parameter without a value.", nameof(direction));

    var parameter = command.Parameters.Add(parameterName, sqlDbType);
    parameter.Direction = direction;
    parameter.Value = DBNull.Value;

    return parameter;
  }

  #endregion
}

[Serializable]
public class StoredProcedureException : Exception
{
  /// <summary> The stored procedure name (if known). </summary>
  public string StoredProcedureName { get; }

  /// <summary> The return value indicating the error (e.g., non-zero code). </summary>
  public int ReturnValue { get; }

  /// <summary> Initializes a new instance of the <see cref="StoredProcedureException"/> class. </summary>
  public StoredProcedureException() { }

  /// <summary> Initializes a new instance with a message. </summary>
  /// <param name="message">The error message.</param>
  public StoredProcedureException(string message) : base(message) { }

  /// <summary> Initializes a new instance with a message and inner exception. </summary>
  /// <param name="message">The error message.</param>
  /// <param name="innerException">The underlying exception (e.g., SqlException).</param>
  public StoredProcedureException(string message, Exception innerException)
    : base(message, innerException) { }

  /// <summary> Initializes a new instance with SP details. </summary>
  /// <param name="storedProcedureName">The name of the stored procedure.</param>
  /// <param name="returnValue">The non-zero return value from the SP.</param>
  /// <param name="message">Optional custom message.</param>
  public StoredProcedureException(string storedProcedureName, int returnValue, string? message = null)
    : base(message ?? $"Stored procedure '{storedProcedureName}' returned error code {returnValue}.")
  {
    StoredProcedureName = storedProcedureName;
    ReturnValue = returnValue;
  }

  /// <summary>
  /// Initializes a new instance for serialization.
  /// </summary>
  protected StoredProcedureException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    StoredProcedureName = info.GetString(nameof(StoredProcedureName));
    ReturnValue = info.GetInt32(nameof(ReturnValue));
  }

  /// <summary>
  /// Populates serialization info.
  /// </summary>
  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);

    info.AddValue(nameof(StoredProcedureName), StoredProcedureName);
    info.AddValue(nameof(ReturnValue), ReturnValue);
  }
}