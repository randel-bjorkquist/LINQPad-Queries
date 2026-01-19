<Query Kind="Program" />

#load ".\TypedEnum"

void Main()
{
  #region COMMENTED OUT: R&D CODE ...

  //var service = new FeatureFlagService("FeatureFlags.json");
  //service.IsActive("InspectionSaved").Dump();  // true  

  //NOTE: Make sure the JSON file exists in the same folder as your 
  //      LINQPad query (or adjust the path/filename below if needed) ...
  //FeatureFlag.Initialize("featureflags.json");
  //FeatureFlag.Initialize();

  var flag = FeatureFlag.CustomReports;
  flag.Dump("var flag = mcsFeatureFlag.CustomReports;", 0);
  
  //flag = FeatureFlag.CustomReports["TenantA"];
  
  if (FeatureFlag.CustomReports)
    $"FeatureFlag.CustomReports is active."
      .Dump("if (FeatureFlag.CustomReports)");
  else
    $"FeatureFlag.CustomReports is 'not' active."
      .Dump("if (FeatureFlag.CustomReports)");
  
  FeatureFlag.NewPaymentFlow.Dump("FeatureFlag.NewPaymentFlow");

  if(FeatureFlag.NewPaymentFlow)
    $"FeatureFlag.NewPaymentFlow is active."
      .Dump("if (FeatureFlag.NewPaymentFlow)");
  else
    $"FeatureFlag.NewPaymentFlow is not active."
      .Dump("if (FeatureFlag.NewPaymentFlow)");
  
  
  
  var tenant_code = "TenantA";
  tenant_code.Dump("var tenant_code = \"TenantA\";");

  if (FeatureFlag.NewPaymentFlow[tenant_code])
    $"FeatureFlag.NewPaymentFlow for '{tenant_code}' is active."
      .Dump($"if (FeatureFlag.NewPaymentFlow[{tenant_code}])");
  else
    $"FeatureFlag.NewPaymentFlow for '{tenant_code}' is 'not' active."
      .Dump($"if (FeatureFlag.NewPaymentFlow[{tenant_code}])");
  
  
  
  var pha_id = 1190;  // pulled from state ...
  var event_type = HousingAuthority.GetByID(pha_id);
  var code = event_type.Code;

  var feature_flag = FeatureFlag.NewPaymentFlow[code];
  feature_flag.Dump($"var feature_flag = FeatureFlag.NewPaymentFlow[{code}];");

  //if(FeatureFlag.NewPaymentFlow[HousingAuthority.GetByID(1190).Code])
  if (FeatureFlag.NewPaymentFlow[code])
    $"FeatureFlag.NewPaymentFlow for '{event_type.ToString()}' is active."
      .Dump($"if (FeatureFlag.NewPaymentFlow[{code}])");
  else
    $"FeatureFlag.NewPaymentFlow for '{event_type.ToString()}' is 'not' active."
      .Dump($"if (FeatureFlag.NewPaymentFlow[{code}])");
  
  
  if (FeatureFlag.NewPaymentFlow[1190])
    $"FeatureFlag.NewPaymentFlow for '{event_type.ToString()}' is active."
      .Dump("if (FeatureFlag.NewPaymentFlow[1190])");
  else
    $"FeatureFlag.NewPaymentFlow for '{event_type.ToString()}' is 'not' active."
      .Dump("if (FeatureFlag.NewPaymentFlow[1190])");
  
  
  var flags = FeatureFlag.GetAll();
  flags.Dump("var flags = mcsFeatureFlag.GetAll();", 0);  

  FeatureFlag.CustomReports.Resolve("TenantA").Dump("FeatureFlag.CustomReports.Resovle('TenantA')", 0);
  FeatureFlag.NewPaymentFlow.Resolve("TenantA").Dump("FeatureFlag.NewPaymentFlow.Resovle('TenantA')", 0);
  FeatureFlag.AdvancedReporting.Resolve("TenantA").Dump("FeatureFlag.AdvancedReporting.Resovle('TenantA')", 0);
  
  FeatureFlag.CustomReports.Resolve("TenantB").Dump("FeatureFlag.CustomReports.Resovle('TenantB')", 0);
  FeatureFlag.NewPaymentFlow.Resolve("TenantB").Dump("FeatureFlag.NewPaymentFlow.Resovle('TenantB')", 0);
  FeatureFlag.AdvancedReporting.Resolve("TenantB").Dump("FeatureFlag.AdvancedReporting('TenantB')", 0);

  FeatureFlag.CustomReports.Resolve(1190).Dump("FeatureFlag.CustomReports.Resovle(1190)", 0);
  FeatureFlag.CustomReports.Resolve("Demonstration").Dump("FeatureFlag.CustomReports.Resovle('Demonstration')", 0);

  #endregion
  
  #region COMMENTED OUT: looping via while(true) ...
//  
//  "Feature Flag Test - Press Enter to refresh, Q to quit".Dump();
//  
//  while (true)
//  {
//    Console.WriteLine("\nCurrent 'global' state (as of " + DateTime.Now.ToLongTimeString() + "):");
//    Console.WriteLine("--------------------------------------------------");
//  
//    DumpFlag(FeatureFlag.CustomReports);
//    DumpFlag(FeatureFlag.NewPaymentFlow);
//    DumpFlag(FeatureFlag.AdvancedReporting);
//  
//    Console.WriteLine("\nTenant-specific examples:");
//    Console.WriteLine("--------------------------------------------------");
//    
//    Console.WriteLine($"CustomReports     for TenantA: {FeatureFlag.CustomReports["TenantA"]}");
//    Console.WriteLine($"NewPaymentFlow    for TenantA: {FeatureFlag.NewPaymentFlow["TenantA"]}");
//    Console.WriteLine($"AdvancedReporting for TenantA: {FeatureFlag.AdvancedReporting["TenantA"]}");
//    
//    Console.WriteLine("-------------------------------------");
//    
//    Console.WriteLine($"CustomReports     for TenantB: {FeatureFlag.CustomReports["TenantB"]}");
//    Console.WriteLine($"NewPaymentFlow    for TenantB: {FeatureFlag.NewPaymentFlow["TenantB"]}");
//    Console.WriteLine($"AdvancedReporting for TenantB: {FeatureFlag.AdvancedReporting["TenantB"]}");
//
//    //Console.WriteLine("--------------------------------------------------");
//    //
//    //FeatureFlag.CustomReports.Resovle("TenantA").Dump("FeatureFlag.CustomReports.Resovle(\"TenantA\")");
//    //FeatureFlag.NewPaymentFlow.Resovle("TenantA").Dump("FeatureFlag.CustomReports.Resovle(\"TenantA\")");
//    //FeatureFlag.AdvancedReporting.Resovle("TenantA").Dump("FeatureFlag.CustomReports.Resovle(\"TenantA\")");
//    //
//    //Console.WriteLine("-------------------------------------");
//    //
//    //FeatureFlag.CustomReports.Resovle("TenantB").Dump("FeatureFlag.CustomReports.Resovle(\"TenantB\")");
//    //FeatureFlag.NewPaymentFlow.Resovle("TenantB").Dump("FeatureFlag.CustomReports.Resovle(\"TenantB\")");
//    //FeatureFlag.AdvancedReporting.Resovle("TenantB").Dump("FeatureFlag.CustomReports.Resovle(\"TenantB\")");
//
//    Console.Write("\nPress Enter to refresh, Q to quit: ");
//    var key = Console.Read();
//    if (key == 'q' || key == 'Q') break;
//    
//    Console.WriteLine();
//    
//    // Optional: force reload (mainly for debugging)
//    //FeatureFlag.Initialize();
//  }
//  
//  "Done. You can now edit featureflags.json and re-run or continue watching.".Dump();  
//
  #endregion
}

