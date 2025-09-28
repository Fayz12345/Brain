/****** Object:  StoredProcedure [dbo].[Update_CellbieStatus]    Script Date: 10/16/2019 11:50:01 ******/
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
exec Update_CellbieStatus -1, 'Send', 'Agree','PramMessage'    ,'Misc Text'    ,'jmccomb',@ReturnMessage Output
Print @ReturnMessage
*/


-- =============================================
Create PROCEDURE [dbo].[Update_CellbieStatus]
	@ReceiveDetailID numeric(18, 0),
	@Status nvarchar(20),
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

 if LEN(@Status) < 1
   begin
   Select @Status = 'Send'
   end

 if @Status != 'Send'
and @Status != 'Sent'
and @Status != 'Success'
and @Status != 'Archive'
and @Status != 'Error'
   begin
   Select @ReturnMessage = 'Error: Invalid Status:' + @Status
   return
   end

 if not exists(Select * from ReceiveDetailCellbieStatus where ReceiveDetailID = @ReceiveDetailID)
    begin
    INSERT INTO [ReceiveDetailCellbieStatus]
               ([ReceiveDetailID],[Status],[MiscText],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
        VALUES (@ReceiveDetailID,@Status,@MiscText,GETDATE(),@UserName,GETDATE(),@UserName)
    Select @ReturnMessage = 'Success: Status Added!'
    end
 else
    begin
    Declare @ReceiveDetailCellbieStatusID numeric(18, 0)
     Select @ReceiveDetailCellbieStatusID = ReceiveDetailCellbieStatusID,
            @MiscText =  case when @MiscText = '..' then MiscText else @MiscText end
       From ReceiveDetailCellbieStatus where ReceiveDetailID = @ReceiveDetailID

     UPDATE [ReceiveDetailCellbieStatus]
        SET [Status] = @Status,[MiscText] = @MiscText,[LastUpdateDate] = GETDATE(),[LastUpdateUser] = @UserName
      WHERE ReceiveDetailCellbieStatusID = @ReceiveDetailCellbieStatusID 
    Select @ReturnMessage = 'Success: Status Updated!'
   end   

 
Return 0

END
