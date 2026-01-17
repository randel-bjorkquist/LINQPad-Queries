<Query Kind="Program">
  <NuGetReference>Dapper</NuGetReference>
  <NuGetReference>Microsoft.Data.SqlClient</NuGetReference>
  <Namespace>Dapper</Namespace>
  <Namespace>Microsoft.Data.SqlClient</Namespace>
  <Namespace>Microsoft.Data.SqlClient.Server</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

//NOTE: loading code from other files within 'My Queries"
#load "DAL\SqlFactory"
#load ".\Builder Pattern - FilterOptions"
#load "Extensions\EnumerableExtensions\ToCSV Examples"
#load "Extensions\EnumerableExtensions\HasItems + IsNullOrEmpty + Random + Chunk"

async Task Main()
{
  #region 'Contact Repository'
  
  // ==============================================================================================  
  var contact_repository = RepositoryFactory.CreateContactRepository();
  
  // ----------------------------------------------------------------------------------------------
  //IEnumerable<ContactEntity> contacts = await contact_repository.GetAllAsync();
  //contacts.Dump("Current list of 'contacts' retrieve via 'contact_repository.GetAllAsync()'", 0);
  
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
  //NOTE: GetAll, OrderBy 'LastName', Sort 'SortDirection.DESC' ...
  //var all_contacts_request = new CursorPaginationRequest( PageSize: 0
  //                                                       ,OrderByColumn: "LastName"
  //                                                       ,SortDirection: SortDirection.DESC);
  //
  //var all_contacts = await contact_repository.GetAllAsync(all_contacts_request);
  //all_contacts.Dump($"contact_repository.GetAllAsync({all_contacts_request})", 0);
  
  //-------------------------------------------------------------------------------
  //var contact_cursor_request  = new CursorPaginationRequest( PageSize: 3
  //                                                          ,OrderByColumn: "LastName"
  //                                                          ,SortDirection: SortDirection.DESC );
  //
  //var contact_cursor_metadata = new CursorPaginationMetadata( NextID:    null
  //                                                           ,NextValue: null
  //                                                           ,HasMore:   true);  //NOTE: set 'HasMore' = true to start
  //
  //while(contact_cursor_metadata.HasMore)
  //{    
  //  var contact_cursor_result = await contact_repository.GetAllAsync(contact_cursor_request);
  //  contact_cursor_result.Dump($"contact_repository.GetAllAsync({contact_cursor_request})", 0);
  //  
  //  contact_cursor_metadata = contact_cursor_result.metadata;
  //  contact_cursor_request  = new CursorPaginationRequest( contact_cursor_request.PageSize
  //                                                        ,contact_cursor_request.OrderByColumn
  //                                                        ,contact_cursor_request.SortDirection
  //                                                        ,contact_cursor_metadata.NextID
  //                                                        ,contact_cursor_metadata.NextValue );
  //};
  
  //-------------------------------------------------------------------------------
  //var contact_offset_request  = new OffsetPaginationRequest( PageNumber: 1
  //                                                          ,PageSize: 5
  //                                                          ,OrderByColumn: "LastName"
  //                                                          ,SortDirection: SortDirection.DESC );
  //                                                  
  ////var contact_offset_metadata = new OffsetPaginationMetadata( TotalRows:   0
  ////                                                           ,CurrentPage: 0
  ////                                                           ,PageSize:    0 );
  //
  //var contact_offset_result = await contact_repository.GetAllAsync(contact_offset_request);
  //contact_offset_result.Dump($"contact_repository.GetAllAsync({contact_offset_request})", 0);
  //
  #endregion

  #region 'User Repository'
  
  // ==============================================================================================
  var user_repository = RepositoryFactory.CreateUserRepository();

  // ----------------------------------------------------------------------------------------------
  //IEnumerable<UserEntity> users = await user_repository.GetAllAsync();
  //users.Dump("Current list of 'users' retrieve via 'user_repository.GetAllAsync()'", 0);

  //var ids  = new List<int> { 1, 3, 5, 7 };
  //users = await user_repository.GetByIDsAsync(ids);  
  //users.Dump("user_repository.GetAllAsync( {1, 3, 5, 7} )", 0);
  //
  ////var id = ids.Random(1).First();
  ////var contact = await contact_repository.GetByIDAsync(id);
  ////contact.Dump($"contact_repository.GetByIDAsync( {id} )", 0);

  // ----------------------------------------------------------------------------------------------
  //var new_user = new UserEntity { FirstName = "Fred"
  //                               ,LastName  = "Flintstone"
  //                               ,Email     = $"Fred.Flintstone@bedrock.bc" };
  //                               
  //new_user.Dump("new_user (w/o ID) - before InsertAsync", 1);
  //
  //var user = await user_repository.InsertAsync(new_user);
  //
  //user.Dump("user - after InsertAsync");

  // ----------------------------------------------------------------------------------------------
  //var user_id = user.ID;
  //user = null;
  //Console.WriteLine($"user is null: '{user is null}'");
  //
  //user = await user_repository.GetByIDAsync(user_id);
  //user.Dump($"user from GetByIDAsync( {user_id} )", 1);

  // ----------------------------------------------------------------------------------------------
  //user.FirstName  = "Barny";
  //user.LastName   = "Rubble";
  //user.Email      = "Barny.Rubble@bedrock.bd";
  //
  //var update_user_successful = await user_repository.UpdateAsync(user);
  //update_user_successful.Dump("update_user_successful");
  //
  //var updated_user = await user_repository.GetByIDAsync(user.ID);
  //updated_user.Dump($"updated_user from GetByIDAsync( {user.ID} )", 1);

  // ----------------------------------------------------------------------------------------------
  //var delete_user_successful = await user_repository.DeleteAsync(updated_user.ID);
  //delete_user_successful.Dump("delete_user_successful");

  // ----------------------------------------------------------------------------------------------
  //NOTE: GetAll, OrderBy 'LastName', Sort 'SortDirection.DESC' ...
  //var all_users_request = new CursorPaginationRequest( PageSize: 0
  //                                                    ,OrderByColumn: "LastName"
  //                                                    ,SortDirection: SortDirection.DESC );
  //
  //var all_users = await user_repository.GetAllAsync(all_users_request);
  //all_users.Dump($"user_repository.GetAllAsync({all_users_request})", 0);

  //-------------------------------------------------------------------------------
  //var user_cursor_request  = new CursorPaginationRequest( PageSize: 3
  //                                                       ,OrderByColumn: "LastName"
  //                                                       ,SortDirection: SortDirection.DESC );
  //
  //var user_cursor_metadata = new CursorPaginationMetadata( NextID:    null
  //                                                        ,NextValue: null
  //                                                        ,HasMore:   true );  //NOTE: set 'HasMore' = true to start
  //
  //while(user_cursor_metadata.HasMore)
  //{    
  //  var user_cursor_result = await user_repository.GetAllAsync(user_cursor_request);
  //  user_cursor_result.Dump($"user_repository.GetAllAsync({user_cursor_request})", 0);
  //  
  //  user_cursor_metadata = user_cursor_result.metadata;
  //  user_cursor_request  = new CursorPaginationRequest( PageSize:      user_cursor_request.PageSize
  //                                                     ,OrderByColumn: user_cursor_request.OrderByColumn
  //                                                     ,SortDirection: user_cursor_request.SortDirection
  //                                                     ,AfterID:       user_cursor_metadata.NextID
  //                                                     ,AfterValue:    user_cursor_metadata.NextValue );
  //};

  //-------------------------------------------------------------------------------
  //var user_offset_request  = new OffsetPaginationRequest( PageNumber: 1
  //                                                       ,PageSize: 3
  //                                                       ,OrderByColumn: "LastName"
  //                                                       ,SortDirection: SortDirection.DESC );
  //                                                  
  ////var offset_metadata = new OffsetPaginationMetadata( TotalRows:   0
  ////                                                   ,CurrentPage: 0
  ////                                                   ,PageSize:    0);
  //
  //var uesr_offset_result = await user_repository.GetAllAsync(user_offset_request);
  //uesr_offset_result.Dump($"user_repository.GetAllAsync({user_offset_request})", 0);
  //
  #endregion

  #region GetAllAsync() w/o FilterOptions

  //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
  //FilterOptions contact_filter_options = null;
  //FilterOptions contact_filter_options = new FilterBuilder().WithColumn("ID").Equals(2);
  //FilterOptions contact_filter_options = new FilterBuilder().WithColumn("ID").GreaterThanOrEqual(4);
  //FilterOptions contact_filter_options = new FilterBuilder().WithColumn("ID").GreaterThan(4);
  //FilterOptions contact_filter_options = new FilterBuilder().WithColumn("ID")
  //                                                            .GreaterThan(4)
  //                                                            .And()
  //                                                            .LessThan(6);
  //FilterOptions contact_filter_options = new FilterBuilder().WithColumn("ID").Between(2, 4, true);
  //FilterOptions contact_filter_options = new FilterBuilder().WithColumn("ID").Between(2, 4, false);
  //FilterOptions contact_filter_options = new FilterBuilder().WithColumn("LastName")
  //                                                            .StartsWith("J")
  //                                                          .ThenColumn("FirstName")
  //                                                            .StartsWith("M")
  //                                                            .Or()
  //                                                            .StartsWith("L");
  //FilterOptions contact_filter_options = new FilterBuilder().WithColumn("LastName").Contains("u")
  //                                                          .And()
  //                                                          .ThenColumn("LastName").Contains("v");  
  //
  //contact_filter_options.Dump("contact_filter_options", 0);
  //IEnumerable<ContactEntity> contacts = await contact_repository.GetAllAsync(contact_filter_options);
  //contacts.Dump($"Current list of 'contacts' retrieve via 'contact_repository.GetAllAsync({contact_filter_options})'", 0);
  
  //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~  
  //FilterOptions user_filter_options = null;
  //FilterOptions user_filter_options = new FilterBuilder().WithColumn("ID").Equals(3);
  //
  //user_filter_options.Dump("user_filter_options", 0);
  //IEnumerable<UserEntity> users = await user_repository.GetAllAsync(user_filter_options);
  //users.Dump($"Current list of 'users' retrieve via 'user_repository.GetAllAsync({user_filter_options})'", 0);
  
  #endregion
}

