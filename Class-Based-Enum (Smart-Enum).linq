<Query Kind="Program">
  <Namespace>System.Collections.Immutable</Namespace>
</Query>

void Main()
{
  //var fields_as_dictionary = EventType.GetAllFields(true);
  //fields_as_dictionary.Dump("var fields_as_dictionary = EventType.GetAllFields(true);", 0);
  //
  //var fields_as_list = EventType.GetAllFields(false);
  //fields_as_list.Dump("var fields_as_list = EventType.GetAllFields(false);", 0); 
  //
  //EventType.Properties.Dump("EventType.Properties", 0);
  //
  //var type        = EventType.InspectionSaved;
  //var description = type.Description;
  //
  //type.Dump("var type = EventType.InspectionSaved;");
  
  //var event_type = EventType.GetPropertyByID(type.ID);
  //var event_type = EventType.GetPropertyByID(1);
  //Console.WriteLine("Event: {Descrption}", event_type.Description);  
 
  // CASTING: explict and implicit examples
  EventType type = (EventType)42;                         // explicit cast
  type.Dump("EventType type = (EventType)42;", 0);
  
  EventType.PublicBuildingSaved.Dump("EventType.PublicBuildingSaved", 0);

  int id = EventType.PublicBuildingSaved;                 // implicit conversion
  id.Dump("int id = EventType.PublicBuildingSaved;");
  
  int id2 = (int)EventType.PublicBuildingSaved;
  id2.Dump("int id2 = EventType.PublicBuildingSaved;");   // explicit cast
  
  int id3 = (int)EventType.GetFieldByID(42);
  id3.Dump("int id3 = (int)EventType.GetFieldByID(42);"); // explicit cast


  string description = EventType.PublicBuildingSaved;             // implicit conversion
  description.Dump("string description = EventType.PublicBuildingSaved;");

  string description2 = (string)EventType.PublicBuildingSaved;    // explicit cast
  description2.Dump("string description2 = (string)EventType.PublicBuildingSaved;");
  
  string description3 = (string)EventType.GetFieldByID(42);       // explicit cast
  description3.Dump("string description3 = (string)EventType.GetFieldByID(42);");

  //.ToString() .....
  string description4 = EventType.PublicBuildingSaved.ToString();
  description4.Dump("string description4 = EventType.PublicBuildingSaved.ToString();");

  string description5 = EventType.PublicBuildingSaved.ToString(asJsonObject: true);
  description5.Dump("string description5 = EventType.PublicBuildingSaved.ToString(asJsonObject: true);");
}

#region (Option 1: sealed class ...)
/*************************************************************************************************/

public sealed class EventType
{
  public int ID             { get; }
  public string Description { get; }
  
  private EventType(int id, string description)
  {
    ID          = id;
    Description = description;
  }

  #region Specific EventType Declarations ...
  
  public static readonly EventType InspectionSaved                             = new(  1, "Inspection Saved");
  public static readonly EventType InsertFamily                                = new(  2, "Family Inserted");
  public static readonly EventType VacatedTenantSaved                          = new(  3, "Vacated Tenant Saved");
  public static readonly EventType PmPhaSaved                                  = new(  4, "Agency Saved");
  public static readonly EventType FamilyCertSaved                             = new(  5, "Family Cert Saved");
  
  public static readonly EventType WaitingListApplicationSaved                 = new(  6, "Waiting List Application Saved");
  public static readonly EventType RentReasonablenessSaved                     = new(  7, "Rent Reasonableness Saved");
  public static readonly EventType UpdateFamily                                = new(  8, "Family Updated");
  public static readonly EventType PortabilityTenantSaved                      = new(  9, "Section 8 Portability Tenant Saved");
  public static readonly EventType PortabilityAdjustmentSaved                  = new( 10, "Section 8 Portability Adjustment Saved");
  
  public static readonly EventType RapTrapFileGroupSaved                       = new( 11, "RAP/T-RAP File Group Saved");
  public static readonly EventType RapTrapFormSaved                            = new( 12, "RAP/T-RAP Form Saved");
  public static readonly EventType MCSImportedData                             = new( 13, "MCS Imported Data");
  public static readonly EventType PortabilityMonthlyPayablesSaved             = new( 14, "Section 8 Portability Monthly Payables Saved");
  public static readonly EventType CountySaved                                 = new( 15, "County Saved");
  
  public static readonly EventType PortabilityDisbusementSaved                 = new( 16, "Section 8 Portability Disbursement Saved");
  public static readonly EventType TracsSaved                                  = new( 17, "TRACS Saved");
  public static readonly EventType PhaSaved                                    = new( 18, "PHA Saved");
  public static readonly EventType ChecksSaved                                 = new( 19, "Checks Saved");
  public static readonly EventType gpIncomeLimitSaved                          = new( 20, "Income Limit Saved");
  
  public static readonly EventType MonthEndSaved                               = new( 21, "Month End Saved");
  public static readonly EventType LandlordSaved                               = new( 22, "Landlord Saved");
  public static readonly EventType FamilyCertSetupSaved                        = new( 23, "Family Cert Setup Saved");
  public static readonly EventType HAPContractNumberSaved                      = new( 24, "HAP Contract Number Saved");
  public static readonly EventType Section8UnitSaved                           = new( 25, "Section8 Unit Saved");
  
  public static readonly EventType MCSLandlordImport                           = new( 26, "MCS Landlord Imported");
  public static readonly EventType ComparableUnitSaved                         = new( 27, "Comparable Unit Saved");
  public static readonly EventType RequestedUnitSaved                          = new( 28, "Requested Unit Saved");
  public static readonly EventType PortabilityMasterSaved                      = new( 29, "Section 8 Portability Setup Saved");
  public static readonly EventType ResidentInfoSaved                           = new( 30, "Resident Information Saved");
  
  public static readonly EventType ProgramSaved                                = new( 31, "Program Saved");
  public static readonly EventType ForumMessagePosted                          = new( 32, "Forum Message Posted");
  public static readonly EventType GlobalValueSaved                            = new( 33, "Global Value Saved");
  public static readonly EventType FinBankSaved                                = new( 34, "Bank Saved");
  public static readonly EventType DynamicPageSaved                            = new( 35, "Dynamic Page Saved");
  
  public static readonly EventType FinAccountSaved                             = new( 36, "Account Saved");
  public static readonly EventType LandlordAdjustmentSaved                     = new( 37, "Landlord Adjustment Saved");
//  public static readonly EventType                                             = new( 38, "");                                //NOT DEFINED in 'enum'
  public static readonly EventType LandlordPayablesPosted                      = new( 39, "Landlord Payables Posted");
  public static readonly EventType PublicUnitSaved                             = new( 40, "Public Unit Saved");
  
  public static readonly EventType ProjectSaved                                = new( 41, "Project Saved");
  public static readonly EventType PublicBuildingSaved                         = new( 42, "Public Building Saved");
  public static readonly EventType Section8BuildingSaved                       = new( 43, "Section 8 Building Saved");
  public static readonly EventType PortabilityTenantRentSaved                  = new( 44, "Section 8 Portability Tenant Rent Saved");
  public static readonly EventType tracsMAT30Saved                             = new( 45, "Tracs MAT30 Saved");
  
  public static readonly EventType tracsMonthlySubmissionFileSaved             = new( 46, "Tracs Monthly Submission File Saved");
  public static readonly EventType FinDocumentSaved                            = new( 47, "Financial Document Saved");
  public static readonly EventType FinControlGroupSaved                        = new( 48, "Financial Control Group Saved");
  public static readonly EventType FinTransactionSaved                         = new( 49, "Financial Transaction Saved");
  public static readonly EventType FinGlAccountSaved                           = new( 50, "Fin Gl Account Saved");
  
  public static readonly EventType FinTransactionTypeSaved                     = new( 51, "Fin Transaction Type Saved");
//  public static readonly EventType phaFileSaved                                = new( 52, "PHA File Saved");                  // COMMENTED OUT in 'enum'
  public static readonly EventType MaFormSaved                                 = new( 53, "MaForm Saved");
  public static readonly EventType StMaUnitSaved                               = new( 54, "General Certification Unit Saved");  // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static readonly EventType StMaIncomeRangeBaseSaved                    = new( 55, "StMa Income Range Base Saved");
  
  public static readonly EventType DataExported                                = new( 56, "Data Exported");
//  public static readonly EventType FamilyCertSubmissionFileSaved               = new( 57, "50058 Submission File");           // COMMENTED OUT in 'enum'
  public static readonly EventType PhaUserSaved                                = new( 58, "PHA User Saved");
  public static readonly EventType pmPhaAccountSaved                           = new( 59, "Agency Account Saved");
  public static readonly EventType GeneralLedgerJournalEntrySaved              = new( 60, "General Ledger Journal Entry Saved");
  
  public static readonly EventType finHoldReasonSaved                          = new( 61, "finHoldReason Saved");
  public static readonly EventType finTransPartSelectionSaved                  = new( 62, "finTransactionPartSelection Saved");
  public static readonly EventType stMaSetupSaved                              = new( 63, "stMaSetup Saved");
  public static readonly EventType finAdminFee                                 = new( 64, "finAdminFee Saved");
  public static readonly EventType stMaPaymentStandardTownSaved                = new( 65, "stMaPaymentStandardTown Saved");
  
  public static readonly EventType stMaPaymentStandardBedSaved                 = new( 66, "stMaPaymentStandardBed Saved");
  public static readonly EventType hapScheduleAdjustmentUpdate                 = new( 67, "HAP Schedule Adjustment Update");
  public static readonly EventType imRoomTypeDefinitionSaved                   = new( 68, "Inspection Manager Room Type Definition Saved");
  public static readonly EventType imQuestionTypeDefinitionSaved               = new( 69, "Inspection Manager Question Type Definition Saved");
  public static readonly EventType imFailureTypeDefinitionSaved                = new( 70, "Inspection Manager Failure Type Definition Saved");
  
