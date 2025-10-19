<Query Kind="Program">
  <Connection>
    <ID>d4aa2989-e5c9-44cd-8400-77cfdd4d3d68</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>localhost</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA54DOUebfQEikwaNxbHIyqQAAAAACAAAAAAAQZgAAAAEAACAAAACZ2jrhmnhKGfcOHPpkYcQQKRQHWeG7RMd0fd1MGRjV1QAAAAAOgAAAAAIAACAAAADXod6eh2Jj/Nu+GoHeLHj+GuH3RrOeRztsaQ014pHTNyAAAADRuZNzEsMgFbtr6Rl1A15rXZRlzK4/ECC8RaUjwEwBBUAAAAD0tmz2F8IULaUqyIbdW/byOpLUZNIfDSfKgpQLFAzCmXYYgX5ipCQnb7YdfphqB7tXcJje/hAKIWD50siDh2L1</Password>
    <Database>DataStore.Db.Authentication</Database>
    <DisplayName>localhost (DataStore.Db.Authentication) - sa</DisplayName>
    <AlphabetizeColumns>true</AlphabetizeColumns>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
      <TrustServerCertificate>True</TrustServerCertificate>
    </DriverData>
  </Connection>
  <Namespace>System.ComponentModel.DataAnnotations</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations.Schema</Namespace>
  <Namespace>System.Configuration</Namespace>
  <Namespace>System.IO.Compression</Namespace>
  <Namespace>System.Runtime.Serialization.Formatters.Binary</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.Xml.Serialization</Namespace>
</Query>

void Main()
{
  var welcome_msg = "Using the organization secret in the [DataStore.Db.Authentication].dbo.OrganizationAccount.Secret "
                  + "column in SQL Server you can successfully decrypt data such as configurations and blobs.";
  welcome_msg.Dump("Welcome to the decrypt utility for CluedIn");

  SetKey();

  #region COMMENTED OUT: not currently required for this exercise
  //
  //var configurations = GetConfigurations();
  //configurations.Dump("configurations", 0);
  //
  #endregion

  #region COMMENTED OUT: R&D CODE
  
  //  using System.Xml.Serialization;
  //  XmlSerializer serializer = new XmlSerializer(typeof(Entity));
  //  using (StringReader reader = new StringReader(xml))
  //  {
  //    var test = (Entity)serializer.Deserialize(reader);
  //  }

  //var blobs = GetBlobEntities("CluedIn", ComparisonType.Contains, 25);
  //var blobs = GetBlobEntities("/Person", ComparisonType.StartsWith, 25);
  //blobs.Dump("blobs", 0);
  
  #endregion

  var guids = new List<Guid> {
                              // new Guid("afda2b07-de0c-577e-8495-0214063dae9d")   /* KELLER, KELLI MARIE      = DEV   */
                              // new Guid("4c66b4d5-7344-57cf-881e-dc06b74106fd")   /* Edward Cheng             = DEV   */
  
                              // new Guid("29a2ce41-73fd-509e-87d5-574fa7fd94b0")   /* Frank Bunono             = local */
                              // new Guid("a784f1d2-dcd4-5fc3-bb1d-22d11e5b534f")   /* Thomas-101 Anderson-101  = local */
                              //,new Guid("b7047fc8-f608-593e-8ecd-835109269fc5")   /* Thomas-108 Anderson-108  = local */
                               new Guid("0769f87b-39ea-5b6c-b4ad-5233f3212449")   /* Marco Giovannini         = local */
                              ,new Guid("e7d0104a-759a-55c5-8328-772d73203800")   /* Marta Batus              = local */
                              ,new Guid("bd77a699-a329-5672-8a18-ed02eb361903")   /* Kathryn Nevel            = local */
                              ,new Guid("c335db88-f02b-578d-a701-ec88e2c731f2")   /* Chandrajit Raut          = local */
                              ,new Guid("a27197b4-6aea-5c5e-9608-58219abe30c6")   /* Amanda Parkes            = local */
                              ,new Guid("409fd940-de11-5494-ae6e-e7beede44525")   /* Juneko Grilley-Olson     = local */
                              ,new Guid("5c9f01d9-49db-562c-97a1-91567244be36")   /* Edward Cheng             = local */
                              ,new Guid("eb5d7f05-8ae7-5ead-a954-da96ed8f6046")   /* Steven Attia             = local */
                              ,new Guid("56ab9f51-f6ce-536e-9e20-2023669d35a2")   /* Watson Roye              = local */
                              ,new Guid("0f8f3e19-7a04-5813-9a53-a03ef40a6030")   /* Lucy Langer              = local */
                              ,new Guid("459f1f7a-e56e-595c-9553-cca0f89f1802")   /* Tommy Gabriel            = local */
                              ,new Guid("267efe94-729f-50fa-9bc5-5bae777305c3")   /* Joseph Farr              = local */ 
                             };
                             
  var blobs = GetBlobStorageEntities(guids);
  
//  var entity_ids = new List<string> {
//                                      "a784f1d2-dcd4-5fc3-bb1d-22d11e5b534f"     /* Thomas-101 Anderson-101  = local */
//                                     ,"b7047fc8-f608-593e-8ecd-835109269fc5"     /* Thomas-108 Anderson-108  = local */
//                                    };
//                                    
//  var blobs = GetBlobStorageEntities(entity_ids);
  
//  var blobs = GetBlobStorageEntities("/Person", ComparisonType.StartsWith, 200);
//  var blobs = GetBlobStorageEntities("/Person", ComparisonType.StartsWith, 3, 100);
//  var blobs = GetBlobStorageEntities("CluedIn", ComparisonType.Contains, 25);
  blobs.Select(b => b.Data).Dump("blobs", 0);
  
//  blobs.Select(b => b.Data)
//       .Select(xml => xml.Descendants("tags"))
//       .Dump("blob.Data", 1);

  blobs.Select(b => b.Data)
       .Select(xml => xml.Descendants("versionHistory")
//                         .Elements()
//                         .Where(e => e.Descendants("change")
//                                      .Attributes().Any(a => a.Name == "key" 
//                                                          && a.Value.ToLower().Contains("npi")))
                                                          
                         .Descendants("change")
                         .Where(e => e.Attribute("key").Value.ToLower().Contains("npi"))
  
//                         .Select(e => new { key   = e.Attribute("key").Value
//                                           ,value = e.Attribute("newValue").Value })

//                         .GroupBy(e => e.value)
//                         .Select(e => e.value)
//                         .Distinct()
//                         .Where(e => e.Count() > 1)
              )
       .Dump("blob.Data", 1);
            
#region COMMENTED OUT: R&D CODE
//
//  blobs.Select(b => b.Data)
//       .Select(xml => xml.Descendants("versionHistory")
//                         .Elements()
////                         .Where(e => e.Descendants("change")
////                                      .Attributes().Any(a => a.Name == "key" 
////                                                          && a.Value.ToLower().Contains("authors")))
//                                                          
//                         .Descendants("change")
//                         .Where(e => e.Attribute("key").Value.ToLower().Contains("npi"))
////                         .ToDictionary(e => e.Attribute("key").Value)
//                         
//                         .Select(e => new { key   = e.Attribute("key").Value
//                                           ,value = e.Attribute("newValue").Value })
//                         
////                         .GroupBy(e => e.value)
//                         
////                         .Attributes()
////                         .Where(a => a.Value.Contains("0"))
//                         
////                         .Where(e => e.Attributes().Any(a => a.Value.Contains("1447287610")))
////                         .Where(e => e.Attributes().Any(a => a.Value.ToLower().Contains("npi")))
//
////                         .Where(e => e.Attributes().Any(a => a.Value.ToLower().Contains("npi")))
////                         .Select(e => e.Attribute("newValue").Value)
////                         .Distinct()
//                         
////                         .Select(x => x.Attributes())
////                         .Select(x => x.Attributes().Where(a => a.Value.Contains("kpi")))
////                         .Where(e => e.Element("change").Attribute("type").ToString() == "Removed")
////                         .Where(e => (string)e.Element("change").Attribute("type") == "Removed")
////                         .Where(e => (string)e.Element("change").Attribute("type") == "Removed")
////                         .Where(e => (string)e.Element("change").Attribute("key").Contains("kpi"))
////                         .Where(e => (string)e.Element("change").Attribute("key").Value.Contains("kpi"))
////                         .Where(e => (string)e.Element("change").Attribute("key") == "kpi")
//              ).Dump("blob.Data", 2);            
//
//                         
//       .Select(element => element.Descendants("versionHistory")
//                                 .Elements()
//                                 .Descendants("change")
////                                 .Attributes()
////                                 .Where(attribute => attribute.Name.ToString().ToLower().Contains("kpi"))
//                                 .Where(e => e.Attributes()
//                                              .Select(a => a.Name)
//                                              .Contains("kpi"))
                                 
//                                 .Where(element => element.Attribute("key")
//                                                          .Value
//                                                          .ToLower()
//                                                          .Contains("kpi"))

//                                 .Attributes()
//                                 .Count(e => e.Name == "version")
//                                 .Descendants("change")
//                                 .Attributes()
//                                 .Where(attr => attr.Name == "key"
//                                             && attr.Value.Contains("MEDICAL_EDUCATION_NUMBER"))
//                                 .Where(attr => attr.Name == "key"
//                                             && attr.Value == "snowflake.ama.deads.MEDICAL_EDUCATION_NUMBER"
//                                             || attr.Value == "snowflake.ama.deads.NPINumber")
//         )
//         .Where(element => element.Attributes().Contains(""))
//         .Dump("blob.Data", 1);
//       .SelectMany(element => element.Descendants("properties")
//                                     .Elements())
//       .Where(element => element.Contains("d"))
//       .Dump("blob.Data", 0);
//
#endregion

#region THIS DOESN'T LOOK LIKE IT DOES ANY GOOD - looks like each row is/has a unique EntityId
//
//  blobs.Count()
//       .Dump("count");
//
//  blobs.Select(blob => blob.EntityId)
//       .Distinct()
//       .Count()
//       .Dump("count");
//
#endregion
}

