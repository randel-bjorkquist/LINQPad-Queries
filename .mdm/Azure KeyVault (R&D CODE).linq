<Query Kind="Program">
  <NuGetReference>Azure.Core</NuGetReference>
  <NuGetReference>Azure.Identity</NuGetReference>
  <NuGetReference>Azure.Security.KeyVault.Administration</NuGetReference>
  <NuGetReference>Azure.Security.KeyVault.Secrets</NuGetReference>
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
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

//async void Main(string[] args)
void Main(string[] args)
{
  #region Method Variable(s) -----------------------------------------------------------------------------------

  var show_response = true;
  var show_secret   = new Dictionary<string, bool>(){ {MattangVaultSecretNames.DEV_Expiration_Date        ,true } // simply a temp secret, may be deleted at any time
                                                     ,{MattangVaultSecretNames.DEV_Number_Of_Retries      ,true } // simply a temp secret, may be deleted at any time
                                                     ,{MattangVaultSecretNames.MLSE_SQL_PROD              ,true }
                                                     ,{MattangVaultSecretNames.MLSW_SQL_DEV               ,true }
                                                     ,{MattangVaultSecretNames.Pipeline_Import            ,true }
                                                     ,{MattangVaultSecretNames.Pipeline_Key_Vault_Access  ,true }
                                                     ,{MattangVaultSecretNames.SWTX_Salesforce_Sandbox    ,true } };
                   
  //NOTE: I still have questions around this 'DefaultAzureCredential()' method ... might require an account with
  //      read-only permissions, kind of like a Service Account.
  //var client = new SecretClient(new Uri(KeyVaultUrl), new DefaultAzureCredential());
  
  #endregion
  
  #region GetDevExpirationDate ---------------------------------------------------------------------------------
  
  if(show_secret[MattangVaultSecretNames.DEV_Expiration_Date])
  {
    var dev_expiration_date = GetDevExpirationDate();
    dev_expiration_date.Dump("dev_expiration_date", 0);    
  }

  #endregion

  #region GetDevExpirationDate ---------------------------------------------------------------------------------

  if (show_secret[MattangVaultSecretNames.DEV_Expiration_Date])
  {
    var dev_number_of_retries = GetDevNumberOfRetries();
    dev_number_of_retries.Dump("dev_number_of_retries", 0);
  }

  #endregion

  #region GetMlseSqlProd ---------------------------------------------------------------------------------------
  
  if(show_secret[MattangVaultSecretNames.MLSE_SQL_PROD])
  {
    var mlse_sql_prod = GetMlseSqlProd();
    mlse_sql_prod.Dump("mlse_sql_prod", 0);
    
    #region COMMENTED OUT: ORIGINAL R&D CODE
    //    
    //var mlse_sql_prod = GetMlseSqlProd(client).GetAwaiter()
    //                                          .GetResult();
    //if(show_response)
    //  mlse_sql_prod.Dump("mlse_sql_prod.Value", 0);
    //
    //if(mlse_sql_prod.HasValue)
    //{
    //  var mlse_sql_prod_value = mlse_sql_prod.Value;
    //  mlse_sql_prod_value.Dump("mlse_sql_prod.Value", 0);
    //
    //  var json = JsonConvert.DeserializeObject<DatabaseConnection>(mlse_sql_prod_value.Value);
    //  json.Dump("mlse_sql_prod.Value.Value", 0);
    //}
    //else { "mlse_sql_prod.HasValue == False".Dump(); }    
    //
    #endregion    
  }  

  #endregion

  #region GetMlswSqlDev ----------------------------------------------------------------------------------------
  
  if(show_secret[MattangVaultSecretNames.MLSW_SQL_DEV])
  {
    var mlsw_sql_dev = GetMlswSqlDev();
    mlsw_sql_dev.Dump("mlsw_sql_dev", 0);

    #region COMMENTED OUT: ORIGINAL R&D CODE
    //
    //var mlsw_sql_dev = GetMlswSqlDev(client).GetAwaiter()
    //                                        .GetResult();
    //if(show_response)
    //  mlsw_sql_dev.Dump("mlsw_sql_dev.Value", 0);
    //
    //if(mlsw_sql_dev.HasValue)
    //{
    //  var mlsw_sql_dev_value = mlsw_sql_dev.Value;
    //  mlsw_sql_dev_value.Dump("mlsw_sql_dev.Value", 0);
    //
    //  var json = JsonConvert.DeserializeObject<DatabaseConnection>(mlsw_sql_dev_value.Value);
    //  json.Dump("mlsw_sql_dev.Value.Value", 0);
    //}
    //else { "mlsw_sql_dev.HasValue == False".Dump(); }
    //
    #endregion
  }  
  
  #endregion
  
  #region GetPipelineKeyVaultAccess ----------------------------------------------------------------------------
  
  if(show_secret[MattangVaultSecretNames.Pipeline_Key_Vault_Access])
  {
    var pipeline_key_vault_access = GetPipelineKeyVaultAccess();
    pipeline_key_vault_access.Dump("pipeline_key_vault_access", 0);
    
    #region COMMENTED OUT: ORIGINAL R&D CODE
    //
    //var pipeline_key_vault_access = GetPipelineKeyVaultAccess(client).GetAwaiter()
    //                                                                 .GetResult();
    //if(show_response)
    //  pipeline_key_vault_access.Dump("pipeline_key_vault_access.Value", 0);
    //
    //if(pipeline_key_vault_access.HasValue)
    //{
    //  var pipeline_key_vault_access_value = pipeline_key_vault_access.Value;
    //  pipeline_key_vault_access_value.Dump("pipeline_key_vault_access.Value", 0);
    //
    //  var value = pipeline_key_vault_access_value.Value;
    //  value.Dump("pipeline_key_vault_access.Value.Value", 0);
    //}
    //else { "pipeline_key_vault_access.HasValue == False".Dump(); }
    //
    #endregion
  }  

  #endregion
  
  #region GetPipelineImport ------------------------------------------------------------------------------------
  
  if(show_secret[MattangVaultSecretNames.Pipeline_Import])
  { 
    var pipeline_import = GetPipelineImport();
    pipeline_import.Dump("pipeline_import", 0);
    
    #region COMMENTED OUT: ORIGINAL R&D CODE
    //
    //var pipeline_import = GetPipelineImport(client).GetAwaiter()
    //                                               .GetResult();
    //if(show_response)
    //  pipeline_import.Dump("pipeline_import.Value", 0);
    //
    //if(pipeline_import.HasValue)
    //{
    //  var pipeline_import_value = pipeline_import.Value;
    //  pipeline_import_value.Dump("pipeline_import.Value", 0);
    //
    //  var value = pipeline_import_value.Value;
    //  value.Dump("pipeline_import.Value.Value", 0);
    //}
    //else { "pipeline_import.HasValue == False".Dump(); }
    //
    #endregion
  }  
  
  #endregion
  
  #region GetSWTXSalesforceSandbox -----------------------------------------------------------------------------
  
  if(show_secret[MattangVaultSecretNames.SWTX_Salesforce_Sandbox])
  {    
    var swtx_salesforce_sandbox_secret = GetSWTXSalesforceSandbox();
    swtx_salesforce_sandbox_secret.Dump("swtx_salesforce_sandbox_secret", 0);
    
    #region COMMENTED OUT: ORIGINAL R&D CODE
    //
    //var swtx_salesforce_sandbox_secret = GetSWTXSalesforceSandbox(client).GetAwaiter()
    //                                                                     .GetResult();
    //if(show_response) 
    //  swtx_salesforce_sandbox_secret.Dump("swtx_salesforce_sandbox_secret", 0);
    //
    //if(swtx_salesforce_sandbox_secret.HasValue)
    //{
    //  var value = swtx_salesforce_sandbox_secret.Value;
    //  value.Dump("swtx_salesforce_sandbox_secret.Value", 0);
    //  
    //  var json = JsonConvert.DeserializeObject<SWTX_Salesforce_Sandbox_Secret>(value.Value);
    //  json.Dump("swtx_salesforce_sandbox_secret.Value.Value", 0);
    //}
    //else { "swtx_salesforce_sandbox_secret.HasValue == False".Dump(); }
    //
    #endregion
  }  
  
  "ALL DONE: unless something is 'awaiting...'".Dump();
  
  #endregion
}

