/****** Object:  StoredProcedure [dbo].[OrderEntry_PickDevice]    Script Date: 06/11/2018 15:59:37 ******/
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

Select * from OrderHeader

Select ESN, I.* from ReceiveDetailItem I
Inner Join ReceiveDetail R on R.ReceiveDetailID = I.ReceiveDetailID where value = '003'

356761053605843
013845001038283

Declare @ReturnMessage nvarchar(500)
exec OrderEntry_PickDevice 15, 'cccc', '013845001038283', 'jmccomb',@ReturnMessage Output
Print @ReturnMessage









Select * from OrderDetailReceiveDetail where OrderDetailID = 35
Select * from OrderDetail where OrderHeaderID = 15
'O1361'
Select * from dbo.fn_SplitDistinct('O1361 O1437 O2565 O1446 O1403',' ') 

Select i.OptionID, o.Scankey, o.Name as ABBR, Q.Name as QName from ReceiveDetailItem  I
Inner join [Option] o on I.OptionID = o.OptionID
Inner join Question Q on Q.QuestionID = o.QUestionID
where ReceiveDetailID in (Select ReceiveDetailID from ReceiveDetail where ESN = '356761053605843')
 
 
*/



Create PROCEDURE [dbo].[OrderEntry_PickDevice]
	  @OrderHeaderID numeric(18),
	  @CartonNumber nvarchar(50),
	  @IMEI nvarchar(50),
	  @mUserName nvarchar(50),
	  @ReturnMessage nvarchar(500) Output


AS
BEGIN
	SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

if LEN(@CartonNumber) = 0
   begin 
   Select @ReturnMessage = 'Error:No Carton Number Given'   
   return
   end

if LEN(@IMEI) = 0
   begin 
   Select @ReturnMessage = 'Error:No IMEI Given'   
   return
   end


if ISNULL(@OrderHeaderID, -1) = -1
   begin 
   Select @ReturnMessage = 'Error:No Order Header Given'   
   return
   end

if not exists(Select * from OrderHeader where OrderheaderID = @OrderHeaderID)
   begin 
   Select @ReturnMessage = 'Error:No Order Header Found'   
   return
   end


--if not exists(SELECT MasterIFSLocation.MasterIFSLocationID, MasterIFSLocationStatus.Status FROM MasterIFSLocation INNER JOIN MasterIFSLocationStatus ON MasterIFSLocation.StatusID = MasterIFSLocationStatus.MasterIFSLocationStatusID
--              WHERE (MasterIFSLocationStatus.Status = 'Active') and  IFSLocation = @IFSLocation)
--   begin 
--   Select @ReturnMessage = 'Error:IFS Location Invalid'   
--   return
--   end


if not exists(Select * from ReceiveDetail where ESN = @IMEI and Version = '000')
   begin 
   Select @ReturnMessage = 'Error:No IMEI Found'   
   return
   end


Declare @mOrderDetailID numeric(18),
        @mReceiveDetailID numeric(18),
        @mSite nvarchar(5),
        @mProjectID nvarchar(10),
        @mSku nvarchar(25),
        @mlocation nvarchar(20),        
        @mCondition nvarchar(50),                        
        @DirectiveIgnore smallint




SELECT @mReceiveDetailID = ReceiveDetailID, @mSite = ClientLocation.IFSSite, @mProjectID = ClientLocation.IFSProject, @mSku = ReceiveDetail.SKU, @mCondition = ReceiveDetail.IFSCondition, @mlocation = ReceiveDetail.IFSLocation
  FROM ReceiveDetail
 INNER JOIN ClientLocation ON ClientLocation.ClientLocationID = ReceiveDetail.ClientLocationID
 where ESN = @IMEI and Version = '000' 
 
 
 if exists(Select * from OrderDetailReceiveDetail where ReceiveDetailID = @mReceiveDetailID)
   begin 
   Select @ReturnMessage = 'Error:IMEI already packed on a Order'   
   return
   end
   
   

select CONVERT(numeric(18), 0) as Processed
      ,Convert(numeric(18), 0) as NumberQuestions
      ,Convert(numeric(18), 0) as NumberOptions
      ,Convert(numeric(18), 0) as NumberMatches
      ,Convert(numeric(18), 0) as PassIsZero
      ,Convert(nvarchar(500), 0) as Matches 
      ,Convert(nvarchar(500), 0) as Misses
      , * 
