<Query Kind="Program">
  <Namespace>System.Collections.Immutable</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>System.Text.Json</Namespace>
</Query>

#load ".\TypedEnum"

void Main()
{
  //-----------------------------------------------------------------------------------------------
  var event_type = EventType.InspectionSaved;
  event_type.Dump($"var event_type = mcsEventType.InspectionSaved;", 0);
  
  // implicit conversion of int '1' to 'mcsEventType' - specifically to 'mcsEventType.InspectionSaved'
  var dbEventTypeID = 1;
  var db_event_type = (EventType)dbEventTypeID;
  db_event_type.Dump($"var dbEventTypeID = 1;{Environment.NewLine}var db_event_type = (mcsEventType)dbEventTypeID;", 0);
  
  //-----------------------------------------------------------------------------------------------
  (event_type == db_event_type).Dump("event_type == db_event_type");
  (event_type != db_event_type).Dump("event_type != db_event_type");
  
  //-----------------------------------------------------------------------------------------------
  // implicit conversion of 'mcsEventType.InspectionSaved' to int | ID → 1
  int id = EventType.InspectionSaved;
  id.Dump("int id = mcsEventType.InspectionSaved;");

  // 'mcsEventType.InspectionSaved.ID' → 1
  id = EventType.InspectionSaved.ID;
  id.Dump("int id = mcsEventType.InspectionSaved.ID;");
  
  // implicit conversion of 'mcsEventType.InspectionSaved' to string | Description → "Inspection Saved"
  string description = EventType.InspectionSaved;
  description.Dump("string description = mcsEventType.InspectionSaved;");

  // 'mcsEventType.InspectionSaved.Description' → "Inspection Saved"
  description = EventType.InspectionSaved.Description;
  description.Dump("string description = mcsEventType.InspectionSaved.Description;");
  
  // 'mcsEventType.InspectionSaved.Code' → "InspectionSaved"
  string code = EventType.InspectionSaved.Code;
  code.Dump("string code = mcsEventType.InspectionSaved.Code;");

  //-----------------------------------------------------------------------------------------------
  // Safe lookup
  var event_type_by_id = EventType.GetByID(2);
  event_type_by_id.Dump("var event_type_by_id = mcsEventType.GetByID(2);", 0);

  // All values (for dropdowns, etc.)
  var all_event_types = EventType.GetAll();  // IReadOnlyList<mcsEventType>
  all_event_types.Dump("var all_event_types = mcsEventType.GetAll();", 0);
  
  var descriptions = all_event_types.Select(e => e.Description).Distinct().Order().ToList();
  descriptions.Dump("var descriptions = all_event_types.Select(e => e.Description).Distinct().Order().ToList();", 0);
  
  var codes = all_event_types.Select(e => e.Code).Distinct().Order().ToList();
  codes.Dump("var codes = all_event_types.Select(e => e.Code).Distinct().Order().ToList();", 0);
  
  //-----------------------------------------------------------------------------------------------
  // ToString()s ....
  EventType.VacatedTenantSaved.ToString("D").Dump("mcsEventType.VacatedTenantSaved.ToString(\"D\") => D = 'Description'");
  EventType.VacatedTenantSaved.ToString("C").Dump("mcsEventType.VacatedTenantSaved.ToString(\"C\") => C = 'Code (field name)'");
  EventType.VacatedTenantSaved.ToString("I").Dump("mcsEventType.VacatedTenantSaved.ToString(\"I\") => I = 'ID as string'");
  EventType.VacatedTenantSaved.ToString("F").Dump("mcsEventType.VacatedTenantSaved.ToString(\"F\") => F = 'Full/Verbose Format'");
  EventType.VacatedTenantSaved.ToString("G").Dump("mcsEventType.VacatedTenantSaved.ToString(\"G\") => G = 'General/short + id'");
  EventType.VacatedTenantSaved.ToString("f").Dump("mcsEventType.VacatedTenantSaved.ToString(\"f\") => f = 'alternative Full Format'");
  EventType.VacatedTenantSaved.ToString("g").Dump("mcsEventType.VacatedTenantSaved.ToString(\"g\") => g = 'alternative General'");
  
  // AsJsonString()
  EventType.VacatedTenantSaved.AsJsonString().Dump("mcsEventType.VacatedTenantSaved.AsJsonString()");
  
  // AsJsonObject()
  EventType.VacatedTenantSaved.AsJsonObject().Dump("mcsEventType.VacatedTenantSaved.AsJsonObject()");
}

public sealed class EventType : TypedEnumInt<EventType>
{
  private EventType(int id, string description, string code = null) 
    : base(id, description, code) { }

  #region specific mcsEventType declarations w/nameof(field_name) ...
  
  //NOTE: dumplicated mcsEventTyps for testing only ...
//  public static readonly EventType DuplicateEventType1                        = new(  1, "Duplicate Event Type1"                                    ,nameof(DuplicateEventType1));
//  public static readonly EventType DuplicateEventType324                      = new(324, "Duplicate Event Type324"                                  ,nameof(DuplicateEventType324));
  
  public static readonly EventType InspectionSaved                            = new(  1, "Inspection Saved"                                          ,nameof(InspectionSaved));
  public static readonly EventType InsertFamily                               = new(  2, "Family Inserted"                                           ,nameof(InsertFamily));
  public static readonly EventType VacatedTenantSaved                         = new(  3, "Vacated Tenant Saved"                                      ,nameof(VacatedTenantSaved));
  public static readonly EventType PmPhaSaved                                 = new(  4, "Agency Saved"                                              ,nameof(PmPhaSaved));
  public static readonly EventType FamilyCertSaved                            = new(  5, "Family Cert Saved"                                         ,nameof(FamilyCertSaved));
                                                                                                                                                        
  public static readonly EventType WaitingListApplicationSaved                = new(  6, "Waiting List Application Saved"                            ,nameof(WaitingListApplicationSaved));
  public static readonly EventType RentReasonablenessSaved                    = new(  7, "Rent Reasonableness Saved"                                 ,nameof(RentReasonablenessSaved));
  public static readonly EventType UpdateFamily                               = new(  8, "Family Updated"                                            ,nameof(UpdateFamily));
  public static readonly EventType PortabilityTenantSaved                     = new(  9, "Section 8 Portability Tenant Saved"                        ,nameof(PortabilityTenantSaved));
  public static readonly EventType PortabilityAdjustmentSaved                 = new( 10, "Section 8 Portability Adjustment Saved"                    ,nameof(PortabilityAdjustmentSaved));
                                                                                                                                                         
  public static readonly EventType RapTrapFileGroupSaved                      = new( 11, "RAP/T-RAP File Group Saved"                                ,nameof(RapTrapFileGroupSaved));
  public static readonly EventType RapTrapFormSaved                           = new( 12, "RAP/T-RAP Form Saved"                                      ,nameof(RapTrapFormSaved));
  public static readonly EventType MCSImportedData                            = new( 13, "MCS Imported Data"                                         ,nameof(MCSImportedData));
  public static readonly EventType PortabilityMonthlyPayablesSaved            = new( 14, "Section 8 Portability Monthly Payables Saved"              ,nameof(PortabilityMonthlyPayablesSaved));
  public static readonly EventType CountySaved                                = new( 15, "County Saved"                                              ,nameof(CountySaved));
                                                                                                                                                         
