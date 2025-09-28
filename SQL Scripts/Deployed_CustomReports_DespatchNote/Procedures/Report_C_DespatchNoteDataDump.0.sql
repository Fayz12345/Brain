/****** Object:  StoredProcedure [dbo].[Report_C_DespatchNoteDataDump]    Script Date: 02/13/2018 21:08:19 ******/
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

exec Report_C_DespatchNoteDataDump 'AS098'

[Out-Bound WayBill-S]
Select * from QUestion where Name = 'Out-Bound WayBill-S'
Select * from [option] where QuestionID = 248
Select * from ReceiveDetailItem where optionid = 2252

Select * from QUestion where Name = 'PSlip'
Select * from [option] where QuestionID = 565
Select * from ReceiveDetailItem where optionid = 7479 and Value = 'AS098'

Select * from QUestion where Name like 'Out%'



Declare @RDID numeric(18)
-- Select ReceiveDetailID, convert(int, 0) as process into #Tempx from ReceiveDetailItem where optionid = 7479 and Value = 'AS098'
while exists(Select * from #Tempx where process = 0)
      begin
      select @RDID = ReceiveDetailID from #Tempx where process = 0
      Update #Tempx set process = 1 where ReceiveDetailID = @RDID
      exec UpdateESNAttribute_NoProjectRestriction_BYID @RDID, 'Out-Bound WayBill-S','0wb9IIyyD34i','jimx'
      end

Update #Tempx set process = 0


exec Report_C_DespatchNote 'AS098'

*/

Create PROCEDURE [dbo].[Report_C_DespatchNoteDataDump]
          
      @PSlip nvarchar(200)
AS
	SET NOCOUNT ON;
	
BEGIN	
-- Start
--Declare @ProjectID numeric(18)
--Select @ProjectID = 7
-----------------------------------------------------------------------------
-- Get the Keys for the Data we want to report on.
Declare @IDList AS ListOfIDs 
Insert @IDList
Select ReceiveDetailID from ReceiveDetailItem I
Inner join [Option] O on I.OptionID = o.OptionID
inner join Question q on o.QuestionID = q.QuestionID
where q.Name = 'PSlip' and I.Value = @PSlip
--Select top 500 ReceivedetailID from ReceiveDetail where Version = '000' order by Createdate -- ReceiveDetailID in (47971,47972,47973,47974,47975,47976,47977,47978,47979,47980,47981,47982,47983)




------------------------------------------------------------------------------
-- Create the Temp table required to house the pivot table data
Declare @RValue nvarchar(max)
exec Get_Pivot_RawData_AlterStatement @IDList, '#TempTable', @RValue output
--Print @RValue
Create Table #TempTable (KeyID numeric(18))
EXEC sp_executesql @RValue
-- Select * from #TempTable
--Print 'Just Printed TempTable'
------------------------------------------------------------------
-- Get the data we are interested in.
Insert #TempTable
--Select * from #TempTable
exec GetData_Pivot_RawData @IDList


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

, ReceiveDetail.ESN
, ReceiveDetail.Version
, ReceiveDetail.SKU as Sku
, #TempTable.* 
--into Template_DespatchNote
from #TempTable
Inner join REceiveDetail on ReceiveDetail.ReceiveDetailID = #TempTable.KeyID
Inner Join ClientLocation on ClientLocation.ClientLocationID = ReceiveDetail.ClientLocationID
inner join Client on Client.ClientID = ClientLocation.ClientID
Order by Manufacturer, Model, Colour




--Select * from Template_DespatchNote
Drop Table #TempTable 

-- Select * from #TempTable 
-- All Done	
END
go