#region IUserRepository/UserRepository

public interface IUserRepository<UserEntity> : IRepository<UserEntity>  
  where UserEntity : class, IdbDeletableEntity
{
}

public class UserRepository : Repository<UserEntity>, IUserRepository<UserEntity>
{
  protected override string GetAllWithOffsetPaginationStoredProcedureName => "UserGetAllWithOffsetPagination";
  protected override string GetAllWithCursorPaginationStoredProcedureName => "UserGetAllWithKeysetSeekPagination";

  //protected override string GetAllStoredProcedureName   => "UserGetAll";
  protected override string GetAllStoredProcedureName   => "UserGetAllWithFilterOptions";
  
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

  protected override void AddFilterParameters(DynamicParameters parameters, FilterOptions filters = null)
  {
    if(filters?.Criteria == null || !filters.Criteria.Any()) 
      return;
    
    //NOTE: only apply filtering it filters IS NOT NULL/EMPTY ...    
    base.AddFilterParameters(parameters, filters);
    
    var allowed = new DataTable();
    allowed.Columns.Add("ColumnName", typeof(string));
    allowed.Columns.Add("IsNullable", typeof(bool));
    allowed.Columns.Add("DataType", typeof(string));
    
    allowed.Rows.Add("ID", false, "int");
    allowed.Rows.Add("FirstName", false, "nvarchar");
    allowed.Rows.Add("LastName", false, "nvarchar");
    allowed.Rows.Add("Email", false, "nvarchar");
    allowed.Rows.Add("Deleted", false, "bit");
    
    parameters.Add("@AllowedColumns", allowed.AsTableValuedParameter("dbo.AllowedColumnType"));
  }
}

