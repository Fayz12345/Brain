

/****** Object:  StoredProcedure [dbo].[Report_C_DespatchNote]    Script Date: 07/12/2018 15:20:25 ******/
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

exec Report_C_DespatchNote 'AS098'


Exec UpdateESNAttribute '014476002653547','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476002653661','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476000627170','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476002605034','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476000195467','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476002075543','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476001768874','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476000703096','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476002566343','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476001583430','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '013768006366394','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476000198420','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476000200135','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476002065155','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476001579602','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '013768009185049','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476002693535','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '013682000178236','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476001096821','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014077003578582','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014077003927300','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014077002494427','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014077001750209','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014077000239634','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476001772637','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014505000315186','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014505000246589','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476002377402','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '357116071074440','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DN6H5E5EDFHY','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DN6H5EOLDFHY','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DN6H5E1XDFHY','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DN6H5DZ5DFHY','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DN6H5E67DFHY','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DMPFQ74GDFHY','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DN6H5E0EDFHY','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DN6H5E0BDFHY','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFW2CMDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFW26BDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFV4S7DFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFVER6DFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFW25XDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DN6GT7CSDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFW2FBDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFB4BGDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFW2BEDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFW2OSDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '012923007524993','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFW25UDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFW23QDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFV9J6DFJO','PSlip','AS098','jjjj'




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

Create PROCEDURE [dbo].[Report_D_DespatchNote]
          
       @QuerryField nvarchar(50)
     , @Value nvarchar(200)
AS
	SET NOCOUNT ON;
	
BEGIN	

Declare @IDList AS ListOfIDs 
if @QuerryField = 'PSLIP'
   begin
   Insert @IDList
   Select ReceiveDetailID from ReceiveDetailItem I
   Inner join [Option] O on I.OptionID = o.OptionID
   inner join Question q on o.QuestionID = q.QuestionID
   where q.Name = 'PSlip' and I.Value = @Value
   end


if @QuerryField = 'ORDER'
   begin
   Insert @IDList
   SELECT OrderDetailReceiveDetail.ReceiveDetailID
     FROM OrderHeader 
    INNER JOIN OrderDetail ON OrderHeader.OrderHeaderID = OrderDetail.OrderHeaderID 
    INNER JOIN OrderDetailReceiveDetail ON OrderDetail.OrderDetailID = OrderDetailReceiveDetail.OrderDetailID
    WHERE (OrderHeader.OrderNumber = @Value)
   end

------------------------------------------------------------------------------
-- Create the Temp table required to house the pivot table data
Declare @RValue nvarchar(max)
exec Get_Pivot_RawData_AlterStatement @IDList, '#TempTable', @RValue output
Print @RValue
Create Table #TempTable (KeyID numeric(18))
EXEC sp_executesql @RValue
-- Select * from #TempTable
--Print 'Just Printed TempTable'
------------------------------------------------------------------
-- Get the data we are interested in.
Insert #TempTable
--Select * from #TempTable
exec GetData_Pivot_RawData @IDList
------------------------------------------------------------------
-- Proof of concept -- report what we got.
Select ReceiveDetail.ReceiveDetailID
, ReceiveDetail.ClientLocationID
, #TempTable.PSlip
, #TempTable.ShipTo
, ReceiveDetail.ESN
, ReceiveDetail.Version
, ReceiveDetail.SKU as Sku
, #TempTable.Carrier 
, #TempTable.Manufacturer
, #TempTable.Model
, #TempTable.Colour
, #TempTable.Conditions
, #TempTable.[Grade]
, #TempTable.[Out-Bound_WayBill-S]
, CONVERT(int, 1) as Freq
--into Template_DespatchNote
from #TempTable
Inner join REceiveDetail on ReceiveDetail.ReceiveDetailID = #TempTable.KeyID
Order by Manufacturer, Model, Colour, Conditions
------------------------------------------------------------------
-- Clean up. 
--Select ReceiveDetail.ReceiveDetailID
--, ReceiveDetail.ClientLocationID
--, ReceiveDetail.ESN
--, ReceiveDetail.Version
--, ReceiveDetail.SKU as Sku
--, #TempTable.* 
----into Template_DespatchNote
--from #TempTable
--Inner join REceiveDetail on ReceiveDetail.ReceiveDetailID = #TempTable.KeyID
--Order by Manufacturer, Model, Colour, Conditions




--Select * from Template_DespatchNote
Drop Table #TempTable 

-- Select * from #TempTable 
-- All Done	


END
GO/****** Object:  StoredProcedure [dbo].[Report_C_DespatchNoteDataDump]    Script Date: 07/12/2018 15:20:35 ******/
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

Create PROCEDURE [dbo].[Report_D_DespatchNoteDataDump]
          
       @QuerryField nvarchar(50)
     , @Value nvarchar(200)
AS
	SET NOCOUNT ON;
	
BEGIN	


Declare @IDList AS ListOfIDs 
if @QuerryField = 'PSLIP'
   begin
   Insert @IDList
   Select ReceiveDetailID from ReceiveDetailItem I
   Inner join [Option] O on I.OptionID = o.OptionID
   inner join Question q on o.QuestionID = q.QuestionID
   where q.Name = 'PSlip' and I.Value = @Value
   end


if @QuerryField = 'ORDER'
   begin
   Insert @IDList
   SELECT OrderDetailReceiveDetail.ReceiveDetailID
     FROM OrderHeader 
    INNER JOIN OrderDetail ON OrderHeader.OrderHeaderID = OrderDetail.OrderHeaderID 
    INNER JOIN OrderDetailReceiveDetail ON OrderDetail.OrderDetailID = OrderDetailReceiveDetail.OrderDetailID
    WHERE (OrderHeader.OrderNumber = @Value)
   end


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
------------------------------------------------------------------
-- Proof of concept -- report what we got.
--Select ReceiveDetail.ReceiveDetailID
--, ReceiveDetail.ClientLocationID
--, #TempTable.PSlip
--, #TempTable.ShipTo
--, ReceiveDetail.ESN
--, ReceiveDetail.Version
--, ReceiveDetail.SKU as Sku
--, #TempTable.Carrier 
--, #TempTable.Manufacturer
--, #TempTable.Model
--, #TempTable.Colour
--, #TempTable.Conditions
--, #TempTable.[Grade]
--, #TempTable.[Out-Bound_WayBill-S]
--, CONVERT(int, 1) as Freq
----into Template_DespatchNote
--from #TempTable
--Inner join REceiveDetail on ReceiveDetail.ReceiveDetailID = #TempTable.KeyID
--Order by Manufacturer, Model, Colour, Conditions
------------------------------------------------------------------
-- Clean up. 
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
GO





















