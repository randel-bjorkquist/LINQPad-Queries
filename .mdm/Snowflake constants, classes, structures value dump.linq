<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
  SnowflakeConstants.KeyName.ClientSecret.Dump("SnowflakeConstants.KeyName.ClientSecret"); 
  SnowflakeConstants.EntityType.GpoOverview.Dump("SnowflakeConstants.EntityType.GpoOverview");
  
  
}

// You can define other methods, fields, classes and namespaces here
public class SnowflakeConstants
{
  public struct KeyName
  {
    public const string ClientSecret = nameof(ClientSecret);
    public const string TenantId = nameof(TenantId);
    public const string ClientId = nameof(ClientId);
    public const string BaseUrl = nameof(BaseUrl);
    public const string Warehouse = nameof(Warehouse);
    public const string Schema = nameof(Schema);
    public const string Statement = nameof(Statement);
    public const string Database = nameof(Database);
    public const string Role = nameof(Role);
    public const string SourceName = nameof(SourceName);

    public const string Scope = nameof(Scope);
    public const string SnowflakeEndpointUrl = nameof(SnowflakeEndpointUrl);
    public const string SnowflakeApplicationIdUri = nameof(SnowflakeApplicationIdUri);

    public const string LastFinishedCrawlStartTime = nameof(LastFinishedCrawlStartTime);
    public const string ForceFullCrawl = nameof(ForceFullCrawl);
    public const string RowPerTableLimit = nameof(RowPerTableLimit);
    public const string CronSchedule = nameof(CronSchedule);
  }

  public struct EntityType
  {
    public const string GpoOverview                   = nameof(GpoOverview);
    public const string HospitalAssociatedNpiNumbers  = nameof(HospitalAssociatedNpiNumbers);
    public const string HospitalOverview              = nameof(HospitalOverview);
    public const string PhysicianGroupsOverview       = nameof(PhysicianGroupsOverview);

    public const string PhysiciansKeyOpinionLeaders   = nameof(PhysiciansKeyOpinionLeaders);
    public const string PhysiciansOverview            = nameof(PhysiciansOverview);
    public const string PhysiciansPracticeLocations   = nameof(PhysiciansPracticeLocations);
    public const string PhysiciansStateLicensures     = nameof(PhysiciansStateLicensures);
  }
}

