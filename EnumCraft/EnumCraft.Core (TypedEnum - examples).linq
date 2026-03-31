<Query Kind="Program">
  <NuGetReference Prerelease="true">EnumCraft.Core</NuGetReference>
  <NuGetReference Prerelease="true">EnumCraft.FeatureFlags</NuGetReference>
  <NuGetReference Prerelease="true">EnumCraft.FeatureFlags.Json</NuGetReference>
  <NuGetReference Prerelease="true">EnumCraft.Json</NuGetReference>
  <Namespace>EnumCraft</Namespace>
</Query>

//-------------------------------------------------------------------------------------------------
#load ".\OrderStatus"

//-------------------------------------------------------------------------------------------------
void Main()
{
  #region Public Static ReadOnly Fields
  
  "Public Static ReadOnly Fields -----------------------------------------".Dump();
  
  OrderStatus.Pending.Dump("OrderStatus.Pending", 0);
  OrderStatus.Confirmed.Dump("OrderStatus.Confirmed", 0);
  OrderStatus.Shipped.Dump("OrderStatus.Shipped", 0);
  OrderStatus.Delivered.Dump("OrderStatus.Delivered", 0);
  
  #endregion
  
  #region Variables Assigned to Fields
  
  Console.WriteLine();  
  "Variables Assigned to Fields ------------------------------------------".Dump();
  
  var order_status_pending = OrderStatus.Pending;
  order_status_pending.Dump("var order_status_pending = OrderStatus.Pending;", 0);
  
  var order_status_confirmed = OrderStatus.Confirmed;
  order_status_confirmed.Dump("var order_status_confirmed = OrderStatus.Confirmed;", 0);
  
  var order_status_shipped = OrderStatus.Shipped;
  order_status_shipped.Dump("var order_status_shipped = OrderStatus.Shipped;", 0);
  
  var order_status_delivered = OrderStatus.Delivered;
  order_status_delivered.Dump("var order_status_delivered = OrderStatus.Delivered;", 0);
  
  #endregion
  
  #region GetAll() w/Dump()
  
  "".Dump();
  "GetAll() --------------------------------------------------------------".Dump();
  
  var order_status_all = OrderStatus.GetAll();
  order_status_all.Dump("var order_status_all = OrderStatus.GetAll();", 0);

  #endregion
  
  #region GetByID() w/Dump()

  "".Dump();
  "GetById() -------------------------------------------------------------".Dump();

  var order_status_count = OrderStatus.GetAll().Count();

  for(int i = 1; i <= order_status_count; i++)
  {
    var status = OrderStatus.GetByID(i);
    status.Dump($"var status = OrderStatus.GetByID({i});", 0);
  }
  
  #endregion
}

// You can define other methods, fields, classes and namespaces here
