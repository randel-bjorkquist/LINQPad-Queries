<Query Kind="Program">
  <Namespace>System.Security.Cryptography.X509Certificates</Namespace>
</Query>

void Main()
{
  var path = @"C:\temp\misc\springworkstx_uat_auth.pfx";
  var cert_from_file = new X509Certificate2(path, (string)null);
  
  cert_from_file.Dump("cert_from_file", 0);

  var configuration = ADPWorkforceNowConfiguration.Create();
  var SSLPfxBase64  = configuration[ADPWorkforceNowConstants.KeyName.SSLPfxBase64].ToString();
  var base64Pfx     = Convert.FromBase64String(SSLPfxBase64);
  
  var cert_from_string = new X509Certificate2( base64Pfx
                                              ,(string)null
                                              ,X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable );
                                              
  cert_from_string.Dump("cert_from_string", 0);

#region Read the certificate from the store
//
//  X509Certificate2 Certificate = null;
//
//  X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
//
//  store.Open(OpenFlags.ReadOnly);
//
//  try
//  {
//    var certs = store.Certificates;
//    certs.Dump(0);
//    
//    // Try to find the certificate based on its common name
//    //X509Certificate2Collection Results = store.Certificates.Find( X509FindType.FindBySubjectDistinguishedName
//    //                                                             ,"CN = Tom, CN = Clancy"
//    //                                                             ,false);
//    
//    X509Certificate2Collection Results = store.Certificates.Find( X509FindType.FindBySubjectDistinguishedName
//                                                                 //,"CN = Tom, CN = Clancy"
//                                                                 ,"CN=localhost"
//                                                                 ,false);
//
//    if (Results.Count == 0)
//      throw new Exception("Unable to find certificate!");
//    else
//      Certificate = Results[0];
//      
//    Results.Dump(0);
//    Certificate.Dump(0);
//  }
//  finally
//  {
//    store.Close();
//  }
//
#endregion
}