//namespace CluedIn.Crawling.Snowflake.DefinitiveHealthcare.Core.Models
//{
  public class GpoOverview
  {
    [JsonProperty("HQ_ADDRESS")]
    public string HqAddress { get; set; }
    [JsonProperty("NET_PATIENT_REVENUE")]
    public string NetPatientRevenue { get; set; }
    [JsonProperty("HQ_ZIP_CODE")]
    public string HqZipCode { get; set; }
    [JsonProperty("TOTAL_OPERATING_EXPENSES")]
    public string TotalOperatingExpenses { get; set; }
    [JsonProperty("ZZ_FILE_NAME")]
    public string ZzFileName { get; set; }
    [JsonProperty("ZZ_ROW_ID")]
    public string ZzRowId { get; set; }
    [JsonProperty("HQ_PHONE")]
    public string HqPhone { get; set; }
    [JsonProperty("HQ_STATE")]
    public string HqState { get; set; }
    [JsonProperty("CBSA_POPULATION_GROWTH_MOST_RECENT")]
    public string CbsaPopulationGrowthMostRecent { get; set; }
    [JsonProperty("HQ_CITY")]
    public string HqCity { get; set; }
    [JsonProperty("ZZ_RECORD_STATUS")]
    public string ZzRecordStatus { get; set; }
    [JsonProperty("HOSPITAL_ID")]
    public string HospitalId { get; set; }
    [JsonProperty("DHC_PROFILE_LINK")]
    public string DhcProfileLink { get; set; }
    [JsonProperty("REVENUES")]
    public string Revenues { get; set; }
    [JsonProperty("FIRM_TYPE")]
    public string FirmType { get; set; }
    [JsonProperty("NUMBER_MEMBER_HOSPITAL_BEDS")]
    public string NumberMemberHospitalBeds { get; set; }
    [JsonProperty("HQ_LONGITUDE")]
    public string HqLongitude { get; set; }
    [JsonProperty("HQ_REGION")]
    public string HqRegion { get; set; }
    [JsonProperty("CBSA_CODE")]
    public string CbsaCode { get; set; }
    [JsonProperty("HQ_ADDRESS1")]
    public string HqAddress1 { get; set; }
    [JsonProperty("NUMBER_DISCHARGES")]
    public string NumberDischarges { get; set; }
    [JsonProperty("ZZ_LAST_CHANGE_TIMESTAMP")]
    public DateTime? ZzLastChangeTimestamp { get; set; }
    [JsonProperty("HQ_COUNTY")]
    public string HqCounty { get; set; }
    [JsonProperty("COMPANY_STATUS")]
    public string CompanyStatus { get; set; }
    [JsonProperty("CBSA_POPULATION_EST_MOST_RECENT")]
    public string CbsaPopulationEstMostRecent { get; set; }
    [JsonProperty("HQ_LATITUDE")]
    public string HqLatitude { get; set; }
    [JsonProperty("WEBSITE")]
    public string Website { get; set; }
    [JsonProperty("HOSPITAL_NAME")]
    public string HospitalName { get; set; }
    [JsonProperty("NUMBER_HOSPITAL_MEMBERS")]
    public string NumberHospitalMembers { get; set; }
    [JsonProperty("ZZ_FILE_LAST_CHANGE_TIMESTAMP")]
    public DateTime? ZzFileLastChangeTimestamp { get; set; }
    [JsonProperty("ZZ_ID")]
    public string ZzId { get; set; }
    [JsonProperty("ZZ_INGESTION_ID")]
    public string ZzIngestionId { get; set; }
  }

  public class HospitalAssociatedNpiNumbers
  {
    [JsonProperty("HQ_ADDRESS1")]
    public string HqAddress1 { get; set; }

    [JsonProperty("HQ_ZIP_CODE")]
    public string HqZipCode { get; set; }

    [JsonProperty("HQ_ADDRESS")]
    public string HqAddress { get; set; }

    [JsonProperty("ZZ_INGESTION_ID")]
    public string ZzIngestionId { get; set; }

    [JsonProperty("ZZ_ID")]
    public string ZzId { get; set; }

    [JsonProperty("HOSPITAL_NAME")]
    public string HospitalName { get; set; }

    [JsonProperty("ORGANIZATION_NAME")]
    public string OrganizationName { get; set; }

    [JsonProperty("HOSPITAL_ID")]
    public string HospitalId { get; set; }

    [JsonProperty("ZZ_LAST_CHANGE_TIMESTAMP")]
    public DateTime? ZzLastChangeTimestamp { get; set; }

    [JsonProperty("NPI_NUMBER")]
    public string NpiNumber { get; set; }

    [JsonProperty("OTHER_ORGANIZATION_NAME")]
    public string OtherOrganizationName { get; set; }

    [JsonProperty("PRIMARY_TAXONOMY")]
    public string PrimaryTaxonomy { get; set; }

    [JsonProperty("ZZ_FILE_NAME")]
    public string ZzFileName { get; set; }

    [JsonProperty("ZZ_ROW_ID")]
    public string ZzRowId { get; set; }

    [JsonProperty("PRIMARY_TAXONOMY_CODE")]
    public string PrimaryTaxonomyCode { get; set; }

    [JsonProperty("ZZ_FILE_LAST_CHANGE_TIMESTAMP")]
    public DateTime? ZzFileLastChangeTimestamp { get; set; }

    [JsonProperty("ZZ_RECORD_STATUS")]
    public string ZzRecordStatus { get; set; }

    [JsonProperty("HQ_PHONE")]
    public string HqPhone { get; set; }

    [JsonProperty("HQ_STATE")]
    public string HqState { get; set; }

    [JsonProperty("HQ_CITY")]
    public string HqCity { get; set; }
  }

  public class HospitalOverview
  {
    [JsonProperty("ZZ_ROW_ID")]
    public string ZzRowId { get; set; }

    [JsonProperty("IDN_INTEGRATION_LEVEL")]
    public string IdnIntegrationLevel { get; set; }

    [JsonProperty("POS_MEDICAL_SCHOOL_AFFILIATION")]
    public string PosMedicalSchoolAffiliation { get; set; }

    [JsonProperty("ZZ_FILE_NAME")]
    public string ZzFileName { get; set; }

    [JsonProperty("NETWORK_PARENT_OWNERSHIP")]
    public string NetworkParentOwnership { get; set; }

    [JsonProperty("NETWORK_OWNERSHIP")]
    public string NetworkOwnership { get; set; }

    [JsonProperty("NETWORK_FIRM_TYPE")]
    public string NetworkFirmType { get; set; }

    [JsonProperty("MEDICARE_ADMINISTRATIVE_CONTRACTOR")]
    public string MedicareAdministrativeContractor { get; set; }

    [JsonProperty("SF_PARENT_ACCOUNT_ID")]
    public string SfParentAccountId { get; set; }

    [JsonProperty("HOSPITAL_COMPARE_OVERALL_RATING")]
    public string HospitalCompareOverallRating { get; set; }

    [JsonProperty("HIE_AFFILIATIONS")]
    public string HieAffiliations { get; set; }

    [JsonProperty("HQ_ZIP_CODE")]
    public string HqZipCode { get; set; }

    [JsonProperty("MEDICAL_SCHOOL_AFFILIATES")]
    public string MedicalSchoolAffiliates { get; set; }

    [JsonProperty("PRIMARY_GPO_NAME")]
    public string PrimaryGpoName { get; set; }

    [JsonProperty("WEBSITE")]
    public string Website { get; set; }

    [JsonProperty("NPI_NUMBER")]
    public string NpiNumber { get; set; }

    [JsonProperty("GEOGRAPHIC_CLASSIFICATION")]
    public string GeographicClassification { get; set; }

    [JsonProperty("HQ_LATITUDE")]
    public string HqLatitude { get; set; }

    [JsonProperty("HOSPITAL_OWNERSHIP")]
    public string HospitalOwnership { get; set; }

    [JsonProperty("HQ_REGION")]
    public string HqRegion { get; set; }

    [JsonProperty("ZZ_ID")]
    public string ZzId { get; set; }

    [JsonProperty("PRIMARY_GPO_ID")]
    public string PrimaryGpoId { get; set; }

    [JsonProperty("NETWORK_ID")]
    public string NetworkId { get; set; }

    [JsonProperty("HQ_LONGITUDE")]
    public string HqLongitude { get; set; }

    [JsonProperty("HOSPITAL_TYPE")]
    public string HospitalType { get; set; }

    [JsonProperty("PROGRAM_340B_ID")]
    public string Program340BId { get; set; }

    [JsonProperty("HQ_ADDRESS1")]
    public string HqAddress1 { get; set; }

    [JsonProperty("CBSA_POPULATION_GROWTH_MOST_RECENT")]
    public string CbsaPopulationGrowthMostRecent { get; set; }

    [JsonProperty("ZZ_RECORD_STATUS")]
    public string ZzRecordStatus { get; set; }

    [JsonProperty("HOSPITAL_NAME")]
    public string HospitalName { get; set; }

    [JsonProperty("ZZ_FILE_LAST_CHANGE_TIMESTAMP")]
    public DateTime? ZzFileLastChangeTimestamp { get; set; }

    [JsonProperty("ZZ_LAST_CHANGE_TIMESTAMP")]
    public DateTime? ZzLastChangeTimestamp { get; set; }

    [JsonProperty("HQ_COUNTY")]
    public string HqCounty { get; set; }

    [JsonProperty("FINANCIAL_DATA_DATE")]
    public string FinancialDataDate { get; set; }

    [JsonProperty("ZZ_INGESTION_ID")]
    public string ZzIngestionId { get; set; }

    [JsonProperty("MARKET_CONCENTRATION_INDEX")]
    public string MarketConcentrationIndex { get; set; }

//        [JsonProperty("_340B_HOSPITAL_TYPE")]
//        public string 340BHospitalType { get; set; }

    [JsonProperty("CBSA_CODE")]
    public string CbsaCode { get; set; }

    [JsonProperty("HQ_STATE")]
    public string HqState { get; set; }

    [JsonProperty("HQ_ADDRESS")]
    public string HqAddress { get; set; }

    [JsonProperty("NETWORK_NAME")]
    public string NetworkName { get; set; }

    [JsonProperty("COMPANY_STATUS")]
    public string CompanyStatus { get; set; }

    [JsonProperty("PROVIDER_NUMBER")]
    public string ProviderNumber { get; set; }

    [JsonProperty("ACCREDITATION_AGENCY")]
    public string AccreditationAgency { get; set; }

    [JsonProperty("CBSA_POPULATION_EST_MOST_RECENT")]
    public string CbsaPopulationEstMostRecent { get; set; }

    [JsonProperty("IDN_FINANCIAL_DATA_REPORTING_STATUS")]
    public string IdnFinancialDataReportingStatus { get; set; }

    [JsonProperty("HQ_CITY")]
    public string HqCity { get; set; }

    [JsonProperty("ACO_AFFILIATIONS")]
    public string AcoAffiliations { get; set; }

    [JsonProperty("HOSPITAL_ID")]
    public string HospitalId { get; set; }

    [JsonProperty("PUBLIC_CORPORATION")]
    public string PublicCorporation { get; set; }

    [JsonProperty("TAX_ID")]
    public string TaxId { get; set; }

    [JsonProperty("NETWORK_PARENT_NAME")]
    public string NetworkParentName { get; set; }

    [JsonProperty("HQ_PHONE")]
    public string HqPhone { get; set; }

    [JsonProperty("GPO_AFFILIATIONS")]
    public string GpoAffiliations { get; set; }

    [JsonProperty("NETWORK_PARENT_FIRM_TYPE")]
    public string NetworkParentFirmType { get; set; }

    [JsonProperty("DHC_PROFILE_LINK")]
    public string DhcProfileLink { get; set; }

    [JsonProperty("HCAHPS_SUMMARY_STAR_RATING")]
    public string HcahpsSummaryStarRating { get; set; }

    [JsonProperty("STOCK_SYMBOL")]
    public string StockSymbol { get; set; }

    [JsonProperty("FIRM_TYPE")]
    public string FirmType { get; set; }

    [JsonProperty("SF_PARENT_ACCOUNT_NAME")]
    public string SfParentAccountName { get; set; }

    [JsonProperty("NETWORK_PARENT_ID")]
    public string NetworkParentId { get; set; }

  }

  public class PhysicianGroupsOverview
  {
    [JsonProperty("CBSA_CODE")]
    public string CbsaCode { get; set; }

    [JsonProperty("NUMBER_PROCEDURES")]
    public string NumberProcedures { get; set; }

    [JsonProperty("CBSA_POPULATION_GROWTH_MOST_RECENT")]
    public string CbsaPopulationGrowthMostRecent { get; set; }

    [JsonProperty("CIN_AFFILIATIONS")]
    public string CinAffiliations { get; set; }

    [JsonProperty("HOSPITAL_PARENT_NAME")]
    public string HospitalParentName { get; set; }

    [JsonProperty("SF_PARENT_ACCOUNT_ID")]
    public string SfParentAccountId { get; set; }

    [JsonProperty("ZZ_FILE_LAST_CHANGE_TIMESTAMP")]
    public DateTime? ZzFileLastChangeTimestamp { get; set; }

    [JsonProperty("GPO_AFFILIATIONS")]
    public string GpoAffiliations { get; set; }

    [JsonProperty("NPI_NUMBER")]
    public string NpiNumber { get; set; }

    [JsonProperty("MANAGEMENT_SERVICE_ORGANIZATION")]
    public string ManagementServiceOrganization { get; set; }

    [JsonProperty("CPC_PLUS")]
    public string CpcPlus { get; set; }

    [JsonProperty("MEDICARE_PMTS")]
    public string MedicarePmts { get; set; }

    [JsonProperty("NETWORK_FIRM_TYPE")]
    public string NetworkFirmType { get; set; }

    [JsonProperty("ZZ_ID")]
    public string ZzId { get; set; }

    [JsonProperty("NETWORK_ID")]
    public string NetworkId { get; set; }

    [JsonProperty("DHC_PROFILE_LINK")]
    public string DhcProfileLink { get; set; }

    [JsonProperty("NETWORK_PARENT_NAME")]
    public string NetworkParentName { get; set; }

    [JsonProperty("HQ_ADDRESS1")]
    public string HqAddress1 { get; set; }

    [JsonProperty("HQ_ZIP_CODE")]
    public string HqZipCode { get; set; }

    [JsonProperty("HQ_LONGITUDE")]
    public string HqLongitude { get; set; }

    [JsonProperty("OWNED_LEASED_REAL_ESTATE")]
    public string OwnedLeasedRealEstate { get; set; }

    [JsonProperty("WEBSITE")]
    public string Website { get; set; }

    [JsonProperty("ONCOLOGY_CARE_LABEL")]
    public string OncologyCareLabel { get; set; }

    [JsonProperty("HQ_STATE")]
    public string HqState { get; set; }

    [JsonProperty("BPCI_MODEL")]
    public string BpciModel { get; set; }

    [JsonProperty("HOSPITAL_PARENT_ID")]
    public string HospitalParentId { get; set; }

    [JsonProperty("ZZ_INGESTION_ID")]
    public string ZzIngestionId { get; set; }

    [JsonProperty("HQ_LATITUDE")]
    public string HqLatitude { get; set; }

    [JsonProperty("HQ_COUNTY")]
    public string HqCounty { get; set; }

    [JsonProperty("PG_TYPE")]
    public string PgType { get; set; }

    [JsonProperty("NETWORK_PARENT_ID")]
    public string NetworkParentId { get; set; }

    [JsonProperty("HQ_ADDRESS")]
    public string HqAddress { get; set; }

    [JsonProperty("MEDICARE_ALLOWED_AMT")]
    public string MedicareAllowedAmt { get; set; }

    [JsonProperty("ACO_AFFILIATIONS")]
    public string AcoAffiliations { get; set; }

    [JsonProperty("NETWORK_NAME")]
    public string NetworkName { get; set; }

    [JsonProperty("ZZ_LAST_CHANGE_TIMESTAMP")]
    public DateTime? ZzLastChangeTimestamp { get; set; }

    [JsonProperty("MAIN_SPECIALTY")]
    public string MainSpecialty { get; set; }

    [JsonProperty("MAIN_SPECIALTY_GROUP")]
    public string MainSpecialtyGroup { get; set; }

    [JsonProperty("FIRM_TYPE")]
    public string FirmType { get; set; }

    [JsonProperty("HIE_AFFILIATIONS")]
    public string HieAffiliations { get; set; }

    [JsonProperty("NUMBER_GROUP_PRACTICE_MEMBERS")]
    public string NumberGroupPracticeMembers { get; set; }

    [JsonProperty("ZZ_ROW_ID")]
    public string ZzRowId { get; set; }

    [JsonProperty("HOSPITAL_NAME")]
    public string HospitalName { get; set; }

    [JsonProperty("NETWORK_PARENT_FIRM_TYPE")]
    public string NetworkParentFirmType { get; set; }

    [JsonProperty("HOSPITAL_ID")]
    public string HospitalId { get; set; }

    [JsonProperty("OTHER_SPECIALTIES")]
    public string OtherSpecialties { get; set; }

    [JsonProperty("COMPANY_STATUS")]
    public string CompanyStatus { get; set; }

    [JsonProperty("CBSA_POPULATION_EST_MOST_RECENT")]
    public string CbsaPopulationEstMostRecent { get; set; }

    [JsonProperty("HQ_CITY")]
    public string HqCity { get; set; }

    [JsonProperty("NUMBER_PHYSICIANS_PG")]
    public string NumberPhysiciansPg { get; set; }

    [JsonProperty("HQ_PHONE")]
    public string HqPhone { get; set; }

    [JsonProperty("MEDICARE_CHARGES")]
    public string MedicareCharges { get; set; }

    [JsonProperty("GROUP_PRACTICE_PAC_ID")]
    public string GroupPracticePacId { get; set; }

    [JsonProperty("SF_PARENT_ACCOUNT_NAME")]
    public string SfParentAccountName { get; set; }

    [JsonProperty("ZZ_FILE_NAME")]
    public string ZzFileName { get; set; }

    [JsonProperty("HQ_REGION")]
    public string HqRegion { get; set; }

    [JsonProperty("ZZ_RECORD_STATUS")]
    public string ZzRecordStatus { get; set; }
  }

  public class PhysiciansKeyOpinionLeaders
  {
    [JsonProperty("NPI")]
    public string Npi { get; set; }

    [JsonProperty("KOL_THERAPY_AREA")]
    public string KolTherapyArea { get; set; }

    [JsonProperty("FIRST_NAME")]
    public string FirstName { get; set; }

    [JsonProperty("SPECIALTY_PRIMARY")]
    public string SpecialtyPrimary { get; set; }

    [JsonProperty("ZZ_ROW_ID")]
    public string ZzRowId { get; set; }

    [JsonProperty("ZZ_FILE_NAME")]
    public string ZzFileName { get; set; }

    [JsonProperty("ZZ_RECORD_STATUS")]
    public string ZzRecordStatus { get; set; }

    [JsonProperty("PRIMARY_KOL_INDICATION_FLAG")]
    public string PrimaryKolIndicationFlag { get; set; }

    [JsonProperty("ZZ_FILE_LAST_CHANGE_TIMESTAMP")]
    public DateTime? ZzFileLastChangeTimestamp { get; set; }

    [JsonProperty("ZZ_ID")]
    public string ZzId { get; set; }

    [JsonProperty("ZZ_LAST_CHANGE_TIMESTAMP")]
    public DateTime? ZzLastChangeTimestamp { get; set; }

    [JsonProperty("ZZ_INGESTION_ID")]
    public string ZzIngestionId { get; set; }

    [JsonProperty("LAST_NAME")]
    public string LastName { get; set; }

    [JsonProperty("KOL_INDICATION")]
    public string KolIndication { get; set; }
  }

  public class PhysiciansOverview
  {
    [JsonProperty("HQ_COUNTY")]
    public string HqCounty { get; set; }

    [JsonProperty("KEY_OPINION_LEADER_FLAG")]
    public string KeyOpinionLeaderFlag { get; set; }

    [JsonProperty("PRIMARY_AFFILIATION_NETWORK_PARENT_NAME")]
    public string PrimaryAffiliationNetworkParentName { get; set; }

    [JsonProperty("PRIMARY_KOL_INDICATION")]
    public string PrimaryKolIndication { get; set; }

    [JsonProperty("PRIMARY_AFFILIATION_HOSPITAL_NAME")]
    public string PrimaryAffiliationHospitalName { get; set; }

    [JsonProperty("SPECIALTY_SECONDARY")]
    public string SpecialtySecondary { get; set; }

    [JsonProperty("AGE")]
    public string Age { get; set; }

    [JsonProperty("HQ_LONGITUDE")]
    public string HqLongitude { get; set; }

    [JsonProperty("PREDICTED_EMPLOYMENT_ID")]
    public string PredictedEmploymentId { get; set; }

    [JsonProperty("PRIMARY_TAXONOMY")]
    public string PrimaryTaxonomy { get; set; }

    [JsonProperty("HQ_ADDRESS")]
    public string HqAddress { get; set; }

    [JsonProperty("ZZ_LAST_CHANGE_TIMESTAMP")]
    public DateTime? ZzLastChangeTimestamp { get; set; }

    [JsonProperty("CREDENTIAL")]
    public string Credential { get; set; }

    [JsonProperty("TOTAL_DRUG_COST")]
    public string TotalDrugCost { get; set; }

    [JsonProperty("ZZ_ROW_ID")]
    public string ZzRowId { get; set; }

    [JsonProperty("GRADUATION_YEAR")]
    public string GraduationYear { get; set; }

    [JsonProperty("PARTICIPATED_IN_CLINICAL_TRIALS")]
    public string ParticipatedInClinicalTrials { get; set; }

    [JsonProperty("TOTAL_DRUG_DAY_SUPPLY")]
    public string TotalDrugDaySupply { get; set; }

    [JsonProperty("PRIMARY_AFFILIATION_NETWORK_ID")]
    public string PrimaryAffiliationNetworkId { get; set; }

    [JsonProperty("LAST_NAME")]
    public string LastName { get; set; }

    [JsonProperty("DO_NOT_CALL_PHONE")]
    public string DoNotCallPhone { get; set; }

    [JsonProperty("TOTAL_DRUG_PRESCRIPTION_COUNT")]
    public string TotalDrugPrescriptionCount { get; set; }

    [JsonProperty("MEDICARE_CHARGES")]
    public string MedicareCharges { get; set; }

    [JsonProperty("HQ_REGION")]
    public string HqRegion { get; set; }

    [JsonProperty("PREDICTED_EMPLOYMENT_FIRM_TYPE")]
    public string PredictedEmploymentFirmType { get; set; }

    [JsonProperty("NUMBER_OF_HCPCS_CPT")]
    public string NumberOfHcpcsCpt { get; set; }

    [JsonProperty("ZZ_RECORD_STATUS")]
    public string ZzRecordStatus { get; set; }

    [JsonProperty("TELEHEALTH_SCORE")]
    public string TelehealthScore { get; set; }

    [JsonProperty("ZZ_ID")]
    public string ZzId { get; set; }

    [JsonProperty("SPECIALTY_PRIMARY_CLAIMS_BASED")]
    public string SpecialtyPrimaryClaimsBased { get; set; }

    [JsonProperty("MEDICARE_PMTS")]
    public string MedicarePmts { get; set; }

    [JsonProperty("PRIMARY_PRACTICE_LOCATION_NAME")]
    public string PrimaryPracticeLocationName { get; set; }

    [JsonProperty("SPECIALTY_PRIMARY_GROUP")]
    public string SpecialtyPrimaryGroup { get; set; }

    [JsonProperty("ROLE_NAME")]
    public string RoleName { get; set; }

    [JsonProperty("ZZ_FILE_LAST_CHANGE_TIMESTAMP")]
    public DateTime? ZzFileLastChangeTimestamp { get; set; }

    [JsonProperty("MEDICARE_BENEFICIARIES")]
    public string MedicareBeneficiaries { get; set; }

    [JsonProperty("PARTICIPATES_IN_MIPS")]
    public string ParticipatesInMips { get; set; }

    [JsonProperty("SPECIALTY_PRIMARY")]
    public string SpecialtyPrimary { get; set; }

    [JsonProperty("CBSA_CODE")]
    public string CbsaCode { get; set; }

    [JsonProperty("PRIMARY_AFFILIATION_HOSPITAL_ID")]
    public string PrimaryAffiliationHospitalId { get; set; }

    [JsonProperty("PRIMARY_PRACTICE_LOCATION_HOSPITAL_ID")]
    public string PrimaryPracticeLocationHospitalId { get; set; }

    [JsonProperty("SPECIALTY_PRIMARY_CATEGORY_CLAIMS_BASED")]
    public string SpecialtyPrimaryCategoryClaimsBased { get; set; }

    [JsonProperty("MEDICARE_PROCEDURES")]
    public string MedicareProcedures { get; set; }

    [JsonProperty("ZZ_INGESTION_ID")]
    public string ZzIngestionId { get; set; }

    [JsonProperty("HQ_FAX_NUMBER")]
    public string HqFaxNumber { get; set; }

    [JsonProperty("NPI")]
    public string Npi { get; set; }

    [JsonProperty("CBSA_POPULATION_GROWTH_MOST_RECENT")]
    public string CbsaPopulationGrowthMostRecent { get; set; }

    [JsonProperty("DHC_PROFILE_LINK")]
    public string DhcProfileLink { get; set; }

    [JsonProperty("HQ_ZIP_CODE")]
    public string HqZipCode { get; set; }

    [JsonProperty("HQ_ADDRESS1")]
    public string HqAddress1 { get; set; }

    [JsonProperty("MEDICAL_SCHOOL_NAME")]
    public string MedicalSchoolName { get; set; }

    [JsonProperty("GENDER")]
    public string Gender { get; set; }

    [JsonProperty("PAC_ID")]
    public string PacId { get; set; }

    [JsonProperty("SECONDARY_KOL_INDICATION")]
    public string SecondaryKolIndication { get; set; }

    [JsonProperty("TELEHEALTH_ADOPTION")]
    public string TelehealthAdoption { get; set; }

    [JsonProperty("TOTAL_NUMBER_UNIQUE_DRUGS")]
    public string TotalNumberUniqueDrugs { get; set; }

    [JsonProperty("SUFFIX")]
    public string Suffix { get; set; }

    [JsonProperty("PREDICTED_EMPLOYMENT_NAME")]
    public string PredictedEmploymentName { get; set; }

    [JsonProperty("PRIMARY_PRACTICE_LOCATION_ID")]
    public string PrimaryPracticeLocationId { get; set; }

    [JsonProperty("CBSA_POPULATION_EST_MOST_RECENT")]
    public string CbsaPopulationEstMostRecent { get; set; }

    [JsonProperty("MIDDLE_NAME")]
    public string MiddleName { get; set; }

    [JsonProperty("PRIMARY_AFFILIATION_NETWORK_NAME")]
    public string PrimaryAffiliationNetworkName { get; set; }

    [JsonProperty("PRIMARY_AFFILIATION_NETWORK_PARENT_ID")]
    public string PrimaryAffiliationNetworkParentId { get; set; }

    [JsonProperty("HOSPITALIST_FLAG")]
    public string HospitalistFlag { get; set; }

    [JsonProperty("ACCEPTS_MEDICARE_APPROVED_AMT_IN_FULL")]
    public string AcceptsMedicareApprovedAmtInFull { get; set; }

    [JsonProperty("HQ_CITY")]
    public string HqCity { get; set; }

    [JsonProperty("TOTAL_DRUG_BENEFICIARY_COUNT")]
    public string TotalDrugBeneficiaryCount { get; set; }

    [JsonProperty("ZZ_FILE_NAME")]
    public string ZzFileName { get; set; }

    [JsonProperty("EXECUTIVE_FLAG")]
    public string ExecutiveFlag { get; set; }

    [JsonProperty("HQ_LATITUDE")]
    public string HqLatitude { get; set; }

    [JsonProperty("FIRST_NAME")]
    public string FirstName { get; set; }

    [JsonProperty("HQ_PHONE")]
    public string HqPhone { get; set; }

    [JsonProperty("HQ_STATE")]
    public string HqState { get; set; }

    [JsonProperty("MEDICARE_ALLOWED_AMT")]
    public string MedicareAllowedAmt { get; set; }
  }

  public class PhysiciansPracticeLocations
  {
    [JsonProperty("HQ_CITY")]
    public string HqCity { get; set; }

    [JsonProperty("HQ_PHONE")]
    public string HqPhone { get; set; }

    [JsonProperty("ZZ_FILE_NAME")]
    public string ZzFileName { get; set; }

    [JsonProperty("PRACTICE_LOCATION_HOSPITAL_ID")]
    public string PracticeLocationHospitalId { get; set; }

    [JsonProperty("LAST_NAME")]
    public string LastName { get; set; }

    [JsonProperty("PRIMARY_LOCATION")]
    public string PrimaryLocation { get; set; }

    [JsonProperty("ZZ_LAST_CHANGE_TIMESTAMP")]
    public DateTime? ZzLastChangeTimestamp { get; set; }

    [JsonProperty("HQ_ADDRESS1")]
    public string HqAddress1 { get; set; }

    [JsonProperty("ZZ_ROW_ID")]
    public string ZzRowId { get; set; }

    [JsonProperty("HQ_ZIP_CODE")]
    public string HqZipCode { get; set; }

    [JsonProperty("FIRST_NAME")]
    public string FirstName { get; set; }

    [JsonProperty("ZZ_RECORD_STATUS")]
    public string ZzRecordStatus { get; set; }

    [JsonProperty("ADDRESS_ID")]
    public string AddressId { get; set; }

    [JsonProperty("HQ_FAX_NUMBER")]
    public string HqFaxNumber { get; set; }

    [JsonProperty("ZZ_FILE_LAST_CHANGE_TIMESTAMP")]
    public DateTime? ZzFileLastChangeTimestamp { get; set; }

    [JsonProperty("DO_NOT_CALL_PHONE")]
    public string DoNotCallPhone { get; set; }

    [JsonProperty("HQ_ADDRESS")]
    public string HqAddress { get; set; }

    [JsonProperty("NPI")]
    public string Npi { get; set; }

    [JsonProperty("HQ_LATITUDE")]
    public string HqLatitude { get; set; }

    [JsonProperty("HQ_LONGITUDE")]
    public string HqLongitude { get; set; }

    [JsonProperty("HQ_STATE")]
    public string HqState { get; set; }

    [JsonProperty("LOCATION")]
    public string Location { get; set; }

    [JsonProperty("ZZ_ID")]
    public string ZzId { get; set; }

    [JsonProperty("ZZ_INGESTION_ID")]
    public string ZzIngestionId { get; set; }

    [JsonProperty("LOCATION_ID")]
    public string LocationId { get; set; }
  }

  public class PhysiciansStateLicensures
  {
    [JsonProperty("EXPIRATION_DATE")]
    public string ExpirationDate { get; set; }

    [JsonProperty("ZZ_ROW_ID")]
    public string ZzRowId { get; set; }

    [JsonProperty("PROVIDER_LICENSE_NUMBER")]
    public string ProviderLicenseNumber { get; set; }

    [JsonProperty("ZZ_ID")]
    public string ZzId { get; set; }

    [JsonProperty("ZZ_FILE_NAME")]
    public string ZzFileName { get; set; }

    [JsonProperty("STATUS")]
    public string Status { get; set; }

    [JsonProperty("ZZ_RECORD_STATUS")]
    public string ZzRecordStatus { get; set; }

    [JsonProperty("NPI")]
    public string Npi { get; set; }

    [JsonProperty("FIRST_NAME")]
    public string FirstName { get; set; }

    [JsonProperty("ZZ_LAST_CHANGE_TIMESTAMP")]
    public DateTime? ZzLastChangeTimestamp { get; set; }

    [JsonProperty("LAST_NAME")]
    public string LastName { get; set; }

    [JsonProperty("PROVIDER_LICENSE_STATE")]
    public string ProviderLicenseState { get; set; }

    [JsonProperty("ZZ_FILE_LAST_CHANGE_TIMESTAMP")]
    public DateTime? ZzFileLastChangeTimestamp { get; set; }

    [JsonProperty("ZZ_INGESTION_ID")]
    public string ZzIngestionId { get; set; }

    [JsonProperty("ISSUE_DATE")]
    public string IssueDate { get; set; }
  }
//}