  public static readonly EventType imFormTypeSaved                             = new( 71, "Inspection Manager Form Type Saved");
  public static readonly EventType imInspectionSaved                           = new( 72, "Inspection Manager Inspection Saved");
  public static readonly EventType vendorSaved                                 = new( 73, "Vendor Saved");
//  public static readonly EventType distributionSaved                           = new( 74, "Distribution Saved");  // NOT FOUND in the 'eventTypeDescription's Select Case statement
//  public static readonly EventType stCtUnitSaved                               = new( 75, "stCtUnitSaved");       // COMMENTED OUT in 'enum'
  
  public static readonly EventType saveSignature                               = new( 76, "Signature Saved");
  public static readonly EventType phMiscChargesSaved                          = new( 77, "PH Misc Charges Saved");
  public static readonly EventType finPaymentTermsSaved                        = new( 78, "Payment Terms Saved");
  public static readonly EventType tracsSetupSaved                             = new( 79, "TRACS Setup Saved");
  public static readonly EventType landlordsMerged                             = new( 80, "Landlords Merged");
  
  public static readonly EventType finPaymentScheduleSaved                     = new( 81, "Payment Schedule Saved");
  public static readonly EventType waitingListLotteryProcess                   = new( 82, "Waiting List Lottery Process");
  public static readonly EventType recurringInvoiceSaved                       = new( 83, "Recurring Invoice Saved");
  public static readonly EventType imCustomQuestionTypeDefinitionSaved         = new( 84, "Inspection Manager Custom Question Type Definition Saved");
  public static readonly EventType glReportGroupSaved                          = new( 85, "General Ledger Report Group Saved");
  
  public static readonly EventType phRepaymentAgreementSaved                   = new( 86, "Tenant Repayment Agreement Saved");
  public static readonly EventType FinOpenItemRelationSaved                    = new( 87, "Financial Open Item Relation Saved");
  public static readonly EventType fmCustomValueSetupSaved                     = new( 88, "Family Custom Value Setup Saved");
  public static readonly EventType smTicketSaved                               = new( 89, "Support Manager Ticket Saved");
  public static readonly EventType smUserSaved                                 = new( 90, "Support Manager User Saved");
  
  public static readonly EventType woWorkOrderSaved                            = new( 91, "Work Order Saved");
  public static readonly EventType woAssetSaved                                = new( 92, "Work Order Asset Saved");
  public static readonly EventType woInventorySaved                            = new( 93, "Work Order Inventory Saved");
  public static readonly EventType woTaskSaved                                 = new( 94, "Work Order Task Saved");
  public static readonly EventType woAssetMaintenanceSaved                     = new( 95, "Work Order Asset Maintenance Saved");
  
  public static readonly EventType woEmployeeAdjustmentSaved                   = new( 96, "Work Order Employee Adjustment Saved");
  public static readonly EventType woInventoryAdjustmentSaved                  = new( 97, "Work Order Inventory Adjustment Saved");
  public static readonly EventType woSetupAssetTypeSaved                       = new( 98, "Work Order Setup Asset Type Saved");
  public static readonly EventType woSetupNumberSaved                          = new( 99, "Work Order Setup Number Saved");
  public static readonly EventType woSetupUnitOfMeasureSaved                   = new(100, "Work Order Setup Unit Of Measure Saved");
  
  public static readonly EventType woSetupInventoryTypeSaved                   = new(101, "Work Order Setup Inventory Type Saved");
  public static readonly EventType woSetupInventoryLocationSaved               = new(102, "Work Order Setup Inventory Location Saved");
  public static readonly EventType glTemplateDocSaved                          = new(103, "glTemplateDoc Saved");
  public static readonly EventType woInventoryUpdateSaved                      = new(104, "Work Order Inventory Update Saved");
  public static readonly EventType glProjectGroupSaved                         = new(105, "glProjectGroup Saved");
  
  public static readonly EventType woSetupDefaultCommentsSaved                 = new(106, "Work Order Setup Default Comments Saved");
  public static readonly EventType rapTrapSetupSaved                           = new(107, "Rap Trap Setup Saved");
  public static readonly EventType familiesMerged                              = new(108, "Families Merged");
  public static readonly EventType woSetupLaborTypeSaved                       = new(109, "Work Order Setup Labor Type Saved");
  public static readonly EventType uaScheduleTypeSaved                         = new(110, "Utility Allowance ScheduleType Saved");
  
  public static readonly EventType uaScheduleSaved                             = new(111, "Utility Allowance Schedule Saved");
  public static readonly EventType uaScheduleBedSizeSaved                      = new(112, "Utility Allowance ScheduleBedSize Saved");
  public static readonly EventType zipGroupSaved                               = new(113, "Zip Group Saved");
  public static readonly EventType zipGroupItemSaved                           = new(114, "Zip Group Item Saved");
  public static readonly EventType poPurchaseOrderSaved                        = new(115, "Purchase Order Saved");
  
  public static readonly EventType poLineItemSaved                             = new(116, "Purchase Order Line Item Saved");
  public static readonly EventType woSetupReportItemSaved                      = new(117, "Work Order Setup Report Item Saved");
  public static readonly EventType paymentStandardTypeSaved                    = new(118, "Payment Standard Type Saved");
  public static readonly EventType phaUserSignatureSaved                       = new(119, "Pha User Signature Saved");
  public static readonly EventType finStatementSetupSaved                      = new(120, "Financial Statement Setup Saved");
  
  public static readonly EventType glPayrollReportTypeSetupSaved               = new(121, "Payroll Report Type Setup Saved");
  public static readonly EventType glPayrollTypeSetupSaved                     = new(122, "Payroll Type Setup Saved");
  public static readonly EventType glPayrollSummarySaved                       = new(123, "Payroll Summary Saved");
  public static readonly EventType glPayrollDistributionSaved                  = new(124, "Payroll Distribution Saved");
  public static readonly EventType glPayrollEmployeeDistributionSaved          = new(125, "Payroll Employee Distribution Saved");
  
  public static readonly EventType vmsTypeSaved                                = new(126, "VMS Type Saved");
  public static readonly EventType glPayrollDistributionFieldNumberSaved       = new(127, "Payroll Distribution Field Number Saved");
  public static readonly EventType meterReadingSaved                           = new(128, "Meter Reading Saved");
  public static readonly EventType vmsEffectiveDateSaved                       = new(129, "VMS Effective Date Saved");
  public static readonly EventType phHoEffectiveAdjustmentsSaved               = new(130, "PH Homeownership Effective Adjustments");
  
  public static readonly EventType phHoAdjustmentsToBalanceSaved               = new(131, "PH Homeownership Adjustments to Balance");
  public static readonly EventType wlStatusSaved                               = new(132, "WL Status Saved");
  public static readonly EventType glJESTemplateSaved                          = new(133, "GL Journal Entry Simple Template Saved");
  public static readonly EventType glJESTemplateDetailSaved                    = new(134, "GL Journal Entry Simple Template Detail Saved");
  public static readonly EventType voidedCheckSaved                            = new(135, "Voided Check Saved");
  
  public static readonly EventType recertPacketSetupSaved                      = new(136, "Recertification Packet Setup Saved");
  public static readonly EventType rfParticipatingProgramSaved                 = new(137, "RF Participating Program Saved");
  public static readonly EventType woReportItemSaved                           = new(138, "Work Order Report Item Saved");
  public static readonly EventType projectLookupCodeSaved                      = new(139, "Project Lookup Code Saved");
  public static readonly EventType payeeSaved                                  = new(140, "Payee Saved");
  
  public static readonly EventType payeeTemplateSaved                          = new(141, "Payee Template Saved");
  public static readonly EventType familyCertSubmissionErrorSaved              = new(142, "Family Cert Submission Error Saved");
//  public static readonly EventType orderingProductSaved                        = new(143, "Forms Ordering Product Saved");        // COMMENTED OUT in 'enum'
//  public static readonly EventType orderingOrderSaved                          = new(144, "Forms Ordering Order Saved");          // COMMENTED OUT in 'enum'
//  public static readonly EventType orderingOrderDetailsSaved                   = new(145, "Forms Ordering Order Details Saved");  // COMMENTED OUT in 'enum'
  
  public static readonly EventType phUtilityBillingSaved                       = new(146, "PH Utility Billing Saved");
  public static readonly EventType diFolderSaved                               = new(147, "Document Imaging Folder Saved");
  public static readonly EventType diPHADocumentSaved                          = new(148, "PHA Document Saved");
  public static readonly EventType diUserDocumentSaved                         = new(149, "User Document Saved");
  public static readonly EventType woInventoryExpensingSetupSaved              = new(150, "Work Order Inventory Expensing Setup Saved");
  
  public static readonly EventType woInventoryExpensingSaved                   = new(151, "Work Order Inventory Expensing Saved");
  public static readonly EventType fmAppointmentSaved                          = new(152, "Family Appointment Saved");
  public static readonly EventType phDepositMarginSetupSaved                   = new(153, "Deposit Ticket Margin Setup Saved");
  public static readonly EventType communityServiceDetailSaved                 = new(154, "Community Service Detail Saved");
  public static readonly EventType glAccountReconciliationSaved                = new(155, "Account Reconciliation Saved");
  
  public static readonly EventType genericNoteSaved                            = new(156, "Generic Note Saved");
  public static readonly EventType genericNoteReminderSaved                    = new(157, "Generic Note Reminder Saved");
  public static readonly EventType gnNoteRemindAllSaved                        = new(158, "gnNoteRemindAll Saved");
  public static readonly EventType diSignatureDetailSaved                      = new(159, "Document Imaging Signature Detail Saved");
  public static readonly EventType flatRentAreaTypeSaved                       = new(160, "Flat Rent Area Type Saved");
  
  public static readonly EventType flatRentAreaDateSaved                       = new(161, "Flat Rent Area Date Saved");
  public static readonly EventType tracsHistoricalSaved                        = new(162, "TRACS Historical Saved");
  public static readonly EventType annualHQSCertFormSaved                      = new(163, "Annual HQS Form Saved");
  public static readonly EventType staticFileSaved                             = new(164, "Static File Saved");
  public static readonly EventType mspTaskSaved                                = new(165, "Multi-Step Task Saved");
  
