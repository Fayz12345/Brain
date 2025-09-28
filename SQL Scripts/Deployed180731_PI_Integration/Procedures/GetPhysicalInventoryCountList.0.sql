/****** Object:  StoredProcedure [dbo].[GetPhysicalInventoryCountList]    Script Date: 7/31/2018 10:31:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

/*

exec GetPhysicalInventoryCountList '000000000003'
Select * from PhysicalInventoryCount

*/

ALTER PROCEDURE [dbo].[GetPhysicalInventoryCountList]
      @Batch nvarchar(50)

AS
BEGIN
	SET NOCOUNT ON;


--Declare @Batch nvarchar(50)
--Select @Batch = '000000000003'
    Declare @IDList AS ListOfIDs 
    
    



    SELECT PhysicalInventoryCountID, ReceiveDetailID
         --, MasterIFSLocationID as MasterLocationID
         --, MasterIFSCondtionID as MasterCondtionID
         , Status, IMEI, Batch, isBatchLocked                     --, isRequestKitted, isRequestUnlock, SetGrade
         , IFSSiteScan as SiteScan
		 , IFSProjectScan as ProjectScan
		 , IFSSite as Site
		 --, IFSProject as Project
		 --, POReceiptDate
		 --, SKU
		 , IFSLocation as ScanningLocation
		 --, IFSCondition as Condition
		 --, IFSConditionCode as ConditionCode
		 , StatusMessage
		 , DuplicateFoundBatches
		 , CreateDate
		 , CreateUser
		 --, ManufacturerID
		 --, ModelID
		 --, CarrierID
		 --, ColourID
     into #Temp1		 
     FROM  PhysicalInventoryCount where Batch = @Batch
     
     Insert @IDList
     Select Distinct ReceiveDetailID from #Temp1 where not ReceiveDetailID is null
     
     ------------------------------------------------------------------------------
     -- Create the Temp table required to house the pivot table data
     Declare @RValue nvarchar(max)
     exec Get_Pivot_RawData_AlterStatement @IDList, '#TempTable', @RValue output
     --Print @RValue
     Create Table #TempTable (KeyID numeric(18))
     EXEC sp_executesql @RValue
     --Select * from #TempTable
     --Print 'Just Printed TempTable'
     ------------------------------------------------------------------
     -- Get the data we are interested in.
     Insert #TempTable
     --Select * from #TempTable
     exec GetData_Pivot_RawData @IDList

--Select * from #TempTable
--return 
Select ReceiveDetail.ReceiveDetailID,
       ReceiveDetail.ClientLocationID,
       Client.ClientID as cClientID,
       Client.Name as cName,
       Client.CompanyName as cCompanyName,
       Client.ContactName as cContactName,
       Client.BillingAddress as cBillingAddress,
       Client.AddressLine1 as cAddressLine1,
       Client.AddressLine2 as cAddressLine2,
       Client.City as cCity,
       Client.StateOrProvince as cStateOrProvince,
       Client.PostalCode as cPostalCode,
       Client.PhoneNumber as cPhoneNumber,
       Client.FaxNumber as cFaxNumber,
       Client.EmailAddress as cEmailAddress,

       Client.RMASuffix as cRMASuffix,
       Client.isVendorGroup as cisVendorGroup,
       Client.ProductTag as cProductTag,
       Client.UserName as cUserName,

       ClientLocation.ClientLocationID as lClientLocationID,
       ClientLocation.Name as lName,
       ClientLocation.StoreNumber as lStoreNumber,
       ClientLocation.StoreSuffix as lStoreSuffix,
       ClientLocation.ScanKey as lScanKey,
       ClientLocation.MacroKey as lMacroKey,
       ClientLocation.Sequence as lSequence,
       ClientLocation.CompanyName as lCompanyName,
       ClientLocation.ContactName as lContactName,
       ClientLocation.BillingAddress as lBillingAddress,
       ClientLocation.AddressLine1 as lAddressLine1,
       ClientLocation.AddressLine2 as lAddressLine2,
       ClientLocation.City as lCity,
       ClientLocation.StateOrProvince as lStateOrProvince,
       ClientLocation.PostalCode as lPostalCode,
       ClientLocation.PhoneNumber as lPhoneNumber,
       ClientLocation.FaxNumber as lFaxNumber,
       ClientLocation.EmailAddress as lEmailAddress,
       ClientLocation.UserName as lUserName
	   , ReceiveDetail.ProjectTag
       , ReceiveDetail.ESN
       , ReceiveDetail.Version
       , ReceiveDetail.SKU as Skux
       , #TempTable.* 
       --into Template_DespatchNote
       into #Temp2
       from #TempTable
       Inner join REceiveDetail on ReceiveDetail.ReceiveDetailID = #TempTable.KeyID
       Inner Join ClientLocation on ClientLocation.ClientLocationID = ReceiveDetail.ClientLocationID
       inner join Client on Client.ClientID = ClientLocation.ClientID
       Order by Manufacturer, Model, Colour


   SELECT A.PhysicalInventoryCountID
        , A.ReceiveDetailID
        , B.ClientLocationID as dClientLocationID
        , B.cClientID as ClientID
        , B.cName as ClientName
        , B.cContactName as ClientContactName
        , B.cBillingAddress as ClientBillingAddress
        , B.ProjectTag as dProjectTag
        --, A.ProjectScan
        , A.Status
        , A.IMEI
        , B.Version
        , A.Batch
        , A.isBatchLocked
        , A.ScanningLocation
        , A.StatusMessage
        , A.DuplicateFoundBatches
        , A.CreateDate
        , A.CreateUser
        --, B.*
        , B.DeviceCost as dDeviceCost
        , B.Carrier as dCarrier
        , B.Manufacturer as dManufacturer
        , B.Model as dModel
        , B.Colour as dColour
        , B.*
     FROM #Temp1 A 
     LEFT OUTER JOIN #Temp2 B ON A.ReceiveDetailID = B.ReceiveDetailID

 Drop Table #Temp1
 Drop Table #Temp2
 Drop Table #TempTable
END
Go

