<Query Kind="Statements" />

//---------------------------------------------------------------------------------------
#load ".\TypedEnum"

//---------------------------------------------------------------------------------------
public sealed class HousingAuthority : TypedEnumInt<HousingAuthority>
{
  private HousingAuthority(int id, string description, string code)
    : base(id, description, code) { }
   
  #region specific HousingAuthority definitions ...
  
  public static readonly HousingAuthority TenantA = new(1190, "Tenant A", nameof(TenantA));
  public static readonly HousingAuthority TenantB = new(1290, "Tenant B", nameof(TenantB));
  public static readonly HousingAuthority TenantC = new(1390, "Tenant C", nameof(TenantC));
  
  #endregion
}