  public static readonly EventType mspStepSaved                                = new(166, "Multi-Step Step Saved");
  public static readonly EventType perFormAutoAdjustmentSave                   = new(167, "Per-Form Auto Adjustment Save");
  public static readonly EventType wlApplicantQuestionSaved                    = new(168, "Applicant Question Saved");
  public static readonly EventType wlApplicantFullAppAnswerSaved               = new(169, "Applicant Question Full App Answer Saved");
  public static readonly EventType ocOnlineClassPHAUserSaved                   = new(170, "Online Class PHA User Saved");
  
  public static readonly EventType repaymentAgreementTypeSaved                 = new(171, "Repayment Agreement Type Saved");
  public static readonly EventType fairMarketRentAreaTypeSaved                 = new(172, "Fair Market Rent Area Saved");
  public static readonly EventType fairMarketRentAmountSaved                   = new(173, "Fair Market Rent Amount Saved");
  public static readonly EventType prRequisitionSaved                          = new(174, "Purchase Requisition Saved");
  public static readonly EventType poShippingAddressSaved                      = new(175, "Purchase Order Shipping Address Saved");
  
  public static readonly EventType prApprovalSetupSaved                        = new(176, "PR Approval Setup Saved");
  public static readonly EventType prApprovalCostPhaUserSaved                  = new(177, "PR Approval Cost Pha User Saved");
  public static readonly EventType prApprovalSaved                             = new(178, "PR Approval Saved");
  public static readonly EventType tracsRepAgreementLinkSaved                  = new(179, "Repayment Agreement Link Saved");
  public static readonly EventType fssBalanceAdjustmentSaved                   = new(180, "FSS Balance Adjustment Saved");
  
  public static readonly EventType vendorContractSaved                         = new(181, "Vendor Contract Saved");
//  public static readonly EventType familyNotificationSetupSaved                = new(182, "Family Notification Setup Saved"); // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static readonly EventType familyCertMasterVoucherExtensionSaved       = new(183, "Family Cert Voucher Extension Saved");
  public static readonly EventType apSelectForPayementSaved                    = new(184, "AP Select For Payment Saved");
  public static readonly EventType familyCertContractRentIncreaseSaved         = new(185, "Family Cert Contract Rent Increase");
  
  public static readonly EventType backgroundCheckRequestSaved                 = new(186, "Background Check Request Saved");
  public static readonly EventType fmPublicSafetyIncidentSaved                 = new(187, "Public Safety Incident Saved");
  public static readonly EventType update1099BatchSaved                        = new(188, "Update 1099 Batch Saved");
  public static readonly EventType insert1099BatchSaved                        = new(189, "Insert 1099 Batch Saved");
  public static readonly EventType woBillingPosted                             = new(190, "Post Work Order Billing");
  
  public static readonly EventType fssItspGoalSaved                            = new(191, "FSS ITSP Goal Saved");
  public static readonly EventType fssItspNoteSaved                            = new(192, "FSS ITSP Note Saved");
  public static readonly EventType certNotificationSaved                       = new(193, "Notification Saved");
  public static readonly EventType certNotificationProcessSaved                = new(194, "Notification Process Saved");
  public static readonly EventType finUtilityDistributionSaved                 = new(195, "Utility Distribution Saved");
  
  public static readonly EventType communityServiceRecurringSaved              = new(196, "Community Service Recurring Saved");
  public static readonly EventType noteTypeSaved                               = new(197, "Note Type Saved");
  public static readonly EventType phaPortalSetupSaved                         = new(198, "Portal Management Setup Saved");
  public static readonly EventType phaPortalFamilyOptionsSaved                 = new(199, "Portal Family Options Saved");
//  public static readonly EventType phaFamilyPortalPermissionSetup              = new(200, "Family Portal Permission Setup");  // COMMENTED OUT in 'enum'
  
  public static readonly EventType finTransPartSelectionByProjectSaved         = new(201, "finTransactionPartSelectionByProject Saved");
  public static readonly EventType glExportFileSaved                           = new(202, "GL Export File Saved");
  public static readonly EventType famCertUtilScheduleTypeSaved                = new(203, "FamilyCertUtilityScheduleType Saved");
  public static readonly EventType famCertUtilScheduleDateSaved                = new(204, "FamilyCertUtilityScheduleDate Saved");
  public static readonly EventType stMaUtilityScheduleTypeSaved                = new(205, "StMaUtilityScheduleType Saved");
  
  public static readonly EventType stMaUtilityScheduleDateSaved                = new(206, "StMaUtilityScheduleDate Saved");
  public static readonly EventType phaPortalOppAccSetupSaved                   = new(207, "phaPortalOppAccSetup Saved");
  public static readonly EventType phaPortalOppAccUserSaved                    = new(208, "phaPortalOppAccUser Saved");
  public static readonly EventType phaPortalOppCustomerSaved                   = new(209, "phaPortalOppCustomer Saved");
  public static readonly EventType phaPortalOppBatchSaved                      = new(210, "phaPortalOppBatch Saved");
  
  public static readonly EventType phaPortalOppPaymentSaved                    = new(211, "phaPortalOppPayment Saved");
  public static readonly EventType phaPortalOppPaymentDetailSaved              = new(212, "phaPortalOppPaymentDetail Saved");
  public static readonly EventType sohaDhcdTransmissionFileSaved               = new(213, "SOHA EOHLC Transmission File Saved");
  public static readonly EventType phaPortalMcConversationSaved                = new(214, "PortalConversation Saved");
  public static readonly EventType phaPortalMcAttachmentSaved                  = new(215, "PortalAttachment Saved");
  
  public static readonly EventType phaPortalMcMessageSaved                     = new(216, "PortalMessage Saved");
  public static readonly EventType woSetupEmployeeScheduleSaved                = new(217, "woSetupEmployeeSchedule Saved");
  public static readonly EventType woSetupEmployeeScheduleDeSaved              = new(218, "woSetupEmployeeScheduleDe Saved");
  public static readonly EventType unitTurnoverSaved                           = new(219, "unitTurnover Saved");
  public static readonly EventType certRequestSaved                            = new(220, "certRequest Saved");
  
  public static readonly EventType phaPortalLandlordOptionsSetupSaved          = new(221, "Portal Landlord Options Saved");
  public static readonly EventType stMaHomeRentIncomeScheduleTypeSaved         = new(222, "stMaHomeRentIncomeScheduleType Saved");
  public static readonly EventType stMaHomeRentIncomeScheduleDateSaved         = new(223, "stMaHomeRentIncomeScheduleDate Saved");
  public static readonly EventType phaPortalLandlordSaved                      = new(224, "Portal Landlord Saved");
  public static readonly EventType certFormReviewSaved                         = new(225, "Certification Form Review Saved");
  
  public static readonly EventType invoiceSavedAvidInvoiceImport               = new(226, "Invoice Saved From Avid Invoice Import");
  public static readonly EventType phaPortalApplicantOptionsSaved              = new(227, "Portal Applicant Saved");
  public static readonly EventType glTrialBalanceWildCardSaved                 = new(228, "GL Trial Balance Wild Card Saved");
  public static readonly EventType certFinishFormPacketSetupSaved              = new(229, "Cert Finish Form Packet Setup Saved");
  public static readonly EventType familyCertChangeOfOwnershipSaved            = new(230, "Family Cert Change of Ownership");
  
  public static readonly EventType certSignatureRequestSaved                   = new(231, "Cert Signature Request Saved");
  public static readonly EventType invoiceSavedInvoiceImport                   = new(232, "invoice Saved Invoice Import");
  public static readonly EventType woPhaSetupSaved                             = new(233, "work order pha setup saved");
  public static readonly EventType diUploadApi                                 = new(234, "API Uploaded Document");
  public static readonly EventType apVendorPhaSetupSaved                       = new(235, "ap vendor pha setup saved");
  
  public static readonly EventType ecEmailAddressSaved                         = new(236, "ec Email Address Saved");
  public static readonly EventType woSetupPrioritySaved                        = new(237, "Work Order Setup Priority Saved");
  public static readonly EventType woSetupRequestedBySaved                     = new(238, "Work Order Setup Requested By Saved");
  public static readonly EventType woSetupConfigurationSaved                   = new(239, "Work Order Setup Configuration Saved");
  public static readonly EventType familyCertMassCreateBatchSaved              = new(240, "Family Cert Mass Create Batch Saved");
  
  public static readonly EventType familyCertPaymentStandardChangeSaved        = new(241, "Family Cert Payment Standard Change Saved");
  public static readonly EventType familyCertEndOfParticipationSaved           = new(242, "Family Cert End of Participation Saved");
  public static readonly EventType glDepreciableAssetTypeSaved                 = new(243, "GL Depreciable Asset Type Saved");
  public static readonly EventType familyCertMoveOutReasonSaved                = new(244, "50058 Move Out Reason Saved");
  public static readonly EventType stMaMoveOutReasonSaved                      = new(245, "General Move Out Reason Saved");
  
  public static readonly EventType familyCertPhaSetupSaved                     = new(246, "50058 PHA Setup Saved");
  public static readonly EventType familyCertSetupScheduleTypeSaved            = new(247, "Setup Schedule Type Saved");
  public static readonly EventType familyCertSetupScheduleEffectiveDateSaved   = new(248, "Setup Schedule Effective Date Saved");
  public static readonly EventType mergeTemplateSaved                          = new(249, "Letter Merge Template Saved");
  public static readonly EventType hipSubmissionFileSaved                      = new(250, "HIP Submission File Saved");
  
  public static readonly EventType hipSubmissionFileFormJoinSaved              = new(251, "HIP Submission File Form Join Saved");
  public static readonly EventType hipSubmissionErrorSaved                     = new(252, "HIP Submission Error Saved");
  public static readonly EventType finishFormConfigSaved                       = new(253, "Finish Form Config Saved");
  public static readonly EventType finishFormConfigSignatureSaved              = new(254, "Finish Form Config Signature Saved");
  public static readonly EventType tracsPhaSetupSaved                          = new(255, "TRACS PHA Setup Saved");
  
