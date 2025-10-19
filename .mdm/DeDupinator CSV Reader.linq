<Query Kind="Program">
  <Connection>
    <ID>e0c55da3-d2d4-4e98-846e-45d8bb406067</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="CsvLINQPadDriver" PublicKeyToken="no-strong-name">CsvLINQPadDriver.CsvDataContextDriver</Driver>
    <DisplayName>C:\temp\_springworks\dedupinator\V_DECUPE_HCO_RESYNC_PRD.csv (2023-09-26 14:20:07, 1 file 103.8 MB)</DisplayName>
    <DriverData>
      <Files>C:\temp\_springworks\dedupinator\V_DECUPE_HCO_RESYNC_PRD.csv
</Files>
      <UseCsvHelperSeparatorAutoDetection>false</UseCsvHelperSeparatorAutoDetection>
      <RenameTable>false</RenameTable>
      <StringComparison>Ordinal</StringComparison>
      <UseStringComparerForStringIntern>false</UseStringComparerForStringIntern>
      <DebugInfo>true</DebugInfo>
      <IgnoreBadData>false</IgnoreBadData>
      <CsvSeparator>,</CsvSeparator>
      <HeaderDetection>AllLettersNumbersPunctuation</HeaderDetection>
      <IgnoreBlankLines>false</IgnoreBlankLines>
    </DriverData>
  </Connection>
  <NuGetReference>CsvHelper</NuGetReference>
  <Namespace>CsvHelper</Namespace>
  <Namespace>CsvHelper.Configuration</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
  // NON-HCO RECORDS -------------------------------------------------------------------------------------------------
  #region NON-HCO RECORDS 
  
  var non_hco_records = V_DECUPE_HCO_RESYNC_PRD.Where(r => r.ENTITYTYPE != "/Organization/HCO")
                                               .GroupBy(r => r.SPRINGWORKS_HCO_NPINUMBER)
//                                               .Where(r => r.Count() > 1)
                                               .OrderBy(r => r.Key)
                                               .Select(g => new DeDupinatorHCO { NPI  = g.Key
                                                                                ,HCOs = g.Select(r => new HCO(r))
                                                                                         .OrderBy(r => r.OrganizationName)
                                                                                         .ThenBy(r => r.State)
                                                                                         .ThenBy(r => r.City)
                                                                                         .ThenBy(r => r.AddressLine1)
                                                                                         .ThenBy(r => r.AddressLine2)
                                                                                         .ToList() })
//                                               .Where(r => r.NPI == "1144238908")
                                               .ToList();

  non_hco_records.Dump($"NON-HCO Records ({non_hco_records.Sum(r => r.HCOs.Count())})", 0);
  
  non_hco_records.SelectMany(r => r.HCOs.Select(hco => hco.Source))
                 .Distinct()
                 .Dump($"Distinct Sources from 'CODES'", 0);

  #endregion

  // HCO RECORDS -----------------------------------------------------------------------------------------------------
  #region HCO RECORDS
  
  var hco_records = V_DECUPE_HCO_RESYNC_PRD.Where(r => r.ENTITYTYPE == "/Organization/HCO")
                                           .GroupBy(r => r.SPRINGWORKS_HCO_NPINUMBER)
//                                           .Where(r => r.Count() > 1)
                                           .OrderBy(r => r.Key)
                                           .Select(g => new DeDupinatorHCO { NPI  = g.Key
                                                                            ,HCOs = g.Select(r => new HCO(r))
                                                                                     .OrderBy(r => r.OrganizationName)
                                                                                     .ThenBy(r => r.State)
                                                                                     .ThenBy(r => r.City)
                                                                                     .ThenBy(r => r.AddressLine1)
                                                                                     .ThenBy(r => r.AddressLine2)
                                                                                     .ToList() })
