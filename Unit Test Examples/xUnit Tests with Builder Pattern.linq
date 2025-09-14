<Query Kind="Program">
  <NuGetReference>FluentAssertions</NuGetReference>
  <NuGetReference>xunit</NuGetReference>
  <Namespace>Xunit</Namespace>
  <Namespace>FluentAssertions</Namespace>
</Query>

#load "xunit"

//Source: Infallible Code | URL: https://www.youtube.com/@InfallibleCode
//Episode: Design Patterns for Unit Testing - Builder Pattern
//    URL: https://www.youtube.com/watch?v=zH2cz1o9Vsg

//IMPORTANT - DO NOT forget to add xUnit Test support 
// Query | Add XUnit Test Support
void Main()
{
  //NOTE: Call RunTests() or press Alt+Shift+T to initiate testing.
  //RunTests(quietly: false, reportFailuresOnly: true);  
  RunTests(quietly: false, reportFailuresOnly: false);  //default, same as calling RunTests();
}

public class InventoryTests
{
  public class BuildBag
  {
    [Fact]
    public void Given_SizeSmallAndColorBlack_Then_BuildASmallBlackBag()
    {
      Bag bag = A.Bag.WithSize(SizeFactory.Small)
                     .WithColor(Color.Black);

      bag.Should().BeOfType<Bag>();
      bag.Should().NotBeNull();
      bag.Color.Should().Be(Color.Black);
      bag.Size.Should().BeOfType<Small>();
    }
    
    [Fact]
    public void Given_SizeSmallAndColorBlack_Then_DoNotBuildALargeYellowBag()
    {
      Bag bag = A.Bag.WithColor(Color.Black)
                     .WithSize(SizeFactory.Small);
                     
      bag.Should().BeOfType<Bag>();
      bag.Should().NotBeNull();
      bag.Color.Should().NotBe(Color.Yellow);
      bag.Size.Should().NotBeOfType<Large>();
    }
  }
  
  public class AddBag
  {
    [Fact]
    public void Given_HasSpaceForOneBag_Then_AddBag()
    {
      #region COMMENTED OUT: Pre-Builder
      //
      //var bag = new Bag(1);
      //var inventory = new Inventory(1);
      //
      //inventory.AddBag(bag);
      //
      //inventory.Bags.Should().BeEquivalentTo(bag);
      //
      #endregion

      //VERSION 1:
      //var bag       = new BagBuilder().WithCapacity(1).Build();
      //var inventory = new InventoryBuilder().WithCapacity(1)
      //                                      .WithBags(bag)
      //                                      .Build();
      
      //VERSION 2:
      //var bag       = A.Bag.WithCapacity(1).Build();
      //var inventory = An.Inventory.WithCapacity(1)
      //                            .WithBags(bag)
      //                            .Build();
      
      //VERSION 3:
      //Bag bag = A.Bag.WithCapacity(1);
      Bag bag = A.Bag.WithColor(Color.Black)
                     .WithSize(SizeFactory.Small);
                     
      Inventory inventory = An.Inventory
                              .WithCapacity(1)
                              .WithBags(bag);

      inventory.AddBag(bag);
      
      //inventory.Bags.Should().BeEquivalentTo(bag);
      inventory.Bags.Should().BeEquivalentTo(new object[] { bag });
    }
    
    [Fact]
    public void Given_HasSpaceForMultipleBags_Then_AddBagOnce()
    {
      #region COMMENTED OUT: Pre-Builder
      //
      //var bag = new Bag(1);
      //var inventory = new Inventory(2);
      //
      //inventory.AddBag(bag);
      //
      //inventory.Bags.Should().BeEquivalentTo(bag, null);
      //
      #endregion
      
      Bag bag = A.Bag.WithColor(Color.Yellow)
                     .WithSize(SizeFactory.XLarge);
                     
      Inventory inventory = An.Inventory
                              .WithCapacity(2);
      
      inventory.AddBag(bag);
      
      //inventory.Bags.Should().BeEquivalentTo(bag, null);
      inventory.Bags.Should().BeEquivalentTo(new object[] { bag, null });
    }

