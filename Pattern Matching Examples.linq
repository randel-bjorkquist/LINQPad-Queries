<Query Kind="Program" />

void Main()
{
/// <summary>
/// Pattern Matching
/// Is a functional programming feature that already exists in other popular languages,
/// such as: F#, Scala, Rust, Python, Haskell, Prolog and many others ...
/// 
/// - It's used to provide a way to test expression for some conditiona while testing 
///   the types
/// - Introduced in C# 7
///   * Type-Testing
///   * Nullable-type checking
///   * Type casting & assignment
///   * High readability
///   * Concise syntax
///   * Less convoluted code
/// - Usages:
///   * 'is' expressions
///   * 'switch' expressions
/// 
/// Pattern Matching Types (patterns):
///   C#  7: Type, Declaration, Constant, Null, and VAR
///   C#  8: Property, Discard, Positional, Tuple
///   C#  9: Enhanced Type, Relative, Logical (Combinators), Negated Null Constant, Parenthesized
///   C# 10: Extended Property
///   C# 11: List
/// </summary>
}

#region Type Pattern:

//----------------------------------------------------------------------------------------------------------
//INFO: type-testing for the expression
//----------------------------------------------------------------------------------------------------------
public bool IsProductFood(object product) 
  => product is FoodModel;

#endregion

#region Declaration Pattern:

//----------------------------------------------------------------------------------------------------------
//INFO: type-testing as well as assignment to variable after a successful match of the expression
//----------------------------------------------------------------------------------------------------------
public bool IsProductFoodThatRequiresRefrigeration(object product)
  => product is FoodModel food
  && ProductFoodThatRequiresRefrigeration(food.StorageTemperature);

#endregion

#region Constant Pattern:

//----------------------------------------------------------------------------------------------------------
//INFO: Testing versus a constant value which can include int, float, char, string, bool, enum
//      field declared with const, and null
//----------------------------------------------------------------------------------------------------------
public bool IsFreshProduce(FoodModel food)
  => food?.Category?.ID is (int)ProductCategoryEnum.FreshProduce;

#endregion

#region Null Pattern:

//----------------------------------------------------------------------------------------------------------
//INFO: Check if a reference or nullable type is null
//----------------------------------------------------------------------------------------------------------
public bool FoodDoesNotExists(FoodModel food)
  => food is null;

#endregion

#region VAR Pattern:

//----------------------------------------------------------------------------------------------------------
//INFO: Similar to the Type Pattern (above), the VAR Pattern matches an expression and checks for null, as 
//      well as assigns a value to the variable. The var type is declared based on the matched expression's
//      compile-time type.
//----------------------------------------------------------------------------------------------------------
public bool IsProductFoodThatRequiresRefrigeration(FoodModel food)
  => GetFoodStorageRequirements(food) is var storageRequirement 
  && storageRequirement is StorageRequirementEnum.Freezer;

#endregion

#region Property Pattern:

//----------------------------------------------------------------------------------------------------------
//INFO: Pattern matching using object members rather than variables to match the given conditions.
//----------------------------------------------------------------------------------------------------------
public bool IsOrganicFood(FoodModel food)
  => food is { NonGMO:                true
              ,NoChemicalFertilizers: true
              ,NoSyntheticPesticides: true };

#endregion

#region Discard Pattern:

//----------------------------------------------------------------------------------------------------------
//INFO: Using the discard operation _ to match anything including null. In the below example, is 
//      you pass a food object with storageTempurature as 40, you get an exception.
//----------------------------------------------------------------------------------------------------------
public int GetFoodStorageTemperature(StorageRequirementEnum storage_requirement)
  => storage_requirement switch {
     StorageRequirementEnum.Freezer         => -8
    ,StorageRequirementEnum.Refrigerator    =>  4
    ,StorageRequirementEnum.RoomTemperature => 25
    ,_ => throw new InvalidStorageRequirementException()
  };

#endregion

#region Positional Pattern:

//----------------------------------------------------------------------------------------------------------
//INFO: Mainly used with struct types, leverages a type's deconstructor to match a pattern
//      according to the values position in the deconstructor
//----------------------------------------------------------------------------------------------------------
public bool IsFreeFood(FoodModel food)
//  => food.Price is (0, _);
  => food is (0, _);

#endregion

#region Tuple Pattern:

//----------------------------------------------------------------------------------------------------------
//INFO: A special derivation from the positional pattern where you can test multiple properties 
//      of a type in the same expression, using tuples
//----------------------------------------------------------------------------------------------------------
public string GetFoodDescription(FoodModel food)
  => (food.NonGMO, food.Category!.ID) 
        switch { (true  ,(int)ProductCategoryEnum.FreshProduce) => "Non_GMO Fresh Product"
                ,(true  ,(int)ProductCategoryEnum.Dairy)        => "Non-GMO Dairy"
                ,(false ,(int)ProductCategoryEnum.Meats)        => "GMO Meats. Avoid!"
                ,(_ , _)                                        => "Invalid Food Group" };

#endregion