//                                           .Where(r => r.NPI == "1295848307")
                                           .ToList();
  
  hco_records.Dump($"All HCO Record Count ({hco_records.Sum(r => r.HCOs.Count())})", 0);
  
  // HCOs WITH NPIS --------------------------------------------------------------------------------------------------
  var hcos_with_npis = hco_records.Where(a => !string.IsNullOrWhiteSpace(a.NPI)
                                           && a.HCOs.Count() > 1);
  
  hcos_with_npis.SelectMany(r => r.HCOs.Select(hco => hco.Source))
                .Distinct()
                .Dump($"Distinct Sources from 'CODES'", 0);
  
  hcos_with_npis
                //.Where(hco => hco.Score == "10000000")
                .OrderBy(r => r.Score)
                .Dump("HCO Records w/NPIs", 0);


  //hcos_with_npis.SelectMany(hco => hco.HCOs.Select(h => new { Name = h.OrganizationName
  //                                                           ,HashCode = h.OrganizationName.GetHashCode() }))
  //              .Dump("OrganizationName.GetHashCode");


  // HCOs WITHOUT NPIS -----------------------------------------------------------------------------------------------
  var hcos_without_npis = hco_records.Where(a => string.IsNullOrWhiteSpace(a.NPI)
                                              && a.HCOs.Count() > 1);
  
  hcos_without_npis.SelectMany(r => r.HCOs.Select(hco => hco.Source))
                   .Distinct()
                   .Dump($"Distinct Sources from 'CODES'", 0);
  
  hcos_without_npis.OrderBy(r => r.Score)
                   .Dump("HCO Records w/o NPIs", 0);
  
  #endregion
  
  // -----------------------------------------------------------------------------------------------------------------
  #region COMMENTED OUT: READ CSV FILE MANUALLY

  // -----------------------------------------------------------------------------------------------------------------
  // -----------------------------------------------------------------------------------------------------------------
  //NOTE: this is only here to show PRE-CALCULATED states ...
  //records.Dump("records", 1);
  //
  //foreach(var record in records_with_npis)
  //{    
  //}
  //
  //var records_with_npis = all_records.SelectMany(a => a.HCOs.Where(hco => hco.))
  //
  //var hco_records = all_records.Where(a => a.)
  //
  // -----------------------------------------------------------------------------------------------------------------
  // -----------------------------------------------------------------------------------------------------------------  

  #region Records WITH OUT NPIs
  //
  //var records_without_npis = V_DECUPE_HCO_RESYNC_PRD.Where(r => r.ENTITYTYPE == "/Organization/HCO"
  //                                                           && string.IsNullOrWhiteSpace(r.SPRINGWORKS_HCO_NPINUMBER))
  //                                                  .GroupBy(r => r.SPRINGWORKS_HCO_NPINUMBER)
  //                                                  .Where(r => r.Count() > 1)
  //                                                  .OrderBy(r => r.Key)
  //                                                  .Select(g => new DeDupinatorHCO { NPI  = g.Key
  //                                                                                   ,HCOs = g.Select(r => new HCO(r))
  //                                                                                            .OrderBy(r => r.OrganizationName)
  //                                                                                            .ThenBy(r => r.State)
  //                                                                                            .ThenBy(r => r.City)
  //                                                                                            .ThenBy(r => r.AddressLine1)
  //                                                                                            .ThenBy(r => r.AddressLine2)
  //                                                                                            .ToList()})
  //                                                  .ToList();
  //
  #endregion
  
  #region Records WITH NPIs
  //
  //var records_with_npis = V_DECUPE_HCO_RESYNC_PRD.Where(r => r.ENTITYTYPE == "/Organization/HCO"
  //                                                        && !string.IsNullOrWhiteSpace(r.SPRINGWORKS_HCO_NPINUMBER))
  //                                               .GroupBy(r => r.SPRINGWORKS_HCO_NPINUMBER)
  //                                               .Where(r => r.Count() > 1)
  //                                               .OrderBy(r => r.Key)
  //                                               .Select(g => new DeDupinatorHCO { NPI  = g.Key
  //                                                                                ,HCOs = g.Select(r => new HCO(r))
  //                                                                                         .OrderBy(r => r.OrganizationName)
  //                                                                                         .ThenBy(r => r.State)
  //                                                                                         .ThenBy(r => r.City)
  //                                                                                         .ThenBy(r => r.AddressLine1)
  //                                                                                         .ThenBy(r => r.AddressLine2)
  //                                                                                         .ToList() })
