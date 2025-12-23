<Query Kind="Program">
  <NuGetReference>Dapper</NuGetReference>
  <NuGetReference>Microsoft.Data.SqlClient</NuGetReference>
  <Namespace>Microsoft.Data.SqlClient</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

//NOTE: loading code from other files within 'My Queries"
#load "DAL\SqlFactory"
#load "Extensions\EnumerableExtensions\ToCSV Examples"
#load "Extensions\EnumerableExtensions\HasItems + IsNullOrEmpty + Random + Chunk"

void Main()
{
  
}

public interface IUserRepository
{
  Task<IEnumerable<UserEntity>> GetAllAsync(CancellationToken token = default);

  Task<UserEntity> GetByIDAsync(int id, CancellationToken token = default);
  Task<IEnumerable<UserEntity>> GetByIDsAsync(IEnumerable<int> ids, CancellationToken token = default);

  Task<UserEntity> CreateAsync(UserEntity entity, CancellationToken token = default);
  
  Task UpdateAsync(UserEntity entity, CancellationToken token = default);
  Task DeleteAsync(int id, CancellationToken token = default);
}

public class UserRepository : Repository, IUserRepository
{
  public UserRepository(string db_connection_string) 
    : base(db_connection_string) { }
  
  public Task<IEnumerable<UserEntity>> GetAllAsync(CancellationToken token = default)
  {
    throw new NotImplementedException();
  }

  public async Task<UserEntity> GetByIDAsync(int id, CancellationToken token = default)
    => (await GetByIDsAsync([id], token)).FirstOrDefault();

  public Task<IEnumerable<UserEntity>> GetByIDsAsync(IEnumerable<int> ids, CancellationToken token = default)
  {
    throw new NotImplementedException();
  }

  public Task<UserEntity> CreateAsync(UserEntity entity, CancellationToken token = default)
  {
    throw new NotImplementedException();
  }

  public Task UpdateAsync(UserEntity entity, CancellationToken token = default)
  {
    throw new NotImplementedException();
  }

  public Task DeleteAsync(int id, CancellationToken token = default)
  {
    throw new NotImplementedException();
  }
}

#region abstract classes

public abstract class Repository
{
  public readonly IDbConnection dbConnection;

  protected Repository(string db_connection_string)
  {
    dbConnection = new SqlConnection(db_connection_string);
  }
}

#endregion

#region Database Entities

public class UserEntity
{
  public int ID             { get; set; }
  public string FirstName   { get; set; }
  public string LastName    { get; set; }
}

public class ContactEntity
{
  public int ID           {  get; set; }
  public string FirstName { get; set; } = string.Empty;
  public string LastName  { get; set; } = string.Empty;

  public string Email     { get; set; } = string.Empty;
  public string Company   { get; set; } = string.Empty;
  public string Title     { get; set; } = string.Empty;

  public bool IsNew => ID == default;
  public bool IsDeleted   { get; set; }
  
  public List<AddressEntity> Addresses { get; set; } = [];
}

public class AddressEntity
{
  public int ID               { get; set; }
  public int ContactID        { get; set; }
  public string AddressType   { get; set; } = string.Empty;
  public string StreetAddress { get; set; } = string.Empty;
  public string City          { get; set; } = string.Empty;
  public int StateID          { get; set; }
  public string PostalCode    { get; set; } = string.Empty;

  internal bool IsNew => ID == default;
  public bool IsDeleted       { get; set; }
}

public class StateEntity
{
  public int ID               { get; set; }
  public string Name          { get; set; } = string.Empty;
  public string Abbreviation  { get; set; } = string.Empty;

  public bool IsNew => ID == default;
  public bool IsDeleted { get; set; }
}

#endregion