  public static readonly EventType PortabilityDisbusementSaved                = new( 16, "Section 8 Portability Disbursement Saved"                  ,nameof(PortabilityDisbusementSaved));
  public static readonly EventType TracsSaved                                 = new( 17, "TRACS Saved"                                               ,nameof(TracsSaved));
  public static readonly EventType PhaSaved                                   = new( 18, "PHA Saved"                                                 ,nameof(PhaSaved));
  public static readonly EventType ChecksSaved                                = new( 19, "Checks Saved"                                              ,nameof(ChecksSaved));
  public static readonly EventType gpIncomeLimitSaved                         = new( 20, "Income Limit Saved"                                        ,nameof(gpIncomeLimitSaved));
                                                                                                                                                         
  public static readonly EventType MonthEndSaved                              = new( 21, "Month End Saved"                                           ,nameof(MonthEndSaved));
  public static readonly EventType LandlordSaved                              = new( 22, "Landlord Saved"                                            ,nameof(LandlordSaved));
  public static readonly EventType FamilyCertSetupSaved                       = new( 23, "Family Cert Setup Saved"                                   ,nameof(FamilyCertSetupSaved));
  public static readonly EventType HAPContractNumberSaved                     = new( 24, "HAP Contract Number Saved"                                 ,nameof(HAPContractNumberSaved));
  public static readonly EventType Section8UnitSaved                          = new( 25, "Section8 Unit Saved"                                       ,nameof(Section8UnitSaved));
                                                                                                                                                         
  public static readonly EventType MCSLandlordImport                          = new( 26, "MCS Landlord Imported"                                     ,nameof(MCSLandlordImport));
  public static readonly EventType ComparableUnitSaved                        = new( 27, "Comparable Unit Saved"                                     ,nameof(ComparableUnitSaved));
  public static readonly EventType RequestedUnitSaved                         = new( 28, "Requested Unit Saved"                                      ,nameof(RequestedUnitSaved));
  public static readonly EventType PortabilityMasterSaved                     = new( 29, "Section 8 Portability Setup Saved"                         ,nameof(PortabilityMasterSaved));
  public static readonly EventType ResidentInfoSaved                          = new( 30, "Resident Information Saved"                                ,nameof(ResidentInfoSaved));
                                                                                                                                                         
  public static readonly EventType ProgramSaved                               = new( 31, "Program Saved"                                             ,nameof(ProgramSaved));
  public static readonly EventType ForumMessagePosted                         = new( 32, "Forum Message Posted"                                      ,nameof(ForumMessagePosted));
  public static readonly EventType GlobalValueSaved                           = new( 33, "Global Value Saved"                                        ,nameof(GlobalValueSaved));
  public static readonly EventType FinBankSaved                               = new( 34, "Bank Saved"                                                ,nameof(FinBankSaved));
  public static readonly EventType DynamicPageSaved                           = new( 35, "Dynamic Page Saved"                                        ,nameof(DynamicPageSaved));
                                                                                                                                                         
  public static readonly EventType FinAccountSaved                            = new( 36, "Account Saved"                                             ,nameof(FinAccountSaved));
  public static readonly EventType LandlordAdjustmentSaved                    = new( 37, "Landlord Adjustment Saved"                                 ,nameof(LandlordAdjustmentSaved));
//  public static readonly EventType                                            = new( 38, ""                                                          ,nameof());                        //NOT DEFINED in 'enum'
  public static readonly EventType LandlordPayablesPosted                     = new( 39, "Landlord Payables Posted"                                  ,nameof(LandlordPayablesPosted));
  public static readonly EventType PublicUnitSaved                            = new( 40, "Public Unit Saved"                                         ,nameof(PublicUnitSaved));
                                                                                                                                                         
  public static readonly EventType ProjectSaved                               = new( 41, "Project Saved"                                             ,nameof(ProjectSaved));
  public static readonly EventType PublicBuildingSaved                        = new( 42, "Public Building Saved"                                     ,nameof(PublicBuildingSaved));
  public static readonly EventType Section8BuildingSaved                      = new( 43, "Section 8 Building Saved"                                  ,nameof(Section8BuildingSaved));
  public static readonly EventType PortabilityTenantRentSaved                 = new( 44, "Section 8 Portability Tenant Rent Saved"                   ,nameof(PortabilityTenantRentSaved));
  public static readonly EventType tracsMAT30Saved                            = new( 45, "Tracs MAT30 Saved"                                         ,nameof(tracsMAT30Saved));
                                                                                                                                                         
  public static readonly EventType tracsMonthlySubmissionFileSaved            = new( 46, "Tracs Monthly Submission File Saved"                       ,nameof(tracsMonthlySubmissionFileSaved));
  public static readonly EventType FinDocumentSaved                           = new( 47, "Financial Document Saved"                                  ,nameof(FinDocumentSaved));
  public static readonly EventType FinControlGroupSaved                       = new( 48, "Financial Control Group Saved"                             ,nameof(FinControlGroupSaved));
  public static readonly EventType FinTransactionSaved                        = new( 49, "Financial Transaction Saved"                               ,nameof(FinTransactionSaved));
  public static readonly EventType FinGlAccountSaved                          = new( 50, "Fin Gl Account Saved"                                      ,nameof(FinGlAccountSaved));
                                                                                                                                                         
  public static readonly EventType FinTransactionTypeSaved                    = new( 51, "Fin Transaction Type Saved"                                ,nameof(FinTransactionTypeSaved));
  public static readonly EventType phaFileSaved                               = new( 52, "PHA File Saved"                                            ,nameof(phaFileSaved));
  public static readonly EventType MaFormSaved                                = new( 53, "MaForm Saved"                                              ,nameof(MaFormSaved));
  public static readonly EventType StMaUnitSaved                              = new( 54, "General Certification Unit Saved"                          ,nameof(StMaUnitSaved));
  public static readonly EventType StMaIncomeRangeBaseSaved                   = new( 55, "StMa Income Range Base Saved"                              ,nameof(StMaIncomeRangeBaseSaved));
                                                                                                                                                         
  public static readonly EventType DataExported                               = new( 56, "Data Exported"                                             ,nameof(DataExported));
  public static readonly EventType FamilyCertSubmissionFileSaved              = new( 57, "50058 Submission File"                                     ,nameof(FamilyCertSubmissionFileSaved));
  public static readonly EventType PhaUserSaved                               = new( 58, "PHA User Saved"                                            ,nameof(PhaUserSaved));
  public static readonly EventType pmPhaAccountSaved                          = new( 59, "Agency Account Saved"                                      ,nameof(pmPhaAccountSaved));
  public static readonly EventType GeneralLedgerJournalEntrySaved             = new( 60, "General Ledger Journal Entry Saved"                        ,nameof(GeneralLedgerJournalEntrySaved));
                                                                                                                                                         
  public static readonly EventType finHoldReasonSaved                         = new( 61, "finHoldReason Saved"                                       ,nameof(finHoldReasonSaved));
  public static readonly EventType finTransPartSelectionSaved                 = new( 62, "finTransactionPartSelection Saved"                         ,nameof(finTransPartSelectionSaved));
  public static readonly EventType stMaSetupSaved                             = new( 63, "stMaSetup Saved"                                           ,nameof(stMaSetupSaved));
  public static readonly EventType finAdminFee                                = new( 64, "finAdminFee Saved"                                         ,nameof(finAdminFee));
  public static readonly EventType stMaPaymentStandardTownSaved               = new( 65, "stMaPaymentStandardTown Saved"                             ,nameof(stMaPaymentStandardTownSaved));
                                                                                                                                                         
