<Query Kind="Program">
  <NuGetReference>Dapper</NuGetReference>
  <NuGetReference>Microsoft.Data.SqlClient</NuGetReference>
  <Namespace>Dapper</Namespace>
  <Namespace>Microsoft.Data.SqlClient</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

//NOTE: loading code from other files within 'My Queries"
#load "DAL\SqlFactory"
#load "Extensions\EnumerableExtensions\ToCSV Examples"
#load "Extensions\EnumerableExtensions\HasItems + IsNullOrEmpty + Random + Chunk"

async Task Main()
{
  var user_repository     = RepositoryFactory.CreateUserRepository();
  var contact_repository  = RepositoryFactory.CreateContactRepository();
  
  //IEnumerable<ContactEntity> contacts = await contact_repository.GetAllAsync();
  //contacts.Dump("contact_repository.GetAllAsync()", 0);
  //
  //var ids  = new List<int> { 1, 3, 5, 7 };
  //contacts = await contact_repository.GetByIDsAsync(ids);  
  //contacts.Dump("contact_repository.GetAllAsync( {1, 3, 5, 7} )", 0);
  //
  //var id = ids.Random(1).First();
  //var contact = await contact_repository.GetByIDAsync(id);
  //contact.Dump($"contact_repository.GetByIDAsync( {id} )", 0);
  
  // ----------------------------------------------------------------------------------------------
  var contact = new ContactEntity { FirstName = "Joe"
                                   ,LastName  = "Blow"
                                   ,Email     = "joe.blow@gmail.com"
                                   ,Company   = "Microsoft"
                                   ,Title     = "Developer" };
  
  contact.Dump("contact (w/o ID) - before CreateAsync", 0);
  
  var result = await contact_repository.InsertAsync(contact);
  
  contact.Dump("contact - after CreateAsync", 0);
  result.Dump("result from CreateAsync", 0);
  
  // ----------------------------------------------------------------------------------------------
  var id = contact.ID;
  contact = null;
  contact.Dump($"contact is null: '{contact is null}'", 0);

  contact = await contact_repository.GetByIDAsync(id);
  contact.Dump($"contact from GetByIDAsync( {id} )", 0);
  
  // ----------------------------------------------------------------------------------------------
  contact.Company = "Updated Company";
  
  var update_successful = await contact_repository.UpdateAsync(contact);
  update_successful.Dump("update_successful");

  var updated_contact = await contact_repository.GetByIDAsync(contact.ID);
  updated_contact.Dump($"updated_contact from GetByIDAsync( {contact.ID} )", 0);
  
  // ----------------------------------------------------------------------------------------------
  var delete_successful = await contact_repository.DeleteAsync(updated_contact.ID);
  delete_successful.Dump("delete_successful");
}

#region IUserRepository/UserRepository

public interface IUserRepository : IRepository<UserEntity>
{
//  Task<IEnumerable<UserEntity>> GetAllAsync(CancellationToken token = default);
//
//  Task<UserEntity> GetByIDAsync(int id, CancellationToken token = default);
//  Task<IEnumerable<UserEntity>> GetByIDsAsync(IEnumerable<int> ids, CancellationToken token = default);
//
//  Task<UserEntity> CreateAsync(UserEntity entity, CancellationToken token = default);
//  
//  Task UpdateAsync(UserEntity entity, CancellationToken token = default);
//  Task DeleteAsync(int id, CancellationToken token = default);
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
  {
    var results = await GetByIDsAsync([id], ",", token);
    return results.FirstOrDefault();
  }

  public Task<IEnumerable<UserEntity>> GetByIDsAsync(IEnumerable<int> ids, string separator = ",", CancellationToken token = default)
  {
    throw new NotImplementedException();
  }

  public Task<UserEntity> InsertAsync(UserEntity entity, CancellationToken token = default)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(UserEntity entity, CancellationToken token = default)
  {
    throw new NotImplementedException();
  }

  public Task<bool> DeleteAsync(int id, CancellationToken token = default)
  {
    throw new NotImplementedException();
  }
}

#endregion

#region IContactRepository/ContactRepository

public interface IContactRepository : IRepository<ContactEntity>
{
}

public class ContactRepository : Repository<ContactEntity>, IContactRepository
{
  protected override string GetAllStoredProcedureName    => "ContactGetAll";
  protected override string GetByIDsStoredProcedureName  => "ContactGetByID";
  protected override string InsertStoredProcedureName    => "ContactInsert";
  protected override string UpdateStoredProcedureName    => "ContactUpdate";
  protected override string DeleteStoredProcedureName    => "ContactDelete";
  
  public ContactRepository(string db_connection_string)
    : base(db_connection_string) { }

  protected override void AddInsertParameters(DynamicParameters parameters, ContactEntity contact)
  {
    //Initialized base entity 'parameters' ...
    base.AddInsertParameters(parameters, contact);
    
    //Add entity specific paramters
    parameters.Add("@FirstName", contact.FirstName);
    parameters.Add("@LastName", contact.LastName);
    
    parameters.Add("@Company", contact.Company);
    parameters.Add("@Title", contact.Title);
    parameters.Add("@Email", contact.Email);
  }