public enum ComparisonType
{
   StartsWith = 0
  ,Contains   = 1
  ,EndsWith   = 2
  ,Equals     = 3
}

public static byte[] _key;

public void SetKey()
{
  using var context  = new CluedInAuthenticationDataContext();  
  var authentication = context.OrganizationAccounts.First(); //we only expect one and this will throw if that expectation is not met...
  var secretData     = authentication.Secret;
  
  _key = GetKey(secretData);
}

public byte[] GetKey(byte[] organizationAccountSecret)
{
  //Found the GUID string, hardcoded in SystemContext.Organization
  byte[] organizationContextSystemOrganizationAccountSecret = new Guid("{B79D78F2-67E8-46DB-9BD3-C25A1BE47E7C}").ToByteArray(); 
  byte[] key = AesUtility.Decrypt(organizationAccountSecret, organizationContextSystemOrganizationAccountSecret);
  
  return key;
}

public IEnumerable<string> GetConfigurations()
{
  Console.WriteLine("Reading and decrypting Configurations...");
  
  using var context  = new CluedInConfigurationDataContext();
  var configurations = context.Configurations;
  
  foreach(var configuration in configurations)
  {
    byte[] data = configuration.ConfigurationData;
    var result  = Decrypt(data, _key);
    
    yield return result;
  }
}

public IEnumerable<BlobStorageEntity> GetBlobStorageEntities(IEnumerable<string> entity_ids)
{  
  return GetBlobStorageEntities( null
                                ,ComparisonType.Equals
                                ,entity_ids.Select(id => new Guid(id))
                                ,0 );
}

public IEnumerable<BlobStorageEntity> GetBlobStorageEntities(IEnumerable<Guid> entity_ids)
{
  return GetBlobStorageEntities(null, ComparisonType.Equals, entity_ids, 0);
}

public IEnumerable<BlobStorageEntity> GetBlobStorageEntities(string name, ComparisonType compare, int count)
{
  return GetBlobStorageEntities(name, compare, null, count);
}

internal IEnumerable<BlobStorageEntity> GetBlobStorageEntities(string name, ComparisonType compare, int page, int page_size)
{
  var context = new CluedInBlobStorageDataContext();
  IQueryable<BlobEntity> entities = context.Blobs;
  
  if(!string.IsNullOrWhiteSpace(name))
  {
    var begin_date = new DateTime(2022, 06, 01);
    var end_date   = new DateTime(2022, 12, 31);
    
    switch(compare)
    {
      case ComparisonType.Contains:
        entities = entities.Where(b => b.Name.Contains(name));
        break;
        
      case ComparisonType.EndsWith:
        entities = entities.Where(b => b.Name.EndsWith(name));
        break;
        
      case ComparisonType.Equals:
        entities = entities.Where(b => b.Name.ToLower() == name.ToLower());
        break;
        
      case ComparisonType.StartsWith:
        entities = entities.Where(b => b.Name.StartsWith(name));
        break;
    }
  }
  
  return entities.Skip(page * page_size)
                 .Take(page_size)
                 .ToBlobStorageEntities();
}

