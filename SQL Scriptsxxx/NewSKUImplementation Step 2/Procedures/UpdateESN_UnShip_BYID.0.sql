/****** Object:  StoredProcedure [dbo].[UpdateESNAttribute_NoProjectRestriction_BYID]    Script Date: 06/20/2017 14:28:16 ******/
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

Create PROCEDURE [dbo].[UpdateESN_UnShip_BYID]
    @mReceiveDetailID numeric(18),
    @mUserName nvarchar(50) ='XXXX'
    
   
AS
BEGIN
Set NOCOUNT on


Shipto,PSlip,Out-Bound Waybill-S

---- List of Attributes that need to be reset.
exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'Shipto', '', @mUserName
exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'PSlip', '', @mUserName
exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'Out-Bound Waybill-S', '', @mUserName


Update ReceiveDetail set Version = '000'
                       , LastUpdateDate = GETDATE()
                       , LastUpdateUser = @mUserName 
where ReceiveDetailID = @mReceiveDetailID



Return 0

END