  public static readonly EventType stMaPaymentStandardBedSaved                = new( 66, "stMaPaymentStandardBed Saved"                              ,nameof(stMaPaymentStandardBedSaved));
  public static readonly EventType hapScheduleAdjustmentUpdate                = new( 67, "HAP Schedule Adjustment Update"                            ,nameof(hapScheduleAdjustmentUpdate));
  public static readonly EventType imRoomTypeDefinitionSaved                  = new( 68, "Inspection Manager Room Type Definition Saved"             ,nameof(imRoomTypeDefinitionSaved));
  public static readonly EventType imQuestionTypeDefinitionSaved              = new( 69, "Inspection Manager Question Type Definition Saved"         ,nameof(imQuestionTypeDefinitionSaved));
  public static readonly EventType imFailureTypeDefinitionSaved               = new( 70, "Inspection Manager Failure Type Definition Saved"          ,nameof(imFailureTypeDefinitionSaved));
                                                                                                                                                         
  public static readonly EventType imFormTypeSaved                            = new( 71, "Inspection Manager Form Type Saved"                        ,nameof(imFormTypeSaved));
  public static readonly EventType imInspectionSaved                          = new( 72, "Inspection Manager Inspection Saved"                       ,nameof(imInspectionSaved));
  public static readonly EventType vendorSaved                                = new( 73, "Vendor Saved"                                              ,nameof(vendorSaved));
  public static readonly EventType distributionSaved                          = new( 74, "Distribution Saved"                                        ,nameof(distributionSaved));
  public static readonly EventType stCtUnitSaved                              = new( 75, "stCtUnitSaved"                                             ,nameof(stCtUnitSaved));
                                                                                                                                                         
  public static readonly EventType saveSignature                              = new( 76, "Signature Saved"                                           ,nameof(saveSignature));
  public static readonly EventType phMiscChargesSaved                         = new( 77, "PH Misc Charges Saved"                                     ,nameof(phMiscChargesSaved));
  public static readonly EventType finPaymentTermsSaved                       = new( 78, "Payment Terms Saved"                                       ,nameof(finPaymentTermsSaved));
  public static readonly EventType tracsSetupSaved                            = new( 79, "TRACS Setup Saved"                                         ,nameof(tracsSetupSaved));
  public static readonly EventType landlordsMerged                            = new( 80, "Landlords Merged"                                          ,nameof(landlordsMerged));
                                                                                                                                                         
  public static readonly EventType finPaymentScheduleSaved                    = new( 81, "Payment Schedule Saved"                                    ,nameof(finPaymentScheduleSaved));
  public static readonly EventType waitingListLotteryProcess                  = new( 82, "Waiting List Lottery Process"                              ,nameof(waitingListLotteryProcess));
  public static readonly EventType recurringInvoiceSaved                      = new( 83, "Recurring Invoice Saved"                                   ,nameof(recurringInvoiceSaved));
  public static readonly EventType imCustomQuestionTypeDefinitionSaved        = new( 84, "Inspection Manager Custom Question Type Definition Saved"  ,nameof(imCustomQuestionTypeDefinitionSaved));
  public static readonly EventType glReportGroupSaved                         = new( 85, "General Ledger Report Group Saved"                         ,nameof(glReportGroupSaved));
                                                                                                                                                         
  public static readonly EventType phRepaymentAgreementSaved                  = new( 86, "Tenant Repayment Agreement Saved"                          ,nameof(phRepaymentAgreementSaved));
  public static readonly EventType FinOpenItemRelationSaved                   = new( 87, "Financial Open Item Relation Saved"                        ,nameof(FinOpenItemRelationSaved));
  public static readonly EventType fmCustomValueSetupSaved                    = new( 88, "Family Custom Value Setup Saved"                           ,nameof(fmCustomValueSetupSaved));
  public static readonly EventType smTicketSaved                              = new( 89, "Support Manager Ticket Saved"                              ,nameof(smTicketSaved));
  public static readonly EventType smUserSaved                                = new( 90, "Support Manager User Saved"                                ,nameof(smUserSaved));
                                                                                                                                                         
  public static readonly EventType woWorkOrderSaved                           = new( 91, "Work Order Saved"                                          ,nameof(woWorkOrderSaved));
  public static readonly EventType woAssetSaved                               = new( 92, "Work Order Asset Saved"                                    ,nameof(woAssetSaved));
  public static readonly EventType woInventorySaved                           = new( 93, "Work Order Inventory Saved"                                ,nameof(woInventorySaved));
  public static readonly EventType woTaskSaved                                = new( 94, "Work Order Task Saved"                                     ,nameof(woTaskSaved));
  public static readonly EventType woAssetMaintenanceSaved                    = new( 95, "Work Order Asset Maintenance Saved"                        ,nameof(woAssetMaintenanceSaved));
                                                                                                                                                         
  public static readonly EventType woEmployeeAdjustmentSaved                  = new( 96, "Work Order Employee Adjustment Saved"                      ,nameof(woEmployeeAdjustmentSaved));
  public static readonly EventType woInventoryAdjustmentSaved                 = new( 97, "Work Order Inventory Adjustment Saved"                     ,nameof(woInventoryAdjustmentSaved));
  public static readonly EventType woSetupAssetTypeSaved                      = new( 98, "Work Order Setup Asset Type Saved"                         ,nameof(woSetupAssetTypeSaved));
  public static readonly EventType woSetupNumberSaved                         = new( 99, "Work Order Setup Number Saved"                             ,nameof(woSetupNumberSaved));
  public static readonly EventType woSetupUnitOfMeasureSaved                  = new(100, "Work Order Setup Unit Of Measure Saved"                    ,nameof(woSetupUnitOfMeasureSaved));
                                                                                                                                                         
  public static readonly EventType woSetupInventoryTypeSaved                  = new(101, "Work Order Setup Inventory Type Saved"                     ,nameof(woSetupInventoryTypeSaved));
  public static readonly EventType woSetupInventoryLocationSaved              = new(102, "Work Order Setup Inventory Location Saved"                 ,nameof(woSetupInventoryLocationSaved));
  public static readonly EventType glTemplateDocSaved                         = new(103, "glTemplateDoc Saved"                                       ,nameof(glTemplateDocSaved));
  public static readonly EventType woInventoryUpdateSaved                     = new(104, "Work Order Inventory Update Saved"                         ,nameof(woInventoryUpdateSaved));
  public static readonly EventType glProjectGroupSaved                        = new(105, "glProjectGroup Saved"                                      ,nameof(glProjectGroupSaved));
                                                                                                                                                         
  public static readonly EventType woSetupDefaultCommentsSaved                = new(106, "Work Order Setup Default Comments Saved"                   ,nameof(woSetupDefaultCommentsSaved));
  public static readonly EventType rapTrapSetupSaved                          = new(107, "Rap Trap Setup Saved"                                      ,nameof(rapTrapSetupSaved));
  public static readonly EventType familiesMerged                             = new(108, "Families Merged"                                           ,nameof(familiesMerged));
  public static readonly EventType woSetupLaborTypeSaved                      = new(109, "Work Order Setup Labor Type Saved"                         ,nameof(woSetupLaborTypeSaved));
  public static readonly EventType uaScheduleTypeSaved                        = new(110, "Utility Allowance ScheduleType Saved"                      ,nameof(uaScheduleTypeSaved));
                                                                                                                                                         
