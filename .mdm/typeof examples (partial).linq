<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.ComponentModel</Namespace>
</Query>

void Main()
{
  
  SObjectType<Account>.DisplayName.Dump();
  SObjectType<RecordType>.DisplayName.Dump();
  ((DisplayNameAttribute)typeof(Account).GetCustomAttribute(typeof(DisplayNameAttribute))).Dump();
  
  //Account.GetCustomAttribute(typeof(DisplayNameAttribute)).Dump();
  
  typeof(Account).Dump();
  typeof(Account).GetCustomAttributes().Dump();  
  //typeof(Account).GetCustomAttribute().Dump();
}

public static class SObjectType<T>
{
  public static string DisplayName => ((DisplayNameAttribute)typeof(T).GetCustomAttribute(typeof(DisplayNameAttribute))).DisplayName;
}

[DisplayName("Account")]
public class Account
{
  [JsonIgnore]
  public string PrimaryAddressLine1 { get; set; }

  [JsonIgnore]
  public string PrimaryAddressLine2 { get; set; }

  [JsonIgnore]
  public string PrimaryCity { get; set; }

  [JsonIgnore]
  public string PrimaryState { get; set; }

  [JsonIgnore]
  public string PrimaryZip { get; set; }

  [JsonIgnore]
  public string PrimaryPhone { get; set; }

  [JsonProperty("attributes")]
  public Attributes Attributes { get; set; }

  [JsonProperty("Id")]
  public string Id { get; set; }

  [JsonProperty("IsDeleted")]
  public bool? IsDeleted { get; set; }

  [JsonProperty("MasterRecordId")]
  public object MasterRecordId { get; set; }

  [JsonProperty("Name")]
  public string Name { get; set; }

  [JsonProperty("LastName")]
  public string LastName { get; set; }

  [JsonProperty("FirstName")]
  public string FirstName { get; set; }

  [JsonProperty("Salutation")]
  public string Salutation { get; set; }

  [JsonProperty("RecordTypeId")]
  public string RecordTypeId { get; set; }
  [JsonIgnore]
  public string RecordTypeDescription { get; set; }

  [JsonProperty("ParentId")]
  public string ParentId { get; set; }

  [JsonProperty("Phone")]
  public string Phone { get; set; }

  [JsonProperty("Fax")]
  public object Fax { get; set; }

  [JsonProperty("Website")]
  public Uri Website { get; set; }

  [JsonProperty("PhotoUrl")]
  public object PhotoUrl { get; set; }

  [JsonProperty("NumberOfEmployees")]
  public object NumberOfEmployees { get; set; }

  [JsonProperty("Ownership")]
  public object Ownership { get; set; }

  [JsonProperty("OwnerId")]
  public string OwnerId { get; set; }

  [JsonProperty("CreatedDate")]
  public string CreatedDate { get; set; }

  [JsonProperty("CreatedById")]
  public string CreatedById { get; set; }

  [JsonProperty("LastModifiedDate")]
  public string LastModifiedDate { get; set; }

  [JsonProperty("LastModifiedById")]
  public string LastModifiedById { get; set; }

  [JsonProperty("SystemModstamp")]
  public string SystemModstamp { get; set; }

  [JsonProperty("LastActivityDate")]
  public string LastActivityDate { get; set; }

  [JsonProperty("MayEdit")]
  public bool? MayEdit { get; set; }

  [JsonProperty("IsLocked")]
  public bool? IsLocked { get; set; }

  [JsonProperty("LastViewedDate")]
  public string LastViewedDate { get; set; }

  [JsonProperty("LastReferencedDate")]
  public string LastReferencedDate { get; set; }

  [JsonProperty("IsExcludedFromRealign")]
  public bool? IsExcludedFromRealign { get; set; }

  [JsonProperty("PersonContactId")]
  public string PersonContactId { get; set; }

  [JsonProperty("IsPersonAccount")]
  public bool? IsPersonAccount { get; set; }

