<Query Kind="Program" />

//----------------------------------------------------------------------------------------------------------------
#load "TypedEnums\TypedEnum"
#load "TypedFlags\TypedFlag"
#load "TypedFlags\Permissions"

// Demo Program --------------------------------------------------------------------------------------------------
void Main()
{
  "=== TypedFlags Demo ===".Dump();
  
  #region 1. Basic combination -----------------------------------------------------------------------------------
  
  var readWrite = Permissions.Read | Permissions.Write;
  
  new { Expression  = "Permissions.Read | Permissions.Write"
       ,Result      = readWrite
       ,ID          = readWrite.ID
       ,Description = readWrite.Description
       ,Code        = readWrite.Code }
    .Dump("1. Basic Combination", 0);
  
  #endregion
  
  #region 2. HasFlag checks --------------------------------------------------------------------------------------
  
  new { ReadWrite   = readWrite
       ,HasRead     = readWrite.HasFlag(Permissions.Read)
       ,HasExecute  = readWrite.HasFlag(Permissions.Execute) }
    .Dump("2. HasFlag Checks", 0);
  
  #endregion
  
  #region 3. HasAnyFlag / HasAllFlags ----------------------------------------------------------------------------
  
  var fullAccess = Permissions.Read | Permissions.Write | Permissions.Execute;
  
  new { FullAccess                = fullAccess
       ,HasAny_Read_Delete        = fullAccess.HasAnyFlag(Permissions.Read, Permissions.Delete)
       ,HasAll_Read_Write_Execute = fullAccess.HasAllFlags(Permissions.Read, Permissions.Write, Permissions.Execute)
       ,HasAll_Read_Write_Delete  = fullAccess.HasAllFlags(Permissions.Read, Permissions.Write, Permissions.Delete) }
    .Dump("3. Multiple Flag Checks", 0);
  
  #endregion
  
  #region 4. ToFlags decomposition -------------------------------------------------------------------------------
  
  var combined = Permissions.Read | Permissions.Write | Permissions.Execute;
  
  new { Combined        = combined
       ,IndividualFlags = combined.ToFlags()
       ,FlagCodes       = string.Join(", ", combined.ToFlags().Select(f => f.Code)) }
    .Dump("4. Decomposition (ToFlags)", 0);
  
  #endregion
  
  #region 5. Bitwise AND -----------------------------------------------------------------------------------------
  
  var intersection = (Permissions.Read | Permissions.Write) & Permissions.Read;
  
  new { Expression = "(Read | Write) & Read"
       ,Result     = intersection }
    .Dump("5. Bitwise AND", 0);
  
  #endregion
  
  #region 6. Bitwise XOR -----------------------------------------------------------------------------------------
  
  var difference = (Permissions.Read | Permissions.Write) ^ Permissions.Read;
  
  new { Expression = "(Read | Write) ^ Read"
       ,Result     = difference }
    .Dump("6. Bitwise XOR", 0);
  
  #endregion
  
  #region 7. Bitwise NOT with All --------------------------------------------------------------------------------
  
  var notRead = Permissions.All & ~Permissions.Read;
  
  new { Expression = "All & ~Read"
       ,Result     = notRead
       ,Flags      = notRead.ToFlags()
       ,FlagCodes  = string.Join(", ", notRead.ToFlags().Select(f => f.Code)) }
    .Dump("7. Bitwise NOT (with All)", 0);
  
  #endregion
  
  #region 8. Reference equality (caching) ------------------------------------------------------------------------
  
  var combo1 = Permissions.Read | Permissions.Write;
  var combo2 = Permissions.Read | Permissions.Write;
  
  new { Combo1            = combo1
       ,Combo2            = combo2
       ,AreReferenceEqual = ReferenceEquals(combo1, combo2)
       ,AreEqual          = combo1 == combo2
  }.Dump("8. Reference Equality (Caching)", 0);
  
  #endregion
  
  #region 9. GetAll (static fields only) -------------------------------------------------------------------------
  
  var allDefined = Permissions.GetAll();
  
  new { DefinedFlags = allDefined
       ,FlagCodes    = string.Join(", ", allDefined.Select(p => p.Code))
       ,Count        = allDefined.Count }
    .Dump("9. GetAll (static fields only)", 0);
    
  #endregion
  
  #region 10. None flag ------------------------------------------------------------------------------------------
  
  var none = Permissions.None;
  
  new { None          = none
       ,HasRead       = none.HasFlag(Permissions.Read)
       ,ToFlagsCount  = none.ToFlags().Count
       ,Flags         = none.ToFlags() }
    .Dump("10. None Flag", 0);
  
  #endregion
  
  #region 11. All flag -------------------------------------------------------------------------------------------
  
  var all = Permissions.All;
  
  new { All         = all
       ,ID          = all.ID
       ,HasRead     = all.HasFlag(Permissions.Read)
       ,HasAllFlags = all.HasAllFlags(Permissions.Read, Permissions.Write, Permissions.Execute, Permissions.Delete)
       ,ToFlags     = all.ToFlags()
       ,FlagCodes   = string.Join(", ", all.ToFlags().Select(f => f.Code)) }
    .Dump("11. All Flag", 0);
  
  #endregion

  #region 12. AsJsonString - Single Flag vs Combination ----------------------------------------------------------
  
  var singleFlag  = Permissions.Read;
  var combination = Permissions.Read | Permissions.Write;
  var staticCombo = Permissions.All;
  
  new { SingleFlag_String  = singleFlag.AsJsonString()
       ,Combination_String = combination.AsJsonString()
       ,StaticCombo_String = staticCombo.AsJsonString() }
    .Dump("12. AsJsonString - Single Flag vs Combination", 0);
  
  #endregion
  
  #region 13. AsJsonObject - Single Flag vs Combination ----------------------------------------------------------
  
  new { SingleFlag_Object  = singleFlag.AsJsonObject()
       ,Combination_Object = combination.AsJsonObject()
       ,StaticCombo_Object = staticCombo.AsJsonObject() }
    .Dump("13. AsJsonObject - Single Flag vs Combination", 0);
    
  #endregion
  
  #region 14. Explicit/Implicit Operators ------------------------------------------------------------------------
  
  // Explicit cast: int -> TypedFlag
  var fromInt = (Permissions)3;  // Should be Read | Write
  
  // Implicit cast: TypedFlag -> int
  int toInt = Permissions.Execute;  // Should be 4
  
  // Implicit cast: TypedFlag -> string
  string toString = Permissions.Read;  // Should be "Read"
  
  // Combination with operators
  var combo        = (Permissions)7;  // Should be Read | Write | Execute (or All)
  int comboValue   = combo;           // Should be 7
  string comboDesc = combo;           // Should be description
  
  new { ExplicitCast_FromInt = new { Expression  = "(Permissions)3"
                                    ,Result      = fromInt
                                    ,ID          = fromInt.ID
                                    ,Code        = fromInt.Code
                                    ,Description = fromInt.Description
                                    ,Flags       = fromInt.ToFlags()}
                                    
       ,ImplicitCast_ToInt = new { Expression = "int toInt = Permissions.Execute"
                                  ,Result     = toInt
                                  ,IsCorrect  = toInt == 4 }
                                    
       ,ImplicitCast_ToString = new { Expression = "string toString = Permissions.Read"
                                     ,Result      = toString
                                     ,IsCorrect = toString == "Read" }
                                     
       ,ComboCast = new { Expression  = "(Permissions)7"
                         ,Result      = combo
                         ,ID          = comboValue
                         ,Description = comboDesc
                         ,Flags       = combo.ToFlags() }}
    .Dump("Explicit/Implicit Operator Tests", 0);
  
  // Edge case: Cast to existing static field
  var shouldBeRead = (Permissions)1;
  var areSame = ReferenceEquals(shouldBeRead, Permissions.Read);
  
  new { CastToOne        = shouldBeRead
       ,IsReferenceEqual = areSame
       ,Note             = areSame ? "✅ Cast returns cached static field" 
                                   : "⚠️ Cast created new instance (not cached)"}
    .Dump("Reference Equality Test", 0);
  
  #endregion
}
