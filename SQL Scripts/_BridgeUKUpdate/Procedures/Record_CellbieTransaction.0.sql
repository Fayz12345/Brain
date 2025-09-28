/****** Object:  StoredProcedure [dbo].[Record_CellbieTransaction]    Script Date: 10/16/2019 11:46:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>

/*
Declare @ReturnMessage nvarchar(500)
exec Record_CellbieTransaction 2961, 'Sent','CellbieStatus','api','paramjson','outputjson','info', 'error text', 'errortextinternal', 'Misc Text','jmccomb',@ReturnMessage Output
Print @ReturnMessage


// 2957	358040080423290     LG V30
// 2958	355458060568814     Google Nexus 6
// 2959	356160070900412     Samsung Galaxy S6 Edge
// 2960	990004600575546     BB10


Select * from ReceiveDetailCellbieStatus
Select * from ReceiveDetailCellbieStatusLog
Select * from ReceiveDetailCellbieCommLog
Update ReceiveDetail set ESN = '990004600575546' where ReceiveDetailID = 2960

2961
2960
2959
2958
2957
2956
2955
2954
2953

Select top 30 ReceiveDetailID from receiveDetail where Version = '000' order by ReceiveDetailID desc


*/


-- =============================================
Create PROCEDURE [dbo].[Record_CellbieTransaction]
	@ReceiveDetailID numeric(18, 0),
	@Status nvarchar(20),
	@CellbieStatus nvarchar(20),
	@API [nvarchar] (100),	
	@ParameterJSON [nvarchar](max),
	@OutputJSON [nvarchar](max),
	@TransactionResultJSON [nvarchar](max),
	@ErrorText [nvarchar](max),
	@ErrorInternalText [nvarchar](max),
	--@SendParamAgree nvarchar(20),
	--@SendParamMessage nvarchar(100),
	@MiscText nvarchar(500),
	@UserName nvarchar(50),
	@ReturnMessage nvarchar(500) Output
AS
BEGIN
Set NOCOUNT ON

Select @ReturnMessage = 'Error: Status not set!'
if not exists(Select * from ReceiveDetail where ReceiveDetailID = @ReceiveDetailID)
   begin
   Select @ReturnMessage = 'Error: Device Record not found!'   
   return
   end
   
exec Update_CellbieStatus @ReceiveDetailID, @Status,@MiscText,@UserName,@ReturnMessage Output

if substring(@ReturnMessage,1,5) = 'Error'
   begin
   return
   end


declare @ReceiveDetailCellbieStatusID [numeric](18, 0)
select @ReceiveDetailCellbieStatusID = ReceiveDetailCellbieStatusID from ReceiveDetailCellbieStatus where ReceiveDetailID = @ReceiveDetailID
if ISNULL(@ReceiveDetailCellbieStatusID, -1) < 1
   begin
   select @ReturnMessage = 'Error: Unable to find master Cellbie Status record.'
   return
   end
 
 
 -- Select * from  ReceiveDetailCellbieCommLog
 
 INSERT INTO [ReceiveDetailCellbieCommLog]
           ([ReceiveDetailCellbieStatusID]
           ,[ReceiveDetailID]
           ,[Status]
           ,[API]
           ,[ParameterJSON]
           ,[OutputJSON]
           ,[TransactionResultJSON]
           ,[ErrorText]
           ,[ErrorInternalText]
           ,[MiscText]
           ,[CreateDate]
           ,[CreateUser]
           ,[LastUpdateDate]
           ,[LastUpdateUser])
     VALUES
           (@ReceiveDetailCellbieStatusID
           ,@ReceiveDetailID
           ,@CellbieStatus
           ,@API
           ,@ParameterJSON
           ,@OutputJSON
           ,@TransactionResultJSON
           ,@ErrorText
           ,@ErrorInternalText
           ,@MiscText
           ,getdate()
           ,@UserName
           ,getdate()
           ,@UserName)


   select @ReturnMessage = 'Success: Transaction Added:' + CONVERT(nvarchar(20), @@Identity)
Return 0

END