////                                                 .Where(r => r.NPI == "1144238908")
  //                                               .ToList();
  //
  #endregion
  
//  string csv_file_path  = @"C:\temp\Springworks\dedupinator\V_DECUPE_HCO_RESYNC_PRD.csv";
//  string[] csv_lines    = File.ReadAllLines(csv_file_path);
//  string[] data_rows    = csv_lines.Skip(1).ToArray();
//
//  using(var reader = new StreamReader(csv_file_path))
//  using(var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
//  {
//    var records = csv.GetRecords<RV_DECUPE_HCO_RESYNC_PRD>();
//    
//    //records.Take(172).Dump();
//    records.Skip(172).Take(1).Dump();
//    
////    foreach(var record in records)
////    {
////      record.Dump("record", 0);
////    }
//  }
  
  #region COMMENTED OUT: query
  /*
  var query = from line in data_rows
              let data = line.Split(',')
              select new RV_DECUPE_HCO_RESYNC_PRD { CODES                                 = data[0]
                                                   ,CREATEDDATE                           = data[1]
                                                   ,DISCOVERYDATE                         = data[2]
                                                   ,ENTITYTYPE                            = data[3]
                                                   ,ID                                    = data[4]
                                                   ,ISDELETED                             = data[5]
                                                   ,MODIFIEDDATE                          = data[6]
                                                   ,ORIGINENTITYCODE                      = data[7]
                                                   ,PERSISTHASH                           = data[8]
                                                   ,REVISION                              = data[9]
                                                   ,SORTDATE                              = data[10]
                                                   ,SPRINGWORKS_HCO__340BFLAG              = data[11]
                                                   ,SPRINGWORKS_HCO__340BHOSPITALTYPE      = data[12]
                                                   ,SPRINGWORKS_HCO__340BPROGRAM           = data[13]
                                                   ,SPRINGWORKS_HCO_ACCOUNTOWNER          = data[14]
                                                   ,SPRINGWORKS_HCO_ACCOUNTSTATUS         = data[15]
                                                   ,SPRINGWORKS_HCO_ADDRESSID             = data[16]
                                                   ,SPRINGWORKS_HCO_ADDRESSLINE1          = data[17]
                                                   ,SPRINGWORKS_HCO_ADDRESSLINE2          = data[18]
                                                   ,SPRINGWORKS_HCO_ADDRESSPHONE          = data[19]
                                                   ,SPRINGWORKS_HCO_ADDRESSTYPE           = data[20]
                                                   ,SPRINGWORKS_HCO_AFFILIATIONID         = data[21]
                                                   ,SPRINGWORKS_HCO_ALTERNATEACCOUNTNAME  = data[22]
                                                   ,SPRINGWORKS_HCO_CITY                  = data[23]
                                                   ,SPRINGWORKS_HCO_COTDESCRIPTION        = data[24]
                                                   ,SPRINGWORKS_HCO_COUNTRY               = data[25]
                                                   ,SPRINGWORKS_HCO_CRMEMAIL              = data[26]
                                                   ,SPRINGWORKS_HCO_DEA                   = data[27]
                                                   ,SPRINGWORKS_HCO_EMAIL                 = data[28]
                                                   ,SPRINGWORKS_HCO_FAX                   = data[29]
                                                   ,SPRINGWORKS_HCO_HINNUMBER             = data[30]
                                                   ,SPRINGWORKS_HCO_LASTSENTDATETOVEEVA   = data[31]
                                                   ,SPRINGWORKS_HCO_LATITUDE              = data[32]
                                                   ,SPRINGWORKS_HCO_LONGITUDE             = data[33]
                                                   ,SPRINGWORKS_HCO_MDMID                 = data[34]
                                                   ,SPRINGWORKS_HCO_NPINUMBER             = data[35]
                                                   ,SPRINGWORKS_HCO_NUMBEROFBEDS          = data[36]
                                                   ,SPRINGWORKS_HCO_NUMBEROFMEMBERS       = data[37]
                                                   ,SPRINGWORKS_HCO_ORGANIZATIONNAME      = data[38]
                                                   ,SPRINGWORKS_HCO_ORGANIZATIONSUBTYPE   = data[39]
                                                   ,SPRINGWORKS_HCO_ORGANIZATIONTYPE      = data[40]
                                                   ,SPRINGWORKS_HCO_PHONE                 = data[41]
                                                   ,SPRINGWORKS_HCO_PRIMARYADDRESSFLAG    = data[42]
                                                   ,SPRINGWORKS_HCO_SENDTOVEEVA           = data[43]
                                                   ,SPRINGWORKS_HCO_SOURCE                = data[44]
                                                   ,SPRINGWORKS_HCO_SOURCEID              = data[45]
                                                   ,SPRINGWORKS_HCO_SPECIALTY1            = data[46]
                                                   ,SPRINGWORKS_HCO_SPECIALTY1CODE        = data[47]
                                                   ,SPRINGWORKS_HCO_SPECIALTY2            = data[48]
                                                   ,SPRINGWORKS_HCO_STATE                 = data[49]
                                                   ,SPRINGWORKS_HCO_VERIFIEDEMAIL         = data[50]
                                                   ,SPRINGWORKS_HCO_WEBSITE               = data[51]
                                                   ,SPRINGWORKS_HCO_ZIP                   = data[52]
                                                   ,SPRINGWORKS_HCO_ZIPEXTENSION          = data[53]
                                                   ,ZZ_LAST_CHANGE_TIMESTAMP              = data[54] };
  query.Dump();
  */
  #endregion
  
  #endregion
}

