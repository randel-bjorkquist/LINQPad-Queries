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
  #region COMMENTED OUT: 'Contact Repository'
  /* */
  
  // ==============================================================================================
  
  var contact_repository = RepositoryFactory.CreateContactRepository();
  
  // ----------------------------------------------------------------------------------------------
  //IEnumerable<ContactEntity> contacts = await contact_repository.GetAllAsync();
  //contacts.Dump("Current list of 'contacts' retrieve via 'contact_repository.GetAllAsync()'", 0);
  //
  //var ids  = new List<int> { 1, 3, 5, 7 };
  //contacts = await contact_repository.GetByIDsAsync(ids);  
  //contacts.Dump("contact_repository.GetAllAsync( {1, 3, 5, 7} )", 0);
  //
  //var id = ids.Random(1).First();
  //var contact = await contact_repository.GetByIDAsync(id);
  //contact.Dump($"contact_repository.GetByIDAsync( {id} )", 0);
  
  // ----------------------------------------------------------------------------------------------
  //var new_contact = new ContactEntity { FirstName = "Joe"
  //                                     ,LastName  = "Blow"
  //                                     ,Email     = "joe.blow@gmail.com"
  //                                     ,Company   = "Microsoft"
  //                                     ,Title     = "Developer" };
  //
  //new_contact.Dump("new_contact (w/o ID) - before InsertAsync", 1);
  //
  //contact = await contact_repository.InsertAsync(new_contact);
  //
  //contact.Dump("contact - after InsertAsync", 1);
  
  // ----------------------------------------------------------------------------------------------
  //var contact_id = contact.ID;
  //contact = null;
  //Console.WriteLine($"contact is null: '{contact is null}'");
  //
  //contact = await contact_repository.GetByIDAsync(contact_id);
  //contact.Dump($"contact from GetByIDAsync( {contact_id} )", 1);
  
  // ----------------------------------------------------------------------------------------------
  //contact.Company = "Updated Company";
  //
  //var update_contact_successful = await contact_repository.UpdateAsync(contact);
  //update_contact_successful.Dump("update_contact_successful");
  //
  //var updated_contact = await contact_repository.GetByIDAsync(contact.ID);
  //updated_contact.Dump($"updated_contact from GetByIDAsync( {contact.ID} )", 1);
  
  // ----------------------------------------------------------------------------------------------
  //var delete_contact_successful = await contact_repository.DeleteAsync(updated_contact.ID);
  //delete_contact_successful.Dump("delete_contact_successful");

  // ----------------------------------------------------------------------------------------------
  //NOTE: GetAll, OrderBy 'LastName', Sort 'DESC' ...
  //var info = new GetAllPaginationDTO(0, "LastName", "DESC", null, null);
  //
  //var contacts = await contact_repository.GetAllAsync(info);
  //contacts.Dump($"contact_repository.GetAllAsync({info})", 0);

  //--------------------------------------------------------------------
  //NOTE: First Page, PageSize = 2, OrderBy 'LastName', Sort 'DESC' ...
  //var info = new GetAllPaginationDTO(2, "LastName", "DESC", null, null);
  //
  //var contacts = await contact_repository.GetAllAsync(info);
  //contacts.Dump($"contact_repository.GetAllAsync({info})", 0);
  
  //--------------------------------------------------------------------
  //NOTE: Last Page, PageSize = 2, OrderBy 'LastName', Sort 'DESC', LastID = 6, LastOrderValue = 'Harden' ...
  var info = new GetAllPaginationDTO(2, "LastName", "DESC", 6, "Harden");
  
  var contacts = await contact_repository.GetAllAsync(info);
  contacts.Dump($"contact_repository.GetAllAsync({info})", 0);

  /* */
  #endregion

  #region 'User Repository'
  /*
  
  // ==============================================================================================
  var user_repository = RepositoryFactory.CreateUserRepository();
  
  IEnumerable<UserEntity> users = await user_repository.GetAllAsync();
  users.Dump("Current list of 'users' retrieve via 'user_repository.GetAllAsync()'", 0);
  
  var ids  = new List<int> { 1, 3, 5, 7 };
  users = await user_repository.GetByIDsAsync(ids);  
  users.Dump("user_repository.GetAllAsync( {1, 3, 5, 7} )", 0);
  
  //var id = ids.Random(1).First();
  //var contact = await contact_repository.GetByIDAsync(id);
  //contact.Dump($"contact_repository.GetByIDAsync( {id} )", 0);
  
  
  // ----------------------------------------------------------------------------------------------
  var new_user = new UserEntity { FirstName = "Fred"
                                 ,LastName  = "Flintstone"
                                 ,Email     = $"Fred.Flintstone@bedrock.bc" };
                                 
  new_user.Dump("new_user (w/o ID) - before InsertAsync", 1);
  
  var user = await user_repository.InsertAsync(new_user);
  
  user.Dump("user - after InsertAsync");

  // ----------------------------------------------------------------------------------------------
  var user_id = user.ID;
  user = null;
  Console.WriteLine($"user is null: '{user is null}'");

  user = await user_repository.GetByIDAsync(user_id);
  user.Dump($"user from GetByIDAsync( {user_id} )", 1);

  // ----------------------------------------------------------------------------------------------
  user.FirstName  = "Barny";
  user.LastName   = "Rubble";
  user.Email      = "Barny.Rubble@bedrock.bd";


  var update_user_successful = await user_repository.UpdateAsync(user);
  update_user_successful.Dump("update_user_successful");

  var updated_user = await user_repository.GetByIDAsync(user.ID);
  updated_user.Dump($"updated_user from GetByIDAsync( {user.ID} )", 1);

  // ----------------------------------------------------------------------------------------------
  var delete_user_successful = await user_repository.DeleteAsync(updated_user.ID);
  delete_user_successful.Dump("delete_user_successful");
  
  */
  #endregion
}

