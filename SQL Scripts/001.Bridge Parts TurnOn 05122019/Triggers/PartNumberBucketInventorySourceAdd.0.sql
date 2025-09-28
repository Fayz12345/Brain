
/****** Object:  Trigger [dbo].[PartNumberBucketInventorySourceAdd]    Script Date: 05/16/2019 12:25:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

Create TRIGGER [dbo].[PartNumberBucketInventorySourceAdd]
   ON  [dbo].[PartNumberBucketInventorySource]
  AFTER UPDATE, INSERT
AS 
BEGIN


	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;
	

Declare @mPartNumberBucketInventorySourceID  numeric(18)
Declare @mUserName nvarchar(50)
Declare @List Table (PartNumberBucketInventorySourceID numeric(18))
Declare @ISFTransactionDirective numeric(18)

--Declare   @mMasterPartsLinkTableID numeric(18, 0),
--      @mReceiveDetailID [numeric](18, 0),
--      @mMasterIFSLocationID [numeric](18, 0),            
--      @ToSKUID numeric(18),
--      @QTY smallint,
--      @POCost [numeric](18, 5),
--      @POVendor [nvarchar](50),
--      @PONumber [nvarchar](12),
--      @POReceiptDate Datetime,
--      @POLine [nvarchar](4)

    
IF (SELECT COUNT(*) FROM inserted) > 0 and (SELECT COUNT(*) FROM Deleted) < 1
 BEGIN
    
    While exists (Select PartNumberBucketInventorySourceID from inserted where not PartNumberBucketInventorySourceID in (Select PartNumberBucketInventorySourceID from @List))
    begin
          Select Top 1 @mPartNumberBucketInventorySourceID = PartNumberBucketInventorySourceID, @mUserName = CreateUser, @ISFTransactionDirective = ISNULL(IFSDirective, -1) from inserted where not PartNumberBucketInventorySourceID in (Select PartNumberBucketInventorySourceID from @List)
          insert @List (PartNumberBucketInventorySourceID) values (@mPartNumberBucketInventorySourceID)
          if @ISFTransactionDirective = -1
             Select @ISFTransactionDirective = [dbo].[GetIFSDirective]('Parts_Movement',-1)          
          -- exec IFS_GeneratePartSourceInvtTran @mPartNumberBucketInventorySourceID, @ISFTransactionDirective, @mUserName
          exec IFS_GeneratePartSourceInvtTran @mPartNumberBucketInventorySourceID, @ISFTransactionDirective, @mUserName          
    end 
  
   RETURN
 END

    -- Insert statements for trigger here

END
Go