  [JsonProperty("PersonMailingStreet")]
  public object PersonMailingStreet { get; set; }

  [JsonProperty("PersonMailingCity")]
  public object PersonMailingCity { get; set; }

  [JsonProperty("PersonMailingState")]
  public object PersonMailingState { get; set; }

  [JsonProperty("PersonMailingPostalCode")]
  public object PersonMailingPostalCode { get; set; }

  [JsonProperty("PersonMailingCountry")]
  public object PersonMailingCountry { get; set; }

  [JsonProperty("PersonMailingLatitude")]
  public object PersonMailingLatitude { get; set; }

  [JsonProperty("PersonMailingLongitude")]
  public object PersonMailingLongitude { get; set; }

  [JsonProperty("PersonMailingGeocodeAccuracy")]
  public object PersonMailingGeocodeAccuracy { get; set; }

  [JsonProperty("PersonMailingAddress")]
  public object PersonMailingAddress { get; set; }

  [JsonProperty("PersonOtherStreet")]
  public object PersonOtherStreet { get; set; }

  [JsonProperty("PersonOtherCity")]
  public object PersonOtherCity { get; set; }

  [JsonProperty("PersonOtherState")]
  public object PersonOtherState { get; set; }

  [JsonProperty("PersonOtherPostalCode")]
  public object PersonOtherPostalCode { get; set; }

  [JsonProperty("PersonOtherCountry")]
  public object PersonOtherCountry { get; set; }

  [JsonProperty("PersonOtherLatitude")]
  public object PersonOtherLatitude { get; set; }

  [JsonProperty("PersonOtherLongitude")]
  public object PersonOtherLongitude { get; set; }

  [JsonProperty("PersonOtherGeocodeAccuracy")]
  public object PersonOtherGeocodeAccuracy { get; set; }

  [JsonProperty("PersonOtherAddress")]
  public object PersonOtherAddress { get; set; }

  [JsonProperty("PersonMobilePhone")]
  public object PersonMobilePhone { get; set; }

  [JsonProperty("PersonHomePhone")]
  public object PersonHomePhone { get; set; }

  [JsonProperty("PersonOtherPhone")]
  public object PersonOtherPhone { get; set; }

  [JsonProperty("PersonAssistantPhone")]
  public object PersonAssistantPhone { get; set; }

  [JsonProperty("PersonEmail")]
  public string PersonEmail { get; set; }

  [JsonProperty("PersonTitle")]
  public object PersonTitle { get; set; }

  [JsonProperty("PersonDepartment")]
  public object PersonDepartment { get; set; }

  [JsonProperty("PersonAssistantName")]
  public object PersonAssistantName { get; set; }

  [JsonProperty("PersonBirthdate")]
  public object PersonBirthdate { get; set; }

  [JsonProperty("PersonHasOptedOutOfEmail")]
  public bool? PersonHasOptedOutOfEmail { get; set; }

  [JsonProperty("PersonHasOptedOutOfFax")]
  public bool? PersonHasOptedOutOfFax { get; set; }

  [JsonProperty("PersonDoNotCall")]
  public bool? PersonDoNotCall { get; set; }

  [JsonProperty("PersonLastCURequestDate")]
  public object PersonLastCuRequestDate { get; set; }

  [JsonProperty("PersonLastCUUpdateDate")]
  public object PersonLastCuUpdateDate { get; set; }

  [JsonProperty("PersonEmailBouncedReason")]
  public object PersonEmailBouncedReason { get; set; }

  [JsonProperty("PersonEmailBouncedDate")]
  public object PersonEmailBouncedDate { get; set; }

  [JsonProperty("PersonIndividualId")]
  public string PersonIndividualId { get; set; }

  [JsonProperty("Jigsaw")]
  public object Jigsaw { get; set; }

  [JsonProperty("JigsawCompanyId")]
  public object JigsawCompanyId { get; set; }

  [JsonProperty("AccountSource")]
  public object AccountSource { get; set; }