  public static readonly EventType uaScheduleSaved                            = new(111, "Utility Allowance Schedule Saved"                          ,nameof(uaScheduleSaved));
  public static readonly EventType uaScheduleBedSizeSaved                     = new(112, "Utility Allowance ScheduleBedSize Saved"                   ,nameof(uaScheduleBedSizeSaved));
  public static readonly EventType zipGroupSaved                              = new(113, "Zip Group Saved"                                           ,nameof(zipGroupSaved));
  public static readonly EventType zipGroupItemSaved                          = new(114, "Zip Group Item Saved"                                      ,nameof(zipGroupItemSaved));
  public static readonly EventType poPurchaseOrderSaved                       = new(115, "Purchase Order Saved"                                      ,nameof(poPurchaseOrderSaved));
                                                                                                                                                         
  public static readonly EventType poLineItemSaved                            = new(116, "Purchase Order Line Item Saved"                            ,nameof(poLineItemSaved));
  public static readonly EventType woSetupReportItemSaved                     = new(117, "Work Order Setup Report Item Saved"                        ,nameof(woSetupReportItemSaved));
  public static readonly EventType paymentStandardTypeSaved                   = new(118, "Payment Standard Type Saved"                               ,nameof(paymentStandardTypeSaved));
  public static readonly EventType phaUserSignatureSaved                      = new(119, "Pha User Signature Saved"                                  ,nameof(phaUserSignatureSaved));
  public static readonly EventType finStatementSetupSaved                     = new(120, "Financial Statement Setup Saved"                           ,nameof(finStatementSetupSaved));
                                                                                                                                                         
  public static readonly EventType glPayrollReportTypeSetupSaved              = new(121, "Payroll Report Type Setup Saved"                           ,nameof(glPayrollReportTypeSetupSaved));
  public static readonly EventType glPayrollTypeSetupSaved                    = new(122, "Payroll Type Setup Saved"                                  ,nameof(glPayrollTypeSetupSaved));
  public static readonly EventType glPayrollSummarySaved                      = new(123, "Payroll Summary Saved"                                     ,nameof(glPayrollSummarySaved));
  public static readonly EventType glPayrollDistributionSaved                 = new(124, "Payroll Distribution Saved"                                ,nameof(glPayrollDistributionSaved));
  public static readonly EventType glPayrollEmployeeDistributionSaved         = new(125, "Payroll Employee Distribution Saved"                       ,nameof(glPayrollEmployeeDistributionSaved));
                                                                                                                                                         
  public static readonly EventType vmsTypeSaved                               = new(126, "VMS Type Saved"                                            ,nameof(vmsTypeSaved));
  public static readonly EventType glPayrollDistributionFieldNumberSaved      = new(127, "Payroll Distribution Field Number Saved"                   ,nameof(glPayrollDistributionFieldNumberSaved));
  public static readonly EventType meterReadingSaved                          = new(128, "Meter Reading Saved"                                       ,nameof(meterReadingSaved));
  public static readonly EventType vmsEffectiveDateSaved                      = new(129, "VMS Effective Date Saved"                                  ,nameof(vmsEffectiveDateSaved));
  public static readonly EventType phHoEffectiveAdjustmentsSaved              = new(130, "PH Homeownership Effective Adjustments"                    ,nameof(phHoEffectiveAdjustmentsSaved));
                                                                                                                                                         
  public static readonly EventType phHoAdjustmentsToBalanceSaved              = new(131, "PH Homeownership Adjustments to Balance"                   ,nameof(phHoAdjustmentsToBalanceSaved));
  public static readonly EventType wlStatusSaved                              = new(132, "WL Status Saved"                                           ,nameof(wlStatusSaved));
  public static readonly EventType glJESTemplateSaved                         = new(133, "GL Journal Entry Simple Template Saved"                    ,nameof(glJESTemplateSaved));
  public static readonly EventType glJESTemplateDetailSaved                   = new(134, "GL Journal Entry Simple Template Detail Saved"             ,nameof(glJESTemplateDetailSaved));
  public static readonly EventType voidedCheckSaved                           = new(135, "Voided Check Saved"                                        ,nameof(voidedCheckSaved));
                                                                                                                                                         
  public static readonly EventType recertPacketSetupSaved                     = new(136, "Recertification Packet Setup Saved"                        ,nameof(recertPacketSetupSaved));
  public static readonly EventType rfParticipatingProgramSaved                = new(137, "RF Participating Program Saved"                            ,nameof(rfParticipatingProgramSaved));
  public static readonly EventType woReportItemSaved                          = new(138, "Work Order Report Item Saved"                              ,nameof(woReportItemSaved));
  public static readonly EventType projectLookupCodeSaved                     = new(139, "Project Lookup Code Saved"                                 ,nameof(projectLookupCodeSaved));
  public static readonly EventType payeeSaved                                 = new(140, "Payee Saved"                                               ,nameof(payeeSaved));
                                                                                                                                                         
  public static readonly EventType payeeTemplateSaved                         = new(141, "Payee Template Saved"                                      ,nameof(payeeTemplateSaved));
  public static readonly EventType familyCertSubmissionErrorSaved             = new(142, "Family Cert Submission Error Saved"                        ,nameof(familyCertSubmissionErrorSaved));
  public static readonly EventType orderingProductSaved                       = new(143, "Forms Ordering Product Saved"                              ,nameof(orderingProductSaved));
  public static readonly EventType orderingOrderSaved                         = new(144, "Forms Ordering Order Saved"                                ,nameof(orderingOrderSaved));
  public static readonly EventType orderingOrderDetailsSaved                  = new(145, "Forms Ordering Order Details Saved"                        ,nameof(orderingOrderDetailsSaved));
                                                                                                                                                         
  public static readonly EventType phUtilityBillingSaved                      = new(146, "PH Utility Billing Saved"                                  ,nameof(phUtilityBillingSaved));
  public static readonly EventType diFolderSaved                              = new(147, "Document Imaging Folder Saved"                             ,nameof(diFolderSaved));
  public static readonly EventType diPHADocumentSaved                         = new(148, "PHA Document Saved"                                        ,nameof(diPHADocumentSaved));
  public static readonly EventType diUserDocumentSaved                        = new(149, "User Document Saved"                                       ,nameof(diUserDocumentSaved));
  public static readonly EventType woInventoryExpensingSetupSaved             = new(150, "Work Order Inventory Expensing Setup Saved"                ,nameof(woInventoryExpensingSetupSaved));
                                                                                                                                                         
  public static readonly EventType woInventoryExpensingSaved                  = new(151, "Work Order Inventory Expensing Saved"                      ,nameof(woInventoryExpensingSaved));
  public static readonly EventType fmAppointmentSaved                         = new(152, "Family Appointment Saved"                                  ,nameof(fmAppointmentSaved));
  public static readonly EventType phDepositMarginSetupSaved                  = new(153, "Deposit Ticket Margin Setup Saved"                         ,nameof(phDepositMarginSetupSaved));
  public static readonly EventType communityServiceDetailSaved                = new(154, "Community Service Detail Saved"                            ,nameof(communityServiceDetailSaved));
  public static readonly EventType glAccountReconciliationSaved               = new(155, "Account Reconciliation Saved"                              ,nameof(glAccountReconciliationSaved));
                                                                                                                                                         
