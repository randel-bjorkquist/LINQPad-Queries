<Query Kind="Program">
  <NuGetReference>System.Text.Json</NuGetReference>
  <Namespace>System.Text.Json</Namespace>
</Query>

//-------------------------------------------------------------------------------------------------
//NOTE: This is a C# program because 'FeatureFlag' is a sealed partial class, with its specific, 
//      instances declared in the 'FeatureFlag.Definitions' file. This file defines the what a
//      'FeatureFlag' is, by extending the 'TypedEnumGuid' type.
//
//ERROR: pressing 'F5' in an attempt to 'run' this file will generate the exception "Cannot find
//       Main method." Which is only, because this file is not meant to be run by itself.

//-------------------------------------------------------------------------------------------------
#load "..\TypedEnums\TypedEnum"
#load ".\FeatureFlagConfig"
#load ".\FlagValueSource"
#load ".\FlagResolution"

//-------------------------------------------------------------------------------------------------
public sealed partial class FeatureFlag : TypedEnumGuid<FeatureFlag>
{
  private volatile bool _IsActiveDefault;
  
  public bool IsActive => GetIsActiveValue();  

  private FeatureFlag(Guid id, string description, string code = null, bool active_default = false)
    : base(id, description, code)
  {
    _IsActiveDefault = active_default;  //initial value
  }

  public override string ToString()
    => base.ToString("C");
  
  // FeatureFlag -> bool (implicit or explicit)
  [Obsolete("Prefer mcsEventType.Field.IsActive for clarity and future-proofing.", false)]
  public static implicit operator bool(FeatureFlag flag)
  {
    Initialize();
    
    return flag.IsActive;
  }
  
  /// <summary>
  /// Gets whether this feature flag is active for the specified tenant.
  /// Uses tenant-specific override if present, otherwise falls back to global/default.
  /// Use <c>null</c> or empty string for global/default value.
  /// </summary>
  /// <param name="tenant_code">The tenant code (case-sensitive), or null/empty for global.</param>
  /// <returns>true if the flag is active for the tenant (or globally), false otherwise.</returns>  
  public bool this[string? tenant_code]
  {
    get {
      if(string.IsNullOrWhiteSpace(tenant_code))
        return IsActive;
      
      return GetTenantValue(tenant_code);
    }
  }
  
  /// <summary>
  /// Gets whether this feature flag is active for the specified tenant.
  /// Uses tenant-specific override if present, otherwise falls back to global/default.
  /// Use <c>null</c> or empty string for global/default value.
  /// </summary>
  /// <param name="tenant_id">The tenant ID or 0 (zero) for global.</param>
  /// <returns>true if the flag is active for the tenant (or globally), false otherwise.</returns>  
  public bool this[int tenant_id]
  {
    get {
      if(tenant_id <= 0)
        return IsActive;
      
      string tenant_code = tenant_id.ToString();
      
      return GetTenantValue(tenant_code);
    }
  }
  
  private bool GetIsActiveValue()
  {
    var config = _cached_config;
    
    if(config is not null && 
       config.Global.TryGetValue(Code, out bool global_value))
    {
      return global_value;
    }
    
    return _IsActiveDefault;
  } 
  
  private bool GetTenantValue(string tenant_code)
  {        
    Initialize();
    
    var config = _cached_config;
    if (config is null)
      return IsActive;
  
    if(config.Tenants.TryGetValue(tenant_code, out var tenant_flags) &&
       tenant_flags.TryGetValue(Code, out bool is_active))
    {
      return is_active;
    }
    
    return IsActive;
  }
  
  public List<FlagResolution> Resolve(int tenant_id)
  {
    var tenant_code = tenant_id.ToString();
    return Resolve(tenant_code);
  }
  
  public List<FlagResolution> Resolve(string? tenant_code = null)
  {
    var priority = 0;
    var results  = new List<FlagResolution>();

    Initialize();
    var config = _cached_config;
    
    // tenant ...
    if( !string.IsNullOrWhiteSpace(tenant_code)                       && 
        config is not null                                            && 
        config.Tenants.TryGetValue(tenant_code, out var tenant_flags) &&
        tenant_flags.TryGetValue(Code, out bool tenant_value))
    {
      priority = 1;
      results.Add(new FlagResolution(Description, tenant_code, FlagValueSource.Tenant, priority, tenant_value));
    }    
    
    // global ...
    if(config is not null && 
       config.Global.TryGetValue(Code, out bool global_value))
    {
      priority = results.Any() ? 2 : 1;
      results.Add(new FlagResolution(Description, tenant_code, FlagValueSource.Global, priority, global_value));
    }

    // default ...
    priority = results.Any() ? results.Count() == 2 ? 3 : 2 : 1;
    results.Add(new FlagResolution(Description, tenant_code, FlagValueSource.Default, priority, _IsActiveDefault));

    return results;
  }
  
