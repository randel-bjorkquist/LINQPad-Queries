<Query Kind="Program">
  <NuGetReference>Azure.Core</NuGetReference>
  <NuGetReference>Azure.Identity</NuGetReference>
  <NuGetReference>Azure.Security.KeyVault.Administration</NuGetReference>
  <NuGetReference>Azure.Security.KeyVault.Secrets</NuGetReference>
  <NuGetReference>Microsoft.Extensions.Logging</NuGetReference>
  <NuGetReference>Microsoft.Extensions.Logging.Console</NuGetReference>
  <NuGetReference>NetLah.Extensions.Logging.Serilog</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Azure</Namespace>
  <Namespace>Azure.Core</Namespace>
  <Namespace>Azure.Core.Cryptography</Namespace>
  <Namespace>Azure.Core.Diagnostics</Namespace>
  <Namespace>Azure.Core.Extensions</Namespace>
  <Namespace>Azure.Core.GeoJson</Namespace>
  <Namespace>Azure.Core.Pipeline</Namespace>
  <Namespace>Azure.Core.Serialization</Namespace>
  <Namespace>Azure.Identity</Namespace>
  <Namespace>Azure.Messaging</Namespace>
  <Namespace>Azure.Security.KeyVault.Administration</Namespace>
  <Namespace>Azure.Security.KeyVault.Secrets</Namespace>
  <Namespace>Microsoft.Extensions.Azure</Namespace>
  <Namespace>Microsoft.Extensions.Logging</Namespace>
  <Namespace>Microsoft.Extensions.Logging.Console</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main(string[] args)
{
  var logger = LoggerFactory.Create(builder => {builder.AddConsole(); })
                            .CreateLogger<SecretsService>();
                            
//  var logger_factory = LoggerFactory.Create(builder => { builder.AddConsole(); });
//  var logger = logger_factory.CreateLogger<SecretService>();
  
  var mattang_keyvault_service = new MattangVaultSecretsService(logger);
  var show_secret = new Dictionary<string, bool>(){ {MattangVaultSecretNames.DEV_Expiration_Date            ,false } // simply a temp secret, may be deleted at any time
                                                   ,{MattangVaultSecretNames.DEV_Number_Of_Retries          ,false } // simply a temp secret, may be deleted at any time
                                                   ,{MattangVaultSecretNames.MLSE_SQL_PROD                  ,true  }
                                                   ,{MattangVaultSecretNames.MLSW_SQL_DEV                   ,false }
                                                   ,{MattangVaultSecretNames.Pipeline_Import                ,true  }
                                                   ,{MattangVaultSecretNames.Pipeline_Key_Vault_Access      ,false }
                                                   ,{MattangVaultSecretNames.SWTX_Salesforce_Configuration  ,true  } 
                                                   ,{MattangVaultSecretNames.SWTX_Salesforce_Integration    ,true  } 
                                                   ,{MattangVaultSecretNames.SWTX_Salesforce_Sandbox        ,true  }
                                                   ,{MattangVaultSecretNames.Not_A_Real_Secret_Name         ,false } };
  
  #region GetDevExpirationDate ---------------------------------------------------------------------------------
  
  if(show_secret[MattangVaultSecretNames.DEV_Expiration_Date])
  {
    mattang_keyvault_service.GetDevExpirationDate()
                            .Dump("mattang_keyvault_service.GetDevExpirationDate()", 0);
  }

  #endregion

  #region GetDevExpirationDate ---------------------------------------------------------------------------------

  if(show_secret[MattangVaultSecretNames.DEV_Expiration_Date])
  {
    mattang_keyvault_service.GetDevNumberOfRetries()
                            .Dump("mattang_keyvault_service.GetDevNumberOfRetries()", 0);
  }

  #endregion

  #region GetMlseSqlProd ---------------------------------------------------------------------------------------
  
  if(show_secret[MattangVaultSecretNames.MLSE_SQL_PROD])
  {
    mattang_keyvault_service.GetMlseSqlProd()
                            .Dump("mattang_keyvault_service.GetMlseSqlProd()", 0);
  }  

  #endregion

  #region GetMlswSqlDev ----------------------------------------------------------------------------------------
  
  if(show_secret[MattangVaultSecretNames.MLSW_SQL_DEV])
  {
    mattang_keyvault_service.GetMlswSqlDev()
                            .Dump("mattang_keyvault_service.GetMlswSqlDev()", 0);
  }  
  
  #endregion
  
  #region GetPipelineKeyVaultAccess ----------------------------------------------------------------------------
  
  if(show_secret[MattangVaultSecretNames.Pipeline_Key_Vault_Access])
  {
    mattang_keyvault_service.GetPipelineKeyVaultAccess()
                            .Dump("mattang_keyvault_service.GetPipelineKeyVaultAccess()", 0);
  }  

  #endregion
  
  #region GetPipelineImport ------------------------------------------------------------------------------------
  
  if(show_secret[MattangVaultSecretNames.Pipeline_Import])
  { 
    mattang_keyvault_service.GetPipelineImport()
                            .Dump("mattang_keyvault_service.GetPipelineImport()", 0);
  }  
  
  #endregion
  
  #region GetSWTXSalesforceSandbox -----------------------------------------------------------------------------
  
  if(show_secret[MattangVaultSecretNames.SWTX_Salesforce_Sandbox])
  {    
    mattang_keyvault_service.GetSWTXSalesforceSandbox()
                            .Dump("mattang_keyvault_service.GetSWTXSalesforceSandbox()", 0);
  }

  #endregion

  var swtx_keyvault_service = new SWTXVaultSecretsService(logger);

  #region GetSWTXSalesforceConfiguration -----------------------------------------------------------------------

  if(show_secret[MattangVaultSecretNames.SWTX_Salesforce_Sandbox])
  {
    swtx_keyvault_service.GetSWTXSalesforceConfiguration()
                         .Dump("swtx_keyvault_service.GetSWTXSalesforceConfiguration()", 0);
  }
  
  #endregion
  
  #region GetSWTXSalesforceIntegration -------------------------------------------------------------------------

  if(show_secret[MattangVaultSecretNames.SWTX_Salesforce_Sandbox])
  {
    swtx_keyvault_service.GetSWTXSalesforceIntegration()
                         .Dump("swtx_keyvault_service.GetSWTXSalesforceIntegration", 0);  
  }
  
  #endregion
  
  //NOTE: Returns Request Failed Exception ...
  #region ReturnRequestFailedException -------------------------------------------------------------------------

  if(show_secret[MattangVaultSecretNames.Not_A_Real_Secret_Name])
  {
    swtx_keyvault_service.ReturnRequestFailedException()
                         .Dump("swtx_keyvault_service.ReturnRequestFailedException()", 0);  
  }
  
  #endregion
    
  logger.LogInformation("ALL DONE: unless something is 'awaiting...'");
}