  public static readonly EventType genericNoteSaved                           = new(156, "Generic Note Saved"                                        ,nameof(genericNoteSaved));
  public static readonly EventType genericNoteReminderSaved                   = new(157, "Generic Note Reminder Saved"                               ,nameof(genericNoteReminderSaved));
  public static readonly EventType gnNoteRemindAllSaved                       = new(158, "gnNoteRemindAll Saved"                                     ,nameof(gnNoteRemindAllSaved));
  public static readonly EventType diSignatureDetailSaved                     = new(159, "Document Imaging Signature Detail Saved"                   ,nameof(diSignatureDetailSaved));
  public static readonly EventType flatRentAreaTypeSaved                      = new(160, "Flat Rent Area Type Saved"                                 ,nameof(flatRentAreaTypeSaved));
                                                                                                                                                         
  public static readonly EventType flatRentAreaDateSaved                      = new(161, "Flat Rent Area Date Saved"                                 ,nameof(flatRentAreaDateSaved));
  public static readonly EventType tracsHistoricalSaved                       = new(162, "TRACS Historical Saved"                                    ,nameof(tracsHistoricalSaved));
  public static readonly EventType annualHQSCertFormSaved                     = new(163, "Annual HQS Form Saved"                                     ,nameof(annualHQSCertFormSaved));
  public static readonly EventType staticFileSaved                            = new(164, "Static File Saved"                                         ,nameof(staticFileSaved));
  public static readonly EventType mspTaskSaved                               = new(165, "Multi-Step Task Saved"                                     ,nameof(mspTaskSaved));
                                                                                                                                                         
  public static readonly EventType mspStepSaved                               = new(166, "Multi-Step Step Saved"                                     ,nameof(mspStepSaved));
  public static readonly EventType perFormAutoAdjustmentSave                  = new(167, "Per-Form Auto Adjustment Save"                             ,nameof(perFormAutoAdjustmentSave));
  public static readonly EventType wlApplicantQuestionSaved                   = new(168, "Applicant Question Saved"                                  ,nameof(wlApplicantQuestionSaved));
  public static readonly EventType wlApplicantFullAppAnswerSaved              = new(169, "Applicant Question Full App Answer Saved"                  ,nameof(wlApplicantFullAppAnswerSaved));
  public static readonly EventType ocOnlineClassPHAUserSaved                  = new(170, "Online Class PHA User Saved"                               ,nameof(ocOnlineClassPHAUserSaved));
                                                                                                                                                         
  public static readonly EventType repaymentAgreementTypeSaved                = new(171, "Repayment Agreement Type Saved"                            ,nameof(repaymentAgreementTypeSaved));
  public static readonly EventType fairMarketRentAreaTypeSaved                = new(172, "Fair Market Rent Area Saved"                               ,nameof(fairMarketRentAreaTypeSaved));
  public static readonly EventType fairMarketRentAmountSaved                  = new(173, "Fair Market Rent Amount Saved"                             ,nameof(fairMarketRentAmountSaved));
  public static readonly EventType prRequisitionSaved                         = new(174, "Purchase Requisition Saved"                                ,nameof(prRequisitionSaved));
  public static readonly EventType poShippingAddressSaved                     = new(175, "Purchase Order Shipping Address Saved"                     ,nameof(poShippingAddressSaved));
                                                                                                                                                         
  public static readonly EventType prApprovalSetupSaved                       = new(176, "PR Approval Setup Saved"                                   ,nameof(prApprovalSetupSaved));
  public static readonly EventType prApprovalCostPhaUserSaved                 = new(177, "PR Approval Cost Pha User Saved"                           ,nameof(prApprovalCostPhaUserSaved));
  public static readonly EventType prApprovalSaved                            = new(178, "PR Approval Saved"                                         ,nameof(prApprovalSaved));
  public static readonly EventType tracsRepAgreementLinkSaved                 = new(179, "Repayment Agreement Link Saved"                            ,nameof(tracsRepAgreementLinkSaved));
  public static readonly EventType fssBalanceAdjustmentSaved                  = new(180, "FSS Balance Adjustment Saved"                              ,nameof(fssBalanceAdjustmentSaved));
                                                                                                                                                         
  public static readonly EventType vendorContractSaved                        = new(181, "Vendor Contract Saved"                                     ,nameof(vendorContractSaved));
  public static readonly EventType familyNotificationSetupSaved               = new(182, "Family Notification Setup Saved"                           ,nameof(familyNotificationSetupSaved));
  public static readonly EventType familyCertMasterVoucherExtensionSaved      = new(183, "Family Cert Voucher Extension Saved"                       ,nameof(familyCertMasterVoucherExtensionSaved));
  public static readonly EventType apSelectForPayementSaved                   = new(184, "AP Select For Payment Saved"                               ,nameof(apSelectForPayementSaved));
  public static readonly EventType familyCertContractRentIncreaseSaved        = new(185, "Family Cert Contract Rent Increase"                        ,nameof(familyCertContractRentIncreaseSaved));
                                                                                                                                                         
  public static readonly EventType backgroundCheckRequestSaved                = new(186, "Background Check Request Saved"                            ,nameof(backgroundCheckRequestSaved));
  public static readonly EventType fmPublicSafetyIncidentSaved                = new(187, "Public Safety Incident Saved"                              ,nameof(fmPublicSafetyIncidentSaved));
  public static readonly EventType update1099BatchSaved                       = new(188, "Update 1099 Batch Saved"                                   ,nameof(update1099BatchSaved));
  public static readonly EventType insert1099BatchSaved                       = new(189, "Insert 1099 Batch Saved"                                   ,nameof(insert1099BatchSaved));
  public static readonly EventType woBillingPosted                            = new(190, "Post Work Order Billing"                                   ,nameof(woBillingPosted));
                                                                                                                                                         
  public static readonly EventType fssItspGoalSaved                           = new(191, "FSS ITSP Goal Saved"                                       ,nameof(fssItspGoalSaved));
  public static readonly EventType fssItspNoteSaved                           = new(192, "FSS ITSP Note Saved"                                       ,nameof(fssItspNoteSaved));
  public static readonly EventType certNotificationSaved                      = new(193, "Notification Saved"                                        ,nameof(certNotificationSaved));
  public static readonly EventType certNotificationProcessSaved               = new(194, "Notification Process Saved"                                ,nameof(certNotificationProcessSaved));
  public static readonly EventType finUtilityDistributionSaved                = new(195, "Utility Distribution Saved"                                ,nameof(finUtilityDistributionSaved));
                                                                                                                                                         
  public static readonly EventType communityServiceRecurringSaved             = new(196, "Community Service Recurring Saved"                         ,nameof(communityServiceRecurringSaved));
  public static readonly EventType noteTypeSaved                              = new(197, "Note Type Saved"                                           ,nameof(noteTypeSaved));
  public static readonly EventType phaPortalSetupSaved                        = new(198, "Portal Management Setup Saved"                             ,nameof(phaPortalSetupSaved));
  public static readonly EventType phaPortalFamilyOptionsSaved                = new(199, "Portal Family Options Saved"                               ,nameof(phaPortalFamilyOptionsSaved));
  public static readonly EventType phaFamilyPortalPermissionSetup             = new(200, "Family Portal Permission Setup"                            ,nameof(phaFamilyPortalPermissionSetup));
                                                                                                                                                         
