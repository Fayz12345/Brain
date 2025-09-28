
/****** Object:  StoredProcedure [dbo].[IFS_GenerateInvtTran]    Script Date: 05/11/2015 12:41:30 ******/
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

exec IFS_GenerateInvtTran 1118447,  'LOC', 'jmccomb'

*/



ALTER PROCEDURE [dbo].[IFS_GenerateInvtTran]
      @mReceiveDetailID numeric(18),
      @mS nvarchar(1),
      @mL nvarchar(1),
      @mC nvarchar(1),            
      @mUserName nvarchar(50)

AS
BEGIN
	SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

Declare @IsFirst smallint

Declare @LastFromSku nvarchar(25)
Declare @LastFromLocation [nvarchar](50)
Declare @LastFromCondition [nvarchar](50)
Declare @LastToSku nvarchar(25)
Declare @LastToLocation [nvarchar](50)
Declare @LastToCondition [nvarchar](50)

Declare @FromSku nvarchar(25)
Declare @FromLocation [nvarchar](50)
Declare @FromCondition [nvarchar](50)
Declare @ToSku nvarchar(25)
Declare @ToLocation [nvarchar](50)
Declare @ToCondition [nvarchar](50)

Declare @ToSKUID numeric(18)
Declare @ToLocationID numeric(18)
Declare @ToConditionID numeric(18)

Declare @IFSSite [nvarchar](5)
Declare @IFSProject [nvarchar](10)
Declare @POVendor [nvarchar](50)
Declare @PONumber [nvarchar](12)
Declare @POReceiptDate [nvarchar](10)
Declare @POLine [nvarchar](4)	
Declare @POCostString [nvarchar](20)	
Declare @POCost [numeric](18, 5)
Declare @ProcessID numeric(18)

Declare @OriginalReceiptDate [Datetime]


Select @IFSProject = IFSProject, @IFSSite = IFSSite, @OriginalReceiptDate = ReceiveDate from ReceiveDetail Inner join ClientLocation on ClientLocation.ClientLocationID = ReceiveDetail.ClientLocationID Where ReceiveDetail.ReceiveDetailID = @mReceiveDetailID
Select @ProcessID = dbo.GetReceivedDetailCurrentProcessID(ReceiveDetail.ReceiveDetailID) from ReceiveDetail where ReceiveDetailID = @mReceiveDetailID
Select @PONumber = ''
Select @POLine = ''
Select @POCostString = ''
Select @POCost = 0

-- Get the From and To SKU -----------------------------------------------------------------
Select Top 1 @ToSKU = SKU, @ToSKUID = ReceiveDetailSKUChangeLogID From ReceiveDetailSKUChangeLog where ReceiveDetailID = @mReceiveDetailID Order by CreateDate Desc
Select @FromSku = @ToSKU
-- Get the From and To Locations -----------------------------------------------------------------
Select Top 1 @ToLocation = IFSLocation, @ToLocationID = ReceiveDetailIFSLocationLogID  From ReceiveDetailIFSLocationLog where ReceiveDetailID = @mReceiveDetailID Order by CreateDate Desc
Select @FromLocation = @ToLocation 
-- Get the From and To Condition -----------------------------------------------------------------
Select Top 1 @ToCondition = IFS_Condition, @ToConditionID = ReceiveDetailConditionChangeLogID  From ReceiveDetailConditionChangeLog where ReceiveDetailID = @mReceiveDetailID Order by CreateDate Desc
Select @FromCondition = @ToCondition 


-- If the Source is "LOC" then we need a from location, otherwise stays is equal to the "TO" location 
if (@mL = 'L')
    begin
    Select @FromLocation = ''
    Select Top 1 @FromLocation = IFSLocation From ReceiveDetailIFSLocationLog where ReceiveDetailID = @mReceiveDetailID and ReceiveDetailIFSLocationLogID != @ToLocationID Order by CreateDate Desc
    end

-- If the Source is "COND" then we need a from Condition, otherwise it stays equal to the "TO" Condition 
if (@mC = 'C')
    begin
    Select @FromCondition = ''
    Select Top 1 @FromCondition = IFS_Condition From ReceiveDetailConditionChangeLog where ReceiveDetailID = @mReceiveDetailID and ReceiveDetailConditionChangeLogID != @ToConditionID Order by CreateDate Desc
    end
-- Get the Value Percentage

-- If the Source is "SKU" then we need a from SKU, otherwise it is equal stays the "TO" SKU
if (@mS = 'S')
    begin
    select @FromSKU = ''
    Select Top 1 @FromSKU = SKU From ReceiveDetailSKUChangeLog where ReceiveDetailID = @mReceiveDetailID and ReceiveDetailSKUChangeLogID != @ToSKUID Order by CreateDate Desc
    end


