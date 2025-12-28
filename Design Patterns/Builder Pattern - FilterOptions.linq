<Query Kind="Program" />

void Main()
{
  //NOTE: In order for the implicit operator to work on the reference type, the C# compiler must know
  //      what type to convert it to, and thus the REQUIREMENT to define the variables specific type
  //      AND NOT use the keyword 'var'.
  //FilterOptions filter = new FilterBuilder<FilterOptions>()
  FilterOptions filter = new FilterBuilder()
      .WithColumn("Age")
          .GreaterThan(18)
          .LessThan(65)                         // implicit AND
      .ThenColumn("Age")
          .Between(18, 65, inclusive: true)     // dedicated (preferred)
      .ThenColumn("Age")
          .Not()
          .Between(18, 65, inclusive: true)     // modifier on complex condition → NOT BETWEEN
      .ThenColumn("Status")
          .NotEquals("Deleted")                 // dedicated negative
          .Or()
          .Equals("Pending")
      .ThenColumn("Email")
          .DoesNotContain("spam")               // dedicated
          .Or()
          .ContainsAny("gmail", "yahoo", "hotmail")
      .ThenColumn("Role")
          .NotIn("Guest", "Banned")             // dedicated
      .ThenColumn("FirstName")
          .Not()                                // modifier style
          .StartsWith("X")
      .ThenColumn("CreatedDate")
          .Not()                                // modifier on complex
          .GreaterThan(DateTime.Today.AddDays(-30));
  
  filter.Dump("filter = UserFilterOptions.Builder()", 0);

  //NOTE: In order for the implicit operator to work on the reference type, the C# compiler must know
  //      what type to convert it to, and thus the REQUIREMENT to define the variables specific type
  //      AND NOT use the keyword 'var'.
  FilterOptions user_filter_options = new FilterBuilder()
      .WithColumn("Status")
          .OrGroup(g => g.Equals("Active")
                         .Or()
                         .Equals("Pending"))
      .ThenColumn("Role")
          .AndGroup(g => g.Equals("Admin")
                          .Equals("Moderator"));
      
  user_filter_options.Dump("user_filter_options = UserFilterOptions.Builder()", 0);
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

public class FilterOptions
{
  // Key = column name, Value = list of conditions for that column
  public Dictionary<string, List<FilterCondition>> Criteria { get; init; } = new();
}

public class FilterBuilder
{
  private readonly FilterOptions _options = new();

  internal string CurrentColumn { get; set; }
  internal List<FilterCondition> CurrentConditions { get; set; } = new();
  internal LogicalOperator NextLogical { get; set; } = LogicalOperator.AND;
  internal bool NextIsNegated { get; set; } = false;

  public ColumnFilterBuilder WithColumn(string column)
  {
    CommitCurrentColumn();

    CurrentColumn     = column;    
    CurrentConditions = new List<FilterCondition>();
    NextLogical       = LogicalOperator.AND;
    NextIsNegated     = false;

    return new ColumnFilterBuilder(this);
  }

  internal void CommitCurrentColumn()
  {
    if (CurrentColumn != null && CurrentConditions.Any())
    {
      _options.Criteria[CurrentColumn] = CurrentConditions;
    }

    CurrentColumn = null;
    CurrentConditions = new();
  }

  internal void AddCondition(FilterCondition condition)
  {
    var effective_op = NextIsNegated ? NegateOperator(condition.Op) : condition.Op;
    CurrentConditions.Add(condition with { Op = effective_op, Logical = NextLogical });
    
    // reset for next condition
    NextIsNegated = false;
    NextLogical   = LogicalOperator.AND;
  }

  public FilterOptions Build()
  {
    CommitCurrentColumn();
    return _options;
  }

  public static implicit operator FilterOptions(FilterBuilder builder)
  => builder.Build();
}

public class ColumnFilterBuilder
{
  private readonly FilterBuilder _parent;

  internal ColumnFilterBuilder(FilterBuilder parent)
  {
    _parent = parent;
  }

  private ColumnFilterBuilder AddCondition(object? value, Operator op)
  {
    _parent.AddCondition(new FilterCondition(value, op));
    return this;
  }

  // --- Positive operators ---
  public new ColumnFilterBuilder Equals(object value)         => AddCondition(value, Operator.Equals);
  
  public ColumnFilterBuilder GreaterThan(object value)        => AddCondition(value, Operator.GreaterThan);
  public ColumnFilterBuilder GreaterThanOrEqual(object value) => AddCondition(value, Operator.GreaterThanOrEqual);
  
  public ColumnFilterBuilder LessThan(object value)           => AddCondition(value, Operator.LessThan);
  public ColumnFilterBuilder LessThanOrEqual(object value)    => AddCondition(value, Operator.LessThanOrEqual);
  
  public ColumnFilterBuilder Contains(string value)           => AddCondition($"%{value}%", Operator.Like);
  public ColumnFilterBuilder StartsWith(string value)         => AddCondition($"{value}%", Operator.Like);
  public ColumnFilterBuilder EndsWith(string value)           => AddCondition($"%{value}", Operator.Like);
  
  public ColumnFilterBuilder In(params object[] values)       => AddCondition(values, Operator.In);
  public ColumnFilterBuilder IsNull()                         => AddCondition(null, Operator.IsNull);

  // --- Dedicated negative operators ---
  public ColumnFilterBuilder NotEquals(object value)        => AddCondition(value, Operator.NotEquals);
  
  public ColumnFilterBuilder DoesNotContain(string value)   => AddCondition($"%{value}%", Operator.NotLike);
  public ColumnFilterBuilder DoesNotStartWith(string value) => AddCondition($"{value}%", Operator.NotLike);
  public ColumnFilterBuilder DoesNotEndWith(string value)   => AddCondition($"%{value}", Operator.NotLike);
  
  public ColumnFilterBuilder NotIn(params object[] values)  => AddCondition(values, Operator.NotIn);
  public ColumnFilterBuilder IsNotNull()                    => AddCondition(null, Operator.IsNotNull);

  // --- Convenience: Between ---
  public ColumnFilterBuilder Between(object from, object to, bool inclusive = false)
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
  public ColumnFilterBuilder ContainsAny(params string[] values)
  {
    for(int i = 0; i < values.Length; i++)
    {
      if (i > 0) Or();
      Contains(values[i]);
    }
    
    return this;
  }

  // --- Logical connectors ---
  public ColumnFilterBuilder And() => this;

  public ColumnFilterBuilder Or()
  {
    //_parent.AddCondition(new FilterCondition(null, Operator.Equals, LogicalOperator.OR));
    _parent.NextLogical = LogicalOperator.OR;
    return this;
  }
  
  public ColumnFilterBuilder Not()
  {
    _parent.NextIsNegated = true;
    return this;
  }
  
  // --- Grouping (optional advanced feature) ---
  public ColumnFilterBuilder OrGroup(Action<ColumnFilterBuilder> group)
  {
    Or(); // start with OR
    group(this);
    
    return this;
  }

  public ColumnFilterBuilder AndGroup(Action<ColumnFilterBuilder> group)
  {
    And(); // explicit AND group
    group(this);
    
    return this;
  }

  // --- End column and continue to next column ---
  public ColumnFilterBuilder ThenColumn(string column)
  {
    // Commit the current column's conditions
    _parent.CommitCurrentColumn();
    
    // Start the new column
    _parent.CurrentColumn     = column;    
    _parent.CurrentConditions = new List<FilterCondition>();
    _parent.NextLogical       = LogicalOperator.AND;
    _parent.NextIsNegated     = false;
    
    // Return self - chain continues on the same builder instance
    return this;
  }
  
  // NEW: Return to parent so .Build() is available
  //public FilterBuilder<TFilter> End()
  //{
  //  _parent.CommitCurrentColumn();  // ensures last column is commited
  //  return _parent;
  //}
  
  // Optional alias for familiarity
  //public FilterBuilder<TFilter> Build() => End();
  
  public static implicit operator FilterOptions(ColumnFilterBuilder builder)
//  => builder.End().Build();
  => builder._parent.Build();
}
