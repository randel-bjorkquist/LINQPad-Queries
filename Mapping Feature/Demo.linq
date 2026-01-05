<Query Kind="Program" />

//-------------------------------------------------------------------------------------------------------
#load "Mapping Feature\Interfaces"            //NOTE: Interesting enough, I need to include
#load "Mapping Feature\Mapping Extensions"    //      "Mapping Feature\Interfaces" in my demo file, even
#load "Mapping Feature\Manual Mappings"       //      though it's not used here; but rather the 
#load "Mapping Feature\Mapping Registry"      //      "Mapping Feature\Mapping Scanner", which it's 
#load "Mapping Feature\Mapping Scanner"       //      included in as well.
#load "Mapping Feature\Types"                 

void Main()
{
  // === Register mappings (your composition root) === --------------------------------------------------  
  ManualMappings.RegisterAll();

  // === Auto-Scan (auto-registration) current assembly for IMapFrom<,> implementations === -------------  
  MappingScanner.ScanAndRegister();

  // === Sample data === ----------------------------------------------------------------------------------
  var user_fred_flintstone = new User { ID        = Guid.NewGuid()
                                       ,FirstName = "Fred"
                                       ,LastName  = "Flintstone"
                                       ,Email     = "fred.flintstone@bedrock.bc" }
                                  .Dump("user_fred_flintstone", 0);

  var user_barney_rubble = new User { ID        = Guid.NewGuid()
                                     ,FirstName = "Barney"
                                     ,LastName  = "Rubble"
                                     ,Email     = "barney.rubble@bedrock.bc" }
                                .Dump("user_barney_rubble", 0);

  var users = new List<User> { user_fred_flintstone, user_barney_rubble }
                    .Dump("var users = new List<User> { user_fred_flintstone, user_barney_rubble };", 0);

  // === The beautiful, natural syntax you wanted === -----------------------------------------------------
  UserDTO dto_fred_flintstone = user_fred_flintstone.MapTo<UserDTO>();
  dto_fred_flintstone.Dump("UserDTO dto_fred_flintstone = user_fred_flintstone.MapTo<UserDTO>();", 0);

  User fred_flintstone = dto_fred_flintstone.MapTo<User>();
  fred_flintstone.Dump("NOTE (roundtrip): User fred_flintstone = dto_fred_flintstone.MapTo<User>();", 0);
  
  //-------------------------------------------------------------------------------------------------------
  var dtos = users.MapTo<UserDTO>();
  dtos.Dump("var dtos = users.MapTo<UserDTO>();", 0);

  var back_to_users = dtos.MapTo<User>();
  back_to_users.Dump("NOTE (roundtrip): var back_to_users = dtos.MapTo<User>();", 0);  
}
