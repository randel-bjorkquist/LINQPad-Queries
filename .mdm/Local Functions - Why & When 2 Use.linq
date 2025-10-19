<Query Kind="Program" />

//Source: C# Local Functions - Why And When To Use Them
//URL: https://www.c-sharpcorner.com/article/c-sharp-local-function-vs-delegates/

#region Scenario 1 - Using Lambda Expression
/* *

void Main()
{
  //#1. Create a list Ordered items details  
  List<OrderDetail> lstOrderDetails = DataFactory.CreateOrders();

  //#2. A Lambda to calculate Profit Percentage 
  Func<double, double, double> GetPercentageProfit 
    = (purchasePrice, sellPrice) => (((sellPrice - purchasePrice) / purchasePrice) * 100);

  //#3. Loop through each item and print the profit details by calling our delegate  
  foreach(var order in lstOrderDetails)
  {
    $"Item Name: {order.ItemName}, Profit(%) : {GetPercentageProfit(order.PurchasePrice, order.SellingPrice)}".Dump();
  }
}

/* */
#endregion

#region Scenario 1 - Using Local Function
/* *

void Main()
{
  //#1. Order details  
  List<OrderDetail> lstOrderDetails = DataFactory.CreateOrders();
  
  //#2. Loop through each item and get the profit details  
  foreach (var order in lstOrderDetails)  
  {  
    Console.WriteLine($"Item Name: {order.ItemName}, Profit(%) : {GetPercentageProfit(order.PurchasePrice, order.SellingPrice)} ");  
  }  

  //#3. Local function  - Inside the function "Main"  
  double GetPercentageProfit(double purchasePrice, double sellPrice)  
  {  
    return (sellPrice - purchasePrice) / purchasePrice * 100;
  }  
}

/* */
#endregion

#region Scenario 2 - Using "yeild" in "foreach" loop
//NOTE: Now consider a scenario where your order details list is null. So, when we call the 
//      method "GetItemSellingPice" it should return the ArgumentNullException as List is null.
//      But this is not the case, when you use iterators, It does not execute right away. It 
//      starts executing when you start processing its"result". So in the code below when we 
//      make the lstOrderDetails null, the code will not throw exception at #2. It will thrown 
//      on #4, where we start working on the resulting property "result". 
/* *

static void Main(string[] args)
{
  //#1. Order details  
  List<OrderDetail> lstOrderDetails = null;

  //#2. Get Item Selling Price  
  var result = GetItemSellingPice(lstOrderDetails);

  //#4. Print Item name with its selling price  
  foreach (string s in result)
  {
    Console.WriteLine(s.ToString());
  }
}

/// <summary>  
/// Method to return the item name with price  
/// </summary>  
/// <param name="lstOrderDetails"></param>  
/// <returns></returns>  
public static IEnumerable<string> GetItemSellingPice(List<OrderDetail> lstOrderDetails)
{
  //#3. If List is empty then throw Argument null exception  
  if (lstOrderDetails == null) throw new ArgumentNullException();

  foreach (var order in lstOrderDetails)
  {
    yield return ($"Item Name:{order.ItemName}, Selling Price:{order.SellingPrice}");
  }
}

/* */
#endregion

#region Scenario 2 - Using "yeild" in "foreach" loop, with Local Function
//NOTE: Now consider a scenario where your order details list is null. So, when we call the 
//      method "GetItemSellingPice" it should return the ArgumentNullException as List is null.
//      But this is not the case, when you use iterators, It does not execute right away. It 
//      starts executing when you start processing its"result". So in the code below when we 
//      make the lstOrderDetails null, the code will not throw exception at #2. It will thrown 
//      on #4, where we start working on the resulting property "result". 
//
//      You can see that we moved the foreach loop block into Local Function. So, when we execute 
//      it, we will get the exception at #3, if the list is null. This is the eager validation for
//      iterators.
/* */

static void Main(string[] args)
{
  //#1. Order details  
  List<OrderDetail> lstOrderDetails = null;

  //#2. Get Item Selling Price  
  var result = GetItemSellingPice(lstOrderDetails);

  //#4. Print Item name with its selling price  
  foreach (string s in result)
  {
    Console.WriteLine(s.ToString());
  }
}

/// <summary>  
/// Method to return the item name with price  
/// </summary>  
/// <param name="lstOrderDetails"></param>  
/// <returns></returns>  
public static IEnumerable<string> GetItemSellingPice(List<OrderDetail> lstOrderDetails)
{
  //#3. If List is empty then throw Argument null exception  
  if (lstOrderDetails == null) throw new ArgumentNullException();
  
  //Calling Local Function
  return GetItemPrice();
  
  //#4. Local Function
  IEnumerable<string> GetItemPrice()
  {
    foreach (var order in lstOrderDetails)
    {
      yield return ($"Item Name:{order.ItemName}, Selling Price:{order.SellingPrice}");
    }
  }
}

/* */
#endregion

public static class DataFactory
{
  public static List<OrderDetail> CreateOrders() 
    => new List<OrderDetail> 
           {
             new OrderDetailDataBuilder().WithID(1).WithItemName("Item 1").WithPruchasePrice(100).WithSellingPrice( 120).Build()
            ,new OrderDetailDataBuilder().WithID(2).WithItemName("Item 2").WithPruchasePrice(800).WithSellingPrice(1200).Build()
            ,new OrderDetailDataBuilder().WithID(3).WithItemName("Item 3").WithPruchasePrice(150).WithSellingPrice( 150).Build()
            ,new OrderDetailDataBuilder().WithID(4).WithItemName("Item 4").WithPruchasePrice(155).WithSellingPrice( 310).Build()
            ,new OrderDetailDataBuilder().WithID(5).WithItemName("Item 5").WithPruchasePrice(500).WithSellingPrice( 550).Build()
           };
}

public class OrderDetailDataBuilder
{
  private int _id;
  private string _item_name;
  private double _purchase_price;
  private double _selling_price;
  
  public OrderDetail Build()
  {
    return new OrderDetail { ID             = _id
                            ,ItemName       = _item_name
                            ,PurchasePrice  = _purchase_price
                            ,SellingPrice   = _selling_price };
  }
  
  public OrderDetailDataBuilder WithID(int id)
  {
    _id = id;
    return this;
  }
  
  public OrderDetailDataBuilder WithItemName(string item_name)
  {
    _item_name = item_name;
    return this;
  }
  
  public OrderDetailDataBuilder WithPruchasePrice(double purchase_price)
  {
    _purchase_price = purchase_price;
    return this;
  }

  public OrderDetailDataBuilder WithSellingPrice(double selling_price)
  {
    _selling_price = selling_price;
    return this;
  }
}

public class OrderDetail
{
  public OrderDetail() { }

  public int ID               { get; set; }
  public string ItemName      { get; set; }

  public double PurchasePrice { get; set; }
  public double SellingPrice  { get; set; } 
}