internal IEnumerable<BlobStorageEntity> GetBlobStorageEntities(string name, ComparisonType compare, IEnumerable<Guid> guids, int count)
{
  //Console.WriteLine("Getting BlobStorage Entities ...");

  var context = new CluedInBlobStorageDataContext();
  IQueryable<BlobEntity> entities = context.Blobs;
  
  if(!string.IsNullOrWhiteSpace(name))
  {
    var begin_date = new DateTime(2022, 06, 01);
    var end_date   = new DateTime(2022, 12, 31);
    
    switch(compare)
    {
      case ComparisonType.Contains:
        entities = entities.Where(b => b.Name.Contains(name));
        break;
        
      case ComparisonType.EndsWith:
        entities = entities.Where(b => b.Name.EndsWith(name));
        break;
        
      case ComparisonType.Equals:
        entities = entities.Where(b => b.Name.ToLower() == name.ToLower());
        break;
        
      case ComparisonType.StartsWith:
        entities = entities.Where(b => b.Name.StartsWith(name));
        break;
    }
  }
  
  if(guids?.Any() ?? false)
  {
    entities = entities.Where(e => e.EntityId.HasValue 
                                && guids.Contains(e.EntityId.Value));
  }
  
  if(count > 0)
  {
    entities = entities.Take(count);
  }
     
  return entities.ToBlobStorageEntities();
}

public IEnumerable<string> GetBlobEntities(string name = null, ComparisonType compare = ComparisonType.Equals, int count = 0)
{
  //Console.WriteLine("Reading and decrypting SQL Server blobs...");

  using var context = new CluedInBlobStorageDataContext();    
  IQueryable<BlobEntity> blobs = context.Blobs;
  
  if(!string.IsNullOrWhiteSpace(name))
  {
    switch(compare)
    {
      case ComparisonType.Contains:
        blobs = blobs.Where(b => b.Name.Contains(name));
        break;
        
      case ComparisonType.EndsWith:
        blobs = blobs.Where(b => b.Name.EndsWith(name));
        break;
        
      case ComparisonType.Equals:
        blobs = blobs.Where(b => b.Name.ToLower() == name.ToLower());
        break;
        
      case ComparisonType.StartsWith:
        blobs = blobs.Where(b => b.Name.StartsWith(name));
        break;
    }
  }

  if(count > 0)
  {
    blobs = blobs.Take(count);
  }

  #region COMMENTED OUT: R&D CODE
  //
  //IQueryable<EntityBlob> blobs = Enumerable.Empty<EntityBlob>().AsQueryable();
  //
  //if(count <= 0)
  //{
  //  blobs = string.IsNullOrWhiteSpace(value)
  //        ? context.Blobs
  //        : context.Blobs
  //                 .Where(b => b.Name.StartsWith(value));
  //}
  //else
  //{
  //  blobs = string.IsNullOrWhiteSpace(value)
  //        ? context.Blobs
  //                 .Take(count)
  //        : context.Blobs
  //                 .Where(b => b.Name.StartsWith(value))
  //                 .Take(count);
  //}
  //
  #endregion
   
  foreach(var blob in blobs)
  {
    byte[] data   = blob.EncryptedData;
    Stream stream = new MemoryStream(data);
    string result = ReadBlobData(stream, _key);
    
    yield return result;
  }
}

public static string ByteArrayToString(byte[] data)
{
  return BitConverter.ToString(data, 0, data.Length);
}

public static byte[] StringToByteArray(string hex)
{
  hex = hex.Substring(2); //need to strip off the '0x'
  int cnt = hex.Length;
  byte[] bytes = new byte[cnt / 2];

  for(int i = 0; i < cnt; i += 2)
  {
    bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
  }
  
  return bytes;
}

public static string Decrypt(byte[] data, byte[] key)
{
  var decryptedDataAsByteArray = AesUtility.Decrypt(data, key);
  var result = new BinaryFormatter().Deserialize(new MemoryStream(decryptedDataAsByteArray)) as string;

  return result;
}

public static string ReadBlobData(Stream stream, byte[] key)
{
  Stream compressedData = AesUtility.Decrypt(stream, key);
  var x = Deflate(compressedData);
  
  return x;
}

public static string Deflate(Stream stream)
{
  using DeflateStream decompress = new DeflateStream(stream, CompressionMode.Decompress, leaveOpen: true);
  using StreamReader reader      = new StreamReader(decompress, Encoding.Unicode);
  
  string content = reader.ReadToEnd();
  return content;
}

public class AesUtility
{
  public static Stream Decrypt(Stream dataToDecrypt, byte[] password)
  {
    byte[] salt = new byte[32];
    
    if (dataToDecrypt.Read(salt, 0, 32) != 32)
    {
      throw new Exception("Could not read salt from data.");
    }
    
    return Decrypt(dataToDecrypt, password, salt);
  }

  public static Stream Decrypt(Stream dataToDecrypt, byte[] password, byte[] salt, int pbkdfRounds = 1000)
  {
    Rfc2898DeriveBytes rfc2898    = new Rfc2898DeriveBytes(password, salt, pbkdfRounds);
    AesCryptoServiceProvider aes  = new AesCryptoServiceProvider();
    
    aes.Mode    = CipherMode.CBC;
    aes.Padding = PaddingMode.PKCS7;
    aes.Key     = rfc2898.GetBytes(32);
    aes.IV      = rfc2898.GetBytes(16);
    
    return new CryptoStream(dataToDecrypt, aes.CreateDecryptor(), CryptoStreamMode.Read);
  }

  public static byte[] Decrypt(byte[] dataToDecrypt, byte[] password)
  {
    byte[] salt = dataToDecrypt.Take(32)
                               .ToArray();
    
    return Decrypt(dataToDecrypt.Skip(32)
                                .ToArray(), password, salt);
  }

  public static byte[] Decrypt(byte[] dataToDecrypt, byte[] password, byte[] salt, int pbkdfRounds = 1000)
  {
    using Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(password, salt, pbkdfRounds);
    
    #pragma warning disable SYSLIB0021 // Type or member is obsolete
    using AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
    #pragma warning restore SYSLIB0021 // Type or member is obsolete

    aes.Mode    = CipherMode.CBC;
    aes.Padding = PaddingMode.PKCS7;
    aes.Key     = rfc2898.GetBytes(32);
    aes.IV      = rfc2898.GetBytes(16);
    
    using MemoryStream memoryStream = new MemoryStream(dataToDecrypt.Length);
    using CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
    
    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
    cryptoStream.FlushFinalBlock();
    
    return memoryStream.ToArray();
  }
}

//TODO: passwords and connection strings should not be hardcoded.
//TODO: move these out to their own classes and behind a repository pattern

public class CluedInAuthenticationDataContext : DbContext
{
  public DbSet<OrganizationAccountEntity> OrganizationAccounts { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    //optionsBuilder.UseSqlServer(@"Data Source=tcp:127.0.0.1,55495;Initial Catalog=DataStore.Db.Authentication;TrustServerCertificate=True;User ID=sa;Password=uo1ulaBE8NZnVu8wdRnvdRa5GIFXLmCveqPD");
    optionsBuilder.UseSqlServer(@"Data Source=tcp:127.0.0.1,1433;Initial Catalog=DataStore.Db.Authentication;TrustServerCertificate=True;User ID=sa;Password=yourStrong(!)Password");
  }
}