  public static readonly EventType finTransPartSelectionByProjectSaved        = new(201, "finTransactionPartSelectionByProject Saved"                ,nameof(finTransPartSelectionByProjectSaved));
  public static readonly EventType glExportFileSaved                          = new(202, "GL Export File Saved"                                      ,nameof(glExportFileSaved));
  public static readonly EventType famCertUtilScheduleTypeSaved               = new(203, "FamilyCertUtilityScheduleType Saved"                       ,nameof(famCertUtilScheduleTypeSaved));
  public static readonly EventType famCertUtilScheduleDateSaved               = new(204, "FamilyCertUtilityScheduleDate Saved"                       ,nameof(famCertUtilScheduleDateSaved));
  public static readonly EventType stMaUtilityScheduleTypeSaved               = new(205, "StMaUtilityScheduleType Saved"                             ,nameof(stMaUtilityScheduleTypeSaved));
                                                                                                                                                         
  public static readonly EventType stMaUtilityScheduleDateSaved               = new(206, "StMaUtilityScheduleDate Saved"                             ,nameof(stMaUtilityScheduleDateSaved));
  public static readonly EventType phaPortalOppAccSetupSaved                  = new(207, "phaPortalOppAccSetup Saved"                                ,nameof(phaPortalOppAccSetupSaved));
  public static readonly EventType phaPortalOppAccUserSaved                   = new(208, "phaPortalOppAccUser Saved"                                 ,nameof(phaPortalOppAccUserSaved));
  public static readonly EventType phaPortalOppCustomerSaved                  = new(209, "phaPortalOppCustomer Saved"                                ,nameof(phaPortalOppCustomerSaved));
  public static readonly EventType phaPortalOppBatchSaved                     = new(210, "phaPortalOppBatch Saved"                                   ,nameof(phaPortalOppBatchSaved));
                                                                                                                                                         
  public static readonly EventType phaPortalOppPaymentSaved                   = new(211, "phaPortalOppPayment Saved"                                 ,nameof(phaPortalOppPaymentSaved));
  public static readonly EventType phaPortalOppPaymentDetailSaved             = new(212, "phaPortalOppPaymentDetail Saved"                           ,nameof(phaPortalOppPaymentDetailSaved));
  public static readonly EventType sohaDhcdTransmissionFileSaved              = new(213, "SOHA EOHLC Transmission File Saved"                        ,nameof(sohaDhcdTransmissionFileSaved));
  public static readonly EventType phaPortalMcConversationSaved               = new(214, "PortalConversation Saved"                                  ,nameof(phaPortalMcConversationSaved));
  public static readonly EventType phaPortalMcAttachmentSaved                 = new(215, "PortalAttachment Saved"                                    ,nameof(phaPortalMcAttachmentSaved));
                                                                                                                                                         
  public static readonly EventType phaPortalMcMessageSaved                    = new(216, "PortalMessage Saved"                                       ,nameof(phaPortalMcMessageSaved));
  public static readonly EventType woSetupEmployeeScheduleSaved               = new(217, "woSetupEmployeeSchedule Saved"                             ,nameof(woSetupEmployeeScheduleSaved));
  public static readonly EventType woSetupEmployeeScheduleDeSaved             = new(218, "woSetupEmployeeScheduleDe Saved"                           ,nameof(woSetupEmployeeScheduleDeSaved));
  public static readonly EventType unitTurnoverSaved                          = new(219, "unitTurnover Saved"                                        ,nameof(unitTurnoverSaved));
  public static readonly EventType certRequestSaved                           = new(220, "certRequest Saved"                                         ,nameof(certRequestSaved));
                                                                                                                                                         
  public static readonly EventType phaPortalLandlordOptionsSetupSaved         = new(221, "Portal Landlord Options Saved"                             ,nameof(phaPortalLandlordOptionsSetupSaved));
  public static readonly EventType stMaHomeRentIncomeScheduleTypeSaved        = new(222, "stMaHomeRentIncomeScheduleType Saved"                      ,nameof(stMaHomeRentIncomeScheduleTypeSaved));
  public static readonly EventType stMaHomeRentIncomeScheduleDateSaved        = new(223, "stMaHomeRentIncomeScheduleDate Saved"                      ,nameof(stMaHomeRentIncomeScheduleDateSaved));
  public static readonly EventType phaPortalLandlordSaved                     = new(224, "Portal Landlord Saved"                                     ,nameof(phaPortalLandlordSaved));
  public static readonly EventType certFormReviewSaved                        = new(225, "Certification Form Review Saved"                           ,nameof(certFormReviewSaved));
                                                                                                                                                         
  public static readonly EventType invoiceSavedAvidInvoiceImport              = new(226, "Invoice Saved From Avid Invoice Import"                    ,nameof(invoiceSavedAvidInvoiceImport));
  public static readonly EventType phaPortalApplicantOptionsSaved             = new(227, "Portal Applicant Saved"                                    ,nameof(phaPortalApplicantOptionsSaved));
  public static readonly EventType glTrialBalanceWildCardSaved                = new(228, "GL Trial Balance Wild Card Saved"                          ,nameof(glTrialBalanceWildCardSaved));
  public static readonly EventType certFinishFormPacketSetupSaved             = new(229, "Cert Finish Form Packet Setup Saved"                       ,nameof(certFinishFormPacketSetupSaved));
  public static readonly EventType familyCertChangeOfOwnershipSaved           = new(230, "Family Cert Change of Ownership"                           ,nameof(familyCertChangeOfOwnershipSaved));
                                                                                                                                                         
  public static readonly EventType certSignatureRequestSaved                  = new(231, "Cert Signature Request Saved"                              ,nameof(certSignatureRequestSaved));
  public static readonly EventType invoiceSavedInvoiceImport                  = new(232, "invoice Saved Invoice Import"                              ,nameof(invoiceSavedInvoiceImport));
  public static readonly EventType woPhaSetupSaved                            = new(233, "work order pha setup saved"                                ,nameof(woPhaSetupSaved));
  public static readonly EventType diUploadApi                                = new(234, "API Uploaded Document"                                     ,nameof(diUploadApi));
  public static readonly EventType apVendorPhaSetupSaved                      = new(235, "ap vendor pha setup saved"                                 ,nameof(apVendorPhaSetupSaved));
                                                                                                                                                         
  public static readonly EventType ecEmailAddressSaved                        = new(236, "ec Email Address Saved"                                    ,nameof(ecEmailAddressSaved));
  public static readonly EventType woSetupPrioritySaved                       = new(237, "Work Order Setup Priority Saved"                           ,nameof(woSetupPrioritySaved));
  public static readonly EventType woSetupRequestedBySaved                    = new(238, "Work Order Setup Requested By Saved"                       ,nameof(woSetupRequestedBySaved));
  public static readonly EventType woSetupConfigurationSaved                  = new(239, "Work Order Setup Configuration Saved"                      ,nameof(woSetupConfigurationSaved));
  public static readonly EventType familyCertMassCreateBatchSaved             = new(240, "Family Cert Mass Create Batch Saved"                       ,nameof(familyCertMassCreateBatchSaved));
                                                                                                                                                         
