/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2017 (14.0.3023)
    Source Database Engine Edition : Microsoft SQL Server Standard Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [BWUK_Sandbox]
GO
/****** Object:  StoredProcedure [dbo].[Utility_RestDatabaseToDefault]    Script Date: 2/11/2019 8:30:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------

--------------------------------------------------------------------------------------
/*

-- Delete the Order entry Data
exec Utility_RestDatabaseToDefault 0

-- Delete the Cycle Count Records
exec Utility_RestDatabaseToDefault 1

--  Delete the Device specific records.
exec Utility_RestDatabaseToDefault 2

-- Delete the Client Demographic portion of the database
exec Utility_RestDatabaseToDefault 3



-- Delete the asp User Accounts
exec Utility_RestDatabaseToDefault 4

-- Reset System Data
exec Utility_RestDatabaseToDefault 5
*/
--------------------------------------------------------------------------------------
--
*/




ALTER PROCEDURE [dbo].[Utility_RestDatabaseToDefault]
       @Level int 

 AS

Begin
--SET NOCOUNT ON
if @Level = 0
   begin
   print '------ Deletng Order Entry, Pick List records'
   Truncate Table OrderCompany
   Truncate Table OrderDetailReceiveDetail
   Truncate Table OrderMoveChangeLog
   Truncate Table OrderMoveLog
   Truncate Table OrderMoveShipToDoneLog
   Truncate Table OrderDetail
   Truncate Table OrderHeader
   print '-- DONE.'
   end


if @Level = 1
   begin
   print '------ Deletng Cycle Count Data'
   Truncate table CycleCountInventoryCount
   Truncate Table CCRunBatchesScanResult
   Truncate table CycleCountInventoryCountParts
   Truncate table CycleInventoryCountControlDetail
   Truncate table CycleInventoryCountControlDetailParts
   Truncate table CycleInventoryCountHeader
   Truncate table CycleInventoryCountHeaderParts
   Truncate table CycleInventoryCountIterationDetail
   Truncate table CycleInventoryCountIterationDetailParts

   Truncate table CycleInventoryCountIterationHeader
   Truncate table CycleInventoryCountIterationHeaderParts
   Truncate table CycleInventoryCountTemplateHeader
   Truncate table CycleInventoryCountTemplateHeaderDetail
   Truncate table CycleInventoryCountTemplateHeaderParts
   Truncate table CycleInventoryCountTemplateHeaderPartsDetail


   Truncate table CycleInventoryCountIterationDetail
   Truncate table CycleInventoryCountIterationDetailParts
   
   print '-- DONE.'
   end

if @Level = 2
   begin
   print '------ Deletng Device Data'
Truncate table REceiveDetailItem_Archive
Truncate table ReceiveDetailItem_Archive_01
Truncate table ReceiveDetailItem_Archive_02
Truncate table ReceiveDetailItem_Deleted_01
Truncate table ReceiveDetailItemBulk
Truncate table ReceiveDetailNotationLog
Truncate table ReceiveDetailPartsUsage
Truncate table ReceiveDetailPreReceiveAttribute
Truncate table ReceiveDetailProcessLog
Truncate table ReceiveDetailSaveLog
Truncate table ReceiveDetailSaveTransaction
Truncate table ReceiveDetailSKUChangeLog
Truncate table ReceiveDetailTriggerLog
Truncate table ReceiveDetailUtilityRunLog
Truncate table ReceiveDetailVersionChangeLog
Truncate table ReceiveDetailVersionChangeLogSource
Truncate table ReceiveDetailIFSInOutLog
Truncate table Template_DespatchNote
Truncate table ReceiveDetailXBINXDetailLog
Truncate table ReceiveDetailXBINXHeaderLog
Truncate table ReceiveDetailXBinXLocationLog
Truncate table ReceiveDockingCounts
Truncate table ReservedAvailableStock
Truncate table ReceiveDetailConditionChangeLog
Truncate table ReceiveDetailIFSLocationLog
Truncate table ReceiveDetailExcelUploadLog
Truncate table ReceiveDetailItem
Truncate table ReceiveDetail
Truncate table ReceiveHeader
-----------------------------------------------------------------------------Truncate table ReceiveDetailPreReceive
Delete ReceiveDetailPreReceive

