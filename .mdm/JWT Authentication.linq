<Query Kind="Program">
  <NuGetReference>System.IdentityModel.Tokens.Jwt</NuGetReference>
  <Namespace>Microsoft.IdentityModel.Tokens</Namespace>
  <Namespace>System.Security.Claims</Namespace>
  <Namespace>System.IdentityModel.Tokens.Jwt</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

#region XML COMMENTS
/// <title>JWT Authentication using C#</title>
/// <url>https://medium.com/@mmoshikoo/jwt-authentication-using-c-54e0c71f21b0</url>
/// <summary></summary>
/// <additional_resources>
///   Simple JWT Authentication Explanation
///     - https://medium.com/@mmoshikoo/simple-jwt-authentication-explanation-81e930a1a01f
///   JWT Authentication using NodeJS
///     - https://medium.com/@mmoshikoo/jwt-authentication-using-nodejs-82a1d8fb8648
/// </additional_resources>
#endregion
void Main()
{
  IAuthContainerModel model = Data.GetJWTContainerModel("Fred Flintstone", "Fred.Flintstone@bedrock.bc");
  IAuthService auth_service = new JWTService(model.SecretKey);
  
  string token = auth_service.GenerateToken(model);
  token.Dump("token", 0);
  
  if(!auth_service.IsTokenValid(token))
    throw new UnauthorizedAccessException();
    
  var claims = auth_service.GetTokenClaims(token).ToList();
  
  var name_claim  = claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Name));  
  name_claim.Dump("name_claim", 0);
  name_claim.Value.Dump("name_claim.Value");
  
  var email_claim = claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email));
  email_claim.Dump("email_claim", 0);
  email_claim.Value.Dump("email_claim.Value");
}

#region Interfaces =======================================================

public interface IAuthService
{
  string SecretKey { get; set; }
  
  bool IsTokenValid(string token);  
  string GenerateToken(IAuthContainerModel model);  
  IEnumerable<Claim> GetTokenClaims(string token);  
}

public interface IAuthContainerModel
{
  #region Member(s)
  
  string SecretKey          { get; set; }
  string SecurityAlgorithm  { get; set; }
  int ExpireMinutes         { get; set; }
  
  Claim[] Claims            { get; set; }
  
  #endregion
}

#endregion

#region Model(s) =========================================================

public class JWTContainerModel : IAuthContainerModel
{
  #region Public Method(s)
  
  public int ExpireMinutes        { get; set; } = 10080; // 7 days
  public string SecretKey         { get; set; } = "TW9zaGVFcmV6UHJpdmF0ZUtleQ=="; //this secret key should be moved to some configuration outter service
  public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
  
  public Claim[] Claims           { get; set; }
  
  #endregion
}

#endregion

#region Services(s) ======================================================

public class JWTService : IAuthService
{
  #region Constructor(s) -------------------------------------------------
  
  public JWTService(string secret_key)
  {
    SecretKey = secret_key;
  }
  
  #endregion
  
  #region Member(s) ------------------------------------------------------
  
  public string SecretKey { get; set; }
  
  #endregion
  
  #region Private Method(s) ----------------------------------------------
  
  private SecurityKey GetSymmetricSecurityKey()
  {
    byte[] symmetric_key = Convert.FromBase64String(SecretKey);
    return new SymmetricSecurityKey(symmetric_key);
  }
  
  private TokenValidationParameters GetTokenValidationParameters()
  {
    return new TokenValidationParameters { ValidateIssuer   = false
                                          ,ValidateAudience = false
                                          ,IssuerSigningKey = GetSymmetricSecurityKey() };
  }

  #endregion
  
  #region Public Method(s) -----------------------------------------------
  
  public bool IsTokenValid(string token)
  {
    try
    {	        
      _ = GetTokenClaims(token);
      return true;
    }
    catch
    {      
      return false;
    }
    
    #region COMMENTED OUT: ORIGINAL CODE
    //
    //if(string.IsNullOrWhiteSpace(token))
    //  throw new ArgumentException("Given token is null, empty, or only contains whitespaces.");
    //  
    //var token_validation_parameters = GetTokenValidationParameters();
    //var security_token_handler      = new JwtSecurityTokenHandler();
    //
    //try
    //{	        
    //  ClaimsPrincipal token_valid = security_token_handler.ValidateToken( token
    //                                                                     ,token_validation_parameters
    //                                                                     ,out SecurityToken validated_token );
    //  return true;
    //}
    //catch
    //{
    //  return false;
    //}
    //
    #endregion
  }
  
  public string GenerateToken(IAuthContainerModel model) 
  {
    if(model == null || model.Claims == null || !model.Claims.Any())
      throw new ArgumentException("Arguements to create token are not valid.");
      
    var token_descriptor = new SecurityTokenDescriptor { Subject            = new ClaimsIdentity(model.Claims)
                                                        ,Expires            = DateTime.UtcNow.AddMinutes(Convert.ToInt32(model.ExpireMinutes))
                                                        ,SigningCredentials = new SigningCredentials( GetSymmetricSecurityKey()
                                                                                                     ,model.SecurityAlgorithm ) };
    
    var security_token_handler = new JwtSecurityTokenHandler();
    var security_token         = security_token_handler.CreateToken(token_descriptor);
    
    var token = security_token_handler.WriteToken(security_token);
    return token;
  }  
  
  public IEnumerable<Claim> GetTokenClaims(string token) 
  { 
    if(string.IsNullOrWhiteSpace(token))
      throw new ArgumentException("Given token is null, empty, or only contains whitespaces.");
      
    var token_validation_parameters = GetTokenValidationParameters();
    var security_token_handler      = new JwtSecurityTokenHandler();
    
    try
    {	        
      ClaimsPrincipal token_valid = security_token_handler.ValidateToken( token
                                                                         ,token_validation_parameters
                                                                         ,out SecurityToken validated_token );
      return token_valid.Claims;
    }
    catch(Exception ex)
    {
      throw ex;
    }    
  }

  #endregion
}

#endregion

//========================================================================
public static class Data
{
  //NOTE: incomplete - something not right with this ... thought process ...
  //public static string InvalidSecretKey => "TW9zaGVFcmV6UHJpdmF0ZUtleQ++";
  
  public static JWTContainerModel GetJWTContainerModel(string name, string email)
  {
    return new JWTContainerModel { Claims = new Claim[] { new Claim(ClaimTypes.Name  ,name)
                                                         ,new Claim(ClaimTypes.Email ,email ) }};
  }   
}