//RULE (DO NOT MERGE):
//  - DHC to DHC
//  - Salesforce (Veeva) to Salesforce (Veeva) 
//  - Different Organization Types
//  Jennifer was also doing ..
//  - data standardization
//    ~ To Upper Case
//    ~ examples:
//      + FLR = FLOOR
//      + AVE = AVENUE
//      + STE = SUITE

public class DeDupinatorHCO 
{
  public string NPI { get; set; }

  public string Score => string.Concat(new[] { 
                                               DistinctSources            == 0 || DistinctSources            == 1 ? "0" : "1" 
                                              ,DistinctOrganizationTypes  == 0 || DistinctOrganizationTypes  == 1 ? "0" : "1" 
                                              ,DistinctOrganizationNames  == 0 || DistinctOrganizationNames  == 1 ? "0" : "1"
                                              ,DistinctAddessLine1s       == 0 || DistinctAddessLine1s       == 1 ? "0" : "1"
                                              ,DistinctAddessLine2s       == 0 || DistinctAddessLine2s       == 1 ? "0" : "1"
                                              ,DistinctCities             == 0 || DistinctCities             == 1 ? "0" : "1"
                                              ,DistinctStates             == 0 || DistinctStates             == 1 ? "0" : "1"
                                              ,DistinctZips               == 0 || DistinctZips               == 1 ? "0" : "1" 
                                             });
  
  //public bool ExactMatches   => HCOs.Select(hco => hco.FullHashCode).Distinct().Count()    == 1;
  //public bool PartialMatches => HCOs.Select(hco => hco.PartialHashCode).Distinct().Count() == 1;
  
  public int DistinctSources => HCOs.Select(hco => hco.Source.ToLower()).Distinct().Count();
  
  public int DistinctOrganizationTypes => HCOs.Where(hco => !hco.IsOrganizationTypeBlank)
                                              .Select(hco => hco.OrganizationType.ToLower()).Distinct().Count();
  
  public int DistinctOrganizationNames => HCOs.Where(hco => !hco.IsOrganizationNameBlank)
                                              .Select(hco => hco.OrganizationName.ToLower()).Distinct().Count();
  
  public int DistinctAddessLine1s => HCOs.Where(hco => !hco.IsAddressLine1Blank)
                                         .Select(hco => hco.AddressLine1.ToLower()).Distinct().Count();
  
  //NOTE: look at building in ways to replace/swap text like "suite" for "ste"
  public int DistinctAddessLine2s => HCOs.Where(hco => !hco.IsAddressLine2Blank)
                                         .Select(hco => hco.AddressLine2.ToLower()).Distinct().Count();
                                         //.Select(hco => hco.AddressLine2.ToLower().Replace("suite", "ste")).Distinct().Count();                                         
  