Truncate table BishopCatalogueSendLog
Truncate table Template_Utility_AnalyzeData
Truncate table RequestProcessCompletionList


Truncate table ScanCodeLog
Truncate table StatisticalRawBucketData
Truncate table StatisticalRawBucketDataGrandTotal
Truncate table StatisticalRawData
Truncate table SystemLog
Truncate table SystemTimeLog
Truncate table MasterProcessWaitTimes
--Truncate table MasterCarrierManufacturerLookup
Truncate Table MasterModelMemoryLookup
Truncate table IFS_GatherLog
Truncate table IFSPickListOrderCompany
Truncate table IFSPickListOrderDetail
Truncate table IFSPickListOrderHeader
Truncate table IFSPurchaseOrderDetail
Truncate table IFSPurchaseOrderHeader
Truncate table IFSXMLFileHeader
Truncate table InvtTran_IFS
Truncate table InvtTran_IFS_Analyze
Truncate table InvtTran_IFS_ErrorLog
Truncate table InvtTran_IFSDirectiveLog
Truncate table JimErrorLog
Truncate table LocationLog

Truncate table Discrepancy
   print '-- DONE.'



--Truncate table JimTempSwap
--Truncate table JimTempSwap2


  
   End



if @Level = 3
   begin
   print '------ Deletng Client Data'

   Truncate table ClientAnswerRestrict
   Truncate table ClientBillingPoints
   Truncate table ClientProcessDependencies
   Truncate table ClientProjectDependencies
   Truncate table ClientQuestionRestrict
   ------------------------------------------------------------------------------Truncate table ClientLocation
   Delete ClientLocation
   ------------------------------------------------------------------------------Truncate table Client
   Delete Client

   Truncate table EmailLog
   Truncate table MasterBucketTransactions
   print '-- DONE.'
   end