    [Fact]
    public void Given_HasNoSpaceForBags_Then_DoesNotAddBag()
    {
      #region COMMENTED OUT: Pre-Builder
      //
      //var bag1 = new Bag(1);
      //var bag2 = new Bag(1);
      //
      //var inventory = new Inventory(1, bag1);
      //
      //inventory.AddBag(bag);
      //
      //inventory.Bags.Should().BeEquivalentTo(bag, null);
      //
      #endregion
      
      Bag bag1 = A.Bag.WithColor(Color.Blue).WithSize(SizeFactory.Medium);
      Bag bag2 = A.Bag.WithColor(Color.Red).WithSize(SizeFactory.Large);
      
      Inventory inventory = An.Inventory
                              .WithCapacity(2)
                              .WithBags(bag1, bag2);
                              
      inventory.AddBag(A.Bag);
      
      //inventory.Bags.Should().BeEquivalentTo(bag1, bag2);
      inventory.Bags.Should().BeEquivalentTo(new object[] { bag1, bag2 });
    }

    [Fact]
    public void Given_HasSpaceForMultipleBags_Then_AddsBagToFirstAvailableSlot()
    {
      #region COMMENTED OUT: Pre-Builder
      //
      //var bag1 = new Bag(1);
      //var bag2 = new Bag(1);
      //var bag3 = new Bag(1);
      //
      //var inventory = new Inventory(1, bag1, null, bag3, null);
      //
      //inventory.AddBag(bag2);
      //
      //inventory.Bags.Should().BeEquivalentTo(bag1, bag2, bag3, null);
      //
      #endregion
      
      Bag bag1 = A.Bag.WithColor(Color.Blue)
                      .WithSize(SizeFactory.Medium);
      
      Bag bag2 = A.Bag.WithColor(Color.Yellow)
                      .WithSize(SizeFactory.Small);
      
      Bag bag3 = A.Bag.WithColor(Color.Yellow)
                      .WithSize(SizeFactory.XLarge);
      
      //PRE CAPACITY FIX: 
      //  Because the method "WithCapacity" might not be called and the value requirement
      //  that "capacity" be greater than or equal to the length of the "bags" array, code 
      //  was added in the "Build" method to ensure the "capacity"
      //Inventory inventory = An.Inventory
      //                        .WithCapacity(4)
      //                        .WithBags(bag1, null, bag3, null);

      Inventory inventory = An.Inventory
                              .WithBags(bag1, null, bag3, null);

      inventory.AddBag(bag2);

      //inventory.Bags.Should().BeEquivalentTo(bag1, bag2, bag3, null);
      inventory.Bags.Should().BeEquivalentTo(new object[] { bag1, bag2, bag3, null });
    }
  }
  
  public class RemoveBag
  {
    [Fact]
    public void Given_DoesNotHaveBag_Then_DoesNothing()
    {
      Bag bag = A.Bag.WithSize(SizeFactory.Large)
                     .WithColor(Color.Black);
      
      Inventory inventory = An.Inventory
                              .WithCapacity(1);

      inventory.RemoveBag(bag);
      
      inventory.Bags.Should().BeEquivalentTo(new object[] { null });
    }
    
    [Fact]
    public void Given_HadBag_Then_RemovesBag()
    {
      Bag bag1 = A.Bag.WithSize(SizeFactory.Small)
                      .WithColor(Color.Red);
      
      Bag bag2 = A.Bag.WithColor(Color.Red)
                      .WithSize(SizeFactory.Large);
      
      Inventory inventory = An.Inventory
                              .WithBags(bag1, bag2);

      inventory.RemoveBag(bag2);
      
      inventory.Bags.Should().BeEquivalentTo(new object[] { bag1, null });
    }
  }
}

#region Builder Class(s)

public static class A
{
  public static BagBuilder Bag => new BagBuilder();
}

public static class An
{
  public static InventoryBuilder Inventory => new InventoryBuilder();
}

public class BagBuilder
{
  #region COMMENTED OUT: ORIGINAL CODE
  //
  //private int _capacity;
  //
  //public Bag Build()
  //{
  //  return new Bag(_capacity);
  //}
  //
  //public BagBuilder WithCapacity(int capacity)
  //{
  //  _capacity = capacity;
  //  return this;
  //}
  //
  #endregion
  
