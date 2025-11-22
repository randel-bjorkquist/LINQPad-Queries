<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
  //DI Registration: below code registers the UnitOfWork implementation in the DI container 
  //with a scoped lifetime, ensuring one instance per request for transaction consistency.
  //
  //NOTE: when using the Unit of Work pattern, you don't need to register repositories separately.
  //services.AddScoped<IUnitOfWork, UnitOfWork>();
}

/// <summary>
/// Unit of Work Interface: UoW interface provides a centralized abstraction for managing multiple
/// repositories under a single database context, allowing coordinated transactions and a unified
/// SaveChangesAsync() operation.
/// </summary>
public interface IUnitOfWork : IAsyncDisposable
{
  IProductRepository ProductRepository  { get; }
  IOrderRepository OrderRepository      { get; }
  
  Task<int> SaveChangesAsync(CancellationToken token = default);
}

/// <summary>
/// Unit of Work Implementation: UoW implementation encapsulates the shared DbContext instance and 
/// initializes all repositories that use it. Ensure all operations are committed or disposed as one
/// logical transaction.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
  private readonly MyDbContext _dbContext;  
  
  public IProductRepository ProductRepository { get; }
  public IOrderRepository OrderRepository     { get; }
  
  //NOTE: include other repositories as needed ...

  public UnitOfWork(MyDbContext dbContext)
  {
    _dbContext = dbContext;
    
    ProductRepository = new ProductRepository(_dbContext);
    OrderRepository   = new OrderRepository(_dbContext);
    
    //NOTE: initialize other repositories if needed ...
  }

  public Task<int> SaveChangesAsync(CancellationToken token = default)
  {
    return await _dbContext.SaveChangesAsync(token);
  }
  
  public ValueTask DisposeAsync()
  {
    await _dbContext.DisposeAsync();
  }
}

[HttpPost]
public async Task<IActionResult> CreateOrder([FromBody] OrderDTO dto)
{
  if(!ModelState.IsValid)
    return BadRequest.(ModelState);
    
  var product = await _unitOfWork.ProductRepository.GetByIdAsync(dto.ProductId);
  
  if(product == null)
    return NotFound("Product not found.");
    
  if(product.Stock < dto.Quantity)
    return BadRequest("Insufficient stock.");
    
  product.Stock -= dto.Quantity;
  
  var order = new Order();
  
  _unitOfWork.ProductRepository.Update(product);
  
  await _unitOfWork.OrderRepository.AddAsync(order);
  await _unitOfWork.SaveChangesAsync();

  return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
}