#region Service Layer Code ========================================================================================

public abstract class SecretsService
{  
  public virtual string KeyVaultName  => null;
  public virtual string KeyVaultUrl   => $"https://{KeyVaultName}.vault.azure.net";
  public virtual string Name          => "Mattang.Common.Azure.KeyVault.Secrets.SecretService";
  
  protected readonly SecretClient _client = null;
  protected readonly ILogger _logger      = null;
  
  //NOTE: this is only here for testing purposes, no class should implement this
  internal SecretsService(SecretClient client, ILogger logger)
  {
    if(client == null)
      throw new ArgumentNullException("The 'client' argument cannot be null!");

    _client = client;
    _logger = logger;
  }

  public SecretsService(ILogger logger)
  {
      if(string.IsNullOrWhiteSpace(KeyVaultName))
        throw new ArgumentNullException("The 'Key Vault Name' cannot be null, empty, or contain only whitespaces!");
      
    _client = new SecretClient( new Uri(KeyVaultUrl)
                               ,new DefaultAzureCredential() );
    _logger = logger;
  }
  
  //NOTE: this is here as a test for when a secret is not found ...
  public virtual string ReturnRequestFailedException()
  {
    return GetSecret("not-a-real-secret-name").GetAwaiter()
                                              .GetResult();
  }
  
  internal async Task<T> GetSecret<T>(string secret_name)
  {
    var value = await GetSecret(secret_name);

    return value != null
            ? JsonConvert.DeserializeObject<T>(value)
            : default;
  }
  
