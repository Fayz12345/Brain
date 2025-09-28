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