private void DumpFlag(FeatureFlag flag)
{
  Console.WriteLine( $"{flag.Code,-18}  IsActive: {flag.IsActive,5} → bool: {(bool)flag}");
}

public sealed class FeatureFlag : TypedEnumGuid<FeatureFlag>
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
    //return flag is null ? throw new ArgumentNullException(nameof(flag)) : flag.IsActive;
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
      if(string.IsNullOrEmpty(tenant_code?.Trim()))
        return IsActive;
      
      return GetTenantValue(tenant_code);
        
      //Initialize();
      //
      //var config = _cached_config;
      //if (config is null)
      //  return IsActive;
      //
      //if(config.Tenants.TryGetValue(tenant_code, out var tenant_flags) &&
      //   tenant_flags.TryGetValue(Code, out bool is_active))
      //{
      //  return is_active;
      //}
      //
      //return IsActive;
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
      
      string tenant_code = $"{tenant_id}";
      
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
    var tenant_code = $"{tenant_id}";
    return Resolve(tenant_code);
  }
  
  public List<FlagResolution> Resolve(string? tenant_code = null)
  {
    var priority = 0;
    var results  = new List<FlagResolution>();

    Initialize();
    var config = _cached_config;
    
    // tenant ...
    if( !string.IsNullOrEmpty(tenant_code.Trim())
     && config is not null
     && config.Tenants.TryGetValue(tenant_code, out var tenant_flags)
     && tenant_flags.TryGetValue(Code, out bool tenant_value))
    {
      priority = 1;
      results.Add(new FlagResolution(Description, tenant_code, FlagValueSource.Tenant, priority, tenant_value));
    }    
    
    // global ...
    if(config.Global.TryGetValue(Code, out bool global_value))
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
    string file_path = @"D:\repos\randel-bjorkquist\LINQPad-Queries\TypedEnums & TypedFlags";
    
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
      
      //foreach (var flag in GetAll())
      //{
      //  if (_cached_config.Global.TryGetValue(flag.Code, out bool value))
      //  {
      //    flag.IsActive = value;
      //  }
      //  //else: keep current/default (constructor default or previous override)
      //}
    }
  }

  private static void OnConfigFileChanged(object? sender, FileSystemEventArgs e)
  {
    if (Volatile.Read(ref _watcher_disposed) == 1)
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
      if (_watcher is null)
        return;  
      
      _watcher.EnableRaisingEvents = false;
      _watcher.Changed -= OnConfigFileChanged;
      
      _watcher.Dispose();
      _watcher = null;
    }
  }
      
  #endregion
  
  #region specific FeatureFlag definitions ...
  
  public static readonly FeatureFlag CustomReports      = new(Guid.Parse("00000000-0000-0000-0000-000000000000") ,"Customer Reports"   ,nameof(CustomReports));
  public static readonly FeatureFlag NewPaymentFlow     = new(Guid.Parse("00000000-0000-0000-0000-000000000001") ,"New Payment Flow"   ,nameof(NewPaymentFlow));
  public static readonly FeatureFlag AdvancedReporting  = new(Guid.Parse("00000000-0000-0000-0000-000000000002") ,"Advanced Reporting" ,nameof(AdvancedReporting));
  
  //public static readonly FeatureFlag CustomReports      = new(Guid.Parse("00000000-0000-0000-0000-000000000000") ,"Customer Reports"   ,nameof(CustomReports)     ,true);
  //public static readonly FeatureFlag NewPaymentFlow     = new(Guid.Parse("00000000-0000-0000-0000-000000000001") ,"New Payment Flow"   ,nameof(NewPaymentFlow)    ,true);
  //public static readonly FeatureFlag AdvancedReporting  = new(Guid.Parse("00000000-0000-0000-0000-000000000002") ,"Advanced Reporting" ,nameof(AdvancedReporting) ,true);
  
  #endregion
}