  [JsonProperty("SicDesc")]
  public object SicDesc { get; set; }

  [JsonProperty("External_ID_vod__c")]
  public string ExternalIdVodC { get; set; }

  [JsonProperty("Credentials_vod__c")]
  public string CredentialsVodC { get; set; }

  [JsonProperty("Territory_vod__c")]
  public string TerritoryVodC { get; set; }

  [JsonProperty("Exclude_from_Zip_to_Terr_Processing_vod__c")]
  public bool? ExcludeFromZipToTerrProcessingVodC { get; set; }

  [JsonProperty("Group_Specialty_1_vod__c")]
  public object GroupSpecialty1_VodC { get; set; }

  [JsonProperty("Group_Specialty_2_vod__c")]
  public object GroupSpecialty2_VodC { get; set; }

  [JsonProperty("Specialty_1_vod__c")]
  public string Specialty1_VodC { get; set; }

  [JsonProperty("Specialty_2_vod__c")]
  public string Specialty2_VodC { get; set; }

  [JsonProperty("Formatted_Name_vod__c")]
  public string FormattedNameVodC { get; set; }

  [JsonProperty("Territory_Test_vod__c")]
  public string TerritoryTestVodC { get; set; }

  [JsonProperty("Mobile_ID_vod__c")]
  public object MobileIdVodC { get; set; }

  [JsonProperty("Gender_vod__c")]
  public string GenderVodC { get; set; }

  [JsonProperty("ID_vod__c")]
  public object IdVodC { get; set; }

  [JsonProperty("Do_Not_Sync_Sales_Data_vod__c")]
  public bool? DoNotSyncSalesDataVodC { get; set; }

  [JsonProperty("ID2_vod__c")]
  public object Id2VodC { get; set; }

  [JsonProperty("Preferred_Name_vod__c")]
  public string PreferredNameVodC { get; set; }

  [JsonProperty("Sample_Default_vod__c")]
  public object SampleDefaultVodC { get; set; }

  [JsonProperty("Segmentations_vod__c")]
  public object SegmentationsVodC { get; set; }

  [JsonProperty("Restricted_Products_vod__c")]
  public object RestrictedProductsVodC { get; set; }

  [JsonProperty("Payer_Id_vod__c")]
  public object PayerIdVodC { get; set; }

  [JsonProperty("Alternate_Name_vod__c")]
  public string AlternateNameVodC { get; set; }

  [JsonProperty("Do_Not_Call_vod__c")]
  public string DoNotCallVodC { get; set; }

  [JsonProperty("Beds__c")]
  public object BedsC { get; set; }

  [JsonProperty("Spend_Amount__c")]
  public object SpendAmountC { get; set; }

  [JsonProperty("PDRP_Opt_Out_vod__c")]
  public bool? PdrpOptOutVodC { get; set; }

  [JsonProperty("Spend_Status_Value_vod__c")]
  public string SpendStatusValueVodC { get; set; }

  [JsonProperty("PDRP_Opt_Out_Date_vod__c")]
  public string PdrpOptOutDateVodC { get; set; }

  [JsonProperty("Spend_Status_vod__c")]
  public string SpendStatusVodC { get; set; }

  [JsonProperty("Enable_Restricted_Products_vod__c")]
  public bool? EnableRestrictedProductsVodC { get; set; }

  [JsonProperty("Call_Reminder_vod__c")]
  public object CallReminderVodC { get; set; }

  [JsonProperty("Account_Group_vod__c")]
  public object AccountGroupVodC { get; set; }

  [JsonProperty("Primary_Parent_vod__c")]
  public string PrimaryParentVodC { get; set; }

  [JsonProperty("Color_vod__c")]
  public string ColorVodC { get; set; }

  [JsonProperty("Middle_vod__c")]
  public string MiddleVodC { get; set; }

  [JsonProperty("Suffix_vod__c")]
  public string SuffixVodC { get; set; }

