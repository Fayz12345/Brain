/****** Object:  StoredProcedure [dbo].[Report_UnitView]    Script Date: 02/05/2018 22:07:31 ******/
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

*/

Create PROCEDURE [dbo].[Report_C_DespatchNote]
          
      @PSlip nvarchar(200)
AS
	SET NOCOUNT ON;

Begin	
	
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
from #TempTable
Inner join REceiveDetail on ReceiveDetail.ReceiveDetailID = #TempTable.KeyID
Order by Manufacturer, Model, Colour, Conditions
------------------------------------------------------------------
-- Clean up. 
Drop Table #TempTable 

-- Select * from #TempTable 
-- All Done	
return
END

Go