public class CluedInBlobStorageDataContext : DbContext
{
  public DbSet<BlobEntity> Blobs { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    //optionsBuilder.UseSqlServer(@"Data Source=tcp:127.0.0.1,55495;Initial Catalog=DataStore.Db.BlobStorage;TrustServerCertificate=True;User ID=sa;Password=uo1ulaBE8NZnVu8wdRnvdRa5GIFXLmCveqPD");
    optionsBuilder.UseSqlServer(@"Data Source=tcp:127.0.0.1,1433;Initial Catalog=DataStore.Db.BlobStorage;TrustServerCertificate=True;User ID=sa;Password=yourStrong(!)Password");
  }
}

public class CluedInConfigurationDataContext : DbContext
{
  public DbSet<ConfigurationEntity> Configurations { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    //optionsBuilder.UseSqlServer(@"Data Source=tcp:127.0.0.1,55495;Initial Catalog=DataStore.Db.Configuration;TrustServerCertificate=True;User ID=sa;Password=uo1ulaBE8NZnVu8wdRnvdRa5GIFXLmCveqPD");
    optionsBuilder.UseSqlServer(@"Data Source=tcp:127.0.0.1,1433;Initial Catalog=DataStore.Db.Configuration;TrustServerCertificate=True;User ID=sa;Password=yourStrong(!)Password");
  }
}

[Table("OrganizationAccount")]
public class OrganizationAccountEntity
{
  //TODO: there are many more columns here to map but we really did not need them for the decryption
  [Required]
  public Guid Id                  { get; set; }
  public byte[] Secret            { get; set; }
}

[Table("Configuration")]
public class ConfigurationEntity
{
  [Required]
  public Guid Id                  { get; set; }
  
  [Required]
  public Guid OrganizationId      { get; set; }

  [Column("Configuration")]
  public byte[] ConfigurationData { get; set; }
}

[Table("Blobs")]
public class BlobEntity
{
  public BlobEntity() {    
  }
  
  public BlobEntity(BlobEntity entity) {
    Id = entity.Id;
    OrganizationId = entity.OrganizationId;
    
    EntityId = entity.EntityId;
    
    Name = entity.Name;
    MimeType = entity.MimeType;
    
    CreationDate = entity.CreationDate;
    ModificationDate = entity.ModificationDate;
    
    DataLength = entity.DataLength;
    DataMD5 = entity.DataMD5;
    
    EncryptedData = entity.EncryptedData;
  }

  [Required]
  public Guid Id                    { get; set; }

  [Required]
  public Guid OrganizationId        { get; set; }

  public Guid? EntityId             { get; set; }

  [StringLength(150)]
  public string Name                { get; set; }

  [StringLength(80)]
  public string MimeType            { get; set; }

  [Column(TypeName = "datetime2")]
  public DateTime? CreationDate     { get; set; }

  [Column(TypeName = "datetime2")]
  public DateTime? ModificationDate { get; set; }

  public long DataLength            { get; set; }

  public string DataMD5             { get; set; }

  [Column("Data")]
  public byte[] EncryptedData       { get; set; }

  //public bool ReadEncryptedData(Action<Stream> readAction)
  //{
  //	if (Context == null)
  //	{
  //		throw new Exception("Blob instance does not have a context.");
  //	}
  //	using ExecutionContext executionContext = Context.CreateExecutionContext(OrganizationId);
  //	return Context.Container.Resolve<IBlobDataStore>().ReadBlobData(executionContext, Id, readAction);
  //}
}

public class BlobStorageEntity : BlobEntity
{
  #region COMMENTED OUT: separate object properties
  //
  //[Required]
  //public Guid Id { get; set; }
  //
  //[Required]
  //public Guid OrganizationId { get; set; }
  //
  //public Guid? EntityId { get; set; }
  //
  //[StringLength(150)]
  //public string Name { get; set; }
  //
  //[StringLength(80)]
  //public string MimeType { get; set; }
  //
  //[Column(TypeName = "datetime2")]
  //public DateTime? CreationDate { get; set; }
  //
  //[Column(TypeName = "datetime2")]
  //public DateTime? ModificationDate { get; set; }
  //
  //public long DataLength { get; set; }
  //
  //public string DataMD5 { get; set; }
  //
  //[Column("Data")]
  //public XElement Data { get; set; }
  //
  #endregion

  //private XElement _data = null;

  public BlobStorageEntity()
  : base()
  {
  }

  public BlobStorageEntity(BlobEntity entity)
  : base(entity)
  {
    //NOTE: I don't like this, as it binds the code which decrypts the "EncryptedData" and
    //      the "BlobStorageEntity" class ... not sure that's a good thing.
    //_data = XElement.Parse(ReadBlobData(new MemoryStream(this.EncryptedData), _key));
  }

  public XElement Data { get; set; }
  
  //NOTE: ONLY REQUIRED for C# class serialization
  //public Entity Entity { get; set; }
  
  //NOTE: I don't like this, as it binds the code which decrypts the "EncryptedData" and
  //      the "BlobStorageEntity" class ... not sure that's a good thing.
  //public XElement Data => XElement.Parse(ReadBlobData(new MemoryStream(this.EncryptedData), _key));
  
  //public XElement SetData(XElement element)
  //{
  //  _data = element;
  //  return _data;
  //}
}

#region BlobEntity Extension Method(s)

public static class BlobEntityExtensions
{
  public static IEnumerable<BlobStorageEntity> ToBlobStorageEntities(this IEnumerable<BlobEntity> entities)
  {
    return entities?.Select(entity => ToBlobStorageEntity(entity)) ?? Enumerable.Empty<BlobStorageEntity>();
  }
  