  internal async Task<string> GetSecret(string secret_name)
  {
    var logger_prefix = $"{Name}: \n\t -";
    
    if(_logger == null)
      throw new ArgumentNullException($"{logger_prefix} The 'logger' cannot be null!");      

    Azure.Response<KeyVaultSecret> secret;
    
    try
    {
      if(string.IsNullOrWhiteSpace(secret_name))
        throw new ArgumentNullException("The 'Secret Name' cannot be null, empty, or contain only whitespaces!");
      
      Console.Write($"{logger_prefix} Retrieving the Secret '{secret_name}' from {KeyVaultName} ... ");

      secret = await _client.GetSecretAsync(secret_name);

      Console.Write("... RETRIEVED");
    }
    catch(RequestFailedException ex) when (ex.Status == 404)
    {
      secret = null;
      _logger.LogError( ex
                       ,$"{logger_prefix} A Request Failed Exception was returned and logged."
                       ,new object[] { "source", Name } );
      
      //var secret_name   = "any_valid_secret_name";
      //var secret_value  = new KeyVaultSecret(secret_name, "any_secret_value");
      //secret = Azure.Response.FromValue<KeyVaultSecret>(null, default);
      //secret.HasValue = false;
      
      //TODO: figure out how to return a "default" Azure.Response<KeyVaultSecret>(), with a .HasValue of 'false' ...
      //secret = new Azure.Response<KeyVaultSecret>().Value = null;
      //secret = new Azure.Response<KeyVaultSecret>(System.Net.HttpStatusCode.NotFound
      //                                            ,new System.Net.Http.Headers.HttpResponseHeaders()
      //                                                                        .Add("my-header", "header-value")
      //                                            ,null );
    }
    catch(Exception ex)
    {
      secret = null;
      _logger.LogError( ex
                       ,$"{logger_prefix} An exception occurred and logged."
                       ,new object[] { "source", Name } );
      
      throw;
    }

    //TODO: remove once the below GetSecret() method, which actually retrieves the Secret, returns
    //      a "default" secret with a .HasValue of 'false' ...
    //return secret.HasValue == false
    return secret == null || secret.HasValue == false
              ? null
              : secret.Value.Value;
  }  
  
#region COMMENTED OUT: ORIGINAL CODE  
/*  
  protected async Task<string> GetSecret(string secret_name)
  {
    client ??= new SecretClient( new Uri(KeyVaultUrl)
                                ,new DefaultAzureCredential() );
    
    var secret = await GetSecret(client, secret_name);
    
    //TODO: remove once the below GetSecret() method, which actually retrieves the Secret, returns
    //      a "default" secret with a .HasValue of 'false' ...
    if(secret == null)
      return null;
    
    return secret.HasValue
              ? secret.Value.Value
              : null;
  }
  
  private async Task<Azure.Response<KeyVaultSecret>> GetSecret( SecretClient client
                                                               ,string secret_name )
  {
    var logger_prefix = $"{Name}: \n\t -";
    
    if(_logger == null)
      throw new ArgumentNullException($"{logger_prefix} The 'logger' cannot be null!");      

    Azure.Response<KeyVaultSecret> secret;
    
    try
    {
      if(string.IsNullOrWhiteSpace(KeyVaultName))
        throw new ArgumentNullException("The 'Key Vault Name' cannot be null, empty, or contain only whitespaces!");
  
      if(string.IsNullOrWhiteSpace(secret_name))
        throw new ArgumentNullException("The 'Secret Name' cannot be null, empty, or contain only whitespaces!");
      
      Console.Write($"{logger_prefix} Retrieving the Secret '{secret_name}' from {KeyVaultName} ... ");

      secret = await client.GetSecretAsync(secret_name);

      Console.Write("... RETRIEVED");
    }
    catch(RequestFailedException ex) when (ex.Status == 404)
    {
      secret = null;
      _logger.LogError(ex, $"{logger_prefix} A Request Failed Exception was returned and logged." );
      
      //TODO: figure out how to return a "default" Azure.Response<KeyVaultSecret>(), with a .HasValue of 'false' ...
      //secret = new Azure.Response<KeyVaultSecret>().Value = null;
      //secret = new Azure.Response<KeyVaultSecret>(System.Net.HttpStatusCode.NotFound
      //                                            ,new System.Net.Http.Headers.HttpResponseHeaders()
      //                                                                        .Add("my-header", "header-value")
      //                                            ,null );
    }
    catch(Exception ex)
    {
      secret = null;
      _logger.LogError(ex, $"{logger_prefix} An exception occurred and logged.");
      
      throw;
    }

    return secret;
  }  
*/  
#endregion  
}