  public static readonly EventType familyCertPaymentStandardChangeSaved       = new(241, "Family Cert Payment Standard Change Saved"                 ,nameof(familyCertPaymentStandardChangeSaved));
  public static readonly EventType familyCertEndOfParticipationSaved          = new(242, "Family Cert End of Participation Saved"                    ,nameof(familyCertEndOfParticipationSaved));
  public static readonly EventType glDepreciableAssetTypeSaved                = new(243, "GL Depreciable Asset Type Saved"                           ,nameof(glDepreciableAssetTypeSaved));
  public static readonly EventType familyCertMoveOutReasonSaved               = new(244, "50058 Move Out Reason Saved"                               ,nameof(familyCertMoveOutReasonSaved));
  public static readonly EventType stMaMoveOutReasonSaved                     = new(245, "General Move Out Reason Saved"                             ,nameof(stMaMoveOutReasonSaved));
                                                                                                                                                         
  public static readonly EventType familyCertPhaSetupSaved                    = new(246, "50058 PHA Setup Saved"                                     ,nameof(familyCertPhaSetupSaved));
  public static readonly EventType familyCertSetupScheduleTypeSaved           = new(247, "Setup Schedule Type Saved"                                 ,nameof(familyCertSetupScheduleTypeSaved));
  public static readonly EventType familyCertSetupScheduleEffectiveDateSaved  = new(248, "Setup Schedule Effective Date Saved"                       ,nameof(familyCertSetupScheduleEffectiveDateSaved));
  public static readonly EventType mergeTemplateSaved                         = new(249, "Letter Merge Template Saved"                               ,nameof(mergeTemplateSaved));
  public static readonly EventType hipSubmissionFileSaved                     = new(250, "HIP Submission File Saved"                                 ,nameof(hipSubmissionFileSaved));
                                                                                                                                                         
  public static readonly EventType hipSubmissionFileFormJoinSaved             = new(251, "HIP Submission File Form Join Saved"                       ,nameof(hipSubmissionFileFormJoinSaved));
  public static readonly EventType hipSubmissionErrorSaved                    = new(252, "HIP Submission Error Saved"                                ,nameof(hipSubmissionErrorSaved));
  public static readonly EventType finishFormConfigSaved                      = new(253, "Finish Form Config Saved"                                  ,nameof(finishFormConfigSaved));
  public static readonly EventType finishFormConfigSignatureSaved             = new(254, "Finish Form Config Signature Saved"                        ,nameof(finishFormConfigSignatureSaved));
  public static readonly EventType tracsPhaSetupSaved                         = new(255, "TRACS PHA Setup Saved"                                     ,nameof(tracsPhaSetupSaved));
                                                                                                                                                         
  public static readonly EventType insInspectionSaved                         = new(256, "Inspection Standard Inspection Saved"                      ,nameof(insInspectionSaved));
  public static readonly EventType insQuestionSaved                           = new(257, "Inspection Standard Question Setup Saved"                  ,nameof(insQuestionSaved));
  public static readonly EventType insRoomSaved                               = new(258, "Inspection Standard Room Setup Saved"                      ,nameof(insRoomSaved));
  public static readonly EventType insInspectionDeficiencyWorkOrderSaved      = new(259, "Inspection Standard Deficiency Work Order Saved"           ,nameof(insInspectionDeficiencyWorkOrderSaved));
  public static readonly EventType insInspectionMitigationSaved               = new(260, "Inspection Standard Mitigation Saved"                      ,nameof(insInspectionMitigationSaved));
                                                                                                                                                         
  public static readonly EventType stMaMassCreateBatchSaved                   = new(261, "General Cert Mass Create Batch Saved"                      ,nameof(stMaMassCreateBatchSaved));
  public static readonly EventType insFamilyNotificationSetupSaved            = new(262, "NSPIRE Family Notification Template Saved"                 ,nameof(insFamilyNotificationSetupSaved));
  public static readonly EventType glPhaSetupSaved                            = new(263, "General Ledger PHA Setup Saved"                            ,nameof(glPhaSetupSaved));
  public static readonly EventType phaPortalOppBatchDetailSaved               = new(264, "Deposit Batch Detail Saved"                                ,nameof(phaPortalOppBatchDetailSaved));
  public static readonly EventType depositProcessed                           = new(265, "Deposit Processed"                                         ,nameof(depositProcessed));
                                                                                                                                                         
  public static readonly EventType phaPortalOppBatchCompletionToggle          = new(266, "Deposit Batch Completed Toggle"                            ,nameof(phaPortalOppBatchCompletionToggle));
  public static readonly EventType insInspectionDownloaded                    = new(267, "Inspection Standard Inspection Downloaded"                 ,nameof(insInspectionDownloaded));
  public static readonly EventType insTemplateTypeProgramSaved                = new(268, "Inspection Standard Template Type Program Saved"           ,nameof(insTemplateTypeProgramSaved));
  public static readonly EventType familyCertProjectContractSaved             = new(269, "Family Cert Project Contract Saved"                        ,nameof(familyCertProjectContractSaved));
  public static readonly EventType insPhaSetupSaved                           = new(270, "NSPIRE PHA Setup Saved"                                    ,nameof(insPhaSetupSaved));
                                                                                                                                                         
  public static readonly EventType phArSetupReceiptCashAccountSaved           = new(271, "T AR Setup Receipt Cash Account Saved"                     ,nameof(phArSetupReceiptCashAccountSaved));
  public static readonly EventType phArSetupReceiptCashAccountProjectSaved    = new(272, "T AR Setup Receipt Cash Account Project Saved"             ,nameof(phArSetupReceiptCashAccountProjectSaved));
  public static readonly EventType vendorContractorHoursSaved                 = new(273, "Vendor Contractor Hours Saved"                             ,nameof(vendorContractorHoursSaved));
  public static readonly EventType finAssetDepTemplateSaved                   = new(274, "Asset Depreciation Template Saved"                         ,nameof(finAssetDepTemplateSaved));
  public static readonly EventType finAssetDepTemplateDetailSaved             = new(275, "Asset Depreciation Template Detail Saved"                  ,nameof(finAssetDepTemplateDetailSaved));
                                                                                                                                                         
  public static readonly EventType finAssetDepDistributionDetailSaved         = new(276, "Asset Depreciation Distribution Detail Saved"              ,nameof(finAssetDepDistributionDetailSaved));
  public static readonly EventType phDirectDebitGroupSaved                    = new(277, "PH Direct Debit Group Saved"                               ,nameof(phDirectDebitGroupSaved));
  public static readonly EventType phaPortalMcConversationSeen                = new(278, "Portal Conversation Seen"                                  ,nameof(phaPortalMcConversationSeen));
  public static readonly EventType wlStatusUpdateBatchSaved                   = new(279, "Waiting List Status Update Batch Saved"                    ,nameof(wlStatusUpdateBatchSaved));
  public static readonly EventType wlStatusUpdateCriteriaSaved                = new(280, "Waiting List Status Update Criteria Saved"                 ,nameof(wlStatusUpdateCriteriaSaved));
                                                                                                                                                         
