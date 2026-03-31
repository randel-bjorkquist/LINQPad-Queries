<Query Kind="Program">
  <NuGetReference Prerelease="true">EnumCraft.Core</NuGetReference>
  <NuGetReference Prerelease="true">EnumCraft.FeatureFlags</NuGetReference>
  <NuGetReference Prerelease="true">EnumCraft.FeatureFlags.Json</NuGetReference>
  <NuGetReference Prerelease="true">EnumCraft.Json</NuGetReference>
  <Namespace>EnumCraft</Namespace>
</Query>

//-------------------------------------------------------------------------------------------------
#load ".\Permissions"

//-------------------------------------------------------------------------------------------------
void Main()
{
  #region TypedFlags
  
  #region Public Static ReadOnly Fields
  
  "Public Static ReadOnly Fields -----------------------------------------".Dump();
  
  Permissions.None.Dump("Permissions.None", 0);
  Permissions.Read.Dump("Permissions.Read", 0);
  Permissions.Write.Dump("Permissions.Write", 0);
  Permissions.Execute.Dump("Permissions.Execute", 0);
  Permissions.Delete.Dump("Permissions.Delete", 0);
  
  #endregion
  
  #region Variables Assigned to Fields
  
  Console.WriteLine();  
  "Variables Assigned to Fields ------------------------------------------".Dump();
  
  var permissions_none = Permissions.None;
  permissions_none.Dump("var permissions_none = Permissions.None;", 0);
  
  var permissions_read = Permissions.Read;
  permissions_read.Dump("var permissions_read = Permissions.Read", 0);
  
  var permissions_write = Permissions.Write;
  permissions_write.Dump("var permissions_write = Permissions.Write;", 0);
  
  var permissions_execute = Permissions.Execute;
  permissions_execute.Dump("var permissions_execute = Permissions.Execute;", 0);
  
  var permissions_delete = Permissions.Delete;
  permissions_delete.Dump("var permissions_delete = Permissions.Delete;", 0);
  
  #endregion
  
  #region GetAll() w/Dump()
  
  "".Dump();
  "GetAll() --------------------------------------------------------------".Dump();
  
  var permissions_all = Permissions.GetAll();
  permissions_all.Dump("var permissions_all = Permissions.All;", 0);

  foreach(var permission in permissions_all)
  {
    new { permission  = permission
         ,Flags       = permission.ToFlags() }
      .Dump($"var permission in permissions_all", 1);
  }

  #endregion
  
  #region GetByID() w/Dump()

  "".Dump();
  "GetById() -------------------------------------------------------------".Dump();

  var max_id = Permissions.GetAll().Max(p => p.ID);

  for(int i = 0; i <= max_id; i++)
  {
    var permission = Permissions.GetByID(i);
    
    new { permission  = permission
         ,Flags       = permission.ToFlags() }
      .Dump($"var permission = Permissions.GetByID({i});", 1);
  }
  
  #endregion
  
  #endregion
}

// You can define other methods, fields, classes and namespaces here