------------------------------------------------------------------------------------------------------
-- Now we need to add the new transaction.
Select @IsFirst = 1       -- Assume all transactions are the first
-- IF we have an earlier transaction for thie Device, then it is not the first one
if exists(Select * From InvtTran_IFS where ReceiveDetailID = @mReceiveDetailID)
   Select @IsFirst = 0

if LEN(@ToLocation) = 0 
   Select @IsFirst = 2            -- SOLD, Left the building
   
   
if @IsFirst = 1
   begin
   Select @PONumber = [dbo].[GetReceivedQuestionAnswerString](@mReceiveDetailID,'IFS PO Number')
   Select @POLine = [dbo].[GetReceivedQuestionAnswerString](@mReceiveDetailID,'IFS PO Line Number')
   Select @POCostString = [dbo].[GetReceivedQuestionAnswerString](@mReceiveDetailID,'IFS PO Unit Cost')
   Select @POVendor =  [dbo].[GetReceivedQuestionAnswerString](@mReceiveDetailID,'IFS PO Vendor')
   Select @POReceiptDate = [dbo].[GetReceivedQuestionAnswerString](@mReceiveDetailID,'IFS PO Receipt Date')
   Select @POCost = 0

   if (len(@POReceiptDate) = 0)
       Select @POReceiptDate = convert(nvarchar(10), @OriginalReceiptDate, 101)

   if ISNUMERIC(@POCostString) = 1
      Select @POCost = CONVERT(numeric(18,5),@POCostString)   

   if len(@PONumber) = 0
      begin
      Declare @PONumberx nvarchar(50)
      Declare @POLinex nvarchar(5)
	  Declare @Sku nvarchar(25)
      Select @Sku = case when len(@ToSku) > 0 then @ToSku else @FromSku end

      Exec GetNextPONumberAndLine_B
           @IFSSite,
           @IFSProject,
		   @Sku,
           @POVendor,
           @mUserName,
           @PONumberx output,
           @POLinex Output
      Select @PONumber = @PONumberx
      Select @POLine = @POLinex
      exec [dbo].[UpdateESNAttribute_BYID] @mReceiveDetailID, 'IFS PO Number', @PONumber, @mUserName
      exec [dbo].[UpdateESNAttribute_BYID] @mReceiveDetailID, 'IFS PO Line Number', @POLine, @mUserName
	  end
   end   

-- Get the data from the last one.   
Select Top 1 @LastFromLocation = FromLocation, @LastToLocation = ToLocation,  
             @LastFromCondition = FromCondition, @LastToCondition = ToCondition, 
             @LastFromSku = FromSku, @LastToSku = ToSku
 From InvtTran_IFS where ReceiveDetailID = @mReceiveDetailID Order by CreatedDate Desc

if isnull(@LastFromLocation,'') != isnull(@FromLocation,'') or isnull(@LastToLocation,'') != isnull(@ToLocation,'')
or isnull(@LastFromCondition,'') != isnull(@FromCondition,'') or isnull(@LastToCondition,'') != isnull(@ToCondition,'')
or isnull(@LastFromSKU,'') != isnull(@FromSKU,'') or isnull(@LastToSKU,'') != isnull(@ToSKU,'')
   begin
   if isnull(@FromLocation,'') != isnull(@ToLocation,'')
   or isnull(@FromCondition,'') != isnull(@ToCondition,'')
   or isnull(@FromSKU,'') != isnull(@ToSKU,'')
   Insert InvtTran_IFS (ReceiveDetailID, FromSKU, Quantity
                      , IFSSite, IFSProject, POVendor, PONumber,POReceiptDate, POLine, POCost
                      , FromLocation, FromCondition, ToSku, ToLocation, ToCondition, CreatedDate, CreateUser, CreateSource, ProcessID, ToSkuID, ToLocationID, ToConditionID, Directive )
   Values (@mReceiveDetailID, @FromSku, 1
          ,@IFSSite
          ,@IFSProject
		  ,@POVendor
          ,@PONumber
		  ,@POReceiptDate
          ,@POLine
          ,@POCost
          ,@FromLocation,@FromCondition, @ToSku, @ToLocation, @ToCondition, getdate(), @mUserName, ltrim(rtrim(@mS)) + ltrim(rtrim(@mL)) + ltrim(rtrim(@mC)), @ProcessID, @ToSKUID,@ToLocationID ,@ToConditionID, @IsFirst ) 
   end
END




GO


