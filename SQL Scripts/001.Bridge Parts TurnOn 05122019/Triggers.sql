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


/****** Object:  Trigger [dbo].[RecordStorageChange]    Script Date: 05/12/2019 10:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

Create TRIGGER [dbo].[RecordStorageChange]
   ON  [dbo].[PartNumberBucketInventorySource]
  AFTER UPDATE, INSERT
AS 
BEGIN

	
/*

Select * from MasterPartsTableIFSLocationStorage


*/	
	
	
SET NOCOUNT ON;
    
IF (SELECT COUNT(*) FROM inserted) > 0 and (SELECT COUNT(*) FROM Deleted) < 1
 BEGIN

       INSERT INTO [dbo].[MasterPartsTableIFSLocationStorage]
           ([MasterPartsLinkTableID]
           ,[MasterIFSLocationID]
           ,[QTY]
           ,[CreateDate]
           ,[CreateUser]
           ,[LastUpdateDate]
           ,[LastUpdateUser])
       Select MasterPartsLinkTableID
             ,MasterIFSLocationID
             ,0
             , LastUpdateDate
             , LastUpdateUser
             , LastUpdateDate
             , LastUpdateUser
      from inserted where MasterIFSLocationID not in (select MasterIFSLocationID from [MasterPartsTableIFSLocationStorage] where MasterPartsTableIFSLocationStorage.MasterIFSLocationID = inserted.MasterIFSLocationID and MasterPartsTableIFSLocationStorage.MasterPartsLinkTableID = inserted.MasterPartsLinkTableID)
      
      Insert into [MasterPartsTableIFSLocationStorageMoveLog]  (FromMasterPartsTableIFSLocationStorageID
                                                             ,ToMasterPartsTableIFSLocationStorageID
                                                             ,PartNumberBucketInventorySourceID
                                                             ,PartNumberBucketInventoryPlacementID
                                                             ,MasterPartsTechAssignedLogID
                                                             ,QTY
                                                             ,CreateDate
                                                             ,CreateUser)
    Select MasterPartsTableIFSLocationStorageID
          ,MasterPartsTableIFSLocationStorageID
          ,PartNumberBucketInventorySourceID
          ,null
          ,null
          ,inserted.Quantity
          ,GETDATE()
          ,inserted.LastUpdateUser
          
     from inserted   
     inner join MasterPartsTableIFSLocationStorage on MasterPartsTableIFSLocationStorage.MasterIFSLocationID = inserted.MasterIFSLocationID
     and MasterPartsTableIFSLocationStorage.MasterPartsLinkTableID = inserted.MasterPartsLinkTableID
     
     
     Update MasterPartsTableIFSLocationStorage set QTY = QTY + inserted.quantity
     from inserted   
     inner join MasterPartsTableIFSLocationStorage on MasterPartsTableIFSLocationStorage.MasterIFSLocationID = inserted.MasterIFSLocationID 
     and MasterPartsTableIFSLocationStorage.MasterPartsLinkTableID = inserted.MasterPartsLinkTableID
  
   RETURN
 END

    -- Insert statements for trigger here

END
Go


/****** Object:  Trigger [dbo].[RecordStorageChangePlacement]    Script Date: 05/16/2019 12:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

Create TRIGGER [dbo].[RecordStorageChangePlacement]
   ON  [dbo].[PartNumberBucketInventoryPlacement]
  AFTER UPDATE, INSERT
AS 
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;



IF (SELECT COUNT(*) FROM inserted) > 0 and (SELECT COUNT(*) FROM Deleted) < 1
 BEGIN

       INSERT INTO [dbo].[MasterPartsTableIFSLocationStorage]
           ([MasterPartsLinkTableID]
           ,[MasterIFSLocationID]
           ,[QTY]
           ,[CreateDate]
           ,[CreateUser]
           ,[LastUpdateDate]
           ,[LastUpdateUser])
       Select MasterPartsLinkTableID
             ,MasterIFSLocationID
             ,0
             , LastUpdateDate
             , LastUpdateUser
             , LastUpdateDate
             , LastUpdateUser
      from inserted where MasterIFSLocationID not in (select MasterIFSLocationID from [MasterPartsTableIFSLocationStorage] where MasterPartsTableIFSLocationStorage.MasterIFSLocationID = inserted.MasterIFSLocationID and MasterPartsTableIFSLocationStorage.MasterPartsLinkTableID = inserted.MasterPartsLinkTableID)
      
      Insert into [MasterPartsTableIFSLocationStorageMoveLog]  (FromMasterPartsTableIFSLocationStorageID
                                                             ,ToMasterPartsTableIFSLocationStorageID
                                                             ,PartNumberBucketInventorySourceID
                                                             ,PartNumberBucketInventoryPlacementID
                                                             ,MasterPartsTechAssignedLogID
                                                             ,QTY
                                                             ,CreateDate
                                                             ,CreateUser)
    Select MasterPartsTableIFSLocationStorageID
          ,MasterPartsTableIFSLocationStorageID
          ,null
          ,PartNumberBucketInventoryPlacementID
          ,MasterPartsTechAssignedLogID
          ,inserted.Quantity
          ,GETDATE()
          ,inserted.LastUpdateUser
     from inserted   
     inner join MasterPartsTableIFSLocationStorage on MasterPartsTableIFSLocationStorage.MasterIFSLocationID = inserted.MasterIFSLocationID
       and MasterPartsTableIFSLocationStorage.MasterPartsLinkTableID = inserted.MasterPartsLinkTableID     
     
     
     
     Update MasterPartsTableIFSLocationStorage set QTY = QTY + inserted.quantity
     from inserted   
     inner join MasterPartsTableIFSLocationStorage on MasterPartsTableIFSLocationStorage.MasterIFSLocationID = inserted.MasterIFSLocationID 
       and MasterPartsTableIFSLocationStorage.MasterPartsLinkTableID = inserted.MasterPartsLinkTableID
  
   RETURN
 END

    -- Insert statements for trigger here

END


Go






