  public static BlobStorageEntity ToBlobStorageEntity(this BlobEntity entity)
  {
    //NOTE: trying to serialize to a C# class structure(s)
    //
    //if(entity == null) { return null; }
    //
    //BlobStorageEntity blob_storage_entity = null;
    //
    //var serializer = new XmlSerializer(typeof(Entity));    
    //var xml        = ReadBlobData(new MemoryStream(entity.EncryptedData), _key);
    //
    //using(StringReader reader = new StringReader(xml))
    //{
    //  blob_storage_entity = new BlobStorageEntity { Id                = entity.Id
    //                                               ,OrganizationId    = entity.OrganizationId
    //                                               ,EntityId          = entity.EntityId
    //                                               ,Name              = entity.Name
    //                                               ,MimeType          = entity.MimeType
    //                                               ,CreationDate      = entity.CreationDate
    //                                               ,ModificationDate  = entity.ModificationDate
    //                                               ,DataLength        = entity.DataLength
    //                                               ,DataMD5           = entity.DataMD5
    //                                               ,EncryptedData     = entity.EncryptedData
    //                                               ,Data              = XElement.Parse(xml)
    //                                               ,Entity            = (Entity)serializer.Deserialize(reader)
    //                                               //,Data              = XElement.Parse(ReadBlobData(new MemoryStream(entity.EncryptedData), _key))
    //                                               //,Entity            = (Entity)serializer.Deserialize(reader)
    //                                              };
    //}
    //
    //return blob_storage_entity;
    
    //NOTE: I don't like this because it requires an association between the "BlobStorageEntity" class
    //      and the code to decrypt the "EncryptedData" property.
    //return entity == null ? null 
    //                      : new BlobStorageEntity(entity);

    //NOTE: I like this because it decouples the "Data" property and the code which decrypts the
    //      value of the "EncryptedData" property.
    return entity == null 
                  ?  null 
                  :  new BlobStorageEntity { Id                = entity.Id
                                            ,OrganizationId    = entity.OrganizationId
                                            ,EntityId          = entity.EntityId
                                            ,Name              = entity.Name
                                            ,MimeType          = entity.MimeType
                                            ,CreationDate      = entity.CreationDate
                                            ,ModificationDate  = entity.ModificationDate
                                            ,DataLength        = entity.DataLength
                                            ,DataMD5           = entity.DataMD5
                                            ,EncryptedData     = entity.EncryptedData
                                            ,Data              = XElement.Parse(ReadBlobData(new MemoryStream(entity.EncryptedData), _key))
                                           };
  }
}

#endregion

#region Joe's DecryptCluedInConfiguration project (XML -> C# Class Models)
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Serialization;

namespace DecryptCluedIn
{
  [XmlRoot(ElementName = "aliases")]
  public class Aliases
  {
    [XmlElement(ElementName = "value")]
    public string Value { get; set; }
  }

  [XmlRoot(ElementName = "codes")]
  public class Codes
  {
    [XmlElement(ElementName = "value")]
    public List<string> Value { get; set; }
  }

  [XmlRoot(ElementName = "edge")]
  public class Edge
  {
    [XmlAttribute(AttributeName = "type")]
    public string Type { get; set; }

    [XmlAttribute(AttributeName = "creationOptions")]
    public string CreationOptions { get; set; }

    [XmlAttribute(AttributeName = "from")]
    public string From { get; set; }

    [XmlAttribute(AttributeName = "to")]
    public string To { get; set; }
  }

  [XmlRoot(ElementName = "outgoing")]
  public class Outgoing
  {
    [XmlElement(ElementName = "edge")]
    public List<Edge> Edge { get; set; }

    [XmlElement(ElementName = "edgeSummary")]
    public List<EdgeSummary> EdgeSummary { get; set; }
  }

  [XmlRoot(ElementName = "edges")]
  public class Edges
  {
    [XmlElement(ElementName = "outgoing")]
    public Outgoing Outgoing { get; set; }
  }

  [XmlRoot(ElementName = "edgeSummary")]
  public class EdgeSummary
  {
    [XmlAttribute(AttributeName = "edgeType")]
    public string EdgeType { get; set; }

    [XmlAttribute(AttributeName = "count")]
    public int Count { get; set; }

    [XmlAttribute(AttributeName = "entityType")]
    public string EntityType { get; set; }
  }

  [XmlRoot(ElementName = "edgesSummary")]
  public class EdgesSummary
  {
    [XmlElement(ElementName = "outgoing")]
    public Outgoing Outgoing { get; set; }
  }

  [XmlRoot(ElementName = "property")]
  public class Property
  {
    [XmlAttribute(AttributeName = "key")]
    public string Key { get; set; }

    [XmlText]
    public string Text { get; set; }
  }

  [XmlRoot(ElementName = "properties")]
  public class Properties
  {
    [XmlElement(ElementName = "property")]
    public List<Property> Property { get; set; }

    [XmlAttribute(AttributeName = "type")]
    public string Type { get; set; }

    [XmlText]
    public string Text { get; set; }
  }

  [XmlRoot(ElementName = "parentIds")]
  public class ParentIds
  {
    [XmlElement(ElementName = "value")]
    public string Value { get; set; }
  }

  [XmlRoot(ElementName = "providerDefinitionIds")]
  public class ProviderDefinitionIds
  {
    [XmlElement(ElementName = "value")]
    public string Value { get; set; }
  }

  [XmlRoot(ElementName = "dataPersistHashes")]
  public class DataPersistHashes
  {
    [XmlElement(ElementName = "value")]
    public List<string> Value { get; set; }
  }

  [XmlRoot(ElementName = "vocabulariesUsed")]
  public class VocabulariesUsed
  {
    [XmlElement(ElementName = "value")]
    public List<string> Value { get; set; }
  }

  [XmlRoot(ElementName = "dataClasses")]
  public class DataClasses
  {
    [XmlElement(ElementName = "value")]
    public List<string> Value { get; set; }
  }

  [XmlRoot(ElementName = "dataDescription")]
  public class DataDescription
  {
    [XmlElement(ElementName = "dataClasses")]
    public DataClasses DataClasses { get; set; }
  }

  [XmlRoot(ElementName = "processedData")]
  public class ProcessedData
  {
    [XmlElement(ElementName = "entityType")]
    public string EntityType { get; set; }