public class FeatureFlagConfig
{
  public Dictionary<string, bool> Global                      { get; set; } = new();  // key = TypedEnum.Code (field name)
  public Dictionary<string, Dictionary<string, bool>> Tenants { get; set; } = new();  // key = TypedEnum.Code (field name)
}

public enum FlagValueSource { Default, Global, Tenant }
public readonly record struct FlagResolution(string FeatureFlag, string? TenantCode, FlagValueSource Source, int Priority, bool Value);

public sealed class HousingAuthority : TypedEnumInt<HousingAuthority>
{
  private HousingAuthority (int id, string description, string code)
    : base(id, description, code) { }
    
  public static readonly HousingAuthority Demonstration = new(1190, "Demonstration (for demos and/or dev)", nameof(Demonstration));
}


#region COMMENTED OUT: original code (R&D code)
/**************************************************************************************************

// interface ----------------------------------------------------------------------------
public interface IFeatureFlagService
{
    bool IsActive(string code);
    bool IsActive<TEnum>(TEnum flag) where TEnum : TypedEnumGuid<TEnum>;
    //bool IsActive<TEnum>(TEnum flag) where TEnum : TypedEnum<TEnum, Tid>, Tid : notnull;
}

public class FeatureFlagService : IDisposable, IFeatureFlagService
{
  private readonly string   _config_path;
  private FileSystemWatcher _watcher;
  
  private Dictionary<string, bool> _flags = new Dictionary<string, bool>();
  
  public FeatureFlagService(string config_path = "FeatureFlags")
  {
    _config_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config_path);
    //LoadConfiguration();

    //Watch for changes
    _watcher = new FileSystemWatcher(Path.GetDirectoryName(_config_path)!) { Filter       = Path.GetFileName(_config_path)
                                                                            ,NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName };
    
    _watcher.Changed += (s, e) => LoadConfiguration();
    _watcher.EnableRaisingEvents = true;
  }
  
  private void LoadConfiguration()
  {
    //if (!File.Exists(_config_path))
    //{ 
    //  _flags = new Dictionary<string, bool>();
    //  return;
    //}
    
    var json_text = File.ReadAllText(_config_path);
    var config    = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, bool>>>(json_text);
    
    _flags = config["Flags"] ?? new Dictionary<string, bool>();  // assume "Flags" section
  }
  
  public bool IsActive(string code)
    => _flags.TryGetValue(code, out var value) ? value : false; // global default false
  
  
  //public bool IsActive<TEnum>(TEnum flag) where TEnum : TypedEnum<TEnum, Tid>, Tid : notnull
  
  public bool IsActive<TEnum>(TEnum flag) where TEnum : TypedEnumGuid<TEnum>
  {
    // First try config, then fall back to code-defined default (if you keep it)
    if (IsActive(flag.Code)) return true;

    // If you want code-defined default (constructor active = true), add a way to store it
    // For now, with global false, this returns false if missing
    return false;    
  }
  
  public void Dispose() => _watcher?.Dispose();
}

**************************************************************************************************/
#endregion