public class MattangVaultSecretsService : SecretsService
{
  public override string KeyVaultName => "MattangVault";
  public override string Name         => "Mattang.Common.Azure.KeyVault.Secrets.MattangVaultSecretsService";

  public MattangVaultSecretsService(ILogger logger) 
    : base(logger) { } 
  
  public DateOnly GetDevExpirationDate()
  {
    var value = base.GetSecret(MattangVaultSecretNames.DEV_Expiration_Date)
                      .GetAwaiter()
                      .GetResult();
                                                                      
    DateOnly.TryParse(value, out var result);
    
    //NOTE: when the DateOnly.TryParse method cannot convert the value to a valid DateOnly value
    //      it returns the value '01/01/0001' ... so if that gets returned, throw the
    //      ArgumentOutOfRangeException
    if(result == new DateOnly(1,1,1))
      throw new ArgumentOutOfRangeException($"ERROR: '{value}' is not a valid DateOnly value.");
    
    return result;
  }

  public int GetDevNumberOfRetries()
  {
    var value = base.GetSecret(MattangVaultSecretNames.DEV_Number_Of_Retries)
                      .GetAwaiter()
                      .GetResult();
    
    int.TryParse(value, out var result);
    
    //NOTE: when the int.TryParse method cannot convert the value to a valid Integer value
    //      it returns the value '0' ... so if that gets parsed AND 'value' was not a zero, 
    //      throw the ArgumentOutOfRangeException
    if(result.ToString() != value)
      throw new ArgumentOutOfRangeException($"ERROR: '{value}' is not a valid Integer value.");
      
    return result;
  }

  public DBConnectionModel GetMlseSqlProd()
  {
    return base.GetSecret<DBConnectionModel>(MattangVaultSecretNames.MLSE_SQL_PROD)
                  .GetAwaiter()
                  .GetResult();
  }

  public DBConnectionModel GetMlswSqlDev()
  {
    return base.GetSecret<DBConnectionModel>(MattangVaultSecretNames.MLSW_SQL_DEV)
                  .GetAwaiter()
                  .GetResult();
  }

  public string GetPipelineKeyVaultAccess()
  {
    return base.GetSecret(MattangVaultSecretNames.Pipeline_Key_Vault_Access)
                  .GetAwaiter()
                  .GetResult();
  }

  public string GetPipelineImport()
  {
    return base.GetSecret(MattangVaultSecretNames.Pipeline_Import)
                  .GetAwaiter()
                  .GetResult();
  }

  public SWTXSalesforceModel GetSWTXSalesforceSandbox()
  {
    return base.GetSecret<SWTXSalesforceModel>(MattangVaultSecretNames.SWTX_Salesforce_Sandbox)
                  .GetAwaiter()
                  .GetResult();
  }
}

public class SWTXVaultSecretsService : SecretsService
{
  //NOTE: yeah, I know ... I'm using the same name, but I only have access to one Key Vault right now
  public override string KeyVaultName => "MattangVault";
  public override string Name         => "Mattang.Common.Azure.KeyVault.Secrets.SWTXVaultSecretsService";
  
  public SWTXVaultSecretsService(ILogger logger) 
    : base(logger) { }
  
  public SWTXSalesforceIntegrationModel GetSWTXSalesforceIntegration()
  {
    return base.GetSecret<SWTXSalesforceIntegrationModel>(MattangVaultSecretNames.SWTX_Salesforce_Sandbox)
                  .GetAwaiter()
                  .GetResult();
  }

  public SalesforceConfigurationModel GetSWTXSalesforceConfiguration()
  {
    return base.GetSecret<SalesforceConfigurationModel>(MattangVaultSecretNames.SWTX_Salesforce_Sandbox)
                  .GetAwaiter()
                  .GetResult();
  }
}

public class DBConnectionModel
{
  public string host     { get; set; }
  public string port     { get; set; }
  
  public string database { get; set; }
  
  public string username { get; set; }
  public string password { get; set; }
}

public class SWTXSalesforceModel
{
  public string username                    { get; set; }
  public string password                    { get; set; }
  public string consumer_key                { get; set; }
  public string consumer_secret             { get; set; }
  
  public string base_url                    { get; set; }
  public string connected_app_customer_key  { get; set; }
  public string jwt_passphrase              { get; set; }
  
