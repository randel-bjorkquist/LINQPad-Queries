<Query Kind="Program" />

void Main()
{
  var filter = new FilterBuilder<UserFilterOptions>()
      .WithColumn("Age")
          .GreaterThan(18)
          .LessThan(65)                         // implicit AND
//          .AndContinue()
          
      .WithColumn("Age")
          .Between(18, 65, inclusive: true)     // dedicated (preferred)
//          .AndContinue()
          
      .WithColumn("Age")
          .Not()
          .Between(18, 65, inclusive: true)     // modifier on complex condition → NOT BETWEEN
//          .AndContinue()
          
      .WithColumn("Status")
          .NotEquals("Deleted")                 // dedicated negative
          .Or()
          .Equals("Pending")
//          .AndContinue()
          
      .WithColumn("Email")
          .DoesNotContain("spam")               // dedicated
          .Or()
          .ContainsAny("gmail", "yahoo", "hotmail")
//          .AndContinue()
          
      .WithColumn("Role")
          .NotIn("Guest", "Banned")             // dedicated
//          .AndContinue()
          
      .WithColumn("FirstName")
          .Not()                                // modifier style
          .StartsWith("X")
//          .AndContinue()
          
      .WithColumn("CreatedDate")
          .Not()                                // modifier on complex
          .GreaterThan(DateTime.Today.AddDays(-30))

      .Build();


  var user_filter_options = UserFilterOptions.Builder()
      .WithColumn("Status")
          .OrGroup(g => g.Equals("Active")
                         .Or()
                         .Equals("Pending"))
//          .AndContinue()
      .WithColumn("Role")
          .AndGroup(g => g.Equals("Admin")
                          .Equals("Moderator"))
      .Build();
}


public abstract class FilterOptions<T> where T : class
{
  // Base: No-op; features add props like public bool IncludeFoo { get; set; }
  // Default constructor ensures empty options = no fills
}

public abstract record FilterOptions
{
  // Key = column name, Value = list of conditions for that column
  public Dictionary<string, List<FilterCondition>> Criteria { get; init; } = new();
}

public record UserFilterOptions : FilterOptions
{
  // Strongly-typed helper (optional but nice)
  public static FilterBuilder<UserFilterOptions> Builder() => new();

  public bool IncludeDeletedUsers { get; set; } = false;
}

public record ContactFilterOptions : FilterOptions
{
  // Strongly-typed helper (optional but nice)
  public static FilterBuilder<ContactFilterOptions> Builder() => new();
}

public enum Operator
{
   Equals
  ,NotEquals
  
  ,GreaterThan
  ,GreaterThanOrEqual
  
  ,LessThan
  ,LessThanOrEqual
  
  ,Like
  ,NotLike
  
  ,In
  ,NotIn
  
  ,IsNull
  ,IsNotNull
}

public enum LogicalOperator
{
   AND
  ,OR
}

private static Operator NegateOperator(Operator op) 
  => op switch { Operator.Equals             => Operator.NotEquals
                ,Operator.GreaterThan        => Operator.LessThanOrEqual     // NOT > x → <= x
                ,Operator.GreaterThanOrEqual => Operator.LessThan
                ,Operator.LessThan           => Operator.GreaterThanOrEqual
                ,Operator.LessThanOrEqual    => Operator.GreaterThan
                ,Operator.Like               => Operator.NotLike
                ,Operator.In                 => Operator.NotIn
                ,Operator.IsNull             => Operator.IsNotNull
                ,Operator.IsNotNull          => Operator.IsNull
                ,                          _ => throw new ArgumentException($"Cannot negate operator {op}") };

public record FilterCondition( object Value
                              ,Operator Op
                              ,LogicalOperator Logical = LogicalOperator.AND );

public class FilterBuilder<TFilter> where TFilter : FilterOptions, new()
{
  private readonly TFilter _options = new();
  private string _currentColumn;
  
  private List<FilterCondition> _currentConditions = new();
  private LogicalOperator _nextLogical = LogicalOperator.AND;

  public ColumnFilterBuilder<TFilter> WithColumn(string column)
  {
      CommitCurrentColumn();
      
      _currentColumn     = column;
      _currentConditions = new List<FilterCondition>();
      _nextLogical       = LogicalOperator.AND;
      
      return new ColumnFilterBuilder<TFilter>(this);
  }
  
  public ColumnFilterBuilder<TFilter> ThenColumn(string column) => WithColumn(column);
//  public ColumnFilterBuilder<TFilter> ThenColumn(string column) => NextColumn(column);
//  public ColumnFilterBuilder<TFilter> NextColumn(string column)
//  {
//    CommitCurrentColumn();
//    return new ColumnFilterBuilder<TFilter>(this);
//  }

  internal void AddCondition(FilterCondition condition)
  {
    _currentConditions.Add(condition);
  }