  public static readonly EventType insInspectionSaved                          = new(256, "Inspection Standard Inspection Saved");
  public static readonly EventType insQuestionSaved                            = new(257, "Inspection Standard Question Setup Saved");
  public static readonly EventType insRoomSaved                                = new(258, "Inspection Standard Room Setup Saved");
  public static readonly EventType insInspectionDeficiencyWorkOrderSaved       = new(259, "Inspection Standard Deficiency Work Order Saved");
  public static readonly EventType insInspectionMitigationSaved                = new(260, "Inspection Standard Mitigation Saved");
  
  public static readonly EventType stMaMassCreateBatchSaved                    = new(261, "General Cert Mass Create Batch Saved");
  public static readonly EventType insFamilyNotificationSetupSaved             = new(262, "NSPIRE Family Notification Template Saved");
  public static readonly EventType glPhaSetupSaved                             = new(263, "General Ledger PHA Setup Saved");
  public static readonly EventType phaPortalOppBatchDetailSaved                = new(264, "Deposit Batch Detail Saved");
  public static readonly EventType depositProcessed                            = new(265, "Deposit Processed");
  
  public static readonly EventType phaPortalOppBatchCompletionToggle           = new(266, "Deposit Batch Completed Toggle");
  public static readonly EventType insInspectionDownloaded                     = new(267, "Inspection Standard Inspection Downloaded");
  public static readonly EventType insTemplateTypeProgramSaved                 = new(268, "Inspection Standard Template Type Program Saved");
  public static readonly EventType familyCertProjectContractSaved              = new(269, "Family Cert Project Contract Saved");
  public static readonly EventType insPhaSetupSaved                            = new(270, "NSPIRE PHA Setup Saved");
  
  public static readonly EventType phArSetupReceiptCashAccountSaved            = new(271, "T AR Setup Receipt Cash Account Saved");
  public static readonly EventType phArSetupReceiptCashAccountProjectSaved     = new(272, "T AR Setup Receipt Cash Account Project Saved");
  public static readonly EventType vendorContractorHoursSaved                  = new(273, "Vendor Contractor Hours Saved");
  public static readonly EventType finAssetDepTemplateSaved                    = new(274, "Asset Depreciation Template Saved");
  public static readonly EventType finAssetDepTemplateDetailSaved              = new(275, "Asset Depreciation Template Detail Saved");
  
  public static readonly EventType finAssetDepDistributionDetailSaved          = new(276, "Asset Depreciation Distribution Detail Saved");
  public static readonly EventType phDirectDebitGroupSaved                     = new(277, "PH Direct Debit Group Saved");
  public static readonly EventType phaPortalMcConversationSeen                 = new(278, "Portal Conversation Seen");
  public static readonly EventType wlStatusUpdateBatchSaved                    = new(279, "Waiting List Status Update Batch Saved");
  public static readonly EventType wlStatusUpdateCriteriaSaved                 = new(280, "Waiting List Status Update Criteria Saved");
  
  public static readonly EventType wlStatusUpdateRequestSaved                  = new(281, "Waiting List Status Update Request Saved");
  public static readonly EventType stMaVoucherExtensionSaved                   = new(282, "General Cert Voucher Extension Saved");
  public static readonly EventType stMaPhaSetupSaved                           = new(283, "General Cert PHA Setup Saved");
  public static readonly EventType aiUnitDesignationSaved                      = new(284, "AI Unit Designation Saved");
  public static readonly EventType aiSetAsideGroupSaved                        = new(285, "AI Set Aside Group Saved");
  
  public static readonly EventType aiApplicableFractionGroupSaved              = new(286, "AI Applicable Fraction Group Saved");
//  public static readonly EventType phaWebsiteSetupSaved                        = new(287, "PHA Website Setup Saved"); // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static readonly EventType adminPhaSetupSaved                          = new(288, "Admin Pha Setup Group Saved");
  public static readonly EventType meterReadingUnitTypeSaved                   = new(289, "Meter Reading Unit Type Saved");
  public static readonly EventType meterReadingUtilityEffectiveSaved           = new(290, "Meter Reading Utility Effective Saved");
  
  public static readonly EventType meterReadingUtilityTypeSaved                = new(291, "Meter Reading Utility Type Saved");
  public static readonly EventType saPHASetupSaved                             = new(292, "Super Admin PHA Setup Saved");
  public static readonly EventType wlStatusReasonSaved                         = new(293, "Waiting List Status Reason Saved");
  public static readonly EventType fmContactSaved                              = new(294, "Family Contact Saved");
  public static readonly EventType fmVehicleSaved                              = new(295, "Family Vehicle Saved");
  
  public static readonly EventType fmPetSaved                                  = new(296, "Family Pet Saved");
  public static readonly EventType lockboxByProgramSaved                       = new(297, "Lockbox By Program Saved");
//  public static readonly EventType glAccountLabelSaved                         = new(298, "Financial Account Label Saved"); // COMMENTED OUT in 'enum'
  public static readonly EventType smInfoMessageSaved                          = new(299, "Support Info Message Saved");
  public static readonly EventType certALApplicationListSaved                  = new(300, "Application List Saved");
  
  public static readonly EventType certALCredentialSaved                       = new(301, "Application List Credential Saved");
  public static readonly EventType certALApplicationSaved                      = new(302, "Application List Application Saved");
  public static readonly EventType glStatementSaved                            = new(303, "General Ledger Statement Saved");
  public static readonly EventType glStatementGroupNodeSaved                   = new(304, "General Ledger Statement Group Node Saved");
  public static readonly EventType glStatementColumnNodeSaved                  = new(305, "General Ledger Statement Column Node Saved");
  
  public static readonly EventType certALStatusSaved                           = new(306, "Application List Status Saved");
  public static readonly EventType certALApplicationListStatusSaved            = new(307, "Application List Status Connection Saved");
  public static readonly EventType glStatementPacketSaved                      = new(308, "General Ledger Statement Packet Saved");
  public static readonly EventType poInvoiceLineItemRelationSaved              = new(309, "Purchase Order Line Item Relation Saved");
  public static readonly EventType poLineItemReleaseSaved                      = new(310, "Purchase Order Line Item Release Saved");
  
  public static readonly EventType phPhaSetupSaved                             = new(311, "Tenant Accounting Pha Setup Saved");
  public static readonly EventType hapMiscChargesSaved                         = new(312, "HAP Misc. Charges Saved");
  public static readonly EventType programFinMiscChargesSaved                  = new(313, "Program Misc Charges Connection Saved");
  public static readonly EventType phaUserGroupSaved                           = new(314, "PHA User Group Saved");
  public static readonly EventType phaUserGroupLinkSaved                       = new(315, "PHA User Group Link Saved");
  
  public static readonly EventType ddDataDownloadSaved                         = new(316, "Data Download Saved");
  public static readonly EventType tracsSpecialClaimSaved                      = new(317, "TRACS Special Claim Saved");
  public static readonly EventType tracsSpecialClaimUnpaidRentDamagesSaved     = new(318, "TRACS Special Claim Unpaid Rent/Damages Saved");
  public static readonly EventType tracsSpecialClaimVacancyDuringRentUpSaved   = new(319, "TRACS Special Claim Vacancy During Rent-Up Saved");
  public static readonly EventType tracsSpecialClaimRegularVacancySaved        = new(320, "TRACS Special Claim Regular Vacancy Saved");
  
  public static readonly EventType tracsSpecialClaimDebtServiceSaved           = new(321, "TRACS Special Claim Debt Service Saved");
  public static readonly EventType finDepartmentSaved                          = new(322, "Department Saved");
  public static readonly EventType expirableFileSaved                          = new(323, "Expirable File Saved");
  public static readonly EventType smTicketTagSaved                            = new(324, "Support Manager Ticket Tag Saved");
  
  #endregion
  
  #region Helper Method(s) ...
  
  // IMPORTANT: make sure ALL EventTypes are defined above, within the "Specific EventType Declarations" region.  IF NOT,
  //            then some EvenTypes "may" be left out of their associated dictionary ...
  
  //public static readonly IReadOnlyDictionary<int, EventType> Fields = 
  //  typeof(EventType).GetFields(BindingFlags.Public | BindingFlags.Static)
  //                   .Where(f  => f.FieldType == typeof(EventType))
  //                   .Select(f => (EventType)f.GetValue(null)!)
  //                   .ToImmutableDictionary(e => e.ID);         //NOTE: requires "using System.Collections.Immutable"
  
  private static readonly IReadOnlyDictionary<int, EventType> _fields = 
    typeof(EventType).GetFields(BindingFlags.Public | BindingFlags.Static)
                     .Where(f  => f.FieldType == typeof(EventType))
                     .Select(f => (EventType)f.GetValue(null)!)
                     .ToDictionary(e => e.ID);
   
  public static EventType GetFieldByID(int id) 
    => _fields.TryGetValue(id, out var value) ? value : throw new ArgumentOutOfRangeException($"Unknown event type id: {id}");
   
  //public static IReadOnlyList<EventType> GetAllFields()
  //  => _fields.Values.ToList().AsReadOnly();
   
  public static object GetAllFields(bool asDictionary = false) 
    => asDictionary ? _fields.Values.ToList().AsReadOnly()
                    : _fields.Values.ToDictionary(v => v.ID);
 
  //NOTE: WE DO NOT WANT TO DO THIS ... since the 'index' and 'ID' are not the same
  //public EventType this[int id] 
  //  => GetFieldByID(id);
  
  //NOTE: I'm not sure if I want to do these or not.  I'm trying to figure a way to
  //      build as much 'enum' support for this 'smart-enum' ...
  // int -> EventType (explicit cast only - forces developer to think about it)
  [Obsolete("Prefer EventType.GetById(int) for clarity and future-proofing.", false)]
  public static explicit operator EventType(int id)
    => GetFieldByID(id);
    
  // EventType -> int (implicit or explicit)
  public static implicit operator int(EventType type)
    => type.ID;