#region IUserRepository/UserRepository

public interface IUserRepository : IRepository<UserEntity>
{
}

public class UserRepository : Repository<UserEntity>, IUserRepository
{
  protected override string GetAllStoredProcedureName   => "UserGetAll";
  protected override string GetByIDStoredProcedureName  => "UserGetByID";
  protected override string InsertStoredProcedureName   => "UserInsert";
  protected override string UpdateStoredProcedureName   => "UserUpdate";
  protected override string DeleteStoredProcedureName   => "UserDelete";
  
  public UserRepository(string db_connection_string)
    : base(db_connection_string) { }

  protected override void AddInsertParameters(DynamicParameters parameters, UserEntity user)
  {
    //Initialized base entity 'parameters' ...
    base.AddInsertParameters(parameters, user);

    //Add entity specific paramters
    parameters.Add("@FirstName" ,user.FirstName);
    parameters.Add("@LastName"  ,user.LastName);
    parameters.Add("@Email"     ,user.Email);
  }

  protected override void AddUpdateParameters(DynamicParameters parameters, UserEntity user)
  {
    //Initialized base entity 'parameters' ...
    base.AddUpdateParameters(parameters, user);

    //Add entity specific paramters
    parameters.Add("@FirstName" ,user.FirstName);
    parameters.Add("@LastName"  ,user.LastName);
    parameters.Add("@Email"     ,user.Email);
  }
}

#endregion

#region IContactRepository/ContactRepository

public interface IContactRepository : IRepository<ContactEntity>
{
  Task<IEnumerable<ContactEntity>> GetAllAsync( GetAllPaginationDTO pagination_info, CancellationToken token = default);
}

public class ContactRepository : Repository<ContactEntity>, IContactRepository
{
  private string GetAllWithPaginationStoredProcedureName => "ContactGetAllWithKeysetPagination";
  
  protected override string GetAllStoredProcedureName   => "ContactGetAll";
  protected override string GetByIDStoredProcedureName  => "ContactGetByID";
  protected override string InsertStoredProcedureName   => "ContactInsert";
  protected override string UpdateStoredProcedureName   => "ContactUpdate";
  protected override string DeleteStoredProcedureName   => "ContactDelete";
  
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
  