  internal void CommitCurrentColumn()
  {
    if(_currentColumn != null && _currentConditions.Any())
    {
      _options.Criteria[_currentColumn] = _currentConditions;
    }
    
    _currentColumn     = null;
    _currentConditions = new();
  }

  public TFilter Build()
  {
    CommitCurrentColumn();
    return _options;
  }
}

public class ColumnFilterBuilder<TFilter> where TFilter : FilterOptions, new()
{
  private readonly FilterBuilder<TFilter> _parent;
  private bool _nextIsNegated = false;

  internal ColumnFilterBuilder(FilterBuilder<TFilter> parent)
  {
    _parent = parent;
  }

  private ColumnFilterBuilder<TFilter> AddCondition(object? value, Operator op)
  {
    var effective_operator = _nextIsNegated ? NegateOperator(op) : op;
    
    _parent.AddCondition(new FilterCondition(value, effective_operator));
    _nextIsNegated = false;
    
    return this;
  }

  // --- Positive operators ---
  public ColumnFilterBuilder<TFilter> Equals(object value)             => AddCondition(value, Operator.Equals);
  
  public ColumnFilterBuilder<TFilter> GreaterThan(object value)        => AddCondition(value, Operator.GreaterThan);
  public ColumnFilterBuilder<TFilter> GreaterThanOrEqual(object value) => AddCondition(value, Operator.GreaterThanOrEqual);
  
  public ColumnFilterBuilder<TFilter> LessThan(object value)           => AddCondition(value, Operator.LessThan);
  public ColumnFilterBuilder<TFilter> LessThanOrEqual(object value)    => AddCondition(value, Operator.LessThanOrEqual);
  
  public ColumnFilterBuilder<TFilter> Contains(string value)           => AddCondition($"%{value}%", Operator.Like);
  public ColumnFilterBuilder<TFilter> StartsWith(string value)         => AddCondition($"{value}%", Operator.Like);
  public ColumnFilterBuilder<TFilter> EndsWith(string value)           => AddCondition($"%{value}", Operator.Like);
  
  public ColumnFilterBuilder<TFilter> In(params object[] values)       => AddCondition(values, Operator.In);
  public ColumnFilterBuilder<TFilter> IsNull()                         => AddCondition(null, Operator.IsNull);

  // --- Dedicated negative operators ---
  public ColumnFilterBuilder<TFilter> NotEquals(object value)          => AddCondition(value, Operator.NotEquals);
  
  public ColumnFilterBuilder<TFilter> DoesNotContain(string value)     => AddCondition($"%{value}%", Operator.NotLike);
  public ColumnFilterBuilder<TFilter> DoesNotStartWith(string value)   => AddCondition($"{value}%", Operator.NotLike);
  public ColumnFilterBuilder<TFilter> DoesNotEndWith(string value)     => AddCondition($"%{value}", Operator.NotLike);
  
  public ColumnFilterBuilder<TFilter> NotIn(params object[] values)    => AddCondition(values, Operator.NotIn);
  public ColumnFilterBuilder<TFilter> IsNotNull()                      => AddCondition(null, Operator.IsNotNull);

  // --- Convenience: Between ---
  public ColumnFilterBuilder<TFilter> Between(object from, object to, bool inclusive = false)
  {
    if(inclusive)
    {
      GreaterThanOrEqual(from);
      LessThanOrEqual(to);
    }
    else
    {
      GreaterThan(from);
      LessThan(to);
    }
    
    return this;
  }

  // --- Multi-value ContainsAny (OR on LIKE) ---
  public ColumnFilterBuilder<TFilter> ContainsAny(params string[] values)
  {
    for(int i = 0; i < values.Length; i++)
    {
      if (i > 0) Or();
      Contains(values[i]);
    }
    
    return this;
  }

  // --- Logical connectors ---
  public ColumnFilterBuilder<TFilter> And() => this;

  public ColumnFilterBuilder<TFilter> Or()
  {
    _parent.AddCondition(new FilterCondition(null, Operator.Equals, LogicalOperator.OR));
    return this;
  }
  
  public ColumnFilterBuilder<TFilter> Not()
  {
    _nextIsNegated = true;
    return this;
  }
  
  // --- Grouping (optional advanced feature) ---
  public ColumnFilterBuilder<TFilter> OrGroup(Action<ColumnFilterBuilder<TFilter>> group)
  {
    Or(); // start with OR
    group(this);
    
    return this;
  }

  public ColumnFilterBuilder<TFilter> AndGroup(Action<ColumnFilterBuilder<TFilter>> group)
  {
    And(); // explicit AND group
    group(this);
    
    return this;
  }

// --- End column and continue to next column ---
//public FilterBuilder<TFilter> AndContinue() => _parent;
//public FilterBuilder<TFilter> OrContinue()  => _parent;
//
//public FilterBuilder<TFilter> NextColumn()  => _parent;
//public FilterBuilder<TFilter> ThenColumn()  => _parent;
}