if @Level = 4
   begin
   print '------ Deletng User Logins Data'
   -- This will remove all the Login User records.
   DECLARE @UserId uniqueidentifier
   Declare @UserName nvarchar(256)
   Declare @UserTableID numeric(18,0)
   Select UserName into #T from aspnet_Users WHERE UserName in ('jmccomb','Admin','fali','Tim')
   
   While exists(Select * from aspnet_Users where not UserName in (Select * from #T))
         begin
         Select top 1 @UserId = UserID, @UserName = Username
   	            FROM aspnet_Users WHERE not UserName in (Select * from #T)
         --Insert #T (UserName) values (@UserName)
   	     print @UserName
   
   
   	     Select @UserTableID = UserTableID from UserTable where UserName = @UserName
         Delete from UserAccessTable where UserTableID = @UserTableID
 	     Delete from UserTable where UserTableID = @UserTableID
   
         DELETE FROM aspnet_Profile WHERE UserID = @UserId
         DELETE FROM aspnet_UsersInRoles WHERE UserID = @UserId
         DELETE FROM aspnet_PersonalizationPerUser WHERE UserID = @UserId
         DELETE FROM dbo.aspnet_Membership WHERE UserID = @UserId
         DELETE FROM aspnet_users WHERE UserID = @UserId
   
   	  end
   --Select * from #T
   Drop Table #T
   print '-- DONE.'
   End



if @Level = 5
   begin
   print '------ Reset System Data'
   Update SystemData set Data = '0' where DataKey = 'PurchaseOrderNumber'
   Update SystemData set Data = '' where DataKey = 'PartReqEmailAddress'
   Update SystemData set Data = '' where DataKey = 'BgEmailProcesIntrval'
   Update SystemData set Data = '' where DataKey = 'DiscrepancyClientID'
   Update SystemData set Data = '' where DataKey = 'PartRetEmailAddress'
   Update SystemData set Data = '0' where DataKey = 'PIDeviceBatchNumber'
   Update SystemData set Data = '0' where DataKey = 'CCDeviceBatchNumber'
   Update SystemData set Data = 'System Version' where DataKey = 'System'
   Update SystemData set Data = '0' where DataKey = 'WorkOrderNumber'
   Update SystemData set Data = '8' where DataKey = 'BinSeed'
   Update SystemData set Data = 'Company Name' where DataKey = 'CompanyName'
   Update SystemData set Data = 'Address Line 1' where DataKey = 'CompanyAddLine1'
   Update SystemData set Data = '' where DataKey = 'CompanyAddLine2'
   Update SystemData set Data = 'City' where DataKey = 'CompanyAddCity'
   Update SystemData set Data = 'Prov' where DataKey = 'CompanyAddProv'
   Update SystemData set Data = 'PCode' where DataKey = 'CompanyAddPostal'
   Update SystemData set Data = '(555)555-5555' where DataKey = 'CompanyPhone'
   Update SystemData set Data = '(5555)555-5555' where DataKey = 'CompanyFax'
   Update SystemData set Data = '' where DataKey = 'CompanyEmail'
   Update SystemData set Data = '' where DataKey = 'CompanyWebSite'
   Update SystemData set Data = '' where DataKey = 'OrderEntryEmail'
   --Update SystemData set Data = 'Manufacturer' where DataKey = 'CloneToMSC'
   --Update SystemData set Data = 'Model' where DataKey = 'CloneToMSC'
   --Update SystemData set Data = 'Carrier' where DataKey = 'CloneToMSC'
   --Update SystemData set Data = 'Colour' where DataKey = 'CloneToMSC'
   Update SystemData set Data = '' where DataKey = 'RedTagEmail'
   Update SystemData set Data = '' where DataKey = 'YellowTagEmail'
   Update SystemData set Data = '06:00 AM' where DataKey = 'StartTime'
   Update SystemData set Data = '04:00 PM' where DataKey = 'EndTime'
   Update SystemData set Data = '1' where DataKey = 'TodayTomorrowTime'
   Update SystemData set Data = '1' where DataKey = 'IFSBatchNumber'
   Update SystemData set Data = '' where DataKey = 'SupportEmail'
   Update SystemData set Data = 'Y' where DataKey = 'AllowMasterTableDel'
   Update SystemData set Data = 'Welcome to Online Inventory System' where DataKey = 'WelcomeText'
   Update SystemData set Data = '#E0FFFF' where DataKey = 'BrandBackgrndColour'
   Delete SystemData where DataKey = 'ARCHWAY_SHIPRETURN_'
   Delete SystemData where DataKey = 'ARCHWAY_MEID_DEFECT_'
   Delete SystemData where DataKey = 'ARCHWAY_DEFECT_'
   print '-- DONE.'
   End
-- Client Related Stuff.
/*
Client
ClientAnswerRestrict
ClientBillingPoints
ClientLocation
ClientProcessDependencies
ClientProjectDependencies
ClientQuestionRestrict


*/

-- Misc to clean out.
/*

Delete AutoGeneratedPONumbers
Delete AutoGeneratedPONumbers_B
Delete BinLocation
Delete BishopCatalogueSendLog
Delete CCRunBatchesScanResult
Delete DataDrop
Delete DataDropColumn
Delete MasterProcessWaitTimes
Delete UnitItemStatus

SystemLog
SystemTimeLog
ScanCodeLog
EmailLog
RequestProcessCompletionList
ReservedAvailableStock

*/

-- Delete all users except Admin and jmccomb
/*
aspnet_Applications
aspnet_Membership
aspnet_Paths
aspnet_PersonalizationAllUsers
aspnet_PersonalizationPerUser
aspnet_Profile
aspnet_Roles
aspnet_SchemaVersions
aspnet_Users
aspnet_UsersInRoles
aspnet_WebEvent_Events
RoleAccessTable
RoleMenuAccess
UserAccessTable
UserStatus
UserTable

MasterTableAcccessList

*/


-- SET NOCOUNT OFF
End