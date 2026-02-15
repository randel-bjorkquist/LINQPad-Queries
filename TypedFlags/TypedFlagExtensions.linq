<Query Kind="Statements" />

//-------------------------------------------------------------------------------------------------
#load "TypedEnums\TypedEnum"
#load "TypedFlags\TypedFlag"

//-------------------------------------------------------------------------------------------------
public static class TypedFlagExtensions
{
  public static TSelf With<TSelf>(this TSelf flags, TSelf flag)
    where TSelf : class
  {
    return (TSelf)((dynamic)flags | (dynamic)flag);
  }

  public static TSelf Without<TSelf>(this TSelf flags, TSelf flag)
    where TSelf : class
  {
    return (TSelf)((dynamic)flags & ~(dynamic)flag);
  }  
}
