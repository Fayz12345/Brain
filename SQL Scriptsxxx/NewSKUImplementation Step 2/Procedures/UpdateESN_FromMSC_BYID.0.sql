
/****** Object:  StoredProcedure [dbo].[UpdateESN_UnShip_BYID]    Script Date: 06/21/2017 12:44:23 ******/
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


*/

Create PROCEDURE [dbo].[UpdateESN_FromMSC_BYID]
    @mReceiveDetailID numeric(18),
    @mUserName nvarchar(50) ='XXXX',
	@mMessage nvarchar(50) output
    
   
AS
BEGIN
Set NOCOUNT on

Select @mMessage = 'Error: Version 8xx not found.'
if exists ( Select * from ReceiveDetail where ReceiveDetailID = @mReceiveDetailID and Version like '8%')
   begin
   Select @mMessage = 'Error: Version 000 already exists.'
   if not exists (Select * from ReceiveDetail where ESN = (Select ESN from ReceiveDetail where ReceiveDetailID = @mReceiveDetailID) and Version = '000')
      begin
      Update ReceiveDetail set Version = '000'
                          , LastUpdateDate = GETDATE()
                          , LastUpdateUser = @mUserName 
      where ReceiveDetailID = @mReceiveDetailID
      Select @mMessage = 'Device moved back from MSC 800.'      
      end
   end

---- List of Attributes that need to be reset.
--exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'Question', 'answer', @mUserName
--exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'Question', 'answer', @mUserName
--exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'Question', 'answer', @mUserName
--exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'Question', 'answer', @mUserName

Return 0

END