  private Color _color = Color.Unknown;
  private Size  _size;
  
  public Bag Build()
  {
    return new Bag(_color, _size);
  }
  
  public BagBuilder WithColor(Color color)
  {
    _color = color;
    return this;
  }
  
  public BagBuilder WithSize(Size size)
  {
    _size = size;
    return this;
  }
  
  public static implicit operator Bag(BagBuilder builder)
  {
    return builder.Build();
  }
}

public class InventoryBuilder
{
  private int _capacity;
  private Bag[] _bags;
  
  public Inventory Build()
  {
    _capacity = _capacity > (_bags?.Length ?? 0) 
              ? _capacity 
              : _bags.Length;
    
    return new Inventory( _capacity, _bags ?? new Bag[_capacity]);
  }
  
  public InventoryBuilder WithCapacity(int capacity)
  {
    _capacity = capacity;
    return this;
  }
  
  public InventoryBuilder WithBags(params Bag[] bags)
  {
    _bags = bags;
    return this;
  }
  
  public static implicit operator Inventory(InventoryBuilder builder)
  {
    return builder.Build();
  }
}

#endregion

#region Factory(s)

public static class SizeFactory
{
  public static Size Small  => new Small();
  public static Size Medium => new Medium();
  public static Size Large  => new Large();
  public static Size XLarge => new ExtraLarge();
}

#endregion

#region Non-Builder Class(s)

public enum Color
{
   Unknown  = -1
  ,Black    =  0
  ,Blue     =  1
  ,Orange   =  2
  ,Red      =  4
  ,Yellow   =  8
}

public abstract class Size
{
  public abstract double Height { get; }
  public abstract double Width  { get; }
  public abstract double Length { get; }
}

public class Small : Size
{
  public override double Height => 18.0;
  public override double Width  =>  8.0;
  public override double Length => 12.0;
}

public class Medium : Size
{
  public override double Height => 22.0;
  public override double Width  =>  9.0;
  public override double Length => 14.0;
}

public class Large : Size
{
  public override double Height => 29.0;
  public override double Width  => 20.0;
  public override double Length => 25.0;
}

public class ExtraLarge : Size
{
  public override double Height => 36.0;
  public override double Width  => 22.0;
  public override double Length => 30.0;
}

public class Bag
{
//  private int _capacity;
//  
//  public Bag(int capacity)
//  {
//    _capacity = capacity;
//  }

  private Color _color = Color.Unknown;
  private Size  _size;
  
  public Bag(Color color) { _color = color; }  
  public Bag(Size size)   { _size  = size;  }
  
  public Bag(Color color, Size size) { _color = color; _size  = size;  }
  
  public Color Color => _color;
  public Size Size => _size;
}

public class Inventory
{
  private int _capacity;
  private Bag[] _bags = null;

  public Inventory(int capacity)
  {
    _capacity = capacity;
  }

  public Inventory(Bag[] bags)
  {
    _capacity = bags.Length;
    _bags = bags;
  }

  public Inventory(int capacity, params Bag[] bags)
  {
    if(bags == null)
    {
      throw new ArgumentNullException("bags"); 
    }    
    
    if(capacity < bags.Length - 1)
    {
      throw new ArgumentOutOfRangeException( "capacity"
                                            ,capacity
                                            ,$"The 'capacity' must be greather than or equal to {bags.Length}"); 
    }    
    
    _capacity = capacity;
    _bags = bags;
  }

  public IEnumerable<Bag> Bags => _bags;
  
  public void AddBag(Bag bag)
  {
    if(bag == null) { throw new ArgumentNullException("bag"); }
    
    for(int index = 0; index < _capacity; index++)
    {
      if(_bags[index] != null) continue;
      
      if(_bags[index] == null)
      {
         _bags[index] = bag;
         break;
      }
    }
  }
  
  public void RemoveBag(Bag bag)
  {
    for(int index = 0; index < _capacity; index++)
    {
      if(_bags[index] == bag)
      {
        _bags[index] = null;
      }
    }
  }
}

#endregion