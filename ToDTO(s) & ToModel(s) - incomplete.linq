<Query Kind="Program">
  <NuGetReference>Microsoft.AspNetCore.Http</NuGetReference>
  <Namespace>Microsoft.AspNetCore.Identity</Namespace>
  <Namespace>NZWalks.API.Models.Domain</Namespace>
  <Namespace>NZWalks.API.Models.DTOs</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations.Schema</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations</Namespace>
</Query>

void Main()
{
  
}

#region Models/Domain

public class Difficulty
{
  //Database Properties ------------------------------------------------
  public Guid ID      { get; set; }
  public string Name  { get; set; } = string.Empty;
}

public class Walk
{
  //Database Properties ------------------------------------------------
  public Guid ID                { get; set; }

  public string Name            { get; set; } = string.Empty;
  public string Description     { get; set; } = string.Empty;

  public double LengthInKM      { get; set; }

  public string? ImageURL       { get; set; }

  public Guid DifficultyID      { get; set; }
  public Guid RegionID          { get; set; }


  //Navigation Properties ----------------------------------------------
  public Difficulty? Difficulty { get; set; }
  public Region? Region         { get; set; }
}

public class Region
{
  //Database Properties ------------------------------------------------
  public Guid ID          { get; set; }

  public string Code      { get; set; } = string.Empty;
  public string Name      { get; set; } = string.Empty;

  public string? ImageURL {  get; set; } = string.Empty;
}

public class Image
{
  public Guid ID { get; set; }

  [NotMapped]
  public IFormFile File { get; set; }

  public string FileName          { get; set; } = string.Empty;
  public string? FileDescription  { get; set; } = null;
  public string FileExtension     { get; set; } = string.Empty;
  public string FilePath          { get; set; } = string.Empty;
  public long FileSize            { get; set; }
}

#endregion

#region Models/DTOs