  public int DistinctCities => HCOs.Where(hco => !hco.IsCityBlank)
                                   .Select(hco => hco.City.ToLower()).Distinct().Count();
  
  public int DistinctStates => HCOs.Where(hco => !hco.IsStateBlank)
                                   .Select(hco => hco.State.ToLower()).Distinct().Count();
  
  public int DistinctZips => HCOs.Where(hco => !hco.IsZipBlank)
                                 .Select(hco => hco.Zip.Substring(0, hco.Zip.Length <= 5 
                                                                      ? hco.Zip.Length 
                                                                      : hco.Zip.Length - 5)).Distinct().Count();
  
  public List<HCO> HCOs { get; set; }
  
  #region R&D CODE: COMMENTED OUT
  //
  //public int DistinctSources            { get; set; }
  //public int DistinctOrganizationTypes  { get; set; }
  //public int DistinctOrganizationNames  { get; set; }
  //
  //public int DistinctAddessLine1s       { get; set; }
  //public int DistinctAddessLine2s       { get; set; }
  //public int DistinctCities             { get; set; }
  //public int DistinctStates             { get; set; }
  //public int DistinctZips               { get; set; }
  //
  //public string Score => string.Concat(new[] { 
  //                                             distinct_source_count_value
  //                                            ,distinct_organization_type_count_value
  //                                            ,distinct_organization_name_count_value
  //                                            ,distinct_address_line_1_count_value
  //                                            ,distinct_address_line_2_count_value
  //                                            ,distinct_cities_count_value
  //                                            ,distinct_states_count_value
  //                                            ,distinct_zips_count_value
  //                                           });
  //
  //private string distinct_source_count_value            => DistinctSources            == 0 || DistinctSources           == 1 ? "0" : "1";
  //private string distinct_organization_type_count_value => DistinctOrganizationTypes  == 0 || DistinctOrganizationTypes == 1 ? "0" : "1";
  //private string distinct_organization_name_count_value => DistinctOrganizationNames  == 0 || DistinctOrganizationNames == 1 ? "0" : "1";
  //private string distinct_address_line_1_count_value    => DistinctAddessLine1s       == 0 || DistinctAddessLine1s      == 1 ? "0" : "1";
  //private string distinct_address_line_2_count_value    => DistinctAddessLine2s       == 0 || DistinctAddessLine2s      == 1 ? "0" : "1";
  //private string distinct_cities_count_value            => DistinctCities             == 0 || DistinctCities            == 1 ? "0" : "1";
  //private string distinct_states_count_value            => DistinctStates             == 0 || DistinctStates            == 1 ? "0" : "1";
  //private string distinct_zips_count_value              => DistinctZips               == 0 || DistinctZips              == 1 ? "0" : "1";
  //
  #endregion
}

public class HCO
{
  public HCO() { }
  public HCO(LINQPad.User.RV_DECUPE_HCO_RESYNC_PRD record)
  {
    var index_of_hash = record.CODES.IndexOf('#');
    var index_of_semicollon = record.CODES.IndexOf(':');

    Source = record.CODES.Substring( index_of_hash + 1                         //Adding 1, removes the "#'
                                    ,index_of_semicollon - 1 - index_of_hash); //Subtracting 1, removes the ':'

    OrganizationType = record.SPRINGWORKS_HCO_ORGANIZATIONTYPE;
    OrganizationName = record.SPRINGWORKS_HCO_ORGANIZATIONNAME;

    AddressLine1 = record.SPRINGWORKS_HCO_ADDRESSLINE1;
    AddressLine2 = record.SPRINGWORKS_HCO_ADDRESSLINE2;
    City = record.SPRINGWORKS_HCO_CITY;
    State = record.SPRINGWORKS_HCO_STATE;
    Zip = record.SPRINGWORKS_HCO_ZIP;

    RawData = record;
  }
  
  public int FullHashCode     => GetHashCode();
  public int PartialHashCode  => GetPartialHashCode();