    [XmlElement(ElementName = "name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "aliases")]
    public Aliases Aliases { get; set; }

    [XmlElement(ElementName = "codes")]
    public Codes Codes { get; set; }

    [XmlElement(ElementName = "discoveryDate")]
    public DateTime DiscoveryDate { get; set; }

    [XmlElement(ElementName = "edges")]
    public Edges Edges { get; set; }

    [XmlElement(ElementName = "edgesSummary")]
    public EdgesSummary EdgesSummary { get; set; }

    [XmlElement(ElementName = "properties")]
    public Properties Properties { get; set; }

    [XmlElement(ElementName = "isSensitiveInformation")]
    public string IsSensitiveInformation { get; set; }

    [XmlElement(ElementName = "sortDate")]
    public DateTime SortDate { get; set; }

    [XmlElement(ElementName = "timeToLive")]
    public int TimeToLive { get; set; }

    [XmlElement(ElementName = "isShadowEntity")]
    public string IsShadowEntity { get; set; }

    [XmlElement(ElementName = "processingFlags")]
    public int ProcessingFlags { get; set; }

    [XmlElement(ElementName = "parentIds")]
    public ParentIds ParentIds { get; set; }

    [XmlElement(ElementName = "providerDefinitionIds")]
    public ProviderDefinitionIds ProviderDefinitionIds { get; set; }

    [XmlElement(ElementName = "dataPersistHashes")]
    public DataPersistHashes DataPersistHashes { get; set; }

    [XmlElement(ElementName = "vocabulariesUsed")]
    public VocabulariesUsed VocabulariesUsed { get; set; }

    [XmlElement(ElementName = "dataDescription")]
    public DataDescription DataDescription { get; set; }

    [XmlAttribute(AttributeName = "origin")]
    public string Origin { get; set; }

    [XmlText]
    public string Text { get; set; }

    [XmlElement(ElementName = "isExternalData")]
    public string IsExternalData { get; set; }

    [XmlAttribute(AttributeName = "ref")]
    public int Ref { get; set; }

    [XmlAttribute(AttributeName = "appVersion")]
    public string AppVersion { get; set; }
  }

  [XmlRoot(ElementName = "sameAs")]
  public class SameAs
  {
    [XmlAttribute(AttributeName = "ref")]
    public int Ref { get; set; }

    [XmlAttribute(AttributeName = "code")]
    public string Code { get; set; }
  }

  [XmlRoot(ElementName = "mapping")]
  public class Mapping
  {
    [XmlElement(ElementName = "sameAs")]
    public List<SameAs> SameAs { get; set; }

    [XmlAttribute(AttributeName = "id")]
    public int Id { get; set; }

    [XmlAttribute(AttributeName = "discoveryDate")]
    public DateTime DiscoveryDate { get; set; }
  }

  [XmlRoot(ElementName = "dataMappings")]
  public class DataMappings
  {
    [XmlElement(ElementName = "mapping")]
    public Mapping Mapping { get; set; }
  }

  [XmlRoot(ElementName = "change")]
  public class Change
  {
    [XmlAttribute(AttributeName = "type")]
    public string Type { get; set; }

    [XmlAttribute(AttributeName = "changedPart")]
    public string ChangedPart { get; set; }

    [XmlAttribute(AttributeName = "key")]
    public string Key { get; set; }

    [XmlAttribute(AttributeName = "newValue")]
    public string NewValue { get; set; }
  }

  [XmlRoot(ElementName = "version")]
  public class Version
  {
    [XmlElement(ElementName = "change")]
    public List<Change> Change { get; set; }

    [XmlAttribute(AttributeName = "dataReference")]
    public int DataReference { get; set; }

    [XmlAttribute(AttributeName = "childVersions")]
    public int ChildVersions { get; set; }

    [XmlAttribute(AttributeName = "date")]
    public DateTime Date { get; set; }

    [XmlAttribute(AttributeName = "changeType")]
    public string ChangeType { get; set; }

    [XmlAttribute(AttributeName = "changeCount")]
    public int ChangeCount { get; set; }

    [XmlAttribute(AttributeName = "editDistance")]
    public int EditDistance { get; set; }

    [XmlAttribute(AttributeName = "branch")]
    public string Branch { get; set; }

    [XmlAttribute(AttributeName = "parentVersions")]
    public int ParentVersions { get; set; }
  }

  [XmlRoot(ElementName = "versionHistory")]
  public class VersionHistory
  {
    [XmlElement(ElementName = "version")]
    public List<Version> Version { get; set; }
  }

  [XmlRoot(ElementName = "processingFlags")]
  public class ProcessingFlags
  {
    [XmlElement(ElementName = "isEdgesProcessed")]
    public string IsEdgesProcessed { get; set; }
  }

  [XmlRoot(ElementName = "entityData")]
  public class EntityData
  {
    [XmlElement(ElementName = "entityType")]
    public string EntityType { get; set; }

    [XmlElement(ElementName = "name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "codes")]
    public Codes Codes { get; set; }

    [XmlElement(ElementName = "discoveryDate")]
    public DateTime DiscoveryDate { get; set; }

    [XmlElement(ElementName = "edges")]
    public Edges Edges { get; set; }

    [XmlElement(ElementName = "edgesSummary")]
    public object EdgesSummary { get; set; }

    [XmlElement(ElementName = "properties")]
    public Properties Properties { get; set; }

    [XmlAttribute(AttributeName = "id")]
    public int Id { get; set; }

    [XmlAttribute(AttributeName = "origin")]
    public string Origin { get; set; }

    [XmlText]
    public string Text { get; set; }
  }

  [XmlRoot(ElementName = "data")]
  public class Data
  {
    [XmlElement(ElementName = "processingFlags")]
    public ProcessingFlags ProcessingFlags { get; set; }

    [XmlElement(ElementName = "entityData")]
    public EntityData EntityData { get; set; }

    [XmlElement(ElementName = "processedData")]
    public ProcessedData ProcessedData { get; set; }

    [XmlAttribute(AttributeName = "persistHash")]
    public string PersistHash { get; set; }

    [XmlAttribute(AttributeName = "originProviderDefinitionId")]
    public string OriginProviderDefinitionId { get; set; }

    [XmlAttribute(AttributeName = "id")]
    public int Id { get; set; }

    [XmlAttribute(AttributeName = "origin")]
    public string Origin { get; set; }

    [XmlAttribute(AttributeName = "appVersion")]
    public string AppVersion { get; set; }

    [XmlText]
    public string Text { get; set; }
  }

  [XmlRoot(ElementName = "details")]
  public class Details
  {
    [XmlElement(ElementName = "dataMappings")]
    public DataMappings DataMappings { get; set; }

    [XmlElement(ElementName = "versionHistory")]
    public VersionHistory VersionHistory { get; set; }

    [XmlElement(ElementName = "data")]
    public List<Data> Data { get; set; }

    [XmlElement(ElementName = "references")]
    public object References { get; set; }
  }

  [XmlRoot(ElementName = "entity")]
  public class Entity
  {
    [XmlElement(ElementName = "processedData")]
    public ProcessedData ProcessedData { get; set; }

    [XmlElement(ElementName = "details")]
    public Details Details { get; set; }

    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }

    [XmlAttribute(AttributeName = "organization")]
    public string Organization { get; set; }

    [XmlAttribute(AttributeName = "persistVersion")]
    public int PersistVersion { get; set; }

    [XmlAttribute(AttributeName = "persistHash")]
    public string PersistHash { get; set; }

    [XmlText]
    public string Text { get; set; }
  }
}

#endregion

#region C# classes generated by json2csharp.com (xml-to-csharp)

// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(Entity));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (Entity)serializer.Deserialize(reader);
// }

[XmlRoot(ElementName = "aliases")]
public class Aliases
{

  [XmlElement(ElementName = "value")]
  public List<double> Value { get; set; }

  [XmlElement(ElementName = "removed")]
  public Removed Removed { get; set; }
}

[XmlRoot(ElementName = "codes")]
public class Codes
{

  [XmlElement(ElementName = "value")]
  public List<string> Value { get; set; }
}

[XmlRoot(ElementName = "edge")]
public class Edge
{

  [XmlElement(ElementName = "from")]
  public From From { get; set; }

  [XmlElement(ElementName = "to")]
  public To To { get; set; }

  [XmlAttribute(AttributeName = "type")]
  public string Type { get; set; }

  [XmlAttribute(AttributeName = "creationOptions")]
  public string CreationOptions { get; set; }

  [XmlText]
  public string Text { get; set; }
}

[XmlRoot(ElementName = "from")]
public class From
{

  [XmlAttribute(AttributeName = "reference")]
  public string Reference { get; set; }
}

[XmlRoot(ElementName = "property")]
public class Property
{

  [XmlAttribute(AttributeName = "key")]
  public string Key { get; set; }

  [XmlText]
  public int Text { get; set; }
}

[XmlRoot(ElementName = "to")]
public class To
{

  [XmlElement(ElementName = "property")]
  public List<Property> Property { get; set; }

  [XmlAttribute(AttributeName = "reference")]
  public string Reference { get; set; }

  [XmlAttribute(AttributeName = "entityId")]
  public string EntityId { get; set; }

  [XmlText]
  public string Text { get; set; }
}

[XmlRoot(ElementName = "outgoing")]
public class Outgoing
{

  [XmlElement(ElementName = "edge")]
  public List<Edge> Edge { get; set; }

  [XmlElement(ElementName = "edgeSummary")]
  public List<EdgeSummary> EdgeSummary { get; set; }
}

[XmlRoot(ElementName = "edges")]
public class Edges
{

  [XmlElement(ElementName = "outgoing")]
  public Outgoing Outgoing { get; set; }
}

[XmlRoot(ElementName = "edgeSummary")]
public class EdgeSummary
{

  [XmlAttribute(AttributeName = "edgeType")]
  public string EdgeType { get; set; }

  [XmlAttribute(AttributeName = "count")]
  public int Count { get; set; }

  [XmlAttribute(AttributeName = "entityType")]
  public string EntityType { get; set; }
}

[XmlRoot(ElementName = "edgesSummary")]
public class EdgesSummary
{

  [XmlElement(ElementName = "outgoing")]
  public Outgoing Outgoing { get; set; }
}

[XmlRoot(ElementName = "externalReferences")]
public class ExternalReferences
{

  [XmlElement(ElementName = "value")]
  public string Value { get; set; }
}

[XmlRoot(ElementName = "properties")]
public class Properties
{

  [XmlElement(ElementName = "removeproperty")]
  public List<Removeproperty> Removeproperty { get; set; }

  [XmlElement(ElementName = "property")]
  public List<Property> Property { get; set; }

  [XmlAttribute(AttributeName = "type")]
  public string Type { get; set; }

  [XmlText]
  public string Text { get; set; }
}

[XmlRoot(ElementName = "tags")]
public class Tags
{

  [XmlElement(ElementName = "value")]
  public List<string> Value { get; set; }
}

[XmlRoot(ElementName = "parentIds")]
public class ParentIds
{

  [XmlElement(ElementName = "value")]
  public List<string> Value { get; set; }
}

[XmlRoot(ElementName = "localParentIds")]
public class LocalParentIds
{

  [XmlElement(ElementName = "value")]
  public List<string> Value { get; set; }
}

[XmlRoot(ElementName = "cachedAggregatingParentIds")]
public class CachedAggregatingParentIds
{

  [XmlElement(ElementName = "value")]
  public string Value { get; set; }
}

[XmlRoot(ElementName = "providerDefinitionIds")]
public class ProviderDefinitionIds
{

  [XmlElement(ElementName = "value")]
  public List<string> Value { get; set; }
}

[XmlRoot(ElementName = "uris")]
public class Uris
{

  [XmlElement(ElementName = "value")]
  public string Value { get; set; }
}

[XmlRoot(ElementName = "dataPersistHashes")]
public class DataPersistHashes
{

  [XmlElement(ElementName = "value")]
  public List<string> Value { get; set; }
}

[XmlRoot(ElementName = "vocabulariesUsed")]
public class VocabulariesUsed
{

  [XmlElement(ElementName = "value")]
  public List<string> Value { get; set; }
}

[XmlRoot(ElementName = "dataClasses")]
public class DataClasses
{

  [XmlElement(ElementName = "value")]
  public List<string> Value { get; set; }
}

[XmlRoot(ElementName = "dataDescription")]
public class DataDescription
{

  [XmlElement(ElementName = "dataClasses")]
  public DataClasses DataClasses { get; set; }
}

[XmlRoot(ElementName = "processedData")]
public class ProcessedData
{

  [XmlElement(ElementName = "entityType")]
  public string EntityType { get; set; }

  [XmlElement(ElementName = "name")]
  public string Name { get; set; }

  [XmlElement(ElementName = "aliases")]
  public Aliases Aliases { get; set; }

  [XmlElement(ElementName = "codes")]
  public Codes Codes { get; set; }

  [XmlElement(ElementName = "createdDate")]
  public DateTime CreatedDate { get; set; }

  [XmlElement(ElementName = "modifiedDate")]
  public DateTime ModifiedDate { get; set; }

  [XmlElement(ElementName = "description")]
  public string Description { get; set; }

  [XmlElement(ElementName = "discoveryDate")]
  public DateTime DiscoveryDate { get; set; }

  [XmlElement(ElementName = "edges")]
  public Edges Edges { get; set; }

  [XmlElement(ElementName = "edgesSummary")]
  public EdgesSummary EdgesSummary { get; set; }

  [XmlElement(ElementName = "externalReferences")]
  public ExternalReferences ExternalReferences { get; set; }

  [XmlElement(ElementName = "properties")]
  public Properties Properties { get; set; }

  [XmlElement(ElementName = "tags")]
  public Tags Tags { get; set; }

  [XmlElement(ElementName = "isSensitiveInformation")]
  public bool IsSensitiveInformation { get; set; }

  [XmlElement(ElementName = "sortDate")]
  public DateTime SortDate { get; set; }

  [XmlElement(ElementName = "timeToLive")]
  public int TimeToLive { get; set; }

  [XmlElement(ElementName = "isShadowEntity")]
  public bool IsShadowEntity { get; set; }

  [XmlElement(ElementName = "parentIds")]
  public ParentIds ParentIds { get; set; }

  [XmlElement(ElementName = "localParentIds")]
  public LocalParentIds LocalParentIds { get; set; }

  [XmlElement(ElementName = "cachedAggregatingParentIds")]
  public CachedAggregatingParentIds CachedAggregatingParentIds { get; set; }

  [XmlElement(ElementName = "providerDefinitionIds")]
  public ProviderDefinitionIds ProviderDefinitionIds { get; set; }

  [XmlElement(ElementName = "uris")]
  public Uris Uris { get; set; }

  [XmlElement(ElementName = "dataPersistHashes")]
  public DataPersistHashes DataPersistHashes { get; set; }

  [XmlElement(ElementName = "vocabulariesUsed")]
  public VocabulariesUsed VocabulariesUsed { get; set; }

  [XmlElement(ElementName = "dataDescription")]
  public DataDescription DataDescription { get; set; }

  [XmlAttribute(AttributeName = "origin")]
  public string Origin { get; set; }

  [XmlText]
  public string Text { get; set; }

  [XmlAttribute(AttributeName = "ref")]
  public int Ref { get; set; }
}

[XmlRoot(ElementName = "sameAs")]
public class SameAs
{

  [XmlAttribute(AttributeName = "ref")]
  public int Ref { get; set; }

  [XmlAttribute(AttributeName = "code")]
  public string Code { get; set; }
}

[XmlRoot(ElementName = "mapping")]
public class Mapping
{

  [XmlElement(ElementName = "sameAs")]
  public List<SameAs> SameAs { get; set; }

  [XmlAttribute(AttributeName = "id")]
  public int Id { get; set; }

  [XmlAttribute(AttributeName = "createdDate")]
  public DateTime CreatedDate { get; set; }

  [XmlAttribute(AttributeName = "modifiedDate")]
  public DateTime ModifiedDate { get; set; }

  [XmlAttribute(AttributeName = "discoveryDate")]
  public DateTime DiscoveryDate { get; set; }
}

[XmlRoot(ElementName = "dataMappings")]
public class DataMappings
{

  [XmlElement(ElementName = "mapping")]
  public Mapping Mapping { get; set; }
}

[XmlRoot(ElementName = "change")]
public class Change
{

  [XmlAttribute(AttributeName = "type")]
  public string Type { get; set; }

  [XmlAttribute(AttributeName = "changedPart")]
  public string ChangedPart { get; set; }

  [XmlAttribute(AttributeName = "key")]
  public string Key { get; set; }

  [XmlAttribute(AttributeName = "newValue")]
  public string NewValue { get; set; }
}

[XmlRoot(ElementName = "version")]
public class Version
{

  [XmlElement(ElementName = "change")]
  public List<Change> Change { get; set; }

  [XmlAttribute(AttributeName = "dataReference")]
  public int DataReference { get; set; }

  [XmlAttribute(AttributeName = "childVersions")]
  public int ChildVersions { get; set; }

  [XmlAttribute(AttributeName = "date")]
  public DateTime Date { get; set; }

  [XmlAttribute(AttributeName = "changeType")]
  public string ChangeType { get; set; }

  [XmlAttribute(AttributeName = "changeCount")]
  public int ChangeCount { get; set; }

  [XmlAttribute(AttributeName = "editDistance")]
  public int EditDistance { get; set; }

  [XmlAttribute(AttributeName = "branch")]
  public string Branch { get; set; }

  [XmlAttribute(AttributeName = "parentVersions")]
  public int ParentVersions { get; set; }
}

[XmlRoot(ElementName = "versionHistory")]
public class VersionHistory
{

  [XmlElement(ElementName = "version")]
  public List<Version> Version { get; set; }
}

[XmlRoot(ElementName = "processingFlags")]
public class ProcessingFlags
{

  [XmlElement(ElementName = "isEdgesProcessed")]
  public bool IsEdgesProcessed { get; set; }
}

[XmlRoot(ElementName = "entityData")]
public class EntityData
{

  [XmlElement(ElementName = "entityType")]
  public string EntityType { get; set; }

  [XmlElement(ElementName = "codes")]
  public Codes Codes { get; set; }

  [XmlElement(ElementName = "discoveryDate")]
  public DateTime DiscoveryDate { get; set; }

  [XmlElement(ElementName = "edges")]
  public Edges Edges { get; set; }

  [XmlElement(ElementName = "edgesSummary")]
  public object EdgesSummary { get; set; }

  [XmlAttribute(AttributeName = "id")]
  public int Id { get; set; }

  [XmlAttribute(AttributeName = "origin")]
  public string Origin { get; set; }

  [XmlText]
  public string Text { get; set; }

  [XmlElement(ElementName = "name")]
  public string Name { get; set; }

  [XmlElement(ElementName = "properties")]
  public Properties Properties { get; set; }

  [XmlElement(ElementName = "aliases")]
  public Aliases Aliases { get; set; }

  [XmlElement(ElementName = "uri")]
  public string Uri { get; set; }

  [XmlElement(ElementName = "createdDate")]
  public DateTime CreatedDate { get; set; }

  [XmlElement(ElementName = "modifiedDate")]
  public DateTime ModifiedDate { get; set; }

  [XmlElement(ElementName = "tags")]
  public Tags Tags { get; set; }
}

[XmlRoot(ElementName = "data")]
public class Data
{

  [XmlElement(ElementName = "processingFlags")]
  public ProcessingFlags ProcessingFlags { get; set; }

  [XmlElement(ElementName = "entityData")]
  public EntityData EntityData { get; set; }

  [XmlElement(ElementName = "processedData")]
  public ProcessedData ProcessedData { get; set; }

  [XmlAttribute(AttributeName = "persistHash")]
  public string PersistHash { get; set; }

  [XmlAttribute(AttributeName = "originProviderDefinitionId")]
  public string OriginProviderDefinitionId { get; set; }

  [XmlAttribute(AttributeName = "id")]
  public int Id { get; set; }

  [XmlAttribute(AttributeName = "origin")]
  public string Origin { get; set; }

  [XmlAttribute(AttributeName = "appVersion")]
  public string AppVersion { get; set; }

  [XmlText]
  public string Text { get; set; }
}

[XmlRoot(ElementName = "removed")]
public class Removed
{

  [XmlElement(ElementName = "value")]
  public double Value { get; set; }
}

[XmlRoot(ElementName = "remove-property")]
public class Removeproperty
{

  [XmlAttribute(AttributeName = "key")]
  public string Key { get; set; }

  [XmlText]
  public string Text { get; set; }
}

[XmlRoot(ElementName = "details")]
public class Details
{

  [XmlElement(ElementName = "dataMappings")]
  public DataMappings DataMappings { get; set; }

  [XmlElement(ElementName = "versionHistory")]
  public VersionHistory VersionHistory { get; set; }

  [XmlElement(ElementName = "data")]
  public List<Data> Data { get; set; }

  [XmlElement(ElementName = "references")]
  public object References { get; set; }
}

[XmlRoot(ElementName = "entity")]
public class Entity
{

  [XmlElement(ElementName = "processedData")]
  public ProcessedData ProcessedData { get; set; }

  [XmlElement(ElementName = "details")]
  public Details Details { get; set; }

  [XmlAttribute(AttributeName = "id")]
  public string Id { get; set; }

  [XmlAttribute(AttributeName = "organization")]
  public string Organization { get; set; }

  [XmlAttribute(AttributeName = "persistVersion")]
  public int PersistVersion { get; set; }

  [XmlAttribute(AttributeName = "persistHash")]
  public string PersistHash { get; set; }

  [XmlText]
  public string Text { get; set; }
}

#endregion