#region 'Enchanced' Type Pattern:

//----------------------------------------------------------------------------------------------------------
//INFO: You can do type checking in switch expressions without using the discards with each type
//----------------------------------------------------------------------------------------------------------
public string CheckValueType(object value)
  => value switch { int     => "This is an integer number"
                   ,decimal => "This is a decimal number"
                   ,double  => "This is a double number"
                   ,      _ => throw new InvalidNumberException(value)};

#endregion

#region Relational Pattern:

//----------------------------------------------------------------------------------------------------------
//INFO: A relational pattern allows applying the relational operators: > < >= <= to match patterns
//      versus constants or enum values
//----------------------------------------------------------------------------------------------------------
public StorageRequirementEnum GetFoodStorageRequirements(FoodModel food)
  => food.StorageTemperature 
        switch { <= -18          => StorageRequirementEnum.Freezer
                ,>=   2 and <  6 => StorageRequirementEnum.Refrigerator
                ,>    6 and < 30 => StorageRequirementEnum.RoomTemperature
                ,              _ => throw new InvalidStorageRequirementException(food.StorageTemperature)};

#endregion

#region Logical Pattern:

//----------------------------------------------------------------------------------------------------------
//INFO: This represents the set of negation, conjunctive, disjunctive, not, and, or respectively, 
//      used to combine pattern and apply logical conditions on them. Together these are called 
//      the pattern combinators
//----------------------------------------------------------------------------------------------------------
public bool RequiresRefrigeration(ProductCategoryEnum type)
  => type is ProductCategoryEnum.Dairy or ProductCategoryEnum.Meats;

#endregion

#region Negated NULL Constant Pattern:

//----------------------------------------------------------------------------------------------------------
//INFO: Checks expression for not null value
//----------------------------------------------------------------------------------------------------------
public bool BlogExists(BlogModel blog)
  => blog is not null;

#endregion

#region Parenthesized Pattern:

//----------------------------------------------------------------------------------------------------------
//INFO: This allows the use of parenthesis to control the order of the execution and to group logical 
//      expressions together. Mainly used with pattern combinators.
//----------------------------------------------------------------------------------------------------------
public bool RequiresRefrigeration(int temperature)
  => temperature is > 1 and (< 6);

#endregion

#region Extended Property Pattern:

//----------------------------------------------------------------------------------------------------------
//INFO: In C# 10, the nested properties matching syntax issue was addressed with the introduction of the
//      Extended Property Pattern, syntax to use nested properties in pattern matching is now clean and 
//      concise.
//----------------------------------------------------------------------------------------------------------
public bool RequiresRefrigeration(FoodModel food)
=> food is { Category.ID : (int)ProductCategoryEnum.Dairy or
                           (int)ProductCategoryEnum.Meats };


#endregion

#region List Pattern:

//----------------------------------------------------------------------------------------------------------
//INFO: The list matching pattern is the latest addition to the great set of pattern matching in C# 11. With
//      List Pattern, you can match a list or an array with a set of sequential elements
//----------------------------------------------------------------------------------------------------------
public (int?, int?) FindNumber1AndNumber4()
{
  int[] numbers = { 1, 2, 3, 4, 5 };
  
  //Match if 2nd value is anything, 3rd is greater than or equal to 3, fifth is 5
  if(numbers is [var number1, _, >= 3, int number4, 5])
    return (number1, number4);
  
  return (null, null);
}

#endregion


//----------------------------------------------------------------------------------------------------------
#region Supporting Objects: classes, enums, static methods ...

public static bool ProductFoodThatRequiresRefrigeration(decimal storageTemperature)
  => storageTemperature <= 65;


public enum ProductCategoryEnum
{  
   Dairy
  ,FreshProduce
  ,Meats
}

public enum StorageRequirementEnum
{
   Freezer          = -8
  ,Refrigerator     = 4
  ,RoomTemperature  = 25

}


public class FoodModel
{
  public FoodModel(int price, string name)
  {
    Price = price;
    Name = name ?? throw new ArgumentNullException(nameof(name));
  }
  
  public void Deconstruct(out decimal price, out string name)
  {
    price = Price;
    name  = Name;
  }
  
  public string Name                  { get; set; }
  public decimal Price                { get; set; }
  
  public decimal StorageTemperature   { get; set; }
  public ProductCategory? Category    { get; set; }
  
  public bool NonGMO                  { get; set; }
  public bool NoChemicalFertilizers   { get; set; }
  public bool NoSyntheticPesticides   { get; set; }
}

public class BlogModel { }

public class ProductCategory
{
  public int ID { get; set; }
}

public class InvalidStorageRequirementException : Exception
{
  public InvalidStorageRequirementException() { }

  public InvalidStorageRequirementException(decimal storageTemperature)
    : base($"INVALID NUMBER: '{storageTemperature}' is an out of range value.") { }
}

public class InvalidNumberException : Exception
{
  public InvalidNumberException(object value)
    : base($"INVALID NUMBER: '{nameof(value)}' is of an unsupported data type.") { }
}

#endregion