  [Obsolete("Prefer EventType.event_type.Description for clarity and future-proofing.", false)]
  public static implicit operator string(EventType type)
    => type.Description;

  //public override string ToString()
  //  => $"ID: {ID}, Description: {Description}";

  public string ToString(bool asJsonObject = false)
    => !asJsonObject ? $"ID: {ID}, Description: {Description}"
                     : $"{{ID: {ID}, Description: {Description}}}";

  private static readonly Lazy<IReadOnlyDictionary<int, EventType>> _properties = new(() => 
    typeof(EventType).GetProperties(BindingFlags.Public | BindingFlags.Static)
                     .Where(p  => p.PropertyType == typeof(EventType))
                     .Select(p => (EventType)p.GetValue(null)!)
                     .ToDictionary(e => e.ID));
  
  public static IReadOnlyDictionary<int, EventType> Properties = _properties.Value;

  public static EventType GetPropertyByID(int id)
    => Properties.TryGetValue(id, out var value) ? value : throw new ArgumentOutOfRangeException($"Unknown event type id: {id}");

  #endregion

  #region enum-like equality: compare based on ID only (ignore Description for uniqueness)

  public override bool Equals(object obj)
    => obj is EventType type && Equals(type);

  public bool Equals(EventType other)
    => other is not null && ID == other.ID;

  public override int GetHashCode()
    => ID.GetHashCode();

  public static bool operator == (EventType left, EventType right)
    => left.Equals(right);
    //=> left?.ID == right?.ID;
  
  public static bool operator != (EventType left, EventType right)
    => !left.Equals(right);
    //=> left?.ID != right?.ID;
  
  #endregion

}

/*************************************************************************************************/
#endregion