#region Service Layer Code ========================================================================================

public static string KeyVaultName => "MattangVault";
public static string KeyVaultUrl  => $"https://{KeyVaultName}.vault.azure.net";

protected static SecretClient client = null;

#region OPTION 1: public async Method(s) w/client parameter

public async Task<Azure.Response<KeyVaultSecret>> GetMlseSqlProd(SecretClient client)
{
  return await GetSecret(client, MattangVaultSecretNames.MLSE_SQL_PROD);
}

public async Task<Azure.Response<KeyVaultSecret>> GetMlswSqlDev(SecretClient client)
{
  return await GetSecret(client, MattangVaultSecretNames.MLSW_SQL_DEV);
}

public async Task<Azure.Response<KeyVaultSecret>> GetPipelineKeyVaultAccess(SecretClient client)
{
  return await GetSecret(client, MattangVaultSecretNames.Pipeline_Key_Vault_Access);
}

public async Task<Azure.Response<KeyVaultSecret>> GetPipelineImport(SecretClient client)
{
  return await GetSecret(client, MattangVaultSecretNames.Pipeline_Import);
}

public async Task<Azure.Response<KeyVaultSecret>> GetSWTXSalesforceSandbox(SecretClient client)
{
  return await GetSecret(client, MattangVaultSecretNames.SWTX_Salesforce_Sandbox);
}

