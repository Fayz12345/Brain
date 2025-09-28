/****** Object:  Trigger [dbo].[PartNumberBucketInventoryPlacementAdd]    Script Date: 05/16/2019 12:22:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

Create TRIGGER [dbo].[PartNumberBucketInventoryPlacementAdd]
   ON  [dbo].[PartNumberBucketInventoryPlacement]
  AFTER UPDATE, INSERT
AS 
BEGIN

--Select * from PartNumberBucketInventoryPlacement

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;
  
   
Declare @mPartNumberBucketInventoryPlacementID  numeric(18)
Declare @mUserName nvarchar(50)
Declare @List Table (PartNumberBucketInventoryPlacementID numeric(18))
Declare @ISFTransactionDirective numeric(18)

    
IF (SELECT COUNT(*) FROM inserted) > 0 and (SELECT COUNT(*) FROM Deleted) < 1
 BEGIN
    
    While exists (Select PartNumberBucketInventoryPlacementID from inserted where not PartNumberBucketInventoryPlacementID in (Select PartNumberBucketInventoryPlacementID from @List))
    begin
          Select Top 1 @mPartNumberBucketInventoryPlacementID = PartNumberBucketInventoryPlacementID, @mUserName = CreateUser, @ISFTransactionDirective = ISNULL(IFSDirective, -1) from inserted where not PartNumberBucketInventoryPlacementID in (Select PartNumberBucketInventoryPlacementID from @List)
          insert @List (PartNumberBucketInventoryPlacementID) values (@mPartNumberBucketInventoryPlacementID)
          
          if @ISFTransactionDirective = -1
             Select @ISFTransactionDirective = [dbo].[GetIFSDirective]('Parts_Movement',-1)          
          
          exec IFS_GeneratePartPlacementInvtTran @mPartNumberBucketInventoryPlacementID, @ISFTransactionDirective, @mUserName
    end 
  
   RETURN
 END

    -- Insert statements for trigger here

END


Go