  public static readonly EventType wlStatusUpdateRequestSaved                 = new(281, "Waiting List Status Update Request Saved"                  ,nameof(wlStatusUpdateRequestSaved));
  public static readonly EventType stMaVoucherExtensionSaved                  = new(282, "General Cert Voucher Extension Saved"                      ,nameof(stMaVoucherExtensionSaved));
  public static readonly EventType stMaPhaSetupSaved                          = new(283, "General Cert PHA Setup Saved"                              ,nameof(stMaPhaSetupSaved));
  public static readonly EventType aiUnitDesignationSaved                     = new(284, "AI Unit Designation Saved"                                 ,nameof(aiUnitDesignationSaved));
  public static readonly EventType aiSetAsideGroupSaved                       = new(285, "AI Set Aside Group Saved"                                  ,nameof(aiSetAsideGroupSaved));
                                                                                                                                                         
  public static readonly EventType aiApplicableFractionGroupSaved             = new(286, "AI Applicable Fraction Group Saved"                        ,nameof(aiApplicableFractionGroupSaved));
  public static readonly EventType phaWebsiteSetupSaved                       = new(287, "PHA Website Setup Saved"                                   ,nameof(phaWebsiteSetupSaved));
  public static readonly EventType adminPhaSetupSaved                         = new(288, "Admin Pha Setup Group Saved"                               ,nameof(adminPhaSetupSaved));
  public static readonly EventType meterReadingUnitTypeSaved                  = new(289, "Meter Reading Unit Type Saved"                             ,nameof(meterReadingUnitTypeSaved));
  public static readonly EventType meterReadingUtilityEffectiveSaved          = new(290, "Meter Reading Utility Effective Saved"                     ,nameof(meterReadingUtilityEffectiveSaved));
                                                                                                                                                         
  public static readonly EventType meterReadingUtilityTypeSaved               = new(291, "Meter Reading Utility Type Saved"                          ,nameof(meterReadingUtilityTypeSaved));
  public static readonly EventType saPHASetupSaved                            = new(292, "Super Admin PHA Setup Saved"                               ,nameof(saPHASetupSaved));
  public static readonly EventType wlStatusReasonSaved                        = new(293, "Waiting List Status Reason Saved"                          ,nameof(wlStatusReasonSaved));
  public static readonly EventType fmContactSaved                             = new(294, "Family Contact Saved"                                      ,nameof(fmContactSaved));
  public static readonly EventType fmVehicleSaved                             = new(295, "Family Vehicle Saved"                                      ,nameof(fmVehicleSaved));
                                                                                                                                                         
  public static readonly EventType fmPetSaved                                 = new(296, "Family Pet Saved"                                          ,nameof(fmPetSaved));
  public static readonly EventType lockboxByProgramSaved                      = new(297, "Lockbox By Program Saved"                                  ,nameof(lockboxByProgramSaved));
  public static readonly EventType glAccountLabelSaved                        = new(298, "Financial Account Label Saved"                             ,nameof(glAccountLabelSaved));
  public static readonly EventType smInfoMessageSaved                         = new(299, "Support Info Message Saved"                                ,nameof(smInfoMessageSaved));
  public static readonly EventType certALApplicationListSaved                 = new(300, "Application List Saved"                                    ,nameof(certALApplicationListSaved));
                                                                                                                                                         
  public static readonly EventType certALCredentialSaved                      = new(301, "Application List Credential Saved"                         ,nameof(certALCredentialSaved));
  public static readonly EventType certALApplicationSaved                     = new(302, "Application List Application Saved"                        ,nameof(certALApplicationSaved));
  public static readonly EventType glStatementSaved                           = new(303, "General Ledger Statement Saved"                            ,nameof(glStatementSaved));
  public static readonly EventType glStatementGroupNodeSaved                  = new(304, "General Ledger Statement Group Node Saved"                 ,nameof(glStatementGroupNodeSaved));
  public static readonly EventType glStatementColumnNodeSaved                 = new(305, "General Ledger Statement Column Node Saved"                ,nameof(glStatementColumnNodeSaved));
                                                                                                                                                         
  public static readonly EventType certALStatusSaved                          = new(306, "Application List Status Saved"                             ,nameof(certALStatusSaved));
  public static readonly EventType certALApplicationListStatusSaved           = new(307, "Application List Status Connection Saved"                  ,nameof(certALApplicationListStatusSaved));
  public static readonly EventType glStatementPacketSaved                     = new(308, "General Ledger Statement Packet Saved"                     ,nameof(glStatementPacketSaved));
  public static readonly EventType poInvoiceLineItemRelationSaved             = new(309, "Purchase Order Line Item Relation Saved"                   ,nameof(poInvoiceLineItemRelationSaved));
  public static readonly EventType poLineItemReleaseSaved                     = new(310, "Purchase Order Line Item Release Saved"                    ,nameof(poLineItemReleaseSaved));
                                                                                                                                                         
  public static readonly EventType phPhaSetupSaved                            = new(311, "Tenant Accounting Pha Setup Saved"                         ,nameof(phPhaSetupSaved));
  public static readonly EventType hapMiscChargesSaved                        = new(312, "HAP Misc. Charges Saved"                                   ,nameof(hapMiscChargesSaved));
  public static readonly EventType programFinMiscChargesSaved                 = new(313, "Program Misc Charges Connection Saved"                     ,nameof(programFinMiscChargesSaved));
  public static readonly EventType phaUserGroupSaved                          = new(314, "PHA User Group Saved"                                      ,nameof(phaUserGroupSaved));
  public static readonly EventType phaUserGroupLinkSaved                      = new(315, "PHA User Group Link Saved"                                 ,nameof(phaUserGroupLinkSaved));
                                                                                                                                                         
  public static readonly EventType ddDataDownloadSaved                        = new(316, "Data Download Saved"                                       ,nameof(ddDataDownloadSaved));
  public static readonly EventType tracsSpecialClaimSaved                     = new(317, "TRACS Special Claim Saved"                                 ,nameof(tracsSpecialClaimSaved));
  public static readonly EventType tracsSpecialClaimUnpaidRentDamagesSaved    = new(318, "TRACS Special Claim Unpaid Rent/Damages Saved"             ,nameof(tracsSpecialClaimUnpaidRentDamagesSaved));
  public static readonly EventType tracsSpecialClaimVacancyDuringRentUpSaved  = new(319, "TRACS Special Claim Vacancy During Rent-Up Saved"          ,nameof(tracsSpecialClaimVacancyDuringRentUpSaved));
  public static readonly EventType tracsSpecialClaimRegularVacancySaved       = new(320, "TRACS Special Claim Regular Vacancy Saved"                 ,nameof(tracsSpecialClaimRegularVacancySaved));
                                                                                                                                                         
  public static readonly EventType tracsSpecialClaimDebtServiceSaved          = new(321, "TRACS Special Claim Debt Service Saved"                    ,nameof(tracsSpecialClaimDebtServiceSaved));
  public static readonly EventType finDepartmentSaved                         = new(322, "Department Saved"                                          ,nameof(finDepartmentSaved));
  public static readonly EventType expirableFileSaved                         = new(323, "Expirable File Saved"                                      ,nameof(expirableFileSaved));
  public static readonly EventType smTicketTagSaved                           = new(324, "Support Manager Ticket Tag Saved"                          ,nameof(smTicketTagSaved));
  
  #endregion
}
