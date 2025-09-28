

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

CREATE TRIGGER [dbo].[RecordStorageChangePlacement]
   ON  [dbo].[PartNumberBucketInventoryPlacement]
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
          ,null
          ,PartNumberBucketInventoryPlacementID
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



/****** Object:  Trigger [dbo].[ProcessStepChange]    Script Date: 05/11/2015 12:38:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

ALTER TRIGGER [dbo].[ProcessStepChange]
   ON  [dbo].[ReceiveDetail]
  AFTER UPDATE, INSERT
AS 
BEGIN


	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;
	
	
Declare @mReceiveDetailID numeric(18)
Declare @mUserName nvarchar(50)
Declare @List Table (ReceiveDetailID numeric(18))
Declare @mS nvarchar(1)
Declare @mL nvarchar(1)
Declare @mC nvarchar(1)
Select @mS = ' '
Select @mL = ' '
Select @mC = ' '



    
IF (SELECT COUNT(*) FROM inserted) > 0 and (SELECT COUNT(*) FROM Deleted) < 1
 BEGIN
    Insert into ReceiveDetailVersionChangeLog  (ReceiveDetailID,Version,CreateDate,CreateUser)
    Select ReceiveDetailID,Version,LastUpdateDate,LastUpdateUser from inserted 
    
    Insert into ReceiveDetailSKUChangeLog (ReceiveDetailID, SKU, CreateDate,CreateUser)
    Select ReceiveDetailID, SKU, getdate(),LastUpdateUser from inserted 
    where not inserted.SKU is null  
    
     Insert into ReceiveDetailIFSLocationLog (ReceiveDetailID, IFSLocation, MiscText, CreateDate,CreateUser)
    Select ReceiveDetailID, IFSLocation, '', getdate(),LastUpdateUser from inserted 
    where not inserted.IFSLocation is null   
    
    Insert into ReceiveDetailConditionChangeLog (ReceiveDetailID, IFS_Condition, CreateDate,CreateUser)
    Select ReceiveDetailID, IFSCondition, getdate(),LastUpdateUser from inserted 
    where not inserted.IFSCondition is null    
    Select @mS = 'S'
    Select @mL = 'L'
    Select @mC = 'C'
    
    While exists (Select ReceiveDetailID from inserted where not ReceiveDetailID in (Select ReceiveDetailID from @List))
    begin
          Select Top 1 @mReceiveDetailID = ReceiveDetailID, @mUserName = CreateUser from inserted where not ReceiveDetailID in (Select ReceiveDetailID from @List)
          insert @List (ReceiveDetailID) values (@mReceiveDetailID)
          exec IFS_GenerateInvtTran @mReceiveDetailID, @mS, @mL, @mC, @mUserName
    end 
  
   RETURN
 END

IF UPDATE(Version)
   BEGIN
   Insert into ReceiveDetailVersionChangeLog  (ReceiveDetailID,Version,CreateDate,CreateUser)
   Select ReceiveDetailID,Version,LastUpdateDate,LastUpdateUser from inserted 
   --RETURN
   END

IF UPDATE(SKU)
   BEGIN
    Select @mS = 'S'
   Insert into ReceiveDetailSKUChangeLog (ReceiveDetailID, SKU, CreateDate,CreateUser)
   Select ReceiveDetailID, SKU, getdate(),LastUpdateUser from inserted 
   where not inserted.SKU is null   
   --RETURN
   END
   
IF UPDATE(IFSLocation)
   BEGIN
    Select @mL = 'L'
    Insert into ReceiveDetailIFSLocationLog (ReceiveDetailID, IFSLocation, MiscText, CreateDate,CreateUser)
    Select ReceiveDetailID, IFSLocation, '', GETDATE(),LastUpdateUser from inserted 
    where not inserted.IFSLocation is null    
   --RETURN
   END

IF UPDATE(IFSCondition)
   BEGIN
    Select @mC = 'C'
    Insert into ReceiveDetailConditionChangeLog (ReceiveDetailID, IFS_Condition, CreateDate,CreateUser)
    Select inserted.ReceiveDetailID, inserted.IFSCondition, getdate(),inserted.LastUpdateUser from inserted 
    where not inserted.IFSCondition is null 
   END
   
if UPDATE(ProcessID)
   BEGIN
   Declare @mProcessID numeric(18)
   Select @mProcessID = ProcessID from inserted
   Declare @mProcessName nvarchar(20)
   Select @mProcessName = Name from Process where ProcessID = @mProcessID

   Insert into ReceiveDetailProcessLog (ReceiveDetailID,ProcessID,ProcessText,MiscText,CreateDate,CreateUser)
   Select ReceiveDetailID,ProcessID,@mProcessName,'',LastUpdateDate,LastUpdateUser
   from inserted
   END
   
   
   
 IF UPDATE(SKU)or UPDATE(IFSLocation) or UPDATE(IFSCondition)
 BEGIN
    
    While exists (Select ReceiveDetailID from inserted where not ReceiveDetailID in (Select ReceiveDetailID from @List))
    begin
    Select Top 1 @mReceiveDetailID = ReceiveDetailID, @mUserName = CreateUser from inserted where not ReceiveDetailID in (Select ReceiveDetailID from @List)
    insert @List (ReceiveDetailID) values (@mReceiveDetailID)
    exec IFS_GenerateInvtTran @mReceiveDetailID, @mS, @mL, @mC, @mUserName
    end
END
    -- Insert statements for trigger here

END



GO






