  public string Source { get; set; }
  public string OrganizationType { get; set; }
  public string OrganizationName { get; set; }

  public string AddressLine1 { get; set; }
  public string AddressLine2 { get; set; }
  public string City { get; set; }
  public string State { get; set; }
  public string Zip { get; set; }

  public bool IsOrganizationTypeBlank => string.IsNullOrWhiteSpace(OrganizationType);
  public bool IsOrganizationNameBlank => string.IsNullOrWhiteSpace(OrganizationName);
  
  public bool IsAddressLine1Blank     => string.IsNullOrWhiteSpace(AddressLine1);
  public bool IsAddressLine2Blank     => string.IsNullOrWhiteSpace(AddressLine2);
  public bool IsCityBlank             => string.IsNullOrWhiteSpace(City);
  public bool IsStateBlank            => string.IsNullOrWhiteSpace(State);
  public bool IsZipBlank              => string.IsNullOrWhiteSpace(Zip);

  public LINQPad.User.RV_DECUPE_HCO_RESYNC_PRD RawData { get; set; }
  
  //NOTE: "possibly" used to calculate "exact/same" entities ... 
  public override int GetHashCode() 
  {
    unchecked
    {
      int hash = 17;
      
      hash = hash * 23 + (OrganizationType?.GetHashCode() ?? 0);
      hash = hash * 23 + (OrganizationName?.GetHashCode() ?? 0);      
      
      hash = hash * 23 + (AddressLine1?.GetHashCode() ?? 0);
      hash = hash * 23 + (AddressLine2?.GetHashCode() ?? 0);
      hash = hash * 23 + (City?.GetHashCode()         ?? 0);
      hash = hash * 23 + (State?.GetHashCode()        ?? 0);

      //var zip = Zip.Substring(0, Zip.Length <= 5 ? Zip.Length : Zip.Length - 5);
      //hash = hash * 23 + (zip?.GetHashCode() ?? 0);
      hash = hash * 23 + (Zip?.GetHashCode()          ?? 0);

      return hash;
    }
  }
  
  public int GetPartialHashCode() 
  {
    unchecked
    {
      int hash = 17;
      
      //NOTE: There's an issue doing this, it doesn't take into acocunt the different OrganizationTypes.
      //      The intent was to compare HCO's with an OrganizationType to those which do not and commenting
      //      this out, DOES NOT allow for that; it would simply ignores this field all together.
      //if(!string.IsNullOrWhiteSpace(OrganizationType))
      //  hash = hash * 23 + (OrganizationType?.ToLower().GetHashCode() ?? 0);
      
      if(!string.IsNullOrWhiteSpace(OrganizationName))
        hash = hash * 23 + (OrganizationName?.ToLower().GetHashCode() ?? 0);      
      
      if(!string.IsNullOrWhiteSpace(AddressLine1))
        hash = hash * 23 + (AddressLine1?.ToLower().GetHashCode() ?? 0);
      
      if(!string.IsNullOrWhiteSpace(AddressLine2))
        hash = hash * 23 + (AddressLine2?.ToLower()
                                         //.Replace("ste", "suite")
                                         //.Replace("flr", "floor")
                                         //.Replace("&", "and")
                                         .GetHashCode() ?? 0);
        
      if(!string.IsNullOrWhiteSpace(City))  
        hash = hash * 23 + (City?.ToLower().GetHashCode() ?? 0);
        
      if(!string.IsNullOrWhiteSpace(State))
        hash = hash * 23 + (State?.ToLower().GetHashCode() ?? 0);

      if (!string.IsNullOrWhiteSpace(Zip))
      {
        //hash = hash * 23 + (Zip?.GetHashCode() ?? 0);
        var zip = Zip.Substring(0, Zip.Length <= 5 ? Zip.Length : Zip.Length - 5);
        hash = hash * 23 + (zip?.GetHashCode() ?? 0);
      }
      
      return hash;
    }
  }
}