#endregion

#region OPTION 2: public non-async Method(s) w/o client parameter

public DateOnly GetDevExpirationDate()
{
  var value = GetSecret(MattangVaultSecretNames.DEV_Expiration_Date).GetAwaiter()
                                                                    .GetResult();
                                                                    
  DateOnly.TryParse(value, out var result);
  return result;
}

public int GetDevNumberOfRetries()
{
  var value = GetSecret(MattangVaultSecretNames.DEV_Number_Of_Retries).GetAwaiter()
                                                                      .GetResult();
  int.TryParse(value, out var result);
  return result;
}

public DBConnectionModel GetMlseSqlProd()
{
  #region COMMENTED OUT: R&D CODE
  
  //DatabaseConnection db_conn = null;
  //var secret = GetSecret(MattangVaultSecret.MLSE_SQL_PROD).GetAwaiter()
  //                                                        .GetResult();
  //                                                        
  //if(secret.HasValue)
  //{
  //  db_conn = JsonConvert.DeserializeObject<DBConnectionModel>(secret.Value.Value);
  //}
  //
  //return db_conn;  
  //
  //
  //var secret = GetSecret(MattangVaultSecretNames.MLSE_SQL_PROD).GetAwaiter()
  //                                                             .GetResult();
  //                                                         
  //return secret.HasValue 
  //        ? JsonConvert.DeserializeObject<DBConnectionModel>(secret.Value.Value) 
  //        : null;
  //
  #endregion
  
  return GetSecret<DBConnectionModel>(MattangVaultSecretNames.MLSE_SQL_PROD).GetAwaiter()
                                                                            .GetResult();
}

public DBConnectionModel GetMlswSqlDev()
{
  #region COMMENTED OUT: R&D CODE
  //
  //DatabaseConnection connection = null;
  //var secret = GetSecret(MattangVaultSecret.MLSW_SQL_DEV).GetAwaiter()
  //                                                       .GetResult();
  //
  //if(secret.HasValue)
  //{
  //  connection = JsonConvert.DeserializeObject<DBConnectionModel>(secret.Value.Value);
  //}
  //
  //return connection;
  //
  //
  //var secret = GetSecret(MattangVaultSecretNames.MLSW_SQL_DEV).GetAwaiter()
  //                                                            .GetResult();
  //                                                        
  //return secret.HasValue 
  //        ? JsonConvert.DeserializeObject<DBConnectionModel>(secret.Value.Value)
  //        : null;
  //
  #endregion

  return GetSecret<DBConnectionModel>(MattangVaultSecretNames.MLSW_SQL_DEV).GetAwaiter()
                                                                           .GetResult();
}

public string GetPipelineKeyVaultAccess()
{
  #region COMMENTED OUT: R&D CODE
  //
  //string value = null;
  //var secret = GetSecret(MattangVaultSecret.PipelineKeyVaultAccess).GetAwaiter()
  //                                                                 .GetResult();
  //
  //if(secret.HasValue)
  //{
  //  value = secret.Value.Value;
  //}
  //
  //return value;
  //
  //
  //var secret = GetSecret(MattangVaultSecretNames.Pipeline_Key_Vault_Access).GetAwaiter()
  //                                                                         .GetResult();
  //return secret.HasValue
  //        ? secret.Value.Value
  //        : null;
  //
  #endregion

  return GetSecret(MattangVaultSecretNames.Pipeline_Key_Vault_Access).GetAwaiter()
                                                                     .GetResult();
}

public string GetPipelineImport()
{
  #region COMMENTED OUT: R&D CODE
  //
  //string value = null;
  //var secret = await GetSecret(MattangVaultSecret.PipelineImport);
  //
  //if(secret.HasValue)
  //{
  //  value = secret.Value.Value;
  //}
  //
  //return value;
  //
  //
  //var secret = GetSecret(MattangVaultSecretNames.Pipeline_Import).GetAwaiter()
  //                                                               .GetResult();
  //return secret.HasValue
  //        ? secret.Value.Value
  //        : null;
  //
  #endregion
  
  return GetSecret(MattangVaultSecretNames.Pipeline_Import).GetAwaiter()
                                                           .GetResult();
}

