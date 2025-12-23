<Query Kind="Program">
  <NuGetReference>Microsoft.Data.SqlClient</NuGetReference>
  <Namespace>Microsoft.Data.SqlClient</Namespace>
  <Namespace>System.Configuration</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
}

public static class SqlHelpers
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