  public string api_version                 { get; set; }
  public string is_sandbox                  { get; set; }
}

public class SWTXSalesforceIntegrationModel
{
  public string username                    { get; set; }
  public string password                    { get; set; }
  public string consumer_key                { get; set; }
  public string consumer_secret             { get; set; }
  
  public string base_url                    { get; set; }
  public string connected_app_customer_key  { get; set; }
  public string jwt_passphrase              { get; set; }
  
  public string api_version                 { get; set; }
  public string is_sandbox                  { get; set; }
}

public class SalesforceConfigurationModel
{
  public string username                    { get; set; }
  public string password                    { get; set; }
  
  public string consumer_key                { get; set; }
  public string consumer_secret             { get; set; }
  
  public string base_url                    { get; set; }
  public string api_version                 { get; set; }
}

/// <summary>
/// These are the actual names of the secrets within the MattangVault/Secrets, they are case insensitive.
/// When adding a new secret, make sure there's an associated entry in Secrets/Models which represent
/// the secret's "value".
/// 
/// Example:
///   - the values of both 'mlse-sql-prod' and 'mlsw-sql-dev' use the 'DBConnectionModel'
///   - the value of 'swtx-salesforce-sandbox' uses 'SWTXSalesforceModel'
/// Exception(s):
///   - since the values of both 'pipeline-import' and 'pipeline-key-vault-access' are simple strings,
///     there's no reason to have have to convert it into a model.
///   - other simple types like, like numeric, "may not" require conversions like the complex types.
/// </summary>
public static class MattangVaultSecretNames
{
  public static string DEV_Expiration_Date            => nameof(DEV_Expiration_Date).ToLower().Replace('_', '-');
  public static string DEV_Number_Of_Retries          => nameof(DEV_Number_Of_Retries).ToLower().Replace('_', '-');
                                                      
  public static string MLSE_SQL_PROD                  => nameof(MLSE_SQL_PROD).ToLower().Replace('_', '-');
  public static string MLSW_SQL_DEV                   => nameof(MLSW_SQL_DEV).ToLower().Replace('_', '-');
                                                      
  public static string SWTX_Salesforce_Configuration  => nameof(SWTX_Salesforce_Configuration).ToLower().Replace('_', '-');
  public static string SWTX_Salesforce_Integration    => nameof(SWTX_Salesforce_Integration).ToLower().Replace('_', '-');  
  public static string SWTX_Salesforce_Sandbox        => nameof(SWTX_Salesforce_Sandbox).ToLower().Replace('_', '-');
  
  public static string Pipeline_Import                => nameof(Pipeline_Import).ToLower().Replace('_', '-');
  public static string Pipeline_Key_Vault_Access      => nameof(Pipeline_Key_Vault_Access).ToLower().Replace('_', '-');
  
  public static string Not_A_Real_Secret_Name         => nameof(Not_A_Real_Secret_Name).ToLower().Replace('_', '-');
}

#endregion

#region COMMENTED OUT: Sample Code from Azure Portal (includes adds, edits, and deletes)
/*
using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace key_vault_console_app
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string secretName = "SECRET_NAME";
            var keyVaultName = "KV_NAME"
            var kvUri = $"https://{keyVaultName}.vault.azure.net";

            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

            var secretValue = "SECRET_VALUE"

            Console.Write($"Creating a secret in {keyVaultName} called '{secretName}' with the value '{secretValue}' ...");
            await client.SetSecretAsync(secretName, secretValue);
            Console.WriteLine(" done.");

            Console.WriteLine("Forgetting your secret.");
            secretValue = string.Empty;
            Console.WriteLine($"Your secret is '{secretValue}'.");

            Console.WriteLine($"Retrieving your secret from {keyVaultName}.");
            var secret = await client.GetSecretAsync(secretName);
            Console.WriteLine($"Your secret is '{secret.Value.Value}'.");

            Console.Write($"Deleting your secret from {keyVaultName} ...");
            DeleteSecretOperation operation = await client.StartDeleteSecretAsync(secretName);
            // You only need to wait for completion if you want to purge or recover the secret.
            await operation.WaitForCompletionAsync();
            Console.WriteLine(" done.");

            Console.Write($"Purging your secret from {keyVaultName} ...");
            await client.PurgeDeletedSecretAsync(secretName);
            Console.WriteLine(" done.");
        }
    }
}
*/
#endregion
