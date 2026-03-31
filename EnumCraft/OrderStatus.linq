<Query Kind="Statements">
  <Namespace>EnumCraft</Namespace>
</Query>

//-------------------------------------------------------------------------------------------------
public class OrderStatus : TypedEnumInt<OrderStatus>
{
  public static readonly OrderStatus Pending    = new(1, "Pending"   ,nameof(Pending));
  public static readonly OrderStatus Confirmed  = new(2, "Confirmed" ,nameof(Confirmed));
  public static readonly OrderStatus Shipped    = new(3, "Shipped"   ,nameof(Shipped));
  public static readonly OrderStatus Delivered  = new(4, "Delivered" ,nameof(Delivered));

  private OrderStatus(int id, string description, string code)
      : base(id, description, code) { }
}