  [JsonProperty("No_Orders_vod__c")]
  public string NoOrdersVodC { get; set; }

  [JsonProperty("Account_Identifier_vod__c")]
  public object AccountIdentifierVodC { get; set; }

  [JsonProperty("Approved_Email_Opt_Type_vod__c")]
  public object ApprovedEmailOptTypeVodC { get; set; }

  [JsonProperty("Account_Search_FirstLast_vod__c")]
  public string AccountSearchFirstLastVodC { get; set; }

  [JsonProperty("Account_Search_LastFirst_vod__c")]
  public string AccountSearchLastFirstVodC { get; set; }

  [JsonProperty("Language_vod__c")]
  public object LanguageVodC { get; set; }

  [JsonProperty("Practice_at_Hospital_vod__c")]
  public bool? PracticeAtHospitalVodC { get; set; }

  [JsonProperty("Practice_Near_Hospital_vod__c")]
  public bool? PracticeNearHospitalVodC { get; set; }

  [JsonProperty("Do_Not_Create_Child_Account_vod__c")]
  public bool? DoNotCreateChildAccountVodC { get; set; }

  [JsonProperty("Total_MDs_DOs__c")]
  public object TotalMDsDOsC { get; set; }

  [JsonProperty("AHA__c")]
  public object AhaC { get; set; }

  [JsonProperty("Order_Type_vod__c")]
  public object OrderTypeVodC { get; set; }

  [JsonProperty("NPI_vod__c")]
  public string NpiVodC { get; set; }

  [JsonProperty("ME__c")]
  public string MeC { get; set; }

  [JsonProperty("Speaker__c")]
  public bool? SpeakerC { get; set; }

  [JsonProperty("Investigator_vod__c")]
  public bool? InvestigatorVodC { get; set; }

  [JsonProperty("Default_Order_Type_vod__c")]
  public object DefaultOrderTypeVodC { get; set; }

  [JsonProperty("Tax_Status__c")]
  public object TaxStatusC { get; set; }

  [JsonProperty("Model__c")]
  public object ModelC { get; set; }

  [JsonProperty("Offerings__c")]
  public object OfferingsC { get; set; }

  [JsonProperty("Departments__c")]
  public object DepartmentsC { get; set; }

  [JsonProperty("Account_Type__c")]
  public object AccountTypeC { get; set; }

  [JsonProperty("Account_Search_Business_vod__c")]
  public string AccountSearchBusinessVodC { get; set; }

  [JsonProperty("Business_Professional_Person_vod__c")]
  public object BusinessProfessionalPersonVodC { get; set; }

  [JsonProperty("Hospital_Type_vod__c")]
  public object HospitalTypeVodC { get; set; }

  [JsonProperty("Account_Class_vod__c")]
  public object AccountClassVodC { get; set; }

  [JsonProperty("Furigana_vod__c")]
  public object FuriganaVodC { get; set; }

  [JsonProperty("Country_vod__c")]
  public string CountryVodC { get; set; }

  [JsonProperty("NET_External_Id__c")]
  public string NetExternalIdC { get; set; }

  [JsonProperty("Total_Revenue_000__c")]
  public object TotalRevenue000__C { get; set; }

  [JsonProperty("Net_Income_Loss_000__c")]
  public object NetIncomeLoss000__C { get; set; }

  [JsonProperty("PMPM_Income_Loss_000__c")]
  public object PmpmIncomeLoss000__C { get; set; }

  [JsonProperty("Commercial_Premiums_PMPM__c")]
  public object CommercialPremiumsPmpmC { get; set; }

  [JsonProperty("Medical_Loss_Ratio__c")]
  public object MedicalLossRatioC { get; set; }

  [JsonProperty("Medical_Expenses_PMPM__c")]
  public object MedicalExpensesPmpmC { get; set; }

  [JsonProperty("Commercial_Patient_Days_1000__c")]
  public object CommercialPatientDays1000__C { get; set; }