public class AddDifficultyRequestDTO
{
  [Required]
  [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters.")]
  public string Name  { get; set; } = string.Empty;
}

public class AddRegionRequestDTO
{
  [Required]
  [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 characters.")]
  [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 characters.")]
  public string Code        { get; set; } = string.Empty;

  [Required]
  [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters.")]
  public string Name        { get; set; } = string.Empty;

  public string? ImageURL   {  get; set; } = string.Empty;
}

public class AddWalkRequestDTO
{
  [Required]
  [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters.")]
  public string Name { get; set; } = string.Empty;

  [Required]
  [MaxLength(1000, ErrorMessage = "Description has to be a maximum of 1000 characters.")]
  public string Description { get; set; } = string.Empty;

  [Required]
  [Range(0, 50, ErrorMessage = "LengthInKm has a range of zero (0) to 50.")]
  public double LengthInKM { get; set; }

  [MaxLength(500, ErrorMessage = "ImageURL has to be a maximum of 500 characters.")]
  public string? ImageURL { get; set; }

  [Required]
  public Guid DifficultyID { get; set; }
  
  [Required]
  public Guid RegionID { get; set; }
}

public class DifficultyDTO
{
  public Guid ID      { get; set; }
  public string Name  { get; set; } = string.Empty;
}

public class ImageUploadRequestDTO
{
  [Required]
  public IFormFile File { get; set; }

  [Required]
  public string FileName {  get; set; } = string.Empty;

  public string? FileDescription { get; set; }
}

public class RegionDTO
{
  public Guid ID            { get; set; }

  public string Code        { get; set; } = string.Empty;
  public string Name        { get; set; } = string.Empty;

  public string? ImageURL   {  get; set; } = string.Empty;
}

public class UpdateDifficultyRequestDTO
{
  [Required]
  [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters.")]
  public string Name  { get; set; } = string.Empty;
}

public class UpdateRegionRequestDTO
{
  [Required]
  [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 characters.")]
  [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 characters.")]
  public string Code      { get; set; } = string.Empty;

  [Required]
  [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters.")]
  public string Name      { get; set; } = string.Empty;

  public string? ImageURL {  get; set; } = string.Empty;
}

public class UpdateWalkRequestDTO
{
  [Required]
  [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters.")]
  public string Name { get; set; } = string.Empty;

  [Required]
  [MaxLength(1000, ErrorMessage = "Description has to be a maximum of 1000 characters.")]
  public string Description { get; set; } = string.Empty;

  [Required]
  [Range(0, 50, ErrorMessage = "LengthInKm has a range of zero (0) to 50.")]
  public double LengthInKM { get; set; }

  [MaxLength(500, ErrorMessage = "ImageURL has to be a maximum of 500 characters.")]
  public string? ImageURL { get; set; }

  [Required]
  public Guid DifficultyID { get; set; }

  [Required]
  public Guid RegionID { get; set; }
}

public class WalkDTO
{
  public Guid ID                    { get; set; }

  public string Name                { get; set; } = string.Empty;
  public string Description         { get; set; } = string.Empty;

  public double LengthInKM          { get; set; }

  public string? ImageURL           { get; set; }

  public Guid DifficultyID          { get; set; }
  public DifficultyDTO? Difficulty  { get; set; } 

  public Guid RegionID              { get; set; }
  public RegionDTO? Region          { get; set; }
}

#endregion

public static class DTOExtensions
{
  #region Generic ToDTO(s) & ToModel(s) --------------------------------------------------------------------------------------------

  #region COMMENTED OUT: R&D CODE: does not require the class constraints ...
  /* *
  
  //NOTE: Removing the class constraints, elimiates the Warning messages found within the "Error List" window ...
  
  public static TDto? ToDTO<TModel, TDto>(this TModel model, Func<TModel, TDto> convert)
  {
    if(model is null) { return default; }
  
    return convert(model);
  }
  
  public static IEnumerable<TDto> ToDTOs<TModel, TDto>(this IEnumerable<TModel> models, Func<TModel, TDto> convert)
  {
    if(models is null) { return [ ]; }
  
    return models.Select(dto => convert(dto));
  }
  
  public static TModel? ToModel<TDto, TModel>(this TDto dto, Func<TDto, TModel> convert)
  {
    if (dto is null) { return default; }

    return convert(dto);
  }

  public static IEnumerable<TModel> ToModels<TDto, TModel>(this IEnumerable<TDto> dtos, Func<TDto, TModel> convert)
  {
    if (dtos is null) { return [ ]; }

    return dtos.Select(dto => convert(dto));
  }

  * */
  #endregion R&D CODE: does not require the class constraints ...

  #region contains class constraints ...
  
  public static TDto? ToDTO<TModel, TDto>(this TModel model, Func<TModel, TDto> convert)
    where TDto   : class
    where TModel : class
  {
    if(model is null) { return null; }

    return convert(model);
  }

  public static IEnumerable<TDto> ToDTOs<TModel, TDto>(this IEnumerable<TModel> models, Func<TModel, TDto> convert)
    where TModel : class
    where TDto   : class
  {
    if(models is null) { return Enumerable.Empty<TDto>(); }

    return models.Select(dto => convert(dto));
  }

  public static TModel? ToModel<TDto, TModel>(this TDto dto, Func<TDto, TModel> convert)
    where TDto   : class
    where TModel : class
  {
    if (dto is null) { return null; }

    return convert(dto);
  }

  public static IEnumerable<TModel> ToModels<TDto, TModel>(this IEnumerable<TDto> dtos, Func<TDto, TModel> convert)
    where TDto   : class
    where TModel : class
  {
    if (dtos is null) { return Enumerable.Empty<TModel>(); }

    return dtos.Select(dto => convert(dto));
  }
  
  #endregion COMMENTED OUT: contains class constraints ...
  
  #endregion

  #region ToDTO(s) -----------------------------------------------------------------------------------------------------------------

  public static DifficultyDTO? ToDTO(this Difficulty model)
  {
    if (model is null) { return null; }

    return new DifficultyDTO { ID           = model.ID  
                              ,Name         = model.Name };
  }

  public static RegionDTO? ToDTO(this Region model)
  {
    if (model is null) { return null; }

    return new RegionDTO { ID       = model.ID
                          ,Code     = model.Code
                          ,Name     = model.Name
                          ,ImageURL = model.ImageURL };
  }

  public static WalkDTO? ToDTO(this Walk model)
  {
    if (model is null) { return null; }

    return new WalkDTO { ID           = model.ID  
                        ,Name         = model.Name
                        ,Description  = model.Description
                        ,LengthInKM   = model.LengthInKM
                        ,ImageURL     = model.ImageURL
                        ,DifficultyID = model.DifficultyID
                        ,Difficulty   = null
                        ,RegionID     = model.RegionID 
                        ,Region       = null };
  }

  #endregion

  #region ToModel(s) ---------------------------------------------------------------------------------------------------------------

  public static Difficulty? ToModel(this AddDifficultyRequestDTO dto)
  {
    if (dto is null) { return null; }

    return new Difficulty { Name = dto.Name };
  }

  public static Difficulty? ToModel(this UpdateDifficultyRequestDTO dto)
  {
    if (dto is null) { return null; }

    return new Difficulty { Name = dto.Name };
  }

  public static Region? ToModel(this AddRegionRequestDTO dto)
  {
    if(dto is null) { return null; }

    return new Region { Code      = dto.Code.Trim()
                       ,Name      = dto.Name.Trim()
                       ,ImageURL  = dto.ImageURL?.Trim() };
  }

  public static Region? ToModel(this UpdateRegionRequestDTO dto)
  {
    if(dto is null) { return null; }

    return new Region { Code      = dto.Code.Trim()
                       ,Name      = dto.Name.Trim()
                       ,ImageURL  = dto.ImageURL?.Trim() };
  }

  public static Walk? ToModel(this AddWalkRequestDTO dto)
  {
    if (dto is null) { return null; }

    return new Walk { Name          = dto.Name
                     ,Description   = dto.Description
                     ,LengthInKM    = dto.LengthInKM
                     ,ImageURL      = dto.ImageURL
                     ,DifficultyID  = dto.DifficultyID
                     ,Difficulty    = null
                     ,RegionID      = dto.RegionID 
                     ,Region        = null };
  }

  public static Walk? ToModel(this UpdateWalkRequestDTO dto)
  {
    if (dto is null) { return null; }

    return new Walk { Name          = dto.Name
                     ,Description   = dto.Description
                     ,LengthInKM    = dto.LengthInKM
                     ,ImageURL      = dto.ImageURL
                     ,DifficultyID  = dto.DifficultyID
                     ,Difficulty    = null
                     ,RegionID      = dto.RegionID 
                     ,Region        = null };
  }

  public static Image? ToModel(this ImageUploadRequestDTO dto) 
  {
    if (dto is null) { return null; }

    return new Image { File             = dto.File
                      ,FileName         = dto.FileName
                      ,FileDescription  = dto.FileDescription
                      ,FileExtension    = Path.GetExtension(dto.File.FileName)
                                              .ToLower()
                      ,FileSize         = dto.File.Length };
  }

  #endregion
}