public static class ADPWorkforceNowConfiguration
{
  public static Dictionary<string, object> Create()
  {
    return new Dictionary<string, object>
               {
                   { ADPWorkforceNowConstants.KeyName.TokenUrl, "https://uat-accounts.adp.com" }
                  ,{ ADPWorkforceNowConstants.KeyName.ADPRequestUrl, "https://uat-api.adp.com" }
                  ,{ ADPWorkforceNowConstants.KeyName.ClientId, "549641d8-41eb-4e21-98cc-76441a8757dc" }
                  ,{ ADPWorkforceNowConstants.KeyName.ClientSecret, "b5bbc219-2657-475f-afdc-3f309af60fe3" }
                  ,{ ADPWorkforceNowConstants.KeyName.AccessScope, "demo" }
                  ,{ ADPWorkforceNowConstants.KeyName.SSLKey, "-----BEGIN PRIVATE KEY-----\nMIIEvwIBADANBgkqhkiG9w0BAQEFAASCBKkwggSlAgEAAoIBAQCm7glea4hVLqfL\nxMyKGuTt5MLqnl6U7vPuyZAhR2Bf6WfReRib3yrgtpaCW4HRWCpHleoJs2rsl5MH\n3C3bC582bQsVfaOYK7WijFu1rKzrYUnhaz7NxHwYeYUxtcxTYDsg626PnDZIWTTR\nBfHLa8wwTe0aM4P+LoTPvV5eYz9XzrdB71ap5f3hIWi9efbvxcRgJAmr5h5q/MCB\n8Nbi9F45RHsgwNv/eehNRMDrS2WlpIWucamBVI/bos7wULJf1TVPg/j7YJHWzOMr\n05Kq3zn1gudBNC5WThaPWAkelAvPRrFDgJLJNpM0BiGTT/JCxqPMl9WtG3EifbZ5\n29pO5A0BAgMBAAECggEATa6ESZ49CXb2uwrQLIyrcFF+q3Qbi69PcRX3rqTqoneZ\nyo9R9md6KqyNighPdz5SHpITIYSPA57d21CJPmBALTG9cLjRAqWElyo+G2NJ7ReP\nLAmeZl9DaJhUdXemqD4mBgHJp6cIU86/l23uwb1efjQlXIYQrJ61S39ShRYmihN0\njvXmNpvV5L+K1xV4nAnfbXo4qCsPthjJ1kzvZDM5YiNV3FjzYYfVUYRmMVh4ViZE\nmCtBNoJyTeMx4FpqTJOFYREO7lH6LQnV/kxLB0L/+6kgSmt3a+QlJW7xiz5pyGAH\n+OyJj+cy8/wh5LLZ+/kOEVAMw/CvkysuJ59k2teJCQKBgQC1sR4QnKgittrOIU//\nqGnr6hC4PxcrHbsmU3qZXmw4/1Qn5bw9WHJVVkx24H23vP6pHpKvXUhX4GCsFCzp\nHUqEw1i2aV2VdMxXb5lpn/zabCPO92PWztViv/VEDwjoOmYqcDcRXrbdYS1aQJm/\nBpoDRxdNN3pd6cVGP5fdAjJ0lwKBgQDrM1rbrcSXn0aoY0AC2Ytkw44SGPRsFOUR\nRC3WGBweQ8DzDglFtKa3kWepxPoE4dOvwE48fSlFl/7EmWNoO9yU8XJ90gRB8Zb1\nIQ2thS88JFlNrY2lAtmcWBKmODieD2FsAcTPKFBx47WovsUNQPM+z/SwP0HswvJH\nV5TJOKVGJwKBgQCQt4qi4KRrQgn1tULq91GmvsCigF4L0ZqHZGZ6UmMQ4w482Ree\nJRysLBirdxOrjbWpLtjXJQ3CMM3PQiKjatk39gBrCnGn4WgBHLqtDulvcEL1TscP\nCZ+dEBhjJTaLAjjuBINYKoCsxAJDqMDNDo0Nn0pIDrBArTqcQXrMZjuZ8QKBgQCT\nlFu0b+plQRmy2uP1Zc9jYJkqneHwV4QMO32PUv2Bt/3ABNSomlvp3yDuk5xVp+WN\n59qvddGJy3+emKiZZMZZ5s5ySG9HeXHBSgYtFN64nA41AWtZWNp2kYTSK4DLHNeC\nT/HQEnm3nKwFv/4g/NokVZQxat/Q0dn4DepcAGEkDQKBgQCE8suMpscz3s/3aNkK\nELF1s6VKcarj58TQV//XQH+A0U2xXR/xJjAHGjT2QnMwDjYfZ5EOAJe5Smx11SN1\nKQuRKztwolxhBBPhULlhLnETEnQJuUiYWnSzdv0GUBWqnafSH7iCgXR+9DrXRqe9\neMbzv2t+YD1ymXGkrZjqFxwRmA==\n-----END PRIVATE KEY-----\n" }
                  ,{ ADPWorkforceNowConstants.KeyName.SSLPfxBase64, "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" }
                  ,{ ADPWorkforceNowConstants.KeyName.GrantType, "client_credentials" }
               };
  }
}

public class ADPWorkforceNowConstants
{
    public struct KeyName
    {
        public const string TokenUrl      = nameof(TokenUrl);
        public const string ADPRequestUrl = nameof(ADPRequestUrl);
        public const string ClientId      = nameof(ClientId);
        public const string ClientSecret  = nameof(ClientSecret);
        public const string AccessScope   = nameof(AccessScope);
        public const string SSLKey        = nameof(SSLKey);
        public const string SSLPfxBase64  = nameof(SSLPfxBase64);
        public const string DoFullCrawl   = nameof(DoFullCrawl);
        public const string GrantType     = nameof(GrantType);
    }
}