  [JsonProperty("HMO_Market_Shr__c")]
  public object HmoMarketShrC { get; set; }

  [JsonProperty("HMO__c")]
  public object HmoC { get; set; }

  [JsonProperty("HMO_POS__c")]
  public object HmoPosC { get; set; }

  [JsonProperty("PPO__c")]
  public object PpoC { get; set; }

  [JsonProperty("PPO_POS__c")]
  public object PpoPosC { get; set; }

  [JsonProperty("Medicare__c")]
  public object MedicareC { get; set; }

  [JsonProperty("Medicaid__c")]
  public object MedicaidC { get; set; }

  [JsonProperty("NET_Account_Status__c")]
  public string NetAccountStatusC { get; set; }

  [JsonProperty("NET_TIN__c")]
  public object NetTinC { get; set; }

  [JsonProperty("NET_Account_Type__c")]
  public object NetAccountTypeC { get; set; }

  [JsonProperty("Career_Status_vod__c")]
  public object CareerStatusVodC { get; set; }

  [JsonProperty("Photo_vod__c")]
  public object PhotoVodC { get; set; }

  [JsonProperty("Business_Description__c")]
  public object BusinessDescriptionC { get; set; }

  [JsonProperty("Regional_Strategy__c")]
  public object RegionalStrategyC { get; set; }

  [JsonProperty("Contracts_Process__c")]
  public object ContractsProcessC { get; set; }

  [JsonProperty("Case_Safe_ID__c")]
  public string CaseSafeIdC { get; set; }

  [JsonProperty("SPR_Credentials_2__c")]
  public string SprCredentials2__C { get; set; }

  [JsonProperty("SPR_Tier__c")]
  public string SprTierC { get; set; }

  [JsonProperty("SPR_PNT_Committee_Membership__c")]
  public bool? SprPntCommitteeMembershipC { get; set; }

  [JsonProperty("SPR_Preferred_Contact_Method__c")]
  public string SprPreferredContactMethodC { get; set; }

  [JsonProperty("SPR_Email__c")]
  public string SprEmailC { get; set; }

  [JsonProperty("SPR_Contact_Name_1__c")]
  public string SprContactName1__C { get; set; }

  [JsonProperty("SPR_Contact_Name_2__c")]
  public string SprContactName2__C { get; set; }

  [JsonProperty("Target__c")]
  public bool? TargetC { get; set; }

  [Obsolete("Deprecated by Feature:3764 (PBI-4251)", false)]
  [JsonProperty("KOL_vod__c")]
  public bool? KolVodC { get; set; }

  [JsonProperty("SPR_Contact_Phone_1__c")]
  public string SprContactPhone1__C { get; set; }

  [JsonProperty("SPR_Contact_Phone_2__c")]
  public object SprContactPhone2__C { get; set; }

  [JsonProperty("Total_Lives__c")]
  public object TotalLivesC { get; set; }

  [JsonProperty("Total_Physicians_Enrolled__c")]
  public object TotalPhysiciansEnrolledC { get; set; }

  [JsonProperty("Disease_State__c")]
  public string DiseaseStateC { get; set; }

  [JsonProperty("SPR_Mobile__c")]
  public string SprMobileC { get; set; }

  [JsonProperty("SPR_Notes__c")]
  public string SprNotesC { get; set; }

  [JsonProperty("SPR_Contact_Email_1__c")]
  public string SprContactEmail1__C { get; set; }

  [JsonProperty("SPR_Contact_Email_2__c")]
  public string SprContactEmail2__C { get; set; }

  [JsonProperty("Last_Activity_Date__c")]
  public string LastActivityDateC { get; set; }

  [JsonProperty("NET_Major_Class_of_Trade__c")]
  public object NetMajorClassOfTradeC { get; set; }

  [JsonProperty("NET_Veeva_Mastered__c")]
  public bool? NetVeevaMasteredC { get; set; }

  [JsonProperty("SpringWorks_Email__c")]
  public string SpringWorksEmailC { get; set; }

