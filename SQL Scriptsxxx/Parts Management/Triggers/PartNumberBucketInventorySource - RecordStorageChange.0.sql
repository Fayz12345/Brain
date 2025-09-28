

/****** Object:  Trigger [dbo].[ProcessStepChange]    Script Date: 05/12/2015 18:55:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE TRIGGER [dbo].[RecordStorageChange]
   ON  [dbo].[PartNumberBucketInventorySource]
  AFTER UPDATE, INSERT
AS 
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;
    
IF (SELECT COUNT(*) FROM inserted) > 0 and (SELECT COUNT(*) FROM Deleted) < 1
 BEGIN

       INSERT INTO [GMP_Data].[dbo].[MasterPartsTableIFSLocationStorage]
           ([MasterPartsTableID]
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
      from inserted where MasterIFSLocationID not in (select MasterIFSLocationID from [MasterPartsTableIFSLocationStorage])
      
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
     
     
     Update MasterPartsTableIFSLocationStorage set QTY = QTY + inserted.quantity
     from inserted   
     inner join MasterPartsTableIFSLocationStorage on MasterPartsTableIFSLocationStorage.MasterIFSLocationID = inserted.MasterIFSLocationID
  
   RETURN
 END

    -- Insert statements for trigger here

END



GO