  public async Task<IEnumerable<ContactEntity>> GetAllAsync( GetAllPaginationDTO info, CancellationToken token = default)                                                            
  {
    var parameters = new DynamicParameters( new { PageSize        = info.PageSize
                                                 ,OrderByColumn   = info.OrderByColumn
                                                 ,SortDirection   = info.SortOrder });
    
    if(info.LastID is not null)
      parameters.Add( name: "@LastID"
                     ,value: info.LastID
                     ,dbType: DbType.Int32
                     ,direction: ParameterDirection.Input );

    if(info.LastOrderValue is not null)
      parameters.Add( name: "@LastOrderValue"
                     ,value: info.LastOrderValue
                     ,dbType: DbType.Object
                     ,direction: ParameterDirection.Input );
    
    parameters.Add( name: "@NextLastID"
                   ,dbType: DbType.Int32
                   ,direction: ParameterDirection.Output );
    
    parameters.Add( name: "@NextLastOrderValue"
                   ,dbType: DbType.Object
                   ,direction: ParameterDirection.Output );
    
    parameters.Add( name: "@HasMoreRows"
                   ,dbType: DbType.Boolean
                   ,direction: ParameterDirection.Output );    
    
    var contacts = await dbConnection.QueryAsync<ContactEntity>( GetAllWithPaginationStoredProcedureName
                                                                ,param: parameters
                                                                ,commandType: CommandType.StoredProcedure);
                                                                
    var next_last_id          = parameters.Get<int>("@NextLastID");
    var next_last_order_value = parameters.Get<object>("@NextLastOrderValue");
    var has_more_rows         = parameters.Get<bool>("@HasMoreRows");
    
    return contacts;
  }
}

#endregion

#region Core Repository Artifacts

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
  protected virtual string GetAllStoredProcedureName
    => throw new NotImplementedException($"{nameof(GetAllStoredProcedureName)} has not been implemented yet.");
    
  protected virtual string GetByIDStoredProcedureName
    => throw new NotImplementedException($"{nameof(GetByIDStoredProcedureName)} has not been implemented yet.");
    
  protected virtual string InsertStoredProcedureName
    => throw new NotImplementedException($"{nameof(InsertStoredProcedureName)} has not been implemented yet.");
    
  protected virtual string UpdateStoredProcedureName
    => throw new NotImplementedException($"{nameof(UpdateStoredProcedureName)} has not been implemented yet.");

  protected virtual string DeleteStoredProcedureName
    => throw new NotImplementedException($"{nameof(DeleteStoredProcedureName)} has not been implemented yet.");
  
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
    
    return await dbConnection.QueryAsync<TEntity>( sql: GetByIDStoredProcedureName
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
                                    ,param: parameters
                                    ,commandType: CommandType.StoredProcedure );
    
    return HydrateFromDeleteCommand(parameters);
  }
  
  #region helper methods
  
  protected virtual void AddInsertParameters(DynamicParameters parameters, TEntity entity)
  {
    parameters.Add( name: "@ID"
                   ,value: entity.ID
                   ,dbType: DbType.Int32
                   ,direction: ParameterDirection.Output );
    
    parameters.Add( name: "@RETURN_VALUE"
                   ,dbType: DbType.Int32
                   ,direction: ParameterDirection.ReturnValue );
  }
  
  protected virtual bool HydrateFromInsertCommand(DynamicParameters parameters, TEntity entity)
  {
    //Get outputs ...
    int id            = parameters.Get<int>("@ID");
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
                   ,direction: ParameterDirection.Input );
    
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
    int return_value = parameters.Get<int>("@RETURN_VALUE");

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
    int return_value = parameters.Get<int>("@RETURN_VALUE");

    // Check error
    if (return_value != 0)
      throw new InvalidOperationException($"Stored Procedure: '{DeleteStoredProcedureName}' returned error code '{return_value}'.");

    return return_value == 0;
  }

  #endregion
}

#endregion

#region Core Pagination DTOs

public record GetAllPaginationDTO(int PageSize, string OrderByColumn, string SortOrder, int? LastID = null, object LastOrderValue = null)
{
  // New properties populated by repo for next page
  //public int? NextLastID            { get; init; }
  //public string? NextLastOrderValue { get; init; }
  //public bool HasMoreRows           { get; init; }
}

#endregion

#region Database Interfaces & abstract Entities

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

#endregion

#region Database Entities

public class UserEntity : dbDeletableEntity
{
  public string FirstName { get; set; } = string.Empty;
  public string LastName  { get; set; } = string.Empty;
  public string Email     { get; set; } = string.Empty;
}

public class ContactEntity : dbEntity
{
  public string FirstName { get; set; } = string.Empty;
  public string LastName  { get; set; } = string.Empty;

  public string Email     { get; set; } = string.Empty;
  public string Company   { get; set; } = string.Empty;
  public string Title     { get; set; } = string.Empty;

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