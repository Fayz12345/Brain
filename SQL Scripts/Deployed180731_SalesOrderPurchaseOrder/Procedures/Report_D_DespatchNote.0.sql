
/****** Object:  StoredProcedure [dbo].[Report_D_DespatchNote]    Script Date: 08/07/2018 12:43:58 ******/
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

exec Report_D_DespatchNote 'ORDER', 'BW0000000016'
exec Report_D_DespatchNoteDataDump 'ORDER', 'BW0000000016'

   SELECT OrderDetailReceiveDetail.ReceiveDetailID, OrderHeader.OrderNumber
     FROM OrderHeader 
    INNER JOIN OrderDetail ON OrderHeader.OrderHeaderID = OrderDetail.OrderHeaderID 
    INNER JOIN OrderDetailReceiveDetail ON OrderDetail.OrderDetailID = OrderDetailReceiveDetail.OrderDetailID
    WHERE (OrderHeader.OrderNumber = @Value)


*/

ALTER PROCEDURE [dbo].[Report_D_DespatchNote]
          
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
   Select ReceiveDetailID from ReceiveDetailItem I
   Inner join [Option] O on I.OptionID = o.OptionID
   inner join Question q on o.QuestionID = q.QuestionID
   where q.Name = 'PO No' and I.Value = @Value
   end

if @QuerryField = 'SORDER'
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
Go