  [JsonProperty("Total_Pharmacists__c")]
  public object TotalPharmacistsC { get; set; }

  [JsonProperty("Primary_Address_Count__c")]
  public long? PrimaryAddressCountC { get; set; }

  [JsonProperty("Address_Count__c")]
  public long? AddressCountC { get; set; }

  [JsonProperty("SPR_Product2__c")]
  public string SprProduct2C { get; set; }

  [JsonProperty("SPR_Product1__c")]
  public string SprProduct1C { get; set; }

  [JsonProperty("Ex_US_Group__c")]
  public object ExUsGroupC { get; set; }

  [JsonProperty("Practice_Type__c")]
  public string PracticeTypeC { get; set; }

  [JsonProperty("DeFi_Investigator_vod__c")]
  public string DeFiInvestigatorVodC { get; set; }

  [JsonProperty("Frequency__c")]
  public string FrequencyC { get; set; }

  [JsonProperty("Mird_Advisory_Board_Steering_Comm_Member__c")]
  public bool? MirdAdvisoryBoardSteeringCommMemberC { get; set; }


  [JsonProperty("Mirda_BGB_3245_Investigator__c")]
  public bool? MirdaBGB3245InvestigatorC { get; set; }

  [JsonProperty("Mirda_Disease_State__c")]
  public string MirdaDiseaseStateC { get; set; }

  [JsonProperty("Mirda_Frequency__c")]
  public string MirdaFrequencyC { get; set; }

  [JsonProperty("Mirda_KOL__c")]
  public bool? MirdaKOLC { get; set; }

  [JsonProperty("Mirda_No_See__c")]
  public bool? MirdaNoSeeC { get; set; }

  [JsonProperty("Mirda_P_T_Committee_Member__c")]
  public bool? MirdaPTCommitteeMemberC { get; set; }

  [JsonProperty("Mirda_Speaker_s_Bureau_Member__c")]
  public bool? MirdaSpeakerSBureauMemberC { get; set; }

  [JsonProperty("Mirda_Tier__c")]
  public string MirdaTierC { get; set; }

  [JsonProperty("Mirda_Unresponsive__c")]
  public bool? MirdaUnresponsiveC { get; set; }

  [JsonProperty("NCCN_Institution__c")]
  public bool? NCCNInstitutionC { get; set; }

  [JsonProperty("NCCN_Panel_Member__c")]
  public bool? NCCNPanelMemberC { get; set; }

  [JsonProperty("NFCN_Institution__c")]
  public bool? NFCNInstitutionC { get; set; }

  [JsonProperty("NF_1_Diag_Criteria_Cons_Group_Member__c")]
  public bool? NF1DiagCriteriaConsGroupMemberC { get; set; }

  [JsonProperty("NF_Collective_Institution__c")]
  public bool? NFCollectiveInstitutionC { get; set; }

  [JsonProperty("Niro_Disease_State__c")]
  public string NiroDiseaseStateC { get; set; }

  [JsonProperty("Niro_KOL__c")]
  public bool? NiroKOLC { get; set; }

  [JsonProperty("Niro_Tier__c")]
  public string NiroTierC { get; set; }

  [JsonProperty("No_DT_Experience__c")]
  public bool? NoDTExperienceC { get; set; }

  [JsonProperty("No_See__c")]
  public bool? NoSeeC { get; set; }

  [JsonProperty("Peds_Adult__c")]
  public bool? PedsAdultC { get; set; }

  [JsonProperty("Peds__c")]
  public bool? PedsC { get; set; }

  [JsonProperty("ReNeu_Investigator__c")]
  public string ReNeuInvestigatorC { get; set; }

  [JsonProperty("SARC_Center__c")]
  public bool? SARCCenterC { get; set; }

  [JsonProperty("SPR_Email_3__c")]
  public string SPREmail3C { get; set; }

  [JsonProperty("SPR_Linkedin_Handle__c")]
  public string SPRLinkedinHandleC { get; set; }