#region (Option 2: sealed class ...)
/**************************************************************************************************

public sealed record EventType(int ID, string Description)
{
  #region Specific EventType Declarations ...
  
  public static EventType InspectionSaved                             { get; } = new(  1, "Inspection Saved");
  public static EventType InsertFamily                                { get; } = new(  2, "Family Inserted");
  public static EventType VacatedTenantSaved                          { get; } = new(  3, "Vacated Tenant Saved");
  public static EventType PmPhaSaved                                  { get; } = new(  4, "Agency Saved");
  public static EventType FamilyCertSaved                             { get; } = new(  5, "Family Cert Saved");
  
  public static EventType WaitingListApplicationSaved                 { get; } = new(  6, "Waiting List Application Saved");
  public static EventType RentReasonablenessSaved                     { get; } = new(  7, "Rent Reasonableness Saved");
  public static EventType UpdateFamily                                { get; } = new(  8, "Family Updated");
  public static EventType PortabilityTenantSaved                      { get; } = new(  9, "Section 8 Portability Tenant Saved");
  public static EventType PortabilityAdjustmentSaved                  { get; } = new( 10, "Section 8 Portability Adjustment Saved");
  
  public static EventType RapTrapFileGroupSaved                       { get; } = new( 11, "RAP/T-RAP File Group Saved");
  public static EventType RapTrapFormSaved                            { get; } = new( 12, "RAP/T-RAP Form Saved");
  public static EventType MCSImportedData                             { get; } = new( 13, "MCS Imported Data");
  public static EventType PortabilityMonthlyPayablesSaved             { get; } = new( 14, "Section 8 Portability Monthly Payables Saved");
  public static EventType CountySaved                                 { get; } = new( 15, "County Saved");
  
  public static EventType PortabilityDisbusementSaved                 { get; } = new( 16, "Section 8 Portability Disbursement Saved");
  public static EventType TracsSaved                                  { get; } = new( 17, "TRACS Saved");
  public static EventType PhaSaved                                    { get; } = new( 18, "PHA Saved");
  public static EventType ChecksSaved                                 { get; } = new( 19, "Checks Saved");
  public static EventType gpIncomeLimitSaved                          { get; } = new( 20, "Income Limit Saved");
  
  public static EventType MonthEndSaved                               { get; } = new( 21, "Month End Saved");
  public static EventType LandlordSaved                               { get; } = new( 22, "Landlord Saved");
  public static EventType FamilyCertSetupSaved                        { get; } = new( 23, "Family Cert Setup Saved");
  public static EventType HAPContractNumberSaved                      { get; } = new( 24, "HAP Contract Number Saved");
  public static EventType Section8UnitSaved                           { get; } = new( 25, "Section8 Unit Saved");
  
  public static EventType MCSLandlordImport                           { get; } = new( 26, "MCS Landlord Imported");
  public static EventType ComparableUnitSaved                         { get; } = new( 27, "Comparable Unit Saved");
  public static EventType RequestedUnitSaved                          { get; } = new( 28, "Requested Unit Saved");
  public static EventType PortabilityMasterSaved                      { get; } = new( 29, "Section 8 Portability Setup Saved");
  public static EventType ResidentInfoSaved                           { get; } = new( 30, "Resident Information Saved");
  
  public static EventType ProgramSaved                                { get; } = new( 31, "Program Saved");
  public static EventType ForumMessagePosted                          { get; } = new( 32, "Forum Message Posted");
  public static EventType GlobalValueSaved                            { get; } = new( 33, "Global Value Saved");
  public static EventType FinBankSaved                                { get; } = new( 34, "Bank Saved");
  public static EventType DynamicPageSaved                            { get; } = new( 35, "Dynamic Page Saved");
  
  public static EventType FinAccountSaved                             { get; } = new( 36, "Account Saved");
  public static EventType LandlordAdjustmentSaved                     { get; } = new( 37, "Landlord Adjustment Saved");
//  public static EventType                                             { get; } = new( 38, "");                                //NOT DEFINED in 'enum'
  public static EventType LandlordPayablesPosted                      { get; } = new( 39, "Landlord Payables Posted");
  public static EventType PublicUnitSaved                             { get; } = new( 40, "Public Unit Saved");
  
  public static EventType ProjectSaved                                { get; } = new( 41, "Project Saved");
  public static EventType PublicBuildingSaved                         { get; } = new( 42, "Public Building Saved");
  public static EventType Section8BuildingSaved                       { get; } = new( 43, "Section 8 Building Saved");
  public static EventType PortabilityTenantRentSaved                  { get; } = new( 44, "Section 8 Portability Tenant Rent Saved");
  public static EventType tracsMAT30Saved                             { get; } = new( 45, "Tracs MAT30 Saved");
  
  public static EventType tracsMonthlySubmissionFileSaved             { get; } = new( 46, "Tracs Monthly Submission File Saved");
  public static EventType FinDocumentSaved                            { get; } = new( 47, "Financial Document Saved");
  public static EventType FinControlGroupSaved                        { get; } = new( 48, "Financial Control Group Saved");
  public static EventType FinTransactionSaved                         { get; } = new( 49, "Financial Transaction Saved");
  public static EventType FinGlAccountSaved                           { get; } = new( 50, "Fin Gl Account Saved");
  
  public static EventType FinTransactionTypeSaved                     { get; } = new( 51, "Fin Transaction Type Saved");
//  public static EventType phaFileSaved                                { get; } = new( 52, "PHA File Saved");                  // COMMENTED OUT in 'enum'
  public static EventType MaFormSaved                                 { get; } = new( 53, "MaForm Saved");
  public static EventType StMaUnitSaved                               { get; } = new( 54, "General Certification Unit Saved");  // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static EventType StMaIncomeRangeBaseSaved                    { get; } = new( 55, "StMa Income Range Base Saved");
  
  public static EventType DataExported                                { get; } = new( 56, "Data Exported");
//  public static EventType FamilyCertSubmissionFileSaved               { get; } = new( 57, "50058 Submission File");           // COMMENTED OUT in 'enum'
  public static EventType PhaUserSaved                                { get; } = new( 58, "PHA User Saved");
  public static EventType pmPhaAccountSaved                           { get; } = new( 59, "Agency Account Saved");
  public static EventType GeneralLedgerJournalEntrySaved              { get; } = new( 60, "General Ledger Journal Entry Saved");
  
  public static EventType finHoldReasonSaved                          { get; } = new( 61, "finHoldReason Saved");
  public static EventType finTransPartSelectionSaved                  { get; } = new( 62, "finTransactionPartSelection Saved");
  public static EventType stMaSetupSaved                              { get; } = new( 63, "stMaSetup Saved");
  public static EventType finAdminFee                                 { get; } = new( 64, "finAdminFee Saved");
  public static EventType stMaPaymentStandardTownSaved                { get; } = new( 65, "stMaPaymentStandardTown Saved");
  
  public static EventType stMaPaymentStandardBedSaved                 { get; } = new( 66, "stMaPaymentStandardBed Saved");
  public static EventType hapScheduleAdjustmentUpdate                 { get; } = new( 67, "HAP Schedule Adjustment Update");
  public static EventType imRoomTypeDefinitionSaved                   { get; } = new( 68, "Inspection Manager Room Type Definition Saved");
  public static EventType imQuestionTypeDefinitionSaved               { get; } = new( 69, "Inspection Manager Question Type Definition Saved");
  public static EventType imFailureTypeDefinitionSaved                { get; } = new( 70, "Inspection Manager Failure Type Definition Saved");
  
  public static EventType imFormTypeSaved                             { get; } = new( 71, "Inspection Manager Form Type Saved");
  public static EventType imInspectionSaved                           { get; } = new( 72, "Inspection Manager Inspection Saved");
  public static EventType vendorSaved                                 { get; } = new( 73, "Vendor Saved");
//  public static EventType distributionSaved                           { get; } = new( 74, "Distribution Saved");  // NOT FOUND in the 'eventTypeDescription's Select Case statement
//  public static EventType stCtUnitSaved                               { get; } = new( 75, "stCtUnitSaved");       // COMMENTED OUT in 'enum'
  
  public static EventType saveSignature                               { get; } = new( 76, "Signature Saved");
  public static EventType phMiscChargesSaved                          { get; } = new( 77, "PH Misc Charges Saved");
  public static EventType finPaymentTermsSaved                        { get; } = new( 78, "Payment Terms Saved");
  public static EventType tracsSetupSaved                             { get; } = new( 79, "TRACS Setup Saved");
  public static EventType landlordsMerged                             { get; } = new( 80, "Landlords Merged");
  
  public static EventType finPaymentScheduleSaved                     { get; } = new( 81, "Payment Schedule Saved");
  public static EventType waitingListLotteryProcess                   { get; } = new( 82, "Waiting List Lottery Process");
  public static EventType recurringInvoiceSaved                       { get; } = new( 83, "Recurring Invoice Saved");
  public static EventType imCustomQuestionTypeDefinitionSaved         { get; } = new( 84, "Inspection Manager Custom Question Type Definition Saved");
  public static EventType glReportGroupSaved                          { get; } = new( 85, "General Ledger Report Group Saved");
  
  public static EventType phRepaymentAgreementSaved                   { get; } = new( 86, "Tenant Repayment Agreement Saved");
  public static EventType FinOpenItemRelationSaved                    { get; } = new( 87, "Financial Open Item Relation Saved");
  public static EventType fmCustomValueSetupSaved                     { get; } = new( 88, "Family Custom Value Setup Saved");
  public static EventType smTicketSaved                               { get; } = new( 89, "Support Manager Ticket Saved");
  public static EventType smUserSaved                                 { get; } = new( 90, "Support Manager User Saved");
  
  public static EventType woWorkOrderSaved                            { get; } = new( 91, "Work Order Saved");
  public static EventType woAssetSaved                                { get; } = new( 92, "Work Order Asset Saved");
  public static EventType woInventorySaved                            { get; } = new( 93, "Work Order Inventory Saved");
  public static EventType woTaskSaved                                 { get; } = new( 94, "Work Order Task Saved");
  public static EventType woAssetMaintenanceSaved                     { get; } = new( 95, "Work Order Asset Maintenance Saved");
  
  public static EventType woEmployeeAdjustmentSaved                   { get; } = new( 96, "Work Order Employee Adjustment Saved");
  public static EventType woInventoryAdjustmentSaved                  { get; } = new( 97, "Work Order Inventory Adjustment Saved");
  public static EventType woSetupAssetTypeSaved                       { get; } = new( 98, "Work Order Setup Asset Type Saved");
  public static EventType woSetupNumberSaved                          { get; } = new( 99, "Work Order Setup Number Saved");
  public static EventType woSetupUnitOfMeasureSaved                   { get; } = new(100, "Work Order Setup Unit Of Measure Saved");
  
  public static EventType woSetupInventoryTypeSaved                   { get; } = new(101, "Work Order Setup Inventory Type Saved");
  public static EventType woSetupInventoryLocationSaved               { get; } = new(102, "Work Order Setup Inventory Location Saved");
  public static EventType glTemplateDocSaved                          { get; } = new(103, "glTemplateDoc Saved");
  public static EventType woInventoryUpdateSaved                      { get; } = new(104, "Work Order Inventory Update Saved");
  public static EventType glProjectGroupSaved                         { get; } = new(105, "glProjectGroup Saved");
  
  public static EventType woSetupDefaultCommentsSaved                 { get; } = new(106, "Work Order Setup Default Comments Saved");
  public static EventType rapTrapSetupSaved                           { get; } = new(107, "Rap Trap Setup Saved");
  public static EventType familiesMerged                              { get; } = new(108, "Families Merged");
  public static EventType woSetupLaborTypeSaved                       { get; } = new(109, "Work Order Setup Labor Type Saved");
  public static EventType uaScheduleTypeSaved                         { get; } = new(110, "Utility Allowance ScheduleType Saved");
  
  public static EventType uaScheduleSaved                             { get; } = new(111, "Utility Allowance Schedule Saved");
  public static EventType uaScheduleBedSizeSaved                      { get; } = new(112, "Utility Allowance ScheduleBedSize Saved");
  public static EventType zipGroupSaved                               { get; } = new(113, "Zip Group Saved");
  public static EventType zipGroupItemSaved                           { get; } = new(114, "Zip Group Item Saved");
  public static EventType poPurchaseOrderSaved                        { get; } = new(115, "Purchase Order Saved");
  
  public static EventType poLineItemSaved                             { get; } = new(116, "Purchase Order Line Item Saved");
  public static EventType woSetupReportItemSaved                      { get; } = new(117, "Work Order Setup Report Item Saved");
  public static EventType paymentStandardTypeSaved                    { get; } = new(118, "Payment Standard Type Saved");
  public static EventType phaUserSignatureSaved                       { get; } = new(119, "Pha User Signature Saved");
  public static EventType finStatementSetupSaved                      { get; } = new(120, "Financial Statement Setup Saved");
  
  public static EventType glPayrollReportTypeSetupSaved               { get; } = new(121, "Payroll Report Type Setup Saved");
  public static EventType glPayrollTypeSetupSaved                     { get; } = new(122, "Payroll Type Setup Saved");
  public static EventType glPayrollSummarySaved                       { get; } = new(123, "Payroll Summary Saved");
  public static EventType glPayrollDistributionSaved                  { get; } = new(124, "Payroll Distribution Saved");
  public static EventType glPayrollEmployeeDistributionSaved          { get; } = new(125, "Payroll Employee Distribution Saved");
  
  public static EventType vmsTypeSaved                                { get; } = new(126, "VMS Type Saved");
  public static EventType glPayrollDistributionFieldNumberSaved       { get; } = new(127, "Payroll Distribution Field Number Saved");
  public static EventType meterReadingSaved                           { get; } = new(128, "Meter Reading Saved");
  public static EventType vmsEffectiveDateSaved                       { get; } = new(129, "VMS Effective Date Saved");
  public static EventType phHoEffectiveAdjustmentsSaved               { get; } = new(130, "PH Homeownership Effective Adjustments");
  
  public static EventType phHoAdjustmentsToBalanceSaved               { get; } = new(131, "PH Homeownership Adjustments to Balance");
  public static EventType wlStatusSaved                               { get; } = new(132, "WL Status Saved");
  public static EventType glJESTemplateSaved                          { get; } = new(133, "GL Journal Entry Simple Template Saved");
  public static EventType glJESTemplateDetailSaved                    { get; } = new(134, "GL Journal Entry Simple Template Detail Saved");
  public static EventType voidedCheckSaved                            { get; } = new(135, "Voided Check Saved");
  
  public static EventType recertPacketSetupSaved                      { get; } = new(136, "Recertification Packet Setup Saved");
  public static EventType rfParticipatingProgramSaved                 { get; } = new(137, "RF Participating Program Saved");
  public static EventType woReportItemSaved                           { get; } = new(138, "Work Order Report Item Saved");
  public static EventType projectLookupCodeSaved                      { get; } = new(139, "Project Lookup Code Saved");
  public static EventType payeeSaved                                  { get; } = new(140, "Payee Saved");
  
  public static EventType payeeTemplateSaved                          { get; } = new(141, "Payee Template Saved");
  public static EventType familyCertSubmissionErrorSaved              { get; } = new(142, "Family Cert Submission Error Saved");
//  public static EventType orderingProductSaved                        { get; } = new(143, "Forms Ordering Product Saved");        // COMMENTED OUT in 'enum'
//  public static EventType orderingOrderSaved                          { get; } = new(144, "Forms Ordering Order Saved");          // COMMENTED OUT in 'enum'
//  public static EventType orderingOrderDetailsSaved                   { get; } = new(145, "Forms Ordering Order Details Saved");  // COMMENTED OUT in 'enum'
  
  public static EventType phUtilityBillingSaved                       { get; } = new(146, "PH Utility Billing Saved");
  public static EventType diFolderSaved                               { get; } = new(147, "Document Imaging Folder Saved");
  public static EventType diPHADocumentSaved                          { get; } = new(148, "PHA Document Saved");
  public static EventType diUserDocumentSaved                         { get; } = new(149, "User Document Saved");
  public static EventType woInventoryExpensingSetupSaved              { get; } = new(150, "Work Order Inventory Expensing Setup Saved");
  
  public static EventType woInventoryExpensingSaved                   { get; } = new(151, "Work Order Inventory Expensing Saved");
  public static EventType fmAppointmentSaved                          { get; } = new(152, "Family Appointment Saved");
  public static EventType phDepositMarginSetupSaved                   { get; } = new(153, "Deposit Ticket Margin Setup Saved");
  public static EventType communityServiceDetailSaved                 { get; } = new(154, "Community Service Detail Saved");
  public static EventType glAccountReconciliationSaved                { get; } = new(155, "Account Reconciliation Saved");
  
  public static EventType genericNoteSaved                            { get; } = new(156, "Generic Note Saved");
  public static EventType genericNoteReminderSaved                    { get; } = new(157, "Generic Note Reminder Saved");
  public static EventType gnNoteRemindAllSaved                        { get; } = new(158, "gnNoteRemindAll Saved");
  public static EventType diSignatureDetailSaved                      { get; } = new(159, "Document Imaging Signature Detail Saved");
  public static EventType flatRentAreaTypeSaved                       { get; } = new(160, "Flat Rent Area Type Saved");
  
  public static EventType flatRentAreaDateSaved                       { get; } = new(161, "Flat Rent Area Date Saved");
  public static EventType tracsHistoricalSaved                        { get; } = new(162, "TRACS Historical Saved");
  public static EventType annualHQSCertFormSaved                      { get; } = new(163, "Annual HQS Form Saved");
  public static EventType staticFileSaved                             { get; } = new(164, "Static File Saved");
  public static EventType mspTaskSaved                                { get; } = new(165, "Multi-Step Task Saved");
  
  public static EventType mspStepSaved                                { get; } = new(166, "Multi-Step Step Saved");
  public static EventType perFormAutoAdjustmentSave                   { get; } = new(167, "Per-Form Auto Adjustment Save");
  public static EventType wlApplicantQuestionSaved                    { get; } = new(168, "Applicant Question Saved");
  public static EventType wlApplicantFullAppAnswerSaved               { get; } = new(169, "Applicant Question Full App Answer Saved");
  public static EventType ocOnlineClassPHAUserSaved                   { get; } = new(170, "Online Class PHA User Saved");
  
  public static EventType repaymentAgreementTypeSaved                 { get; } = new(171, "Repayment Agreement Type Saved");
  public static EventType fairMarketRentAreaTypeSaved                 { get; } = new(172, "Fair Market Rent Area Saved");
  public static EventType fairMarketRentAmountSaved                   { get; } = new(173, "Fair Market Rent Amount Saved");
  public static EventType prRequisitionSaved                          { get; } = new(174, "Purchase Requisition Saved");
  public static EventType poShippingAddressSaved                      { get; } = new(175, "Purchase Order Shipping Address Saved");
  
  public static EventType prApprovalSetupSaved                        { get; } = new(176, "PR Approval Setup Saved");
  public static EventType prApprovalCostPhaUserSaved                  { get; } = new(177, "PR Approval Cost Pha User Saved");
  public static EventType prApprovalSaved                             { get; } = new(178, "PR Approval Saved");
  public static EventType tracsRepAgreementLinkSaved                  { get; } = new(179, "Repayment Agreement Link Saved");
  public static EventType fssBalanceAdjustmentSaved                   { get; } = new(180, "FSS Balance Adjustment Saved");
  
  public static EventType vendorContractSaved                         { get; } = new(181, "Vendor Contract Saved");
//  public static EventType familyNotificationSetupSaved                { get; } = new(182, "Family Notification Setup Saved"); // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static EventType familyCertMasterVoucherExtensionSaved       { get; } = new(183, "Family Cert Voucher Extension Saved");
  public static EventType apSelectForPayementSaved                    { get; } = new(184, "AP Select For Payment Saved");
  public static EventType familyCertContractRentIncreaseSaved         { get; } = new(185, "Family Cert Contract Rent Increase");
  
  public static EventType backgroundCheckRequestSaved                 { get; } = new(186, "Background Check Request Saved");
  public static EventType fmPublicSafetyIncidentSaved                 { get; } = new(187, "Public Safety Incident Saved");
  public static EventType update1099BatchSaved                        { get; } = new(188, "Update 1099 Batch Saved");
  public static EventType insert1099BatchSaved                        { get; } = new(189, "Insert 1099 Batch Saved");
  public static EventType woBillingPosted                             { get; } = new(190, "Post Work Order Billing");
  
  public static EventType fssItspGoalSaved                            { get; } = new(191, "FSS ITSP Goal Saved");
  public static EventType fssItspNoteSaved                            { get; } = new(192, "FSS ITSP Note Saved");
  public static EventType certNotificationSaved                       { get; } = new(193, "Notification Saved");
  public static EventType certNotificationProcessSaved                { get; } = new(194, "Notification Process Saved");
  public static EventType finUtilityDistributionSaved                 { get; } = new(195, "Utility Distribution Saved");
  
  public static EventType communityServiceRecurringSaved              { get; } = new(196, "Community Service Recurring Saved");
  public static EventType noteTypeSaved                               { get; } = new(197, "Note Type Saved");
  public static EventType phaPortalSetupSaved                         { get; } = new(198, "Portal Management Setup Saved");
  public static EventType phaPortalFamilyOptionsSaved                 { get; } = new(199, "Portal Family Options Saved");
//  public static EventType phaFamilyPortalPermissionSetup              { get; } = new(200, "Family Portal Permission Setup");  // COMMENTED OUT in 'enum'
  
  public static EventType finTransPartSelectionByProjectSaved         { get; } = new(201, "finTransactionPartSelectionByProject Saved");
  public static EventType glExportFileSaved                           { get; } = new(202, "GL Export File Saved");
  public static EventType famCertUtilScheduleTypeSaved                { get; } = new(203, "FamilyCertUtilityScheduleType Saved");
  public static EventType famCertUtilScheduleDateSaved                { get; } = new(204, "FamilyCertUtilityScheduleDate Saved");
  public static EventType stMaUtilityScheduleTypeSaved                { get; } = new(205, "StMaUtilityScheduleType Saved");
  
  public static EventType stMaUtilityScheduleDateSaved                { get; } = new(206, "StMaUtilityScheduleDate Saved");
  public static EventType phaPortalOppAccSetupSaved                   { get; } = new(207, "phaPortalOppAccSetup Saved");
  public static EventType phaPortalOppAccUserSaved                    { get; } = new(208, "phaPortalOppAccUser Saved");
  public static EventType phaPortalOppCustomerSaved                   { get; } = new(209, "phaPortalOppCustomer Saved");
  public static EventType phaPortalOppBatchSaved                      { get; } = new(210, "phaPortalOppBatch Saved");
  
  public static EventType phaPortalOppPaymentSaved                    { get; } = new(211, "phaPortalOppPayment Saved");
  public static EventType phaPortalOppPaymentDetailSaved              { get; } = new(212, "phaPortalOppPaymentDetail Saved");
  public static EventType sohaDhcdTransmissionFileSaved               { get; } = new(213, "SOHA EOHLC Transmission File Saved");
  public static EventType phaPortalMcConversationSaved                { get; } = new(214, "PortalConversation Saved");
  public static EventType phaPortalMcAttachmentSaved                  { get; } = new(215, "PortalAttachment Saved");
  
  public static EventType phaPortalMcMessageSaved                     { get; } = new(216, "PortalMessage Saved");
  public static EventType woSetupEmployeeScheduleSaved                { get; } = new(217, "woSetupEmployeeSchedule Saved");
  public static EventType woSetupEmployeeScheduleDeSaved              { get; } = new(218, "woSetupEmployeeScheduleDe Saved");
  public static EventType unitTurnoverSaved                           { get; } = new(219, "unitTurnover Saved");
  public static EventType certRequestSaved                            { get; } = new(220, "certRequest Saved");
  
  public static EventType phaPortalLandlordOptionsSetupSaved          { get; } = new(221, "Portal Landlord Options Saved");
  public static EventType stMaHomeRentIncomeScheduleTypeSaved         { get; } = new(222, "stMaHomeRentIncomeScheduleType Saved");
  public static EventType stMaHomeRentIncomeScheduleDateSaved         { get; } = new(223, "stMaHomeRentIncomeScheduleDate Saved");
  public static EventType phaPortalLandlordSaved                      { get; } = new(224, "Portal Landlord Saved");
  public static EventType certFormReviewSaved                         { get; } = new(225, "Certification Form Review Saved");
  
  public static EventType invoiceSavedAvidInvoiceImport               { get; } = new(226, "Invoice Saved From Avid Invoice Import");
  public static EventType phaPortalApplicantOptionsSaved              { get; } = new(227, "Portal Applicant Saved");
  public static EventType glTrialBalanceWildCardSaved                 { get; } = new(228, "GL Trial Balance Wild Card Saved");
  public static EventType certFinishFormPacketSetupSaved              { get; } = new(229, "Cert Finish Form Packet Setup Saved");
  public static EventType familyCertChangeOfOwnershipSaved            { get; } = new(230, "Family Cert Change of Ownership");
  
  public static EventType certSignatureRequestSaved                   { get; } = new(231, "Cert Signature Request Saved");
  public static EventType invoiceSavedInvoiceImport                   { get; } = new(232, "invoice Saved Invoice Import");
  public static EventType woPhaSetupSaved                             { get; } = new(233, "work order pha setup saved");
  public static EventType diUploadApi                                 { get; } = new(234, "API Uploaded Document");
  public static EventType apVendorPhaSetupSaved                       { get; } = new(235, "ap vendor pha setup saved");
  
  public static EventType ecEmailAddressSaved                         { get; } = new(236, "ec Email Address Saved");
  public static EventType woSetupPrioritySaved                        { get; } = new(237, "Work Order Setup Priority Saved");
  public static EventType woSetupRequestedBySaved                     { get; } = new(238, "Work Order Setup Requested By Saved");
  public static EventType woSetupConfigurationSaved                   { get; } = new(239, "Work Order Setup Configuration Saved");
  public static EventType familyCertMassCreateBatchSaved              { get; } = new(240, "Family Cert Mass Create Batch Saved");
  
  public static EventType familyCertPaymentStandardChangeSaved        { get; } = new(241, "Family Cert Payment Standard Change Saved");
  public static EventType familyCertEndOfParticipationSaved           { get; } = new(242, "Family Cert End of Participation Saved");
  public static EventType glDepreciableAssetTypeSaved                 { get; } = new(243, "GL Depreciable Asset Type Saved");
  public static EventType familyCertMoveOutReasonSaved                { get; } = new(244, "50058 Move Out Reason Saved");
  public static EventType stMaMoveOutReasonSaved                      { get; } = new(245, "General Move Out Reason Saved");
  
  public static EventType familyCertPhaSetupSaved                     { get; } = new(246, "50058 PHA Setup Saved");
  public static EventType familyCertSetupScheduleTypeSaved            { get; } = new(247, "Setup Schedule Type Saved");
  public static EventType familyCertSetupScheduleEffectiveDateSaved   { get; } = new(248, "Setup Schedule Effective Date Saved");
  public static EventType mergeTemplateSaved                          { get; } = new(249, "Letter Merge Template Saved");
  public static EventType hipSubmissionFileSaved                      { get; } = new(250, "HIP Submission File Saved");
  
  public static EventType hipSubmissionFileFormJoinSaved              { get; } = new(251, "HIP Submission File Form Join Saved");
  public static EventType hipSubmissionErrorSaved                     { get; } = new(252, "HIP Submission Error Saved");
  public static EventType finishFormConfigSaved                       { get; } = new(253, "Finish Form Config Saved");
  public static EventType finishFormConfigSignatureSaved              { get; } = new(254, "Finish Form Config Signature Saved");
  public static EventType tracsPhaSetupSaved                          { get; } = new(255, "TRACS PHA Setup Saved");
  
  public static EventType insInspectionSaved                          { get; } = new(256, "Inspection Standard Inspection Saved");
  public static EventType insQuestionSaved                            { get; } = new(257, "Inspection Standard Question Setup Saved");
  public static EventType insRoomSaved                                { get; } = new(258, "Inspection Standard Room Setup Saved");
  public static EventType insInspectionDeficiencyWorkOrderSaved       { get; } = new(259, "Inspection Standard Deficiency Work Order Saved");
  public static EventType insInspectionMitigationSaved                { get; } = new(260, "Inspection Standard Mitigation Saved");
  
  public static EventType stMaMassCreateBatchSaved                    { get; } = new(261, "General Cert Mass Create Batch Saved");
  public static EventType insFamilyNotificationSetupSaved             { get; } = new(262, "NSPIRE Family Notification Template Saved");
  public static EventType glPhaSetupSaved                             { get; } = new(263, "General Ledger PHA Setup Saved");
  public static EventType phaPortalOppBatchDetailSaved                { get; } = new(264, "Deposit Batch Detail Saved");
  public static EventType depositProcessed                            { get; } = new(265, "Deposit Processed");
  
  public static EventType phaPortalOppBatchCompletionToggle           { get; } = new(266, "Deposit Batch Completed Toggle");
  public static EventType insInspectionDownloaded                     { get; } = new(267, "Inspection Standard Inspection Downloaded");
  public static EventType insTemplateTypeProgramSaved                 { get; } = new(268, "Inspection Standard Template Type Program Saved");
  public static EventType familyCertProjectContractSaved              { get; } = new(269, "Family Cert Project Contract Saved");
  public static EventType insPhaSetupSaved                            { get; } = new(270, "NSPIRE PHA Setup Saved");
  
  public static EventType phArSetupReceiptCashAccountSaved            { get; } = new(271, "T AR Setup Receipt Cash Account Saved");
  public static EventType phArSetupReceiptCashAccountProjectSaved     { get; } = new(272, "T AR Setup Receipt Cash Account Project Saved");
  public static EventType vendorContractorHoursSaved                  { get; } = new(273, "Vendor Contractor Hours Saved");
  public static EventType finAssetDepTemplateSaved                    { get; } = new(274, "Asset Depreciation Template Saved");
  public static EventType finAssetDepTemplateDetailSaved              { get; } = new(275, "Asset Depreciation Template Detail Saved");
  
  public static EventType finAssetDepDistributionDetailSaved          { get; } = new(276, "Asset Depreciation Distribution Detail Saved");
  public static EventType phDirectDebitGroupSaved                     { get; } = new(277, "PH Direct Debit Group Saved");
  public static EventType phaPortalMcConversationSeen                 { get; } = new(278, "Portal Conversation Seen");
  public static EventType wlStatusUpdateBatchSaved                    { get; } = new(279, "Waiting List Status Update Batch Saved");
  public static EventType wlStatusUpdateCriteriaSaved                 { get; } = new(280, "Waiting List Status Update Criteria Saved");
  
  public static EventType wlStatusUpdateRequestSaved                  { get; } = new(281, "Waiting List Status Update Request Saved");
  public static EventType stMaVoucherExtensionSaved                   { get; } = new(282, "General Cert Voucher Extension Saved");
  public static EventType stMaPhaSetupSaved                           { get; } = new(283, "General Cert PHA Setup Saved");
  public static EventType aiUnitDesignationSaved                      { get; } = new(284, "AI Unit Designation Saved");
  public static EventType aiSetAsideGroupSaved                        { get; } = new(285, "AI Set Aside Group Saved");
  
  public static EventType aiApplicableFractionGroupSaved              { get; } = new(286, "AI Applicable Fraction Group Saved");
//  public static EventType phaWebsiteSetupSaved                        { get; } = new(287, "PHA Website Setup Saved"); // NOT FOUND in the 'eventTypeDescription's Select Case statement
  public static EventType adminPhaSetupSaved                          { get; } = new(288, "Admin Pha Setup Group Saved");
  public static EventType meterReadingUnitTypeSaved                   { get; } = new(289, "Meter Reading Unit Type Saved");
  public static EventType meterReadingUtilityEffectiveSaved           { get; } = new(290, "Meter Reading Utility Effective Saved");
  
  public static EventType meterReadingUtilityTypeSaved                { get; } = new(291, "Meter Reading Utility Type Saved");
  public static EventType saPHASetupSaved                             { get; } = new(292, "Super Admin PHA Setup Saved");
  public static EventType wlStatusReasonSaved                         { get; } = new(293, "Waiting List Status Reason Saved");
  public static EventType fmContactSaved                              { get; } = new(294, "Family Contact Saved");
  public static EventType fmVehicleSaved                              { get; } = new(295, "Family Vehicle Saved");
  
  public static EventType fmPetSaved                                  { get; } = new(296, "Family Pet Saved");
  public static EventType lockboxByProgramSaved                       { get; } = new(297, "Lockbox By Program Saved");
//  public static EventType glAccountLabelSaved                         { get; } = new(298, "Financial Account Label Saved"); // COMMENTED OUT in 'enum'
  public static EventType smInfoMessageSaved                          { get; } = new(299, "Support Info Message Saved");
  public static EventType certALApplicationListSaved                  { get; } = new(300, "Application List Saved");
  
  public static EventType certALCredentialSaved                       { get; } = new(301, "Application List Credential Saved");
  public static EventType certALApplicationSaved                      { get; } = new(302, "Application List Application Saved");
  public static EventType glStatementSaved                            { get; } = new(303, "General Ledger Statement Saved");
  public static EventType glStatementGroupNodeSaved                   { get; } = new(304, "General Ledger Statement Group Node Saved");
  public static EventType glStatementColumnNodeSaved                  { get; } = new(305, "General Ledger Statement Column Node Saved");
  
  public static EventType certALStatusSaved                           { get; } = new(306, "Application List Status Saved");
  public static EventType certALApplicationListStatusSaved            { get; } = new(307, "Application List Status Connection Saved");
  public static EventType glStatementPacketSaved                      { get; } = new(308, "General Ledger Statement Packet Saved");
  public static EventType poInvoiceLineItemRelationSaved              { get; } = new(309, "Purchase Order Line Item Relation Saved");
  public static EventType poLineItemReleaseSaved                      { get; } = new(310, "Purchase Order Line Item Release Saved");
  
  public static EventType phPhaSetupSaved                             { get; } = new(311, "Tenant Accounting Pha Setup Saved");
  public static EventType hapMiscChargesSaved                         { get; } = new(312, "HAP Misc. Charges Saved");
  public static EventType programFinMiscChargesSaved                  { get; } = new(313, "Program Misc Charges Connection Saved");
  public static EventType phaUserGroupSaved                           { get; } = new(314, "PHA User Group Saved");
  public static EventType phaUserGroupLinkSaved                       { get; } = new(315, "PHA User Group Link Saved");
  
  public static EventType ddDataDownloadSaved                         { get; } = new(316, "Data Download Saved");
  public static EventType tracsSpecialClaimSaved                      { get; } = new(317, "TRACS Special Claim Saved");
  public static EventType tracsSpecialClaimUnpaidRentDamagesSaved     { get; } = new(318, "TRACS Special Claim Unpaid Rent/Damages Saved");
  public static EventType tracsSpecialClaimVacancyDuringRentUpSaved   { get; } = new(319, "TRACS Special Claim Vacancy During Rent-Up Saved");
  public static EventType tracsSpecialClaimRegularVacancySaved        { get; } = new(320, "TRACS Special Claim Regular Vacancy Saved");
  
  public static EventType tracsSpecialClaimDebtServiceSaved           { get; } = new(321, "TRACS Special Claim Debt Service Saved");
  public static EventType finDepartmentSaved                          { get; } = new(322, "Department Saved");
  public static EventType expirableFileSaved                          { get; } = new(323, "Expirable File Saved");
  public static EventType smTicketTagSaved                            { get; } = new(324, "Support Manager Ticket Tag Saved");
  
  #endregion
  
  #region Helper Method(s) ...
  
  // IMPORTANT: make sure ALL EventTypes are defined above, within the "Specific EventType Declarations" region.  IF NOT,
  //            then some EvenTypes "may" be left out of their associated dictionary ...
  
  public static readonly Lazy<IReadOnlyDictionary<int, EventType>> _fields = new (() => 
    typeof(EventType).GetFields(BindingFlags.Public | BindingFlags.Static)
                     .Where(f  => f.FieldType == typeof(EventType))
                     .Select(f => (EventType)f.GetValue(null)!)
                     .ToDictionary(e => e.ID));
  
  private static readonly Lazy<IReadOnlyDictionary<int, EventType>> _properties = new(() => 
    typeof(EventType).GetProperties(BindingFlags.Public | BindingFlags.Static)
                     .Where(p  => p.PropertyType == typeof(EventType))
                     .Select(p => (EventType)p.GetValue(null)!)
                     .ToDictionary(e => e.ID));
  
  public static IReadOnlyDictionary<int, EventType> Fields     = _fields.Value;
  public static IReadOnlyDictionary<int, EventType> Properties = _properties.Value;
  
  public static EventType GetFieldByID(int id) 
    => Fields.TryGetValue(id, out var value) ? value : throw new ArgumentOutOfRangeException($"Unknown event type id: {id}");
  
  public static EventType GetPropertyByID(int id) 
    => Properties.TryGetValue(id, out var value) ? value : throw new ArgumentOutOfRangeException($"Unknown event type id: {id}");

  #endregion

  #region enum-like equality: compare based on ID only (ignore Description for uniqueness)

  //public override bool Equals(object obj)
  //  => obj is EventType type && Equals(type);

  public bool Equals(EventType other)
    => other is not null && ID == other.ID;

  public override int GetHashCode()
    => ID.GetHashCode();

  //public static bool operator == (EventType left, EventType right)
  //  => left.Equals(right);
  //  //=> left?.ID == right?.ID;
  //
  //public static bool operator != (EventType left, EventType right)
  //  => !left.Equals(right);
  //  //=> left?.ID != right?.ID;
  
  #endregion
}

**************************************************************************************************/
#endregion