  protected override void AddUpdateParameters(DynamicParameters parameters, ContactEntity contact)
  {
    //Initialized base entity 'parameters' ...
    base.AddUpdateParameters(parameters, contact);

    //Add entity specific paramters
    parameters.Add("@FirstName", contact.FirstName);
    parameters.Add("@LastName", contact.LastName);

    parameters.Add("@Company", contact.Company);
    parameters.Add("@Title", contact.Title);
    parameters.Add("@Email", contact.Email);
  }
}

#endregion

#region Core Artifacts

public interface IRepository
{
  public IDbConnection dbConnection { get; }
}

public interface IRepository<TEntity> : IRepository where TEntity : class, IdbEntity
{
  Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token = default);

  Task<TEntity> GetByIDAsync(int id, CancellationToken token = default);
  Task<IEnumerable<TEntity>> GetByIDsAsync(IEnumerable<int> ids, string separator = ",", CancellationToken token = default);

  Task<TEntity> InsertAsync(TEntity entity, CancellationToken token = default);

  Task<bool> UpdateAsync(TEntity entity, CancellationToken token = default);
  Task<bool> DeleteAsync(int id, CancellationToken token = default);
}

public abstract class Repository : IRepository
{
  protected readonly IDbConnection _dbConnection;

  protected Repository(string db_connection_string)
  {
    _dbConnection = new SqlConnection(db_connection_string);
  }

  public IDbConnection dbConnection { get => _dbConnection; }
}

public abstract class Repository<TEntity> : Repository, IRepository<TEntity> where TEntity : class, IdbEntity
{
  protected abstract string GetAllStoredProcedureName    { get; }
  protected abstract string GetByIDsStoredProcedureName  { get; }
  protected abstract string InsertStoredProcedureName    { get; }
  protected abstract string UpdateStoredProcedureName    { get; }
  protected abstract string DeleteStoredProcedureName    { get; }
  
  public Repository(string db_connection_string)
    : base(db_connection_string) { }

  public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token = default)
  {  
    var entities = await dbConnection.QueryAsync<TEntity>( GetAllStoredProcedureName
                                                          ,commandType: CommandType.StoredProcedure);
    return entities;
  }

  public virtual async Task<TEntity> GetByIDAsync(int id, CancellationToken token = default)
  {
    var results = await GetByIDsAsync([id], ",", token);
    return results.FirstOrDefault();
  }

  public virtual async Task<IEnumerable<TEntity>> GetByIDsAsync(IEnumerable<int> ids, string separator = ",", CancellationToken token = default)
  {
    var object_ids  = ids.Join(separator);
    var parameters  = new { IDs = object_ids, separator };
    
    return await dbConnection.QueryAsync<TEntity>( sql: GetByIDsStoredProcedureName
                                                  ,param: parameters
                                                  ,commandType: CommandType.StoredProcedure);
  }

  public virtual async Task<TEntity> InsertAsync(TEntity entity, CancellationToken token = default)
  {
    if (entity is null) return default;
    
    var parameters = new DynamicParameters();
    AddInsertParameters(parameters, entity);
    
    await dbConnection.ExecuteAsync( InsertStoredProcedureName
                                    ,param: parameters
                                    ,commandType: CommandType.StoredProcedure );
    
    bool success = HydrateFromInsertCommand(parameters, entity);

    // Success: Return populated entity (Id set from output)
    // Failure: Return null (e.g., 0 rows or error code)
    return success ? entity : default;
  }
  
  public virtual async Task<bool> UpdateAsync(TEntity entity, CancellationToken token = default)
  {
    if(entity is null or { ID: <= 0 })
      throw new ArgumentOutOfRangeException("The entity must have an 'ID' greater than 0 (zero) to update.", nameof(entity));
    
    var parameters = new DynamicParameters();
    AddUpdateParameters(parameters, entity);
    
    await dbConnection.ExecuteAsync( UpdateStoredProcedureName
                                    ,param: parameters
                                    ,commandType: CommandType.StoredProcedure );
    
    return HydrateFromUpdateCommand(parameters);
  }
  
  public virtual async Task<bool> DeleteAsync(int id, CancellationToken token = default)
  {
    if(id <= 0)
      throw new ArgumentOutOfRangeException("The 'ID' must be greater than 0 (zero) to delete.");

    var parameters = new DynamicParameters(new { ID = id });
    AddDeleteParameters(parameters, id);
    
    await dbConnection.ExecuteAsync( DeleteStoredProcedureName
                                    ,new { ID = id }
                                    ,commandType: CommandType.StoredProcedure );
    
    return HydrateFromDeleteCommand(parameters);
  }
  
  #region helper methods
  
  protected virtual void AddInsertParameters(DynamicParameters parameters, TEntity entity)
  {
    parameters.Add( name: "@ID"
                   ,value: entity.ID
                   ,dbType: DbType.Int32
                   ,direction: ParameterDirection.InputOutput );
    
    parameters.Add( name: "@RETURN_VALUE"
                   ,dbType: DbType.Int32
                   ,direction: ParameterDirection.ReturnValue );
  }
  
  protected virtual bool HydrateFromInsertCommand(DynamicParameters parameters, TEntity entity)
  {
    //Get outputs ...
    int id            = parameters.Get<int?>("@ID") ?? -1;
    int return_value  = parameters.Get<int>("@RETURN_VALUE");

    //Check for errors
    if (return_value != 0)
      throw new InvalidOperationException($"Stored Procedure: '{InsertStoredProcedureName}' returned error code '{return_value}'.");
    
    //Set ID on entity
    entity.ID = id;
    
    return id > 0;
  }
  
  protected virtual void AddUpdateParameters(DynamicParameters parameters, TEntity entity)
  {
    parameters.Add( name: "@ID"
                   ,value: entity.ID
                   ,dbType: DbType.Int32
                   ,direction: ParameterDirection.InputOutput );
    
    if(entity is dbDeletableEntity deletable_entity)
      parameters.Add( name: "@Deleted"
                     ,value: deletable_entity.Deleted
                     ,dbType: DbType.Byte
                     ,direction: ParameterDirection.Input );
    
    parameters.Add( name: "@RETURN_VALUE"
                   ,dbType: DbType.Int32
                   ,direction: ParameterDirection.ReturnValue );    
  }
  
  protected virtual bool HydrateFromUpdateCommand(DynamicParameters parameters)
  {
    //Get 'RETURN_VALUE' (error codes)
    int return_value = parameters.Get<int?>("@RETURN_VALUE") ?? 0;

    // Check error
    if (return_value != 0)
      throw new InvalidOperationException($"Stored Procedure: '{UpdateStoredProcedureName}' returned error code '{return_value}'.");

    return return_value == 0;
  }
  
  protected virtual void AddDeleteParameters(DynamicParameters parameters, int id)
  {
    parameters.Add( name: "@ID"
                   ,value: id
                   ,dbType: DbType.Int32
                   ,direction: ParameterDirection.Input );
    
    parameters.Add( name: "@RETURN_VALUE"
                   ,dbType: DbType.Int32
                   ,direction: ParameterDirection.ReturnValue );    
  }
  
  protected virtual bool HydrateFromDeleteCommand(DynamicParameters parameters)
  {
    //Get 'RETURN_VALUE' (error codes)
    int return_value = parameters.Get<int?>("@RETURN_VALUE") ?? 0;

    // Check error
    if (return_value != 0)
      throw new InvalidOperationException($"Stored Procedure: '{DeleteStoredProcedureName}' returned error code '{return_value}'.");

    return return_value == 0;
  }

  #endregion
}