  [JsonProperty("SPR_Population_Based_Decision_Maker__c")]
  public bool? SPRPopulationBasedDecisionMakerC { get; set; }

  [JsonProperty("SPR_Sales_Target__c")]
  public bool? SPRSalesTargetC { get; set; }

  [JsonProperty("SPR_Twitter_Handle__c")]
  public string SPRTwitterHandleC { get; set; }

  [JsonProperty("SPR_Type_1__c")]
  public string SPRType1C { get; set; }

  [JsonProperty("SPR_Type_2__c")]
  public string SPRType2C { get; set; }

  [JsonProperty("Sarcoma_Alliance_Center__c")]
  public bool? SarcomaAllianceCenterC { get; set; }

  [JsonProperty("St_Jude_Study_Investigator__c")]
  public bool? StJudeStudyInvestigatorC { get; set; }

  [JsonProperty("Unresponsive__c")]
  public bool? UnresponsiveC { get; set; }

  [JsonProperty("DTRF_Panel_Member__c")]
  public bool? DTRFPanelMemberC { get; set; }

  [JsonProperty("DTWG_Panel_Member__c")]
  public bool? DTWGPanelMemberC { get; set; }

  [JsonProperty("Mirda_Compassionate_Use_Investigator__c")]
  public bool? MirdaCompassionateUseInvestigatorC { get; set; }

  [JsonProperty("Mirda_IIR_Investigator__c")]
  public bool? MirdaIIRInvestigatorC { get; set; }

  [JsonProperty("ReNeu_Investigator_checkbox__c")]
  public bool? ReNeuInvestigatorCheckboxC { get; set; }

  [JsonProperty("Non_Prescriber__c")]
  public bool? NonPrescriberC { get; set; }

  [JsonProperty("Niro_IIR_Investigator__c")]
  public bool? NiroIIRInvestigatorC { get; set; }

  [JsonProperty("Niro_Compassionate_Use_Investigator__c")]
  public bool? NiroCompassionateUseInvestigatorC { get; set; }

  [JsonProperty("DeFi_Investigator__c")]
  public bool? DeFiInvestigatorC { get; set; }

  [JsonProperty("COG_Investigator__c")]
  public bool? COGInvestigatorC { get; set; }

  [JsonProperty("OvGCT_Investigator__c")]
  public bool? OvGCTInvestigatorC { get; set; }

  [JsonProperty("HCP_Tier__c")]
  public string HCPTierC { get; set; }

  [JsonProperty("Account_Type_Segment__c")]
  public string AccountTypeSegmentC { get; set; }

  [JsonProperty("Account_Setting__c")]
  public string AccountSettingC { get; set; }

  [JsonProperty("Account_Decile__c")]
  public string AccountDecileC { get; set; }

  [JsonProperty("Mobile_ID_vod__pc")]
  public string MobileIDVodPc { get; set; }

  [JsonProperty("Send_To_SFMC__c")]
  public bool SendToSFMCC { get; set; }
}

public class Attributes
{
  [JsonProperty("type")]
  public string Type { get; set; }

  [JsonProperty("url")]
  public string Url { get; set; }
}

[DisplayName("RecordType")]
public class RecordType
{
  public const string ObjectTypeName = "RecordType";

  public string Id                  { get; set; }
  
  public string Name                { get; set; }
  public string DeveloperName       { get; set; }
  public string NamespacePrefix     { get; set; }
  
  public string Description         { get; set; }  
  public string BusinessProcessId   { get; set; }
  public string SobjectType         { get; set; }
                                    
  public bool IsActive              { get; set; }
  public bool IsPersonType          { get; set; }
                                    
  public string CreatedById         { get; set; }
  public DateTime CreatedDate       { get; set; }
                                    
  public string LastModifiedById    { get; set; }
  public DateTime LastModifiedDate  { get; set; }
  
  public DateTime SystemModstamp    { get; set; }
}