  #region internal service & auto-updating ...
  
  private static volatile FeatureFlagConfig? _cached_config;
  private static readonly object _config_lock = new();  
  private static volatile string? _config_path;
  
  private static FileSystemWatcher? _watcher;
  private static int _watcher_disposed; // 0 = active, 1 = disposed
  private static DateTime _last_load = DateTime.MinValue;
  private const int DEBOUNCE_MS = 150;

  /// <summary>
  /// Must be called once at application startup (or lazily on first use) ...
  /// It can be put call in the 'global.asax' page OR both methods with implicit operators ...
  /// </summary>
  public static void Initialize(string config_file_name = "FeatureFlags.json")
  {
    string file_path = @"D:\repos\randel-bjorkquist\LINQPad-Queries\FeatureFlags";
    
    if(_config_path is not null)
      return;
    
    lock(_config_lock)
    {
      if(_config_path is not null)
        return;
        
      Volatile.Write(ref _watcher_disposed, 0);
      
      //_config_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config_file_name);
      _config_path = Path.Combine(file_path, config_file_name);
      LoadConfiguration();
      
      var dir = Path.GetDirectoryName(_config_path);
      
      if (string.IsNullOrEmpty(dir)) 
        return;

      _watcher = new FileSystemWatcher(dir) { Filter              = Path.GetFileName(_config_path)
                                             ,NotifyFilter        = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName
                                             ,EnableRaisingEvents = true };
      
      _watcher.Changed += OnConfigFileChanged;
      _watcher.Created += OnConfigFileChanged;
      _watcher.Deleted += OnConfigFileChanged;
      
      _watcher.Renamed += OnConfigFileRenamed;
      _watcher.Error   += OnWatcherError;
      
      _watcher.InternalBufferSize = 64 * 1024;  // max 64KB
    }
  }
  
  private static void DebouncedReload()
  {
    var now = DateTime.UtcNow;
    
    if ((now - _last_load).TotalMilliseconds < DEBOUNCE_MS)
      return;
  
    lock (_config_lock)
    {
      if ((now - _last_load).TotalMilliseconds < DEBOUNCE_MS)
        return;
      
      LoadConfiguration();
      _last_load = now;
    }
  }
      
  private static void LoadConfiguration()
  {
    if (_config_path == null || !File.Exists(_config_path))
    {
      lock(_config_lock)
      {
        _cached_config = new FeatureFlagConfig(); // empty -> defaults apply
      }
      
      return;
    }

    string json = string.Empty;
    
    try
    {
      json = File.ReadAllText(_config_path);
    }
    catch { return; }

    FeatureFlagConfig? new_config = null;

    try
    {
      var options = new JsonSerializerOptions () { PropertyNameCaseInsensitive = true
                                                  ,WriteIndented               = true };
      
      new_config = JsonSerializer.Deserialize<FeatureFlagConfig>(json, options);
      
      if(new_config is null)
        return;
    }
    catch { return; }
  
    lock (_config_lock)
    {
      _cached_config = new_config;
    }
  }

  private static void OnConfigFileChanged(object? sender, FileSystemEventArgs e)
  {
    if(Volatile.Read(ref _watcher_disposed) == 1)
      return;
    
    DebouncedReload();
  }
  
  private static void OnConfigFileRenamed(object? sender, RenamedEventArgs e)
  {
    if(Volatile.Read(ref _watcher_disposed) == 1)
      return;

    DebouncedReload();
  }
  
  private static void OnWatcherError(object? sender, ErrorEventArgs e)
  {
    // buffer overflow is common under heavy file churn, safest
    // behavior: recreate watcher ...
    
    lock(_config_lock)
    {
      if(_watcher is null)
        return;
        
      _watcher.EnableRaisingEvents = false;
      _watcher.Dispose();
      _watcher = null;
      
      // re-init watcher (reusing current _config_path)
      if(_config_path is not null)
        Initialize(Path.GetFileName(_config_path));
    }
  }
  
  public static void DisposeWatcher()
  {
    //Get first: any late callbacks become no-ops
    Volatile.Write(ref _watcher_disposed, 1);
    
    lock(_config_lock)
    {
      if(_watcher is null)
        return;  
      
      _watcher.EnableRaisingEvents = false;
      _watcher.Changed -= OnConfigFileChanged;
      
      _watcher.Dispose();
      _watcher = null;
    }
  }
      
  #endregion
}