public SWTXSalesforceModel GetSWTXSalesforceSandbox()
{
  #region COMMENTED OUT: R&D CODE
  //
  //SWTX_Salesforce_Sandbox_Secret value = null;
  //var secret = GetSecret(MattangVaultSecret.SWTX_SalesforceSandbox).GetAwaiter()
  //                                                                 .GetResult();
  //
  //if(secret.HasValue)
  //{
  //  value = JsonConvert.DeserializeObject<SWTXSalesforceModel>(secret.Value.Value);
  //}
  //
  //return value;
  //
  //
  //var secret = GetSecret(MattangVaultSecretNames.SWTX_Salesforce_Sandbox).GetAwaiter()
  //                                                                       .GetResult();
  //
  //return secret.HasValue
  //        ? JsonConvert.DeserializeObject<SWTXSalesforceModel>(secret.Value.Value)
  //        : null;  
  //
  #endregion

  return GetSecret<SWTXSalesforceModel>(MattangVaultSecretNames.SWTX_Salesforce_Sandbox).GetAwaiter()
                                                                                        .GetResult();
}

#endregion

private async Task<string> GetSecret(string secret_name = null)
{
  client ??= new SecretClient(new Uri(KeyVaultUrl), new DefaultAzureCredential());
  var secret = await GetSecret(client, secret_name);
  
  return secret.HasValue
          ? secret.Value.Value
          : null;
}

private async Task<T> GetSecret<T>(string secret_name = null)
{
  var value = await GetSecret(secret_name);
  
  return value != null
          ? JsonConvert.DeserializeObject<T>(value)
          : default;
}

#region COMMENTED OUT: R&D CODE
//
//private async Task<Azure.Response<KeyVaultSecret>> GetSecret(string secret_name = null)
//{
//  client ??= new SecretClient(new Uri(KeyVaultUrl), new DefaultAzureCredential());  
////  client = new SecretClient(new Uri(KeyVaultUrl), new DefaultAzureCredential());  
//  var secret = await GetSecret(client, secret_name);
//  
//  return secret;
//}
//
#endregion

private async Task<Azure.Response<KeyVaultSecret>> GetSecret( SecretClient client
                                                             ,string secret_name = null )
{
  if(client == null)
    throw new ArgumentNullException("The client cannot be null!");
    
  if(string.IsNullOrWhiteSpace(KeyVaultName))
    throw new ArgumentNullException("The key vault name cannot be null, empty, or contain only whitespaces!");

  if(string.IsNullOrWhiteSpace(secret_name))
    throw new ArgumentNullException("The secret name cannot be null, empty, or contain only whitespaces!");

  Azure.Response<KeyVaultSecret> secret = null;

  try
  {
    Console.Write($"Retrieving the Secret '{secret_name}' from {KeyVaultName} ...");
    
    secret = await client.GetSecretAsync(secret_name);
    
    Console.Write("... RETRIEVED");
  }
  catch(Exception ex)
  {    
    throw;
  }

  return secret;
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
  //public static string MLSE_SQL_PROD            => "mlse-sql-prod";
  //public static string MLSW_SQL_DEV             => "mlsw-sql-dev";
  //
  //public static string SWTX_SalesforceSandbox   => "swtx-salesforce-sandbox";
  //public static string PipelineImport           => "pipeline-import";
  //public static string PipelineKeyVaultAccess   => "pipeline-key-vault-access";
  
  public static string DEV_Expiration_Date        => nameof(DEV_Expiration_Date).ToLower().Replace('_', '-');
  public static string DEV_Number_Of_Retries      => nameof(DEV_Number_Of_Retries).ToLower().Replace('_', '-');
  
  public static string MLSE_SQL_PROD              => nameof(MLSE_SQL_PROD).ToLower().Replace('_', '-');
  public static string MLSW_SQL_DEV               => nameof(MLSW_SQL_DEV).ToLower().Replace('_', '-');
  
  public static string SWTX_Salesforce_Sandbox    => nameof(SWTX_Salesforce_Sandbox).ToLower().Replace('_', '-');
  public static string Pipeline_Import            => nameof(Pipeline_Import).ToLower().Replace('_', '-');
  public static string Pipeline_Key_Vault_Access  => nameof(Pipeline_Key_Vault_Access).ToLower().Replace('_', '-');
}

#endregion

#region COMMENTED OUT: Sample Code from Azure Portal
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