#endregion

#region IContactRepository/ContactRepository

public interface IContactRepository<ContactEntity> : IRepository<ContactEntity>
  where ContactEntity : class, IdbEntity
{
}

public class ContactRepository : Repository<ContactEntity>, IContactRepository<ContactEntity>
{
  protected override string GetAllWithOffsetPaginationStoredProcedureName => "ContactGetAllWithOffsetPagination";
  protected override string GetAllWithCursorPaginationStoredProcedureName => "ContactGetAllWithKeysetSeekPagination";
  
  //protected override string GetAllStoredProcedureName   => "ContactGetAll";
  protected override string GetAllStoredProcedureName   => "ContactGetAllWithFilterOptions";
  
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

  protected override void AddFilterParameters(DynamicParameters parameters, FilterOptions filter_options = null)
  {
    base.AddFilterParameters(parameters, filter_options);
    
    var allowed = new DataTable();
    allowed.Columns.Add("ColumnName", typeof(string));
    allowed.Columns.Add("IsNullable", typeof(bool));
    allowed.Columns.Add("DataType", typeof(string));
    
    allowed.Rows.Add("ID", false, "int");
    allowed.Rows.Add("FirstName", false, "nvarchar");
    allowed.Rows.Add("LastName", false, "nvarchar");
    allowed.Rows.Add("Email", false, "nvarchar");
    allowed.Rows.Add("Company", false, "nvarchar");
    allowed.Rows.Add("Title", false, "nvarchar");
    
    parameters.Add("@AllowedColumns", allowed.AsTableValuedParameter("dbo.AllowedColumnType"));
  }
}

