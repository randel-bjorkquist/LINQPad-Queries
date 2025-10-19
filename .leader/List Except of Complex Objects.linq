<Query Kind="Program" />

void Main()
{
  ProductA[] fruits1 = { new ProductA { Name = "apple"  ,Code =  9 }
                        ,new ProductA { Name = "orange" ,Code =  4 }
                        ,new ProductA { Name = "lemon"  ,Code = 12 }};

  ProductA[] fruits2 = { new ProductA { Name = "apple" ,Code = 9 }};

  //NOTE: Get all the elements from the first array except for the 
  //      elements from the second array.
  IEnumerable<ProductA> except = fruits1.Except(fruits2);

  /* This code produces the following output:
   * orange 4
   * lemon 12
   */
  foreach(var product in except)
  {
    $"{product.Name} {product.Code}".Dump();
  }    
}

public class ProductA : IEquatable<ProductA>
{
  public string Name  { get; set; }
  public int Code     { get; set; }

  public bool Equals(ProductA other)
  {
    if (other is null)
      return false;

    return this.Name == other.Name && this.Code == other.Code;
  }

  public override bool Equals(object obj) => Equals(obj as ProductA);
  public override int GetHashCode() => (Name, Code).GetHashCode();
}