public record RV_DECUPE_HCO_RESYNC_PRD 
{
  public string CODES                                 { get; set; }
  public string CREATEDDATE                           { get; set; }
  public string DISCOVERYDATE                         { get; set; }
  public string ENTITYTYPE                            { get; set; }
  public string ID                                    { get; set; }
  public string ISDELETED                             { get; set; }
  public string MODIFIEDDATE                          { get; set; }
  public string ORIGINENTITYCODE                      { get; set; }
  public string PERSISTHASH                           { get; set; }
  public string REVISION                              { get; set; }
  public string SORTDATE                              { get; set; }
                                                      
  public string SPRINGWORKS_HCO__340BFLAG             { get; set; }
  public string SPRINGWORKS_HCO__340BHOSPITALTYPE     { get; set; }
  public string SPRINGWORKS_HCO__340BPROGRAM          { get; set; }
  public string SPRINGWORKS_HCO_ACCOUNTOWNER          { get; set; }
  public string SPRINGWORKS_HCO_ACCOUNTSTATUS         { get; set; }
  public string SPRINGWORKS_HCO_ADDRESSID             { get; set; }
  public string SPRINGWORKS_HCO_ADDRESSLINE1          { get; set; }
  public string SPRINGWORKS_HCO_ADDRESSLINE2          { get; set; }
  public string SPRINGWORKS_HCO_ADDRESSPHONE          { get; set; }
  public string SPRINGWORKS_HCO_ADDRESSTYPE           { get; set; }
  public string SPRINGWORKS_HCO_AFFILIATIONID         { get; set; }
  public string SPRINGWORKS_HCO_ALTERNATEACCOUNTNAME  { get; set; }
  public string SPRINGWORKS_HCO_CITY                  { get; set; }
  public string SPRINGWORKS_HCO_COTDESCRIPTION        { get; set; }
  public string SPRINGWORKS_HCO_COUNTRY               { get; set; }
  public string SPRINGWORKS_HCO_CRMEMAIL              { get; set; }
  public string SPRINGWORKS_HCO_DEA                   { get; set; }
  public string SPRINGWORKS_HCO_EMAIL                 { get; set; }
  public string SPRINGWORKS_HCO_FAX                   { get; set; }
  public string SPRINGWORKS_HCO_HINNUMBER             { get; set; }
  public string SPRINGWORKS_HCO_LASTSENTDATETOVEEVA   { get; set; }
  public string SPRINGWORKS_HCO_LATITUDE              { get; set; }
  public string SPRINGWORKS_HCO_LONGITUDE             { get; set; }
  public string SPRINGWORKS_HCO_MDMID                 { get; set; }
  public string SPRINGWORKS_HCO_NPINUMBER             { get; set; }
  public string SPRINGWORKS_HCO_NUMBEROFBEDS          { get; set; }
  public string SPRINGWORKS_HCO_NUMBEROFMEMBERS       { get; set; }
  public string SPRINGWORKS_HCO_ORGANIZATIONNAME      { get; set; }
  public string SPRINGWORKS_HCO_ORGANIZATIONSUBTYPE   { get; set; }
  public string SPRINGWORKS_HCO_ORGANIZATIONTYPE      { get; set; }
  public string SPRINGWORKS_HCO_PHONE                 { get; set; }
  public string SPRINGWORKS_HCO_PRIMARYADDRESSFLAG    { get; set; }
  public string SPRINGWORKS_HCO_SENDTOVEEVA           { get; set; }
  public string SPRINGWORKS_HCO_SOURCE                { get; set; }
  public string SPRINGWORKS_HCO_SOURCEID              { get; set; }
  public string SPRINGWORKS_HCO_SPECIALTY1            { get; set; }
  public string SPRINGWORKS_HCO_SPECIALTY1CODE        { get; set; }
  public string SPRINGWORKS_HCO_SPECIALTY2            { get; set; }
  public string SPRINGWORKS_HCO_STATE                 { get; set; }
  public string SPRINGWORKS_HCO_VERIFIEDEMAIL         { get; set; }
  public string SPRINGWORKS_HCO_WEBSITE               { get; set; }
  public string SPRINGWORKS_HCO_ZIP                   { get; set; }
  public string SPRINGWORKS_HCO_ZIPEXTENSION          { get; set; }
  
  public string ZZ_LAST_CHANGE_TIMESTAMP              { get; set; }
}