#endregion

#region Database Entities

public interface IdbEntity
{
  int ID      { get; set; }
  bool IsNew  { get; }
}

public abstract class dbEntity : IdbEntity
{
  public virtual int ID { get; set; }
  public virtual bool IsNew => ID == default;
}

public interface IdbDeletableEntity : IdbEntity
{
  bool Deleted   { get; set; }
  bool IsDeleted { get; }
}

public abstract class dbDeletableEntity : dbEntity, IdbDeletableEntity
{
  public virtual bool Deleted { get; set; }
  public virtual bool IsDeleted => Deleted;
}

public class UserEntity : dbDeletableEntity
{
  public string FirstName { get; set; }
  public string LastName  { get; set; }
}

public class ContactEntity : dbEntity
{
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;

  public string Email { get; set; } = string.Empty;
  public string Company { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  
  public List<AddressEntity> Addresses { get; set; } = [];
}

public class AddressEntity : dbEntity
{
  public int ContactID        { get; set; }
  public string AddressType   { get; set; } = string.Empty;
  public string StreetAddress { get; set; } = string.Empty;
  public string City          { get; set; } = string.Empty;
  public int StateID          { get; set; }
  public string PostalCode    { get; set; } = string.Empty;
}

public class StateEntity : dbEntity
{
  public string Name          { get; set; } = string.Empty;
  public string Abbreviation  { get; set; } = string.Empty;
}

#endregion

#region Repository Factory

private static class RepositoryFactory
{
  //NOTE: Builds a connection string manually via SqlFactory;
  //      DO NOT use the 'expression-bodied' syntax here!
  private static readonly string ConnectionString 
    = new SqlFactory().BuildSqlConnection( server: "localhost\\SQL2022"
                                          ,database: "Pluralsight_ContactDB"
                                          ,username: "sa"
                                          ,password: "SQLP@ssw0rd"
                                          ,encrypt: true
                                          ,trust_certificate: true)
                      .ConnectionString;

  public static IUserRepository CreateUserRepository()
    => new UserRepository(ConnectionString);
    
  public static IContactRepository CreateContactRepository()
    => new ContactRepository(ConnectionString);
}


#endregion