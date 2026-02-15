<Query Kind="Statements" />

//-------------------------------------------------------------------------------------------------
#load "TypedEnums\TypedEnum"
#load "TypedFlags\TypedFlag"

//-------------------------------------------------------------------------------------------------
/// <summary>
/// Example: File system permissions using TypedFlags
/// </summary>
public class Permissions : TypedFlagInt<Permissions>
{
  public static readonly Permissions None     = new( 0, "None"    ,nameof(None));
  public static readonly Permissions Read     = new( 1, "Read"    ,nameof(Read));
  public static readonly Permissions Write    = new( 2, "Write"   ,nameof(Write));
  public static readonly Permissions Execute  = new( 4, "Execute" ,nameof(Execute));
  public static readonly Permissions Delete   = new( 8, "Delete"  ,nameof(Delete));
  public static readonly Permissions All      = new(15, "All"     ,nameof(All));    // 1|2|4|8

  private Permissions(int id, string description, string code)
    : base(id, description, code) { }
}