#endregion

#region Core Repository Artifacts

public interface IRepository
{
  public IDbConnection dbConnection { get; }
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

public interface IRepository<TEntity> : IRepository 
  where TEntity : class, IdbEntity
{
  Task<IEnumerable<TEntity>> GetAllAsync(FilterOptions filters = default, CancellationToken token = default);
  Task<OffsetPaginationResponse<TEntity>> GetAllAsync(OffsetPaginationRequest pagination_info, CancellationToken token = default);
  Task<CursorPaginationResponse<TEntity>> GetAllAsync(CursorPaginationRequest pagination_info, CancellationToken token = default);

  Task<TEntity> GetByIDAsync(int id, CancellationToken token = default);
  Task<IEnumerable<TEntity>> GetByIDsAsync(IEnumerable<int> ids, string separator = ",", CancellationToken token = default);

  Task<TEntity> InsertAsync(TEntity entity, CancellationToken token = default);

  Task<bool> UpdateAsync(TEntity entity, CancellationToken token = default);
  Task<bool> DeleteAsync(int id, CancellationToken token = default);
}

public abstract class Repository<TEntity> : Repository, IRepository<TEntity> 
  where TEntity : class, IdbEntity
{
  protected virtual string GetAllStoredProcedureName
    => throw new NotImplementedException($"{nameof(GetAllStoredProcedureName)} has not been implemented yet.");

  protected virtual string GetAllWithOffsetPaginationStoredProcedureName
    => throw new NotImplementedException($"{nameof(GetAllWithOffsetPaginationStoredProcedureName)} has not been implemented yet.");

  protected virtual string GetAllWithCursorPaginationStoredProcedureName
    => throw new NotImplementedException($"{nameof(GetAllWithCursorPaginationStoredProcedureName)} has not been implemented yet.");
    
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

  public virtual async Task<IEnumerable<TEntity>> GetAllAsync(FilterOptions filters, CancellationToken token = default)
  {
    var parameters = new DynamicParameters();
    
    AddFilterParameters(parameters, filters);
    
    var entities = await dbConnection.QueryAsync<TEntity>( GetAllStoredProcedureName
                                                          ,param: parameters
                                                          ,commandType: CommandType.StoredProcedure);
    return entities;
  }
  
  public virtual async Task<OffsetPaginationResponse<TEntity>> GetAllAsync( OffsetPaginationRequest request
                                                                           ,CancellationToken token = default)
  {
    var total_rows = "@TotalRows";
    var parameters = new DynamicParameters( new { PageNumber      = request.PageNumber
                                                 ,PageSize        = request.PageSize
                                                 ,OrderByColumn   = request.OrderByColumn
                                                 ,SortDirection   = request.SortDirection });
    
    parameters.Add( name: total_rows
                   ,dbType: DbType.Int32
                   ,direction: ParameterDirection.Output );    
    
    var entities = await dbConnection.QueryAsync<TEntity>( GetAllWithOffsetPaginationStoredProcedureName
                                                          ,param: parameters
                                                          ,commandType: CommandType.StoredProcedure);
    
    var metadata = new OffsetPaginationMetadata( TotalRows:   parameters.Get<int>(total_rows)
                                                ,CurrentPage: request.PageNumber
                                                ,PageSize:    request.PageSize);

    return new OffsetPaginationResponse<TEntity>(entities, metadata);
  }

  public virtual async Task<CursorPaginationResponse<TEntity>> GetAllAsync( CursorPaginationRequest request
                                                                           ,CancellationToken token = default)
  {
    var after_id    = "@AfterID";
    var after_value = "@AfterValue";
    
    var next_id     = "@NextID";
    var next_value  = "@NextValue";
    var has_more    = "@HasMore";
    
    var parameters = new DynamicParameters( new { PageSize        = request.PageSize
                                                 ,OrderByColumn   = request.OrderByColumn
                                                 ,SortDirection   = request.SortDirection });
    
    if(request.AfterID is not null)
      parameters.Add( name: after_id
                     ,value: request.AfterID
                     ,dbType: DbType.Int32
                     ,direction: ParameterDirection.Input );

    if(request.AfterValue is not null)
      parameters.Add( name: after_value
                     ,value: request.AfterValue
                     ,dbType: DbType.Object
                     ,direction: ParameterDirection.Input );
    
    parameters.Add( name: next_id
                   ,dbType: DbType.Int32
                   ,direction: ParameterDirection.Output );
    
    parameters.Add( name: next_value
                   ,dbType: DbType.Object
                   ,direction: ParameterDirection.Output );
    
    parameters.Add( name: has_more
                   ,dbType: DbType.Boolean
                   ,direction: ParameterDirection.Output );
    
    var entities = await dbConnection.QueryAsync<TEntity>( GetAllWithCursorPaginationStoredProcedureName
                                                          ,param: parameters
                                                          ,commandType: CommandType.StoredProcedure);
    
    var metadata = new CursorPaginationMetadata( NextID:    parameters.Get<int?>(next_id)
                                                ,NextValue: parameters.Get<object>(next_value)
                                                ,HasMore:   parameters.Get<bool>(has_more));

    return new CursorPaginationResponse<TEntity>(entities, metadata);
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
  
  protected virtual void AddFilterParameters(DynamicParameters parameters, FilterOptions filters)
  {
    if(filters?.Criteria == null || !filters.Criteria.Any()) 
      return;
    
    int index    = 0;
    var tvp_rows = new List<FilterRow>();    
    
    foreach(var kvp in filters.Criteria)
    {
      foreach(var condition in kvp.Value)
      {
        var row = new FilterRow( ColumnName:      kvp.Key
                                ,Operator:        condition.Op.ToString()
                                ,LogicalOperator: condition.Logical.ToString()
                                ,Value:           condition.Value //SQL_VARIANT - Dapper handles this correctly
                                ,ParameterIndex:  index );
        
        tvp_rows.Add(row);
        
        index++;
      }
    }
    
    var records = tvp_rows.ToSqlDataRecords();
    parameters.Add( "@Filters"
                   ,new SqlDataRecordTvp(records, "FilterCriteriaType") );
 }
  
  #endregion
}

#endregion

#region Core Pagination DTOs (Request, Response, Metadata)

public static class SortDirection
{
  public static string ASC  => "ASC";
  public static string DESC => "DESC";
}

public record CursorPaginationRequest(int PageSize = 20, string OrderByColumn = "ID", string SortDirection = "ASC", int? AfterID = null, object AfterValue = null);
public record CursorPaginationResponse<TEntity>(IEnumerable<TEntity> entities, CursorPaginationMetadata metadata);
public record CursorPaginationMetadata(int? NextID, object NextValue, bool HasMore);

public record OffsetPaginationRequest(int PageNumber = 1, int PageSize = 20, string OrderByColumn = "ID", string SortDirection = "ASC");
public record OffsetPaginationResponse<TEntity>(IEnumerable<TEntity> entities, OffsetPaginationMetadata metadata);
public record OffsetPaginationMetadata(int TotalRows, int CurrentPage, int PageSize)
{
  public int PreviousPage => CurrentPage - 1;
  public int NextPage     => CurrentPage + 1;
  public int TotalPages   => (int)Math.Ceiling((double)TotalRows / PageSize);

  public bool HasPrevious => CurrentPage > 1;
  public bool HasNext     => CurrentPage < TotalPages;
}

#endregion

#region COMMENTED OUT: R&D CODE (FilterOptions)
/*

public class UserFilterOptions: FilterOptions
//public record UserFilterOptions: FilterOptions
{
  // Strongly-typed helper (optional but nice)
  //public static FilterBuilder<UserFilterOptions> Builder() => new();
  
  public bool IncludeDeletedUsers { get; set; } = false;
}

public class ContactFilterOptions : FilterOptions
//public record ContactFilterOptions : FilterOptions
{
  // Strongly-typed helper (optional but nice)
  //public static FilterBuilder<ContactFilterOptions> Builder() => new();  
}

*/
#endregion

#region Database Interfaces & abstract Entities

public interface IdbEntity
{
  int ID { get; set; }
  bool IsNew { get; }
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
  //NOTE: using Windows Authentication ...
  //NOTE: Builds a connection string manually via SqlFactory; DO NOT use the 'expression-bodied' 
  //      syntax here, as doing so will create new ConnectionsStrings each time it's called!
  private static readonly string ConnectionString 
    = new SqlFactory().BuildSqlConnection( server: "localhost\\SQL2022"
                                          ,database: "Pluralsight_ContactDB"
                                          ,encrypt: true
                                          ,trust_certificate: true)
                      .ConnectionString;

  public static IUserRepository<UserEntity> CreateUserRepository()
    => new UserRepository(ConnectionString);
    
  public static IContactRepository<ContactEntity> CreateContactRepository()
    => new ContactRepository(ConnectionString);
}

#endregion

public sealed record FilterRow(string ColumnName, string Operator, string LogicalOperator, object Value, int ParameterIndex);

public static class SqlDataRecordExtensions
{
  public static IEnumerable<SqlDataRecord> ToSqlDataRecords(this IEnumerable<FilterRow> rows)
    => rows?.Select(r => r.ToSqlDataRecord()) ?? [];

  public static SqlDataRecord ToSqlDataRecord(this FilterRow row)
  {
    var meta = new [] { new SqlMetaData("ColumnName"      ,SqlDbType.NVarChar ,100)
                       ,new SqlMetaData("Operator"        ,SqlDbType.NVarChar , 20)
                       ,new SqlMetaData("LogicalOperator" ,SqlDbType.NVarChar , 10)
                       ,new SqlMetaData("Value"           ,SqlDbType.Variant)
                       ,new SqlMetaData("ParameterIndex"  ,SqlDbType.Int) };
  
  
    var _record = new SqlDataRecord(meta);
    
    _record.SetString(0, row.ColumnName);
    _record.SetString(1, row.Operator);
    _record.SetString(2, row.LogicalOperator);
    
    if(row.Value is null)
      _record.SetDBNull(3);
    else
      _record.SetValue(3, row.Value);
    
    _record.SetInt32(4, row.ParameterIndex);
    
    return _record;
  }
}

public sealed class SqlDataRecordTvp : SqlMapper.ICustomQueryParameter
{
  private readonly IEnumerable<SqlDataRecord> _records;
  private readonly string _typeName;
  
  public SqlDataRecordTvp(IEnumerable<SqlDataRecord> records, string typeName)
  {
    _records  = records;
    _typeName = typeName;
  }
  
  public void AddParameter(IDbCommand command, string name)
  {
    var sql_command = (SqlCommand)command;
    
    var p = sql_command.Parameters.Add(name, SqlDbType.Structured);
    p.TypeName = _typeName;   //e.g. dbo.FilterCriteriaType
    p.Value    = _records;    // IEnumerable<SqlDataRecord>
  }
}