into #TempPick      
from OrderDetail where OrderHeaderID = @OrderHeaderID and OrderDetail.QTY - OrderDetail.QTYPacked > 0

Declare @id numeric(18)
Declare @CodeList nvarchar(500)

Declare @NumberQuestions  numeric(18)
Declare @NumberOptions  numeric(18)
Declare @NumberMatches  numeric(18)
Declare @PassIsZero  numeric(18)
Declare @Matches nvarchar(500)
Declare @Misses nvarchar(500) 

Select @NumberQuestions = 0
Select @NumberOptions = 0
Select @NumberMatches = 0
Select @PassIsZero = 0
Select @Matches = ''
Select @Misses = ''


-- Pick the correct line for the IMEI
While exists(Select * from #TempPick where Processed = 0)
      begin
      Select top 1 @id = OrderDetailID, @CodeList = #TempPick.Desc_Code from #TempPick where Processed = 0
      exec dbo.DoesAttrMatch_ScanKey @CodeList, @mReceiveDetailID, @NumberQuestions OUTPUT, @NumberOptions OUTPUT, @NumberMatches OUTPUT, @PassIsZero OUTPUT, @Matches OUTPUT, @Misses OUTPUT
      update #TempPick set Processed = 1, NumberMatches = @NumberMatches, NumberOptions = @NumberOptions, NumberQuestions = @NumberQuestions, PassIsZero = @PassIsZero, Matches = @Matches, Misses = @Misses
      where OrderDetailID = @id
      end

Select top 1 @mOrderDetailID = OrderDetailID from #TempPick where PassIsZero = 0 order by NumberQuestions, PassIsZero
------------------------------------------------------------------------------------------------------------------------------
                     
if ISNULL(@mOrderDetailID, -1) < 1
   begin                    
   Select @ReturnMessage = 'Error:Unable to locate Available Line on Sales Order'   
   return   
   End
                      
Select @DirectiveIgnore = dbo.GetIFSDirective('Ignore',-1)


INSERT INTO [dbo].[OrderDetailReceiveDetail]
           ([OrderDetailID]
           ,[ReceiveDetailID]
           ,[ESN]
           ,[SKU]
           ,[CreateDate]
           ,[CreateUser]
           ,[LastUpdateDate]
           ,[LastUpdateUser]
           ,[Message]
           ,[IFSSite]
           ,[IFSProject]
           ,[IFSSKU]
           ,[IFSLocation]
           ,[IFSCondition]
           ,[IFSConditionCode])
Values (@mOrderDetailID, @mReceiveDetailID, @IMEI, @CartonNumber, GETDATE(), @mUserName, GETDATE(), @mUserName, '',
        @mSite,@mProjectID,@mSku,@mlocation,@mCondition,@mCondition)

Update OrderDetail set QTYPacked = QTYPacked + 1 where OrderDetailID = @mOrderDetailID

Update ReceiveDetail set IFSLocation = @mlocation, ISFTransactionDirective = @DirectiveIgnore where ReceiveDetailID = @mReceiveDetailID



--SELECT     OrderHeader.OrderHeaderID, OrderHeader.Site, OrderDetail.Location, OrderDetail.Condition, OrderDetail.Project_ID, OrderDetail.IFSSKU, OrderDetail.Line_NO, 
--                      OrderDetail.OrderDetailID, OrderDetail.QTY, OrderDetail.QTYPacked, OrderDetail.QTY - OrderDetail.QTYPacked as QTYLeft
--FROM         OrderHeader INNER JOIN
--                      OrderDetail ON OrderHeader.OrderHeaderID = OrderDetail.OrderHeaderID


-- Find the right Line

Declare @OriginalQTY numeric(18)
Declare @PackedQTY numeric(18)
Declare @LeftQTY numeric(18)


SELECT     @OriginalQTY = SUM(isnull(QTY,0)), @PackedQTY = SUM(isnull(QTYPacked,0))
FROM         OrderDetail
where   (OrderHeaderID = @OrderHeaderID)
GROUP BY OrderHeaderID

Select @OriginalQTY = ISNULL(@OriginalQTY, 0)
Select @PackedQTY = ISNULL(@PackedQTY, 0)

Select @LeftQTY = @OriginalQTY - @PackedQTY

Select @ReturnMessage = CONVERT(nvarchar(10),@OriginalQTY) + ':' + CONVERT(nvarchar(10),@PackedQTY) + ':' + CONVERT(nvarchar(10),@LeftQTY) + ':' + @IMEI + ' Packed'
   
END

Go
