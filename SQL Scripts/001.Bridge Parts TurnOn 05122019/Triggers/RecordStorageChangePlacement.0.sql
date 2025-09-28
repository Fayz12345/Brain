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


