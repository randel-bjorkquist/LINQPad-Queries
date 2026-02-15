<Query Kind="Statements" />

//-------------------------------------------------------------------------------------------------
#load "TypedEnums\TypedEnum"
#load "TypedFlags\TypedFlag"

//-------------------------------------------------------------------------------------------------
public static class TypedFlagExtensions
{
  // Fluent API for adding flags
  public static TSelf With<TSelf, TId>(this TSelf flags, TSelf flag)
    where TSelf : TypedFlag<TSelf, TId>
    where TId : notnull, IEquatable<TId>, IComparable<TId>, IBitwiseOperators<TId, TId, TId>
      => (TSelf)(flags | flag);
  
  // Fluent API for removing flags
  public static TSelf Without<TSelf, TId>(this TSelf flags, TSelf flag)
    where TSelf : TypedFlag<TSelf, TId>
    where TId : notnull, IEquatable<TId>, IComparable<TId>, IBitwiseOperators<TId, TId, TId>
      => (TSelf)(flags & ~flag);
}

// Usage:
//var perms = Permissions.None
//  .With(Permissions.Read)
//  .With(Permissions.Write)
//  .Without(Permissions.Delete);